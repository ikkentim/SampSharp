// SampSharp
// Copyright 2019 Tim Potze
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

namespace SampSharp.EntityComponentSystem.SAMP
{
    /// <summary>
    /// Contains limits of SA:MP.
    /// </summary>
    public static class SampLimits
    {
        /// <summary>
        /// Maximum number of players which can exist.
        /// </summary>
        public const int MaxPlayers = 1000;

        /// <summary>
        /// Maximum number of actors which can exist.
        /// </summary>
        public const int MaxActors = 1000;

        /// <summary>
        /// Maximum number of vehicles which can exist.
        /// </summary>
        public const int MaxVehicles = 2000;

        /// <summary>
        /// Maximum number of global objects which can exist.
        /// </summary>
        public const int MaxGlobalObjects = 1000;

        /// <summary>
        /// Maximum number of per-player objects which can exist per player.
        /// </summary>
        public const int MaxPlayerObjects = 1000;

        /// <summary>
        /// Maximum number of attached objects attached to a player.
        /// </summary>
        public const int MaxPlayerAttachedObjects = 10;

        /// <summary>
        /// Maximum length of a player's name.
        /// </summary>
        public const int MaxPlayerNameLength = 24;

        /// <summary>
        /// Maximum length of the text in a player chat bubble.
        /// </summary>
        public const int MaxPlayerChatBubbleLength = 144;
    }
}