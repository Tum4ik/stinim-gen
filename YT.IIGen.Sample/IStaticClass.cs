using YT.IIGen.Attributes;
using YT.IIGen.Sample.Types;

namespace YT.IIGen.Sample;

[IIFor(typeof(StaticClass), "StaticClassWrapper")]
internal partial interface IStaticClass
{
}
