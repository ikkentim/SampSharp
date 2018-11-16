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
    ///     Provides data for the _not_yet_ event.
    /// </summary>
    public class ActorStreamEventArgs : PlayerEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VehicleStreamEventArgs" /> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="actor">The actor.</param>
        /// <param name="mode">Streaming IN or OUT.</param>
        public ActorStreamEventArgs(BasePlayer player, Actor actor, StreamMode mode) : base(player)
        {
            Actor = actor;
            Mode = mode;
        }
        
        /// <summary>
        ///     Gets the actor
        /// </summary>
        public Actor Actor { get; private set; }

        /// <summary>
        ///     Gets the stream mode.
        /// </summary>
        public StreamMode Mode { get; private set; }
    }
}