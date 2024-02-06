using System.Collections.Immutable;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Tum4ik.StinimGen.Extensions;
using Tum4ik.StinimGen.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Tum4ik.StinimGen;

[Generator(LanguageNames.CSharp)]
internal sealed partial class IIGenerator : IIncrementalGenerator
{
  private static readonly string s_iiForAttributeFullName = "Tum4ik.StinimGen.Attributes.IIForAttribute";
  private const string Indentation = "  ";


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
        if (attributeArguments.Length < 2)
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

        string? wrapperClassName = null;
        var isPublic = false;
        var isSealed = true;
        for (var i = 1; i < attributeArguments.Length; i++)
        {
          var attributeArgument = attributeArguments[i];
          var argumentName = attributeArgument.NameEquals?.Name.Identifier.ValueText;
          var argumentValue = (attributeArgument.Expression as LiteralExpressionSyntax)?.Token.ValueText;
          if (argumentName is null || argumentValue is null)
          {
            continue;
          }

          switch (argumentName)
          {
            case "WrapperClassName":
              wrapperClassName = argumentValue;
              break;
            case "IsPublic":
              isPublic = bool.Parse(argumentValue);
              break;
            case "IsSealed":
              isSealed = bool.Parse(argumentValue);
              break;
          }
        }

        if (wrapperClassName is null)
        {
          throw new ArgumentException("Incorrect wrapper class name.");
        }

        var propertyForFieldInfoList = new List<Models.PropertyInfo>();
        var propertyInfoList = new List<Models.PropertyInfo>();
        var eventInfoList = new List<Models.EventInfo>();
        var methodInfoList = new List<MethodDeclarationSyntax>();

        var publicMembers = sourceNamedTypeSymbol.GetMembersIncludingBaseTypes(
          m => m.DeclaredAccessibility == Accessibility.Public
               && m.IsStatic
               && !IsObjectType(m.ContainingType)
        );
        var syntaxGenerator = SyntaxGenerator.GetGenerator(new AdhocWorkspace(), LanguageNames.CSharp);
        foreach (var member in publicMembers)
        {
          switch (member)
          {
            case IFieldSymbol fieldSymbol:
            {
              var forwardedAttributes = fieldSymbol.GetObsoleteAttributeSyntaxIfPresent(syntaxGenerator);
              propertyForFieldInfoList.Add(new(
                TypeNameWithNullabilityAnnotations: fieldSymbol.Type.GetFullyQualifiedNameWithNullabilityAnnotations(),
                PropertyName: fieldSymbol.Name,
                HasGetter: true,
                HasSetter: !fieldSymbol.IsConst && !fieldSymbol.IsReadOnly,
                ForwardedAttributes: forwardedAttributes
              ));
              break;
            }
            case IPropertySymbol propertySymbol:
            {
              var hasGetter = propertySymbol.GetMethod is not null
                           && propertySymbol.GetMethod.DeclaredAccessibility == Accessibility.Public;
              var hasSetter = propertySymbol.SetMethod is not null
                           && propertySymbol.SetMethod.DeclaredAccessibility == Accessibility.Public;
              if (!hasGetter && !hasSetter)
              {
                continue;
              }
              var forwardedAttributes = propertySymbol.GetObsoleteAttributeSyntaxIfPresent(syntaxGenerator);
              propertyInfoList.Add(new(
                TypeNameWithNullabilityAnnotations: propertySymbol.Type.GetFullyQualifiedNameWithNullabilityAnnotations(),
                PropertyName: propertySymbol.Name,
                HasGetter: hasGetter,
                HasSetter: hasSetter,
                ForwardedAttributes: forwardedAttributes
              ));
              break;
            }
            case IEventSymbol eventSymbol:
            {
              var forwardedAttributes = eventSymbol.GetObsoleteAttributeSyntaxIfPresent(syntaxGenerator);
              eventInfoList.Add(new(
                TypeNameWithNullabilityAnnotations: eventSymbol.Type.GetFullyQualifiedNameWithNullabilityAnnotations(),
                EventName: eventSymbol.Name,
                ForwardedAttributes: forwardedAttributes
              ));
              break;
            }
            case IMethodSymbol methodSymbol:
            {
              if (methodSymbol.MethodKind != MethodKind.Ordinary)
              {
                continue;
              }
              var forwardedAttributes = methodSymbol.GetObsoleteAttributeSyntaxIfPresent(syntaxGenerator);
              var methodDeclarationSyntax = ((MethodDeclarationSyntax) syntaxGenerator.MethodDeclaration(methodSymbol))
                .WithExplicitInterfaceSpecifier(null)
                .AddAttributeLists(forwardedAttributes);
              methodInfoList.Add(methodDeclarationSyntax);
              break;
            }
          }
        }

        var sourceForwardedAttributes = sourceNamedTypeSymbol.GetObsoleteAttributeSyntaxIfPresent(syntaxGenerator);

