using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Tum4ik.StinimGen.Models;
internal sealed record PropertyInfo(
  string TypeNameWithNullabilityAnnotations,
  string PropertyName,
  bool HasGetter,
  bool HasSetter,
  AttributeListSyntax[] ForwardedAttributes
);
