Feature: Const field


Background:
  Given source declaration
    """
    using System;
    namespace Fields;
    public class FieldHolder
    {
      <member>
    }
    """
  And attribute usage
    """
    using Tum4ik.StinimGen.Attributes;
    using Fields;
    namespace Attribute.Usage;
    [IIFor(typeof(FieldHolder), "FieldHolderWrapper")]
    internal partial interface IFieldHolder { }
    """


Scenario: Field with keyworded type
  Given source member declaration
    """
    public const int ConstField = 0;
    """
  When run generator
  Then there must not be generation exception
  Then generated interface member must be
    """
    int ConstField { get; }
    """
  And generated implementation member must be
    """
    public int ConstField { get => Fields.FieldHolder.ConstField; }
    """


Scenario: Field with non-keyworded type
  Given source member declaration
    """
    public const Double ConstField = 0d;
    """
  When run generator
  Then there must not be generation exception
  Then generated interface member must be
    """
    double ConstField { get; }
    """
  And generated implementation member must be
    """
    public double ConstField { get => Fields.FieldHolder.ConstField; }
    """
