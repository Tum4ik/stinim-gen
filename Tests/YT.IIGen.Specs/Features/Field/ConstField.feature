Feature: Const field


Scenario: Non-nullable field with keyworded type
  Given source member declaration
    """
    public const int ConstField;
    """
  When run generator for field
  Then generated for interface
    """
    int ConstField { get; }
    """
  And generated for struct implementation
    """
    public int ConstField { get => @Namespace.@TypeName.ConstField; }
    """
  And generated for class implementation
    """
    public new int ConstField { get => @Namespace.@TypeName.ConstField; }
    """
  And generated for sealed class implementation
    """
    public int ConstField { get => @Namespace.@TypeName.ConstField; }
    """
  And generated for static class implementation
    """
    public int ConstField { get => @Namespace.@TypeName.ConstField; }
    """


Scenario: Nullable field with keyworded type
  Given source member declaration
    """
    public const int? ConstNullableField;
    """
  When run generator for field
  Then generated for interface
    """
    int? ConstNullableField { get; }
    """
  And generated for struct implementation
    """
    public int? ConstNullableField { get => @Namespace.@TypeName.ConstNullableField; }
    """
  And generated for class implementation
    """
    public new int? ConstNullableField { get => @Namespace.@TypeName.ConstNullableField; }
    """
  And generated for sealed class implementation
    """
    public int? ConstNullableField { get => @Namespace.@TypeName.ConstNullableField; }
    """
  And generated for static class implementation
    """
    public int? ConstNullableField { get => @Namespace.@TypeName.ConstNullableField; }
    """


Scenario: Non-nullable field
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public const Double ConstField;
    """
  When run generator for field
  Then generated for interface
    """
    double ConstField { get; }
    """
  And generated for struct implementation
    """
    public double ConstField { get => @Namespace.@TypeName.ConstField; }
    """
  And generated for class implementation
    """
    public new double ConstField { get => @Namespace.@TypeName.ConstField; }
    """
  And generated for sealed class implementation
    """
    public double ConstField { get => @Namespace.@TypeName.ConstField; }
    """
  And generated for static class implementation
    """
    public double ConstField { get => @Namespace.@TypeName.ConstField; }
    """


Scenario: Nullable field
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public const Double? ConstNullableField;
    """
  When run generator for field
  Then generated for interface
    """
    double? ConstNullableField { get; }
    """
  And generated for struct implementation
    """
    public double? ConstNullableField { get => @Namespace.@TypeName.ConstNullableField; }
    """
  And generated for class implementation
    """
    public new double? ConstNullableField { get => @Namespace.@TypeName.ConstNullableField; }
    """
  And generated for sealed class implementation
    """
    public double? ConstNullableField { get => @Namespace.@TypeName.ConstNullableField; }
    """
  And generated for static class implementation
    """
    public double? ConstNullableField { get => @Namespace.@TypeName.ConstNullableField; }
    """
