namespace YT.IIGen.Sandbox.Types;
internal sealed class SealedClass
{
  public const int ConstInt = 0;
  public const string ConstString = "ConstString";
  public const string? ConstNullableString = "ConstNullableString";

  public int RegularInt = 1;
  public int? RegularNullableInt = 2;
  public DummyStruct RegularDummyStruct;
  public DummyStruct? RegularNullableDummyStruct;
  public string RegularString = "RegularString";
  public string? RegularNullableString = "RegularNullableString";
  public DummyClass RegularDummyClass = new();
  public DummyClass? RegularNullableDummyClass = new();

  public static int StaticInt = 1;
  public static int? StaticNullableInt = 2;
  public static DummyStruct StaticDummyStruct;
  public static DummyStruct? StaticNullableDummyStruct;
  public static string StaticString = "StaticString";
  public static string? StaticNullableString = "StaticNullableString";
  public static DummyClass StaticDummyClass = new();
  public static DummyClass? StaticNullableDummyClass = new();

  public readonly int ReadonlyInt = 3;
  public readonly int? ReadonlyNullableInt = 4;
  public readonly DummyStruct ReadonlyDummyStruct;
  public readonly DummyStruct? ReadonlyNullableDummyStruct;
  public readonly string ReadonlyString = "ReadonlyString";
  public readonly string? ReadonlyNullableString = "ReadonlyNullableString";
  public readonly DummyClass ReadonlyDummyClass = new();
  public readonly DummyClass? ReadonlyNullableDummyClass = new();

  public static readonly int StaticReadonlyInt = 3;
  public static readonly int? StaticReadonlyNullableInt = 4;
  public static readonly DummyStruct StaticReadonlyDummyStruct;
  public static readonly DummyStruct? StaticReadonlyNullableDummyStruct;
  public static readonly string StaticReadonlyString = "StaticReadonlyString";
  public static readonly string? StaticReadonlyNullableString = "StaticReadonlyNullableString";
  public static readonly DummyClass StaticReadonlyDummyClass = new();
  public static readonly DummyClass? StaticReadonlyNullableDummyClass = new();
}
