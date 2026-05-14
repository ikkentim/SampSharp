using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Std;

/// <summary>
/// Represents a size which is represented in memory like an <c>std::size_t</c> from the C++ standard library.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Size" /> struct.
/// </remarks>
/// <param name="value">The size value.</param>
[StructLayout(LayoutKind.Sequential)]
public readonly struct Size(nint value)
{
    /// <summary>
    /// Gets the length in bytes of the Size structure.
    /// </summary>
    public const int Length = 8; // 64-bits

    static Size()
    {
        if (Length != Marshal.SizeOf<Size>())
        {
            throw new InvalidOperationException("Size structure has an unexpected size. Are you running a 32-bit build of SampSharp?");
        }
    }

    /// <summary>
    /// Gets the size value.
    /// </summary>
    public nint Value { get; } = value;

    /// <summary>
    /// Converts the size to an <see cref="int" />.
    /// </summary>
    /// <returns>The converted value.</returns>
    public int ToInt32()
    {
        return Value.ToInt32();
    }

    /// <summary>
    /// Converts the size to an <see cref="int" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static explicit operator int(Size value)
    {
        return value.ToInt32();
    }

    /// <summary>
    /// Converts an <see cref="int" /> to a size.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator Size(int value)
    {
        return new Size(value);
    }
}