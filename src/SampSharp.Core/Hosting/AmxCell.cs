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

namespace SampSharp.Core.Hosting;

/// <summary>Represents an AMX cell.</summary>
[StructLayout(LayoutKind.Explicit, Size = Size)]
public readonly struct AmxCell
{
    /// <summary>The size of an AMX cell.</summary>
    public const int Size = 4;

    [FieldOffset(0)]
    private readonly int _value;

    /// <summary>Initializes a new instance of the <see cref="AmxCell" /> struct.</summary>
    /// <param name="value">The value.</param>
    public AmxCell(int value) => _value = value;

    /// <summary>Performs an implicit conversion from <see cref="AmxCell" /> to <see cref="int" />.</summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator int(AmxCell value)
    {
        return value._value;
    }

    /// <summary>Performs an implicit conversion from <see cref="int" /> to <see cref="AmxCell" />.</summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator AmxCell(int value)
    {
        return new AmxCell(value);
    }
}