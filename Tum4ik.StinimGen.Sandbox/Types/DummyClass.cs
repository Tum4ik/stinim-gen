using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Tum4ik.StinimGen.Sandbox.Types;
[Obsolete("dmke")]
public sealed class DummyClass
{
  public DummyClass()
  {
  }

  /// <summary>
  /// field
  /// </summary>
  public static readonly List<int> Field;

  /// <inheritdoc cref="DummyClass.Field"/>
  public static int ff { get; set; }


  /// <summary>
  /// this is generic method for test
  /// </summary>
  /// <typeparam name="T">T type para</typeparam>
  /// <typeparam name="K">K type parm</typeparam>
  /// <param name="t">t par</param>
  /// <param name="k">k par</param>
  /// <returns>ret tupp</returns>
  public static int TestGeneric<T, K>([NotNull] ref List<T> t, out K k, params IntPtr[] pp)
    where T : class
    where K : struct
  {
    k = default(K);
    return 0;
  }


  /// <inheritdoc cref="DummyClass.TestGeneric{T, K}(ref List{T}, out K, nint[])"/>
  public int TestGeneric1<T, K>(List<T> t, K k)
    where T : class
    where K : struct
  {
    return 0;
  }


  /// <inheritdoc cref="TryParse(string?, out int)"/>
  public static bool TryParse([NotNullWhen(true)][In] string? s, [MaybeNullWhen(false), Out][AllowNull] out int result)
  {
    result = 0;
    return false;
  }
}
