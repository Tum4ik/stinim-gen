namespace YT.IIGen.Models;
internal sealed record EventInfo(
  string TypeNameWithNullabilityAnnotations,
  string EventName,
  bool IsStatic
);
