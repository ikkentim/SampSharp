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

namespace SampSharp.GameMode.Definitions
{
    /// <summary>
    ///     Misc defined values.
    /// </summary>
    public static class Misc
    {
        /// <summary>
        ///     Invalid player id.
        /// </summary>
        public const int InvalidPlayerId = 0xFFFF;

        /// <summary>
        ///     Invalid vehicle id.
        /// </summary>
        public const int InvalidVehicleId = 0xFFFF;

        /// <summary>
        ///     No team.
        /// </summary>
        public const int NoTeam = 255;

        /// <summary>
        ///     Invalid object id.
        /// </summary>
        public const int InvalidObjectId = 0xFFFF;

        /// <summary>
        ///     Invalid menu id.
        /// </summary>
        public const int InvalidMenu = 0xFF;

        /// <summary>
        ///     Invalid textdraw id.
        /// </summary>
        public const int InvalidTextDraw = 0xFFFF;

        /// <summary>
        ///     Invalid gangzone id.
        /// </summary>
        public const int InvalidGangZone = -1;

        /// <summary>
        ///     Invalid 3D textlabel id.
        /// </summary>
        public const int Invalid_3DTextId = 0xFFFF;

        /// <summary>
        ///     Max number of attachedobjects attached to a player.
        /// </summary>
        public const int MaxPlayerAttachedObjects = 10;
    }
}