Feature: Read-only field


Scenario: Non-nullable field with keyworded type
  Given source member declaration
    """
    public readonly string ReadonlyField;
    """
  When run generator for field
  Then generated for interface
    """
    string ReadonlyField { get; }
    """
  And generated for struct implementation
    """
    public string ReadonlyField { get => _instance.ReadonlyField; }
    """
  And generated for class implementation
    """
    public new string ReadonlyField { get => _instance.ReadonlyField; }
    """
  And generated for sealed class implementation
    """
    public string ReadonlyField { get => _instance.ReadonlyField; }
    """


Scenario: Nullable field with keyworded type
  Given source member declaration
    """
    public readonly string? ReadonlyField;
    """
  When run generator for field
  Then generated for interface
    """
    string? ReadonlyField { get; }
    """
  And generated for struct implementation
    """
    public string? ReadonlyField { get => _instance.ReadonlyField; }
    """
  And generated for class implementation
    """
    public new string? ReadonlyField { get => _instance.ReadonlyField; }
    """
  And generated for sealed class implementation
    """
    public string? ReadonlyField { get => _instance.ReadonlyField; }
    """


Scenario: Non-nullable field
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public readonly DateTime ReadonlyField;
    """
  When run generator for field
  Then generated for interface
    """
    global::System.DateTime ReadonlyField { get; }
    """
  And generated for struct implementation
    """
    public global::System.DateTime ReadonlyField { get => _instance.ReadonlyField; }
    """
  And generated for class implementation
    """
    public new global::System.DateTime ReadonlyField { get => _instance.ReadonlyField; }
    """
  And generated for sealed class implementation
    """
    public global::System.DateTime ReadonlyField { get => _instance.ReadonlyField; }
    """


Scenario: Nullable field
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public readonly DateTime? ReadonlyField;
    """
  When run generator for field
  Then generated for interface
    """
    global::System.DateTime? ReadonlyField { get; }
    """
  And generated for struct implementation
    """
    public global::System.DateTime? ReadonlyField { get => _instance.ReadonlyField; }
    """
  And generated for class implementation
    """
    public new global::System.DateTime? ReadonlyField { get => _instance.ReadonlyField; }
    """
  And generated for sealed class implementation
    """
    public global::System.DateTime? ReadonlyField { get => _instance.ReadonlyField; }
    """
