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

using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP.Middleware;
using SampSharp.Entities.SAMP.NativeComponents;
using static SampSharp.Entities.SAMP.SampEntities;

namespace SampSharp.Entities.SAMP
{
    /// <summary>
    /// Provides methods for enabling SA:MP systems in an <see cref="IEcsBuilder" /> instance.
    /// </summary>
    public static class SampEcsBuilderExtensions
    {
        internal static IEcsBuilder EnableWorld(this IEcsBuilder builder)
        {
            var server = builder.Services.GetService<IEntityManager>()
                .Create(null, ServerService.ServerId);
            server.AddComponent<NativeServer>();

            builder.Services.GetService<IEntityManager>()
                .Create(server, WorldService.WorldId)
                .AddComponent<NativeWorld>();

            return builder;
        }

        /// <summary>
        /// Enables all actor, player related SA:MP events.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableSampEvents(this IEcsBuilder builder)
        {
            return builder
                .EnableActorEvents()
                .EnablePlayerEvents()
                .EnableObjectEvents()
                .EnableRconEvents()
                .EnableVehicleEvents();
        }

        /// <summary>
        /// Enables all actor related SA:MP events.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableActorEvents(this IEcsBuilder builder)
        {
            builder.EnableEvent<int, int>("OnActorStreamIn");
            builder.EnableEvent<int, int>("OnActorStreamOut");

            builder.UseMiddleware<EntityMiddleware>("OnActorStreamIn", 0, ActorType, true);
            builder.UseMiddleware<EntityMiddleware>("OnActorStreamIn", 1, PlayerType, true);
            builder.UseMiddleware<EntityMiddleware>("OnActorStreamOut", 0, ActorType, true);
            builder.UseMiddleware<EntityMiddleware>("OnActorStreamOut", 1, PlayerType, true);

            return builder;
        }

        /// <summary>
        /// Enables all player related SA:MP events.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnablePlayerEvents(this IEcsBuilder builder)
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
            builder.EnableEvent<int, int, int, int, int, float, float, float, float, float, float, float, float, float>(
                "OnPlayerEditAttachedObject");
            builder.EnableEvent<int, int, int, int, float, float, float>("OnPlayerSelectObject");
            builder.EnableEvent<int, int, int, int, float, float, float>("OnPlayerWeaponShot");
            builder.EnableEvent<int, bool, int>("OnEnterExitModShop");
            builder.EnableEvent<int, int, int, int, string>("OnDialogResponse");
            builder.EnableEvent<int, string, int>("OnIncomingConnection"); // Don't swap out player id 

            void AddPlayerTarget(string callback)
            {
                builder.UseMiddleware<EntityMiddleware>(callback, 0, PlayerType, true);
            }

            builder.UseMiddleware<PlayerConnectMiddleware>("OnPlayerConnect");
            builder.UseMiddleware<PlayerDisconnectMiddleware>("OnPlayerDisconnect");
            AddPlayerTarget("OnPlayerSpawn");
            AddPlayerTarget("OnPlayerDeath");
            builder.UseMiddleware<EntityMiddleware>("OnPlayerDeath", 1, PlayerType, false);
            AddPlayerTarget("OnPlayerText");
            AddPlayerTarget("OnPlayerCommandText");
            AddPlayerTarget("OnPlayerRequestClass");
            AddPlayerTarget("OnPlayerEnterVehicle");
            builder.UseMiddleware<EntityMiddleware>("OnPlayerEnterVehicle", 1, VehicleType, true);
            AddPlayerTarget("OnPlayerExitVehicle");
            builder.UseMiddleware<EntityMiddleware>("OnPlayerExitVehicle", 1, VehicleType, true);
            AddPlayerTarget("OnPlayerStateChange");
            AddPlayerTarget("OnPlayerEnterCheckpoint");
            AddPlayerTarget("OnPlayerLeaveCheckpoint");
            AddPlayerTarget("OnPlayerEnterRaceCheckpoint");
            AddPlayerTarget("OnPlayerLeaveRaceCheckpoint");
            AddPlayerTarget("OnPlayerRequestSpawn");
            AddPlayerTarget("OnPlayerPickUpPickup");
            builder.UseMiddleware<EntityMiddleware>("OnPlayerPickUpPickup", 1, PickupType, true);
            AddPlayerTarget("OnPlayerSelectedMenuRow");
            AddPlayerTarget("OnPlayerExitedMenu");
            AddPlayerTarget("OnPlayerInteriorChange");
            AddPlayerTarget("OnPlayerKeyStateChange");
            AddPlayerTarget("OnPlayerUpdate");
            AddPlayerTarget("OnPlayerStreamIn");
            builder.UseMiddleware<EntityMiddleware>("OnPlayerStreamIn", 1, PlayerType, true);
            AddPlayerTarget("OnPlayerStreamOut");
            builder.UseMiddleware<EntityMiddleware>("OnPlayerStreamOut", 1, PlayerType, true);
            AddPlayerTarget("OnPlayerTakeDamage");
            builder.UseMiddleware<EntityMiddleware>("OnPlayerTakeDamage", 1, PlayerType, false);
            AddPlayerTarget("OnPlayerGiveDamage");
            builder.UseMiddleware<EntityMiddleware>("OnPlayerGiveDamage", 1, PlayerType, true);
            AddPlayerTarget("OnPlayerGiveDamageActor");
            builder.UseMiddleware<EntityMiddleware>("OnPlayerGiveDamageActor", 1, ActorType, true);
            AddPlayerTarget("OnPlayerClickMap");
            builder.UseMiddleware<TextDrawClickMiddleware>("OnPlayerClickTextDraw");
            builder.UseMiddleware<PlayerTextDrawMiddleware>("OnPlayerClickPlayerTextDraw");
            AddPlayerTarget("OnPlayerClickPlayer");
            builder.UseMiddleware<EntityMiddleware>("OnPlayerClickPlayer", 1, PlayerType, true);
            builder.UseMiddleware<PlayerEditObjectMiddleware>("OnPlayerEditObject");
            AddPlayerTarget("OnPlayerEditAttachedObject");
            builder.UseMiddleware<PlayerSelectObjectMiddleware>("OnPlayerSelectObject");
            builder.UseMiddleware<PlayerWeaponShotMiddleware>("OnPlayerWeaponShot");
            AddPlayerTarget("OnEnterExitModShop");
            AddPlayerTarget("OnDialogResponse");

