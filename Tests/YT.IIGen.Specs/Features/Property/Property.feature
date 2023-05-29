Feature: Property


Scenario: Property with only getter
  Given source member declaration
    """
    public int Property { get; }
    """
  When run generator for property
  Then generated for interface
    """
    int Property { get; }
    """
  And generated for struct implementation
    """
    public int Property { get => _instance.Property; }
    """
  And inherited for class implementation
  And generated for sealed class implementation
    """
    public int Property { get => _instance.Property; }
    """


Scenario: Property with only setter
  Given source member declaration
    """
    private float? _property;
    public float? Property { set => _property = value; }
    """
  When run generator for property
  Then generated for interface
    """
    float? Property { set; }
    """
  And generated for struct implementation
    """
    public float? Property { set => _instance.Property = value; }
    """
  And inherited for class implementation
  And generated for sealed class implementation
    """
    public float? Property { set => _instance.Property = value; }
    """


Scenario: Property with getter and setter
  Given usings
    """
    using System.IO;
    """
  And source member declaration
    """
    public Stream Property { get; set; }
    """
  When run generator for property
  Then generated for interface
    """
    global::System.IO.Stream Property { get; set; }
    """
  And generated for struct implementation
    """
    public global::System.IO.Stream Property { get => _instance.Property; set => _instance.Property = value; }
    """
  And inherited for class implementation
  And generated for sealed class implementation
    """
    public global::System.IO.Stream Property { get => _instance.Property; set => _instance.Property = value; }
    """


Scenario: Property with getter and private setter
  Given usings
    """
    using System.IO;
    """
  And source member declaration
    """
    public Stream? Property { get; private set; }
    """
  When run generator for property
  Then generated for interface
    """
    global::System.IO.Stream? Property { get; }
    """
  And generated for struct implementation
    """
    public global::System.IO.Stream? Property { get => _instance.Property; }
    """
  And inherited for class implementation
  And generated for sealed class implementation
    """
    public global::System.IO.Stream? Property { get => _instance.Property; }
    """


Scenario: Property with getter and protected setter
  Given source member declaration
    """
    public decimal Property { get; protected set; }
    """
  When run generator for property
  Then generated for interface
    """
    decimal Property { get; }
    """
  And inherited for class implementation


Scenario: Property with getter and init
  Given source member declaration
    """
    public decimal? Property { get; init; }
    """
  When run generator for property
  Then generated for interface
    """
    decimal? Property { get; }
    """
  And generated for struct implementation
    """
    public decimal? Property { get => _instance.Property; }
    """
  And inherited for class implementation
  And generated for sealed class implementation
    """
    public decimal? Property { get => _instance.Property; }
    """


Scenario: Property with private getter and setter
Given source member declaration
    """
    public decimal? Property { private get; set; }
    """
  When run generator for property
  Then generated for interface
    """
    decimal? Property { set; }
    """
  And generated for struct implementation
    """
    public decimal? Property { set => _instance.Property = value; }
    """
  And inherited for class implementation
  And generated for sealed class implementation
    """
    public decimal? Property { set => _instance.Property = value; }
    """


Scenario: Property with protected getter and setter
Given source member declaration
    """
    public decimal Property { protected get; set; }
    """
  When run generator for property
  Then generated for interface
    """
    decimal Property { set; }
    """
  And inherited for class implementation
