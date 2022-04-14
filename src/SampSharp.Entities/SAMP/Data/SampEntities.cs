// SampSharp
// Copyright 2020 Tim Potze
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

namespace SampSharp.Entities.SAMP
{
    /// <summary>
    /// Contains functions for constructing <see cref="EntityId" /> values for SA:MP native entities.
    /// </summary>
    public static class SampEntities
    {
        /// <summary>
        /// The SA:MP actor entity type identifier.
        /// </summary>
        [EntityType(InvalidHandle = NativeActor.InvalidId)]
        public static readonly Guid ActorType = new("CAD0D9CC-EA4A-4D21-9D8A-DF3BB03F59DD");

        /// <summary>
        /// The SA:MP player entity type identifier.
        /// </summary>
        [EntityType(InvalidHandle = NativePlayer.InvalidId)]
        public static readonly Guid PlayerType = new("C96C8A5A-80D6-40EF-9308-4AF28CBE9657");

        /// <summary>
        /// The SA:MP vehicle entity type identifier.
        /// </summary>
        [EntityType(InvalidHandle = NativeVehicle.InvalidId)]
        public static readonly Guid VehicleType = new("877A5625-9F2A-4C92-BC83-1C6C220A9D05");

        /// <summary>
        /// The SA:MP gang zone entity type identifier.
        /// </summary>
        [EntityType(InvalidHandle = NativeGangZone.InvalidId)]
        public static readonly Guid GangZoneType = new("D607F200-268E-4614-AEAB-8158067767BA");

        /// <summary>
        /// The SA:MP object entity type identifier.
        /// </summary>
        [EntityType(InvalidHandle = NativeObject.InvalidId)]
        public static readonly Guid ObjectType = new("52F9EC25-82E5-4B17-B19E-76DA67965D87");

        /// <summary>
        /// The SA:MP pickup entity type identifier.
        /// </summary>
        [EntityType(InvalidHandle = NativePickup.InvalidId)]
        public static readonly Guid PickupType = new("16712D3F-8FFF-4871-A0B7-C2A163314E11");

        /// <summary>
        /// The SA:MP player object entity type identifier.
        /// </summary>
        [EntityType(InvalidHandle = NativePlayerObject.InvalidId)]
        public static readonly Guid PlayerObjectType = new("0A728950-C2CD-4379-95E9-B564E1D430ED");

        /// <summary>
        /// The SA:MP player text label entity type identifier.
        /// </summary>
        [EntityType(InvalidHandle = NativePlayerTextDraw.InvalidId)]
        public static readonly Guid PlayerTextLabelType = new("B7B43DE2-6C37-4D78-BBA7-5F394EBAFCD6");

        /// <summary>
        /// The SA:MP text label entity type identifier.
        /// </summary>
        [EntityType(InvalidHandle = NativeTextLabel.InvalidId)]
        public static readonly Guid TextLabelType = new("CD8A9F9B-9207-4E98-870F-1341B61BE06E");
        
        /// <summary>
        /// The SA:MP player text draw entity type identifier.
        /// </summary>
        [EntityType(InvalidHandle = NativePlayerTextDraw.InvalidId)]
        public static readonly Guid PlayerTextDrawType = new("2EBBB103-5FFC-44B2-97A5-FA5295A4BD14");
        
        /// <summary>
        /// The SA:MP text draw entity type identifier.
        /// </summary>
        [EntityType(InvalidHandle = NativeTextDraw.InvalidId)]
        public static readonly Guid TextDrawType = new("F7D5A8C9-2066-4BB1-A1A8-4C185C15D95F");

        /// <summary>
        /// The SA:MP menu entity type identifier.
        /// </summary>
        [EntityType(InvalidHandle = NativeObject.InvalidId)]
        public static readonly Guid MenuType = new("0AED0820-6CCF-4ED4-A879-8B161773769E");

