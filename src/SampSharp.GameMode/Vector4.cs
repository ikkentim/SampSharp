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
using SampSharp.GameMode.Helpers;

namespace SampSharp.GameMode
{
    /// <summary>
    ///     Represents a 4D vector.
    /// </summary>
    public struct Vector4 : IEquatable<Vector4>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector4" /> struct.
        /// </summary>
        /// <param name="x">Value of the x component.</param>
        /// <param name="y">Value of the y component.</param>
        /// <param name="z">Value of the z component.</param>
        /// <param name="w">Value of the w component.</param>
        public Vector4(float x, float y, float z, float w) : this()
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector4" /> struct.
        /// </summary>
        /// <param name="value">The x and y component.</param>
        /// <param name="z">Value of the z component.</param>
        /// <param name="w">Value of the w component.</param>
        public Vector4(Vector2 value, float z, float w) : this()
        {
            X = value.X;
            Y = value.Y;
            Z = z;
            W = w;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector4" /> struct.
        /// </summary>
        /// <param name="value">The x, y and z component.</param>
        /// <param name="w">Value of the w component.</param>
        public Vector4(Vector3 value, float w) : this()
        {
            X = value.X;
            Y = value.Y;
            Z = value.Z;
            W = w;
        }

        /// <summary>
        ///     Gets the X component of this <see cref="Vector4" />.
        /// </summary>
        public float X { get; }

        /// <summary>
        ///     Gets the Y component of this <see cref="Vector4" />.
        /// </summary>
        public float Y { get; }

        /// <summary>
        ///     Gets the Z component of this <see cref="Vector4" />.
        /// </summary>
        public float Z { get; }

        /// <summary>
        ///     Gets the W component of this <see cref="Vector4" />.
        /// </summary>
        public float W { get; }

        /// <summary>
        ///     Returns an empty <see cref="Vector4" />.
        /// </summary>
        public static Vector4 Zero { get; } = new Vector4(0, 0, 0, 0);

        /// <summary>
        ///     Returns a <see cref="Vector4" /> with each component set to 1.
        /// </summary>
        public static Vector4 One { get; } = new Vector4(1, 1, 1, 1);

        /// <summary>
        ///     Returns a <see cref="Vector4" /> with components 1, 0, 0, 0.
        /// </summary>
        public static Vector4 UnitX { get; } = new Vector4(1, 0, 0, 0);

        /// <summary>
        ///     Returns a <see cref="Vector4" /> with components 0, 1, 0, 0.
        /// </summary>
        public static Vector4 UnitY { get; } = new Vector4(0, 1, 0, 0);

        /// <summary>
        ///     Returns a <see cref="Vector4" /> with components 0, 0, 1, 0.
        /// </summary>
        public static Vector4 UnitZ { get; } = new Vector4(0, 0, 1, 0);

        /// <summary>
        ///     Returns a <see cref="Vector4" /> with components 0, 0, 0, 1.
        /// </summary>
        public static Vector4 UnitW { get; } = new Vector4(0, 0, 0, 1);

        /// <summary>
        ///     Gets the length of this <see cref="Vector4" />.
        /// </summary>
        public float Length => Distance(this, Zero);

        /// <summary>
        ///     Gets the squared length of this <see cref="Vector4" />.
        /// </summary>
        public float LengthSquared => DistanceSquared(this, Zero);

        /// <summary>
        ///     Gets whether this <see cref="Vector4" /> is empty.
        /// </summary>
        public bool IsEmpty => X.Equals(0) && Y.Equals(0) && Z.Equals(0);

        /// <summary>
        ///     Gets the distance to another <see cref="Vector4" />.
        /// </summary>
        /// <param name="other">The <see cref="Vector4" /> to calculate the distance to.</param>
        /// <returns>The distance between the vectors.</returns>
        public float DistanceTo(Vector4 other)
        {
            return Distance(this, other);
        }

        /// <summary>
        ///     Creates a new <see cref="Vector4" /> instance with the components normalized to a single unit.
        /// </summary>
        public Vector4 Normalized()
        {
            return Normalize(this);
        }

        /// <summary>
        ///     Creates a new <see cref="Vector4" /> that contains the cartesian coordinates of a vector specified in barycentric
        ///     coordinates and relative to 4d-triangle.
        /// </summary>
        /// <param name="value1">The first vector of 4d-triangle.</param>
        /// <param name="value2">The second vector of 4d-triangle.</param>
        /// <param name="value3">The third vector of 4d-triangle.</param>
        /// <param name="amount1">
        ///     Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of
        ///     4d-triangle.
        /// </param>
        /// <param name="amount2">
        ///     Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of
        ///     4d-triangle.
        /// </param>
        /// <returns>The cartesian translation of barycentric coordinates.</returns>
        public static Vector4 Barycentric(Vector4 value1, Vector4 value2, Vector4 value3, float amount1, float amount2)
        {
            return new Vector4(
                MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2),
                MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2),
                MathHelper.Barycentric(value1.W, value2.W, value3.W, amount1, amount2));
        }

        /// <summary>
        ///     Creates a new <see cref="Vector4" /> that contains CatmullRom interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector in interpolation.</param>
        /// <param name="value2">The second vector in interpolation.</param>
        /// <param name="value3">The third vector in interpolation.</param>
        /// <param name="value4">The fourth vector in interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The result of CatmullRom interpolation.</returns>
        public static Vector4 CatmullRom(Vector4 value1, Vector4 value2, Vector4 value3, Vector4 value4, float amount)
        {
            return new Vector4(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount),
                MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount),
                MathHelper.CatmullRom(value1.W, value2.W, value3.W, value4.W, amount));
        }

        /// <summary>
        ///     Clamps the specified value within a range.
        /// </summary>
        /// <param name="value1">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The clamped value.</returns>
        public static Vector4 Clamp(Vector4 value1, Vector4 min, Vector4 max)
        {
            return new Vector4(
                MathHelper.Clamp(value1.X, min.X, max.X),
                MathHelper.Clamp(value1.Y, min.Y, max.Y),
                MathHelper.Clamp(value1.Z, min.Z, max.Z),
                MathHelper.Clamp(value1.W, min.W, max.W));
        }

        /// <summary>
        ///     Returns the distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The distance between two vectors.</returns>
        public static float Distance(Vector4 value1, Vector4 value2)
        {
            return (float) Math.Sqrt(DistanceSquared(value1, value2));
        }

        /// <summary>
        ///     Returns the distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The distance between two vectors as an output parameter.</param>
        public static void Distance(ref Vector4 value1, ref Vector4 value2, out float result)
        {
            result = (float) Math.Sqrt(DistanceSquared(value1, value2));
        }

        /// <summary>
        ///     Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The squared distance between two vectors.</returns>
        public static float DistanceSquared(Vector4 value1, Vector4 value2)
        {
            return (value1.X - value2.X)*(value1.X - value2.X) +
                   (value1.Y - value2.Y)*(value1.Y - value2.Y) +
                   (value1.Z - value2.Z)*(value1.Z - value2.Z) +
                   (value1.W - value2.W)*(value1.W - value2.W);
        }

        /// <summary>
        ///     Returns a dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The dot product of two vectors.</returns>
        public static float Dot(Vector4 value1, Vector4 value2)
        {
            return value1.X*value2.X + value1.Y*value2.Y + value1.Z*value2.Z + value1.W*value2.W;
        }

        /// <summary>
        ///     Creates a new <see cref="Vector4" /> that contains hermite spline interpolation.
        /// </summary>
        /// <param name="value1">The first position vector.</param>
        /// <param name="tangent1">The first tangent vector.</param>
        /// <param name="value2">The second position vector.</param>
        /// <param name="tangent2">The second tangent vector.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The hermite spline interpolation vector.</returns>
        public static Vector4 Hermite(Vector4 value1, Vector4 tangent1, Vector4 value2, Vector4 tangent2, float amount)
        {
            return new Vector4(MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount),
                MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount),
                MathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount),
                MathHelper.Hermite(value1.W, tangent1.W, value2.W, tangent2.W, amount));
        }

        /// <summary>
        ///     Creates a new <see cref="Vector4" /> that contains linear interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <returns>The result of linear interpolation of the specified vectors.</returns>
        public static Vector4 Lerp(Vector4 value1, Vector4 value2, float amount)
        {
            return new Vector4(
                MathHelper.Lerp(value1.X, value2.X, amount),
                MathHelper.Lerp(value1.Y, value2.Y, amount),
                MathHelper.Lerp(value1.Z, value2.Z, amount),
                MathHelper.Lerp(value1.W, value2.W, amount));
        }

        /// <summary>
        ///     Creates a new <see cref="Vector4" /> that contains a maximal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The <see cref="Vector4" /> with maximal values from the two vectors.</returns>
        public static Vector4 Max(Vector4 value1, Vector4 value2)
        {
            return new Vector4(
                MathHelper.Max(value1.X, value2.X),
                MathHelper.Max(value1.Y, value2.Y),
                MathHelper.Max(value1.Z, value2.Z),
                MathHelper.Max(value1.W, value2.W));
        }

        /// <summary>
        ///     Creates a new <see cref="Vector4" /> that contains a minimal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The <see cref="Vector4" /> with minimal values from the two vectors.</returns>
        public static Vector4 Min(Vector4 value1, Vector4 value2)
        {
            return new Vector4(
                MathHelper.Min(value1.X, value2.X),
                MathHelper.Min(value1.Y, value2.Y),
                MathHelper.Min(value1.Z, value2.Z),
                MathHelper.Min(value1.W, value2.W));
        }

        /// <summary>
        ///     Creates a new <see cref="Vector4" /> that contains a normalized values from another vector.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4" />.</param>
        /// <returns>Unit vector.</returns>
        public static Vector4 Normalize(Vector4 value)
        {
            var factor = 1f/value.Length;

            return new Vector4(value.X*factor, value.Y*factor, value.Z*factor, value.W*factor);
        }

        /// <summary>
        ///     Creates a new <see cref="Vector4" /> that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4" />.</param>
        /// <param name="value2">Source <see cref="Vector4" />.</param>
        /// <param name="amount">Weighting value.</param>
        /// <returns>Cubic interpolation of the specified vectors.</returns>
        public static Vector4 SmoothStep(Vector4 value1, Vector4 value2, float amount)
        {
            return new Vector4(
                MathHelper.SmoothStep(value1.X, value2.X, amount),
                MathHelper.SmoothStep(value1.Y, value2.Y, amount),
                MathHelper.SmoothStep(value1.Z, value2.Z, amount),
                MathHelper.SmoothStep(value1.W, value2.W, amount));
        }

        /// <summary>
        ///     Creates a new <see cref="Vector4" /> that contains a transformation of 2d-vector by the specified
        ///     <see cref="Matrix" />.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2" />.</param>
        /// <param name="matrix">The transformation <see cref="Matrix" />.</param>
        /// <returns>Transformed <see cref="Vector4" />.</returns>
        public static Vector4 Transform(Vector2 value, Matrix matrix)
        {
            return new Vector4(
                value.X*matrix.M11 + value.Y*matrix.M21 + matrix.M41,
                value.X*matrix.M12 + value.Y*matrix.M22 + matrix.M42,
                value.X*matrix.M13 + value.Y*matrix.M23 + matrix.M43,
                value.X*matrix.M14 + value.Y*matrix.M24 + matrix.M44);
        }

        /// <summary>
        ///     Creates a new <see cref="Vector4" /> that contains a transformation of 3d-vector by the specified
        ///     <see cref="Matrix" />.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3" />.</param>
        /// <param name="matrix">The transformation <see cref="Matrix" />.</param>
        /// <returns>Transformed <see cref="Vector4" />.</returns>
        public static Vector4 Transform(Vector3 value, Matrix matrix)
        {
            return new Vector4(
                value.X*matrix.M11 + value.Y*matrix.M21 + value.Z*matrix.M31 + matrix.M41,
                value.X*matrix.M12 + value.Y*matrix.M22 + value.Z*matrix.M32 + matrix.M42,
                value.X*matrix.M13 + value.Y*matrix.M23 + value.Z*matrix.M33 + matrix.M43,
                value.X*matrix.M14 + value.Y*matrix.M24 + value.Z*matrix.M34 + matrix.M44);
        }

        /// <summary>
        ///     Creates a new <see cref="Vector4" /> that contains a transformation of 4d-vector by the specified
        ///     <see cref="Matrix" />.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4" />.</param>
        /// <param name="matrix">The transformation <see cref="Matrix" />.</param>
        /// <returns>Transformed <see cref="Vector4" />.</returns>
        public static Vector4 Transform(Vector4 value, Matrix matrix)
        {
            return new Vector4(
                value.X*matrix.M11 + value.Y*matrix.M21 + value.Z*matrix.M31 + value.W*matrix.M41,
                value.X*matrix.M12 + value.Y*matrix.M22 + value.Z*matrix.M32 + value.W*matrix.M42,
                value.X*matrix.M13 + value.Y*matrix.M23 + value.Z*matrix.M33 + value.W*matrix.M43,
                value.X*matrix.M14 + value.Y*matrix.M24 + value.Z*matrix.M34 + value.W*matrix.M44);
        }

        /// <summary>
        ///     Adds the left <see cref="Vector4" />'s components to the right <see cref="Vector4" />'s components and stores it in
        ///     a
        ///     new <see cref="Vector4" />.
        /// </summary>
        /// <param name="left">A <see cref="Vector4" />.</param>
        /// <param name="right">A <see cref="Vector4" />.</param>
        /// <returns>A new <see cref="Vector4" />.</returns>
        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
        }

        /// <summary>
        ///     Subtracts the right <see cref="Vector4" />'s components from the left <see cref="Vector4" />'s components and
        ///     stores
        ///     it in a new <see cref="Vector4" />.
        /// </summary>
        /// <param name="left">A <see cref="Vector4" />.</param>
        /// <param name="right">A <see cref="Vector4" />.</param>
        /// <returns>A new <see cref="Vector4" />.</returns>
        public static Vector4 operator -(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
        }

        /// <summary>
        ///     Creates a <see cref="Vector4" /> with the components set to the negative values of the given
        ///     <paramref name="vector" />'s components.
        /// </summary>
        /// <param name="vector">The vector to invert.</param>
        /// <returns>The new <see cref="Vector4" />.</returns>
        public static Vector4 operator -(Vector4 vector)
        {
            return new Vector4(-vector.X, -vector.Y, -vector.Z, -vector.W);
        }

        /// <summary>
        ///     Multiplies the components <see cref="Vector4" /> by the given scalar and stores them in a new
        ///     <see cref="Vector4" />.
        /// </summary>
        /// <param name="vector">The <see cref="Vector4" />.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>The new <see cref="Vector4" />.</returns>
        public static Vector4 operator *(Vector4 vector, float scalar)
        {
            return new Vector4(vector.X*scalar, vector.Y*scalar, vector.Z*scalar, vector.W*scalar);
        }

        /// <summary>
        ///     Multiplies the components <see cref="Vector4" /> by the components of the right <see cref="Vector4" /> and stores
        ///     them in a new <see cref="Vector4" />.
        /// </summary>
        /// <param name="left">A <see cref="Vector4" />.</param>
        /// <param name="right">A <see cref="Vector4" />.</param>
        /// <returns>The new <see cref="Vector4" />.</returns>
        public static Vector4 operator *(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X*right.X, left.Y*right.Y, left.Z*right.Z, left.W*right.W);
        }

        /// <summary>
        ///     Divides the components <see cref="Vector4" /> by the given scalar and stores them in a new <see cref="Vector4" />.
        /// </summary>
        /// <param name="vector">The <see cref="Vector4" />.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>The new <see cref="Vector4" />.</returns>
        public static Vector4 operator /(Vector4 vector, float scalar)
        {
            return new Vector4(vector.X/scalar, vector.Y/scalar, vector.Z/scalar, vector.W/scalar);
        }

        /// <summary>
        ///     Divides the components <see cref="Vector4" /> by the components of the right <see cref="Vector4" /> and stores them
        ///     in a new <see cref="Vector4" />.
        /// </summary>
        /// <param name="left">The numerator.</param>
        /// <param name="right">The denominator.</param>
        /// <returns>The new <see cref="Vector4" />.</returns>
        public static Vector4 operator /(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X/right.X, left.Y/right.Y, left.Z/right.Z, left.W/right.W);
        }

        /// <summary>
        ///     Tests whether all components of both <see cref="Vector4" /> are equivalent.
        /// </summary>
        /// <param name="left">Instance of <see cref="Vector4" /> to compare.</param>
        /// <param name="right">Instance of <see cref="Vector4" /> to compare.</param>
        /// <returns>true if all components of both <see cref="Vector4" /> are equivalent; otherwise, false.</returns>
        public static bool operator ==(Vector4 left, Vector4 right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Tests whether any component of both <see cref="Vector4" /> are not equivalent.
        /// </summary>
        /// <param name="left">Instance of <see cref="Vector4" /> to compare.</param>
        /// <param name="right">Instance of <see cref="Vector4" /> to compare.</param>
        /// <returns>true if any component of both <see cref="Vector4" /> are not equivalent; otherwise, false.</returns>
        public static bool operator !=(Vector4 left, Vector4 right)
        {
            return !left.Equals(right);
        }

        #region Equality members

        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer that is the hash code for this instance.
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

        /// <summary>
        ///     Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current instance. </param>
        public bool Equals(Vector4 obj)
        {
            return X.Equals(obj.X) && Y.Equals(obj.Y) && Z.Equals(obj.Z) && W.Equals(obj.W);
        }

        /// <summary>
        ///     Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current instance. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector4 && Equals((Vector4) obj);
        }

        #endregion

        /// <summary>
        ///     Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String" /> containing a fully qualified type name.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"({X}, {Y}, {Z}, {W})";
        }
    }
}