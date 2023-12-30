namespace YT.IIGen.Sandbox.Types;
internal static class StaticClass
{
  public const int ConstInt = 0;
  public const string ConstString = "ConstString";
  public const string? ConstNullableString = "ConstNullableString";

  public static int StaticInt = 1;
  public static int? StaticNullableInt = 2;
  public static DummyStruct StaticDummyStruct;
  public static DummyStruct? StaticNullableDummyStruct;
  public static string StaticString = "StaticString";
  public static string? StaticNullableString = "StaticNullableString";
  public static DummyClass StaticDummyClass = new();
  public static DummyClass? StaticNullableDummyClass = new();

  public static readonly int StaticReadonlyInt = 3;
  public static readonly int? StaticReadonlyNullableInt = 4;
  public static readonly DummyStruct StaticReadonlyDummyStruct;
  public static readonly DummyStruct? StaticReadonlyNullableDummyStruct;
  public static readonly string StaticReadonlyString = "StaticReadonlyString";
  public static readonly string? StaticReadonlyNullableString = "StaticReadonlyNullableString";
  public static readonly DummyClass StaticReadonlyDummyClass = new();
  public static readonly DummyClass? StaticReadonlyNullableDummyClass = new();


  public static int IntPropertyGetSet { get; set; }
  public static int? IntPropertyGet { get; }
  public static DummyStruct DummyStructPropertySet { set => StaticDummyStruct = value; }
  public static DummyStruct? DummyStructPropertyPrivateGetSet { private get; set; }
  public static string StringPropertyGetPrivateSet { get; set; }


  public static event EventHandler Event;
  public static event EventHandler? NullableEvent;


  public static int Method(int intParma,
                           out DummyClass outParam,
                           double def = default,
                           float fl = 0.4f,
                           string str = "str",
                           DummyClass? dummyClass = null,
                           params DummyStruct[] dummyParams)
  => Method(intParma, out outParam, def, fl, str, dummyClass, dummyParams);
}
