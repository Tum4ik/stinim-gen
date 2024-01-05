using System.Collections.Immutable;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Tum4ik.StinimGen.Extensions;
using Tum4ik.StinimGen.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Tum4ik.StinimGen;

[Generator(LanguageNames.CSharp)]
internal sealed partial class IIGenerator : IIncrementalGenerator
{
  private static readonly string s_iiForAttributeFullName = "Tum4ik.StinimGen.Attributes.IIForAttribute";
  private const string Indentation = "  ";
  private const string InstanceFieldName = "_instance";

  public void Initialize(IncrementalGeneratorInitializationContext context)
  {
    //System.Diagnostics.Debugger.Launch();

    var interfacesProvider = context.SyntaxProvider.ForAttributeWithMetadataName(
      s_iiForAttributeFullName,
      static (node, _) => node is InterfaceDeclarationSyntax interfaceDeclaration
                          && (interfaceDeclaration.AttributeLists.Count > 0
                              || interfaceDeclaration.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword))
                             ),
      static (ctx, _) =>
      {
        var interfaceNamedTypeSymbol = (INamedTypeSymbol) ctx.TargetSymbol;
        var attributeArguments = ctx.TargetNode
          .DescendantNodes()
          .First(n => n.IsKind(SyntaxKind.Attribute))
          .DescendantNodes()
          .OfType<AttributeArgumentSyntax>()
          .ToImmutableArray();
        if (attributeArguments.Length != 2)
        {
          throw new ArgumentException("Incorrect amount of the declared attribute arguments.");
        }

        var sourceNamedTypeSymbol = ctx.SemanticModel
          .GetSymbolInfo(((TypeOfExpressionSyntax) attributeArguments[0].Expression).Type)
          .Symbol as INamedTypeSymbol;
        if (sourceNamedTypeSymbol is null)
        {
          throw new ArgumentException("Can not get the class named type symbol.");
        }

        var wrapperClassName = (attributeArguments[1].Expression as LiteralExpressionSyntax)?.Token.ValueText;
        if (wrapperClassName is null)
        {
          throw new ArgumentException("Incorrect wrapper class name.");
        }

        var containsDynamicFields = false;
        var propertyForFieldInfoList = new List<Models.PropertyInfo>();
        var propertyInfoList = new List<Models.PropertyInfo>();
        var eventInfoList = new List<Models.EventInfo>();
        var methodInfoList = new List<Models.MethodInfo>();

        var publicMembers = sourceNamedTypeSymbol
          .GetMembersIncludingBaseTypes(m => m.DeclaredAccessibility == Accessibility.Public && m.IsStatic);
        foreach (var member in publicMembers)
        {
          switch (member)
          {
            case IFieldSymbol fieldSymbol:
              if (!fieldSymbol.IsStatic)
              {
                containsDynamicFields = true;
              }
              propertyForFieldInfoList.Add(fieldSymbol.ToFieldInfo());
              break;
            case IPropertySymbol propertySymbol:
              var hasGetter = propertySymbol.GetMethod is not null
                           && propertySymbol.GetMethod.DeclaredAccessibility == Accessibility.Public;
              var hasSetter = propertySymbol.SetMethod is not null
                           && propertySymbol.SetMethod.DeclaredAccessibility == Accessibility.Public;
              if (!hasGetter && !hasSetter)
              {
                continue;
              }
              propertyInfoList.Add(new(
                propertySymbol.Type.GetFullyQualifiedNameWithNullabilityAnnotations(),
                propertySymbol.Name,
                propertySymbol.IsStatic,
                hasGetter,
                hasSetter
              ));
              break;
            case IEventSymbol eventSymbol:
              eventInfoList.Add(new(
                eventSymbol.Type.GetFullyQualifiedNameWithNullabilityAnnotations(),
                eventSymbol.Name,
                eventSymbol.IsStatic
              ));
              break;
            case IMethodSymbol methodSymbol:
              if (methodSymbol.MethodKind != MethodKind.Ordinary)
              {
                continue;
              }
              var parameters = methodSymbol.Parameters.Select(p => new Models.ParameterInfo(
                p.Type.GetFullyQualifiedNameWithNullabilityAnnotations(),
                p.Type.TypeKind,
                p.Type.SpecialType,
                p.Name,
                p.RefKind,
                p.IsParams,
                p.IsOptional,
                p.HasExplicitDefaultValue ? p.ExplicitDefaultValue : null
              )).ToImmutableArray();
              methodInfoList.Add(new(
                methodSymbol.ReturnsVoid
                  ? null
                  : methodSymbol.ReturnType.GetFullyQualifiedNameWithNullabilityAnnotations(),
                methodSymbol.Name,
                methodSymbol.IsStatic,
                parameters
              ));
              break;
          }
        }

        return new IIInfo(
          interfaceNamedTypeSymbol.ContainingNamespace.ToDisplayString(new(
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces
          )),
          new(
            interfaceNamedTypeSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
            interfaceNamedTypeSymbol.TypeKind,
            interfaceNamedTypeSymbol.IsRecord
          ),
          new(
            wrapperClassName,
            TypeKind.Class,
            false
          ),
          propertyForFieldInfoList.ToImmutableArray(),
          propertyInfoList.ToImmutableArray(),
          eventInfoList.ToImmutableArray(),
          methodInfoList.ToImmutableArray(),
          sourceNamedTypeSymbol.GetFullyQualifiedMetadataName(),
          sourceNamedTypeSymbol.IsSealed,
          sourceNamedTypeSymbol.IsStatic,
          sourceNamedTypeSymbol.IsReferenceType,
          containsDynamicFields
        );
      }
    );

    context.RegisterSourceOutput(interfacesProvider, Generate);
  }


  private static void Generate(SourceProductionContext ctx, IIInfo iiInfo)
  {
    var interfaceMembers = new List<MemberDeclarationSyntax>();
    var implementationMembers = new List<MemberDeclarationSyntax>();

    var baseListTypes = new List<BaseTypeSyntax>(2);

    if (!iiInfo.IsSourceStatic && (iiInfo.IsSourceSealed || iiInfo.ContainsDynamicFields))
    {
      var modifiers = new List<SyntaxToken>(2)
      {
        Token(SyntaxKind.PrivateKeyword)
      };
      if (iiInfo.IsSourceReferenceType)
      {
        modifiers.Add(Token(SyntaxKind.ReadOnlyKeyword));
      }
      var instanceFieldDeclarationSyntax = FieldDeclaration(
        VariableDeclaration(IdentifierName(iiInfo.SourceFullyQualifiedName))
          .AddVariables(
            VariableDeclarator(Identifier(InstanceFieldName))
              .WithInitializer(
                EqualsValueClause(
                  ImplicitObjectCreationExpression()
                )
              )
          )
        )
        .AddModifiers(modifiers.ToArray());
      implementationMembers.Add(instanceFieldDeclarationSyntax);
    }

    var inherited = false;
    if (!iiInfo.IsSourceStatic && iiInfo.IsSourceReferenceType && !iiInfo.IsSourceSealed)
    {
      // inherit
      baseListTypes.Add(SimpleBaseType(ParseTypeName(iiInfo.SourceFullyQualifiedName)));
      inherited = true;
    }

    baseListTypes.Add(SimpleBaseType(ParseTypeName(iiInfo.InterfaceTypeInfo.QualifiedName)));

    GenerateMembersForFields(iiInfo, interfaceMembers, implementationMembers);
    GenerateMembersForProperties(iiInfo, interfaceMembers, implementationMembers, inherited);
    GenerateMembersForEvents(iiInfo, interfaceMembers, implementationMembers, inherited);
    GenerateMembersForMethods(iiInfo, interfaceMembers, implementationMembers, inherited);

    var attributes = new[]
    {
      AttributeList(SingletonSeparatedList(
        Attribute(IdentifierName("global::System.CodeDom.Compiler.GeneratedCode"))
          .AddArgumentListArguments(
            AttributeArgument(LiteralExpression(
              SyntaxKind.StringLiteralExpression,
              Literal(Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product))
            ),
            AttributeArgument(LiteralExpression(
              SyntaxKind.StringLiteralExpression,
              Literal(Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version))
            )
          )
      )),
      AttributeList(SingletonSeparatedList(
        Attribute(IdentifierName("global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage"))
      ))
    };

    var interfaceTypeDeclarationSyntax = iiInfo.InterfaceTypeInfo
      .GetSyntax()
      .AddModifiers(Token(SyntaxKind.PartialKeyword))
      .AddMembers(interfaceMembers.ToArray());

    var implementationTypeDeclarationSyntax = iiInfo.ImplementationTypeInfo
      .GetSyntax()
      .AddAttributeLists(attributes)
      .AddModifiers(Token(SyntaxKind.InternalKeyword))
      .AddMembers(implementationMembers.ToArray())
      .AddBaseListTypes(baseListTypes.ToArray());

    AddSource(
      ctx,
      $"{iiInfo.Namespace}.{iiInfo.InterfaceTypeInfo.QualifiedName}.g.cs",
      iiInfo.Namespace,
      interfaceTypeDeclarationSyntax
    );
    AddSource(
      ctx,
      $"{iiInfo.Namespace}.{iiInfo.ImplementationTypeInfo.QualifiedName}.g.cs",
      iiInfo.Namespace,
      implementationTypeDeclarationSyntax
    );
  }


  private static void GenerateMembersForFields(IIInfo iiInfo,
                                               List<MemberDeclarationSyntax> interfaceMembers,
                                               List<MemberDeclarationSyntax> implementationMembers)
  {
    foreach (var fieldInfo in iiInfo.PropertyForFieldInfoList)
    {
      interfaceMembers.Add(Execute.GetInterfacePropertySyntax(fieldInfo));
      implementationMembers.Add(Execute.GetImplementationPropertySyntax(fieldInfo, iiInfo));
    }
  }


  private static void GenerateMembersForProperties(IIInfo iiInfo,
                                                   List<MemberDeclarationSyntax> interfaceMembers,
                                                   List<MemberDeclarationSyntax> implementationMembers,
                                                   bool inherited)
  {
    foreach (var propertyInfo in iiInfo.PropertyInfoList)
    {
      interfaceMembers.Add(Execute.GetInterfacePropertySyntax(propertyInfo));

      if (inherited && !propertyInfo.IsStatic)
      {
        continue;
      }

      implementationMembers.Add(Execute.GetImplementationPropertySyntax(propertyInfo, iiInfo));
    }
  }


  private static void GenerateMembersForEvents(IIInfo iiInfo,
                                               List<MemberDeclarationSyntax> interfaceMembers,
                                               List<MemberDeclarationSyntax> implementationMembers,
                                               bool inherited)
  {
    foreach (var eventInfo in iiInfo.EventInfoList)
    {
      interfaceMembers.Add(Execute.GetInterfaceEventSyntax(eventInfo));

      if (inherited && !eventInfo.IsStatic)
      {
        continue;
      }

      implementationMembers.Add(Execute.GetImplementationEventSyntax(eventInfo, iiInfo));
    }
  }


  private static void GenerateMembersForMethods(IIInfo iiInfo,
                                                List<MemberDeclarationSyntax> interfaceMembers,
                                                List<MemberDeclarationSyntax> implementationMembers,
                                                bool inherited)
  {
    foreach (var methodInfo in iiInfo.MethodInfoList)
    {
      interfaceMembers.Add(Execute.GetInterfaceMethodSyntax(methodInfo));

      if (inherited && !methodInfo.IsStatic)
      {
        continue;
      }

      implementationMembers.Add(Execute.GetImplementationMethodSyntax(methodInfo,iiInfo));
    }
  }


  private static void AddSource(SourceProductionContext ctx,
                                string hintName,
                                string nameSpace,
                                BaseTypeDeclarationSyntax typeDeclarationSyntax)
  {
    var compilationUnit = CompilationUnit()
      .AddMembers(
        FileScopedNamespaceDeclaration(IdentifierName(nameSpace))
          .WithLeadingTrivia(
            Comment("// <auto-generated/>"),
            Trivia(NullableDirectiveTrivia(Token(SyntaxKind.EnableKeyword), true))
          )
          .AddMembers(typeDeclarationSyntax)
      )
      .NormalizeWhitespace(indentation: Indentation);

    var source = compilationUnit.GetText(Encoding.UTF8);
    ctx.AddSource(hintName, source);
  }
}
