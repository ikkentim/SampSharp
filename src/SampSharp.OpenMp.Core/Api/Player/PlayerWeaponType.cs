namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Categorises a weapon by its damage and behaviour profile.
/// Mirrors the open.mp SDK <c>PlayerWeaponType</c> enum.
/// </summary>
public enum PlayerWeaponType
{
    /// <summary>
    /// Not a real weapon (e.g. parachute, camera, weapon ID gaps).
    /// </summary>
    None,

    /// <summary>
    /// Melee weapons: fists, knives, bats, chainsaw, etc. Weapon IDs 0-15.
    /// </summary>
    Melee,

    /// <summary>
    /// Throwable explosives and incendiaries: grenade, tear gas, molotov. Weapon IDs 16-18.
    /// </summary>
    Throwable,

    /// <summary>
    /// Bullet-firing firearms: pistols, SMGs, rifles, sniper, miniguns.
    /// </summary>
    Bullet,

    /// <summary>
    /// Rocket-launching weapons: RPG, heat-seeker.
    /// </summary>
    Rocket,

    /// <summary>
    /// Continuous-spray weapons: flamethrower, spray can, fire extinguisher.
    /// </summary>
    Sprayable,

    /// <summary>
    /// Special-purpose weapons that don't fit other categories: detonator, parachute, night vision, etc.
    /// </summary>
    Special
}
