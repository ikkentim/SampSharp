﻿// SampSharp
// Copyright 2022 Tim Potze
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
using System.Runtime.InteropServices;
using System.Text;

namespace SampSharp.Core;

/// <summary>
///     Contains methods for converting byte arrays to values, values to byte arrays and values to other values.
/// </summary>
public static class ValueConverter
{
    /// <summary>
    ///     Gets the bytes representing the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The bytes representing the specified value.</returns>
    public static byte[] GetBytes(int value)
    {
        return BitConverter.GetBytes(value);
    }
    /// <summary>
    ///     Gets the bytes representing the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The bytes representing the specified value.</returns>
    public static byte[] GetBytes(ushort value)
    {
        return BitConverter.GetBytes(value);
    }

    /// <summary>
    ///     Gets the bytes representing the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The bytes representing the specified value.</returns>
    public static byte[] GetBytes(bool value)
    {
        return BitConverter.GetBytes(ToInt32(value));
    }

    /// <summary>
    ///     Gets the bytes representing the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The bytes representing the specified value.</returns>
    public static byte[] GetBytes(float value)
    {
        return BitConverter.GetBytes(ToInt32(value));
    }

    /// <summary>
    ///     Gets the bytes representing the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The bytes representing the specified value</returns>
    public static byte[] GetBytes(uint value)
    {
        return BitConverter.GetBytes(value);
    }

    /// <summary>
    ///     Gets the bytes representing the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="encoding">The encoding to use.</param>
    /// <returns>The bytes representing the specified value</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value" /> is null.</exception>
    public static byte[] GetBytes(string value, Encoding encoding)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));

        encoding ??= Encoding.ASCII;

        var bytes = new byte[encoding.GetByteCount(value) + 1];
        bytes[^1] = (byte) '\0';
        encoding.GetBytes(value, 0, value.Length, bytes, 0);
        return bytes;
    }

    /// <summary>
    ///     Reads an <see cref="int" /> from the specified <paramref name="buffer" /> starting at the specified
    ///     <paramref name="startIndex" />.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The <see cref="int" /> read from the specified buffer.</returns>
    public static int ToInt32(byte[] buffer, int startIndex)
    {
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));

        return BitConverter.ToInt32(buffer, startIndex);
    }

    /// <summary>
    ///     Converts the specified <paramref name="value" /> to a <see cref="int" />
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The converted <see cref="int" />.</returns>
    public static int ToInt32(float value)
    {
        return new ValueUnion { single = value }.int32;
    }

    /// <summary>
    ///     Converts the specified <paramref name="value" /> to a <see cref="int" />
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The converted <see cref="int" />.</returns>
    public static int ToInt32(bool value)
    {
        return value ? 1 : 0;
    }

    /// <summary>
    ///     Reads an <see cref="ushort" /> from the specified <paramref name="buffer" /> starting at the specified
    ///     <paramref name="startIndex" />.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The <see cref="ushort" /> read from the specified buffer.</returns>
    public static ushort ToUInt16(byte[] buffer, int startIndex)
    {
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));

        return BitConverter.ToUInt16(buffer, startIndex);

    }

    /// <summary>
    ///     Converts the specified <paramref name="value" /> to a <see cref="uint" />
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The converted <see cref="uint" />.</returns>
    public static uint ToUInt32(int value)
    {
        return new ValueUnion { int32 = value }.uint32;
    }
        
    /// <summary>
    ///     Reads an <see cref="uint" /> from the specified <paramref name="buffer" /> starting at the specified
    ///     <paramref name="startIndex" />.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The <see cref="uint" /> read from the specified buffer.</returns>
    public static uint ToUInt32(byte[] buffer, int startIndex)
    {
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));

        return BitConverter.ToUInt32(buffer, startIndex);
    }

    /// <summary>
    ///     Reads a <see cref="bool" /> from the specified <paramref name="buffer" /> starting at the specified
    ///     <paramref name="startIndex" />.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The <see cref="bool" /> read from the specified buffer.</returns>
    public static bool ToBoolean(byte[] buffer, int startIndex)
    {
        return buffer == null
            ? throw new ArgumentNullException(nameof(buffer))
            : ToBoolean(ToInt32(buffer, startIndex));
    }

    /// <summary>
    ///     Converts the specified <paramref name="value" /> to a <see cref="bool" />
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The converted <see cref="bool" />.</returns>
    public static bool ToBoolean(int value)
    {
        return value != 0;
    }

    /// <summary>
    ///     Reads a <see cref="float" /> from the specified <paramref name="buffer" /> starting at the specified
    ///     <paramref name="startIndex" />.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The <see cref="float" /> read from the specified buffer.</returns>
    public static float ToSingle(byte[] buffer, int startIndex)
    {
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));

        return ToSingle(ToInt32(buffer, startIndex));
    }

    /// <summary>
    ///     Converts the specified <paramref name="value" /> to a <see cref="float" />
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The converted <see cref="float" />.</returns>
    public static float ToSingle(int value)
    {
        return new ValueUnion { int32 = value }.single;
    }

    /// <summary>
    ///     Reads a <see cref="Version" /> from the specified <paramref name="buffer" /> starting at the specified
    ///     <paramref name="startIndex" />.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The <see cref="Version" /> read from the specified buffer.</returns>
    public static Version ToVersion(byte[] buffer, int startIndex)
    {
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));

        var num = ToUInt32(buffer, startIndex);
        return new Version((int) ((num >> 16) & 0xff), (int) ((num >> 8) & 0xff), (int) (num & 0xff));
    }

    /// <summary>
    ///     Reads a <see cref="string" /> from the specified <paramref name="buffer" /> starting at the specified
    ///     <paramref name="startIndex" />.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="encoding">The encoding to use.</param>
    /// <returns>The <see cref="string" /> read from the specified buffer.</returns>
    public static string ToString(byte[] buffer, int startIndex, Encoding encoding)
    {
        encoding ??= Encoding.ASCII;

        var terminatorIndex = Array.IndexOf(buffer, (byte)'\0', startIndex);
        if (terminatorIndex < 0)
            terminatorIndex = buffer.Length;

        return encoding.GetString(buffer, startIndex, terminatorIndex - startIndex);
    }
        
    /// <summary>
    ///     A struct to immediately cast <see cref="int" />, <see cref="uint" /> or <see cref="float" /> values.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    private struct ValueUnion
    {
        [FieldOffset(0)]
        public int int32;

        [FieldOffset(0)]
        public readonly uint uint32;

        [FieldOffset(0)]
        public float single;
    }
}