using System.Numerics;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the aiming data of a player, including camera position, direction, and weapon state.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct PlayerAimData
{
    /// <summary>
    /// The forward vector of the player's camera, indicating the direction the camera is facing.
    /// </summary>
    public readonly Vector3 camFrontVector;

    /// <summary>
    /// The position of the player's camera in the game world.
    /// </summary>
    public readonly Vector3 camPos;

    /// <summary>
    /// The Z-axis aiming position of the player.
    /// </summary>
    public readonly float aimZ;

    /// <summary>
    /// The zoom level of the player's camera.
    /// </summary>
    public readonly float camZoom;

    /// <summary>
    /// The aspect ratio of the player's camera.
    /// </summary>
    public readonly float aspectRatio;

    /// <summary>
    /// The current weapon state of the player.
    /// </summary>
    public readonly PlayerWeaponState weaponState;

    /// <summary>
    /// The mode of the player's camera.
    /// </summary>
    public readonly byte camMode;
}