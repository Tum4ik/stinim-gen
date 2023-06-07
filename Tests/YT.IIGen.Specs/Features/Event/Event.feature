Feature: Event


Scenario: Event with EventHandler type
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public event EventHandler Event;
    """
  When run generator for event
  Then generated for interface
    """
    event global::System.EventHandler Event;
    """
  And generated for struct implementation
    """
    public event global::System.EventHandler Event { add => _instance.Event += value; remove => _instance.Event -= value; }
    """
  And inherited for class implementation
  And generated for sealed class implementation
    """
    public event global::System.EventHandler Event { add => _instance.Event += value; remove => _instance.Event -= value; }
    """


Scenario: Event with custom type
  Given source member declaration
    """
    public event CustomEventHandler Event;
    """
  And additional namespace declarations
    """
    public delegate void CustomEventHandler(int count, string search);
    """
  When run generator for event
  Then generated for interface
    """
    event global::@Namespace.CustomEventHandler Event;
    """
  And generated for struct implementation
    """
    public event global::@Namespace.CustomEventHandler Event { add => _instance.Event += value; remove => _instance.Event -= value; }
    """
  And inherited for class implementation
  And generated for sealed class implementation
    """
    public event global::@Namespace.CustomEventHandler Event { add => _instance.Event += value; remove => _instance.Event -= value; }
    """


Scenario: Event with Action type
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public event Action Event;
    """
  When run generator for event
  Then generated for interface
    """
    event global::System.Action Event;
    """
  And generated for struct implementation
    """
    public event global::System.Action Event { add => _instance.Event += value; remove => _instance.Event -= value; }
    """
  And inherited for class implementation
  And generated for sealed class implementation
    """
    public event global::System.Action Event { add => _instance.Event += value; remove => _instance.Event -= value; }
    """


Scenario: Event with Action type with keyworded generic parameter
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public event Action<int> Event;
    """
  When run generator for event
  Then generated for interface
    """
    event global::System.Action<int> Event;
    """
  And generated for struct implementation
    """
    public event global::System.Action<int> Event { add => _instance.Event += value; remove => _instance.Event -= value; }
    """
  And inherited for class implementation
  And generated for sealed class implementation
    """
    public event global::System.Action<int> Event { add => _instance.Event += value; remove => _instance.Event -= value; }
    """


Scenario: Event with Action type with keyworded nullable generic parameter
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public event Action<string?> Event;
    """
  When run generator for event
  Then generated for interface
    """
    event global::System.Action<string?> Event;
    """
  And generated for struct implementation
    """
    public event global::System.Action<string?> Event { add => _instance.Event += value; remove => _instance.Event -= value; }
    """
  And inherited for class implementation
  And generated for sealed class implementation
    """
    public event global::System.Action<string?> Event { add => _instance.Event += value; remove => _instance.Event -= value; }
    """


Scenario: Event with Action type with non-keyworded generic parameter
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public event Action<DateTime> Event;
    """
  When run generator for event
  Then generated for interface
    """
    event global::System.Action<global::System.DateTime> Event;
    """
  And generated for struct implementation
    """
    public event global::System.Action<global::System.DateTime> Event { add => _instance.Event += value; remove => _instance.Event -= value; }
    """
  And inherited for class implementation
  And generated for sealed class implementation
    """
    public event global::System.Action<global::System.DateTime> Event { add => _instance.Event += value; remove => _instance.Event -= value; }
    """


Scenario: Event with Action type with non-keyworded nullable generic parameter
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public event Action<DayOfWeek?> Event;
    """
  When run generator for event
  Then generated for interface
    """
    event global::System.Action<global::System.DayOfWeek?> Event;
    """
  And generated for struct implementation
    """
    public event global::System.Action<global::System.DayOfWeek?> Event { add => _instance.Event += value; remove => _instance.Event -= value; }
    """
  And inherited for class implementation
  And generated for sealed class implementation
    """
    public event global::System.Action<global::System.DayOfWeek?> Event { add => _instance.Event += value; remove => _instance.Event -= value; }
    """


Scenario: Event with Func
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public event Func<DayOfWeek?, double, int?> Event;
    """
  When run generator for event
  Then generated for interface
    """
    event global::System.Func<global::System.DayOfWeek?, double, int?> Event;
    """
  And generated for struct implementation
    """
    public event global::System.Func<global::System.DayOfWeek?, double, int?> Event { add => _instance.Event += value; remove => _instance.Event -= value; }
    """
  And inherited for class implementation
  And generated for sealed class implementation
    """
    public event global::System.Func<global::System.DayOfWeek?, double, int?> Event { add => _instance.Event += value; remove => _instance.Event -= value; }
    """
