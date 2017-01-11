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
    ///     Represents a 2D vector.
    /// </summary>
    public struct Vector2 : IEquatable<Vector2>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector2" /> struct.
        /// </summary>
        /// <param name="x">Value of the x component.</param>
        /// <param name="y">Value of the y component.</param>
        public Vector2(float x, float y)
            : this()
        {
            X = x;
            Y = y;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector2" /> struct.
        /// </summary>
        /// <param name="x">Value of the x component.</param>
        /// <param name="y">Value of the y component.</param>
        public Vector2(double x, double y)
            : this((float) x, (float) y)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector2" /> struct with same values for x and y components.
        /// </summary>
        /// <param name="xy">Value of x and y components.</param>
        public Vector2(float xy)
            : this(xy, xy)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector2" /> struct with same values for x and y components.
        /// </summary>
        /// <param name="xy">Value of x and y components.</param>
        public Vector2(double xy)
            : this((float) xy)
        {
        }

        /// <summary>
        ///     Gets the X component of this <see cref="Vector2" />.
        /// </summary>
        public float X { get; }

        /// <summary>
        ///     Gets the Y component of this <see cref="Vector2" />.
        /// </summary>
        public float Y { get; }

        /// <summary>
        ///     Returns an empty <see cref="Vector2" />.
        /// </summary>
        public static Vector2 Zero { get; } = new Vector2(0);

        /// <summary>
        ///     Returns a <see cref="Vector2" /> with each component set to 1.
        /// </summary>
        public static Vector2 One { get; } = new Vector2(1);

        /// <summary>
        ///     Returns a <see cref="Vector2" /> with components 1, 0.
        /// </summary>
        public static Vector2 UnitX { get; } = new Vector2(1, 0);

        /// <summary>
        ///     Returns a <see cref="Vector2" /> with components 0, 1.
        /// </summary>
        public static Vector2 UnitY { get; } = new Vector2(0, 1);

        /// <summary>
        ///     Gets the length of this <see cref="Vector2" />.
        /// </summary>
        public float Length => Distance(this, Zero);

        /// <summary>
        ///     Gets the squared length of this <see cref="Vector2" />.
        /// </summary>
        public float SquaredLength => DistanceSquared(this, Zero);

        /// <summary>
        ///     Gets whether this <see cref="Vector2" /> is empty.
        /// </summary>
        public bool IsEmpty => X.Equals(0.0f) && Y.Equals(0.0f);

        /// <summary>
        ///     Gets the distance to another <see cref="Vector2" />.
        /// </summary>
        /// <param name="other">The <see cref="Vector2" /> to calculate the distance to.</param>
        /// <returns>The distance between the vectors.</returns>
        public float DistanceTo(Vector2 other)
        {
            return Distance(this, other);
        }

        /// <summary>
        ///     Creates a new <see cref="Vector2" /> instance with the components normalized to a single unit.
        /// </summary>
        public Vector2 Normalized()
        {
            return Normalize(this);
        }

        /// <summary>
        ///     Creates a new <see cref="Vector2" /> that contains the cartesian coordinates of a vector specified in barycentric
        ///     coordinates and relative to 2d-triangle.
        /// </summary>
        /// <param name="value1">The first vector of 2d-triangle.</param>
        /// <param name="value2">The second vector of 2d-triangle.</param>
        /// <param name="value3">The third vector of 2d-triangle.</param>
        /// <param name="amount1">
        ///     Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of
        ///     2d-triangle.
        /// </param>
        /// <param name="amount2">
        ///     Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of
        ///     2d-triangle.
        /// </param>
        /// <returns>The cartesian translation of barycentric coordinates.</returns>
        public static Vector2 Barycentric(Vector2 value1, Vector2 value2, Vector2 value3, float amount1, float amount2)
        {
            return new Vector2(
                MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2));
        }

        /// <summary>
        ///     Creates a new <see cref="Vector2" /> that contains CatmullRom interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector in interpolation.</param>
        /// <param name="value2">The second vector in interpolation.</param>
        /// <param name="value3">The third vector in interpolation.</param>
        /// <param name="value4">The fourth vector in interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The result of CatmullRom interpolation.</returns>
        public static Vector2 CatmullRom(Vector2 value1, Vector2 value2, Vector2 value3, Vector2 value4, float amount)
        {
            return new Vector2(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount));
        }

        /// <summary>
        ///     Clamps the specified value within a range.
        /// </summary>
        /// <param name="value1">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The clamped value.</returns>
        public static Vector2 Clamp(Vector2 value1, Vector2 min, Vector2 max)
        {
            return new Vector2(
                MathHelper.Clamp(value1.X, min.X, max.X),
                MathHelper.Clamp(value1.Y, min.Y, max.Y));
        }

        /// <summary>
        ///     Returns the distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The distance between two vectors.</returns>
        public static float Distance(Vector2 value1, Vector2 value2)
        {
            float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            return (float) Math.Sqrt(v1*v1 + v2*v2);
        }

        /// <summary>
        ///     Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The squared distance between two vectors.</returns>
        public static float DistanceSquared(Vector2 value1, Vector2 value2)
        {
            float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            return v1*v1 + v2*v2;
        }

        /// <summary>
        ///     Returns a dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The dot product of two vectors.</returns>
        public static float Dot(Vector2 value1, Vector2 value2)
        {
            return value1.X*value2.X + value1.Y*value2.Y;
        }


        /// <summary>
        ///     Creates a new <see cref="Vector2" /> that contains hermite spline interpolation.
        /// </summary>
        /// <param name="value1">The first position vector.</param>
        /// <param name="tangent1">The first tangent vector.</param>
        /// <param name="value2">The second position vector.</param>
        /// <param name="tangent2">The second tangent vector.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The hermite spline interpolation vector.</returns>
        public static Vector2 Hermite(Vector2 value1, Vector2 tangent1, Vector2 value2, Vector2 tangent2, float amount)
        {
            return new Vector2(MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount),
                MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount));
        }

        /// <summary>
        ///     Creates a new <see cref="Vector2" /> that contains linear interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <returns>The result of linear interpolation of the specified vectors.</returns>
        public static Vector2 Lerp(Vector2 value1, Vector2 value2, float amount)
        {
            return new Vector2(
                MathHelper.Lerp(value1.X, value2.X, amount),
                MathHelper.Lerp(value1.Y, value2.Y, amount));
        }

        /// <summary>
        ///     Adds the left <see cref="Vector2" />'s components to the right <see cref="Vector2" />'s components and stores it in
        ///     a
        ///     new <see cref="Vector2" />.
        /// </summary>
        /// <param name="left">A <see cref="Vector2" />.</param>
        /// <param name="right">A <see cref="Vector2" />.</param>
        /// <returns>A new <see cref="Vector2" />.</returns>
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        /// <summary>
        ///     Creates a new <see cref="Vector2" /> that contains a maximal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The <see cref="Vector2" /> with maximal values from the two vectors.</returns>
        public static Vector2 Max(Vector2 value1, Vector2 value2)
        {
            return new Vector2(value1.X > value2.X ? value1.X : value2.X,
                value1.Y > value2.Y ? value1.Y : value2.Y);
        }

        /// <summary>
        ///     Creates a new <see cref="Vector2" /> that contains a minimal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The <see cref="Vector2" /> with minimal values from the two vectors.</returns>
        public static Vector2 Min(Vector2 value1, Vector2 value2)
        {
            return new Vector2(value1.X < value2.X ? value1.X : value2.X,
                value1.Y < value2.Y ? value1.Y : value2.Y);
        }

        /// <summary>
        ///     Creates a new <see cref="Vector2" /> that contains a normalized values from another vector.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2" />.</param>
        /// <returns>Unit vector.</returns>
        public static Vector2 Normalize(Vector2 value)
        {
            var val = 1.0f/(float) Math.Sqrt(value.X*value.X + value.Y*value.Y);
            return new Vector2(value.X*val, value.Y*val);
        }

        /// <summary>
        ///     Creates a new <see cref="Vector2" /> that contains reflect vector of the given vector and normal.
        /// </summary>
        /// <param name="vector">Source <see cref="Vector2" />.</param>
        /// <param name="normal">Reflection normal.</param>
        /// <returns>Reflected vector.</returns>
        public static Vector2 Reflect(Vector2 vector, Vector2 normal)
        {
            var val = 2.0f*(vector.X*normal.X + vector.Y*normal.Y);
            return new Vector2(vector.X - normal.X*val, vector.Y - normal.Y*val);
        }

        /// <summary>
        ///     Creates a new <see cref="Vector2" /> that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2" />.</param>
        /// <param name="value2">Source <see cref="Vector2" />.</param>
        /// <param name="amount">Weighting value.</param>
        /// <returns>Cubic interpolation of the specified vectors.</returns>
        public static Vector2 SmoothStep(Vector2 value1, Vector2 value2, float amount)
        {
            return new Vector2(
                MathHelper.SmoothStep(value1.X, value2.X, amount),
                MathHelper.SmoothStep(value1.Y, value2.Y, amount));
        }

        /// <summary>
        ///     Creates a new <see cref="Vector2" /> that contains a transformation of 2d-vector by the specified
        ///     <see cref="Matrix" />.
        /// </summary>
        /// <param name="position">Source <see cref="Vector2" />.</param>
        /// <param name="matrix">The transformation <see cref="Matrix" />.</param>
        /// <returns>Transformed <see cref="Vector2" />.</returns>
        public static Vector2 Transform(Vector2 position, Matrix matrix)
        {
            return new Vector2(position.X*matrix.M11 + position.Y*matrix.M21 + matrix.M41,
                position.X*matrix.M12 + position.Y*matrix.M22 + matrix.M42);
        }

        /// <summary>
        ///     Creates a new <see cref="Vector2" /> that contains a transformation of 2d-vector by the specified
        ///     <see cref="Quaternion" />, representing the rotation.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2" />.</param>
        /// <param name="rotation">The <see cref="Quaternion" /> which contains rotation transformation.</param>
        /// <returns>Transformed <see cref="Vector2" />.</returns>
        public static Vector2 Transform(Vector2 value, Quaternion rotation)
        {
            var rot1 = new Vector3(rotation.X + rotation.X, rotation.Y + rotation.Y, rotation.Z + rotation.Z);
            var rot2 = new Vector3(rotation.X, rotation.X, rotation.W);
            var rot3 = new Vector3(1, rotation.Y, rotation.Z);
            var rot4 = rot1*rot2;
            var rot5 = rot1*rot3;

            return new Vector2(
                (float) (value.X*(1.0 - rot5.Y - rot5.Z) + value.Y*((double) rot4.Y - rot4.Z)),
                (float) (value.X*((double) rot4.Y + rot4.Z) + value.Y*(1.0 - rot4.X - rot5.Z))
            );
        }

        /// <summary>
        ///     Creates a new <see cref="Vector2" /> that contains a transformation of the specified normal by the specified
        ///     <see cref="Matrix" />.
        /// </summary>
        /// <param name="normal">Source <see cref="Vector2" /> which represents a normal vector.</param>
        /// <param name="matrix">The transformation <see cref="Matrix" />.</param>
        /// <returns>Transformed normal.</returns>
        public static Vector2 TransformNormal(Vector2 normal, Matrix matrix)
        {
            return new Vector2(normal.X*matrix.M11 + normal.Y*matrix.M21, normal.X*matrix.M12 + normal.Y*matrix.M22);
        }

        /// <summary>
        ///     Subtracts the right <see cref="Vector2" />'s components from the left <see cref="Vector2" />'s components and
        ///     stores
        ///     it in a new <see cref="Vector2" />.
        /// </summary>
        /// <param name="left">A <see cref="Vector2" />.</param>
        /// <param name="right">A <see cref="Vector2" />.</param>
        /// <returns>A new <see cref="Vector2" />.</returns>
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }

        /// <summary>
        ///     Creates a <see cref="Vector2" /> with the components set to the negative values of the given
        ///     <paramref name="vector" />'s components.
        /// </summary>
        /// <param name="vector">The vector to invert.</param>
        /// <returns>The new <see cref="Vector2" />.</returns>
        public static Vector2 operator -(Vector2 vector)
        {
            return new Vector2(-vector.X, -vector.Y);
        }

        /// <summary>
        ///     Multiplies the components <see cref="Vector2" /> by the given scalar and stores them in a new
        ///     <see cref="Vector2" />.
        /// </summary>
        /// <param name="scalar">The scalar.</param>
        /// <param name="vector">The <see cref="Vector2" />.</param>
        /// <returns>The new <see cref="Vector2" />.</returns>
        public static Vector2 operator *(float scalar, Vector2 vector)
        {
            return new Vector2(vector.X*scalar, vector.Y*scalar);
        }

        /// <summary>
        ///     Multiplies the components <see cref="Vector2" /> by the given scalar and stores them in a new
        ///     <see cref="Vector2" />.
        /// </summary>
        /// <param name="vector">The <see cref="Vector2" />.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>The new <see cref="Vector2" />.</returns>
        public static Vector2 operator *(Vector2 vector, float scalar)
        {
            return new Vector2(vector.X*scalar, vector.Y*scalar);
        }

        /// <summary>
        ///     Multiplies the components <see cref="Vector2" /> by the components of the right <see cref="Vector2" /> and stores
        ///     them in a new <see cref="Vector2" />.
        /// </summary>
        /// <param name="left">A <see cref="Vector2" />.</param>
        /// <param name="right">A <see cref="Vector2" />.</param>
        /// <returns>The new <see cref="Vector2" />.</returns>
        public static Vector2 operator *(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X*right.X, left.Y*right.Y);
        }

        /// <summary>
        ///     Divides the components <see cref="Vector2" /> by the given scalar and stores them in a new <see cref="Vector2" />.
        /// </summary>
        /// <param name="vector">The <see cref="Vector2" />.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>The new <see cref="Vector2" />.</returns>
        public static Vector2 operator /(Vector2 vector, float scalar)
        {
            return new Vector2(vector.X/scalar, vector.Y/scalar);
        }

        /// <summary>
        ///     Divides the components <see cref="Vector2" /> by the components of the right <see cref="Vector2" /> and stores them
        ///     in a new <see cref="Vector2" />.
        /// </summary>
        /// <param name="left">The numerator.</param>
        /// <param name="right">The denominator.</param>
        /// <returns>The new <see cref="Vector2" />.</returns>
        public static Vector2 operator /(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X/right.X, left.Y/right.Y);
        }

        /// <summary>
        ///     Tests whether all components of both <see cref="Vector2" /> are equivalent.
        /// </summary>
        /// <param name="left">Instance of <see cref="Vector2" /> to compare.</param>
        /// <param name="right">Instance of <see cref="Vector2" /> to compare.</param>
        /// <returns>true if all components of both <see cref="Vector2" /> are equivalent; otherwise, false.</returns>
        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Tests whether any component of both <see cref="Vector2" /> are not equivalent.
        /// </summary>
        /// <param name="left">Instance of <see cref="Vector2" /> to compare.</param>
        /// <param name="right">Instance of <see cref="Vector2" /> to compare.</param>
        /// <returns>true if any component of both <see cref="Vector2" /> are not equivalent; otherwise, false.</returns>
        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return !left.Equals(right);
        }

        #region Equality members

        /// <summary>
        ///     Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current instance. </param>
        public bool Equals(Vector2 obj)
        {
            return X.Equals(obj.X) && Y.Equals(obj.Y);
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
            return obj is Vector2 && Equals((Vector2) obj);
        }

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
                return (X.GetHashCode()*397) ^ Y.GetHashCode();
            }
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
            return $"({X}, {Y})";
        }
    }
}