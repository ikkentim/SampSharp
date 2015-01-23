// SampSharp
// Copyright 2015 Tim Potze
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
    ///     Provides data for the <see cref="BaseMode.PlayerEditAttachedObject" /> event.
    /// </summary>
    public class PlayerEditAttachedObjectEventArgs : PlayerEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the PlayerEditAttachedObjectEventArgs class.
        /// </summary>
        /// <param name="playerid">Id of the player.</param>
        /// <param name="response">EditObjectResponse.</param>
        /// <param name="index">Index of the attached object.</param>
        /// <param name="modelid">Model of the attached object.</param>
        /// <param name="boneid">Id of the bone the object was attached to.</param>
        /// <param name="offset">Offset of the attached object.</param>
        /// <param name="rotation">Rotation of the attached object.</param>
        /// <param name="scale">Scale of the attached object.</param>
        public PlayerEditAttachedObjectEventArgs(int playerid, EditObjectResponse response, int index, int modelid,
            int boneid, Vector offset, Vector rotation, Vector scale)
            : base(playerid)
        {
            EditObjectResponse = response;
            Index = index;
            ModelId = modelid;
            BoneId = boneid;
            Bone = (Bone) boneid;
            Offset = offset;
            Rotation = rotation;
            Scale = scale;
        }

        /// <summary>
        ///     Gets the EditObjectResponse.
        /// </summary>
        public EditObjectResponse EditObjectResponse { get; private set; }

        /// <summary>
        ///     Gets the index of the attachedobject.
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        ///     Gets the id of the model.
        /// </summary>
        public int ModelId { get; private set; }

        /// <summary>
        ///     Gets the boneid the object was attached to.
        /// </summary>
        public int BoneId { get; private set; }

        /// <summary>
        ///     Gets the Bone the object was attached to.
        /// </summary>
        public Bone Bone { get; private set; }

        /// <summary>
        ///     Gets the offset of the attached object.
        /// </summary>
        public Vector Offset { get; private set; }

        /// <summary>
        ///     Gets the rotation of the attached object.
        /// </summary>
        public Vector Rotation { get; private set; }

        /// <summary>
        ///     Gets the scale of the attached object.
        /// </summary>
        public Vector Scale { get; private set; }
    }
}