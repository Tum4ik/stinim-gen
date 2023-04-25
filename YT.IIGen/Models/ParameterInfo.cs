using Microsoft.CodeAnalysis;

namespace YT.IIGen.Models;
internal sealed record ParameterInfo(
  string TypeNameWithNullabilityAnnotations,
  TypeKind TypeKind,
  SpecialType SpecialType,
  string ParameterName,
  RefKind RefKind,
  bool IsParams,
  bool IsOptional,
  object? ExplicitDefaultValue
);
