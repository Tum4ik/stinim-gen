using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace YT.IIGen.UnitTests;
internal static class Helper
{
  public static Compilation CreateCompilation(string assemblyName,
                                              string sourceCode,
                                              params Type[] references)
  {
    return CSharpCompilation.Create(
      assemblyName,
      new[] { CSharpSyntaxTree.ParseText(sourceCode) },
      references.Select(t => MetadataReference.CreateFromFile(t.GetTypeInfo().Assembly.Location)).Distinct(),
      new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
    );
  }
}
