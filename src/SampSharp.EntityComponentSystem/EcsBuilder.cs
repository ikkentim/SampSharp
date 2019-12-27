using System;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.EntityComponentSystem.Events;
using SampSharp.EntityComponentSystem.Systems;

namespace SampSharp.EntityComponentSystem
{
    public class EcsBuilder : IEcsBuilder
    {
        private readonly IEventService _eventService;
        private readonly ISystemRegistry _systemRegistry;

        public EcsBuilder(IServiceProvider services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            
            _systemRegistry = services.GetRequiredService<ISystemRegistry>();
            _eventService = services.GetRequiredService<IEventService>();
        }

        public IServiceProvider Services { get; }

        public IEcsBuilder Use(string name, Func<EventDelegate, EventDelegate> middleware)
        {
            _eventService.Use(name, middleware);

            return this;
        }

        public IEcsBuilder UseSystem(Type systemType)
        {
            if (systemType == null) throw new ArgumentNullException(nameof(systemType));

            _systemRegistry.Add(systemType);

            return this;
        }
    }
}