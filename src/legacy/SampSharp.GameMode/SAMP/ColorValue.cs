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

using System.Globalization;

namespace SampSharp.GameMode.SAMP;

/// <summary>
/// Represents a human readable color. The string representation of this value provides the name of the color value or a hexadecimal formatted
/// representation of the value.
/// </summary>
public readonly struct ColorValue
{
    private readonly ColorFormat _fallbackFormat;

    /// <summary>Initializes a new instance of the <see cref="ColorValue" /> struct.</summary>
    /// <param name="value">The color value.</param>
    /// <param name="fallbackFormat">The fallback format used when the value is does not have a known name.</param>
    public ColorValue(Color value, ColorFormat fallbackFormat = ColorFormat.RGB)
    {
        _fallbackFormat = fallbackFormat;
        Value = value;
    }

    /// <summary>Gets the color value.</summary>
    public Color Value { get; }

    /// <summary>Performs an implicit conversion from <see cref="ColorValue" /> to <see cref="Color" />.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Color(ColorValue value) => value.Value;

    /// <inheritdoc />
    public override string ToString() => Value.GetName() ?? GetHexString(Value, _fallbackFormat);

    private static string GetHexString(Color color, ColorFormat colorFormat) => color.ToInteger(colorFormat)
        .ToString(colorFormat == ColorFormat.RGB
            ? "X6"
            : "X8", CultureInfo.InvariantCulture);
}