using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using YT.IIGen.Attributes;
using YT.IIGen.Extensions;
using YT.IIGen.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace YT.IIGen;

[Generator(LanguageNames.CSharp)]
internal sealed partial class IIGenerator : IIncrementalGenerator
{
  private static readonly string s_iiForAttributeFullName = typeof(IIForAttribute).FullName;
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
        var attributeArguments = interfaceNamedTypeSymbol
          .GetAttributes()
          .Single(a => a.AttributeClass?.GetFullyQualifiedMetadataName() == s_iiForAttributeFullName)
          .ConstructorArguments;
        if (attributeArguments.Length != 2)
        {
          throw new ArgumentException("Incorrect amount of the declared attribute arguments.");
        }

        var sourceNamedTypeSymbol = attributeArguments[0].Value as INamedTypeSymbol;
        if (sourceNamedTypeSymbol is null)
        {
          throw new ArgumentException("Can not get the class named type symbol.");
        }

        var wrapperClassName = attributeArguments[1].Value as string;
        if (wrapperClassName is null)
        {
          throw new ArgumentException("Incorrect wrapper class name.");
        }

        var containsDynamicFields = false;
        var propertyForFieldInfoList = new List<PropertyInfo>();
        var propertyInfoList = new List<PropertyInfo>();

        var publicMembers = sourceNamedTypeSymbol
          .GetMembersIncludingBaseTypes(m => m.DeclaredAccessibility == Accessibility.Public && m.IsDefinition);
        foreach (var member in publicMembers)
        {
          switch (member)
          {
            case IFieldSymbol field:
              if (!field.IsStatic)
              {
                containsDynamicFields = true;
              }
              propertyForFieldInfoList.Add(new PropertyInfo(
                field.Type.GetFullyQualifiedNameWithNullabilityAnnotations(),
                field.Name,
                field.IsStatic,
                true,
                !field.IsConst && !field.IsReadOnly
              ));
              break;
            case IPropertySymbol property:
              var hasGetter = property.GetMethod is not null
                           && property.GetMethod.DeclaredAccessibility == Accessibility.Public;
              var hasSetter = property.SetMethod is not null
                           && property.SetMethod.DeclaredAccessibility == Accessibility.Public;
              if (!hasGetter && !hasSetter)
              {
                continue;
              }
              propertyInfoList.Add(new(
                property.Type.GetFullyQualifiedNameWithNullabilityAnnotations(),
                property.Name,
                property.IsStatic,
                hasGetter,
                hasSetter
              ));
              break;
            case IEventSymbol @event:
              break;
            case IMethodSymbol method:
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

    if (!iiInfo.IsSourceStatic && iiInfo.IsSourceReferenceType && !iiInfo.IsSourceSealed)
    {
      // inherit
      baseListTypes.Add(SimpleBaseType(ParseTypeName(iiInfo.SourceFullyQualifiedName)));
    }

    baseListTypes.Add(SimpleBaseType(ParseTypeName(iiInfo.InterfaceTypeInfo.QualifiedName)));

    GenerateMembersForFields(iiInfo, interfaceMembers, implementationMembers);
    GenerateMembersForProperties(iiInfo, interfaceMembers, implementationMembers);

    var interfaceTypeDeclarationSyntax = iiInfo.InterfaceTypeInfo
      .GetSyntax()
      .AddModifiers(Token(SyntaxKind.PartialKeyword))
      .AddMembers(interfaceMembers.ToArray());

    var implementationTypeDeclarationSyntax = iiInfo.ImplementationTypeInfo
      .GetSyntax()
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
      var interfacePropertyForFieldDeclaration = Execute.GetInterfacePropertySyntax(fieldInfo);
      interfaceMembers.Add(interfacePropertyForFieldDeclaration);

      MemberDeclarationSyntax implementationPropertyForFieldDeclaration;
      if (iiInfo.IsSourceStatic)
      {
        implementationPropertyForFieldDeclaration = Execute.GetImplementationPropertySyntax(
          fieldInfo,
          iiInfo.SourceFullyQualifiedName
        );
      }
      else
      {
        implementationPropertyForFieldDeclaration = Execute.GetImplementationPropertySyntax(
          fieldInfo,
          fieldInfo.IsStatic ? iiInfo.SourceFullyQualifiedName : InstanceFieldName,
          isNewKeywordRequired: !iiInfo.IsSourceSealed
        );
      }
      implementationMembers.Add(implementationPropertyForFieldDeclaration);
    }
  }


  private static void GenerateMembersForProperties(IIInfo iiInfo,
                                                   List<MemberDeclarationSyntax> interfaceMembers,
                                                   List<MemberDeclarationSyntax> implementationMembers)
  {
    foreach (var propertyInfo in iiInfo.PropertyInfoList)
    {
      var interfacePropertyDeclaration = Execute.GetInterfacePropertySyntax(propertyInfo);
      interfaceMembers.Add(interfacePropertyDeclaration);

      MemberDeclarationSyntax implementationPropertyDeclaration;
      if (iiInfo.IsSourceStatic)
      {
        implementationPropertyDeclaration = Execute.GetImplementationPropertySyntax(
          propertyInfo,
          iiInfo.SourceFullyQualifiedName
        );
      }
      else
      {
        implementationPropertyDeclaration = Execute.GetImplementationPropertySyntax(
          propertyInfo,
          propertyInfo.IsStatic ? iiInfo.SourceFullyQualifiedName : InstanceFieldName,
          isNewKeywordRequired: !iiInfo.IsSourceSealed
        );
      }
      implementationMembers.Add(implementationPropertyDeclaration);
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
