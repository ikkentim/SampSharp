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
            _eventService.Load("OnPlayerText", typeof(int), typeof(string));
            
            builder.UseMiddleware<PlayerConnectMiddleware>("OnPlayerConnect");

            builder.UseMiddleware<EntityMiddleware>("OnPlayerText", 0,
                (Func<int, EntityId>) SampEntities.GetPlayerId);
        }
    }
}