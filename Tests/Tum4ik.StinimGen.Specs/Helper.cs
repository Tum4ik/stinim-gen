using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Tum4ik.StinimGen.Specs;
internal static class Helper
{
  public static Compilation CreateCompilation(string assemblyName,
                                              string[] sourceCodeTrees,
                                              Type[] references)
  {
    return CSharpCompilation.Create(
      assemblyName,
      sourceCodeTrees.Select(sct => CSharpSyntaxTree.ParseText(sct)),
      references.Select(t => MetadataReference.CreateFromFile(t.GetTypeInfo().Assembly.Location)).Distinct(),
      new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
    );
  }
}
