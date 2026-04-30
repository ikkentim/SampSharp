using System.Numerics;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the movement data of an object, including its target position, target rotation, and speed.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct ObjectMoveData
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectMoveData" /> struct.
    /// </summary>
    /// <param name="targetPos">The target position of the object.</param>
    /// <param name="targetRot">The target rotation of the object.</param>
    /// <param name="speed">The speed at which the object moves.</param>
    public ObjectMoveData(Vector3 targetPos, Vector3 targetRot, float speed)
    {
        TargetPos = targetPos;
        TargetRot = targetRot;
        Speed = speed;
    }

    /// <summary>
    /// Gets the target position of the object.
    /// </summary>
    public readonly Vector3 TargetPos;

    /// <summary>
    /// Gets the target rotation of the object.
    /// </summary>
    public readonly Vector3 TargetRot;

    /// <summary>
    /// Gets the speed at which the object moves.
    /// </summary>
    public readonly float Speed;
}