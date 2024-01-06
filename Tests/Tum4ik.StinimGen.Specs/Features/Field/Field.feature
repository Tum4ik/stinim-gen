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
    [IIFor(typeof(FieldHolder), "FieldHolderWrapper")]
    internal partial interface IFieldHolder { }
    """


Scenario: Non-nullable field with keyworded type
  Given source member declaration
    """
    public static object StaticField;
    """
  When run generator
  Then there must not be generation exception
  Then generated interface member must be
    """
    object StaticField { get; set; }
    """
  And generated implementation member must be
    """
    public object StaticField { get => Fields.FieldHolder.StaticField; set => Fields.FieldHolder.StaticField = value; }
    """
  

Scenario: Nullable field with keyworded type
  Given source member declaration
    """
    public static object? StaticField;
    """
  When run generator
  Then there must not be generation exception
  Then generated interface member must be
    """
    object? StaticField { get; set; }
    """
  And generated implementation member must be
    """
    public object? StaticField { get => Fields.FieldHolder.StaticField; set => Fields.FieldHolder.StaticField = value; }
    """
  

Scenario: Non-nullable field
  Given source member declaration
    """
    public static Delegate StaticField;
    """
  When run generator
  Then there must not be generation exception
  Then generated interface member must be
    """
    global::System.Delegate StaticField { get; set; }
    """
  And generated implementation member must be
    """
    public global::System.Delegate StaticField { get => Fields.FieldHolder.StaticField; set => Fields.FieldHolder.StaticField = value; }
    """
  

Scenario: Nullable field
  Given source member declaration
    """
    public static Delegate? StaticField;
    """
  When run generator
  Then there must not be generation exception
  Then generated interface member must be
    """
    global::System.Delegate? StaticField { get; set; }
    """
  And generated implementation member must be
    """
    public global::System.Delegate? StaticField { get => Fields.FieldHolder.StaticField; set => Fields.FieldHolder.StaticField = value; }
    """
