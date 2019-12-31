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
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.Events;

namespace SampSharp.Entities
{
    /// <summary>
    /// Represents the ECS system builder.
    /// </summary>
    /// <seealso cref="IEcsBuilder" />
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