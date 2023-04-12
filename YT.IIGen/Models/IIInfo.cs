using System.Collections.Immutable;

namespace YT.IIGen.Models;
internal sealed record IIInfo(
  string Namespace,
  TypeInfo InterfaceTypeInfo,
  TypeInfo ImplementationTypeInfo,
  ImmutableArray<PropertyInfo> PropertyForFieldInfoList,
  string SourceFullyQualifiedName,
  bool IsSourceSealed,
  bool IsSourceStatic,
  bool IsSourceReferenceType
);
