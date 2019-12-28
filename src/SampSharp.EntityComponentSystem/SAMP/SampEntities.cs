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

using System;
using SampSharp.EntityComponentSystem.Entities;

namespace SampSharp.EntityComponentSystem.SAMP
{
    /// <summary>
    /// Contains functions for constructing <see cref="EntityId" /> values for SA:MP native entities.
    /// </summary>
    public static class SampEntities
    {
        /// <summary>
        /// The SA:MP player entity type identifier.
        /// </summary>
        public static readonly Guid PlayerType = new Guid("C96C8A5A-80D6-40EF-9308-4AF28CBE9657");

        /// <summary>
        /// The SA:MP vehicle entity type identifier.
        /// </summary>
        public static readonly Guid VehicleType = new Guid("877A5625-9F2A-4C92-BC83-1C6C220A9D05");

        /// <summary>
        /// Gets a player entity identifier based on an integer player identifier.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <returns>The entity identifier.</returns>
        public static EntityId GetPlayerId(int playerId)
        {
            return new EntityId(PlayerType, playerId);
        }
    }
}