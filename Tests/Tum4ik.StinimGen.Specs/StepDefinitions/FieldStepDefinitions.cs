using Microsoft.CodeAnalysis.CSharp;
using Tum4ik.StinimGen.Specs.Extensions;

namespace Tum4ik.StinimGen.Specs.StepDefinitions;

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
