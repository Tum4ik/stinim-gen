Feature: Static field


Scenario: Non-nullable field with keyworded type
  Given source member declaration
    """
    public static object StaticField;
    """
  When run generator for field
  Then generated for interface
    """
    object StaticField { get; set; }
    """
  And generated for struct implementation
    """
    public object StaticField { get => @Namespace.@TypeName.StaticField; set => @Namespace.@TypeName.StaticField = value; }
    """
  And generated for class implementation
    """
    public new object StaticField { get => @Namespace.@TypeName.StaticField; set => @Namespace.@TypeName.StaticField = value; }
    """
  And generated for sealed class implementation
    """
    public object StaticField { get => @Namespace.@TypeName.StaticField; set => @Namespace.@TypeName.StaticField = value; }
    """
  And generated for static class implementation
    """
    public object StaticField { get => @Namespace.@TypeName.StaticField; set => @Namespace.@TypeName.StaticField = value; }
    """


Scenario: Nullable field with keyworded type
  Given source member declaration
    """
    public static object? StaticField;
    """
  When run generator for field
  Then generated for interface
    """
    object? StaticField { get; set; }
    """
  And generated for struct implementation
    """
    public object? StaticField { get => @Namespace.@TypeName.StaticField; set => @Namespace.@TypeName.StaticField = value; }
    """
  And generated for class implementation
    """
    public new object? StaticField { get => @Namespace.@TypeName.StaticField; set => @Namespace.@TypeName.StaticField = value; }
    """
  And generated for sealed class implementation
    """
    public object? StaticField { get => @Namespace.@TypeName.StaticField; set => @Namespace.@TypeName.StaticField = value; }
    """
  And generated for static class implementation
    """
    public object? StaticField { get => @Namespace.@TypeName.StaticField; set => @Namespace.@TypeName.StaticField = value; }
    """


Scenario: Non-nullable field
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public static Delegate StaticField;
    """
  When run generator for field
  Then generated for interface
    """
    global::System.Delegate StaticField { get; set; }
    """
  And generated for struct implementation
    """
    public global::System.Delegate StaticField { get => @Namespace.@TypeName.StaticField; set => @Namespace.@TypeName.StaticField = value; }
    """
  And generated for class implementation
    """
    public new global::System.Delegate StaticField { get => @Namespace.@TypeName.StaticField; set => @Namespace.@TypeName.StaticField = value; }
    """
  And generated for sealed class implementation
    """
    public global::System.Delegate StaticField { get => @Namespace.@TypeName.StaticField; set => @Namespace.@TypeName.StaticField = value; }
    """
  And generated for static class implementation
    """
    public global::System.Delegate StaticField { get => @Namespace.@TypeName.StaticField; set => @Namespace.@TypeName.StaticField = value; }
    """


Scenario: Nullable field
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public static Delegate? StaticField;
    """
  When run generator for field
  Then generated for interface
    """
    global::System.Delegate? StaticField { get; set; }
    """
  And generated for struct implementation
    """
    public global::System.Delegate? StaticField { get => @Namespace.@TypeName.StaticField; set => @Namespace.@TypeName.StaticField = value; }
    """
  And generated for class implementation
    """
    public new global::System.Delegate? StaticField { get => @Namespace.@TypeName.StaticField; set => @Namespace.@TypeName.StaticField = value; }
    """
  And generated for sealed class implementation
    """
    public global::System.Delegate? StaticField { get => @Namespace.@TypeName.StaticField; set => @Namespace.@TypeName.StaticField = value; }
    """
  And generated for static class implementation
    """
    public global::System.Delegate? StaticField { get => @Namespace.@TypeName.StaticField; set => @Namespace.@TypeName.StaticField = value; }
    """
