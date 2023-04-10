namespace YT.IIGen.Templates;

internal class InterfaceGenerator
{
  private InterfaceFields? _fields;

  public InterfaceGenerator AddFields(InterfaceFields fields)
  {
    _fields = fields;
    return this;
  }


  public string Generate(string nameSpace, string accessModifiers, string interfaceName)
  {
    return $@"namespace {nameSpace};
{accessModifiers} interface {interfaceName}
{{
{_fields}
}}
";
  }
}
