using Microsoft.CodeAnalysis.CSharp;
using YT.IIGen.Specs.Extensions;

namespace YT.IIGen.Specs.StepDefinitions;

[Binding]
public class FieldStepDefinitions
{
  private readonly ScenarioContext _scenarioContext;

  public FieldStepDefinitions(ScenarioContext scenarioContext)
  {
    _scenarioContext = scenarioContext;
  }


  [When(@"run generator for field")]
  public void WhenRunGeneratorForField()
  {
    _scenarioContext.AddInterfaceGeneratedMemberDeclarationKind(SyntaxKind.PropertyDeclaration);
    _scenarioContext.AddImplementationGeneratedMemberDeclarationKind(SyntaxKind.PropertyDeclaration);
  }
}
