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

namespace SampSharp.GameMode.API
{
    /// <summary>
    ///     Specifies information about a native function. This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class NativeAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NativeAttribute" /> class.
        /// </summary>
        /// <param name="name">The name of the native function.</param>
        /// <remarks>
        ///     The <see cref="Lengths" /> array is automatically generated based on the indices of the array string reference
        ///     arguments + 1.
        /// </remarks>
        public NativeAttribute(string name) : this(name, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NativeAttribute" /> class.
        /// </summary>
        /// <param name="name">The name of the native function.</param>
        /// <param name="lengths">
        ///     The indexes of the arguments which specify the length of the array and string reference arguments
        ///     of the native function.
        /// </param>
        /// <remarks>
        ///     Specifying a negative length index indicates that the length is always the negation of the specified length, e.g.
        ///     -100 specifies a static length of 100 cells.
        /// </remarks>
        /// <exception cref="ArgumentNullException">name</exception>
        public NativeAttribute(string name, params int[] lengths)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            Name = name;
            Lengths = lengths;
        }

        /// <summary>
        ///     Gets the name of the native function.
        /// </summary>
        public string Name { get; private set; }


        /// <summary>
        ///     Gets the indexes of the arguments which specify the length of the array and string reference arguments of the
        ///     native function.
        /// </summary>
        public int[] Lengths { get; private set; }
    }
}