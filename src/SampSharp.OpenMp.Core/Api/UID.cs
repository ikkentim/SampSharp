using System.Globalization;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents a unique identifier.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UID" /> struct.
/// </remarks>
/// <param name="value">The underlying value.</param>
[StructLayout(LayoutKind.Sequential)]
public readonly struct UID(ulong value)
{
    private readonly ulong _value = value;

    /// <inheritdoc />
    public override string ToString()
    {
        return _value.ToString("x16", CultureInfo.InvariantCulture);
    }
}