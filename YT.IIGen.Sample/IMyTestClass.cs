using YT.IIGen.Attributes;
using YT.IIGen.Sample.TestTypes;

namespace YT.IIGen.Sample;

[IIFor(typeof(MyTestClass), "MyTestClassWrapper")]
internal partial interface IMyTestClass
{
}
