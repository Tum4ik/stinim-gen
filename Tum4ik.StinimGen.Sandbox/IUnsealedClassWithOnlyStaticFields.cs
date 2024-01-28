using Tum4ik.StinimGen.Attributes;
using Tum4ik.StinimGen.Sandbox.Types;

namespace Tum4ik.StinimGen.Sandbox;

[IIFor(typeof(UnsealedClassWithOnlyStaticFields), WrapperClassName = "UnsealedClassWithOnlyStaticFieldsWrapper")]
internal partial interface IUnsealedClassWithOnlyStaticFields
{
}
