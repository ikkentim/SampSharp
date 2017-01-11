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
    ///     Represents a 3D vector.
    /// </summary>
    public struct Vector3 : IEquatable<Vector3>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="x">Value of the x component.</param>
        /// <param name="y">Value of the y component.</param>
        /// <param name="z">Value of the z component.</param>
        public Vector3(float x, float y, float z) : this()
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="x">Value of the x component.</param>
        /// <param name="y">Value of the y component.</param>
        /// <param name="z">Value of the z component.</param>
        public Vector3(double x, double y, double z) : this((float) x, (float) y, (float) z)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> struct with the z component set to 0.
        /// </summary>
        /// <param name="x">Value of the x component.</param>
        /// <param name="y">Value of the y component.</param>
        public Vector3(float x, float y)
            : this(x, y, 0)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> struct with the z component set to 0.
        /// </summary>
        /// <param name="x">Value of the x component.</param>
        /// <param name="y">Value of the y component.</param>
        public Vector3(double x, double y)
            : this((float) x, (float) y)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="xy">Values of the x and y components.</param>
        /// <param name="z">Value of the z component.</param>
        public Vector3(Vector2 xy, float z)
            : this(xy.X, xy.Y, z)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="xy">Values of the x and y components.</param>
        /// <param name="z">Value of the z component.</param>
        public Vector3(Vector2 xy, double z)
            : this(xy.X, xy.Y, (float) z)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> struct with same values for x, y and z components.
        /// </summary>
        /// <param name="xyz">Value of x, y and z components.</param>
        public Vector3(float xyz)
            : this(xyz, xyz, xyz)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> struct with same values for x, y and z components.
        /// </summary>
        /// <param name="xyz">Value of x, y and z components.</param>
        public Vector3(double xyz)
            : this((float) xyz, (float) xyz, (float) xyz)
        {
        }

        /// <summary>
        ///     Gets the X component of this <see cref="Vector3" />.
        /// </summary>
        public float X { get; }

        /// <summary>
        ///     Gets the Y component of this <see cref="Vector3" />.
        /// </summary>
        public float Y { get; }

        /// <summary>
        ///     Gets the Z component of this <see cref="Vector3" />.
        /// </summary>
        public float Z { get; }

        /// <summary>
        ///     Returns an empty <see cref="Vector3" />.
        /// </summary>
        public static Vector3 Zero { get; } = new Vector3(0);

        /// <summary>
        ///     Returns a <see cref="Vector3" /> with each component set to 1.
        /// </summary>
        public static Vector3 One { get; } = new Vector3(1);

        /// <summary>
        ///     Returns a <see cref="Vector3" /> with components 1, 0, 0.
        /// </summary>
        public static Vector3 UnitX { get; } = new Vector3(1, 0, 0);

        /// <summary>
        ///     Returns a <see cref="Vector3" /> with components 0, 1, 0.
        /// </summary>
        public static Vector3 UnitY { get; } = new Vector3(0, 1, 0);

        /// <summary>
        ///     Returns a <see cref="Vector3" /> with components 0, 0, 1.
        /// </summary>
        public static Vector3 UnitZ { get; } = new Vector3(0, 0, 1);

        /// <summary>
        ///     Returns a <see cref="Vector3" /> with components 0, 0, 1.
        /// </summary>
        public static Vector3 Up { get; } = new Vector3(0, 0, 1);

        /// <summary>
        ///     Returns a <see cref="Vector3" /> with components 0, 0, -1.
        /// </summary>
        public static Vector3 Down { get; } = new Vector3(0, 0, -1);

        /// <summary>
        ///     Returns a <see cref="Vector3" /> with components -1, 0, 0.
        /// </summary>
        public static Vector3 Left { get; } = new Vector3(-1, 0, 0);

        /// <summary>
        ///     Returns a <see cref="Vector3" /> with components 1, 0, 0.
        /// </summary>
        public static Vector3 Right { get; } = new Vector3(1, 0, 0);

        /// <summary>
        ///     Returns a <see cref="Vector3" /> with components 0, 1, 0.
        /// </summary>
        public static Vector3 Forward { get; } = new Vector3(0, 1, 0);

        /// <summary>
        ///     Returns a <see cref="Vector3" /> with components 0, -1, 0.
        /// </summary>
        public static Vector3 Backward { get; } = new Vector3(0, -1, 0);

        /// <summary>
        ///     Gets the length of this <see cref="Vector3" />.
        /// </summary>
        public float Length => Distance(this, Zero);

        /// <summary>
        ///     Gets the squared length of this <see cref="Vector3" />.
        /// </summary>
        public float LengthSquared => DistanceSquared(this, Zero);

        /// <summary>
        ///     Gets whether this <see cref="Vector3" /> is empty.
        /// </summary>
        public bool IsEmpty => X.Equals(0) && Y.Equals(0) && Z.Equals(0);

        /// <summary>
        ///     Gets the distance to another <see cref="Vector3" />.
        /// </summary>
        /// <param name="other">The <see cref="Vector3" /> to calculate the distance to.</param>
        /// <returns>The distance between the vectors.</returns>
        public float DistanceTo(Vector3 other)
        {
            return Distance(this, other);
        }

        /// <summary>
        ///     Creates a new <see cref="Vector3" /> instance with the components normalized to a single unit.
        /// </summary>
        public Vector3 Normalized()
        {
            return Normalize(this);
        }

        /// <summary>
        ///     Creates a new <see cref="Vector3" /> that contains the cartesian coordinates of a vector specified in barycentric
        ///     coordinates and relative to 3d-triangle.
        /// </summary>
        /// <param name="value1">The first vector of 3d-triangle.</param>
        /// <param name="value2">The second vector of 3d-triangle.</param>
        /// <param name="value3">The third vector of 3d-triangle.</param>
        /// <param name="amount1">
        ///     Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of
        ///     3d-triangle.
        /// </param>
        /// <param name="amount2">
        ///     Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of
        ///     3d-triangle.
        /// </param>
        /// <returns>The cartesian translation of barycentric coordinates.</returns>
        public static Vector3 Barycentric(Vector3 value1, Vector3 value2, Vector3 value3, float amount1, float amount2)
        {
            return new Vector3(
                MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2),
                MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2));
        }

        /// <summary>
        ///     Creates a new <see cref="Vector3" /> that contains CatmullRom interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector in interpolation.</param>
        /// <param name="value2">The second vector in interpolation.</param>
        /// <param name="value3">The third vector in interpolation.</param>
        /// <param name="value4">The fourth vector in interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The result of CatmullRom interpolation.</returns>
        public static Vector3 CatmullRom(Vector3 value1, Vector3 value2, Vector3 value3, Vector3 value4, float amount)
        {
            return new Vector3(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount),
                MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount));
        }

        /// <summary>
        ///     Clamps the specified value within a range.
        /// </summary>
        /// <param name="value1">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The clamped value.</returns>
        public static Vector3 Clamp(Vector3 value1, Vector3 min, Vector3 max)
        {
            return new Vector3(
                MathHelper.Clamp(value1.X, min.X, max.X),
                MathHelper.Clamp(value1.Y, min.Y, max.Y),
                MathHelper.Clamp(value1.Z, min.Z, max.Z));
        }


        /// <summary>
        ///     Computes the cross product of two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The cross product of two vectors.</returns>
        public static Vector3 Cross(Vector3 vector1, Vector3 vector2)
        {
            return new Vector3(
                vector1.Y*vector2.Z - vector2.Y*vector1.Z,
                -(vector1.X*vector2.Z - vector2.X*vector1.Z),
                vector1.X*vector2.Y - vector2.X*vector1.Y);
        }

        /// <summary>
        ///     Returns the distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The distance between two vectors.</returns>
        public static float Distance(Vector3 value1, Vector3 value2)
        {
            return (float) Math.Sqrt(DistanceSquared(value1, value2));
        }

        /// <summary>
        ///     Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The squared distance between two vectors.</returns>
        public static float DistanceSquared(Vector3 value1, Vector3 value2)
        {
            return (value1.X - value2.X)*(value1.X - value2.X) +
                   (value1.Y - value2.Y)*(value1.Y - value2.Y) +
                   (value1.Z - value2.Z)*(value1.Z - value2.Z);
        }

        /// <summary>
        ///     Returns a dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The dot product of two vectors.</returns>
        public static float Dot(Vector3 value1, Vector3 value2)
        {
            return value1.X*value2.X + value1.Y*value2.Y + value1.Z*value2.Z;
        }

        /// <summary>
        ///     Creates a new <see cref="Vector3" /> that contains hermite spline interpolation.
        /// </summary>
        /// <param name="value1">The first position vector.</param>
        /// <param name="tangent1">The first tangent vector.</param>
        /// <param name="value2">The second position vector.</param>
        /// <param name="tangent2">The second tangent vector.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The hermite spline interpolation vector.</returns>
        public static Vector3 Hermite(Vector3 value1, Vector3 tangent1, Vector3 value2, Vector3 tangent2, float amount)
        {
            return new Vector3(MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount),
                MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount),
                MathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount));
        }

        /// <summary>
        ///     Creates a new <see cref="Vector3" /> that contains linear interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <returns>The result of linear interpolation of the specified vectors.</returns>
        public static Vector3 Lerp(Vector3 value1, Vector3 value2, float amount)
        {
            return new Vector3(
                MathHelper.Lerp(value1.X, value2.X, amount),
                MathHelper.Lerp(value1.Y, value2.Y, amount),
                MathHelper.Lerp(value1.Z, value2.Z, amount));
        }

        /// <summary>
        ///     Creates a new <see cref="Vector3" /> that contains a maximal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The <see cref="Vector3" /> with maximal values from the two vectors.</returns>
        public static Vector3 Max(Vector3 value1, Vector3 value2)
        {
            return new Vector3(
                MathHelper.Max(value1.X, value2.X),
                MathHelper.Max(value1.Y, value2.Y),
                MathHelper.Max(value1.Z, value2.Z));
        }

        /// <summary>
        ///     Creates a new <see cref="Vector3" /> that contains a minimal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The <see cref="Vector3" /> with minimal values from the two vectors.</returns>
        public static Vector3 Min(Vector3 value1, Vector3 value2)
        {
            return new Vector3(
                MathHelper.Min(value1.X, value2.X),
                MathHelper.Min(value1.Y, value2.Y),
                MathHelper.Min(value1.Z, value2.Z));
        }

        /// <summary>
        ///     Creates a new <see cref="Vector3" /> that contains a normalized values from another vector.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3" />.</param>
        /// <returns>Unit vector.</returns>
        public static Vector3 Normalize(Vector3 value)
        {
            var factor = 1f/value.Length;
            return new Vector3(value.X*factor, value.Y*factor, value.Z*factor);
        }

        /// <summary>
        ///     Creates a new <see cref="Vector3" /> that contains reflect vector of the given vector and normal.
        /// </summary>
        /// <param name="vector">Source <see cref="Vector3" />.</param>
        /// <param name="normal">Reflection normal.</param>
        /// <returns>Reflected vector.</returns>
        public static Vector3 Reflect(Vector3 vector, Vector3 normal)
        {
            var dot = vector.X*normal.X + vector.Y*normal.Y + vector.Z*normal.Z;

            return new Vector3(
                vector.X - 2.0f*normal.X*dot,
                vector.Y - 2.0f*normal.Y*dot,
                vector.Z - 2.0f*normal.Z*dot
            );
        }

        /// <summary>
        ///     Creates a new <see cref="Vector3" /> that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector3" />.</param>
        /// <param name="value2">Source <see cref="Vector3" />.</param>
        /// <param name="amount">Weighting value.</param>
        /// <returns>Cubic interpolation of the specified vectors.</returns>
        public static Vector3 SmoothStep(Vector3 value1, Vector3 value2, float amount)
        {
            return new Vector3(
                MathHelper.SmoothStep(value1.X, value2.X, amount),
                MathHelper.SmoothStep(value1.Y, value2.Y, amount),
                MathHelper.SmoothStep(value1.Z, value2.Z, amount));
        }

        /// <summary>
        ///     Creates a new <see cref="Vector3" /> that contains a transformation of 3d-vector by the specified
        ///     <see cref="Matrix" />.
        /// </summary>
        /// <param name="position">Source <see cref="Vector3" />.</param>
        /// <param name="matrix">The transformation <see cref="Matrix" />.</param>
        /// <returns>Transformed <see cref="Vector3" />.</returns>
        public static Vector3 Transform(Vector3 position, Matrix matrix)
        {
            return new Vector3(
                position.X*matrix.M11 + position.Y*matrix.M21 + position.Z*matrix.M31 + matrix.M41,
                position.X*matrix.M12 + position.Y*matrix.M22 + position.Z*matrix.M32 + matrix.M42,
                position.X*matrix.M13 + position.Y*matrix.M23 + position.Z*matrix.M33 + matrix.M43);
        }

        /// <summary>
        ///     Creates a new <see cref="Vector3" /> that contains a transformation of 3d-vector by the specified
        ///     <see cref="Quaternion" />, representing the rotation.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3" />.</param>
        /// <param name="rotation">The <see cref="Quaternion" /> which contains rotation transformation.</param>
        /// <returns>Transformed <see cref="Vector3" />.</returns>
        public static Vector3 Transform(Vector3 value, Quaternion rotation)
        {
            var x = 2*(rotation.Y*value.Z - rotation.Z*value.Y);
            var y = 2*(rotation.Z*value.X - rotation.X*value.Z);
            var z = 2*(rotation.X*value.Y - rotation.Y*value.X);

            return new Vector3(
                value.X + x*rotation.W + (rotation.Y*z - rotation.Z*y),
                value.Y + y*rotation.W + (rotation.Z*x - rotation.X*z),
                value.Z + z*rotation.W + (rotation.X*y - rotation.Y*x));
        }

        /// <summary>
        ///     Creates a new <see cref="Vector3" /> that contains a transformation of the specified normal by the specified
        ///     <see cref="Matrix" />.
        /// </summary>
        /// <param name="normal">Source <see cref="Vector3" /> which represents a normal vector.</param>
        /// <param name="matrix">The transformation <see cref="Matrix" />.</param>
        /// <returns>Transformed normal.</returns>
        public static Vector3 TransformNormal(Vector3 normal, Matrix matrix)
        {
            return new Vector3(
                normal.X*matrix.M11 + normal.Y*matrix.M21 + normal.Z*matrix.M31,
                normal.X*matrix.M12 + normal.Y*matrix.M22 + normal.Z*matrix.M32,
                normal.X*matrix.M13 + normal.Y*matrix.M23 + normal.Z*matrix.M33);
        }


        /// <summary>
        ///     Adds the left <see cref="Vector3" />'s components to the right <see cref="Vector3" />'s components and stores it in
        ///     a
        ///     new <see cref="Vector3" />.
        /// </summary>
        /// <param name="left">A <see cref="Vector3" />.</param>
        /// <param name="right">A <see cref="Vector3" />.</param>
        /// <returns>A new <see cref="Vector3" />.</returns>
        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        /// <summary>
        ///     Subtracts the right <see cref="Vector3" />'s components from the left <see cref="Vector3" />'s components and
        ///     stores
        ///     it in a new <see cref="Vector3" />.
        /// </summary>
        /// <param name="left">A <see cref="Vector3" />.</param>
        /// <param name="right">A <see cref="Vector3" />.</param>
        /// <returns>A new <see cref="Vector3" />.</returns>
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        /// <summary>
        ///     Creates a <see cref="Vector3" /> with the components set to the negative values of the given
        ///     <paramref name="vector" />'s components.
        /// </summary>
        /// <param name="vector">The vector to invert.</param>
        /// <returns>The new <see cref="Vector3" />.</returns>
        public static Vector3 operator -(Vector3 vector)
        {
            return new Vector3(-vector.X, -vector.Y, -vector.Z);
        }

        /// <summary>
        ///     Multiplies the components <see cref="Vector3" /> by the given scalar and stores them in a new
        ///     <see cref="Vector3" />.
        /// </summary>
        /// <param name="vector">The <see cref="Vector3" />.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>The new <see cref="Vector3" />.</returns>
        public static Vector3 operator *(Vector3 vector, float scalar)
        {
            return new Vector3(vector.X*scalar, vector.Y*scalar, vector.Z*scalar);
        }

        /// <summary>
        ///     Multiplies the components <see cref="Vector3" /> by the components of the right <see cref="Vector3" /> and stores
        ///     them in a new <see cref="Vector3" />.
        /// </summary>
        /// <param name="left">A <see cref="Vector3" />.</param>
        /// <param name="right">A <see cref="Vector3" />.</param>
        /// <returns>The new <see cref="Vector3" />.</returns>
        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X*right.X, left.Y*right.Y, left.Z*right.Z);
        }

        /// <summary>
        ///     Divides the components <see cref="Vector3" /> by the given scalar and stores them in a new <see cref="Vector3" />.
        /// </summary>
        /// <param name="vector">The <see cref="Vector3" />.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>The new <see cref="Vector3" />.</returns>
        public static Vector3 operator /(Vector3 vector, float scalar)
        {
            return new Vector3(vector.X/scalar, vector.Y/scalar, vector.Z/scalar);
        }

        /// <summary>
        ///     Divides the components <see cref="Vector3" /> by the components of the right <see cref="Vector3" /> and stores them
        ///     in a new <see cref="Vector3" />.
        /// </summary>
        /// <param name="left">The numerator.</param>
        /// <param name="right">The denominator.</param>
        /// <returns>The new <see cref="Vector3" />.</returns>
        public static Vector3 operator /(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X/right.X, left.Y/right.Y, left.Z/right.Z);
        }

        /// <summary>
        ///     Tests whether all components of both <see cref="Vector3" /> are equivalent.
        /// </summary>
        /// <param name="left">Instance of <see cref="Vector3" /> to compare.</param>
        /// <param name="right">Instance of <see cref="Vector3" /> to compare.</param>
        /// <returns>true if all components of both <see cref="Vector3" /> are equivalent; otherwise, false.</returns>
        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Tests whether any component of both <see cref="Vector3" /> are not equivalent.
        /// </summary>
        /// <param name="left">Instance of <see cref="Vector3" /> to compare.</param>
        /// <param name="right">Instance of <see cref="Vector3" /> to compare.</param>
        /// <returns>true if any component of both <see cref="Vector3" /> are not equivalent; otherwise, false.</returns>
        public static bool operator !=(Vector3 left, Vector3 right)
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
        public bool Equals(Vector3 obj)
        {
            return X.Equals(obj.X) && Y.Equals(obj.Y) && Z.Equals(obj.Z);
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
            return obj is Vector3 && Equals((Vector3) obj);
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
            return $"({X}, {Y}, {Z})";
        }
    }
}