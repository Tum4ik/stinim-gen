using System.Collections.Immutable;

namespace YT.IIGen.Models;
internal sealed record IIInfo(
  string Namespace,
  TypeInfo InterfaceTypeInfo,
  TypeInfo ImplementationTypeInfo,
  ImmutableArray<PropertyInfo> PropertyForFieldInfoList,
  ImmutableArray<PropertyInfo> PropertyInfoList,
  ImmutableArray<EventInfo> EventInfoList,
  ImmutableArray<MethodInfo> MethodInfoList,
  string SourceFullyQualifiedName,
  bool IsSourceSealed,
  bool IsSourceStatic,
  bool IsSourceReferenceType,
  bool ContainsDynamicFields
);
