using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using YT.IIGen.Attributes;

namespace YT.IIGen.UnitTests.Class;
public class GenerateMembersForFieldTests
{
  [Theory]

  // const
  [InlineData( // const field with keyworded type
    "public const int ConstField;",
    "int ConstField { get; }",
    "public new int ConstField { get => Something.Somewhere.SomeClass.ConstField; }"
  )]
  [InlineData( // const nullable field with keyworded type
    "public const int? ConstNullableField;",
    "int? ConstNullableField { get; }",
    "public new int? ConstNullableField { get => Something.Somewhere.SomeClass.ConstNullableField; }"
  )]

  // read-only
  [InlineData( // read-only field with keyworded type
    "public readonly string ReadonlyField;",
    "string ReadonlyField { get; }",
    "public new string ReadonlyField { get => _instance.ReadonlyField; }"
  )]
  [InlineData( // read-only nullable field with keyworded type
    "public readonly string? ReadonlyNullableField;",
    "string? ReadonlyNullableField { get; }",
    "public new string? ReadonlyNullableField { get => _instance.ReadonlyNullableField; }"
  )]
  [InlineData( // read-only field
    "public readonly DateTime ReadonlyField;",
    "global::System.DateTime ReadonlyField { get; }",
    "public new global::System.DateTime ReadonlyField { get => _instance.ReadonlyField; }"
  )]
  [InlineData( // read-only nullable field
    "public readonly DateTime? ReadonlyNullableField;",
    "global::System.DateTime? ReadonlyNullableField { get; }",
    "public new global::System.DateTime? ReadonlyNullableField { get => _instance.ReadonlyNullableField; }"
  )]

  // static read-only
  [InlineData( // static read-only field with keyworded type
    "public static readonly double StaticReadonlyField;",
    "double StaticReadonlyField { get; }",
    "public new double StaticReadonlyField { get => Something.Somewhere.SomeClass.StaticReadonlyField; }"
  )]
  [InlineData( // static read-only nullable field with keyworded type
    "public static readonly float? StaticReadonlyNullableField;",
    "float? StaticReadonlyNullableField { get; }",
    "public new float? StaticReadonlyNullableField { get => Something.Somewhere.SomeClass.StaticReadonlyNullableField; }"
  )]
  [InlineData( // static read-only field
    "public static readonly StringComparer StaticReadonlyField;",
    "global::System.StringComparer StaticReadonlyField { get; }",
    "public new global::System.StringComparer StaticReadonlyField { get => Something.Somewhere.SomeClass.StaticReadonlyField; }"
  )]
  [InlineData( // static read-only nullable field
    "public static readonly StringComparer? StaticReadonlyNullableField;",
    "global::System.StringComparer? StaticReadonlyNullableField { get; }",
    "public new global::System.StringComparer? StaticReadonlyNullableField { get => Something.Somewhere.SomeClass.StaticReadonlyNullableField; }"
  )]

  // regular
  [InlineData( // regular field with keyworded type
    "public nint Field;",
    "nint Field { get; set; }",
    "public new nint Field { get => _instance.Field; set => _instance.Field = value; }"
  )]
  [InlineData( // regular nullable field with keyworded type
    "public nint? NullableField;",
    "nint? NullableField { get; set; }",
    "public new nint? NullableField { get => _instance.NullableField; set => _instance.NullableField = value; }"
  )]
  [InlineData( // regular field
    "public DayOfWeek Field;",
    "global::System.DayOfWeek Field { get; set; }",
    "public new global::System.DayOfWeek Field { get => _instance.Field; set => _instance.Field = value; }"
  )]
  [InlineData( // regular nullable field
    "public DayOfWeek? NullableField;",
    "global::System.DayOfWeek? NullableField { get; set; }",
    "public new global::System.DayOfWeek? NullableField { get => _instance.NullableField; set => _instance.NullableField = value; }"
  )]

  //static
  [InlineData( // static field with keyworded type
    "public static object StaticField;",
    "object StaticField { get; set; }",
    "public new object StaticField { get => Something.Somewhere.SomeClass.StaticField; set => Something.Somewhere.SomeClass.StaticField = value; }"
  )]
  [InlineData( // static nullable field with keyworded type
    "public static object? StaticNullableField;",
    "object? StaticNullableField { get; set; }",
    "public new object? StaticNullableField { get => Something.Somewhere.SomeClass.StaticNullableField; set => Something.Somewhere.SomeClass.StaticNullableField = value; }"
  )]
  [InlineData( // static field
    "public static Delegate StaticField;",
    "global::System.Delegate StaticField { get; set; }",
    "public new global::System.Delegate StaticField { get => Something.Somewhere.SomeClass.StaticField; set => Something.Somewhere.SomeClass.StaticField = value; }"
  )]
  [InlineData( // static nullable field
    "public static Delegate? StaticNullableField;",
    "global::System.Delegate? StaticNullableField { get; set; }",
    "public new global::System.Delegate? StaticNullableField { get => Something.Somewhere.SomeClass.StaticNullableField; set => Something.Somewhere.SomeClass.StaticNullableField = value; }"
  )]
  public void GenerateMembersForFieldTest(string fieldInputSourceCode,
                                          string expectedFieldGeneratedForInterface,
                                          string expectedFieldGeneratedForImplementation)
  {
    var runResult = RunGenerator(fieldInputSourceCode);
    var (interfaceResult, implementationResult) =
      (runResult.GeneratedSources[0].SyntaxTree, runResult.GeneratedSources[1].SyntaxTree);
    var fieldGeneratedForInterface = GetPropertySourceText(interfaceResult);
    var fieldGeneratedForImplementation = GetPropertySourceText(implementationResult);

    runResult.Exception.Should().BeNull();
    fieldGeneratedForInterface.ToString().Should().Be(expectedFieldGeneratedForInterface);
    fieldGeneratedForImplementation.ToString().Should().Be(expectedFieldGeneratedForImplementation);
  }


  private static GeneratorRunResult RunGenerator(string fieldInputSourceCode)
  {
    var inputSourceCode = $@"
namespace Something.Somewhere
{{
  using System;

  public class SomeClass
  {{
    {fieldInputSourceCode}
  }}
}}

namespace My.Assembly.Code
{{
  using YT.IIGen.Attributes;
  using Something.Somewhere;

  [IIFor(typeof(SomeClass), ""SomeClassWrapper"")]
  internal partial interface ISomeClass {{ }}
}}
";
    var inputCompilation = Helper.CreateCompilation("My.Assembly", inputSourceCode,
      typeof(IIForAttribute),
      typeof(DateTime)
    );
    var runResults = CSharpGeneratorDriver.Create(new IIGenerator())
      .RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _)
      .GetRunResult()
      .Results;
    return runResults.First();
  }


  private static SourceText GetPropertySourceText(SyntaxTree syntaxTree)
  {
    return syntaxTree.GetRoot()
      .DescendantNodes()
      .First(n => n.IsKind(SyntaxKind.PropertyDeclaration))
      .NormalizeWhitespace()
      .GetText();
  }
}
