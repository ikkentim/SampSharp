using System.Numerics;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the movement data of an object, including its target position, target rotation, and speed.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ObjectMoveData" /> struct.
/// </remarks>
/// <param name="targetPos">The target position of the object.</param>
/// <param name="targetRot">The target rotation of the object.</param>
/// <param name="speed">The speed at which the object moves.</param>
[StructLayout(LayoutKind.Sequential)]
public readonly struct ObjectMoveData(Vector3 targetPos, Vector3 targetRot, float speed)
{

    /// <summary>
    /// Gets the target position of the object.
    /// </summary>
    public readonly Vector3 TargetPos = targetPos;

    /// <summary>
    /// Gets the target rotation of the object.
    /// </summary>
    public readonly Vector3 TargetRot = targetRot;

    /// <summary>
    /// Gets the speed at which the object moves.
    /// </summary>
    public readonly float Speed = speed;
}