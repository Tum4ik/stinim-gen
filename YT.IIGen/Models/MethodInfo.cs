using System.Collections.Immutable;

namespace YT.IIGen.Models;
internal sealed record MethodInfo(
  string? ReturnTypeNameWithNullabilityAnnotations,
  string MethodName,
  bool IsStatic,
  ImmutableArray<ParameterInfo> Parameters
);
