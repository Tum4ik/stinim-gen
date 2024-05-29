using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Tum4ik.StinimGen.Extensions;
internal static class SymbolExtensions
{
  /// <summary>
  /// Gets the fully qualified name for a given symbol
  /// </summary>
  /// <param name="symbol">The input <see cref="ISymbol"/> instance.</param>
  /// <returns>The fully qualified name for <paramref name="symbol"/>.</returns>
  public static string GetFullyQualifiedName(this ISymbol symbol)
  {
    return symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
  }


  /// <summary>
  /// Gets the fully qualified name for a given symbol, including nullability annotations
  /// </summary>
  /// <param name="symbol">The input <see cref="ISymbol"/> instance.</param>
  /// <returns>The fully qualified name for <paramref name="symbol"/>.</returns>
  public static string GetFullyQualifiedNameWithNullabilityAnnotations(this ISymbol symbol)
  {
    return symbol.ToDisplayString(
      SymbolDisplayFormat.FullyQualifiedFormat
        .AddMiscellaneousOptions(SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier)
    );
  }


  public static AttributeListSyntax[] GetObsoleteAttributeSyntaxIfPresent(this ISymbol symbol,
                                                                          SyntaxGenerator syntaxGenerator)
  {
    return symbol.GetAttributes()
      .Where(a => a.AttributeClass?.ContainingNamespace.Name == "System"
               && a.AttributeClass.Name == "ObsoleteAttribute")
      .Select(a => (AttributeListSyntax) syntaxGenerator.Attribute(a))
      .ToArray();
  }
}
