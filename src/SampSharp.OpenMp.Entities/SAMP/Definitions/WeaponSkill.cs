using System.Diagnostics.CodeAnalysis;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Contains all weapon skills types.
/// </summary>
[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "CommentTypo")]
public enum WeaponSkill
{
    /// <summary>
    /// Pistol skills.
    /// </summary>
    Pistol = 0,

    /// <summary>
    /// Silenced pistol skills.
    /// </summary>
    PistolSilenced = 1,

    /// <summary>
    /// Desert eagle skills.
    /// </summary>
    DesertEagle = 2,

    /// <summary>
    /// Shotgun skills.
    /// </summary>
    Shotgun = 3,

    /// <summary>
    /// Sawn-off shotgun skills.
    /// </summary>
    SawnoffShotgun = 4,

    /// <summary>
    /// Combat shotgun skills.
    /// </summary>
    Spas12Shotgun = 5,

    /// <summary>
    /// Micro uzi skills.
    /// </summary>
    MicroUzi = 6,

    /// <summary>
    /// MP5 skills.
    /// </summary>
    MP5 = 7,

    /// <summary>
    /// AK47 skills.
    /// </summary>
    AK47 = 8,

    /// <summary>
    /// M4 skills.
    /// </summary>
    M4 = 9,

    /// <summary>
    /// Sniper rifle skills.
    /// </summary>
    SniperRifle = 10
}