Feature: Regular method


Background:
  Given source declaration
    """
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
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
    /// <inheritdoc cref = "global::Methods.MethodHolder.Method()"/>
    void Method();
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public void Method() => global::Methods.MethodHolder.Method();
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
    /// <inheritdoc cref = "global::Methods.MethodHolder.Method()"/>
    global::System.String Method();
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public global::System.String Method() => global::Methods.MethodHolder.Method();
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
    /// <inheritdoc cref = "global::Methods.MethodHolder.Method()"/>
    global::System.String? Method();
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public global::System.String? Method() => global::Methods.MethodHolder.Method();
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
    /// <inheritdoc cref = "global::Methods.MethodHolder.Method()"/>
    global::System.Text.StringBuilder Method();
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public global::System.Text.StringBuilder Method() => global::Methods.MethodHolder.Method();
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
    /// <inheritdoc cref = "global::Methods.MethodHolder.Method()"/>
    global::System.Text.StringBuilder? Method();
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public global::System.Text.StringBuilder? Method() => global::Methods.MethodHolder.Method();
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
    /// <inheritdoc cref = "global::Methods.MethodHolder.MethodAsync()"/>
    void MethodAsync();
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public void MethodAsync() => global::Methods.MethodHolder.MethodAsync();
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
    /// <inheritdoc cref = "global::Methods.MethodHolder.MethodAsync()"/>
    global::System.Threading.Tasks.Task MethodAsync();
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public global::System.Threading.Tasks.Task MethodAsync() => global::Methods.MethodHolder.MethodAsync();
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
    /// <inheritdoc cref = "global::Methods.MethodHolder.MethodAsync()"/>
    global::System.Threading.Tasks.Task<global::System.String> MethodAsync();
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public global::System.Threading.Tasks.Task<global::System.String> MethodAsync() => global::Methods.MethodHolder.MethodAsync();
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
    /// <inheritdoc cref = "global::Methods.MethodHolder.MethodAsync()"/>
    global::System.Threading.Tasks.Task<global::System.String?> MethodAsync();
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public global::System.Threading.Tasks.Task<global::System.String?> MethodAsync() => global::Methods.MethodHolder.MethodAsync();
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
    /// <inheritdoc cref = "global::Methods.MethodHolder.MethodAsync()"/>
    global::System.Threading.Tasks.Task<global::System.Text.StringBuilder> MethodAsync();
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public global::System.Threading.Tasks.Task<global::System.Text.StringBuilder> MethodAsync() => global::Methods.MethodHolder.MethodAsync();
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
    /// <inheritdoc cref = "global::Methods.MethodHolder.MethodAsync()"/>
    global::System.Threading.Tasks.Task<global::System.Text.StringBuilder?> MethodAsync();
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public global::System.Threading.Tasks.Task<global::System.Text.StringBuilder?> MethodAsync() => global::Methods.MethodHolder.MethodAsync();
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
    /// <inheritdoc cref = "global::Methods.MethodHolder.Method(global::System.Int32)"/>
    void Method(global::System.Int32 p);
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public void Method(global::System.Int32 p) => global::Methods.MethodHolder.Method(p);
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
    /// <inheritdoc cref = "global::Methods.MethodHolder.Method(global::System.Int32, global::System.String, global::System.Int32)"/>
    void Method(global::System.Int32 p1, global::System.String p2 = "def", global::System.Int32 p3 = 10);
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public void Method(global::System.Int32 p1, global::System.String p2 = "def", global::System.Int32 p3 = 10) => global::Methods.MethodHolder.Method(p1, p2, p3);
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
    /// <inheritdoc cref = "global::Methods.MethodHolder.Method(global::System.Int32, global::System.String[], global::System.Single? , global::System.Double? [], global::System.Object[] ? )"/>
    void Method(global::System.Int32 p1, global::System.String[] p2, global::System.Single? p3, global::System.Double? [] p4, global::System.Object[]? p5);
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public void Method(global::System.Int32 p1, global::System.String[] p2, global::System.Single? p3, global::System.Double? [] p4, global::System.Object[]? p5) => global::Methods.MethodHolder.Method(p1, p2, p3, p4, p5);
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
    /// <inheritdoc cref = "global::Methods.MethodHolder.Method(global::System.DateTime, global::System.DateTime[], global::System.DateTime? , global::System.DateTime? [], global::System.DateTime[] ? )"/>
    void Method(global::System.DateTime p1, global::System.DateTime[] p2, global::System.DateTime? p3, global::System.DateTime? [] p4, global::System.DateTime[]? p5);
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public void Method(global::System.DateTime p1, global::System.DateTime[] p2, global::System.DateTime? p3, global::System.DateTime? [] p4, global::System.DateTime[]? p5) => global::Methods.MethodHolder.Method(p1, p2, p3, p4, p5);
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
    /// <inheritdoc cref = "global::Methods.MethodHolder.Method(global::System.Collections.Generic.List{global::System.Int32}, global::System.Collections.Generic.List{global::System.String[]}, global::System.Collections.Generic.List{global::System.Single?}, global::System.Collections.Generic.List{global::System.Double? []}, global::System.Collections.Generic.List{global::System.Object[]?  } )"/>
    void Method(global::System.Collections.Generic.List<global::System.Int32> p1, global::System.Collections.Generic.List<global::System.String[]> p2, global::System.Collections.Generic.List<global::System.Single?> p3, global::System.Collections.Generic.List<global::System.Double? []> p4, global::System.Collections.Generic.List<global::System.Object[]?> p5);
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public void Method(global::System.Collections.Generic.List<global::System.Int32> p1, global::System.Collections.Generic.List<global::System.String[]> p2, global::System.Collections.Generic.List<global::System.Single?> p3, global::System.Collections.Generic.List<global::System.Double? []> p4, global::System.Collections.Generic.List<global::System.Object[]?> p5) => global::Methods.MethodHolder.Method(p1, p2, p3, p4, p5);
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
    /// <inheritdoc cref = "global::Methods.MethodHolder.Method(global::System.Collections.Generic.List{global::System.DateTime}, global::System.Collections.Generic.List{global::System.DateTime[]}, global::System.Collections.Generic.List{global::System.DateTime?}, global::System.Collections.Generic.List{global::System.DateTime? []}, global::System.Collections.Generic.List{global::System.DateTime[]?  } )"/>
    void Method(global::System.Collections.Generic.List<global::System.DateTime> p1, global::System.Collections.Generic.List<global::System.DateTime[]> p2, global::System.Collections.Generic.List<global::System.DateTime?> p3, global::System.Collections.Generic.List<global::System.DateTime? []> p4, global::System.Collections.Generic.List<global::System.DateTime[]?> p5);
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public void Method(global::System.Collections.Generic.List<global::System.DateTime> p1, global::System.Collections.Generic.List<global::System.DateTime[]> p2, global::System.Collections.Generic.List<global::System.DateTime?> p3, global::System.Collections.Generic.List<global::System.DateTime? []> p4, global::System.Collections.Generic.List<global::System.DateTime[]?> p5) => global::Methods.MethodHolder.Method(p1, p2, p3, p4, p5);
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
    /// <inheritdoc cref = "global::Methods.MethodHolder.Method(ref global::System.Int32, out global::System.Double? , in global::System.DateTime, global::System.DateTime? [])"/>
    void Method(ref global::System.Int32 refP, out global::System.Double? outP, in global::System.DateTime inP, params global::System.DateTime? [] paramsP);
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public void Method(ref global::System.Int32 refP, out global::System.Double? outP, in global::System.DateTime inP, params global::System.DateTime? [] paramsP) => global::Methods.MethodHolder.Method(ref refP, out outP, in inP, paramsP);
    """


