using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Tum4ik.StinimGen.Extensions;
internal static class TypeSymbolExtensions
{
  public static ImmutableArray<ISymbol> GetMembersIncludingBaseTypes(this ITypeSymbol symbol,
                                                                     Func<ISymbol, bool> predicate)
  {
    var members = symbol.GetMembers()
      .Where(predicate)
      .ToList();
    if (symbol.BaseType is not null)
    {
      members.AddRange(symbol.BaseType.GetMembersIncludingBaseTypes(predicate));
    }
    return [.. members];
  }
}
