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
  private static readonly string s_indentation = "  ";

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

        var propertyForFieldInfoList = fields
          .Select(f => new PropertyInfo(
            f.Type.GetFullyQualifiedNameWithNullabilityAnnotations(),
            f.Name,
            f.IsStatic,
            !f.IsConst && !f.IsReadOnly
          ))
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
          sourceNamedTypeSymbol.IsReferenceType
        );
      }
    );

    context.RegisterSourceOutput(interfacesProvider, static (ctx, iiInfo) =>
    {
      var interfacePropertyForFieldDeclarations = iiInfo.PropertyForFieldInfoList
        .Select(Execute.GetInterfacePropertySyntax);

      var interfaceTypeDeclarationSyntax = iiInfo.InterfaceTypeInfo
        .GetSyntax()
        .AddModifiers(Token(SyntaxKind.PartialKeyword))
        .AddMembers(interfacePropertyForFieldDeclarations.ToArray());

      var implementationTypeDeclarationSyntax = iiInfo.ImplementationTypeInfo
        .GetSyntax()
        .AddModifiers(Token(SyntaxKind.InternalKeyword));
      var baseListTypes = new List<BaseTypeSyntax>(2);
      if (iiInfo.IsSourceStatic)
      {
        var implementationPropertyForFieldDeclarations = iiInfo.PropertyForFieldInfoList
          .Select(pi => Execute.GetImplementationPropertySyntax(pi, iiInfo.SourceFullyQualifiedName));
        implementationTypeDeclarationSyntax = implementationTypeDeclarationSyntax
          .AddMembers(implementationPropertyForFieldDeclarations.ToArray());
      }
      else
      {
        const string InstanceFieldName = "_instance";

        var implementationPropertyForFieldDeclarations = iiInfo.PropertyForFieldInfoList
          .Select(pi => Execute.GetImplementationPropertySyntax(
            pi,
            pi.IsStatic ? iiInfo.SourceFullyQualifiedName : InstanceFieldName,
            isNewKeywordRequired: !iiInfo.IsSourceSealed
          ));

        if (iiInfo.IsSourceSealed || iiInfo.PropertyForFieldInfoList.Any(f => !f.IsStatic))
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
          implementationTypeDeclarationSyntax = implementationTypeDeclarationSyntax
            .AddMembers(instanceFieldDeclarationSyntax);
        }

        implementationTypeDeclarationSyntax = implementationTypeDeclarationSyntax
          .AddMembers(implementationPropertyForFieldDeclarations.ToArray());
        if (iiInfo.IsSourceReferenceType && !iiInfo.IsSourceSealed)
        {
          // inherit
          baseListTypes.Add(SimpleBaseType(ParseTypeName(iiInfo.SourceFullyQualifiedName)));
        }
        else
        {
          // add properties, events, methods

        }
      }

      baseListTypes.Add(SimpleBaseType(ParseTypeName(iiInfo.InterfaceTypeInfo.QualifiedName)));

      implementationTypeDeclarationSyntax = (TypeDeclarationSyntax) implementationTypeDeclarationSyntax
        .AddBaseListTypes(baseListTypes.ToArray());

      var namespaceDeclarationSyntax = FileScopedNamespaceDeclaration(IdentifierName(iiInfo.Namespace))
        .WithLeadingTrivia(
          Comment("// <auto-generated/>"),
          Trivia(NullableDirectiveTrivia(Token(SyntaxKind.EnableKeyword), true))
        );

      var interfaceCompilationUnit = CompilationUnit()
        .AddMembers(
          namespaceDeclarationSyntax
            .AddMembers(interfaceTypeDeclarationSyntax)
        )
        .NormalizeWhitespace(indentation: s_indentation);

      var implementationCompilationUnit = CompilationUnit()
        .AddMembers(
          namespaceDeclarationSyntax
            .AddMembers(implementationTypeDeclarationSyntax)
        )
        .NormalizeWhitespace(indentation: s_indentation);

      var interfaceSource = interfaceCompilationUnit.GetText(Encoding.UTF8);
      var implementationSource = implementationCompilationUnit.GetText(Encoding.UTF8);
      ctx.AddSource($"{iiInfo.Namespace}.{iiInfo.InterfaceTypeInfo.QualifiedName}.g.cs", interfaceSource);
      ctx.AddSource($"{iiInfo.Namespace}.{iiInfo.ImplementationTypeInfo.QualifiedName}.g.cs", implementationSource);
    });
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
