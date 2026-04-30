using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the position boundaries of a gang zone.
/// </summary>
public readonly struct GangZonePos(Vector2 min, Vector2 max)
{
    /// <summary>
    /// The minimum corner coordinates of the gang zone.
    /// </summary>
    public readonly Vector2 Min = min;

    /// <summary>
    /// The maximum corner coordinates of the gang zone.
    /// </summary>
    public readonly Vector2 Max = max;
}