using Tum4ik.StinimGen.Attributes;

namespace Tum4ik.StinimGen.Sandbox;

[IIFor(typeof(DateTime), WrapperClassName = "DateTimeWrapper", IsSealed = false)]
internal partial interface IDateTime
{
}
