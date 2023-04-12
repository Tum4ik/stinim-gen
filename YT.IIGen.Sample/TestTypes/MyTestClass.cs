namespace YT.IIGen.Sample.TestTypes;
internal class MyTestClass : MyBaseTestClass
{
  public const int ConstIntField = 1;
  public int IntField = 2;
  public readonly int ReadonlyIntField = 3;
  public static int StaticIntField = 4;
  public static readonly int StaticReadonlyIntField = 5;

  public const string ConstStringField = "1";
  public string StringField = "2";
  public readonly string ReadonlyStringField = "3";
  public static string StaticStringField = "4";
  public static readonly string StaticReadonlyStringField = "5";

  public MyTestClass MyTestClassField = new();
  public readonly MyTestClass ReadonlyMyTestClassField = new();
  public static MyTestClass StaticMyTestClassField = new();
  public static readonly MyTestClass StaticReadonlyMyTestClassField = new();

}
