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
    ///     Represents a command-parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public abstract class ParameterAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the ParameterAttribute class.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        protected ParameterAttribute(string name)
        {
            Name = name;
            DisplayName = name;
        }

        /// <summary>
        ///     Gets the name of this parameter.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Gets or sets the displayname of this parameter.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     Gets or sets whether this parameter is optional.
        /// </summary>
        /// <remarks>
        ///     This property is auto-filled based on the methods' signature.
        /// </remarks>
        public bool Optional { get; set; }

        /// <summary>
        ///     Check if the parameter is well-formatted and return the output.
        /// </summary>
        /// <param name="command">The command text.</param>
        /// <param name="output">The output of this parameter.</param>
        /// <returns>True if the parameter is well-formatted, False otherwise.</returns>
        public abstract bool Check(ref string command, out object output);
    }
}