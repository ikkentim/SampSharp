using System.Numerics;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Defines player spawn class information.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct PlayerClass
{
    /// <summary>
    /// The team ID for this class.
    /// </summary>
    public readonly int Team;

    /// <summary>
    /// The skin model ID for this class.
    /// </summary>
    public readonly int Skin;

    /// <summary>
    /// The spawn position for this class.
    /// </summary>
    public readonly Vector3 Spawn;

    /// <summary>
    /// The spawn angle (rotation) for this class.
    /// </summary>
    public readonly float Angle;

    /// <summary>
    /// The weapon slots assigned to this class.
    /// </summary>
    public readonly WeaponSlots Weapons;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerClass" /> struct.
    /// </summary>
    /// <param name="team">The team ID.</param>
    /// <param name="skin">The skin model ID.</param>
    /// <param name="spawn">The spawn position.</param>
    /// <param name="angle">The spawn angle in degrees.</param>
    /// <param name="weapons">The weapon slots for the class.</param>
    public PlayerClass(int team, int skin, Vector3 spawn, float angle, WeaponSlots weapons)
    {
        Team = team;
        Skin = skin;
        Spawn = spawn;
        Angle = angle;
        Weapons = weapons;
    }
}