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

namespace SampSharp.Core.Natives.NativeObjects
{
    /// <summary>
    ///     Indicates a method should be proxied by the <see cref="NativeObjectILGenerator" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class NativeMethodAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NativeMethodAttribute" /> class.
        /// </summary>
        public NativeMethodAttribute()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NativeMethodAttribute" /> class.
        /// </summary>
        /// <param name="lengths">The lengths of special arguments.</param>
        public NativeMethodAttribute(params uint[] lengths) : this(false, lengths)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NativeMethodAttribute" /> class.
        /// </summary>
        /// <param name="ignoreIdentifiers">if set to <c>true</c> identifiers are ignored.</param>
        /// <param name="lengths">The lengths.</param>
        public NativeMethodAttribute(bool ignoreIdentifiers, params uint[] lengths)
        {
            IgnoreIdentifiers = ignoreIdentifiers;
            Lengths = lengths;
        }

        /// <summary>
        ///     Gets or sets the function name.
        /// </summary>
        public string Function { get; set; }

        /// <summary>
        ///     Gets a value indicating whether to ignore identifiers.
        /// </summary>
        public bool IgnoreIdentifiers { get; }

        /// <summary>
        ///     Gets the lengths of special arguments.
        /// </summary>
        public uint[] Lengths { get; }
    }
}