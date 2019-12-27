using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Core;
using SampSharp.Core.Logging;
using SampSharp.EntityComponentSystem.Entities;
using SampSharp.EntityComponentSystem.Events;
using SampSharp.EntityComponentSystem.SAMP;
using SampSharp.EntityComponentSystem.Systems;

namespace SampSharp.EntityComponentSystem
{
    public class EcsManager : IGameModeProvider
	{
        private readonly IStartup _startup;
        private IServiceProvider _serviceProvider;

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
