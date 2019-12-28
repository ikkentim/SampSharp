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
using SampSharp.Core;
using SampSharp.Core.Logging;
using SampSharp.EntityComponentSystem.Entities;
using SampSharp.EntityComponentSystem.Events;
using SampSharp.EntityComponentSystem.SAMP;
using SampSharp.EntityComponentSystem.Systems;

namespace SampSharp.EntityComponentSystem
{
    /// <summary>
    /// Represents the manager of the EntityComponentSystem which provides the game mode routines for SampSharp.
    /// </summary>
    public class EcsManager : IGameModeProvider
    {
        private readonly IStartup _startup;
        private IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="EcsManager" /> class.
        /// </summary>
        /// <param name="startup">The startup configuration for the game mode.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="startup" /> is null.</exception>
        public EcsManager(IStartup startup)
        {
            _startup = startup ?? throw new ArgumentNullException(nameof(startup));
        }

        public void Dispose()
        {
        }

        public void Initialize(IGameModeClient client)
        {
            client.UnhandledException += (sender, args) =>
            {
                CoreLog.Log(CoreLogLevel.Error, "Unhandled exception: " + args.Exception);
            };

            var services = new ServiceCollection();
            services.AddSingleton(client);
            Configure(services);

            _serviceProvider = services.BuildServiceProvider();

            Configure();
        }

        public void Tick()
        {
            // TODO: Implement
        }

        private void Configure(IServiceCollection services)
        {
            services.AddSingleton<IEventService, EventService>();
            services.AddSingleton<ISystemRegistry, SystemRegistry>();
            services.AddSingleton<IEntityManager, EntityManager>();

            services.AddSingleton<PlayerSystem>();

            _startup.Configure(services);
        }

        private void Configure()
        {
            var eventService = _serviceProvider.GetRequiredService<IEventService>();

            // Required callbacks
            eventService.Load("OnGameModeInit");
            eventService.Load("OnGameModeExit");

            var builder = new EcsBuilder(_serviceProvider);

            builder.UseSystem<PlayerSystem>();

            // Configure startup
            _startup.Configure(builder);

            // Configure services
            var registry = _serviceProvider.GetRequiredService<ISystemRegistry>();

            foreach (var serviceType in registry.ConfiguringSystems)
            {
                var service = _serviceProvider.GetService(serviceType) as IConfiguringSystem;
                service?.Configure(builder);
            }
        }
    }
}