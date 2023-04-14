using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;

namespace YT.IIGen.Extensions;
internal static class ITypeSymbolExtensions
{
  /// <summary>
  /// Gets the fully qualified metadata name for a given <see cref="ITypeSymbol"/> instance.
  /// </summary>
  /// <param name="symbol">The input <see cref="ITypeSymbol"/> instance.</param>
  /// <returns>The fully qualified metadata name for <paramref name="symbol"/>.</returns>
  public static string GetFullyQualifiedMetadataName(this ITypeSymbol symbol)
  {
    var builder = new StringBuilder();
    symbol.AppendFullyQualifiedMetadataName(builder);
    return builder.ToString();
  }

  private static void AppendFullyQualifiedMetadataName(this ISymbol symbol, StringBuilder builder)
  {
    switch (symbol)
    {
      // Namespaces that are nested also append a leading '.'
      case INamespaceSymbol { ContainingNamespace.IsGlobalNamespace: false }:
        AppendFullyQualifiedMetadataName(symbol.ContainingNamespace, builder);
        builder.Append('.');
        builder.Append(symbol.MetadataName);
        break;

      // Other namespaces (ie. the one right before global) skip the leading '.'
      case INamespaceSymbol { IsGlobalNamespace: false }:
        builder.Append(symbol.MetadataName);
        break;

      // Types with no namespace just have their metadata name directly written
      case ITypeSymbol { ContainingSymbol: INamespaceSymbol { IsGlobalNamespace: true } }:
        builder.Append(symbol.MetadataName);
        break;

      // Types with a containing non-global namespace also append a leading '.'
      case ITypeSymbol { ContainingSymbol: INamespaceSymbol namespaceSymbol }:
        AppendFullyQualifiedMetadataName(namespaceSymbol, builder);
        builder.Append('.');
        builder.Append(symbol.MetadataName);
        break;

      // Nested types append a leading '+'
      case ITypeSymbol { ContainingSymbol: ITypeSymbol typeSymbol }:
        AppendFullyQualifiedMetadataName(typeSymbol, builder);
        builder.Append('+');
        builder.Append(symbol.MetadataName);
        break;
    }
  }


  public static ImmutableArray<ISymbol> GetMembersIncludingBaseTypes(this ITypeSymbol symbol,
                                                                     Func<ISymbol, bool> predicate)
  {
    var members = symbol.GetMembers().Where(predicate).ToList();
    if (symbol.BaseType is not null)
    {
      members.AddRange(symbol.BaseType.GetMembersIncludingBaseTypes(predicate));
    }
    return members.ToImmutableArray();
  }
}
