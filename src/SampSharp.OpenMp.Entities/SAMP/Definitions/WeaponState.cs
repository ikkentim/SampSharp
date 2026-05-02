namespace SampSharp.Entities.SAMP;

/// <summary>
/// Contains all weapon states.
/// </summary>
public enum WeaponState
{
    /// <summary>
    /// Unknown state.
    /// </summary>
    Unknown = -1,

    /// <summary>
    /// Weapon is out of bullets.
    /// </summary>
    NoBullets = 0,

    /// <summary>
    /// Last bullet in gun.
    /// </summary>
    LastBullet = 1,

    /// <summary>
    /// More bullets in gun.
    /// </summary>
    MoreBullets = 2,

    /// <summary>
    /// Weapon is reloading.
    /// </summary>
    Reloading = 3
}