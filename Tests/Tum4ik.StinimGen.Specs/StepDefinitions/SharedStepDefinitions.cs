using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Tum4ik.StinimGen.Attributes;
using Tum4ik.StinimGen.Specs.Extensions;

namespace Tum4ik.StinimGen.Specs.StepDefinitions;

[Binding]
public class SharedStepDefinitions
{
  private readonly ScenarioContext _scenarioContext;

  public SharedStepDefinitions(ScenarioContext scenarioContext)
  {
    _scenarioContext = scenarioContext;
  }


  [Given("source declaration")]
  public void GivenSourceDeclaration(string declaration)
  {
    _scenarioContext.AddDeclaration(declaration);
  }


  [Given("attribute usage")]
  public void GivenAttributeUsage(string attributeUsage)
  {
    _scenarioContext.AddAttributeUsage(attributeUsage);
  }


  [Given("source member declaration")]
  public void GivenMemberDeclaration(string memberDeclaration)
  {
    _scenarioContext.AddMemberDeclaration(memberDeclaration);
  }


  [When("run generator")]
  public void WhenRunGenerator()
  {
    var memberDeclaration = _scenarioContext.GetMemberDeclaration();
    var declaration = _scenarioContext.GetDeclaration();
    if (memberDeclaration is not null && declaration is not null)
    {
      declaration = declaration.Replace("<member>", memberDeclaration);
    }
    var attributeUsage = _scenarioContext.GetAttributeUsage();
    var generatorRunResult = RunGenerator(attributeUsage, declaration);
    _scenarioContext.AddGeneratorRunResult(generatorRunResult);
  }


  [Then("there must not be generation exception")]
  public void ThenThereMustNotBeGenerationException()
  {
    var generatorRunResult = _scenarioContext.GetGeneratorRunResult();
    generatorRunResult.Exception.Should().BeNull();
  }


  [Then("generated interface must be")]
  public void GeneratedInterfaceMustBe(string expectedInterface)
  {
    var generatorRunResult = _scenarioContext.GetGeneratorRunResult();
    var generatedInterfaceSourceResult = generatorRunResult.GeneratedSources[0];
    var generatedInterface = generatedInterfaceSourceResult.SyntaxTree
      .GetRoot()
      .DescendantNodes()
      .First(n => n.IsKind(SyntaxKind.InterfaceDeclaration))
      .As<InterfaceDeclarationSyntax>()
      .WithAttributeLists(new SyntaxList<AttributeListSyntax>())
      .NormalizeWhitespace()
      .GetText()
      .ToString();
    generatedInterface.Should().Be(expectedInterface);
  }


  [Then("generated implementation must be")]
  public void GeneratedImplementationMustBe(string expectedImplementation)
  {
    var generatorRunResult = _scenarioContext.GetGeneratorRunResult();
    var generatedImplementationSourceResult = generatorRunResult.GeneratedSources[1];
    var generatedImplementation = generatedImplementationSourceResult.SyntaxTree
      .GetRoot()
      .DescendantNodes()
      .First(n => n.IsKind(SyntaxKind.ClassDeclaration))
      .As<ClassDeclarationSyntax>()
      .WithAttributeLists(new SyntaxList<AttributeListSyntax>())
      .NormalizeWhitespace()
      .GetText()
      .ToString();
    generatedImplementation.Should().Be(expectedImplementation);
  }


  [Then("generated interface member must be")]
  public void ThenGeneratedInterfaceMustBe(string expectedInterfaceMember)
  {
    var generatorRunResult = _scenarioContext.GetGeneratorRunResult();
    var generatedInterfaceSourceResult = generatorRunResult.GeneratedSources[0];
    var generatedMember = generatedInterfaceSourceResult.SyntaxTree
      .GetRoot()
      .DescendantNodes()
      .First(n => n.IsKind(SyntaxKind.InterfaceDeclaration))
      .As<InterfaceDeclarationSyntax>()
      .Members
      .First()
      .NormalizeWhitespace()
      .GetText()
      .ToString();
    generatedMember.Should().Be(expectedInterfaceMember);
  }


  [Then("generated implementation member must be")]
  public void ThenGeneratedImplementationMustBe(string expectedImplementationMember)
  {
    var generatorRunResult = _scenarioContext.GetGeneratorRunResult();
    var generatedImplementationSourceResult = generatorRunResult.GeneratedSources[1];
    var generatedMember = generatedImplementationSourceResult.SyntaxTree
      .GetRoot()
      .DescendantNodes()
      .First(n => n.IsKind(SyntaxKind.ClassDeclaration))
      .As<ClassDeclarationSyntax>()
      .Members
      .First()
      .NormalizeWhitespace()
      .GetText()
      .ToString();
    generatedMember.Should().Be(expectedImplementationMember);
  }


  private static GeneratorRunResult RunGenerator(string attributeUsage, string? declaration)
  {
    string[] sourceCodeTrees = declaration is null ? [attributeUsage] : [declaration, attributeUsage];
    var inputCompilation = Helper.CreateCompilation("Virtual.Assembly",
      sourceCodeTrees,
      [typeof(IIForAttribute), typeof(object), typeof(Stream)]
    );
    return CSharpGeneratorDriver.Create(new IIGenerator())
      .RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _)
      .GetRunResult()
      .Results
      .First();
  }
}
