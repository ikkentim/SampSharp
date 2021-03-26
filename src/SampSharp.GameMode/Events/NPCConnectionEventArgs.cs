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
using System;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="NPC"/>NPC connection event.
    /// </summary>
    public class NPCConnectionEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the NPCConnectionEventArgs class.
        /// </summary>
        /// <param name="npcBasePlayer"><see cref="BasePlayer"/> of the NPC.</param>
        public NPCConnectionEventArgs(BasePlayer npcBasePlayer)
        {
            NPCBasePlayer = npcBasePlayer;
        }

        // Not using Player here as we don't delete the object when
        // the player fails to connect to the server.

        /// <summary>
        ///     Gets the NPC's <see cref="BasePlayer"/> trying to connect.
        /// </summary>
        public BasePlayer NPCBasePlayer { get; }
    }
}