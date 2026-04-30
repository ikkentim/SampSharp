using System.Diagnostics.Contracts;
using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities;

/// <summary>Contains commonly used pre-calculated values and mathematical operations.</summary>
[Pure]
public static class MathHelper
{
    /// <summary>Represents the value of pi divided by two(1.57079637).</summary>
    public const float PiOver2 = (float)(Math.PI / 2.0);

    /// <summary>Represents the value of pi divided by four(0.7853982).</summary>
    public const float PiOver4 = (float)(Math.PI / 4.0);

    /// <summary>Represents the value of pi times two(6.28318548).</summary>
    public const float TwoPi = (float)(Math.PI * 2.0);

    /// <summary>Returns the Cartesian coordinate for one axis of a point that is defined by a given triangle and two normalized barycentric (areal) coordinates.</summary>
    /// <param name="value1">The coordinate on one axis of vertex 1 of the defining triangle.</param>
    /// <param name="value2">The coordinate on the same axis of vertex 2 of the defining triangle.</param>
    /// <param name="value3">The coordinate on the same axis of vertex 3 of the defining triangle.</param>
    /// <param name="amount1">
    /// The normalized barycentric (areal) coordinate b2, equal to the weighting factor for vertex 2, the coordinate of which is specified in
    /// value2.
    /// </param>
    /// <param name="amount2">
    /// The normalized barycentric (areal) coordinate b3, equal to the weighting factor for vertex 3, the coordinate of which is specified in
    /// value3.
    /// </param>
    /// <returns>Cartesian coordinate of the specified point with respect to the axis being used.</returns>
    public static float Barycentric(float value1, float value2, float value3, float amount1, float amount2)
    {
        return value1 + (value2 - value1) * amount1 + (value3 - value1) * amount2;
    }

    /// <summary>Performs a Catmull-Rom interpolation using the specified positions.</summary>
    /// <param name="value1">The first position in the interpolation.</param>
    /// <param name="value2">The second position in the interpolation.</param>
    /// <param name="value3">The third position in the interpolation.</param>
    /// <param name="value4">The fourth position in the interpolation.</param>
    /// <param name="amount">Weighting factor.</param>
    /// <returns>A position that is the result of the Catmull-Rom interpolation.</returns>
    public static float CatmullRom(float value1, float value2, float value3, float value4, float amount)
    {
        // Using formula from http://www.mvps.org/directx/articles/catmull/
        // Internally using doubles not to lose precision
        double amountSquared = amount * amount;
        var amountCubed = amountSquared * amount;
        return (float)(0.5 * (2.0 * value2 + (value3 - value1) * amount + (2.0 * value1 - 5.0 * value2 + 4.0 * value3 - value4) * amountSquared +
                              (3.0 * value2 - value1 - 3.0 * value3 + value4) * amountCubed));
    }

    /// <summary>Calculates the absolute value of the difference of two values.</summary>
    /// <param name="value1">Source value.</param>
    /// <param name="value2">Source value.</param>
    /// <returns>Distance between the two values.</returns>
    public static float Distance(float value1, float value2)
    {
        return Math.Abs(value1 - value2);
    }

    /// <summary>Performs a Hermite spline interpolation.</summary>
    /// <param name="value1">Source position.</param>
    /// <param name="tangent1">Source tangent.</param>
    /// <param name="value2">Source position.</param>
    /// <param name="tangent2">Source tangent.</param>
    /// <param name="amount">Weighting factor.</param>
    /// <returns>The result of the Hermite spline interpolation.</returns>
    public static float Hermite(float value1, float tangent1, float value2, float tangent2, float amount)
    {
        // All transformed to double not to lose precision
        // Otherwise, for high numbers of param:amount the result is NaN instead of Infinity
        double v1 = value1, v2 = value2, t1 = tangent1, t2 = tangent2, s = amount, result;
        var sCubed = s * s * s;
        var sSquared = s * s;

        switch (amount)
        {
            case 0f:
                result = value1;
                break;
            case 1f:
                result = value2;
                break;
            default:
                result = (2 * v1 - 2 * v2 + t2 + t1) * sCubed + (3 * v2 - 3 * v1 - 2 * t1 - t2) * sSquared + t1 * s + v1;
                break;
        }

        return (float)result;
    }

    /// <summary>Interpolates between two values using a cubic equation.</summary>
    /// <param name="value1">Source value.</param>
    /// <param name="value2">Source value.</param>
    /// <param name="amount">Weighting value.</param>
    /// <returns>Interpolated value.</returns>
    public static float SmoothStep(float value1, float value2, float amount)
    {
        // It is expected that 0 < amount < 1
        // If amount < 0, return value1
        // If amount > 1, return value2
        var result = float.Clamp(amount, 0f, 1f);
        result = Hermite(value1, 0f, value2, 0f, result);

        return result;
    }

