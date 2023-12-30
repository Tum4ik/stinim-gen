namespace Tum4ik.StinimGen.Models;
internal sealed record PropertyInfo(
  string TypeNameWithNullabilityAnnotations,
  string PropertyName,
  bool IsStatic,
  bool HasGetter,
  bool HasSetter
);
