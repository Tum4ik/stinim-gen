using YT.IIGen.Attributes;
using YT.IIGen.Sample.Types;

namespace YT.IIGen.Sample;

[IIFor(typeof(UnsealedClassWithOnlyStaticFields), "UnsealedClassWithOnlyStaticFieldsWrapper")]
internal partial interface IUnsealedClassWithOnlyStaticFields
{
}
