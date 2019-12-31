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
using SampSharp.Entities.Events;
using SampSharp.Entities.SAMP.Middleware;

namespace SampSharp.Entities.SAMP.Systems
{
    internal class PlayerSystem : IConfiguringSystem
    {
        public void Configure(IEcsBuilder builder)
        {
            builder.EnableEvent<int>("OnPlayerConnect");
            builder.EnableEvent<int, int>("OnPlayerDisconnect");
            builder.EnableEvent<int>("OnPlayerSpawn");
            builder.EnableEvent<int, int, int>("OnPlayerDeath");
            builder.EnableEvent<int, string>("OnPlayerText");
            builder.EnableEvent<int, string>("OnPlayerCommandText");
            builder.EnableEvent<int, int>("OnPlayerRequestClass");
            builder.EnableEvent<int, int, bool>("OnPlayerEnterVehicle");
            builder.EnableEvent<int, int>("OnPlayerExitVehicle");
            builder.EnableEvent<int, int, int>("OnPlayerStateChange");
            builder.EnableEvent<int>("OnPlayerEnterCheckpoint");
            builder.EnableEvent<int>("OnPlayerLeaveCheckpoint");
            builder.EnableEvent<int>("OnPlayerEnterRaceCheckpoint");
            builder.EnableEvent<int>("OnPlayerLeaveRaceCheckpoint");
            builder.EnableEvent<int>("OnPlayerRequestSpawn");
            builder.EnableEvent<int, int>("OnPlayerObjectMoved");
            builder.EnableEvent<int, int>("OnPlayerPickUpPickup");
            builder.EnableEvent<int, int>("OnPlayerSelectedMenuRow");
            builder.EnableEvent<int>("OnPlayerExitedMenu");
            builder.EnableEvent<int, int, int>("OnPlayerInteriorChange");
            builder.EnableEvent<int, int, int>("OnPlayerKeyStateChange");
            builder.EnableEvent<int>("OnPlayerUpdate");
            builder.EnableEvent<int, int>("OnPlayerStreamIn");
            builder.EnableEvent<int, int>("OnPlayerStreamOut");
            builder.EnableEvent<int, int, float, int, int>("OnPlayerTakeDamage");
            builder.EnableEvent<int, int, float, int, int>("OnPlayerGiveDamage");
            builder.EnableEvent<int, int, float, int, int>("OnPlayerGiveDamageActor");
            builder.EnableEvent<int, float, float, float>("OnPlayerClickMap");
            builder.EnableEvent<int, int>("OnPlayerClickTextDraw");
            builder.EnableEvent<int, int>("OnPlayerClickPlayerTextDraw");
            builder.EnableEvent<int, int, int>("OnPlayerClickPlayer");
            builder.EnableEvent<int, bool, int, int, float, float, float, float, float, float>("OnPlayerEditObject");
            builder.EnableEvent<int, int, int, int, int, float, float, float, float, float, float, float, float, float>("OnPlayerEditAttachedObject");
            builder.EnableEvent<int, int, int, int, float, float, float>("OnPlayerSelectObject");
            builder.EnableEvent<int, int, int, int, float, float, float>("OnPlayerWeaponShot");
            
            builder.UseMiddleware<PlayerConnectMiddleware>("OnPlayerConnect");
            builder.UseMiddleware<PlayerDisconnectMiddleware>("OnPlayerDisconnect");

            void AddPlayerTarget(string callback) =>
                builder.UseMiddleware<EntityMiddleware>(callback, 0,
                    (Func<int, EntityId>) SampEntities.GetPlayerId, true, true, callback.Replace("OnPlayer", "On"));

            AddPlayerTarget("OnPlayerSpawn");
            AddPlayerTarget("OnPlayerDeath");
            AddPlayerTarget("OnPlayerText");
            AddPlayerTarget("OnPlayerCommandText");
            AddPlayerTarget("OnPlayerRequestClass");
            AddPlayerTarget("OnPlayerEnterVehicle");
            AddPlayerTarget("OnPlayerExitVehicle");
            AddPlayerTarget("OnPlayerStateChange");
            AddPlayerTarget("OnPlayerEnterCheckpoint");
            AddPlayerTarget("OnPlayerLeaveCheckpoint");
            AddPlayerTarget("OnPlayerEnterRaceCheckpoint");
            AddPlayerTarget("OnPlayerLeaveRaceCheckpoint");
            AddPlayerTarget("OnPlayerRequestSpawn");
            AddPlayerTarget("OnPlayerObjectMoved");
            AddPlayerTarget("OnPlayerPickUpPickup");
            AddPlayerTarget("OnPlayerSelectedMenuRow");
            AddPlayerTarget("OnPlayerExitedMenu");
            AddPlayerTarget("OnPlayerInteriorChange");
            AddPlayerTarget("OnPlayerKeyStateChange");
            AddPlayerTarget("OnPlayerUpdate");
            AddPlayerTarget("OnPlayerStreamIn");
            AddPlayerTarget("OnPlayerStreamOut");
            AddPlayerTarget("OnPlayerTakeDamage");
            AddPlayerTarget("OnPlayerGiveDamage");
            AddPlayerTarget("OnPlayerGiveDamageActor");
            AddPlayerTarget("OnPlayerClickMap");
            AddPlayerTarget("OnPlayerClickTextDraw");
            AddPlayerTarget("OnPlayerClickPlayerTextDraw");
            AddPlayerTarget("OnPlayerClickPlayer");
            AddPlayerTarget("OnPlayerEditObject");
            AddPlayerTarget("OnPlayerEditAttachedObject");
            AddPlayerTarget("OnPlayerSelectObject");
            AddPlayerTarget("OnPlayerWeaponShot");
        }


    }
}