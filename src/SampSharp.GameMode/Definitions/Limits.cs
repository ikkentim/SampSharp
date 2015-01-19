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
    ///     Contains all limit definitions.
    /// </summary>
    public static class Limits
    {
        /// <summary>
        ///     This is the number of attached indexes available ie 10 = 0-9
        /// </summary>
        public const int MaxPlayerAttachedObjects = 10;

        /// <summary>
        ///     The maximum length of chatbubble text.
        /// </summary>
        public const int MaxChatbubbleLength = 144;

        /// <summary>
        ///     The maximum length of a playername.
        /// </summary>
        public const int MaxPlayerName = 24;

        /// <summary>
        ///     The maximum number of players.
        /// </summary>
        public const int MaxPlayers = 500;

        /// <summary>
        ///     The maximum number of vehicles.
        /// </summary>
        public const int MaxVehicles = 2000;

        /// <summary>
        ///     The maximum number of global objects.
        /// </summary>
        public const int MaxObjects = 1000;

        /// <summary>
        ///     The maximum number of gangzones.
        /// </summary>
        public const int MaxGangZones = 1024;

        /// <summary>
        ///     The maximum number of textdraws.
        /// </summary>
        public const int MaxTextDraws = 2048;

        /// <summary>
        ///     The maximum number of player-textdraws.
        /// </summary>
        public const int MaxPlayerTextDraws = 256;

        /// <summary>
        ///     The maximum number of menus.
        /// </summary>
        public const int MaxMenus = 128;

        /// <summary>
        ///     The maximum number of global 3D textlabels.
        /// </summary>
        public const int Max_3DTextGlobal = 1024;

        /// <summary>
        ///     The maximum number of player 3D textlabels.
        /// </summary>
        public const int Max_3DTextPlayer = 1024;

        /// <summary>
        ///     The maximum number of pickups.
        /// </summary>
        public const int MaxPickups = 4096;
    }
}