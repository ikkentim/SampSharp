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
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerRequestClass" /> or <see cref="BasePlayer.RequestClass" /> event.
    /// </summary>
    public class RequestClassEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the RequestClassEventArgs class.
        /// </summary>
        /// <param name="classid">The id of the class.</param>
        public RequestClassEventArgs(int classid)
        {
            ClassId = classid;
        }

        /// <summary>
        ///     Gets the id of the class.
        /// </summary>
        public int ClassId { get; private set; }

        /// <summary>
        ///     Gets or sets whether the player is prevented from spawning.
        /// </summary>
        public bool PreventSpawning { get; set; }
    }
}