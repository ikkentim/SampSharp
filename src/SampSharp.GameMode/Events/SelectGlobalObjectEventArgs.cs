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
    ///     Provides data for the <see cref="BaseMode.PlayerSelectGlobalObject" />,
    ///     <see cref="BasePlayer.SelectGlobalObject" />
    ///     or <see cref="GlobalObject.Selected" /> event.
    /// </summary>
    public class SelectGlobalObjectEventArgs : PositionEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SelectGlobalObjectEventArgs" /> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="object">The global object.</param>
        /// <param name="modelid">The modelid.</param>
        /// <param name="position">The position.</param>
        public SelectGlobalObjectEventArgs(BasePlayer player, GlobalObject @object, int modelid, Vector3 position)
            : base(position)
        {
            Player = player;
            Object = @object;
            ModelId = modelid;
        }

        /*
         * Since the BaseMode.OnPlayerSelectGlobalObject can either have a GtaPlayer of GlobalObject instance as sender,
         * we add both to the event args so we can access what's not the sender.
         */

        /// <summary>
        ///     Gets the player.
        /// </summary>
        public BasePlayer Player { get; private set; }

        /// <summary>
        ///     Gets the global object.
        /// </summary>
        public GlobalObject Object { get; private set; }

        /// <summary>
        ///     Gets the model identifier.
        /// </summary>
        public int ModelId { get; private set; }
    }

    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerSelectPlayerObject" />,
    ///     <see cref="BasePlayer.SelectPlayerObject" />
    ///     or <see cref="PlayerObject.Selected" /> event.
    /// </summary>
    public class SelectPlayerObjectEventArgs : PositionEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SelectPlayerObjectEventArgs" /> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="object">The player object.</param>
        /// <param name="modelid">The modelid.</param>
        /// <param name="position">The position.</param>
        public SelectPlayerObjectEventArgs(BasePlayer player, PlayerObject @object, int modelid, Vector3 position)
            : base(position)
        {
            Player = player;
            Object = @object;
            ModelId = modelid;
        }

        /// <summary>
        ///     Gets the player.
        /// </summary>
        public BasePlayer Player { get; private set; }

        /// <summary>
        ///     Gets the player object.
        /// </summary>
        public PlayerObject Object { get; private set; }

        /// <summary>
        ///     Gets the model identifier.
        /// </summary>
        public int ModelId { get; private set; }
    }
}