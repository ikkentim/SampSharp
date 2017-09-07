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
    ///     Specifies the identifier properties for a native object class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class NativeObjectIdentifiersAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NativeObjectIdentifiersAttribute" /> class.
        /// </summary>
        /// <param name="identifiers">The identifiers of the native object.</param>
        public NativeObjectIdentifiersAttribute(params string[] identifiers)
        {
            Identifiers = identifiers;
        }

        /// <summary>
        ///     Gets or sets the identifiers of the native object.
        /// </summary>
        public string[] Identifiers { get; set; }
    }
}