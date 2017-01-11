// SampSharp
// Copyright 2017 Tim Potze
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
using SampSharp.GameMode.SAMP.Commands.ParameterTypes;

namespace SampSharp.GameMode.SAMP.Commands.Parameters
{
    /// <summary>
    ///     Represents a command parameter.
    /// </summary>
    public class CommandParameterInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandParameterInfo" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="commandParameterType">Type of the command parameter.</param>
        /// <param name="isOptional">if set to <c>true</c> the parameter is optional.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <exception cref="ArgumentNullException">Thrown if name or commandParameterType is null</exception>
        public CommandParameterInfo(string name, ICommandParameterType commandParameterType, bool isOptional,
            object defaultValue)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (commandParameterType == null) throw new ArgumentNullException(nameof(commandParameterType));

            CommandParameterType = commandParameterType;
            IsOptional = isOptional;
            Name = name;
            DefaultValue = defaultValue;
        }

        /// <summary>
        ///     Gets the type of the command parameter.
        /// </summary>
        public ICommandParameterType CommandParameterType { get; }

        /// <summary>
        ///     Gets a value indicating whether this instance is optional.
        /// </summary>
        public bool IsOptional { get; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Gets the default value.
        /// </summary>
        public object DefaultValue { get; }
    }
}