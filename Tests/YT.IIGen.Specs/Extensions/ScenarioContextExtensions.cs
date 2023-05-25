using Microsoft.CodeAnalysis.CSharp;

namespace YT.IIGen.Specs.Extensions;
internal static class ScenarioContextExtensions
{
  private const string UsingsKey = nameof(UsingsKey);

  public static void AddUsings(this ScenarioContext context, string usings)
  {
    context[UsingsKey] = usings;
  }

  public static string GetUsings(this ScenarioContext context)
  {
    if (context.TryGetValue(UsingsKey, out string usings))
    {
      return usings;
    }

    return string.Empty;
  }


  private const string SourceMemberDeclarationKey = nameof(SourceMemberDeclarationKey);

  public static void AddSourceMemberDeclaration(this ScenarioContext context, string sourceMemberDeclaration)
  {
    context[SourceMemberDeclarationKey] = sourceMemberDeclaration;
  }

  public static string GetSourceMemberDeclaration(this ScenarioContext context)
  {
    return (string) context[SourceMemberDeclarationKey];
  }


  private const string ExpectedGeneratedMemberForInterfaceKey = nameof(ExpectedGeneratedMemberForInterfaceKey);

  public static void AddExpectedGeneratedMemberForInterface(this ScenarioContext context,
                                                            string expectedGeneratedMemberForInterface)
  {
    context[ExpectedGeneratedMemberForInterfaceKey] = expectedGeneratedMemberForInterface;
  }

  public static string GetExpectedGeneratedMemberForInterface(this ScenarioContext context)
  {
    return (string) context[ExpectedGeneratedMemberForInterfaceKey];
  }


  private const string GeneratedMemberDeclarationKindKey = nameof(GeneratedMemberDeclarationKindKey);

  public static void AddGeneratedMemberDeclarationKind(this ScenarioContext context, SyntaxKind kind)
  {
    context[GeneratedMemberDeclarationKindKey] = kind;
  }

  public static SyntaxKind GetGeneratedMemberDeclarationKind(this ScenarioContext context)
  {
    return (SyntaxKind) context[GeneratedMemberDeclarationKindKey];
  }
}
