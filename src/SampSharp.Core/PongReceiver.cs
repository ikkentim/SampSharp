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
    ///     Represents an measurement device for the duration of SampSharp server pings.
    /// </summary>
    internal class PongReceiver : ICommandReceiveScope
    {
        private bool _done;
        private DateTime _start;

        /// <summary>
        ///     The result of the ping.
        /// </summary>
        public TimeSpan Result { get; private set; }

        #region Implementation of ICommandReceiveScope

        /// <summary>
        ///     Gets a value indicating whether the <see cref="IGameModeClient" /> should continue to receive commands.
        /// </summary>
        public bool ShouldReceiveCommands => !_done;

        #endregion

        /// <summary>
        ///     Ends the ping measurement.
        /// </summary>
        public void Pong()
        {
            _done = true;
            Result = DateTime.Now - _start;
        }

        /// <summary>
        ///     Starts the ping measurement.
        /// </summary>
        public void Ping()
        {
            _done = false;
            _start = DateTime.Now;
        }
    }
}