using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using YT.IIGen.Attributes;
using YT.IIGen.Templates;

namespace YT.IIGen;

[Generator]
internal class IIGenerator : IIncrementalGenerator
{
  public void Initialize(IncrementalGeneratorInitializationContext context)
  {
    System.Diagnostics.Debugger.Launch();

    var interfacesProvider = context.SyntaxProvider.ForAttributeWithMetadataName(
      typeof(IIForAttribute).ToString(),
      static (syntaxNode, _) => syntaxNode.IsKind(SyntaxKind.InterfaceDeclaration),
      static (ctx, _) => (InterfaceDeclarationSyntax) ctx.TargetNode
    );

    var getSemanticModelProvider = context.CompilationProvider
      .Select<Compilation, CompilationMethods>(static (c, _) => new(c.GetSemanticModel, c.GetTypeByMetadataName));

    var combined = interfacesProvider.Combine(getSemanticModelProvider);

    context.RegisterSourceOutput(combined, Generate);
  }


  private static void Generate(SourceProductionContext ctx,
                               (
                                 InterfaceDeclarationSyntax declarationSyntax,
                                 CompilationMethods compilation
                               ) combined)
  {
    var (declarationSyntax, compilation) = combined;
    var attributeSyntax = declarationSyntax.AttributeLists.SelectMany(al => al.Attributes).First();
    var arguments = attributeSyntax.ArgumentList?.Arguments;
    if (!arguments.HasValue || arguments.Value.Count != 2)
    {
      throw new ArgumentException("Incorrect amount of arguments.");
    }

    var classTypeSyntax = (arguments.Value[0].Expression as TypeOfExpressionSyntax)?.Type;
    if (classTypeSyntax is null)
    {
      throw new ArgumentException("Can not get the class type syntax.");
    }

    var classNamedTypeSymbol = compilation.GetSemanticModel(classTypeSyntax.SyntaxTree, false)
      .GetSymbolInfo(classTypeSyntax)
      .Symbol as INamedTypeSymbol;
    if (classNamedTypeSymbol is null)
    {
      throw new ArgumentException("Can not get the class named type symbol.");
    }
    //var classFullName = classNamedTypeSymbol.ToString();


    var wrapperClassName = (arguments.Value[1].Expression as LiteralExpressionSyntax)?.Token.ValueText;
    if (wrapperClassName is null)
    {
      throw new ArgumentException("Wrapper class name is not provided");
    }

    var nameSpace = compilation.GetSemanticModel(declarationSyntax.SyntaxTree, false)
      .GetDeclaredSymbol(declarationSyntax)?
      .ContainingNamespace
      .ToString();
    if (nameSpace is null)
    {
      throw new ArgumentException("Can not get the interface namespace.");
    }

    var accessModifier = declarationSyntax.Modifiers.ToString();

    // TODO: optimize
    var fields = classNamedTypeSymbol.GetMembers()
      .Where(m => m is IFieldSymbol)
      .Cast<IFieldSymbol>()
      .Where(fs => fs.DeclaredAccessibility == Accessibility.Public);
    var properties = classNamedTypeSymbol.GetMembers()
      .Select(m => m as IPropertySymbol)
      .Where(ps => ps is not null
                && ps.DeclaredAccessibility == Accessibility.Public);
    var events = classNamedTypeSymbol.GetMembers()
      .Select(m => m as IEventSymbol)
      .Where(es => es is not null
                && es.DeclaredAccessibility == Accessibility.Public);
    var methods = classNamedTypeSymbol.GetMembers()
      .Select(m => m as IMethodSymbol)
      .Where(ms => ms is not null
                && ms.DeclaredAccessibility == Accessibility.Public
                && ms.MethodKind == MethodKind.Ordinary);

    var generatedFields = new FieldsGenerator(fields).Generate(classNamedTypeSymbol);
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
