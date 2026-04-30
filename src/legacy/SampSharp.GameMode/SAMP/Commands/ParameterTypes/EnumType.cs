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
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace SampSharp.GameMode.SAMP.Commands.ParameterTypes;

/// <summary>Represents an enum command parameter.</summary>
/// <typeparam name="T">The enum type.</typeparam>
public class EnumType<T> : ICommandParameterType where T : struct, IConvertible
{
    /// <summary>Initializes a new instance of the <see cref="EnumType{T}" /> class.</summary>
    /// <exception cref="System.ArgumentException">T must be an enumerated type</exception>
    public EnumType()
    {
        if (!typeof(T).GetTypeInfo()
                .IsEnum)
            throw new ArgumentException("T must be an enumerated type");

        TestForValue = true;
    }

    /// <summary>Gets or sets whether input should be matches against the enum values. When False, the input will only be matches against the names.</summary>
    public bool TestForValue { get; set; }

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
        var lowerWord = word.ToLower(CultureInfo.InvariantCulture);

        // find all candidates containing the input word, case insensitive.
        var candidates = typeof(T).GetTypeInfo()
            .GetEnumValues()
            .OfType<object>()
            .Where(v => v.ToString()!.Contains(lowerWord, StringComparison.InvariantCultureIgnoreCase))
            .ToList();

        // in case of ambiguities find all candidates containing the input word, case sensitive.
        if (candidates.Count > 1)
        {
            candidates = candidates.Where(v => v.ToString()!.Contains(word, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
        }

        // in case of ambiguities find all candidates matching exactly the input word, case insensitive.
        if (candidates.Count > 1)
        {
            candidates = candidates.Where(v => v.ToString()!.ToLower(CultureInfo.InvariantCulture) == lowerWord)
                .ToList();
        }

        // in case of ambiguities find all candidates matching exactly the input word, case sensitive.
        if (candidates.Count > 1)
        {
            candidates = candidates.Where(v => v.ToString() == word)
                .ToList();
        }

        // if also testing against underlying values, loop trough every value, convert it to the underlying type and compare.
        if (TestForValue)
        {
            var valueCandidates = typeof(T).GetTypeInfo()
                .GetEnumValues()
                .OfType<object>()
                .Where(t => Convert.ChangeType(t, Enum.GetUnderlyingType(typeof(T)), CultureInfo.InvariantCulture)
                    .ToString() == word)
                .ToList();

            if (valueCandidates.Count == 1)
                candidates = valueCandidates;
        }

        if (candidates.Count == 1)
        {
            output = candidates.First();
            commandText = commandText.Substring(word.Length)
                .TrimStart(' ');
            return true;
        }

        return false;
    }
}