        /// <summary>
        /// Gets a actor entity identifier based on an integer actor identifier.
        /// </summary>
        /// <param name="actorId">The actor identifier.</param>
        /// <returns>The entity identifier.</returns>
        public static EntityId GetActorId(int actorId)
        {
            return new EntityId(ActorType, actorId);
        }

        /// <summary>
        /// Gets a player entity identifier based on an integer player identifier.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <returns>The entity identifier.</returns>
        public static EntityId GetPlayerId(int playerId)
        {
            return new EntityId(PlayerType, playerId);
        }

        /// <summary>
        /// Gets a vehicle entity identifier based on an integer vehicle identifier.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>The entity identifier.</returns>
        public static EntityId GetVehicleId(int vehicleId)
        {
            return new EntityId(VehicleType, vehicleId);
        }

        /// <summary>
        /// Gets a gang zone entity identifier based on an integer gang zone identifier.
        /// </summary>
        /// <param name="gangZoneId">The gang zone identifier.</param>
        /// <returns>The entity identifier.</returns>
        public static EntityId GetGangZoneId(int gangZoneId)
        {
            return new EntityId(GangZoneType, gangZoneId);
        }

        /// <summary>
        /// Gets a object entity identifier based on an integer object identifier.
        /// </summary>
        /// <param name="objectId">The object identifier.</param>
        /// <returns>The entity identifier.</returns>
        public static EntityId GetObjectId(int objectId)
        {
            return new EntityId(ObjectType, objectId);
        }

        /// <summary>
        /// Gets a player object entity identifier based on an integer player object identifier.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <param name="playerObjectId">The player object identifier.</param>
        /// <returns>The entity identifier.</returns>
        public static EntityId GetPlayerObjectId(int playerId, int playerObjectId)
        {
            return new EntityId(PlayerObjectType, playerObjectId * SampLimits.MaxPlayers + playerId);
        }

        /// <summary>
        /// Gets a pickup entity identifier based on an integer pickup identifier.
        /// </summary>
        /// <param name="pickupId">The pickup identifier.</param>
        /// <returns>The entity identifier.</returns>
        public static EntityId GetPickupId(int pickupId)
        {
            return new EntityId(PickupType, pickupId);
        }

        /// <summary>
        /// Gets a player text label entity identifier based on an integer player text label identifier.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <param name="playerTextLabelId">The player text label identifier.</param>
        /// <returns>The entity identifier.</returns>
        public static EntityId GetPlayerTextLabelId(int playerId, int playerTextLabelId)
        {
            return new EntityId(PlayerTextLabelType, playerTextLabelId * SampLimits.MaxPlayers + playerId);
        }

        /// <summary>
        /// Gets a text label entity identifier based on an integer text label identifier.
        /// </summary>
        /// <param name="textLabelId">The text label identifier.</param>
        /// <returns>The entity identifier.</returns>
        public static EntityId GetTextLabelId(int textLabelId)
        {
            return new EntityId(TextLabelType, textLabelId);
        }

        /// <summary>
        /// Gets a player text draw entity identifier based on an integer player text draw identifier.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <param name="playerTextDrawId">The player text draw identifier.</param>
        /// <returns>The entity identifier.</returns>
        public static EntityId GetPlayerTextDrawId(int playerId, int playerTextDrawId)
        {
            return new EntityId(PlayerTextDrawType, playerTextDrawId * SampLimits.MaxPlayers + playerId);
        }
        
        /// <summary>
        /// Gets a text draw entity identifier based on an integer text draw identifier.
        /// </summary>
        /// <param name="textDrawId">The text draw identifier.</param>
        /// <returns>The entity identifier.</returns>
        public static EntityId GetTextDrawId(int textDrawId)
        {
            return new EntityId(TextDrawType, textDrawId);
        }

        /// <summary>
        /// Gets a menu entity identifier based on an integer menu identifier.
        /// </summary>
        /// <param name="menuId">The menu identifier.</param>
        /// <returns>The entity identifier.</returns>
        public static EntityId GetMenuId(int menuId)
        {
            return new EntityId(MenuType, menuId);
        }
    }
}