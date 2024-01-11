Feature: Generic method


Background:
  Given source declaration
    """
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    namespace Methods;
    public class MethodHolder
    {
      <member>
    }
    """
  And attribute usage
    """
    using Tum4ik.StinimGen.Attributes;
    using Methods;
    namespace Attribute.Usage;
    [IIFor(typeof(MethodHolder), "MethodHolderWrapper")]
    internal partial interface IMethodHolder { }
    """


Scenario: Void method with a generic parameter and without parameters
  Given source member declaration
    """
    public static void Method<T>() { }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    void Method<T>();
    """
  And generated implementation member must be
    """
    public void Method<T>() => Methods.MethodHolder.Method<T>();
    """


Scenario: Void method with several generic parameters and without parameters
  Given source member declaration
    """
    public static void Method<T1, T2, T3>() { }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    void Method<T1, T2, T3>();
    """
  And generated implementation member must be
    """
    public void Method<T1, T2, T3>() => Methods.MethodHolder.Method<T1, T2, T3>();
    """


Scenario: Method without parameters returns generic type
  Given source member declaration
    """
    public static T Method<T>() { }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    T Method<T>();
    """
  And generated implementation member must be
    """
    public T Method<T>() => Methods.MethodHolder.Method<T>();
    """


Scenario: Method with parameters returns generic type
  Given source member declaration
    """
    public static T1 Method<T1, T2>(T1 p1, T2 p2) { }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    T1 Method<T1, T2>(T1 p1, T2 p2);
    """
  And generated implementation member must be
    """
    public T1 Method<T1, T2>(T1 p1, T2 p2) => Methods.MethodHolder.Method<T1, T2>(p1, p2);
    """


Scenario: Async method without parameters returns Task of generic type
  Given source member declaration
    """
    public static async Task<T> MethodAsync<T>() => await Task.FromResult<T>(default);
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    global::System.Threading.Tasks.Task<T> MethodAsync<T>();
    """
  And generated implementation member must be
    """
    public global::System.Threading.Tasks.Task<T> MethodAsync<T>() => Methods.MethodHolder.MethodAsync<T>();
    """


Scenario: Generic method with a constraint
  Given source member declaration
    """
    public static void Method<T>() where T : class, new() { }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    void Method<T>()
        where T : class, new();
    """
  And generated implementation member must be
    """
    public void Method<T>()
        where T : class, new() => Methods.MethodHolder.Method<T>();
    """


Scenario: Generic method with several constraints
  Given source member declaration
    """
    public static void Method<TParent, TChild>()
      where TParent : class
      where TChild : TParent, new()
    { }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    void Method<TParent, TChild>()
        where TParent : class where TChild : TParent, new();
    """
  And generated implementation member must be
    """
    public void Method<TParent, TChild>()
        where TParent : class where TChild : TParent, new() => Methods.MethodHolder.Method<TParent, TChild>();
    """
