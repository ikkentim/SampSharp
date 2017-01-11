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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerEditPlayerObject" />, <see cref="BasePlayer.EditPlayerObject" /> or
    ///     <see cref="PlayerObject.Edited" /> event.
    /// </summary>
    public class EditPlayerObjectEventArgs : PositionEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EditPlayerObjectEventArgs" /> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="object">The global object.</param>
        /// <param name="response">The response.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        public EditPlayerObjectEventArgs(BasePlayer player, PlayerObject @object, EditObjectResponse response,
            Vector3 position, Vector3 rotation)
            : base(position)
        {
            Player = player;
            Object = @object;
            EditObjectResponse = response;
            Rotation = rotation;
        }

        /// <summary>
        ///     Gets the player.
        /// </summary>
        public BasePlayer Player { get; private set; }

        /// <summary>
        ///     Gets the global object.
        /// </summary>
        public PlayerObject Object { get; private set; }

        /// <summary>
        ///     Gets the edit object response.
        /// </summary>
        public EditObjectResponse EditObjectResponse { get; private set; }

        /// <summary>
        ///     Gets the rotation.
        /// </summary>
        public Vector3 Rotation { get; private set; }
    }
}