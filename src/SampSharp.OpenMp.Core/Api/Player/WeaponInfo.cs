namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Static metadata for a single weapon ID: its category, slot, effective range,
/// clip size, shoot time and reload time. Mirrors the open.mp SDK <c>WeaponInfo</c> struct.
/// </summary>
/// <remarks>
/// Values are pulled verbatim from <c>WeaponInfoList</c> in <c>open.mp-sdk/include/player.hpp</c>.
/// They are GTA: San Andreas constants and don't change at runtime, so the table
/// is mirrored here as managed data — there is no native lookup.
/// </remarks>
public readonly struct WeaponInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WeaponInfo" /> struct.
    /// </summary>
    public WeaponInfo(PlayerWeaponType type, int slot, float range, int clipSize, int shootTime, int reloadTime)
    {
        Type = type;
        Slot = slot;
        Range = range;
        ClipSize = clipSize;
        ShootTime = shootTime;
        ReloadTime = reloadTime;
    }

    /// <summary>
    /// The weapon's category (melee, bullet, rocket, etc.).
    /// </summary>
    public readonly PlayerWeaponType Type;

    /// <summary>
    /// The weapon's slot (0-12). <c>-1</c> for IDs that don't map to a real weapon
    /// (matches the SDK's <c>INVALID_WEAPON_SLOT</c> sentinel).
    /// </summary>
    public readonly int Slot;

    /// <summary>
    /// Effective range, in game units.
    /// </summary>
    public readonly float Range;

    /// <summary>
    /// Magazine capacity. 0 if not applicable.
    /// </summary>
    public readonly int ClipSize;

    /// <summary>
    /// Time between shots, in milliseconds.
    /// </summary>
    public readonly int ShootTime;

    /// <summary>
    /// Reload time in milliseconds. 0 if the weapon doesn't reload.
    /// </summary>
    public readonly int ReloadTime;

    private static readonly WeaponInfo _invalid = new(PlayerWeaponType.None, -1, 0f, 0, 0, 0);

    private static readonly WeaponInfo[] _list =
    [
        new(PlayerWeaponType.Melee, 0, 1.6f, 0, 250, 0), // 0 Fist
        new(PlayerWeaponType.Melee, 0, 1.6f, 0, 250, 0), // 1 Brass Knuckles
        new(PlayerWeaponType.Melee, 1, 1.6f, 0, 250, 0), // 2 Golf Club
        new(PlayerWeaponType.Melee, 1, 1.6f, 0, 250, 0), // 3 Nite Stick
        new(PlayerWeaponType.Melee, 1, 1.6f, 0, 250, 0), // 4 Knife
        new(PlayerWeaponType.Melee, 1, 1.6f, 0, 250, 0), // 5 Baseball Bat
        new(PlayerWeaponType.Melee, 1, 1.6f, 0, 250, 0), // 6 Shovel
        new(PlayerWeaponType.Melee, 1, 1.6f, 0, 250, 0), // 7 Pool Cue
        new(PlayerWeaponType.Melee, 1, 1.6f, 0, 250, 0), // 8 Katana
        new(PlayerWeaponType.Melee, 1, 1.6f, 0, 30, 0), // 9 Chainsaw
        new(PlayerWeaponType.Melee, 10, 1.6f, 0, 250, 0), // 10 Dildo
        new(PlayerWeaponType.Melee, 10, 1.6f, 0, 250, 0), // 11 Dildo
        new(PlayerWeaponType.Melee, 10, 1.6f, 0, 250, 0), // 12 Vibrator
        new(PlayerWeaponType.Melee, 10, 1.6f, 0, 250, 0), // 13 Vibrator
        new(PlayerWeaponType.Melee, 10, 1.6f, 0, 250, 0), // 14 Flowers
        new(PlayerWeaponType.Melee, 10, 1.6f, 0, 250, 0), // 15 Cane
        new(PlayerWeaponType.Throwable, 8, 40.0f, 1, 500, 0), // 16 Grenade
        new(PlayerWeaponType.Throwable, 8, 40.0f, 1, 500, 0), // 17 Tear Gas
        new(PlayerWeaponType.Throwable, 8, 40.0f, 1, 500, 0), // 18 Molotov
        new(PlayerWeaponType.None, -1, 0.0f, 0, 0, 0), // 19 (gap)
        new(PlayerWeaponType.None, -1, 0.0f, 0, 0, 0), // 20 (gap)
        new(PlayerWeaponType.None, -1, 0.0f, 0, 0, 0), // 21 (gap)
        new(PlayerWeaponType.Bullet, 2, 35.0f, 17, 350, 1300), // 22 Colt 45
        new(PlayerWeaponType.Bullet, 2, 35.0f, 17, 450, 1300), // 23 Silenced Pistol
        new(PlayerWeaponType.Bullet, 2, 35.0f, 7, 950, 1300), // 24 Desert Eagle
        new(PlayerWeaponType.Bullet, 3, 40.0f, 1, 1100, 0), // 25 Shotgun
        new(PlayerWeaponType.Bullet, 3, 35.0f, 2, 300, 1300), // 26 Sawn-off
        new(PlayerWeaponType.Bullet, 3, 40.0f, 7, 400, 1500), // 27 Combat Shotgun
        new(PlayerWeaponType.Bullet, 4, 35.0f, 50, 110, 1500), // 28 UZI
        new(PlayerWeaponType.Bullet, 4, 45.0f, 30, 95, 1650), // 29 MP5
        new(PlayerWeaponType.Bullet, 5, 70.0f, 30, 120, 1900), // 30 AK47
        new(PlayerWeaponType.Bullet, 5, 90.0f, 50, 120, 1900), // 31 M4
        new(PlayerWeaponType.Bullet, 4, 35.0f, 50, 110, 1500), // 32 TEC9
        new(PlayerWeaponType.Bullet, 6, 100.0f, 1, 1050, 0), // 33 Rifle
        new(PlayerWeaponType.Bullet, 6, 100.0f, 1, 1050, 0), // 34 Sniper Rifle
        new(PlayerWeaponType.Rocket, 7, 55.0f, 1, 1050, 0), // 35 Rocket Launcher
        new(PlayerWeaponType.Rocket, 7, 55.0f, 1, 1050, 0), // 36 Heat-Seeker
        new(PlayerWeaponType.Sprayable, 7, 5.1f, 500, 500, 500), // 37 Flamethrower
        new(PlayerWeaponType.Bullet, 7, 75.0f, 500, 20, 200), // 38 Minigun
        new(PlayerWeaponType.Special, 8, 40.0f, 1, 500, 0), // 39 Satchel
        new(PlayerWeaponType.Special, 12, 25.0f, 1, 500, 0), // 40 Detonator
        new(PlayerWeaponType.Sprayable, 9, 6.1f, 500, 10, 200), // 41 Spray Can
        new(PlayerWeaponType.Sprayable, 9, 10.1f, 500, 10, 200), // 42 Fire Extinguisher
        new(PlayerWeaponType.Special, 9, 100.0f, 1, 1200, 0), // 43 Camera
        new(PlayerWeaponType.Special, 11, 200.0f, 0, 1500, 0), // 44 Night Vision
        new(PlayerWeaponType.Special, 11, 200.0f, 0, 1500, 0), // 45 Thermal Goggles
        new(PlayerWeaponType.Special, 11, 1.6f, 1, 1500, 0) // 46 Parachute
    ];

    /// <summary>
    /// The static table of weapon information indexed by weapon ID, with one
    /// entry per ID from 0 to <see cref="OpenMpConstants.MAX_WEAPON_ID" /> - 1.
    /// </summary>
    public static IReadOnlyList<WeaponInfo> List => _list;

    /// <summary>
    /// Looks up information for the given weapon ID. Returns a sentinel
    /// (<see cref="PlayerWeaponType.None" /> with slot <c>-1</c>) for IDs out of range.
    /// </summary>
    public static WeaponInfo Get(byte weapon)
    {
        if (weapon >= _list.Length)
        {
            return _invalid;
        }

        return _list[weapon];
    }

    /// <inheritdoc cref="Get(byte)" />
    public static WeaponInfo Get(PlayerWeapon weapon)
    {
        return Get((byte)weapon);
    }
}
