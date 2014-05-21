// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System;

namespace SampSharp.GameMode.World
{
    public struct Vector
    {
        #region Constructors

        public Vector(float x, float y, float z) : this()
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector(float x, float y)
            : this(x, y, 0)
        {
        }

        public Vector(double x, double y, double z)
            : this((float) x, (float) y, (float) z)
        {
        }

        public Vector(double x, double y)
            : this(x, y, 0)
        {
        }

        public Vector(float xyz)
            : this(xyz, xyz, xyz)
        {
        }

        public Vector(double xyz)
            : this((float) xyz)
        {
        }

        public Vector(Vector vector)
            : this(vector.X, vector.Y, vector.Z)
        {
        }

        #endregion

        #region Properties

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        #endregion

        public static Vector Zero
        {
            get { return new Vector(0); }
        }

        public static Vector One
        {
            get { return new Vector(1); }
        }

        public static Vector CrossProduct(Vector a, Vector b)
        {
            return new Vector(a.Y*b.Z - a.Z*b.Y, a.Z*b.X - a.X*b.Z, a.X*b.Y - a.Y*b.X);
        }

        #region Methods

        public float DotProduct(Vector other)
        {
            return X*other.X + Y*other.Y + Z*other.Z;
        }

        public Vector Add(Vector v)
        {
            X += v.X;
            Y += v.Y;
            Z += v.Z;
            return this;
        }

        public float DistanceTo(Vector v)
        {
            float dx = X - v.X;
            float dy = Y - v.Y;
            float dz = Z - v.Z;
            return (float) Math.Sqrt(dx*dx + dy*dy + dz*dz);
        }

        public float Size()
        {
            return DistanceTo(Zero);
        }

        public Vector Normalize()
        {
            float size = Size();
            X /= size;
            Y /= size;
            Z /= size;
            return this;
        }

        #endregion

        #region Operators

        public static Vector operator +(Vector left, Vector right)
        {
            return new Vector(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Vector operator -(Vector left, Vector right)
        {
            return new Vector(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Vector operator -(Vector vector)
        {
            return new Vector(-vector.X, -vector.Y, -vector.Z);
        }

        public static Vector operator *(Vector vector, float scalar)
        {
            return new Vector(vector.X*scalar, vector.Y*scalar, vector.Z*scalar);
        }

        public static Vector operator /(Vector vector, float scalar)
        {
            return new Vector(vector.X/scalar, vector.Y/scalar, vector.Z/scalar);
        }

        public static bool operator ==(Vector left, Vector right)
        {
            try
            {
                return left.Equals(right);
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        public static bool operator !=(Vector left, Vector right)
        {
            try
            {
                return !left.Equals(right);
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        #endregion

        #region Object releated methods

        public Vector Clone()
        {
            return new Vector(this);
        }

        public bool Equals(Vector other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector && Equals((Vector) obj);
        }

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

        public override string ToString()
        {
            return "(" + X + ", " + Y + ", " + Z + ")";
        }

        #endregion
    }
}