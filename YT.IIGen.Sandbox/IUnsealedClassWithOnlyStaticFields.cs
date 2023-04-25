using YT.IIGen.Attributes;
using YT.IIGen.Sandbox.Types;

namespace YT.IIGen.Sandbox;

[IIFor(typeof(UnsealedClassWithOnlyStaticFields), "UnsealedClassWithOnlyStaticFieldsWrapper")]
internal partial interface IUnsealedClassWithOnlyStaticFields
{
}
