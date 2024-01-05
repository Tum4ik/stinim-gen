Feature: Read-only field


Scenario: Non-nullable field with keyworded type
  Given source member declaration
    """
    public static readonly float StaticReadonlyField;
    """
  When run generator for field
  Then generated for interface
    """
    float StaticReadonlyField { get; }
    """
  And generated for struct implementation
    """
    public float StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyField; }
    """
  And generated for class implementation
    """
    public new float StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyField; }
    """
  And generated for sealed class implementation
    """
    public float StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyField; }
    """
  And generated for static class implementation
    """
    public float StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyField; }
    """


Scenario: Nullable field with keyworded type
  Given source member declaration
    """
    public static readonly float? StaticReadonlyField;
    """
  When run generator for field
  Then generated for interface
    """
    float? StaticReadonlyField { get; }
    """
  And generated for struct implementation
    """
    public float? StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyField; }
    """
  And generated for class implementation
    """
    public new float? StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyField; }
    """
  And generated for sealed class implementation
    """
    public float? StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyField; }
    """
  And generated for static class implementation
    """
    public float? StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyField; }
    """


Scenario: Non-nullable field
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public static readonly StringComparer StaticReadonlyField;
    """
  When run generator for field
  Then generated for interface
    """
    global::System.StringComparer StaticReadonlyField { get; }
    """
  And generated for struct implementation
    """
    public global::System.StringComparer StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyField; }
    """
  And generated for class implementation
    """
    public new global::System.StringComparer StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyField; }
    """
  And generated for sealed class implementation
    """
    public global::System.StringComparer StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyField; }
    """
  And generated for static class implementation
    """
    public global::System.StringComparer StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyField; }
    """


Scenario: Nullable field
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public static readonly StringComparer? StaticReadonlyField;
    """
  When run generator for field
  Then generated for interface
    """
    global::System.StringComparer? StaticReadonlyField { get; }
    """
  And generated for struct implementation
    """
    public global::System.StringComparer? StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyField; }
    """
  And generated for class implementation
    """
    public new global::System.StringComparer? StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyField; }
    """
  And generated for sealed class implementation
    """
    public global::System.StringComparer? StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyField; }
    """
  And generated for static class implementation
    """
    public global::System.StringComparer? StaticReadonlyField { get => @Namespace.@TypeName.StaticReadonlyField; }
    """
