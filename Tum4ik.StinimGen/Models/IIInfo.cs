using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Tum4ik.StinimGen.Models;
internal sealed record IIInfo(
  string Namespace,
  TypeInfo InterfaceTypeInfo,
  TypeInfo ImplementationTypeInfo,
  ImmutableArray<PropertyInfo> PropertyForFieldInfoList,
  ImmutableArray<PropertyInfo> PropertyInfoList,
  ImmutableArray<EventInfo> EventInfoList,
  ImmutableArray<MethodDeclarationSyntax> MethodInfoList,
  string SourceFullyQualifiedName
);
