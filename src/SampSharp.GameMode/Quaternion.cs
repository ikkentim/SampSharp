// SampSharp
// Copyright 2017 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System;

namespace SampSharp.GameMode
{
    /// <summary>
    ///     Represents a quaternion rotation.
    /// </summary>
    public struct Quaternion : IEquatable<Quaternion>
    {
        /// <summary>
        ///     Gets the x-coordinate of this <see cref="Quaternion" />.
        /// </summary>
        public float X { get; }

        /// <summary>
        ///     Gets the y-coordinate of this <see cref="Quaternion" />.
        /// </summary>
        public float Y { get; }

        /// <summary>
        ///     Gets the z-oordinate of this <see cref="Quaternion" />.
        /// </summary>
        public float Z { get; }

        /// <summary>
        ///     Gets the rotation component of this <see cref="Quaternion" />.
        /// </summary>
        public float W { get; }

        /// <summary>
        ///     Constructs a quaternion with X, Y, Z and W from four values.
        /// </summary>
        /// <param name="x">The x coordinate in 3d-space.</param>
        /// <param name="y">The y coordinate in 3d-space.</param>
        /// <param name="z">The z coordinate in 3d-space.</param>
        /// <param name="w">The rotation component.</param>
        public Quaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        ///     Constructs a quaternion with X, Y, Z from <see cref="Vector3" /> and rotation component from a scalar.
        /// </summary>
        /// <param name="value">The x, y, z coordinates in 3d-space.</param>
        /// <param name="w">The rotation component.</param>
        public Quaternion(Vector3 value, float w)
        {
            X = value.X;
            Y = value.Y;
            Z = value.Z;
            W = w;
        }

        /// <summary>
        ///     Constructs a quaternion from <see cref="Vector4" />.
        /// </summary>
        /// <param name="value">The x, y, z coordinates in 3d-space and the rotation component.</param>
        public Quaternion(Vector4 value)
        {
            X = value.X;
            Y = value.Y;
            Z = value.Z;
            W = value.W;
        }

        /// <summary>
        ///     Returns a quaternion representing no rotation.
        /// </summary>
        public static Quaternion Identity { get; } = new Quaternion(0, 0, 0, 1);

        /// <summary>
        ///     Creates a new <see cref="Quaternion" /> that contains concatenation between two quaternion.
        /// </summary>
        /// <param name="value1">The first <see cref="Quaternion" /> to concatenate.</param>
        /// <param name="value2">The second <see cref="Quaternion" /> to concatenate.</param>
        /// <returns>The result of rotation of <paramref name="value1" /> followed by <paramref name="value2" /> rotation.</returns>
        public static Quaternion Concatenate(Quaternion value1, Quaternion value2)
        {
            var x1 = value1.X;
            var y1 = value1.Y;
            var z1 = value1.Z;
            var w1 = value1.W;

            var x2 = value2.X;
            var y2 = value2.Y;
            var z2 = value2.Z;
            var w2 = value2.W;

            return new Quaternion(
                x2*w1 + x1*w2 + (y2*z1 - z2*y1),
                y2*w1 + y1*w2 + (z2*x1 - x2*z1),
                z2*w1 + z1*w2 + (x2*y1 - y2*x1),
                w2*w1 - (x2*x1 + y2*y1 + z2*z1));
        }

        /// <summary>
        ///     Creates a new <see cref="Quaternion" /> that contains conjugated version of the specified quaternion.
        /// </summary>
        /// <param name="value">The quaternion which values will be used to create the conjugated version.</param>
        /// <returns>The conjugate version of the specified quaternion.</returns>
        public static Quaternion Conjugate(Quaternion value)
        {
            return new Quaternion(-value.X, -value.Y, -value.Z, value.W);
        }

        /// <summary>
        ///     Creates a new <see cref="Quaternion" /> from the specified axis and angle.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>The new quaternion builded from axis and angle.</returns>
        public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle)
        {
            var half = angle*0.5f;
            var sin = (float) Math.Sin(half);
            var cos = (float) Math.Cos(half);
            return new Quaternion(axis.X*sin, axis.Y*sin, axis.Z*sin, cos);
        }

