Feature: Regular method


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
    [IIFor(typeof(MethodHolder), WrapperClassName = "MethodHolderWrapper")]
    internal partial interface IMethodHolder { }
    """


Scenario: Void method without parameters
  Given source member declaration
    """
    public static void Method() { }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    void Method();
    """
  And generated implementation member must be
    """
    public void Method() => Methods.MethodHolder.Method();
    """


Scenario: Method without parameters returns keyworded type
  Given source member declaration
    """
    public static string Method() => string.Empty;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    global::System.String Method();
    """
  And generated implementation member must be
    """
    public global::System.String Method() => Methods.MethodHolder.Method();
    """


Scenario: Method without parameters returns keyworded nullable type
  Given source member declaration
    """
    public static string? Method() => null;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    global::System.String? Method();
    """
  And generated implementation member must be
    """
    public global::System.String? Method() => Methods.MethodHolder.Method();
    """


Scenario: Method without parameters returns non-nullable type
  Given source member declaration
    """
    public static StringBuilder Method() => new();
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    global::System.Text.StringBuilder Method();
    """
  And generated implementation member must be
    """
    public global::System.Text.StringBuilder Method() => Methods.MethodHolder.Method();
    """


Scenario: Method without parameters returns nullable type
  Given source member declaration
    """
    public static StringBuilder? Method() => null;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    global::System.Text.StringBuilder? Method();
    """
  And generated implementation member must be
    """
    public global::System.Text.StringBuilder? Method() => Methods.MethodHolder.Method();
    """


Scenario: Async void method without parameters
  Given source member declaration
    """
    public static async void MethodAsync() => await Task.CompletedTask;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    void MethodAsync();
    """
  And generated implementation member must be
    """
    public void MethodAsync() => Methods.MethodHolder.MethodAsync();
    """


Scenario: Async method without parameters returns Task
  Given source member declaration
    """
    public static async Task MethodAsync() => await Task.CompletedTask;
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    global::System.Threading.Tasks.Task MethodAsync();
    """
  And generated implementation member must be
    """
    public global::System.Threading.Tasks.Task MethodAsync() => Methods.MethodHolder.MethodAsync();
    """


Scenario: Method without parameters returns Taks of keyworded type
  Given source member declaration
    """
    public static async Task<string> MethodAsync() => await Task.FromResult(string.Empty);
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    global::System.Threading.Tasks.Task<global::System.String> MethodAsync();
    """
  And generated implementation member must be
    """
    public global::System.Threading.Tasks.Task<global::System.String> MethodAsync() => Methods.MethodHolder.MethodAsync();
    """


Scenario: Method without parameters returns Task of keyworded nullable type
  Given source member declaration
    """
    public static async Task<string?> MethodAsync() => await Task.FromResult(string.Empty);
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    global::System.Threading.Tasks.Task<global::System.String?> MethodAsync();
    """
  And generated implementation member must be
    """
    public global::System.Threading.Tasks.Task<global::System.String?> MethodAsync() => Methods.MethodHolder.MethodAsync();
    """


Scenario: Method without parameters returns Task of non-nullable type
  Given source member declaration
    """
    public static async Task<StringBuilder> MethodAsync() => await Task.FromResult(new StringBuilder());
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    global::System.Threading.Tasks.Task<global::System.Text.StringBuilder> MethodAsync();
    """
  And generated implementation member must be
    """
    public global::System.Threading.Tasks.Task<global::System.Text.StringBuilder> MethodAsync() => Methods.MethodHolder.MethodAsync();
    """


Scenario: Method without parameters returns Task of nullable type
  Given source member declaration
    """
    public static async Task<StringBuilder?> MethodAsync() => await Task.FromResult(new StringBuilder());
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    global::System.Threading.Tasks.Task<global::System.Text.StringBuilder?> MethodAsync();
    """
  And generated implementation member must be
    """
    public global::System.Threading.Tasks.Task<global::System.Text.StringBuilder?> MethodAsync() => Methods.MethodHolder.MethodAsync();
    """


Scenario: Void method with a parameter
  Given source member declaration
    """
    public static void Method(int p) { }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    void Method(global::System.Int32 p);
    """
  And generated implementation member must be
    """
    public void Method(global::System.Int32 p) => Methods.MethodHolder.Method(p);
    """


Scenario: Void method with default parameters
  Given source member declaration
    """
    public static void Method(int p1, string p2 = "def", int p3 = 10) { }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    void Method(global::System.Int32 p1, global::System.String p2 = "def", global::System.Int32 p3 = 10);
    """
  And generated implementation member must be
    """
    public void Method(global::System.Int32 p1, global::System.String p2 = "def", global::System.Int32 p3 = 10) => Methods.MethodHolder.Method(p1, p2, p3);
    """


Scenario: Void method with several keyworded parameters
  Given source member declaration
    """
    public static void Method(int p1, string[] p2, float? p3, double?[] p4, object[]? p5) { }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    void Method(global::System.Int32 p1, global::System.String[] p2, global::System.Single? p3, global::System.Double? [] p4, global::System.Object[]? p5);
    """
  And generated implementation member must be
    """
    public void Method(global::System.Int32 p1, global::System.String[] p2, global::System.Single? p3, global::System.Double? [] p4, global::System.Object[]? p5) => Methods.MethodHolder.Method(p1, p2, p3, p4, p5);
    """


Scenario: Void method with several non-keyworded parameters
  Given source member declaration
    """
    public static void Method(DateTime p1, DateTime[] p2, DateTime? p3, DateTime?[] p4, DateTime[]? p5) { }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    void Method(global::System.DateTime p1, global::System.DateTime[] p2, global::System.DateTime? p3, global::System.DateTime? [] p4, global::System.DateTime[]? p5);
    """
  And generated implementation member must be
    """
    public void Method(global::System.DateTime p1, global::System.DateTime[] p2, global::System.DateTime? p3, global::System.DateTime? [] p4, global::System.DateTime[]? p5) => Methods.MethodHolder.Method(p1, p2, p3, p4, p5);
    """


Scenario: Void method with several keyworded generic parameters
  Given source member declaration
    """
    public static void Method(List<int> p1, List<string[]> p2, List<float?> p3, List<double?[]> p4, List<object[]?> p5) { }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    void Method(global::System.Collections.Generic.List<global::System.Int32> p1, global::System.Collections.Generic.List<global::System.String[]> p2, global::System.Collections.Generic.List<global::System.Single?> p3, global::System.Collections.Generic.List<global::System.Double? []> p4, global::System.Collections.Generic.List<global::System.Object[]?> p5);
    """
  And generated implementation member must be
    """
    public void Method(global::System.Collections.Generic.List<global::System.Int32> p1, global::System.Collections.Generic.List<global::System.String[]> p2, global::System.Collections.Generic.List<global::System.Single?> p3, global::System.Collections.Generic.List<global::System.Double? []> p4, global::System.Collections.Generic.List<global::System.Object[]?> p5) => Methods.MethodHolder.Method(p1, p2, p3, p4, p5);
    """


Scenario: Void method with several non-keyworded generic parameters
  Given source member declaration
    """
    public static void Method(List<DateTime> p1, List<DateTime[]> p2, List<DateTime?> p3, List<DateTime?[]> p4, List<DateTime[]?> p5) { }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    void Method(global::System.Collections.Generic.List<global::System.DateTime> p1, global::System.Collections.Generic.List<global::System.DateTime[]> p2, global::System.Collections.Generic.List<global::System.DateTime?> p3, global::System.Collections.Generic.List<global::System.DateTime? []> p4, global::System.Collections.Generic.List<global::System.DateTime[]?> p5);
    """
  And generated implementation member must be
    """
    public void Method(global::System.Collections.Generic.List<global::System.DateTime> p1, global::System.Collections.Generic.List<global::System.DateTime[]> p2, global::System.Collections.Generic.List<global::System.DateTime?> p3, global::System.Collections.Generic.List<global::System.DateTime? []> p4, global::System.Collections.Generic.List<global::System.DateTime[]?> p5) => Methods.MethodHolder.Method(p1, p2, p3, p4, p5);
    """


Scenario: Void method with ref/out/in and params parameters
  Given source member declaration
    """
    public static void Method(ref int refP, out double? outP, in DateTime inP, params DateTime?[] paramsP) { }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    void Method(ref global::System.Int32 refP, out global::System.Double? outP, in global::System.DateTime inP, params global::System.DateTime? [] paramsP);
    """
  And generated implementation member must be
    """
    public void Method(ref global::System.Int32 refP, out global::System.Double? outP, in global::System.DateTime inP, params global::System.DateTime? [] paramsP) => Methods.MethodHolder.Method(ref refP, out outP, in inP, paramsP);
    """
