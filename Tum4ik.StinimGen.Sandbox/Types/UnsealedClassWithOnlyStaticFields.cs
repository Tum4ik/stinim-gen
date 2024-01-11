using Tum4ik.StinimGen.Sandbox.Types;

namespace Tum4ik.StinimGen.Sandbox.Types;
internal class UnsealedClassWithOnlyStaticFields
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

  public static int Prop { get; protected set; }


  public static event EventHandler PropChanged;
  public static event Func<int> Event;
}