    /// <summary>
    /// Gets the Z angle of the specified <paramref name="rotationMatrix" />.
    /// </summary>
    /// <param name="rotationMatrix">The rotation matrix to the Z angle of.</param>
    /// <returns>The Z angle.</returns>
    public static float GetZAngleFromRotationMatrix(Matrix4x4 rotationMatrix)
    {
        return WrapAngle(-MathF.Atan2(rotationMatrix.M21, rotationMatrix.M11));
    }

    /// <summary>
    /// Gets the yaw, pitch and roll in radians from the specified <paramref name="quat" />.
    /// </summary>
    /// <param name="quat">The quaternion</param>
    /// <returns>A vector containing the roll, pitch and yaw components in radians.</returns>
    public static Vector3 CreateYawPitchRollFromQuaternion(Quaternion quat)
    {
        // using a smaller epsilon than float.Epsilon
        const float epsilon = 0.000002f;
        var q = quat;

        var r = 2 * -q.Y * -q.Z - 2 * -q.X * q.W;
        float roll, pitch, yaw;

        switch (r)
        {
            case >= 1.0f - epsilon:
                roll = PiOver2;
                pitch = -float.Atan2(float.Clamp(-q.Y, -1.0f, 1.0f), float.Clamp(q.W, -1.0f, 1.0f));
                yaw = -float.Atan2(float.Clamp(-q.Z, -1.0f, 1.0f), float.Clamp(q.W, -1.0f, 1.0f));
                break;
            case <= -(1.0f - epsilon):
                roll = -PiOver2;
                pitch = -float.Atan2(float.Clamp(-q.Y, -1.0f, 1.0f), float.Clamp(q.W, -1.0f, 1.0f));
                yaw = -float.Atan2(float.Clamp(-q.Z, -1.0f, 1.0f), float.Clamp(q.W, -1.0f, 1.0f));
                break;
            default:
                roll = float.Asin(float.Clamp(r, -1.0f, 1.0f));
                pitch = -float.Atan2(float.Clamp(q.X * q.Z + -q.Y * q.W, -1.0f, 1.0f), float.Clamp(0.5f - q.X * q.X - q.Y * q.Y, -1.0f, 1.0f));
                yaw = -float.Atan2(float.Clamp(q.X * q.Y + -q.Z * q.W, -1.0f, 1.0f), float.Clamp(0.5f - q.X *-q.X - q.Z * q.Z, -1.0f, 1.0f));
                break;
        }

        roll %= TwoPi;
        pitch %= TwoPi;
        yaw %= TwoPi;

        return new Vector3(roll, pitch, yaw);
    }

    /// <summary>
    /// Converts the specified vector containing roll, pitch and yaw in radians to a quaternion.
    /// </summary>
    /// <param name="rollPitchYaw">The vector containing roll, pitch and yaw in radians.</param>
    /// <returns>The quaternion.</returns>
    public static Quaternion CreateQuaternionFromYawPitchRoll(Vector3 rollPitchYaw)
    {
        // see https://math.stackexchange.com/questions/1477926/quaternion-to-euler-with-some-properties
        // It looks like the rotation order is yxz, but angles negated for some reason.

        rollPitchYaw = -rollPitchYaw;
        var quat = Quaternion.CreateFromYawPitchRoll(rollPitchYaw.Z, rollPitchYaw.X, rollPitchYaw.Y);
        return new GTAQuat(quat.X, quat.Z, quat.Y, quat.W);
    }

    /// <summary>Reduces a given angle to a value between π and -π.</summary>
    /// <param name="angle">The angle to reduce, in radians.</param>
    /// <returns>The new angle, in radians.</returns>
    public static float WrapAngle(float angle)
    {
        angle = (float)Math.IEEERemainder(angle, TwoPi);
        switch (angle)
        {
            case <= -float.Pi:
                angle += TwoPi;
                break;
            case > float.Pi:
                angle -= TwoPi;
                break;
        }

        return angle;
    }

    /// <summary>Determines if value is powered by two.</summary>
    /// <param name="value">A value.</param>
    /// <returns><see langword="true" /> if <c>value</c> is powered by two; otherwise, <see langword="false" />.</returns>
    public static bool IsPowerOfTwo(int value)
    {
        return value > 0 && (value & value - 1) == 0;
    }
}