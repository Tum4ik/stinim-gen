Feature: Static event


Scenario: Event with EventHandler type
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public static event EventHandler Event;
    """
  When run generator for event
  Then generated for interface
    """
    event global::System.EventHandler Event;
    """
  And generated for struct implementation
    """
    public event global::System.EventHandler Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for class implementation
    """
    public new event global::System.EventHandler Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for sealed class implementation
    """
    public event global::System.EventHandler Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for static class implementation
    """
    public event global::System.EventHandler Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """


Scenario: Event with custom type
  Given source member declaration
    """
    public static event CustomEventHandler Event;
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
    public event global::@Namespace.CustomEventHandler Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for class implementation
    """
    public new event global::@Namespace.CustomEventHandler Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for sealed class implementation
    """
    public event global::@Namespace.CustomEventHandler Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for static class implementation
    """
    public event global::@Namespace.CustomEventHandler Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """


Scenario: Event with Action type
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public static event Action Event;
    """
  When run generator for event
  Then generated for interface
    """
    event global::System.Action Event;
    """
  And generated for struct implementation
    """
    public event global::System.Action Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for class implementation
    """
    public new event global::System.Action Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for sealed class implementation
    """
    public event global::System.Action Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for static class implementation
    """
    public event global::System.Action Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """


Scenario: Event with Action type with keyworded generic parameter
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public static event Action<int> Event;
    """
  When run generator for event
  Then generated for interface
    """
    event global::System.Action<int> Event;
    """
  And generated for struct implementation
    """
    public event global::System.Action<int> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for class implementation
    """
    public new event global::System.Action<int> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for sealed class implementation
    """
    public event global::System.Action<int> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for static class implementation
    """
    public event global::System.Action<int> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """


Scenario: Event with Action type with keyworded nullable generic parameter
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public static event Action<string?> Event;
    """
  When run generator for event
  Then generated for interface
    """
    event global::System.Action<string?> Event;
    """
  And generated for struct implementation
    """
    public event global::System.Action<string?> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for class implementation
    """
    public new event global::System.Action<string?> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for sealed class implementation
    """
    public event global::System.Action<string?> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for static class implementation
    """
    public event global::System.Action<string?> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """


Scenario: Event with Action type with non-keyworded generic parameter
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public static event Action<DateTime> Event;
    """
  When run generator for event
  Then generated for interface
    """
    event global::System.Action<global::System.DateTime> Event;
    """
  And generated for struct implementation
    """
    public event global::System.Action<global::System.DateTime> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for class implementation
    """
    public new event global::System.Action<global::System.DateTime> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for sealed class implementation
    """
    public event global::System.Action<global::System.DateTime> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for static class implementation
    """
    public event global::System.Action<global::System.DateTime> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """


Scenario: Event with Action type with non-keyworded nullable generic parameter
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public static event Action<DayOfWeek?> Event;
    """
  When run generator for event
  Then generated for interface
    """
    event global::System.Action<global::System.DayOfWeek?> Event;
    """
  And generated for struct implementation
    """
    public event global::System.Action<global::System.DayOfWeek?> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for class implementation
    """
    public new event global::System.Action<global::System.DayOfWeek?> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for sealed class implementation
    """
    public event global::System.Action<global::System.DayOfWeek?> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for static class implementation
    """
    public event global::System.Action<global::System.DayOfWeek?> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """


Scenario: Event with Func
  Given usings
    """
    using System;
    """
  And source member declaration
    """
    public static event Func<DayOfWeek?, double, int?> Event;
    """
  When run generator for event
  Then generated for interface
    """
    event global::System.Func<global::System.DayOfWeek?, double, int?> Event;
    """
  And generated for struct implementation
    """
    public event global::System.Func<global::System.DayOfWeek?, double, int?> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for class implementation
    """
    public new event global::System.Func<global::System.DayOfWeek?, double, int?> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for sealed class implementation
    """
    public event global::System.Func<global::System.DayOfWeek?, double, int?> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
  And generated for static class implementation
    """
    public event global::System.Func<global::System.DayOfWeek?, double, int?> Event { add => @Namespace.@TypeName.Event += value; remove => @Namespace.@TypeName.Event -= value; }
    """
