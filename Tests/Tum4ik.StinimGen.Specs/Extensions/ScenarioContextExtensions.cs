using Microsoft.CodeAnalysis.CSharp;

namespace Tum4ik.StinimGen.Specs.Extensions;
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


  private const string InterfaceGeneratedMemberDeclarationKindKey = nameof(InterfaceGeneratedMemberDeclarationKindKey);

  public static void AddInterfaceGeneratedMemberDeclarationKind(this ScenarioContext context, SyntaxKind kind)
  {
    context[InterfaceGeneratedMemberDeclarationKindKey] = kind;
  }

  public static SyntaxKind GetInterfaceGeneratedMemberDeclarationKind(this ScenarioContext context)
  {
    return (SyntaxKind) context[InterfaceGeneratedMemberDeclarationKindKey];
  }


  private const string ImplementationGeneratedMemberDeclarationKindKey = nameof(ImplementationGeneratedMemberDeclarationKindKey);

  public static void AddImplementationGeneratedMemberDeclarationKind(this ScenarioContext context, SyntaxKind kind)
  {
    context[ImplementationGeneratedMemberDeclarationKindKey] = kind;
  }

  public static SyntaxKind GetImplementationGeneratedMemberDeclarationKind(this ScenarioContext context)
  {
    return (SyntaxKind) context[ImplementationGeneratedMemberDeclarationKindKey];
  }


  private const string AdditionalNamespaceDeclarationsKey = nameof(AdditionalNamespaceDeclarationsKey);

  public static void AddAdditionalNamespaceDeclarations(this ScenarioContext context, string declarations)
  {
    context[AdditionalNamespaceDeclarationsKey] = declarations;
  }

  public static string GetAdditionalNamespaceDeclarations(this ScenarioContext context)
  {
    if (context.TryGetValue(AdditionalNamespaceDeclarationsKey, out string declarations))
    {
      return declarations;
    }
    return string.Empty;
  }
}
