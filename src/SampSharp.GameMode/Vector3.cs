// SampSharp
// Copyright 2015 Tim Potze
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
using System.Xml.Linq;

namespace SampSharp.GameMode
{
    /// <summary>
    ///     Represents a 3D vector.
    /// </summary>
    public struct Vector3
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
        ///     Initializes a new instance of the <see cref="Vector3" /> struct with the z component set to 0.
        /// </summary>
        /// <param name="x">Value of the x component.</param>
        /// <param name="y">Value of the y component.</param>
        public Vector3(float x, float y)
            : this(x, y, 0)
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
        ///     Initializes a new instance of the <see cref="Vector3" /> struct with same values for x, y and z components.
        /// </summary>
        /// <param name="xyz">Value of x, y and z components.</param>
        public Vector3(float xyz)
            : this(xyz, xyz, xyz)
        {
        }

        /// <summary>
        ///     Gets or sets the X component of this <see cref="Vector3" />.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        ///     Gets or sets the Y component of this <see cref="Vector3" />.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        ///     Gets or sets the Z component of this <see cref="Vector3" />.
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        ///     Gets an empty <see cref="Vector3" />.
        /// </summary>
        public static Vector3 Zero
        {
            get { return new Vector3(0); }
        }

        /// <summary>
        ///     Gets a <see cref="Vector3" /> with each component set to 1.
        /// </summary>
        public static Vector3 One
        {
            get { return new Vector3(1); }
        }

        /// <summary>
        ///     Gets the length of this <see cref="Vector3" />.
        /// </summary>
        public float Length
        {
            get { return DistanceTo(Zero); }
        }

        /// <summary>
        ///     Gets whether this <see cref="Vector3" /> is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return X == 0 && Y == 0 && Z == 0; }
        }

        /// <summary>
        ///     Adds all components of a different <see cref="Vector3" /> to this <see cref="Vector3" />.
        /// </summary>
        /// <param name="other">The <see cref="Vector3" /> to add to this <see cref="Vector3" />.</param>
        public void Add(Vector3 other)
        {
            X += other.X;
            Y += other.Y;
            Z += other.Z;
        }

        /// <summary>
        ///     Gets the distance to another <see cref="Vector3" />.
        /// </summary>
        /// <param name="other">The <see cref="Vector3" /> to calculate the distance to.</param>
        /// <returns>The distance between the vectors.</returns>
        public float DistanceTo(Vector3 other)
        {
            float dx = X - other.X;
            float dy = Y - other.Y;
            float dz = Z - other.Z;
            return (float) Math.Sqrt(dx*dx + dy*dy + dz*dz);
        }

        /// <summary>
        ///     Normalizes this <see cref="Vector3" /> to a single unit.
        /// </summary>
        public void Normalize()
        {
            float length = Length;
            X /= length;
            Y /= length;
            Z /= length;
        }

        /// <summary>
        ///     Adds the left <see cref="Vector3" />'s components to the right <see cref="Vector3" />'s components and stores it in a
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
        ///     Subtracts the right <see cref="Vector3" />'s components from the left <see cref="Vector3" />'s components and stores
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
        ///     Multiplies the components <see cref="Vector3" /> by the given scalar and stores them in a new <see cref="Vector3" />.
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

        /// <summary>
        ///     Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="other">Another object to compare to.</param>
        /// <returns>
        ///     true if <paramref name="other" /> and this instance are the same type and represent the same value; otherwise,
        ///     false.
        /// </returns>
        public bool Equals(Vector3 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        /// <summary>
        ///     Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">Another object to compare to. </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector3 && Equals((Vector3) obj);
        }

        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = X.GetHashCode();
                hashCode = (hashCode*397) ^ Y.GetHashCode();
                hashCode = (hashCode*397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        ///     Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String" /> containing a fully qualified type name.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return "(" + X + ", " + Y + ", " + Z + ")";
        }
    }
}