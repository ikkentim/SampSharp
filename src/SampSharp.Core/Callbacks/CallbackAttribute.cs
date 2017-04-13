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

namespace SampSharp.Core.Callbacks
{
    /// <summary>
    ///     Indicates a method should be loaded by <see cref="IGameModeClient.RegisterCallbacksInObject" /> and indicates the
    ///     name of the callback.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Method)]
    public class CallbackAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CallbackAttribute" /> class.
        /// </summary>
        public CallbackAttribute()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CallbackAttribute" /> class.
        /// </summary>
        /// <param name="name">The name of the callback.</param>
        public CallbackAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        ///     Gets or sets the name of the callback.
        /// </summary>
        public string Name { get; set; }
    }
}