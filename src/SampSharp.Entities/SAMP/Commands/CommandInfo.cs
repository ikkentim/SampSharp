// SampSharp
// Copyright 2020 Tim Potze
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

using System.Linq;

namespace SampSharp.Entities.SAMP.Commands
{
    /// <summary>
    /// Provides information about a command.
    /// </summary>
    public class CommandInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandInfo" /> class.
        /// </summary>
        /// <param name="names">The names of the command.</param>
        /// <param name="parameters">The parameters of the command.</param>
        /// <param name="ignoreCase">Ignore the names case</param>
        public CommandInfo(string[] names, CommandParameterInfo[] parameters, bool ignoreCase)
        {
            Names = names;
            Parameters = parameters;
            IgnoreCase = ignoreCase;
        }

        /// <summary>
        /// Gets the names of this command.
        /// </summary>
        public string[] Names { get; }

        /// <summary>
        /// Gets whether the command names case are ignored.
        /// </summary>
        public bool IgnoreCase { get; }

        /// <summary>
        /// Gets the display name of this command.
        /// </summary>
        public string DisplayName { get => Names.OrderByDescending(n => n.Length).First(); }

        /// <summary>
        /// Gets the parameters of this command.
        /// </summary>
        public CommandParameterInfo[] Parameters { get; }
    }
}