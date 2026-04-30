using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents a quaternion in the GTA coordinate space.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct GTAQuat
{
    /// <summary>
    /// The W component of this quaternion.
    /// </summary>
    public readonly float W;

    /// <summary>
    /// The X component of this quaternion.
    /// </summary>
    public readonly float X;

    /// <summary>
    /// The Y component of this quaternion.
    /// </summary>
    public readonly float Y;

    /// <summary>
    /// The Z component of this quaternion.
    /// </summary>
    public readonly float Z;

    /// <summary>
    /// Initializes a new instance of the <see cref="GTAQuat" /> struct.
    /// </summary>
    /// <param name="x">The X component of the quaternion.</param>
    /// <param name="y">The Y component of the quaternion.</param>
    /// <param name="z">The Z component of the quaternion.</param>
    /// <param name="w">The W component of the quaternion.</param>
    public GTAQuat(float x, float y, float z, float w)
    {
        W = w;
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Converts a <see cref="GTAQuat" /> to a <see cref="Quaternion" />.
    /// </summary>
    /// <param name="gtaQuat">The value to convert</param>
    public static implicit operator Quaternion(GTAQuat gtaQuat)
    {
        // GTA quaternions are fubar, correct the components in our coordinate space.
        return new Quaternion(-gtaQuat.X, -gtaQuat.Y, -gtaQuat.Z, gtaQuat.W);
    }

    /// <summary>
    /// Converts a <see cref="Quaternion" /> to a <see cref="GTAQuat" />.
    /// </summary>
    /// <param name="quat">The value to convert.</param>
    public static implicit operator GTAQuat(Quaternion quat)
    {
        // GTA quaternions are fubar, correct the components in our coordinate space.
        return new GTAQuat(-quat.X, -quat.Y, -quat.Z, quat.W);
    }
}