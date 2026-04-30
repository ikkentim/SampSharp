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

using SampSharp.Entities.SAMP.Commands.Parsers;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>Provides information about a parameter of a command.</summary>
public class CommandParameterInfo
{
    /// <summary>Initializes a new instance of the <see cref="CommandParameterInfo" /> class.</summary>
    /// <param name="name">The name.</param>
    /// <param name="parser">The parser.</param>
    /// <param name="isRequired">If set to <c>true</c> the parameter is required.</param>
    /// <param name="defaultValue">The default value of this parameter</param>
    /// <param name="parameterIndex">Index of the parameter.</param>
    public CommandParameterInfo(string name, ICommandParameterParser parser, bool isRequired, object defaultValue, int parameterIndex)
    {
        Name = name;
        Parser = parser;
        IsRequired = isRequired;
        DefaultValue = defaultValue;
        Index = parameterIndex;
    }

    /// <summary>Gets the name of this parameter.</summary>
    public string Name { get; }

    /// <summary>Gets the parser of this parameter.</summary>
    public ICommandParameterParser Parser { get; }

    /// <summary>Gets a value indicating whether this parameter is required.</summary>
    public bool IsRequired { get; }

    /// <summary>Gets the default value of this parameter.</summary>
    public object DefaultValue { get; }

    /// <summary>Gets the index of this parameter.</summary>
    public int Index { get; }
}