        /// <summary>
        ///     Creates a new <see cref="Quaternion" /> from the specified <see cref="Matrix" />.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <returns>A quaternion composed from the rotation part of the matrix.</returns>
        public static Quaternion CreateFromRotationMatrix(Matrix matrix)
        {
            float sqrt;
            float half;
            var scale = matrix.M11 + matrix.M22 + matrix.M33;

            if (scale > 0.0f)
            {
                sqrt = (float) Math.Sqrt(scale + 1.0f);
                var w = sqrt*0.5f;
                sqrt = 0.5f/sqrt;

                return new Quaternion(
                    (matrix.M23 - matrix.M32)*sqrt,
                    (matrix.M31 - matrix.M13)*sqrt,
                    (matrix.M12 - matrix.M21)*sqrt,
                    w
                );
            }
            if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
            {
                sqrt = (float) Math.Sqrt(1.0f + matrix.M11 - matrix.M22 - matrix.M33);
                half = 0.5f/sqrt;

                return new Quaternion(
                    0.5f*sqrt,
                    (matrix.M12 + matrix.M21)*half,
                    (matrix.M13 + matrix.M31)*half,
                    (matrix.M23 - matrix.M32)*half
                );
            }
            if (matrix.M22 > matrix.M33)
            {
                sqrt = (float) Math.Sqrt(1.0f + matrix.M22 - matrix.M11 - matrix.M33);
                half = 0.5f/sqrt;

                return new Quaternion(
                        (matrix.M21 + matrix.M12)*half,
                        0.5f*sqrt,
                        (matrix.M32 + matrix.M23)*half,
                        (matrix.M31 - matrix.M13)*half
                    )
                    ;
            }
            sqrt = (float) Math.Sqrt(1.0f + matrix.M33 - matrix.M11 - matrix.M22);
            half = 0.5f/sqrt;

            return new Quaternion(
                (matrix.M31 + matrix.M13)*half,
                (matrix.M32 + matrix.M23)*half,
                0.5f*sqrt,
                (matrix.M12 - matrix.M21)*half
            );
        }

        /// <summary>
        ///     Creates a new <see cref="Quaternion" /> from the specified yaw, pitch and roll angles.
        /// </summary>
        /// <param name="yaw">Yaw around the y axis in radians.</param>
        /// <param name="pitch">Pitch around the x axis in radians.</param>
        /// <param name="roll">Roll around the z axis in radians.</param>
        /// <returns>A new quaternion from the concatenated yaw, pitch, and roll angles.</returns>
        public static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
        {
            var halfRoll = roll*0.5f;
            var halfPitch = pitch*0.5f;
            var halfYaw = yaw*0.5f;

            var sinRoll = (float) Math.Sin(halfRoll);
            var cosRoll = (float) Math.Cos(halfRoll);
            var sinPitch = (float) Math.Sin(halfPitch);
            var cosPitch = (float) Math.Cos(halfPitch);
            var sinYaw = (float) Math.Sin(halfYaw);
            var cosYaw = (float) Math.Cos(halfYaw);

            return new Quaternion(cosYaw*sinPitch*cosRoll + sinYaw*cosPitch*sinRoll,
                sinYaw*cosPitch*cosRoll - cosYaw*sinPitch*sinRoll,
                cosYaw*cosPitch*sinRoll - sinYaw*sinPitch*cosRoll,
                cosYaw*cosPitch*cosRoll + sinYaw*sinPitch*sinRoll);
        }

        /// <summary>
        ///     Returns a dot product of two quaternions.
        /// </summary>
        /// <param name="quaternion1">The first quaternion.</param>
        /// <param name="quaternion2">The second quaternion.</param>
        /// <returns>The dot product of two quaternions.</returns>
        public static float Dot(Quaternion quaternion1, Quaternion quaternion2)
        {
            return quaternion1.X*quaternion2.X + quaternion1.Y*quaternion2.Y + quaternion1.Z*quaternion2.Z +
                   quaternion1.W*quaternion2.W;
        }

        /// <summary>
        ///     Gets the length of this <see cref="Vector4" />.
        /// </summary>
        public float Length => (float) Math.Sqrt(LengthSquared);


        /// <summary>
        ///     Gets the squared length of this <see cref="Vector4" />.
        /// </summary>
        public float LengthSquared => X*X + Y*Y + Z*Z + W*W;

