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

namespace SampSharp.Core
{
    /// <summary>
    ///     Thrown when an exception occurs while a <see cref="IGameModeClient" /> is running a game mode.
    /// </summary>
    public class GameModeClientException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GameModeClientException" /> class.
        /// </summary>
        public GameModeClientException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameModeClientException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GameModeClientException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameModeClientException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public GameModeClientException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}