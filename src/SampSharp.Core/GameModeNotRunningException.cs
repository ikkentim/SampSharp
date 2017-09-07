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
    ///     An error thrown if the game mode is not running when game mode-specific methods are called.
    /// </summary>
    public class GameModeNotRunningException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GameModeNotRunningException" /> class.
        /// </summary>
        public GameModeNotRunningException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameModeNotRunningException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GameModeNotRunningException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameModeNotRunningException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public GameModeNotRunningException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}