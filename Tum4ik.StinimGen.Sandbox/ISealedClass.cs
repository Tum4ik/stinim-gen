using Tum4ik.StinimGen.Attributes;
using Tum4ik.StinimGen.Sandbox.Types;

namespace Tum4ik.StinimGen.Sandbox;

[IIFor(typeof(SealedClass), WrapperClassName = "SealedClassWrapper")]
public partial interface ISealedClass
{
}
