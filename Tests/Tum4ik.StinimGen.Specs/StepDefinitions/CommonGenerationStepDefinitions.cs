using System.Reflection;
using Tum4ik.StinimGen.Specs.Extensions;

namespace Tum4ik.StinimGen.Specs.StepDefinitions;

[Binding]
public class CommonGenerationStepDefinitions
{
  private readonly ScenarioContext _scenarioContext;

  public CommonGenerationStepDefinitions(ScenarioContext scenarioContext)
  {
    _scenarioContext = scenarioContext;
  }


  [Then("generated interface file must be")]
  public void ThenGeneratedInterfaceFileMustBe(string expectedInterface)
  {
    var generatorAssembly = Assembly.GetAssembly(typeof(IIGenerator));
    var version = generatorAssembly?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
    expectedInterface = expectedInterface.Replace("<version>", version);
    var generatorRunResult = _scenarioContext.GetGeneratorRunResult();
    var generatedInterface = generatorRunResult.GeneratedSources[0]
      .SourceText
      .ToString();

    generatedInterface.Should().Be(expectedInterface);
  }


  [Then("generated implementation file must be")]
  public void ThenGeneratedImplementationFileMustBe(string expectedImplementation)
  {
    var generatorAssembly = Assembly.GetAssembly(typeof(IIGenerator));
    var version = generatorAssembly?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
    expectedImplementation = expectedImplementation.Replace("<version>", version);
    var generatorRunResult = _scenarioContext.GetGeneratorRunResult();
    var generatedImplementation = generatorRunResult.GeneratedSources[1]
      .SourceText
      .ToString();

    generatedImplementation.Should().Be(expectedImplementation);
  }
}
