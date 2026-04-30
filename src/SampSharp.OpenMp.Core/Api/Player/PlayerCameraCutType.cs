namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the type of camera transition for a player.
/// </summary>
public enum PlayerCameraCutType
{
    /// <summary>
    /// Instantly cuts the camera to the new position.
    /// </summary>
    Cut,

    /// <summary>
    /// Smoothly moves the camera to the new position.
    /// </summary>
    Move
}