        /// <summary>
        ///     Performs a linear blend between two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion" />.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion" />.</param>
        /// <param name="amount">
        ///     The blend amount where 0 returns <paramref name="quaternion1" /> and 1
        ///     <paramref name="quaternion2" />.
        /// </param>
        /// <returns>The result of linear blending between two quaternions.</returns>
        public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
        {
            var num = amount;
            var num2 = 1f - num;
            var num5 = quaternion1.X*quaternion2.X + quaternion1.Y*quaternion2.Y + quaternion1.Z*quaternion2.Z +
                       quaternion1.W*quaternion2.W;

            Quaternion quaternion;
            if (num5 >= 0f)
                quaternion = new Quaternion(
                    num2*quaternion1.X + num*quaternion2.X,
                    num2*quaternion1.Y + num*quaternion2.Y,
                    num2*quaternion1.Z + num*quaternion2.Z,
                    num2*quaternion1.W + num*quaternion2.W
                );
            else
                quaternion = new Quaternion(
                    num2*quaternion1.X - num*quaternion2.X,
                    num2*quaternion1.Y - num*quaternion2.Y,
                    num2*quaternion1.Z - num*quaternion2.Z,
                    num2*quaternion1.W - num*quaternion2.W
                );
            var num4 = quaternion.X*quaternion.X + quaternion.Y*quaternion.Y + quaternion.Z*quaternion.Z +
                       quaternion.W*quaternion.W;
            var num3 = 1f/(float) Math.Sqrt(num4);

            return quaternion*num3;
        }

        /// <summary>
        ///     Performs a spherical linear blend between two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion" />.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion" />.</param>
        /// <param name="amount">
        ///     The blend amount where 0 returns <paramref name="quaternion1" /> and 1
        ///     <paramref name="quaternion2" />.
        /// </param>
        /// <returns>The result of spherical linear blending between two quaternions.</returns>
        public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
        {
            float num2;
            float num3;
            var num = amount;
            var num4 = quaternion1.X*quaternion2.X + quaternion1.Y*quaternion2.Y + quaternion1.Z*quaternion2.Z +
                       quaternion1.W*quaternion2.W;
            var flag = false;
            if (num4 < 0f)
            {
                flag = true;
                num4 = -num4;
            }
            if (num4 > 0.999999f)
            {
                num3 = 1f - num;
                num2 = flag ? -num : num;
            }
            else
            {
                var num5 = (float) Math.Acos(num4);
                var num6 = (float) (1.0/Math.Sin(num5));
                num3 = (float) Math.Sin((1f - num)*num5)*num6;
                num2 = flag ? (float) -Math.Sin(num*num5)*num6 : (float) Math.Sin(num*num5)*num6;
            }
            return new Quaternion(
                num3*quaternion1.X + num2*quaternion2.X,
                num3*quaternion1.Y + num2*quaternion2.Y,
                num3*quaternion1.Z + num2*quaternion2.Z,
                num3*quaternion1.W + num2*quaternion2.W
            );
        }

        /// <summary>
        ///     Scales the quaternion magnitude to unit length.
        /// </summary>
        /// <param name="quaternion">Source <see cref="Quaternion" />.</param>
        /// <returns>The unit length quaternion.</returns>
        public static Quaternion Normalize(Quaternion quaternion)
        {
            var factor = 1f/quaternion.Length;
            return quaternion*factor;
        }

        /// <summary>
        ///     Gets a <see cref="Vector4" /> representation for this object.
        /// </summary>
        /// <returns>A <see cref="Vector4" /> representation for this object.</returns>
        public Vector4 ToVector4()
        {
            return new Vector4(X, Y, Z, W);
        }

        /// <summary>
        ///     Adds two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion" /> on the left of the add sign.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion" /> on the right of the add sign.</param>
        /// <returns>Sum of the vectors.</returns>
        public static Quaternion operator +(Quaternion quaternion1, Quaternion quaternion2)
        {
            return new Quaternion(
                quaternion1.X + quaternion2.X,
                quaternion1.Y + quaternion2.Y,
                quaternion1.Z + quaternion2.Z,
                quaternion1.W + quaternion2.W);
        }

        /// <summary>
        ///     Divides a <see cref="Quaternion" /> by the other <see cref="Quaternion" />.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion" /> on the left of the div sign.</param>
        /// <param name="quaternion2">Divisor <see cref="Quaternion" /> on the right of the div sign.</param>
        /// <returns>The result of dividing the quaternions.</returns>
        public static Quaternion operator /(Quaternion quaternion1, Quaternion quaternion2)
        {
            var x = quaternion1.X;
            var y = quaternion1.Y;
            var z = quaternion1.Z;
            var w = quaternion1.W;
            var num14 = quaternion2.X*quaternion2.X + quaternion2.Y*quaternion2.Y + quaternion2.Z*quaternion2.Z +
                        quaternion2.W*quaternion2.W;
            var num5 = 1f/num14;
            var num4 = -quaternion2.X*num5;
            var num3 = -quaternion2.Y*num5;
            var num2 = -quaternion2.Z*num5;
            var num = quaternion2.W*num5;
            var num13 = y*num2 - z*num3;
            var num12 = z*num4 - x*num2;
            var num11 = x*num3 - y*num4;
            var num10 = x*num4 + y*num3 + z*num2;

            return new Quaternion(
                x*num + num4*w + num13,
                y*num + num3*w + num12,
                z*num + num2*w + num11,
                w*num - num10
            );
        }