Scenario: Forward Obsolete attribute
  Given source member declaration
    """
    [Obsolete("Obsolete method")]
    public static void ObsoleteMethod() { }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "global::Methods.MethodHolder.ObsoleteMethod()"/>
    [global::System.ObsoleteAttribute("Obsolete method")]
    void ObsoleteMethod();
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    [global::System.ObsoleteAttribute("Obsolete method")]
    public void ObsoleteMethod() => global::Methods.MethodHolder.ObsoleteMethod();
    """


Scenario: Forward parameters attributes
  Given source member declaration
    """
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false), DisallowNull][AllowNull] out int result)
    {
      result = 0;
      return false;
    }
    """
  When run generator
  Then there must not be generation exception
  And generated interface member must be
    """
    /// <inheritdoc cref = "global::Methods.MethodHolder.TryParse(global::System.String? , out global::System.Int32)"/>
    global::System.Boolean TryParse([global::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute(true)] global::System.String? s, [global::System.Diagnostics.CodeAnalysis.MaybeNullWhenAttribute(false)][global::System.Diagnostics.CodeAnalysis.DisallowNullAttribute][global::System.Diagnostics.CodeAnalysis.AllowNullAttribute] out global::System.Int32 result);
    """
  And generated implementation member must be
    """
    /// <inheritdoc/>
    public global::System.Boolean TryParse([global::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute(true)] global::System.String? s, [global::System.Diagnostics.CodeAnalysis.MaybeNullWhenAttribute(false)][global::System.Diagnostics.CodeAnalysis.DisallowNullAttribute][global::System.Diagnostics.CodeAnalysis.AllowNullAttribute] out global::System.Int32 result) => global::Methods.MethodHolder.TryParse(s, out result);
    """
