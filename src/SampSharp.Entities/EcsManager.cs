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
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Core;
using SampSharp.Core.Logging;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.Entities
{
    /// <summary>
    /// Represents the manager of the EntityComponentSystem which provides the game mode routines for SampSharp.
    /// </summary>
    public class EcsManager : IGameModeProvider
    {
        private readonly IStartup _startup;
        private IServiceProvider _serviceProvider;
        private ISystemRegistry _systemRegistry;

        /// <summary>
        /// Initializes a new instance of the <see cref="EcsManager" /> class.
        /// </summary>
        /// <param name="startup">The startup configuration for the game mode.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="startup" /> is null.</exception>
        public EcsManager(IStartup startup)
        {
            _startup = startup ?? throw new ArgumentNullException(nameof(startup));
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }

        /// <inheritdoc />
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
            _systemRegistry = _serviceProvider.GetRequiredService<ISystemRegistry>();

            AddWrappedSystemTypes();

            Configure();
        }

        /// <inheritdoc />
        public void Tick()
        {
            var types = _systemRegistry.Get(typeof(ITickingSystem));

            foreach (var type in types)
                if (_serviceProvider.GetService(type) is ITickingSystem system)
                    system.Tick();
        }

        private void AddWrappedSystemTypes()
        {
            foreach (var wrapper in _serviceProvider.GetServices<SystemTypeWrapper>())
                _systemRegistry.Add(wrapper.Type);
        }

        private void Configure(IServiceCollection services)
        {
            _startup.Configure(
                services.AddSingleton<IEventService, EventService>()
                    .AddSingleton<ISystemRegistry, SystemRegistry>()
                    .AddSingleton<IEntityManager, EntityManager>()
                    .AddSingleton<IServerService, ServerService>()
                    .AddSingleton<IWorldService, WorldService>()
                    .AddSingleton<IVehicleInfoService, VehicleInfoService>()
                    .AddSingleton<IPlayerCommandService, PlayerCommandService>()
                    .AddSingleton<IRconCommandService, RconCommandService>()
                    .AddTransient<IDialogService, DialogService>()
                    .AddTransient(typeof(INativeProxy<>), typeof(NativeProxy<>))
                    .AddSystem<DialogSystem>()
            );
        }

        private void Configure()
        {
            _startup.Configure(
                new EcsBuilder(_serviceProvider)
                    .EnableEvent("OnGameModeInit")
                    .EnableEvent("OnGameModeExit")
            );
        }
    }
}