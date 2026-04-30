using System.Numerics;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents data related to a player's bullet, including its origin, hit position, and other details.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct PlayerBulletData
{
    /// <summary>
    /// The origin position of the bullet when it was fired.
    /// </summary>
    public readonly Vector3 origin;

    /// <summary>
    /// The position where the bullet hit.
    /// </summary>
    public readonly Vector3 hitPos;

    /// <summary>
    /// The offset of the bullet relative to the player's position.
    /// </summary>
    public readonly Vector3 offset;

    /// <summary>
    /// The weapon ID used to fire the bullet.
    /// </summary>
    public readonly byte weapon;

    /// <summary>
    /// The type of object or entity that was hit by the bullet.
    /// </summary>
    public readonly PlayerBulletHitType hitType;

    /// <summary>
    /// The ID of the object or entity that was hit by the bullet.
    /// </summary>
    public readonly ushort hitID;
}