using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents an array of skill levels for a player.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct SkillsArray
{
    /// <summary>
    /// The array of skill levels, where each index corresponds to a specific skill.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = OpenMpConstants.NUM_SKILL_LEVELS)]
    public readonly ushort[] Values;
}