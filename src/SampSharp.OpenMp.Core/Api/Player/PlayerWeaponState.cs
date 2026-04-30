namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the state of a player's weapon.
/// </summary>
public enum PlayerWeaponState : sbyte
{
    /// <summary>
    /// The weapon state is unknown.
    /// </summary>
    Unknown = -1,

    /// <summary>
    /// The weapon has no bullets remaining.
    /// </summary>
    NoBullets,

    /// <summary>
    /// The weapon has only one bullet remaining.
    /// </summary>
    LastBullet,

    /// <summary>
    /// The weapon has more than one bullet remaining.
    /// </summary>
    MoreBullets,

    /// <summary>
    /// The weapon is currently reloading.
    /// </summary>
    Reloading
}