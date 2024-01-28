using Tum4ik.StinimGen.Attributes;
using Tum4ik.StinimGen.Sandbox.Types;

namespace Tum4ik.StinimGen.Sandbox;

[IIFor(typeof(StaticClass), WrapperClassName = "StaticClassWrapper")]
internal partial interface IStaticClass
{
}
