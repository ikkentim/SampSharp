using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the key data of a player, including pressed keys and directional input.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct PlayerKeyData
{
    /// <summary>
    /// The bitmask representing the keys currently pressed by the player.
    /// </summary>
    public readonly uint keys;

    /// <summary>
    /// The state of the up and down directional keys.
    /// </summary>
    public readonly ushort upDown;

    /// <summary>
    /// The state of the left and right directional keys.
    /// </summary>
    public readonly ushort leftRight;
}