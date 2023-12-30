namespace Tum4ik.StinimGen.Models;
internal sealed record EventInfo(
  string TypeNameWithNullabilityAnnotations,
  string EventName,
  bool IsStatic
);
