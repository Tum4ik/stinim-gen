namespace System.Runtime.CompilerServices
{
  public class RequiredMemberAttribute : Attribute { }
  public class CompilerFeatureRequiredAttribute : Attribute
  {
    public CompilerFeatureRequiredAttribute(string name) { }
  }
}


namespace System.Diagnostics.CodeAnalysis
{
  [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
  public sealed class SetsRequiredMembersAttribute : Attribute
  {
  }
}
