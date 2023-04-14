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

        // TODO: optimize
        // TODO: also include members from base types recursively
        var fields = sourceNamedTypeSymbol.GetMembers()
          .Where(m => m is IFieldSymbol)
          .Cast<IFieldSymbol>()
          .Where(fs => fs.DeclaredAccessibility == Accessibility.Public);

        var containsDynamicFields = false;
        var propertyForFieldInfoList = fields
          .Select(f =>
          {
            if (!f.IsStatic)
            {
              containsDynamicFields = true;
            }
            return new PropertyInfo(
              f.Type.GetFullyQualifiedNameWithNullabilityAnnotations(),
              f.Name,
              f.IsStatic,
              !f.IsConst && !f.IsReadOnly
            );
          })
          .ToImmutableArray();

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
          propertyForFieldInfoList,
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

      if (iiInfo.IsSourceStatic)
      {
        var implementationPropertyForFieldDeclaration = Execute.GetImplementationPropertySyntax(fieldInfo, iiInfo.SourceFullyQualifiedName);
        implementationMembers.Add(implementationPropertyForFieldDeclaration);
      }
      else
      {
        var implementationPropertyForFieldDeclaration = Execute.GetImplementationPropertySyntax(fieldInfo, fieldInfo.IsStatic ? iiInfo.SourceFullyQualifiedName : InstanceFieldName, isNewKeywordRequired: !iiInfo.IsSourceSealed);
        implementationMembers.Add(implementationPropertyForFieldDeclaration);
      }
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


  private static void Generate(SourceProductionContext ctx,
                               (
                                 InterfaceDeclarationSyntax declaredInterfaceSyntax,
                                 CompilationMethods compilation
                               ) combined)
  {
    var (declaredInterfaceSyntax, compilation) = combined;
    var declaredAttributeSyntax = declaredInterfaceSyntax.AttributeLists.SelectMany(al => al.Attributes).First();
    var declaredAttributeArguments = declaredAttributeSyntax.ArgumentList?.Arguments;
    if (!declaredAttributeArguments.HasValue || declaredAttributeArguments.Value.Count != 2)
    {
      throw new ArgumentException("Incorrect amount of the declared attribute arguments.");
    }

    var declaredSourceTypeSyntax = (declaredAttributeArguments.Value[0].Expression as TypeOfExpressionSyntax)?.Type;
    if (declaredSourceTypeSyntax is null)
    {
      throw new ArgumentException("Can not get the class type syntax.");
    }

    var sourceNamedTypeSymbol = compilation.GetSemanticModel(declaredSourceTypeSyntax.SyntaxTree, false)
      .GetSymbolInfo(declaredSourceTypeSyntax)
      .Symbol as INamedTypeSymbol;
    if (sourceNamedTypeSymbol is null)
    {
      throw new ArgumentException("Can not get the class named type symbol.");
    }




    // TODO: optimize
    // TODO: also include members from base types recursively
    var fields = sourceNamedTypeSymbol.GetMembers()
      .Where(m => m is IFieldSymbol)
      .Cast<IFieldSymbol>()
      .Where(fs => fs.DeclaredAccessibility == Accessibility.Public);
    var properties = sourceNamedTypeSymbol.GetMembers()
      .Select(m => m as IPropertySymbol)
      .Where(ps => ps is not null
                && ps.DeclaredAccessibility == Accessibility.Public);
    var events = sourceNamedTypeSymbol.GetMembers()
      .Select(m => m as IEventSymbol)
      .Where(es => es is not null
                && es.DeclaredAccessibility == Accessibility.Public);
    var methods = sourceNamedTypeSymbol.GetMembers()
      .Select(m => m as IMethodSymbol)
      .Where(ms => ms is not null
                && ms.DeclaredAccessibility == Accessibility.Public
                && ms.MethodKind == MethodKind.Ordinary);


  }
}


internal class CompilationMethods
{
  public CompilationMethods(Func<SyntaxTree, bool, SemanticModel> getSemanticModel,
                            Func<string, INamedTypeSymbol?> getTypeByMetadataName)
  {
    GetSemanticModel = getSemanticModel;
    GetTypeByMetadataName = getTypeByMetadataName;
  }

  public Func<SyntaxTree, bool, SemanticModel> GetSemanticModel { get; }
  public Func<string, INamedTypeSymbol?> GetTypeByMetadataName { get; }
}
