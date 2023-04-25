using YT.IIGen.Attributes;
using YT.IIGen.Sandbox.Types;

namespace YT.IIGen.Sandbox;

[IIFor(typeof(SealedClass), "SealedClassWrapper")]
internal partial interface ISealedClass
{
}
