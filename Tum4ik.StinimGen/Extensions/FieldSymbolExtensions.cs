using Microsoft.CodeAnalysis;
using Tum4ik.StinimGen.Models;

namespace Tum4ik.StinimGen.Extensions;
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