        return new IIInfo(
          Namespace: interfaceNamedTypeSymbol.ContainingNamespace.ToDisplayString(new(
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces
          )),
          InterfaceTypeInfo: new(
            interfaceNamedTypeSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
            interfaceNamedTypeSymbol.TypeKind,
            interfaceNamedTypeSymbol.IsRecord
          ),
          ImplementationTypeInfo: new(
            wrapperClassName,
            TypeKind.Class,
            false
          ),
          ImplementationModifiers: new(isPublic, isSealed),
          PropertyForFieldInfoList: propertyForFieldInfoList.ToImmutableArray(),
          PropertyInfoList: propertyInfoList.ToImmutableArray(),
          EventInfoList: eventInfoList.ToImmutableArray(),
          MethodInfoList: methodInfoList.ToImmutableArray(),
          SourceFullyQualifiedName: sourceNamedTypeSymbol.GetFullyQualifiedMetadataName(),
          SourceForwardedAttributes: sourceForwardedAttributes
        );
      }
    );

    context.RegisterSourceOutput(interfacesProvider, Generate);
  }


  private static bool IsObjectType(INamedTypeSymbol namedTypeSymbol)
  {
    var fullyQualifiedName = namedTypeSymbol.ToDisplayString(
      SymbolDisplayFormat.FullyQualifiedFormat
        .RemoveMiscellaneousOptions(SymbolDisplayMiscellaneousOptions.UseSpecialTypes)
    );
    return fullyQualifiedName == "global::System.Object";
  }


  private static void Generate(SourceProductionContext ctx, IIInfo iiInfo)
  {
    var interfaceMembers = new List<MemberDeclarationSyntax>();
    var implementationMembers = new List<MemberDeclarationSyntax>();

    GenerateMembersForFields(iiInfo, interfaceMembers, implementationMembers);
    GenerateMembersForProperties(iiInfo, interfaceMembers, implementationMembers);
    GenerateMembersForEvents(iiInfo, interfaceMembers, implementationMembers);
    GenerateMembersForMethods(iiInfo, interfaceMembers, implementationMembers);

    var executingAssembly = Assembly.GetExecutingAssembly();
    var generatedCodeAttribute = AttributeList(SingletonSeparatedList(
      Attribute(IdentifierName("global::System.CodeDom.Compiler.GeneratedCode"))
        .AddArgumentListArguments(
          AttributeArgument(LiteralExpression(
            SyntaxKind.StringLiteralExpression,
            Literal(executingAssembly.GetCustomAttribute<AssemblyProductAttribute>().Product))
          ),
          AttributeArgument(LiteralExpression(
            SyntaxKind.StringLiteralExpression,
            Literal(executingAssembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version))
          )
        )
    ));
    var excludeFromCodeCoverageAttribute = AttributeList(SingletonSeparatedList(
      Attribute(IdentifierName("global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage"))
    ));

    var interfaceTypeDeclarationSyntax = iiInfo.InterfaceTypeInfo
      .GetSyntax()
      .AddAttributeLists(generatedCodeAttribute)
      .AddAttributeLists(iiInfo.SourceForwardedAttributes)
      .AddModifiers(Token(SyntaxKind.PartialKeyword))
      .AddMembers([.. interfaceMembers]);

    var implementationModifiers = new List<SyntaxToken>(2);
    var accessibilityModifier = iiInfo.ImplementationModifiers.IsPublic
      ? SyntaxKind.PublicKeyword
      : SyntaxKind.InternalKeyword;
    implementationModifiers.Add(Token(accessibilityModifier));
    if (iiInfo.ImplementationModifiers.IsSealed)
    {
      implementationModifiers.Add(Token(SyntaxKind.SealedKeyword));
    }
    var implementationTypeDeclarationSyntax = iiInfo.ImplementationTypeInfo
      .GetSyntax()
      .AddAttributeLists(generatedCodeAttribute, excludeFromCodeCoverageAttribute)
      .AddAttributeLists(iiInfo.SourceForwardedAttributes)
      .AddModifiers([.. implementationModifiers])
      .AddMembers([.. implementationMembers])
      .AddBaseListTypes(SimpleBaseType(ParseTypeName(iiInfo.InterfaceTypeInfo.QualifiedName)));

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
      interfaceMembers.Add(Execute.GetInterfacePropertySyntax(fieldInfo, iiInfo));
      implementationMembers.Add(Execute.GetImplementationPropertySyntax(fieldInfo, iiInfo));
    }
  }


  private static void GenerateMembersForProperties(IIInfo iiInfo,
                                                   List<MemberDeclarationSyntax> interfaceMembers,
                                                   List<MemberDeclarationSyntax> implementationMembers)
  {
    foreach (var propertyInfo in iiInfo.PropertyInfoList)
    {
      interfaceMembers.Add(Execute.GetInterfacePropertySyntax(propertyInfo, iiInfo));
      implementationMembers.Add(Execute.GetImplementationPropertySyntax(propertyInfo, iiInfo));
    }
  }


  private static void GenerateMembersForEvents(IIInfo iiInfo,
                                               List<MemberDeclarationSyntax> interfaceMembers,
                                               List<MemberDeclarationSyntax> implementationMembers)
  {
    foreach (var eventInfo in iiInfo.EventInfoList)
    {
      interfaceMembers.Add(Execute.GetInterfaceEventSyntax(eventInfo, iiInfo));
      implementationMembers.Add(Execute.GetImplementationEventSyntax(eventInfo, iiInfo));
    }
  }


  private static void GenerateMembersForMethods(IIInfo iiInfo,
                                                List<MemberDeclarationSyntax> interfaceMembers,
                                                List<MemberDeclarationSyntax> implementationMembers)
  {
    foreach (var methodInfo in iiInfo.MethodInfoList)
    {
      interfaceMembers.Add(Execute.GetInterfaceMethodSyntax(methodInfo, iiInfo));
      implementationMembers.Add(Execute.GetImplementationMethodSyntax(methodInfo, iiInfo));
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
