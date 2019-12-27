using System;
using SampSharp.Core.Logging;
using SampSharp.EntityComponentSystem.Entities;
using SampSharp.EntityComponentSystem.Events;
using SampSharp.EntityComponentSystem.Systems;

namespace SampSharp.EntityComponentSystem.SAMP
{
    public class PlayerSystem : IConfiguringSystem
    {
        private readonly IEventService _eventService;

        public PlayerSystem(IEventService eventService)
        {
            _eventService = eventService;
        }

        public void Configure(IEcsBuilder builder)
        {
            _eventService.Load("OnPlayerConnect", typeof(int));
            _eventService.Load("OnPlayerDisconnect", typeof(int), typeof(int));
            _eventService.Load("OnPlayerSpawn", typeof(int));
            _eventService.Load("OnPlayerDeath", typeof(int), typeof(int), typeof(int));
            _eventService.Load("OnPlayerText", typeof(int), typeof(string));
            _eventService.Load("OnPlayerCommandText", typeof(int), typeof(string));
            _eventService.Load("OnPlayerRequestClass", typeof(int), typeof(int));
            _eventService.Load("OnPlayerEnterVehicle", typeof(int), typeof(int), typeof(bool));
            _eventService.Load("OnPlayerExitVehicle", typeof(int), typeof(int));
            _eventService.Load("OnPlayerStateChange", typeof(int), typeof(int), typeof(int));
            _eventService.Load("OnPlayerEnterCheckpoint", typeof(int));
            _eventService.Load("OnPlayerLeaveCheckpoint", typeof(int));
            _eventService.Load("OnPlayerEnterRaceCheckpoint", typeof(int));
            _eventService.Load("OnPlayerLeaveRaceCheckpoint", typeof(int));
            _eventService.Load("OnPlayerRequestSpawn", typeof(int));
            _eventService.Load("OnPlayerObjectMoved", typeof(int), typeof(int));
            _eventService.Load("OnPlayerPickUpPickup", typeof(int), typeof(int));
            _eventService.Load("OnPlayerSelectedMenuRow", typeof(int), typeof(int));
            _eventService.Load("OnPlayerExitedMenu", typeof(int));
            _eventService.Load("OnPlayerInteriorChange", typeof(int), typeof(int), typeof(int));
            _eventService.Load("OnPlayerKeyStateChange", typeof(int), typeof(int), typeof(int));
            _eventService.Load("OnPlayerUpdate", typeof(int));
            _eventService.Load("OnPlayerStreamIn", typeof(int), typeof(int));
            _eventService.Load("OnPlayerStreamOut", typeof(int), typeof(int));
            _eventService.Load("OnPlayerTakeDamage", typeof(int), typeof(int), typeof(float), typeof(int), typeof(int));
            _eventService.Load("OnPlayerGiveDamage", typeof(int), typeof(int), typeof(float), typeof(int), typeof(int));
            _eventService.Load("OnPlayerGiveDamageActor", typeof(int), typeof(int), typeof(float), typeof(int),
                typeof(int));
            _eventService.Load("OnPlayerClickMap", typeof(int), typeof(float), typeof(float), typeof(float));
            _eventService.Load("OnPlayerClickTextDraw", typeof(int), typeof(int));
            _eventService.Load("OnPlayerClickPlayerTextDraw", typeof(int), typeof(int));
            _eventService.Load("OnPlayerClickPlayer", typeof(int), typeof(int), typeof(int));
            _eventService.Load("OnPlayerEditObject", typeof(int), typeof(bool), typeof(int), typeof(int), typeof(float),
                typeof(float), typeof(float), typeof(float), typeof(float), typeof(float));
            _eventService.Load("OnPlayerEditAttachedObject", typeof(int), typeof(int), typeof(int), typeof(int),
                typeof(int), typeof(float), typeof(float), typeof(float), typeof(float), typeof(float), typeof(float),
                typeof(float), typeof(float), typeof(float));
            _eventService.Load("OnPlayerSelectObject", typeof(int), typeof(int), typeof(int), typeof(int),
                typeof(float), typeof(float), typeof(float));
            _eventService.Load("OnPlayerWeaponShot", typeof(int), typeof(int), typeof(int), typeof(int), typeof(float),
                typeof(float), typeof(float));

            builder.UseMiddleware<PlayerConnectMiddleware>("OnPlayerConnect");

            AddPlayerTarget(builder, "OnPlayerDisconnect"); // TODO: Destroy entity after disconnect
            AddPlayerTarget(builder, "OnPlayerSpawn");
            AddPlayerTarget(builder, "OnPlayerDeath");
            AddPlayerTarget(builder, "OnPlayerText");
            AddPlayerTarget(builder, "OnPlayerCommandText");
            AddPlayerTarget(builder, "OnPlayerRequestClass");
            AddPlayerTarget(builder, "OnPlayerEnterVehicle");
            AddPlayerTarget(builder, "OnPlayerExitVehicle");
            AddPlayerTarget(builder, "OnPlayerStateChange");
            AddPlayerTarget(builder, "OnPlayerEnterCheckpoint");
            AddPlayerTarget(builder, "OnPlayerLeaveCheckpoint");
            AddPlayerTarget(builder, "OnPlayerEnterRaceCheckpoint");
            AddPlayerTarget(builder, "OnPlayerLeaveRaceCheckpoint");
            AddPlayerTarget(builder, "OnPlayerRequestSpawn");
            AddPlayerTarget(builder, "OnPlayerObjectMoved");
            AddPlayerTarget(builder, "OnPlayerPickUpPickup");
            AddPlayerTarget(builder, "OnPlayerSelectedMenuRow");
            AddPlayerTarget(builder, "OnPlayerExitedMenu");
            AddPlayerTarget(builder, "OnPlayerInteriorChange");
            AddPlayerTarget(builder, "OnPlayerKeyStateChange");
            AddPlayerTarget(builder, "OnPlayerUpdate");
            AddPlayerTarget(builder, "OnPlayerStreamIn");
            AddPlayerTarget(builder, "OnPlayerStreamOut");
            AddPlayerTarget(builder, "OnPlayerTakeDamage");
            AddPlayerTarget(builder, "OnPlayerGiveDamage");
            AddPlayerTarget(builder, "OnPlayerGiveDamageActor");
            AddPlayerTarget(builder, "OnPlayerClickMap");
            AddPlayerTarget(builder, "OnPlayerClickTextDraw");
            AddPlayerTarget(builder, "OnPlayerClickPlayerTextDraw");
            AddPlayerTarget(builder, "OnPlayerClickPlayer");
            AddPlayerTarget(builder, "OnPlayerEditObject");
            AddPlayerTarget(builder, "OnPlayerEditAttachedObject");
            AddPlayerTarget(builder, "OnPlayerSelectObject");
            AddPlayerTarget(builder, "OnPlayerWeaponShot");
        }

        private void AddPlayerTarget(IEcsBuilder builder, string callback)
        {
            builder.UseMiddleware<EntityMiddleware>(callback, 0,
                (Func<int, EntityId>) SampEntities.GetPlayerId, true, callback.Replace("OnPlayer", "On"));
        }
    }
}