using Microsoft.CodeAnalysis;

namespace YT.IIGen.Extensions;
internal static class SymbolExtensions
{
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
}
