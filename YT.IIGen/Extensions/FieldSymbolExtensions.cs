using Microsoft.CodeAnalysis;
using YT.IIGen.Models;

namespace YT.IIGen.Extensions;
internal static class FieldSymbolExtensions
{
  public static PropertyInfo ToFieldInfo(this IFieldSymbol fieldSymbol)
  {
    return new(
      fieldSymbol.Type.GetFullyQualifiedNameWithNullabilityAnnotations(),
      fieldSymbol.Name,
      fieldSymbol.IsStatic,
      true,
      !fieldSymbol.IsConst && !fieldSymbol.IsReadOnly
    );
  }
}
