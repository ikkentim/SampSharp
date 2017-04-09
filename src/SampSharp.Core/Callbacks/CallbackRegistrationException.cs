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
    ///     An error thrown when a callback could not be registered.
    /// </summary>
    public class CallbackRegistrationException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CallbackRegistrationException" /> class.
        /// </summary>
        public CallbackRegistrationException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CallbackRegistrationException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CallbackRegistrationException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CallbackRegistrationException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public CallbackRegistrationException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}