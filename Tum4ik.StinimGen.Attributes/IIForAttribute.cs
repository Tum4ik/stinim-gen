namespace Tum4ik.StinimGen.Attributes;

/// <summary>
/// An attribute that can be used to automatically generate interface members and an implementation wrapper for a
/// declared interface with a given type.
/// </summary>
[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
public class IIForAttribute : Attribute
{
  /// <param name="sourceType">
  /// The type to generate interface members and an implementation wrapper for.
  /// </param>
  public IIForAttribute(Type sourceType)
  {
    SourceType = sourceType;
  }


  /// <summary>
  /// The type to generate interface members and an implementation wrapper for.
  /// </summary>
  public Type SourceType { get; }

  /// <summary>
  /// The implementation wrapper class name to generate.
  /// </summary>
  public required string WrapperClassName { get; init; }

  /// <summary>
  /// Controls the generated implementation wrapper accessibility:
  /// <see langword="true"/> emits <see langword="public"/> access modifier,
  /// <see langword="false"/> - <see langword="internal"/>
  /// </summary>
  public bool IsPublic { get; init; }

  /// <summary>
  /// Controls the ability to inherit generated implementation wrapper:
  /// <see langword="true"/> emits <see langword="sealed"/> modifier, <see langword="false"/> - nothing.
  /// </summary>
  public bool IsSealed { get; init; } = true;
}
