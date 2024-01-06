Feature: Event


Background:
  Given source declaration
    """
    using System;
    namespace Events;
    public class EventHolder
    {
      <member>
    }
    
    public delegate void CustomEventHandler(int count, string search);
    """
  And attribute usage
    """
    using Tum4ik.StinimGen.Attributes;
    using Events;
    namespace Attribute.Usage;
    [IIFor(typeof(EventHolder), "EventHolderWrapper")]
    internal partial interface IEventHolder { }
    """


Scenario: Event with EventHandler type
  Given source member declaration
    """
    public static event EventHandler EventMember;
    """
  When run generator
  Then there must not be generation exception
  Then generated interface member must be
    """
    event global::System.EventHandler EventMember;
    """
  And generated implementation member must be
    """
    public event global::System.EventHandler EventMember { add => Events.EventHolder.EventMember += value; remove => Events.EventHolder.EventMember -= value; }
    """


Scenario: Event with custom type
  Given source member declaration
    """
    public static event CustomEventHandler EventMember;
    """
  When run generator
  Then there must not be generation exception
  Then generated interface member must be
    """
    event global::Events.CustomEventHandler EventMember;
    """
  And generated implementation member must be
    """
    public event global::Events.CustomEventHandler EventMember { add => Events.EventHolder.EventMember += value; remove => Events.EventHolder.EventMember -= value; }
    """


Scenario: Event with Action type
  Given source member declaration
    """
    public static event Action EventMember;
    """
  When run generator
  Then there must not be generation exception
  Then generated interface member must be
    """
    event global::System.Action EventMember;
    """
  And generated implementation member must be
    """
    public event global::System.Action EventMember { add => Events.EventHolder.EventMember += value; remove => Events.EventHolder.EventMember -= value; }
    """


Scenario: Event with Action type with keyworded generic parameter
  Given source member declaration
    """
    public static event Action<int> EventMember;
    """
  When run generator
  Then there must not be generation exception
  Then generated interface member must be
    """
    event global::System.Action<int> EventMember;
    """
  And generated implementation member must be
    """
    public event global::System.Action<int> EventMember { add => Events.EventHolder.EventMember += value; remove => Events.EventHolder.EventMember -= value; }
    """


Scenario: Event with Action type with keyworded nullable generic parameter
  Given source member declaration
    """
    public static event Action<string?> EventMember;
    """
  When run generator
  Then there must not be generation exception
  Then generated interface member must be
    """
    event global::System.Action<string?> EventMember;
    """
  And generated implementation member must be
    """
    public event global::System.Action<string?> EventMember { add => Events.EventHolder.EventMember += value; remove => Events.EventHolder.EventMember -= value; }
    """


Scenario: Event with Action type with non-keyworded generic parameter
  Given source member declaration
    """
    public static event Action<DateTime> EventMember;
    """
  When run generator
  Then there must not be generation exception
  Then generated interface member must be
    """
    event global::System.Action<global::System.DateTime> EventMember;
    """
  And generated implementation member must be
    """
    public event global::System.Action<global::System.DateTime> EventMember { add => Events.EventHolder.EventMember += value; remove => Events.EventHolder.EventMember -= value; }
    """


Scenario: Event with Action type with non-keyworded nullable generic parameter
  Given source member declaration
    """
    public static event Action<DayOfWeek?> EventMember;
    """
  When run generator
  Then there must not be generation exception
  Then generated interface member must be
    """
    event global::System.Action<global::System.DayOfWeek?> EventMember;
    """
  And generated implementation member must be
    """
    public event global::System.Action<global::System.DayOfWeek?> EventMember { add => Events.EventHolder.EventMember += value; remove => Events.EventHolder.EventMember -= value; }
    """


Scenario: Event with Func
  Given source member declaration
    """
    public static event Func<DayOfWeek?, double, int?> EventMember;
    """
  When run generator
  Then there must not be generation exception
  Then generated interface member must be
    """
    event global::System.Func<global::System.DayOfWeek?, double, int?> EventMember;
    """
  And generated implementation member must be
    """
    public event global::System.Func<global::System.DayOfWeek?, double, int?> EventMember { add => Events.EventHolder.EventMember += value; remove => Events.EventHolder.EventMember -= value; }
    """
