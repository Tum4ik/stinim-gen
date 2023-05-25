Feature: Field


Scenario: Non-nullable field with keyworded type
  Given source member declaration
    """
    public nint Field;
    """
  When run generator for field
  Then generated for interface
    """
    nint Field { get; set; }
    """
  And generated for struct implementation
    """
    public nint Field { get => _instance.Field; set => _instance.Field = value; }
    """
  And generated for class implementation
    """
    public new nint Field { get => _instance.Field; set => _instance.Field = value; }
    """
  And generated for sealed class implementation
    """
    public nint Field { get => _instance.Field; set => _instance.Field = value; }
    """


Scenario: Nullable field with keyworded type
  Given source member declaration
    """
    public nint? Field;
    """
  When run generator for field
  Then generated for interface
    """
    nint? Field { get; set; }
    """
  And generated for struct implementation
    """
    public nint? Field { get => _instance.Field; set => _instance.Field = value; }
    """
  And generated for class implementation
    """
    public new nint? Field { get => _instance.Field; set => _instance.Field = value; }
    """
  And generated for sealed class implementation
    """
    public nint? Field { get => _instance.Field; set => _instance.Field = value; }
    """


Scenario: Non-nullable field
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public DayOfWeek Field;
    """
  When run generator for field
  Then generated for interface
    """
    global::System.DayOfWeek Field { get; set; }
    """
  And generated for struct implementation
    """
    public global::System.DayOfWeek Field { get => _instance.Field; set => _instance.Field = value; }
    """
  And generated for class implementation
    """
    public new global::System.DayOfWeek Field { get => _instance.Field; set => _instance.Field = value; }
    """
  And generated for sealed class implementation
    """
    public global::System.DayOfWeek Field { get => _instance.Field; set => _instance.Field = value; }
    """


Scenario: Nullable field
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public DayOfWeek? Field;
    """
  When run generator for field
  Then generated for interface
    """
    global::System.DayOfWeek? Field { get; set; }
    """
  And generated for struct implementation
    """
    public global::System.DayOfWeek? Field { get => _instance.Field; set => _instance.Field = value; }
    """
  And generated for class implementation
    """
    public new global::System.DayOfWeek? Field { get => _instance.Field; set => _instance.Field = value; }
    """
  And generated for sealed class implementation
    """
    public global::System.DayOfWeek? Field { get => _instance.Field; set => _instance.Field = value; }
    """
