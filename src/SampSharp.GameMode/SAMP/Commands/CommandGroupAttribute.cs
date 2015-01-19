// SampSharp
// Copyright 2015 Tim Potze
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

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     Contains a Group property to group a command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandGroupAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the CommandGroupAttribute class.
        /// </summary>
        /// <param name="group">The group in which this command lives.</param>
        public CommandGroupAttribute(params string[] group)
        {
            Group = string.Join(" ", group);
        }

        /// <summary>
        ///     Initializes a new instance of the CommandGroupAttribute class.
        /// </summary>
        /// <param name="group">The group in which this command lives.</param>
        public CommandGroupAttribute(string group)
        {
            Group = group;
        }

        /// <summary>
        ///     The group in which this command lives.
        /// </summary>
        public string Group { get; set; }
    }
}