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
    //System.Diagnostics.Debugger.Launch();

    var interfacesProvider = context.SyntaxProvider.ForAttributeWithMetadataName(
      typeof(IIForAttribute).FullName,
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

    var wrapperClassName = (declaredAttributeArguments.Value[1].Expression as LiteralExpressionSyntax)?.Token.ValueText;
    if (wrapperClassName is null)
    {
      throw new ArgumentException("Wrapper class name is not provided");
    }

    var nameSpace = compilation.GetSemanticModel(declaredInterfaceSyntax.SyntaxTree, false)
      .GetDeclaredSymbol(declaredInterfaceSyntax)?
      .ContainingNamespace
      .ToString();
    if (nameSpace is null)
    {
      throw new ArgumentException("Can not get the interface namespace.");
    }

    var accessModifiers = declaredInterfaceSyntax.Modifiers.ToString();
    var interfaceName = declaredInterfaceSyntax.Identifier.ValueText;

    // TODO: optimize
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

    var generatedFields = new FieldsGenerator(fields).Generate(sourceNamedTypeSymbol);

    var generatedInterface = new InterfaceGenerator()
      .AddFields(generatedFields.InterfaceFields)
      .Generate(nameSpace, accessModifiers, interfaceName);

    ctx.AddSource($"{nameSpace}.{interfaceName}.g.cs", generatedInterface);
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
