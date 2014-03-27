﻿// SampSharp
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

namespace GameMode.World
{
    public struct Rotation
    {
        public Rotation(float x, float y, float z) : this()
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Rotation(float xyz) : this()
        {
            X = xyz;
            Y = xyz;
            Z = xyz;
        }

        public Rotation(Rotation vector) : this()
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Rotation Clone()
        {
            return new Rotation(this);
        }

        public bool Equals(Rotation other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Rotation && Equals((Rotation) obj);
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
    }
}