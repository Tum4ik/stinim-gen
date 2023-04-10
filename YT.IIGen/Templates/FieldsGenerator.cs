using System.Text;
using Microsoft.CodeAnalysis;

namespace YT.IIGen.Templates;
internal class FieldsGenerator
{
  private readonly IEnumerable<IFieldSymbol> _fieldSymbols;

  public FieldsGenerator(IEnumerable<IFieldSymbol> fieldSymbols)
  {
    _fieldSymbols = fieldSymbols;
  }


  public GeneratedFields Generate(INamedTypeSymbol fieldsSource)
  {
    var interfaceFieldsBuilder = new StringBuilder();
    var implementationFields = new StringBuilder();

    var callee = fieldsSource.ToString();

    if (!fieldsSource.IsStatic)
    {
      callee = "_instance";
      implementationFields.AppendLine($"  private readonly {fieldsSource} {callee} = new();");
      implementationFields.AppendLine();
    }

    foreach (var fieldSymbol in _fieldSymbols)
    {
      var typeFullName = fieldSymbol.Type.ToString();
      var name = fieldSymbol.Name;
      var hasSetter = !fieldSymbol.IsConst && !fieldSymbol.IsReadOnly;

      interfaceFieldsBuilder.AppendLine($"  {typeFullName} {name} {{ get; {(hasSetter ? "set; " : string.Empty)}}}");

      implementationFields
        .AppendLine($"  public {typeFullName} {name}")
        .AppendLine("  {")
        .AppendLine($"    get => {callee}.{name};");
      if (hasSetter)
      {
        implementationFields.AppendLine($"    set => {callee}.{name} = value;");
      }
      implementationFields.AppendLine("  }").AppendLine();
    }

    return new(new(interfaceFieldsBuilder.ToString()), new(implementationFields.ToString()));
  }
}


internal class GeneratedFields
{
  public GeneratedFields(InterfaceFields interfaceFields, ImplementationFields implementationFields)
  {
    InterfaceFields = interfaceFields;
    ImplementationFields = implementationFields;
  }

  public InterfaceFields InterfaceFields { get; }
  public ImplementationFields ImplementationFields { get; }
}


internal class InterfaceFields : SourceCode
{
  public InterfaceFields(string sourceCode) : base(sourceCode)
  {
  }
}


internal class ImplementationFields : SourceCode
{
  public ImplementationFields(string sourceCode) : base(sourceCode)
  {
  }
}


internal abstract class SourceCode
{
  private readonly string _sourceCode;

  protected SourceCode(string sourceCode)
  {
    _sourceCode = sourceCode;
  }


  public override string ToString()
  {
    return _sourceCode;
  }
}
