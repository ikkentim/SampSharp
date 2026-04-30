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

namespace SampSharp.Entities.SAMP.Commands.Parsers;

/// <summary>Provides functionality for a parser of a command parameter.</summary>
public interface ICommandParameterParser
{
    /// <summary>
    /// Tries to parse the specified <paramref name="inputText" /> into the argument for this parser. The consumed text is removed from the
    /// <paramref name="inputText" />.
    /// </summary>
    /// <param name="services">A service provider.</param>
    /// <param name="inputText">The input text to parse.</param>
    /// <param name="result">The parsed result.</param>
    /// <returns><c>true</c> if the parameter could be parsed; otherwise <c>false</c>.</returns>
    bool TryParse(IServiceProvider services, ref string inputText, out object result);
}