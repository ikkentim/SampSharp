using System;

namespace GameMode.World
{
    public class Rotation
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Rotation()
        {
            X = 0.0f;
            Y = 0.0f;
            Z = 0.0f;
        }

        public Rotation(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Rotation(float xyz)
        {
            X = xyz;
            Y = xyz;
            Z = xyz;
        }

        public Rotation(Rotation vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }

        public Rotation Clone()
        {
            return new Rotation(this);
        }

        protected bool Equals(Rotation other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public static bool operator ==(Rotation left, Rotation right)
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

        public static bool operator !=(Rotation left, Rotation right)
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Rotation)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ", " + Z + ")";
        }
    }
}
