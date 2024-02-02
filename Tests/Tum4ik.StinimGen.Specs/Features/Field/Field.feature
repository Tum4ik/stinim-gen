Feature: Field


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


Scenario: Non-nullable field with keyworded type
  Given source member declaration
    """
    public static object StaticField;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Fields.FieldHolder.StaticField"/>
    object StaticField { get; set; }
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public object StaticField { get => Fields.FieldHolder.StaticField; set => Fields.FieldHolder.StaticField = value; }
    """
  

Scenario: Nullable field with keyworded type
  Given source member declaration
    """
    public static object? StaticField;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Fields.FieldHolder.StaticField"/>
    object? StaticField { get; set; }
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public object? StaticField { get => Fields.FieldHolder.StaticField; set => Fields.FieldHolder.StaticField = value; }
    """
  

Scenario: Non-nullable field
  Given source member declaration
    """
    public static Delegate StaticField;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Fields.FieldHolder.StaticField"/>
    global::System.Delegate StaticField { get; set; }
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public global::System.Delegate StaticField { get => Fields.FieldHolder.StaticField; set => Fields.FieldHolder.StaticField = value; }
    """
  

Scenario: Nullable field
  Given source member declaration
    """
    public static Delegate? StaticField;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Fields.FieldHolder.StaticField"/>
    global::System.Delegate? StaticField { get; set; }
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public global::System.Delegate? StaticField { get => Fields.FieldHolder.StaticField; set => Fields.FieldHolder.StaticField = value; }
    """
