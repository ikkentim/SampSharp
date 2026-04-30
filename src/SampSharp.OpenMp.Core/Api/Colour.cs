using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents a colour.
/// </summary>
/// <param name="r">The red component of the colour.</param>
/// <param name="g">The green component of the colour.</param>
/// <param name="b">The blue component of the colour.</param>
/// <param name="a">The alpha component of the colour.</param>
[StructLayout(LayoutKind.Sequential)]
public readonly struct Colour(byte r, byte g, byte b, byte a)
{
    /// <summary>
    /// The red component of the colour.
    /// </summary>
    public readonly byte R = r;

    /// <summary>
    /// The green component of the colour.
    /// </summary>
    public readonly byte G = g;

    /// <summary>
    /// The blue component of the colour.
    /// </summary>
    public readonly byte B = b;

    /// <summary>
    /// The alpha component of the colour.
    /// </summary>
    public readonly byte A = a;
}