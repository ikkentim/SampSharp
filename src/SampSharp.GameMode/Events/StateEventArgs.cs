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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerStateChanged" /> or <see cref="BasePlayer.StateChanged" /> event.
    /// </summary>
    public class StateEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StateEventArgs" /> class.
        /// </summary>
        /// <param name="newstate">The newstate.</param>
        /// <param name="oldstate">The oldstate.</param>
        public StateEventArgs(PlayerState newstate, PlayerState oldstate)
        {
            NewState = newstate;
            OldState = oldstate;
        }

        /// <summary>
        ///     Gets the new state.
        /// </summary>
        public PlayerState NewState { get; private set; }

        /// <summary>
        ///     Gets the old state.
        /// </summary>
        public PlayerState OldState { get; private set; }
    }
}