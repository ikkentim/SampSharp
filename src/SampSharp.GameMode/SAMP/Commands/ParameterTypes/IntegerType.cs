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
using System.Linq;
using static System.StringComparison;

namespace SampSharp.GameMode.SAMP.Commands.ParameterTypes;

/// <summary>Represents an integer command parameter.</summary>
public class IntegerType : ICommandParameterType
{
    private static readonly char[] _base10Characters = "1234567890,.-".ToCharArray();
    private static readonly char[] _base16Characters = "1234567890abcdef".ToCharArray();

    /// <summary>Gets the value for the occurrence of this parameter type at the start of the commandText. The processed text will be removed from the commandText.</summary>
    /// <param name="commandText">The command text.</param>
    /// <param name="output">The output.</param>
    /// <param name="isNullable">A value indicating whether the result is allowed to be null when an entity referenced by the argument could not be found.</param>
    /// <returns>true if parsed successfully; false otherwise.</returns>
    public bool Parse(ref string commandText, out object output, bool isNullable = false)
    {
        var text = commandText.TrimStart();
        output = null;

        if (string.IsNullOrEmpty(text))
            return false;


        var word = text.Split(' ')
            .First();


        // Regular base 10 numbers (eg. 14143)
        if (word.All(_base10Characters.Contains) && int.TryParse(word, out var number))
        {
            commandText = commandText.Substring(word.Length)
                .TrimStart(' ');
            output = number;
            return true;
        }

        // Base 16 (hexadecimal) numbers. Can be prefixed with '0x', '#' or post-fixed with 'H' or 'h'.
        var base16Word = word.Length switch
        {
            > 2 when word.StartsWith("0x", InvariantCulture) => word[2..],
            > 1 when word.StartsWith("#", InvariantCulture) => word[1..],
            > 1 when word.ToLower()
                .EndsWith("h") => word[..^1],
            _ => null
        };

        if (base16Word != null && base16Word.ToLower()
                .All(_base16Characters.Contains) && int.TryParse(base16Word, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out number))
        {
            commandText = commandText[word.Length..]
                .TrimStart(' ');
            output = number;
            return true;
        }

        return false;
    }
}