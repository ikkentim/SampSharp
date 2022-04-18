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

using System;
using System.Linq;

namespace SampSharp.Entities.SAMP.Commands.Parsers;

/// <summary>A parser for an <see cref="Enum" /> parameter.</summary>
public class EnumParser : ICommandParameterParser
{
    private readonly Type _enumType;

    private readonly WordParser _wordParser = new();

    /// <summary>Initializes a new instance of the <see cref="EnumParser" /> class.</summary>
    /// <param name="enumType">Type of the enum.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="enumType" /> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="enumType" /> is not an enum type.</exception>
    public EnumParser(Type enumType)
    {
        _enumType = enumType ?? throw new ArgumentNullException(nameof(enumType));

        if (!enumType.IsEnum)
            throw new ArgumentException("Type must be an enum", nameof(enumType));
    }

    /// <inheritdoc />
    public bool TryParse(IServiceProvider services, ref string inputText, out object result)
    {
        if (!_wordParser.TryParse(services, ref inputText, out var subResult) || !(subResult is string word))
        {
            result = null;
            return false;
        }

        if (int.TryParse(word, out var intWord) && Enum.IsDefined(_enumType, intWord))
        {
            result = Enum.ToObject(_enumType, intWord);
            return true;
        }

        var lowerWord = word.ToLowerInvariant();
        var names = Enum.GetNames(_enumType)
            .Where(n => n.ToLowerInvariant()
                .Contains(lowerWord))
            .ToArray();

        if (names.Length > 1)
            names = Enum.GetNames(_enumType)
                .Where(n => n.Contains(word))
                .ToArray();

        if (names.Length == 1)
        {
            result = Enum.Parse(_enumType, names[0]);
            return true;
        }

        result = null;
        return false;
    }
}