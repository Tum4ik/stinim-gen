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
    [IIFor(typeof(EventHolder), WrapperClassName = "EventHolderWrapper")]
    internal partial interface IEventHolder { }
    """


Scenario: Event field with EventHandler type
  Given source member declaration
    """
    public static event EventHandler EventMember;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Events.EventHolder.EventMember"/>
    event global::System.EventHandler EventMember;
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public event global::System.EventHandler EventMember { add => Events.EventHolder.EventMember += value; remove => Events.EventHolder.EventMember -= value; }
    """


Scenario: Event field with custom type
  Given source member declaration
    """
    public static event CustomEventHandler EventMember;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Events.EventHolder.EventMember"/>
    event global::Events.CustomEventHandler EventMember;
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public event global::Events.CustomEventHandler EventMember { add => Events.EventHolder.EventMember += value; remove => Events.EventHolder.EventMember -= value; }
    """


Scenario: Event field with Action type
  Given source member declaration
    """
    public static event Action EventMember;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Events.EventHolder.EventMember"/>
    event global::System.Action EventMember;
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public event global::System.Action EventMember { add => Events.EventHolder.EventMember += value; remove => Events.EventHolder.EventMember -= value; }
    """


Scenario: Event field with Action type with keyworded generic parameter
  Given source member declaration
    """
    public static event Action<int> EventMember;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Events.EventHolder.EventMember"/>
    event global::System.Action<int> EventMember;
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public event global::System.Action<int> EventMember { add => Events.EventHolder.EventMember += value; remove => Events.EventHolder.EventMember -= value; }
    """


Scenario: Event field with Action type with keyworded nullable generic parameter
  Given source member declaration
    """
    public static event Action<string?> EventMember;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Events.EventHolder.EventMember"/>
    event global::System.Action<string?> EventMember;
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public event global::System.Action<string?> EventMember { add => Events.EventHolder.EventMember += value; remove => Events.EventHolder.EventMember -= value; }
    """


Scenario: Event field with Action type with non-keyworded generic parameter
  Given source member declaration
    """
    public static event Action<DateTime> EventMember;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Events.EventHolder.EventMember"/>
    event global::System.Action<global::System.DateTime> EventMember;
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public event global::System.Action<global::System.DateTime> EventMember { add => Events.EventHolder.EventMember += value; remove => Events.EventHolder.EventMember -= value; }
    """


Scenario: Event field with Action type with non-keyworded nullable generic parameter
  Given source member declaration
    """
    public static event Action<DayOfWeek?> EventMember;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Events.EventHolder.EventMember"/>
    event global::System.Action<global::System.DayOfWeek?> EventMember;
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public event global::System.Action<global::System.DayOfWeek?> EventMember { add => Events.EventHolder.EventMember += value; remove => Events.EventHolder.EventMember -= value; }
    """


Scenario: Event field with Func
  Given source member declaration
    """
    public static event Func<DayOfWeek?, double, int?> EventMember;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Events.EventHolder.EventMember"/>
    event global::System.Func<global::System.DayOfWeek?, double, int?> EventMember;
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public event global::System.Func<global::System.DayOfWeek?, double, int?> EventMember { add => Events.EventHolder.EventMember += value; remove => Events.EventHolder.EventMember -= value; }
    """


Scenario: Full event
  Given source member declaration
    """
    private static event EventHandler _fullEvent;
    public static event EventHandler FullEvent
    {
      add => _fullEvent += value;
      remove => _fullEvent -= value;
    }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Events.EventHolder.FullEvent"/>
    event global::System.EventHandler FullEvent;
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public event global::System.EventHandler FullEvent { add => Events.EventHolder.FullEvent += value; remove => Events.EventHolder.FullEvent -= value; }
    """


Scenario: Forward Obsolete attribute
  Given source member declaration
    """
    [Obsolete("Obsolete event")]
    public static event EventHandler ObsoleteEvent;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "Events.EventHolder.ObsoleteEvent"/>
    [global::System.ObsoleteAttribute("Obsolete event")]
    event global::System.EventHandler ObsoleteEvent;
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    [global::System.ObsoleteAttribute("Obsolete event")]
    public event global::System.EventHandler ObsoleteEvent { add => Events.EventHolder.ObsoleteEvent += value; remove => Events.EventHolder.ObsoleteEvent -= value; }
    """
