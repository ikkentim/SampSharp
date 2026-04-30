namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the spectate mode a player can use while spectating.
/// </summary>
public enum PlayerSpectateMode
{
    /// <summary>
    /// The normal spectate mode, where the camera follows the target freely.
    /// </summary>
    Normal = 1,

    /// <summary>
    /// The fixed spectate mode, where the camera remains in a fixed position.
    /// </summary>
    Fixed,

    /// <summary>
    /// The side spectate mode, where the camera follows the target from the side.
    /// </summary>
    Side
}