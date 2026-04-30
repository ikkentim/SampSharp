using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the animation data of a player, including animation ID and flags.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct PlayerAnimationData
{
    /// <summary>
    /// The ID of the animation being played by the player.
    /// </summary>
    public readonly ushort ID;

    /// <summary>
    /// The flags associated with the animation, indicating its properties or behavior.
    /// </summary>
    public readonly ushort flags;
}