using System;

namespace SampSharp.GameMode
{
    /// <summary>
    ///     Represents a 2D vector.
    /// </summary>
    public struct Vector2
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector2" /> struct with the z component set to 0.
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
        ///     Initializes a new instance of the <see cref="Vector2" /> struct with same values for x and y components.
        /// </summary>
        /// <param name="xy">Value of x and y components.</param>
        public Vector2(float xy)
            : this(xy, xy)
        {
        }

        /// <summary>
        ///     Gets or sets the X component of this <see cref="Vector2" />.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        ///     Gets or sets the Y component of this <see cref="Vector2" />.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        ///     Gets an empty <see cref="Vector2" />.
        /// </summary>
        public static Vector2 Zero
        {
            get { return new Vector2(0); }
        }

        /// <summary>
        ///     Gets a <see cref="Vector2" /> with each component set to 1.
        /// </summary>
        public static Vector2 One
        {
            get { return new Vector2(1); }
        }

        /// <summary>
        ///     Gets the length of this <see cref="Vector2" />.
        /// </summary>
        public float Length
        {
            get { return DistanceTo(Zero); }
        }

        /// <summary>
        ///     Gets whether this <see cref="Vector2" /> is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return X == 0 && Y == 0; }
        }

        /// <summary>
        ///     Adds all components of a different <see cref="Vector2" /> to this <see cref="Vector2" />.
        /// </summary>
        /// <param name="other">The <see cref="Vector2" /> to add to this <see cref="Vector2" />.</param>
        public void Add(Vector2 other)
        {
            X += other.X;
            Y += other.Y;
        }

        /// <summary>
        ///     Gets the distance to another <see cref="Vector2" />.
        /// </summary>
        /// <param name="other">The <see cref="Vector2" /> to calculate the distance to.</param>
        /// <returns>The distance between the vectors.</returns>
        public float DistanceTo(Vector2 other)
        {
            float dx = X - other.X;
            float dy = Y - other.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        ///     Normalizes this <see cref="Vector2" /> to a single unit.
        /// </summary>
        public void Normalize()
        {
            float length = Length;
            X /= length;
            Y /= length;
        }

        /// <summary>
        ///     Adds the left <see cref="Vector2" />'s components to the right <see cref="Vector2" />'s components and stores it in a
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
        ///     Subtracts the right <see cref="Vector2" />'s components from the left <see cref="Vector2" />'s components and stores
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
        ///     Multiplies the components <see cref="Vector2" /> by the given scalar and stores them in a new <see cref="Vector2" />.
        /// </summary>
        /// <param name="vector">The <see cref="Vector2" />.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>The new <see cref="Vector2" />.</returns>
        public static Vector2 operator *(Vector2 vector, float scalar)
        {
            return new Vector2(vector.X * scalar, vector.Y * scalar);
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
            return new Vector2(left.X * right.X, left.Y * right.Y);
        }

        /// <summary>
        ///     Divides the components <see cref="Vector2" /> by the given scalar and stores them in a new <see cref="Vector2" />.
        /// </summary>
        /// <param name="vector">The <see cref="Vector2" />.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>The new <see cref="Vector2" />.</returns>
        public static Vector2 operator /(Vector2 vector, float scalar)
        {
            return new Vector2(vector.X / scalar, vector.Y / scalar);
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
            return new Vector2(left.X / right.X, left.Y / right.Y);
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

        /// <summary>
        ///     Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="other">Another object to compare to.</param>
        /// <returns>
        ///     true if <paramref name="other" /> and this instance are the same type and represent the same value; otherwise,
        ///     false.
        /// </returns>
        public bool Equals(Vector2 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
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
            return obj is Vector2 && Equals((Vector2)obj);
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
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
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
            return "(" + X + ", " + Y + ")";
        }
    }
}