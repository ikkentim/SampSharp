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

namespace SampSharp.GameMode.SAMP.Commands.ParameterTypes;

/// <summary>
///     Represents a text command parameter.
/// </summary>
public class TextType : ICommandParameterType
{
    /// <summary>
    ///     Gets the value for the occurrence of this parameter type at the start of the commandText. The processed text will be
    ///     removed from the commandText.
    /// </summary>
    /// <param name="commandText">The command text.</param>
    /// <param name="output">The output.</param>
    /// <param name="isNullable">A value indicating whether the result is allowed to be null when an entity referenced by the argument could not be found.</param>
    /// <returns>
    ///     true if parsed successfully; false otherwise.
    /// </returns>
    public bool Parse(ref string commandText, out object output, bool isNullable = false)
    {
        var text = commandText.Trim();

        if (string.IsNullOrEmpty(text))
        {
            output = null;
            return false;
        }

        output = text;
        commandText = string.Empty;
        return true;
    }
}