using System.Diagnostics.CodeAnalysis;
using YT.IIGen.Attributes;
using YT.IIGen.Sample.Types;

namespace YT.IIGen.Sample;

[IIFor(typeof(SealedClass), "SealedClassWrapper")]
internal partial interface ISealedClass
{
}
