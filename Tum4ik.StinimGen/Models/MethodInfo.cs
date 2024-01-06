using System.Collections.Immutable;

namespace Tum4ik.StinimGen.Models;
internal sealed record MethodInfo(
  string? ReturnTypeNameWithNullabilityAnnotations,
  string MethodName,
  ImmutableArray<ParameterInfo> Parameters
);
