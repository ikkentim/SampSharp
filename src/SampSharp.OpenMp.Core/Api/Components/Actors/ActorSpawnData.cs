using System.Numerics;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the data required to spawn an actor in the game.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct ActorSpawnData
{
    /// <summary>
    /// Gets the position where the actor will be spawned.
    /// </summary>
    public readonly Vector3 Position;

    /// <summary>
    /// Gets the angle at which the actor will be facing upon spawn.
    /// </summary>
    public readonly float FacingAngle;

    /// <summary>
    /// Gets the skin ID of the actor.
    /// </summary>
    public readonly int Skin;
}
