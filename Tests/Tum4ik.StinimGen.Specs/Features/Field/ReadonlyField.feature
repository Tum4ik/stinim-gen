Feature: Read-only field


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
    public static readonly float StaticReadonlyField;
    """
  When run generator
  Then there must not be generation exception
  Then generated interface member must be
    """
    float StaticReadonlyField { get; }
    """
  And generated implementation member must be
    """
    public float StaticReadonlyField { get => Fields.FieldHolder.StaticReadonlyField; }
    """
  

Scenario: Nullable field with keyworded type
  Given source member declaration
    """
    public static readonly float? StaticReadonlyField;
    """
  When run generator
  Then there must not be generation exception
  Then generated interface member must be
    """
    float? StaticReadonlyField { get; }
    """
  And generated implementation member must be
    """
    public float? StaticReadonlyField { get => Fields.FieldHolder.StaticReadonlyField; }
    """
  

Scenario: Non-nullable field
  Given source member declaration
    """
    public static readonly StringComparer StaticReadonlyField;
    """
  When run generator
  Then there must not be generation exception
  Then generated interface member must be
    """
    global::System.StringComparer StaticReadonlyField { get; }
    """
  And generated implementation member must be
    """
    public global::System.StringComparer StaticReadonlyField { get => Fields.FieldHolder.StaticReadonlyField; }
    """
  

Scenario: Nullable field
  Given source member declaration
    """
    public static readonly StringComparer? StaticReadonlyField;
    """
  When run generator
  Then there must not be generation exception
  Then generated interface member must be
    """
    global::System.StringComparer? StaticReadonlyField { get; }
    """
  And generated implementation member must be
    """
    public global::System.StringComparer? StaticReadonlyField { get => Fields.FieldHolder.StaticReadonlyField; }
    """
