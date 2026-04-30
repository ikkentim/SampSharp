using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents data related to a player's spectating state.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct PlayerSpectateData
{
    /// <summary>
    /// Represents the type of spectating a player is performing.
    /// </summary>
    public enum ESpectateType
    {
        /// <summary>
        /// The player is not spectating.
        /// </summary>
        None,

        /// <summary>
        /// The player is spectating a vehicle.
        /// </summary>
        Vehicle,

        /// <summary>
        /// The player is spectating another player.
        /// </summary>
        Player
    }

    /// <summary>
    /// Indicates whether the player is currently spectating.
    /// </summary>
    public readonly bool spectating;

    /// <summary>
    /// The ID of the entity (player or vehicle) being spectated.
    /// </summary>
    public readonly int spectateID;

    /// <summary>
    /// The type of entity being spectated.
    /// </summary>
    public readonly ESpectateType type;
}