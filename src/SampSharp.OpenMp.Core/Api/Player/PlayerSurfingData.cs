using System.Numerics;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents data related to a player's surfing state, such as the type of surface and its offset.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct PlayerSurfingData
{
    /// <summary>
    /// Represents the type of surface the player is surfing on.
    /// </summary>
    public enum Type
    {
        /// <summary>
        /// The player is not surfing on any surface.
        /// </summary>
        None,

        /// <summary>
        /// The player is surfing on a vehicle.
        /// </summary>
        Vehicle,

        /// <summary>
        /// The player is surfing on an object.
        /// </summary>
        Object,

        /// <summary>
        /// The player is surfing on a player-owned object.
        /// </summary>
        PlayerObject
    }

    /// <summary>
    /// The type of surface the player is surfing on.
    /// </summary>
    public readonly Type type;

    /// <summary>
    /// The ID of the surface the player is surfing on.
    /// </summary>
    public readonly int ID;

    /// <summary>
    /// The offset of the player relative to the surface they are surfing on.
    /// </summary>
    public readonly Vector3 offset;
}