using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Tum4ik.StinimGen.Attributes;
using Tum4ik.StinimGen.Specs.Extensions;

namespace Tum4ik.StinimGen.Specs.StepDefinitions;

[Binding]
public class CommonStepDefinitions
{
  private readonly ScenarioContext _scenarioContext;

  public CommonStepDefinitions(ScenarioContext scenarioContext)
  {
    _scenarioContext = scenarioContext;
  }


  private const string Namespace = "Something.Somewhere";
  private const string TypeName = "SomeType";


  [Given(@"usings")]
  public void GivenUsings(string usings)
  {
    _scenarioContext.AddUsings(usings);
  }


  [Given(@"source member declaration")]
  public void GivenMemberDeclaration(string sourceMemberDeclaration)
  {
    _scenarioContext.AddSourceMemberDeclaration(sourceMemberDeclaration);
  }


  [Given(@"additional namespace declarations")]
  public void GivenAdditionalNamespaceDeclarations(string additionalNamespaceDeclarations)
  {
    _scenarioContext.AddAdditionalNamespaceDeclarations(additionalNamespaceDeclarations);
  }


  [Then(@"generated for interface")]
  public void ThenGeneratedForInterface(string expectedGeneration)
  {
    expectedGeneration = expectedGeneration.Replace("@Namespace", Namespace);
    _scenarioContext.AddExpectedGeneratedMemberForInterface(expectedGeneration);
  }


  [Then(@"generated for struct implementation")]
  public void ThenGeneratedForStructImplementation(string expectedGeneration)
  {
    var usings = _scenarioContext.GetUsings();
    var sourceMemberDeclaration = _scenarioContext.GetSourceMemberDeclaration();
    var additionalDeclarations = _scenarioContext.GetAdditionalNamespaceDeclarations();

    var structDeclaration = $@"
{usings}
namespace {Namespace};
public struct {TypeName}
{{
  public {TypeName}() {{ }}
  {sourceMemberDeclaration}
}}

{additionalDeclarations}
";

    RunGeneratorAndValidateResults(structDeclaration, expectedGeneration);
  }


  [Then(@"generated for class implementation")]
  public void ThenGeneratedForClassImplementation(string expectedGeneration)
  {
    var classDeclaration = GetClassDeclaration();
    RunGeneratorAndValidateResults(classDeclaration, expectedGeneration);
  }


  [Then(@"inherited for class implementation")]
  public void ThenInheritedForClassImplementation()
  {
    var classDeclaration = GetClassDeclaration();
    var expectedGeneratedMemberForInterface = _scenarioContext.GetExpectedGeneratedMemberForInterface();
    var (memberGeneratedForInterface, memberGeneratedForImplementation) = RunGenerator(classDeclaration);

    memberGeneratedForInterface.Should().Be(expectedGeneratedMemberForInterface);
    memberGeneratedForImplementation.Should().BeNull();
  }


  private string GetClassDeclaration()
  {
    var usings = _scenarioContext.GetUsings();
    var sourceMemberDeclaration = _scenarioContext.GetSourceMemberDeclaration();
    var additionalDeclarations = _scenarioContext.GetAdditionalNamespaceDeclarations();

    return $@"
{usings}
namespace {Namespace};
public class {TypeName}
{{
  {sourceMemberDeclaration}
}}

{additionalDeclarations}
";
  }


  [Then(@"generated for sealed class implementation")]
  public void ThenGeneratedForSealedClassImplementation(string expectedGeneration)
  {
    var usings = _scenarioContext.GetUsings();
    var sourceMemberDeclaration = _scenarioContext.GetSourceMemberDeclaration();
    var additionalDeclarations = _scenarioContext.GetAdditionalNamespaceDeclarations();

    var sealedClassDeclaration = $@"
{usings}
namespace {Namespace};
public sealed class {TypeName}
{{
  {sourceMemberDeclaration}
}}

{additionalDeclarations}
";

    RunGeneratorAndValidateResults(sealedClassDeclaration, expectedGeneration);
  }


  [Then(@"generated for static class implementation")]
  public void ThenGeneratedForStaticClassImplementation(string expectedGeneration)
  {
    var usings = _scenarioContext.GetUsings();
    var sourceMemberDeclaration = _scenarioContext.GetSourceMemberDeclaration();
    var additionalDeclarations = _scenarioContext.GetAdditionalNamespaceDeclarations();

    var staticClassDeclaration = $@"
{usings}
namespace {Namespace};
public static class {TypeName}
{{
  {sourceMemberDeclaration}
}}

{additionalDeclarations}
";

    RunGeneratorAndValidateResults(staticClassDeclaration, expectedGeneration);
  }


  private void RunGeneratorAndValidateResults(string typeDeclaration, string expectedGeneration)
  {
    expectedGeneration = expectedGeneration.Replace("@Namespace", Namespace).Replace("@TypeName", TypeName);
    var expectedGeneratedMemberForInterface = _scenarioContext.GetExpectedGeneratedMemberForInterface();

    var (memberGeneratedForInterface, memberGeneratedForImplementation) = RunGenerator(typeDeclaration);

    memberGeneratedForInterface.Should().Be(expectedGeneratedMemberForInterface);
    memberGeneratedForImplementation.Should().Be(expectedGeneration);
  }


  private (string? ForInterface, string? ForImplementation) RunGenerator(string typeDeclaration)
  {
    var attributeUsageCode = GetAttributeUsageCode();
    var inputCompilation = Helper.CreateCompilation("My.Assembly",
      new[] { typeDeclaration, attributeUsageCode },
      new[] { typeof(IIForAttribute), typeof(object), typeof(Stream) }
    );
    var generatorRunResult = CSharpGeneratorDriver.Create(new IIGenerator())
      .RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _)
      .GetRunResult()
      .Results
      .First();

    generatorRunResult.Exception.Should().BeNull();

    var interfaceMemberDeclarationKind = _scenarioContext.GetInterfaceGeneratedMemberDeclarationKind();
    var implementationMemberDeclarationKind = _scenarioContext.GetImplementationGeneratedMemberDeclarationKind();

    var (interfaceResult, implementationResult) =
      (generatorRunResult.GeneratedSources[0].SyntaxTree, generatorRunResult.GeneratedSources[1].SyntaxTree);
    var memberGeneratedForInterface = GetPropertySourceText(interfaceResult, interfaceMemberDeclarationKind);
    var memberGeneratedForImplementation = GetPropertySourceText(implementationResult, implementationMemberDeclarationKind);

    return (memberGeneratedForInterface?.ToString(), memberGeneratedForImplementation?.ToString());
  }


  private static string GetAttributeUsageCode()
  {
    return $@"
using Tum4ik.StinimGen.Attributes;
using {Namespace};
namespace My.Assembly.Code;
[IIFor(typeof({TypeName}), ""{TypeName}Wrapper"")]
internal partial interface I{TypeName} {{ }}
";
  }


  private static SourceText? GetPropertySourceText(SyntaxTree syntaxTree, SyntaxKind memberDeclarationKind)
  {
    return syntaxTree.GetRoot()
      .DescendantNodes()
      .FirstOrDefault(n => n.IsKind(memberDeclarationKind))?
      .NormalizeWhitespace()
      .GetText();
  }
}
