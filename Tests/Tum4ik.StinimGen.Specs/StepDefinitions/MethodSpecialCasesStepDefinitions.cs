using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Tum4ik.StinimGen.Specs.Extensions;
using Microsoft.CodeAnalysis;

namespace Tum4ik.StinimGen.Specs.StepDefinitions;

[Binding]
public class MethodSpecialCasesStepDefinitions
{
  private readonly ScenarioContext _scenarioContext;

  public MethodSpecialCasesStepDefinitions(ScenarioContext scenarioContext)
  {
    _scenarioContext = scenarioContext;
  }


  [Then(@"there must not be explicit interface specifier in the generated interface and implementation methods")]
  public void ThenThereMustNotBeExplicitInterfaceSpecifierInTheGeneratedInterfaceAndImplementation()
  {
    var generatorRunResult = _scenarioContext.GetGeneratorRunResult();
    var generatedInterfaceSourceResult = generatorRunResult.GeneratedSources[0];
    var explicitInterfaceSpecifiers = generatedInterfaceSourceResult.SyntaxTree
      .GetRoot()
      .DescendantNodes()
      .First(n => n.IsKind(SyntaxKind.InterfaceDeclaration))
      .As<InterfaceDeclarationSyntax>()
      .Members
      .Where(m => m is MethodDeclarationSyntax)
      .Cast<MethodDeclarationSyntax>()
      .Select(mds => mds.ExplicitInterfaceSpecifier)
      .Where(eis => eis is not null);
    explicitInterfaceSpecifiers.Should().BeEmpty();
  }
}
