using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents a unique identifier.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct UID
{
    private readonly ulong _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="UID" /> struct.
    /// </summary>
    /// <param name="value">The underlying value.</param>
    public UID(ulong value)
    {
        _value = value;
    }
    
    /// <inheritdoc />
    public override string ToString()
    {
        return _value.ToString("x16");
    }
}