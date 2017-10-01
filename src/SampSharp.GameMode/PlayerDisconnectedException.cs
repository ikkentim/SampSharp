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

namespace SampSharp.GameMode
{
    /// <summary>
    ///     Represents errors which occur if a player disconnects during a certain procedure.
    /// </summary>
    public class PlayerDisconnectedException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="T:PlayerDisconnectedException" /> class.</summary>
        public PlayerDisconnectedException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:PlayerDisconnectedException" /> class with a specified error
        ///     message.
        /// </summary>
        /// <param name="message">The message that describes the error. </param>
        public PlayerDisconnectedException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:PlayerDisconnectedException" /> class with a specified error
        ///     message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">
        ///     The exception that is the cause of the current exception, or a null reference (Nothing in
        ///     Visual Basic) if no inner exception is specified.
        /// </param>
        public PlayerDisconnectedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}