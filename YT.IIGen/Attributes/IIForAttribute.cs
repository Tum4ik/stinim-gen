namespace YT.IIGen.Attributes;

/// <summary>
/// An attribute that can be used to automatically generate interface members and an implementation wrapper for a
/// declared interface with a given class type.
/// </summary>
[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
public class IIForAttribute : Attribute
{
  public IIForAttribute(Type classType, string implementationClassName)
  {
    ClassType = classType;
    ImplementationClassName = implementationClassName;
  }

  /// <summary>
  /// The class type to generate interface members and an implementation wrapper for.
  /// </summary>
  public Type ClassType { get; }

  /// <summary>
  /// The implementation wrapper class name to generate.
  /// </summary>
  public string ImplementationClassName { get; }
}
