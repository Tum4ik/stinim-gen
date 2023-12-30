using Microsoft.CodeAnalysis.CSharp;
using Tum4ik.StinimGen.Specs.Extensions;

namespace Tum4ik.StinimGen.Specs.StepDefinitions;

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
