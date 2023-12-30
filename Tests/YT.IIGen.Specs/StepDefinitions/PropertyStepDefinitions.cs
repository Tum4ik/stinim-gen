using Microsoft.CodeAnalysis.CSharp;
using YT.IIGen.Specs.Extensions;

namespace YT.IIGen.Specs.StepDefinitions;

[Binding]
public class PropertyStepDefinitions
{
  private readonly ScenarioContext _scenarioContext;

  public PropertyStepDefinitions(ScenarioContext scenarioContext)
  {
    _scenarioContext = scenarioContext;
  }


  [When(@"run generator for property")]
  public void WhenRunGeneratorForProperty()
  {
    _scenarioContext.AddInterfaceGeneratedMemberDeclarationKind(SyntaxKind.PropertyDeclaration);
    _scenarioContext.AddImplementationGeneratedMemberDeclarationKind(SyntaxKind.PropertyDeclaration);
  }
}
