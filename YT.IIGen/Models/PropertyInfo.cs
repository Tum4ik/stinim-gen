namespace YT.IIGen.Models;
internal sealed record PropertyInfo(
  string TypeNameWithNullabilityAnnotations,
  string PropertyName,
  bool IsStatic,
  bool HasSetter
);
