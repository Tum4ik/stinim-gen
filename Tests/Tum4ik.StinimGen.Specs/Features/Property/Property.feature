Feature: Property


Background:
  Given source declaration
    """
    using System;
    using System.IO;
    namespace Properties;
    public class PropertyHolder
    {
      <member>
    }
    """
  And attribute usage
    """
    using Tum4ik.StinimGen.Attributes;
    using Properties;
    namespace Attribute.Usage;
    [IIFor(typeof(PropertyHolder), WrapperClassName = "PropertyHolderWrapper")]
    internal partial interface IPropertyHolder { }
    """


Scenario: Property with only getter
  Given source member declaration
    """
    public static int Property { get; }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Properties.PropertyHolder.Property"/>
    int Property { get; }
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public int Property { get => Properties.PropertyHolder.Property; }
    """
  

Scenario: Property with only setter
  Given source member declaration
    """
    private static float? _property;
    public static float? Property { set => _property = value; }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Properties.PropertyHolder.Property"/>
    float? Property { set; }
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public float? Property { set => Properties.PropertyHolder.Property = value; }
    """
  

Scenario: Property with getter and setter
  Given source member declaration
    """
    public static Stream Property { get; set; }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Properties.PropertyHolder.Property"/>
    global::System.IO.Stream Property { get; set; }
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public global::System.IO.Stream Property { get => Properties.PropertyHolder.Property; set => Properties.PropertyHolder.Property = value; }
    """
  

Scenario: Property with getter and private setter
  Given source member declaration
    """
    public static Stream? Property { get; private set; }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Properties.PropertyHolder.Property"/>
    global::System.IO.Stream? Property { get; }
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public global::System.IO.Stream? Property { get => Properties.PropertyHolder.Property; }
    """
  

Scenario: Property with getter and protected setter
  Given source member declaration
    """
    public static Stream? Property { get; protected set; }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Properties.PropertyHolder.Property"/>
    global::System.IO.Stream? Property { get; }
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public global::System.IO.Stream? Property { get => Properties.PropertyHolder.Property; }
    """
  

Scenario: Property with private getter and setter
  Given source member declaration
    """
    public static float? Property { private get; set; }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Properties.PropertyHolder.Property"/>
    float? Property { set; }
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public float? Property { set => Properties.PropertyHolder.Property = value; }
    """


Scenario: Property with protected getter and setter
  Given source member declaration
    """
    /// <inheritdoc cref = "Properties.PropertyHolder.Property"/>
    public static string Property { protected get; set; }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Properties.PropertyHolder.Property"/>
    string Property { set; }
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public string Property { set => Properties.PropertyHolder.Property = value; }
    """
