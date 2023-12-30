using Microsoft.CodeAnalysis.CSharp;
using Tum4ik.StinimGen.Specs.Extensions;

namespace Tum4ik.StinimGen.Specs.StepDefinitions;

[Binding]
public class EventStepDefinitions
{
  private readonly ScenarioContext _scenarioContext;

  public EventStepDefinitions(ScenarioContext scenarioContext)
  {
    _scenarioContext = scenarioContext;
  }


  [When(@"run generator for event")]
  public void WhenRunGeneratorForEvent()
  {
    _scenarioContext.AddInterfaceGeneratedMemberDeclarationKind(SyntaxKind.EventFieldDeclaration);
    _scenarioContext.AddImplementationGeneratedMemberDeclarationKind(SyntaxKind.EventDeclaration);
  }
}
