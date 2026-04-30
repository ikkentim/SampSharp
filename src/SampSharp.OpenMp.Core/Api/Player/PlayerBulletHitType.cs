namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the types of entities that can be hit by a player's bullet.
/// </summary>
public enum PlayerBulletHitType : byte
{
    /// <summary>
    /// No entity was hit.
    /// </summary>
    None,

    /// <summary>
    /// A player was hit.
    /// </summary>
    Player = 1,

    /// <summary>
    /// A vehicle was hit.
    /// </summary>
    Vehicle = 2,

    /// <summary>
    /// An object was hit.
    /// </summary>
    Object = 3,

    /// <summary>
    /// A player object was hit.
    /// </summary>
    PlayerObject = 4
}