            return builder;
        }

        /// <summary>
        /// Enables all object related SA:MP events.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableObjectEvents(this IEcsBuilder builder)
        {
            builder.EnableEvent<int>("OnObjectMoved");
            builder.EnableEvent<int, int>("OnPlayerObjectMoved");

            builder.UseMiddleware<EntityMiddleware>("OnObjectMoved", 0, ObjectType, true);
            builder.UseMiddleware<PlayerObjectMiddleware>("OnPlayerObjectMoved");
            return builder;
        }

        /// <summary>
        /// Enables all RCON related SA:MP events.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableRconEvents(this IEcsBuilder builder)
        {
            builder.EnableEvent<string>("OnRconCommand");
            builder.EnableEvent<string, string, bool>("OnRconLoginAttempt");

            return builder;
        }

        /// <summary>
        /// Enables all vehicle related SA:MP events.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IEcsBuilder EnableVehicleEvents(this IEcsBuilder builder)
        {
            builder.EnableEvent<int>("OnVehicleSpawn");
            builder.EnableEvent<int, int>("OnVehicleDeath");
            builder.EnableEvent<int, int, int>("OnVehicleMod");
            builder.EnableEvent<int, int, int>("OnVehiclePaintjob");
            builder.EnableEvent<int, int, int, int>("OnVehicleRespray");
            builder.EnableEvent<int, int>("OnVehicleDamageStatusUpdate");
            builder.EnableEvent<int, int>("OnVehicleStreamIn");
            builder.EnableEvent<int, int>("OnVehicleStreamOut");
            builder.EnableEvent<int, int, int>("OnVehicleSirenStateChange");
            builder.EnableEvent<int, int>("OnTrailerUpdate");
            builder.EnableEvent<int, int, int, float, float, float, float, float, float>("OnUnoccupiedVehicleUpdate");

            builder.UseMiddleware<EntityMiddleware>("OnVehicleSpawn", 0, VehicleType, true);
            builder.UseMiddleware<EntityMiddleware>("OnVehicleDeath", 0, VehicleType, true);
            builder.UseMiddleware<EntityMiddleware>("OnVehicleDeath", 1, PlayerType, false);
            builder.UseMiddleware<EntityMiddleware>("OnVehicleMod", 0, PlayerType, true);
            builder.UseMiddleware<EntityMiddleware>("OnVehicleMod", 1, VehicleType, true);
            builder.UseMiddleware<EntityMiddleware>("OnVehiclePaintjob", 0, PlayerType, true);
            builder.UseMiddleware<EntityMiddleware>("OnVehiclePaintjob", 1, VehicleType, true);
            builder.UseMiddleware<EntityMiddleware>("OnVehicleRespray", 0, PlayerType, true);
            builder.UseMiddleware<EntityMiddleware>("OnVehicleRespray", 1, VehicleType, true);
            builder.UseMiddleware<EntityMiddleware>("OnVehicleDamageStatusUpdate", 0, VehicleType, true);
            builder.UseMiddleware<EntityMiddleware>("OnVehicleDamageStatusUpdate", 1, PlayerType, true);
            builder.UseMiddleware<EntityMiddleware>("OnVehicleStreamIn", 0, VehicleType, true);
            builder.UseMiddleware<EntityMiddleware>("OnVehicleStreamIn", 1, PlayerType, true);
            builder.UseMiddleware<EntityMiddleware>("OnVehicleStreamOut", 0, VehicleType, true);
            builder.UseMiddleware<EntityMiddleware>("OnVehicleStreamOut", 1, PlayerType, true);
            builder.UseMiddleware<EntityMiddleware>("OnVehicleSirenStateChange", 0, PlayerType, true);
            builder.UseMiddleware<EntityMiddleware>("OnVehicleSirenStateChange", 1, VehicleType, true);
            builder.UseMiddleware<EntityMiddleware>("OnTrailerUpdate", 0, PlayerType, true);
            builder.UseMiddleware<EntityMiddleware>("OnTrailerUpdate", 1, VehicleType, true);
            builder.UseMiddleware<EntityMiddleware>("OnUnoccupiedVehicleUpdate", 0, VehicleType, true);
            builder.UseMiddleware<EntityMiddleware>("OnUnoccupiedVehicleUpdate", 1, PlayerType, true);

            return builder;
        }
    }
}