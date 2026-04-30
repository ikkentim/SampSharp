using System.Numerics;

namespace SampSharp.Entities;

/// <summary>
/// Provides various default vector constants for GTA: San Andreas.
/// </summary>
public static class GtaVector
{
    /// <summary>Returns a <see cref="Vector3" /> with components 0, 0, 1.</summary>
    public static Vector3 Up { get; } = new(0, 0, 1);

    /// <summary>Returns a <see cref="Vector3" /> with components 0, 0, -1.</summary>
    public static Vector3 Down { get; } = new(0, 0, -1);

    /// <summary>Returns a <see cref="Vector3" /> with components -1, 0, 0.</summary>
    public static Vector3 Left { get; } = new(-1, 0, 0);

    /// <summary>Returns a <see cref="Vector3" /> with components 1, 0, 0.</summary>
    public static Vector3 Right { get; } = new(1, 0, 0);

    /// <summary>Returns a <see cref="Vector3" /> with components 0, 1, 0.</summary>
    public static Vector3 Forward { get; } = new(0, 1, 0);

    /// <summary>Returns a <see cref="Vector3" /> with components 0, -1, 0.</summary>
    public static Vector3 Backward { get; } = new(0, -1, 0);
}