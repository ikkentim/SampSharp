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
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provided data for the x or y event.
    /// </summary>
    public class SirenStateEventArgs : PlayerEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SirenStateEventArgs" /> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="newState">if set to <c>true</c> the siren is turned on in the new state.</param>
        public SirenStateEventArgs(BasePlayer player, bool newState) : base(player)
        {
            NewState = newState;
        }

        /// <summary>
        ///     Gets a value indicating whether the siren is turned on in the new state.
        /// </summary>
        public bool NewState { get; private set; }
    }
}