        /// <summary>
        ///     Compares whether two <see cref="Quaternion" /> instances are equal.
        /// </summary>
        /// <param name="quaternion1"><see cref="Quaternion" /> instance on the left of the equal sign.</param>
        /// <param name="quaternion2"><see cref="Quaternion" /> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(Quaternion quaternion1, Quaternion quaternion2)
        {
            return quaternion1.X.Equals(quaternion2.X) && quaternion1.Y.Equals(quaternion2.Y) &&
                   quaternion1.Z.Equals(quaternion2.Z) && quaternion1.W.Equals(quaternion2.W);
        }

        /// <summary>
        ///     Compares whether two <see cref="Quaternion" /> instances are not equal.
        /// </summary>
        /// <param name="quaternion1"><see cref="Quaternion" /> instance on the left of the not equal sign.</param>
        /// <param name="quaternion2"><see cref="Quaternion" /> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(Quaternion quaternion1, Quaternion quaternion2)
        {
            if (quaternion1.X.Equals(quaternion2.X) && quaternion1.Y.Equals(quaternion2.Y) &&
                quaternion1.Z.Equals(quaternion2.Z))
                return !quaternion1.W.Equals(quaternion2.W);
            return true;
        }

        /// <summary>
        ///     Multiplies two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion" /> on the left of the mul sign.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion" /> on the right of the mul sign.</param>
        /// <returns>Result of the quaternions multiplication.</returns>
        public static Quaternion operator *(Quaternion quaternion1, Quaternion quaternion2)
        {
            var x = quaternion1.X;
            var y = quaternion1.Y;
            var z = quaternion1.Z;
            var w = quaternion1.W;
            var num4 = quaternion2.X;
            var num3 = quaternion2.Y;
            var num2 = quaternion2.Z;
            var num = quaternion2.W;
            var num12 = y*num2 - z*num3;
            var num11 = z*num4 - x*num2;
            var num10 = x*num3 - y*num4;
            var num9 = x*num4 + y*num3 + z*num2;

            return new Quaternion(
                x*num + num4*w + num12,
                y*num + num3*w + num11,
                z*num + num2*w + num10,
                w*num - num9
            );
        }

        /// <summary>
        ///     Multiplies the components of quaternion by a scalar.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Vector3" /> on the left of the mul sign.</param>
        /// <param name="scaleFactor">Scalar value on the right of the mul sign.</param>
        /// <returns>Result of the quaternion multiplication with a scalar.</returns>
        public static Quaternion operator *(Quaternion quaternion1, float scaleFactor)
        {
            return new Quaternion(
                quaternion1.X*scaleFactor,
                quaternion1.Y*scaleFactor,
                quaternion1.Z*scaleFactor,
                quaternion1.W*scaleFactor
            );
        }

        /// <summary>
        ///     Subtracts a <see cref="Quaternion" /> from a <see cref="Quaternion" />.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Vector3" /> on the left of the sub sign.</param>
        /// <param name="quaternion2">Source <see cref="Vector3" /> on the right of the sub sign.</param>
        /// <returns>Result of the quaternion subtraction.</returns>
        public static Quaternion operator -(Quaternion quaternion1, Quaternion quaternion2)
        {
            return new Quaternion(
                quaternion1.X - quaternion2.X,
                quaternion1.Y - quaternion2.Y,
                quaternion1.Z - quaternion2.Z,
                quaternion1.W - quaternion2.W
            );
        }

        /// <summary>
        ///     Flips the sign of the all the quaternion components.
        /// </summary>
        /// <param name="quaternion">Source <see cref="Quaternion" /> on the right of the sub sign.</param>
        /// <returns>The result of the quaternion negation.</returns>
        public static Quaternion operator -(Quaternion quaternion)
        {
            return new Quaternion(-quaternion.X, -quaternion.Y, -quaternion.Z, -quaternion.W);
        }

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Quaternion other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);
        }

        /// <summary>
        ///     Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Quaternion && Equals((Quaternion) obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode*397) ^ Y.GetHashCode();
                hashCode = (hashCode*397) ^ Z.GetHashCode();
                hashCode = (hashCode*397) ^ W.GetHashCode();
                return hashCode;
            }
        }
    }
}