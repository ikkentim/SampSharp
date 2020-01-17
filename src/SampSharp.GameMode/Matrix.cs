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
    ///     Represents the right-handed 4x4 floating point matrix, which can store translation, scale and rotation information.
    /// </summary>
    public struct Matrix : IEquatable<Matrix>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Matrix" /> struct.
        /// </summary>
        /// <param name="m11">The first row and first column value.</param>
        /// <param name="m12">The first row and second column value.</param>
        /// <param name="m13">The first row and third column value.</param>
        /// <param name="m14">The first row and fourth column value.</param>
        /// <param name="m21">The second row and first column value.</param>
        /// <param name="m22">The second row and second column value.</param>
        /// <param name="m23">The second row and third column value.</param>
        /// <param name="m24">The second row and fourth column value.</param>
        /// <param name="m31">The third row and first column value.</param>
        /// <param name="m32">The third row and second column value.</param>
        /// <param name="m33">The third row and third column value.</param>
        /// <param name="m34">The third row and fourth column value.</param>
        /// <param name="m41">The fourth row and first column value.</param>
        /// <param name="m42">The fourth row and second column value.</param>
        /// <param name="m43">The fourth row and third column value.</param>
        /// <param name="m44">The fourth row and fourth column value.</param>
        public Matrix(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31,
            float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M14 = m14;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M24 = m24;
            M31 = m31;
            M32 = m32;
            M33 = m33;
            M34 = m34;
            M41 = m41;
            M42 = m42;
            M43 = m43;
            M44 = m44;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Matrix" /> struct.
        /// </summary>
        /// <param name="row1">The first row of the matrix.</param>
        /// <param name="row2">The second row of the matrix.</param>
        /// <param name="row3">The third row of the matrix.</param>
        /// <param name="row4">The fourth row of the matrix.</param>
        public Matrix(Vector4 row1, Vector4 row2, Vector4 row3, Vector4 row4)
        {
            M11 = row1.X;
            M12 = row1.Y;
            M13 = row1.Z;
            M14 = row1.W;
            M21 = row2.X;
            M22 = row2.Y;
            M23 = row2.Z;
            M24 = row2.W;
            M31 = row3.X;
            M32 = row3.Y;
            M33 = row3.Z;
            M34 = row3.W;
            M41 = row4.X;
            M42 = row4.Y;
            M43 = row4.Z;
            M44 = row4.W;
        }

        /// <summary>
        ///     Gets the first row and first column value.
        /// </summary>
        public float M11 { get; }

        /// <summary>
        ///     Gets the first row and second column value.
        /// </summary>
        public float M12 { get; }

        /// <summary>
        ///     Gets the first row and third column value.
        /// </summary>
        public float M13 { get; }

        /// <summary>
        ///     Gets the first row and fourth column value.
        /// </summary>
        public float M14 { get; }

        /// <summary>
        ///     Gets the second row and first column value.
        /// </summary>
        public float M21 { get; }

        /// <summary>
        ///     Gets the second row and second column value.
        /// </summary>
        public float M22 { get; }

        /// <summary>
        ///     Gets the second row and third column value.
        /// </summary>
        public float M23 { get; }

        /// <summary>
        ///     Gets the second row and fourth column value.
        /// </summary>
        public float M24 { get; }

        /// <summary>
        ///     Gets the third row and first column value.
        /// </summary>
        public float M31 { get; }

        /// <summary>
        ///     Gets the third row and second column value.
        /// </summary>
        public float M32 { get; }

        /// <summary>
        ///     Gets the third row and third column value.
        /// </summary>
        public float M33 { get; }

        /// <summary>
        ///     Gets the third row and fourth column value.
        /// </summary>
        public float M34 { get; }

        /// <summary>
        ///     Gets the fourth row and first column value.
        /// </summary>
        public float M41 { get; }

        /// <summary>
        ///     Gets the fourth row and second column value.
        /// </summary>
        public float M42 { get; }

        /// <summary>
        ///     Gets the fourth row and third column value.
        /// </summary>
        public float M43 { get; }

        /// <summary>
        ///     Gets the fourth row and fourth column value.
        /// </summary>
        public float M44 { get; }

        /// <summary>
        ///     Gets the value at the specified index.
        /// </summary>
        /// <param name="index">The index of the value to get.</param>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return M11;
                    case 1:
                        return M12;
                    case 2:
                        return M13;
                    case 3:
                        return M14;
                    case 4:
                        return M21;
                    case 5:
                        return M22;
                    case 6:
                        return M23;
                    case 7:
                        return M24;
                    case 8:
                        return M31;
                    case 9:
                        return M32;
                    case 10:
                        return M33;
                    case 11:
                        return M34;
                    case 12:
                        return M41;
                    case 13:
                        return M42;
                    case 14:
                        return M43;
                    case 15:
                        return M44;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        ///     Gets the value at the specified column and row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        public float this[int row, int column] => this[row*4 + column];


        /// <summary>
        ///     Returns the identity matrix.
        /// </summary>
        public static Matrix Identity { get; } = new Matrix(1f, 0f, 0f, 0f,
            0f, 1f, 0f, 0f,
            0f, 0f, 1f, 0f,
            0f, 0f, 0f, 1f);


        /// <summary>
        ///     Gets the down vector formed from the third row -M31, -M32, -M33 elements.
        /// </summary>
        public Vector3 Down => new Vector3(-M31, -M32, -M33);

        /// <summary>
        ///     Gets the upper vector formed from the third row M31, M32, M33 elements.
        /// </summary>
        public Vector3 Up => new Vector3(M31, M32, M33);

        /// <summary>
        ///     Gets the forward vector formed from the second row M21, M22, M23 elements.
        /// </summary>
        public Vector3 Forward => new Vector3(M21, M22, M23);

        /// <summary>
        ///     Gets the backward vector formed from the second row -M21, -M22, -M23 elements.
        /// </summary>
        public Vector3 Backward => new Vector3(-M21, -M22, -M23);

        /// <summary>
        ///     The left vector formed from the first row -M11, -M12, -M13 elements.
        /// </summary>
        public Vector3 Left => new Vector3(-M11, -M12, -M13);

        /// <summary>
        ///     Gets the right vector formed from the first row M11, M12, M13 elements.
        /// </summary>
        public Vector3 Right => new Vector3(M11, M12, M13);

        /// <summary>
        ///     Gets the rotation stored in this matrix.
        /// </summary>
        public Quaternion Rotation => Quaternion.CreateFromRotationMatrix(this);

        /// <summary>
        ///     Gets the position stored in this matrix.
        /// </summary>
        public Vector3 Translation => new Vector3(M41, M42, M43);

        /// <summary>
        ///     Gets the scale stored in this matrix.
        /// </summary>
        public Vector3 Scale => new Vector3(M11, M22, M33);

        /// <summary>
        ///     Creates a new <see cref="Matrix" /> which contains the rotation moment around specified axis.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <returns>The rotation <see cref="Matrix" />.</returns>
        public static Matrix CreateFromAxisAngle(Vector3 axis, float angle)
        {
            var sin = (float) Math.Sin(angle);
            var cos = (float) Math.Cos(angle);
            var xx = axis.X*axis.X;
            var yy = axis.Y*axis.Y;
            var zz = axis.Z*axis.Z;
            var xy = axis.X*axis.Y;
            var xz = axis.X*axis.Z;
            var yz = axis.Y*axis.Z;

            return new Matrix(
                xx + cos*(1f - xx), xy - cos*xy + sin*axis.Z, xz - cos*xz - sin*axis.Y, 0,
                xy - cos*xy - sin*axis.Z, yy + cos*(1f - yy), yz - cos*yz + sin*axis.X, 0,
                xz - cos*xz + sin*axis.Y, yz - cos*yz - sin*axis.X, zz + cos*(1f - zz), 0,
                0, 0, 0, 1
            );
        }

        /// <summary>
        ///     Creates a new rotation <see cref="Matrix" /> from a <see cref="Quaternion" />.
        /// </summary>
        /// <param name="quaternion"><see cref="Quaternion" /> of rotation moment.</param>
        /// <returns>The rotation <see cref="Matrix" />.</returns>
        public static Matrix CreateFromQuaternion(Quaternion quaternion)
        {
            var xx = quaternion.X*quaternion.X;
            var yy = quaternion.Y*quaternion.Y;
            var zz = quaternion.Z*quaternion.Z;
            var xy = quaternion.X*quaternion.Y;
            var zw = quaternion.Z*quaternion.W;
            var zx = quaternion.Z*quaternion.X;
            var yw = quaternion.Y*quaternion.W;
            var yz = quaternion.Y*quaternion.Z;
            var xw = quaternion.X*quaternion.W;

            return new Matrix(
                1f - 2f*(yy + zz), 2f*(xy + zw), 2f*(zx - yw), 0f,
                2f*(xy - zw), 1f - 2f*(zz + xx), 2f*(yz + xw), 0f,
                2f*(zx + yw), 2f*(yz - xw), 1f - 2f*(yy + xx), 0f,
                0f, 0f, 0f, 1f
            );
        }

        /// <summary>
        ///     Creates a new rotation <see cref="Matrix" /> from the specified yaw, pitch and roll values.
        /// </summary>
        /// <param name="yaw">The yaw rotation value in radians.</param>
        /// <param name="pitch">The pitch rotation value in radians.</param>
        /// <param name="roll">The roll rotation value in radians.</param>
        /// <returns>The rotation <see cref="Matrix" />.</returns>
        /// <remarks>
        ///     For more information about yaw, pitch and roll visit http://en.wikipedia.org/wiki/Euler_angles.
        /// </remarks>
        public static Matrix CreateFromYawPitchRoll(float yaw, float pitch, float roll)
        {
            var quaternion = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
            return CreateFromQuaternion(quaternion);
        }

        /// <summary>
        ///     Creates a new viewing <see cref="Matrix" />.
        /// </summary>
        /// <param name="cameraPosition">Position of the camera.</param>
        /// <param name="cameraTarget">Lookup vector of the camera.</param>
        /// <param name="cameraUpVector">The direction of the upper edge of the camera.</param>
        /// <returns>The viewing <see cref="Matrix" />.</returns>
        public static Matrix CreateLookAt(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector)
        {
            var v1 = Vector3.Normalize(cameraPosition - cameraTarget);
            var v2 = Vector3.Normalize(Vector3.Cross(cameraUpVector, v1));
            var v3 = Vector3.Cross(v1, v2);

            return new Matrix(
                v2.X, v3.X, v1.X, 0f,
                v2.Y, v3.Y, v1.Y, 0f,
                v2.Z, v3.Z, v1.Z, 0f,
                -Vector3.Dot(v2, cameraPosition),
                -Vector3.Dot(v3, cameraPosition),
                -Vector3.Dot(v1, cameraPosition),
                1f);
        }

        /// <summary>
        ///     Creates a new rotation <see cref="Matrix" /> around X axis.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <returns>The rotation <see cref="Matrix" /> around X axis.</returns>
        public static Matrix CreateRotationX(float radians)
        {
            var val1 = (float) Math.Cos(radians);
            var val2 = (float) Math.Sin(radians);

            return new Matrix(
                1, 0, 0, 0,
                0, val1, val2, 0,
                0, -val2, val1, 0,
                0, 0, 0, 1
            );
        }

        /// <summary>
        ///     Creates a new rotation <see cref="Matrix" /> around Y axis.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <returns>The rotation <see cref="Matrix" /> around Y axis.</returns>
        public static Matrix CreateRotationY(float radians)
        {
            var val1 = (float) Math.Cos(radians);
            var val2 = (float) Math.Sin(radians);

            return new Matrix(
                val1, 0, -val2, 0,
                0, 1, 0, 0,
                val2, 0, val1, 0,
                0, 0, 0, 1
            );
        }

        /// <summary>
        ///     Creates a new rotation <see cref="Matrix" /> around Z axis.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <returns>The rotation <see cref="Matrix" /> around Z axis.</returns>
        public static Matrix CreateRotationZ(float radians)
        {
            var val1 = (float) Math.Cos(radians);
            var val2 = (float) Math.Sin(radians);

            return new Matrix(
                val1, val2, 0, 0,
                -val2, val1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            );
        }

        /// <summary>
        ///     Creates a new translation <see cref="Matrix" />.
        /// </summary>
        /// <param name="position">The translation.</param>
        /// <returns>The translation <see cref="Matrix" />.</returns>
        public static Matrix CreateTranslation(Vector3 position)
        {
            return new Matrix(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                position.X,
                position.Y,
                position.Z,
                1
            );
        }

        /// <summary>
        ///     Returns a determinant of this <see cref="Matrix" />.
        /// </summary>
        /// <returns>Determinant of this <see cref="Matrix" /></returns>
        /// <remarks>
        ///     See more about determinant here - http://en.wikipedia.org/wiki/Determinant.
        /// </remarks>
        public float Determinant()
        {
            var num22 = M11;
            var num21 = M12;
            var num20 = M13;
            var num19 = M14;
            var num12 = M21;
            var num11 = M22;
            var num10 = M23;
            var num9 = M24;
            var num8 = M31;
            var num7 = M32;
            var num6 = M33;
            var num5 = M34;
            var num4 = M41;
            var num3 = M42;
            var num2 = M43;
            var num = M44;
            var num18 = num6*num - num5*num2;
            var num17 = num7*num - num5*num3;
            var num16 = num7*num2 - num6*num3;
            var num15 = num8*num - num5*num4;
            var num14 = num8*num2 - num6*num4;
            var num13 = num8*num3 - num7*num4;
            return num22*(num11*num18 - num10*num17 + num9*num16) -
                   num21*(num12*num18 - num10*num15 + num9*num14) +
                   num20*(num12*num17 - num11*num15 + num9*num13) -
                   num19*(num12*num16 - num11*num14 + num10*num13);
        }

        /// <summary>
        ///     Compares whether current instance is equal to specified <see cref="Matrix" /> without any tolerance.
        /// </summary>
        /// <param name="other">The <see cref="Matrix" /> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public bool Equals(Matrix other)
        {
            return M11.Equals(other.M11) && M22.Equals(other.M22) && M33.Equals(other.M33) && M44.Equals(other.M44) &&
                   M12.Equals(other.M12) && M13.Equals(other.M13) && M14.Equals(other.M14) && M21.Equals(other.M21) &&
                   M23.Equals(other.M23) && M24.Equals(other.M24) && M31.Equals(other.M31) && M32.Equals(other.M32) &&
                   M34.Equals(other.M34) && M41.Equals(other.M41) && M42.Equals(other.M42) && M43.Equals(other.M43);
        }

        /// <summary>
        ///     Compares whether current instance is equal to specified <see cref="Object" /> without any tolerance.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            var flag = false;
            if (obj is Matrix)
                flag = Equals((Matrix) obj);
            return flag;
        }

        /// <summary>
        ///     Creates a new <see cref="Matrix" /> which contains inversion of the specified matrix.
        /// </summary>
        /// <param name="matrix">Source <see cref="Matrix" />.</param>
        /// <returns>The inverted matrix.</returns>
        public static Matrix Invert(Matrix matrix)
        {
            var num17 = (float) (matrix.M33*(double) matrix.M44 - matrix.M34*(double) matrix.M43);
            var num18 = (float) (matrix.M32*(double) matrix.M44 - matrix.M34*(double) matrix.M42);
            var num19 = (float) (matrix.M32*(double) matrix.M43 - matrix.M33*(double) matrix.M42);
            var num20 = (float) (matrix.M31*(double) matrix.M44 - matrix.M34*(double) matrix.M41);
            var num21 = (float) (matrix.M31*(double) matrix.M43 - matrix.M33*(double) matrix.M41);
            var num22 = (float) (matrix.M31*(double) matrix.M42 - matrix.M32*(double) matrix.M41);
            var num23 = (float) (matrix.M22*(double) num17 - matrix.M23*(double) num18 + matrix.M24*(double) num19);
            var num24 = (float) -(matrix.M21*(double) num17 - matrix.M23*(double) num20 + matrix.M24*(double) num21);
            var num25 = (float) (matrix.M21*(double) num18 - matrix.M22*(double) num20 + matrix.M24*(double) num22);
            var num26 = (float) -(matrix.M21*(double) num19 - matrix.M22*(double) num21 + matrix.M23*(double) num22);
            var num27 =
                (float)
                (1.0/
                 (matrix.M11*(double) num23 + matrix.M12*(double) num24 + matrix.M13*(double) num25 +
                  matrix.M14*(double) num26));

            var num28 = (float) (matrix.M23*(double) matrix.M44 - matrix.M24*(double) matrix.M43);
            var num29 = (float) (matrix.M22*(double) matrix.M44 - matrix.M24*(double) matrix.M42);
            var num30 = (float) (matrix.M22*(double) matrix.M43 - matrix.M23*(double) matrix.M42);
            var num31 = (float) (matrix.M21*(double) matrix.M44 - matrix.M24*(double) matrix.M41);
            var num32 = (float) (matrix.M21*(double) matrix.M43 - matrix.M23*(double) matrix.M41);
            var num33 = (float) (matrix.M21*(double) matrix.M42 - matrix.M22*(double) matrix.M41);

            var num34 = (float) (matrix.M23*(double) matrix.M34 - matrix.M24*(double) matrix.M33);
            var num35 = (float) (matrix.M22*(double) matrix.M34 - matrix.M24*(double) matrix.M32);
            var num36 = (float) (matrix.M22*(double) matrix.M33 - matrix.M23*(double) matrix.M32);
            var num37 = (float) (matrix.M21*(double) matrix.M34 - matrix.M24*(double) matrix.M31);
            var num38 = (float) (matrix.M21*(double) matrix.M33 - matrix.M23*(double) matrix.M31);
            var num39 = (float) (matrix.M21*(double) matrix.M32 - matrix.M22*(double) matrix.M31);

            return new Matrix(
                num23*num27,
                (float) -(matrix.M12*(double) num17 - matrix.M13*(double) num18 + matrix.M14*(double) num19)*num27,
                (float) (matrix.M12*(double) num28 - matrix.M13*(double) num29 + matrix.M14*(double) num30)*num27,
                (float) -(matrix.M12*(double) num34 - matrix.M13*(double) num35 + matrix.M14*(double) num36)*num27,
                num24*num27,
                (float) (matrix.M11*(double) num17 - matrix.M13*(double) num20 + matrix.M14*(double) num21)*num27,
                (float) -(matrix.M11*(double) num28 - matrix.M13*(double) num31 + matrix.M14*(double) num32)*num27,
                (float) (matrix.M11*(double) num34 - matrix.M13*(double) num37 + matrix.M14*(double) num38)*num27,
                num25*num27,
                (float) -(matrix.M11*(double) num18 - matrix.M12*(double) num20 + matrix.M14*(double) num22)*num27,
                (float) (matrix.M11*(double) num29 - matrix.M12*(double) num31 + matrix.M14*(double) num33)*num27,
                (float) -(matrix.M11*(double) num35 - matrix.M12*(double) num37 + matrix.M14*(double) num39)*num27,
                num26*num27,
                (float) (matrix.M11*(double) num19 - matrix.M12*(double) num21 + matrix.M13*(double) num22)*num27,
                (float) -(matrix.M11*(double) num30 - matrix.M12*(double) num32 + matrix.M13*(double) num33)*num27,
                (float) (matrix.M11*(double) num36 - matrix.M12*(double) num38 + matrix.M13*(double) num39)*num27
            );
        }

        /// <summary>
        ///     Creates a new <see cref="Matrix" /> that contains linear interpolation of the values in specified matrixes.
        /// </summary>
        /// <param name="matrix1">The first <see cref="Matrix" />.</param>
        /// <param name="matrix2">The second <see cref="Vector2" />.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <returns>>The result of linear interpolation of the specified matrixes.</returns>
        public static Matrix Lerp(Matrix matrix1, Matrix matrix2, float amount)
        {
            return matrix1 + (matrix2 - matrix1)*amount;
        }

        /// <summary>
        ///     Adds two matrixes.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix" /> on the left of the add sign.</param>
        /// <param name="matrix2">Source <see cref="Matrix" /> on the right of the add sign.</param>
        /// <returns>Sum of the matrixes.</returns>
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            return new Matrix(
                matrix1.M11 + matrix2.M11,
                matrix1.M12 + matrix2.M12,
                matrix1.M13 + matrix2.M13,
                matrix1.M14 + matrix2.M14,
                matrix1.M21 + matrix2.M21,
                matrix1.M22 + matrix2.M22,
                matrix1.M23 + matrix2.M23,
                matrix1.M24 + matrix2.M24,
                matrix1.M31 + matrix2.M31,
                matrix1.M32 + matrix2.M32,
                matrix1.M33 + matrix2.M33,
                matrix1.M34 + matrix2.M34,
                matrix1.M41 + matrix2.M41,
                matrix1.M42 + matrix2.M42,
                matrix1.M43 + matrix2.M43,
                matrix1.M44 + matrix2.M44
            );
        }

        /// <summary>
        ///     Divides the elements of a <see cref="Matrix" /> by the elements of another <see cref="Matrix" />.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix" /> on the left of the div sign.</param>
        /// <param name="matrix2">Divisor <see cref="Matrix" /> on the right of the div sign.</param>
        /// <returns>The result of dividing the matrixes.</returns>
        public static Matrix operator /(Matrix matrix1, Matrix matrix2)
        {
            return new Matrix(
                matrix1.M11/matrix2.M11,
                matrix1.M12/matrix2.M12,
                matrix1.M13/matrix2.M13,
                matrix1.M14/matrix2.M14,
                matrix1.M21/matrix2.M21,
                matrix1.M22/matrix2.M22,
                matrix1.M23/matrix2.M23,
                matrix1.M24/matrix2.M24,
                matrix1.M31/matrix2.M31,
                matrix1.M32/matrix2.M32,
                matrix1.M33/matrix2.M33,
                matrix1.M34/matrix2.M34,
                matrix1.M41/matrix2.M41,
                matrix1.M42/matrix2.M42,
                matrix1.M43/matrix2.M43,
                matrix1.M44/matrix2.M44
            );
        }

        /// <summary>
        ///     Divides the elements of a <see cref="Matrix" /> by a scalar.
        /// </summary>
        /// <param name="matrix">Source <see cref="Matrix" /> on the left of the div sign.</param>
        /// <param name="scalar">Divisor scalar on the right of the div sign.</param>
        /// <returns>The result of dividing a matrix by a scalar.</returns>
        public static Matrix operator /(Matrix matrix, float scalar)
        {
            return matrix*(1f/scalar);
        }

        /// <summary>
        ///     Compares whether two <see cref="Matrix" /> instances are equal without any tolerance.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix" /> on the left of the equal sign.</param>
        /// <param name="matrix2">Source <see cref="Matrix" /> on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(Matrix matrix1, Matrix matrix2)
        {
            return
                matrix1.M11.Equals(matrix2.M11) &&
                matrix1.M12.Equals(matrix2.M12) &&
                matrix1.M13.Equals(matrix2.M13) &&
                matrix1.M14.Equals(matrix2.M14) &&
                matrix1.M21.Equals(matrix2.M21) &&
                matrix1.M22.Equals(matrix2.M22) &&
                matrix1.M23.Equals(matrix2.M23) &&
                matrix1.M24.Equals(matrix2.M24) &&
                matrix1.M31.Equals(matrix2.M31) &&
                matrix1.M32.Equals(matrix2.M32) &&
                matrix1.M33.Equals(matrix2.M33) &&
                matrix1.M34.Equals(matrix2.M34) &&
                matrix1.M41.Equals(matrix2.M41) &&
                matrix1.M42.Equals(matrix2.M42) &&
                matrix1.M43.Equals(matrix2.M43) &&
                matrix1.M44.Equals(matrix2.M44);
        }

        /// <summary>
        ///     Compares whether two <see cref="Matrix" /> instances are not equal without any tolerance.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix" /> on the left of the not equal sign.</param>
        /// <param name="matrix2">Source <see cref="Matrix" /> on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(Matrix matrix1, Matrix matrix2)
        {
            return
                !matrix1.M11.Equals(matrix2.M11) ||
                !matrix1.M12.Equals(matrix2.M12) ||
                !matrix1.M13.Equals(matrix2.M13) ||
                !matrix1.M14.Equals(matrix2.M14) ||
                !matrix1.M21.Equals(matrix2.M21) ||
                !matrix1.M22.Equals(matrix2.M22) ||
                !matrix1.M23.Equals(matrix2.M23) ||
                !matrix1.M24.Equals(matrix2.M24) ||
                !matrix1.M31.Equals(matrix2.M31) ||
                !matrix1.M32.Equals(matrix2.M32) ||
                !matrix1.M33.Equals(matrix2.M33) ||
                !matrix1.M34.Equals(matrix2.M34) ||
                !matrix1.M41.Equals(matrix2.M41) ||
                !matrix1.M42.Equals(matrix2.M42) ||
                !matrix1.M43.Equals(matrix2.M43) ||
                !matrix1.M44.Equals(matrix2.M44);
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
                var hashCode = M11.GetHashCode();
                hashCode = (hashCode*397) ^ M12.GetHashCode();
                hashCode = (hashCode*397) ^ M13.GetHashCode();
                hashCode = (hashCode*397) ^ M14.GetHashCode();
                hashCode = (hashCode*397) ^ M21.GetHashCode();
                hashCode = (hashCode*397) ^ M22.GetHashCode();
                hashCode = (hashCode*397) ^ M23.GetHashCode();
                hashCode = (hashCode*397) ^ M24.GetHashCode();
                hashCode = (hashCode*397) ^ M31.GetHashCode();
                hashCode = (hashCode*397) ^ M32.GetHashCode();
                hashCode = (hashCode*397) ^ M33.GetHashCode();
                hashCode = (hashCode*397) ^ M34.GetHashCode();
                hashCode = (hashCode*397) ^ M41.GetHashCode();
                hashCode = (hashCode*397) ^ M42.GetHashCode();
                hashCode = (hashCode*397) ^ M43.GetHashCode();
                hashCode = (hashCode*397) ^ M44.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        ///     Multiplies two matrixes.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix" /> on the left of the mul sign.</param>
        /// <param name="matrix2">Source <see cref="Matrix" /> on the right of the mul sign.</param>
        /// <returns>Result of the matrix multiplication.</returns>
        /// <remarks>
        ///     Using matrix multiplication algorithm - see http://en.wikipedia.org/wiki/Matrix_multiplication.
        /// </remarks>
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            return new Matrix(
                matrix1.M11*matrix2.M11 + matrix1.M12*matrix2.M21 + matrix1.M13*matrix2.M31 + matrix1.M14*matrix2.M41,
                matrix1.M11*matrix2.M12 + matrix1.M12*matrix2.M22 + matrix1.M13*matrix2.M32 + matrix1.M14*matrix2.M42,
                matrix1.M11*matrix2.M13 + matrix1.M12*matrix2.M23 + matrix1.M13*matrix2.M33 + matrix1.M14*matrix2.M43,
                matrix1.M11*matrix2.M14 + matrix1.M12*matrix2.M24 + matrix1.M13*matrix2.M34 + matrix1.M14*matrix2.M44,
                matrix1.M21*matrix2.M11 + matrix1.M22*matrix2.M21 + matrix1.M23*matrix2.M31 + matrix1.M24*matrix2.M41,
                matrix1.M21*matrix2.M12 + matrix1.M22*matrix2.M22 + matrix1.M23*matrix2.M32 + matrix1.M24*matrix2.M42,
                matrix1.M21*matrix2.M13 + matrix1.M22*matrix2.M23 + matrix1.M23*matrix2.M33 + matrix1.M24*matrix2.M43,
                matrix1.M21*matrix2.M14 + matrix1.M22*matrix2.M24 + matrix1.M23*matrix2.M34 + matrix1.M24*matrix2.M44,
                matrix1.M31*matrix2.M11 + matrix1.M32*matrix2.M21 + matrix1.M33*matrix2.M31 + matrix1.M34*matrix2.M41,
                matrix1.M31*matrix2.M12 + matrix1.M32*matrix2.M22 + matrix1.M33*matrix2.M32 + matrix1.M34*matrix2.M42,
                matrix1.M31*matrix2.M13 + matrix1.M32*matrix2.M23 + matrix1.M33*matrix2.M33 + matrix1.M34*matrix2.M43,
                matrix1.M31*matrix2.M14 + matrix1.M32*matrix2.M24 + matrix1.M33*matrix2.M34 + matrix1.M34*matrix2.M44,
                matrix1.M41*matrix2.M11 + matrix1.M42*matrix2.M21 + matrix1.M43*matrix2.M31 + matrix1.M44*matrix2.M41,
                matrix1.M41*matrix2.M12 + matrix1.M42*matrix2.M22 + matrix1.M43*matrix2.M32 + matrix1.M44*matrix2.M42,
                matrix1.M41*matrix2.M13 + matrix1.M42*matrix2.M23 + matrix1.M43*matrix2.M33 + matrix1.M44*matrix2.M43,
                matrix1.M41*matrix2.M14 + matrix1.M42*matrix2.M24 + matrix1.M43*matrix2.M34 + matrix1.M44*matrix2.M44
            );
        }

        /// <summary>
        ///     Multiplies the elements of matrix by a scalar.
        /// </summary>
        /// <param name="matrix">Source <see cref="Matrix" /> on the left of the mul sign.</param>
        /// <param name="scalar">Scalar value on the right of the mul sign.</param>
        /// <returns>Result of the matrix multiplication with a scalar.</returns>
        public static Matrix operator *(Matrix matrix, float scalar)
        {
            return new Matrix(
                matrix.M11*scalar,
                matrix.M12*scalar,
                matrix.M13*scalar,
                matrix.M14*scalar,
                matrix.M21*scalar,
                matrix.M22*scalar,
                matrix.M23*scalar,
                matrix.M24*scalar,
                matrix.M31*scalar,
                matrix.M32*scalar,
                matrix.M33*scalar,
                matrix.M34*scalar,
                matrix.M41*scalar,
                matrix.M42*scalar,
                matrix.M43*scalar,
                matrix.M44*scalar
            );
        }

        /// <summary>
        ///     Subtracts the values of one <see cref="Matrix" /> from another <see cref="Matrix" />.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix" /> on the left of the sub sign.</param>
        /// <param name="matrix2">Source <see cref="Matrix" /> on the right of the sub sign.</param>
        /// <returns>Result of the matrix subtraction.</returns>
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            return new Matrix(
                matrix1.M11 - matrix2.M11,
                matrix1.M12 - matrix2.M12,
                matrix1.M13 - matrix2.M13,
                matrix1.M14 - matrix2.M14,
                matrix1.M21 - matrix2.M21,
                matrix1.M22 - matrix2.M22,
                matrix1.M23 - matrix2.M23,
                matrix1.M24 - matrix2.M24,
                matrix1.M31 - matrix2.M31,
                matrix1.M32 - matrix2.M32,
                matrix1.M33 - matrix2.M33,
                matrix1.M34 - matrix2.M34,
                matrix1.M41 - matrix2.M41,
                matrix1.M42 - matrix2.M42,
                matrix1.M43 - matrix2.M43,
                matrix1.M44 - matrix2.M44
            );
        }

        /// <summary>
        ///     Inverts values in the specified <see cref="Matrix" />.
        /// </summary>
        /// <param name="matrix">Source <see cref="Matrix" /> on the right of the sub sign.</param>
        /// <returns>Result of the inversion.</returns>
        public static Matrix operator -(Matrix matrix)
        {
            return new Matrix(
                -matrix.M11, -matrix.M12, -matrix.M13, -matrix.M14,
                -matrix.M21, -matrix.M22, -matrix.M23, -matrix.M24,
                -matrix.M31, -matrix.M32, -matrix.M33, -matrix.M34,
                -matrix.M41, -matrix.M42, -matrix.M43, -matrix.M44
            );
        }

        /// <summary>
        ///     Swap the matrix rows and columns.
        /// </summary>
        /// <param name="matrix">The matrix for transposing operation.</param>
        /// <returns>The new <see cref="Matrix" /> which contains the transposing result.</returns>
        public static Matrix Transpose(Matrix matrix)
        {
            return new Matrix(
                matrix.M11, matrix.M21, matrix.M31, matrix.M41,
                matrix.M12, matrix.M22, matrix.M32, matrix.M42,
                matrix.M13, matrix.M23, matrix.M33, matrix.M43,
                matrix.M14, matrix.M24, matrix.M34, matrix.M44);
        }

        /// <summary>
        ///     Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"(({M11}, {M12}, {M13}, {M14}), " +
                   $"({M21}, {M22}, {M23}, {M24}), " +
                   $"({M31}, {M32}, {M33}, {M34}), " +
                   $"({M41}, {M42}, {M43}, {M44}))";
        }
    }
}