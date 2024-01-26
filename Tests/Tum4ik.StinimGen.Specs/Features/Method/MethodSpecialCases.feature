Feature: Method special cases


# In some scenarios it is possible the generator produces the explicit interface specifier for the methods.
# For example it is true for the DateTime struct.
# But it is ridiculous for the generated interface and implementation since they are not inherited from that interface
# in the specifier.
Scenario: Explicit interface implementatins conflict for DateTime struct
  Given attribute usage
    """
    using System;
    using Tum4ik.StinimGen.Attributes;
    namespace Attribute.Usage;
    [IIFor(typeof(DateTime), "DateTimeWrapper")]
    internal partial interface IDateTime { }
    """
  When run generator
  Then there must not be generation exception
  And there must not be explicit interface specifier in the generated interface and implementation methods
