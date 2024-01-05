Feature: Property


Scenario: Property with only getter
  Given source member declaration
    """
    public static int Property { get; }
    """
  When run generator for property
  Then generated for interface
    """
    int Property { get; }
    """
  And generated for struct implementation
    """
    public int Property { get => @Namespace.@TypeName.Property; }
    """
  And generated for class implementation
    """
    public new int Property { get => @Namespace.@TypeName.Property; }
    """
  And generated for sealed class implementation
    """
    public int Property { get => @Namespace.@TypeName.Property; }
    """
  And generated for static class implementation
    """
    public int Property { get => @Namespace.@TypeName.Property; }
    """


Scenario: Property with only setter
  Given source member declaration
    """
    private static float? _property;
    public static float? Property { set => _property = value; }
    """
  When run generator for property
  Then generated for interface
    """
    float? Property { set; }
    """
  And generated for struct implementation
    """
    public float? Property { set => @Namespace.@TypeName.Property = value; }
    """
  And generated for class implementation
    """
    public new float? Property { set => @Namespace.@TypeName.Property = value; }
    """
  And generated for sealed class implementation
    """
    public float? Property { set => @Namespace.@TypeName.Property = value; }
    """
  And generated for static class implementation
    """
    public float? Property { set => @Namespace.@TypeName.Property = value; }
    """


Scenario: Property with getter and setter
  Given usings
    """
    using System.IO;
    """
  And source member declaration
    """
    public static Stream Property { get; set; }
    """
  When run generator for property
  Then generated for interface
    """
    global::System.IO.Stream Property { get; set; }
    """
  And generated for struct implementation
    """
    public global::System.IO.Stream Property { get => @Namespace.@TypeName.Property; set => @Namespace.@TypeName.Property = value; }
    """
  And generated for class implementation
    """
    public new global::System.IO.Stream Property { get => @Namespace.@TypeName.Property; set => @Namespace.@TypeName.Property = value; }
    """
  And generated for sealed class implementation
    """
    public global::System.IO.Stream Property { get => @Namespace.@TypeName.Property; set => @Namespace.@TypeName.Property = value; }
    """
  And generated for static class implementation
    """
    public global::System.IO.Stream Property { get => @Namespace.@TypeName.Property; set => @Namespace.@TypeName.Property = value; }
    """


Scenario: Property with getter and private setter
  Given usings
    """
    using System.IO;
    """
  And source member declaration
    """
    public static Stream? Property { get; private set; }
    """
  When run generator for property
  Then generated for interface
    """
    global::System.IO.Stream? Property { get; }
    """
  And generated for struct implementation
    """
    public global::System.IO.Stream? Property { get => @Namespace.@TypeName.Property; }
    """
  And generated for class implementation
    """
    public new global::System.IO.Stream? Property { get => @Namespace.@TypeName.Property; }
    """
  And generated for sealed class implementation
    """
    public global::System.IO.Stream? Property { get => @Namespace.@TypeName.Property; }
    """
  And generated for static class implementation
    """
    public global::System.IO.Stream? Property { get => @Namespace.@TypeName.Property; }
    """


Scenario: Property with getter and protected setter
  Given usings
    """
    using System.IO;
    """
  And source member declaration
    """
    public static Stream? Property { get; protected set; }
    """
  When run generator for property
  Then generated for interface
    """
    global::System.IO.Stream? Property { get; }
    """
  And generated for struct implementation
    """
    public global::System.IO.Stream? Property { get => @Namespace.@TypeName.Property; }
    """
  And generated for class implementation
    """
    public new global::System.IO.Stream? Property { get => @Namespace.@TypeName.Property; }
    """
  And generated for sealed class implementation
    """
    public global::System.IO.Stream? Property { get => @Namespace.@TypeName.Property; }
    """


Scenario: Property with private getter and setter
  Given source member declaration
    """
    public static float? Property { private get; set; }
    """
  When run generator for property
  Then generated for interface
    """
    float? Property { set; }
    """
  And generated for struct implementation
    """
    public float? Property { set => @Namespace.@TypeName.Property = value; }
    """
  And generated for class implementation
    """
    public new float? Property { set => @Namespace.@TypeName.Property = value; }
    """
  And generated for sealed class implementation
    """
    public float? Property { set => @Namespace.@TypeName.Property = value; }
    """
  And generated for static class implementation
    """
    public float? Property { set => @Namespace.@TypeName.Property = value; }
    """
