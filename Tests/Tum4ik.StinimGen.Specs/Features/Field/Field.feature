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
    /// <inheritdoc cref = "global::Fields.FieldHolder.StaticField"/>
    object StaticField { get; set; }
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public object StaticField { get => global::Fields.FieldHolder.StaticField; set => global::Fields.FieldHolder.StaticField = value; }
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
    /// <inheritdoc cref = "global::Fields.FieldHolder.StaticField"/>
    object? StaticField { get; set; }
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public object? StaticField { get => global::Fields.FieldHolder.StaticField; set => global::Fields.FieldHolder.StaticField = value; }
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
    /// <inheritdoc cref = "global::Fields.FieldHolder.StaticField"/>
    global::System.Delegate StaticField { get; set; }
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public global::System.Delegate StaticField { get => global::Fields.FieldHolder.StaticField; set => global::Fields.FieldHolder.StaticField = value; }
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
    /// <inheritdoc cref = "global::Fields.FieldHolder.StaticField"/>
    global::System.Delegate? StaticField { get; set; }
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public global::System.Delegate? StaticField { get => global::Fields.FieldHolder.StaticField; set => global::Fields.FieldHolder.StaticField = value; }
    """


Scenario: Forward Obsolete attribute
  Given source member declaration
    """
    [Obsolete("Obsolete field")]
    public static object ObsoleteField;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "global::Fields.FieldHolder.ObsoleteField"/>
    [global::System.ObsoleteAttribute("Obsolete field")]
    object ObsoleteField { get; set; }
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    [global::System.ObsoleteAttribute("Obsolete field")]
    public object ObsoleteField { get => global::Fields.FieldHolder.ObsoleteField; set => global::Fields.FieldHolder.ObsoleteField = value; }
    """
