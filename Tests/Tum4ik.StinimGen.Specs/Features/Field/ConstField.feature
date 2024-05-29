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
    [IIFor(typeof(FieldHolder), WrapperClassName = "FieldHolderWrapper")]
    internal partial interface IFieldHolder { }
    """


Scenario: Field with keyworded type
  Given source member declaration
    """
    public const int ConstField = 0;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "global::Fields.FieldHolder.ConstField"/>
    int ConstField { get; }
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public int ConstField { get => global::Fields.FieldHolder.ConstField; }
    """


Scenario: Field with non-keyworded type
  Given source member declaration
    """
    public const Double ConstField = 0d;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "global::Fields.FieldHolder.ConstField"/>
    double ConstField { get; }
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public double ConstField { get => global::Fields.FieldHolder.ConstField; }
    """
