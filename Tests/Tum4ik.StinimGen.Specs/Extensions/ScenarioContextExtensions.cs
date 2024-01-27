using Microsoft.CodeAnalysis;

namespace Tum4ik.StinimGen.Specs.Extensions;
internal static class ScenarioContextExtensions
{
  private const string DeclarationKey = nameof(DeclarationKey);
  public static void AddDeclaration(this ScenarioContext context, string declaration)
  {
    context[DeclarationKey] = declaration;
  }
  public static string? GetDeclaration(this ScenarioContext context)
  {
    if (context.TryGetValue<string>(DeclarationKey, out var declaration))
    {
      return declaration;
    }
    return null;
  }


  private const string AttributeUsageKey = nameof(AttributeUsageKey);
  public static void AddAttributeUsage(this ScenarioContext context, string attributeUsage)
  {
    context[AttributeUsageKey] = attributeUsage;
  }
  public static string GetAttributeUsage(this ScenarioContext context)
  {
    return (string) context[AttributeUsageKey];
  }


  private const string MemberDeclarationKey = nameof(MemberDeclarationKey);
  public static void AddMemberDeclaration(this ScenarioContext context, string memberDeclaration)
  {
    context[MemberDeclarationKey] = memberDeclaration;
  }
  public static string? GetMemberDeclaration(this ScenarioContext context)
  {
    if (context.TryGetValue<string>(MemberDeclarationKey, out var memberDeclaration))
    {
      return memberDeclaration;
    }
    return null;
  }


  private const string GeneratorRunResultKey = nameof(GeneratorRunResultKey);
  public static void AddGeneratorRunResult(this ScenarioContext context, GeneratorRunResult generatorRunResult)
  {
    context[GeneratorRunResultKey] = generatorRunResult;
  }
  public static GeneratorRunResult GetGeneratorRunResult(this ScenarioContext context)
  {
    return (GeneratorRunResult) context[GeneratorRunResultKey];
  }
}
