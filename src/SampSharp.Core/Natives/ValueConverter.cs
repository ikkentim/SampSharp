// SampSharp
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

using System.Runtime.InteropServices;

namespace SampSharp.Core.Natives;

/// <summary>Contains methods for converting byte arrays to values, values to byte arrays and values to other values.</summary>
internal static class ValueConverter
{
    /// <summary>Converts the specified <paramref name="value" /> to a <see cref="int" /></summary>
    /// <param name="value">The value.</param>
    /// <returns>The converted <see cref="int" />.</returns>
    public static int ToInt32(float value)
    {
        return new ValueUnion { single = value }.int32;
    }

    /// <summary>Converts the specified <paramref name="value" /> to a <see cref="int" /></summary>
    /// <param name="value">The value.</param>
    /// <returns>The converted <see cref="int" />.</returns>
    public static int ToInt32(bool value)
    {
        return value
            ? 1
            : 0;
    }
    
    /// <summary>Converts the specified <paramref name="value" /> to a <see cref="bool" /></summary>
    /// <param name="value">The value.</param>
    /// <returns>The converted <see cref="bool" />.</returns>
    public static bool ToBoolean(int value)
    {
        return value != 0;
    }

    /// <summary>Converts the specified <paramref name="value" /> to a <see cref="float" /></summary>
    /// <param name="value">The value.</param>
    /// <returns>The converted <see cref="float" />.</returns>
    public static float ToSingle(int value)
    {
        return new ValueUnion { int32 = value }.single;
    }
    
    /// <summary>A struct to immediately cast <see cref="int" />, <see cref="uint" /> or <see cref="float" /> values.</summary>
    [StructLayout(LayoutKind.Explicit)]
    private struct ValueUnion
    {
        [FieldOffset(0)]
        public int int32;
        
        [FieldOffset(0)]
        public float single;
    }
}