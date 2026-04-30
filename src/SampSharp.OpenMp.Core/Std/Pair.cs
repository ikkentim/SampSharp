using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Std;

/// <summary>
/// Represents a pair of values which is represented in memory like an <c>std::Pair</c> from the C++ standard library.
/// </summary>
/// <typeparam name="T1">The type of the first value in the pair.</typeparam>
/// <typeparam name="T2">The type of the second value in the pair.</typeparam>
[StructLayout(LayoutKind.Sequential)]
public readonly struct Pair<T1, T2> 
    where T1 : unmanaged 
    where T2 : unmanaged
{
    /// <summary>
    /// The first value in the pair.
    /// </summary>
    public readonly T1 First;

    /// <summary>
    /// The second value in the pair.
    /// </summary>
    public readonly T2 Second;

    /// <summary>
    /// Deconstructs the pair into its two values.
    /// </summary>
    /// <param name="first">The first value from the pair.</param>
    /// <param name="second">The second value from the pair.</param>
    public void Deconstruct(out T1 first, out T2 second)
    {
        first = First;
        second = Second;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"({First}, {Second})";
    }

    /// <summary>
    /// Converts a pair to a tuple.
    /// </summary>
    /// <param name="pair">The pair to convert.</param>
    public static implicit operator (T1, T2)(Pair<T1, T2> pair)
    {
        return (pair.First, pair.Second);
    }

    /// <summary>
    /// Converts a tuple to a pair.
    /// </summary>
    /// <param name="tuple">The tuple to convert.</param>
    public static implicit operator Pair<T1, T2>((T1 first,T2 second) tuple)
    {
        return (tuple.first, tuple.second);
    }
}
