using System.Numerics;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Defines player spawn class information.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PlayerClass" /> struct.
/// </remarks>
/// <param name="team">The team ID.</param>
/// <param name="skin">The skin model ID.</param>
/// <param name="spawn">The spawn position.</param>
/// <param name="angle">The spawn angle in degrees.</param>
/// <param name="weapons">The weapon slots for the class.</param>
[StructLayout(LayoutKind.Sequential)]
public readonly struct PlayerClass(int team, int skin, Vector3 spawn, float angle, WeaponSlots weapons)
{
    /// <summary>
    /// The team ID for this class.
    /// </summary>
    public readonly int Team = team;

    /// <summary>
    /// The skin model ID for this class.
    /// </summary>
    public readonly int Skin = skin;

    /// <summary>
    /// The spawn position for this class.
    /// </summary>
    public readonly Vector3 Spawn = spawn;

    /// <summary>
    /// The spawn angle (rotation) for this class.
    /// </summary>
    public readonly float Angle = angle;

    /// <summary>
    /// The weapon slots assigned to this class.
    /// </summary>
    public readonly WeaponSlots Weapons = weapons;
}