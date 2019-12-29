using System;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.EntityComponentSystem;
using TestMode.EntityComponentSystem.Services;
using TestMode.EntityComponentSystem.Systems;

namespace TestMode.EntityComponentSystem
{
    public class TestStartup : IStartup
    {
        public void Configure(IServiceCollection services)
        {
            // Load services (e.g. repositories), systems, etc.
            services.AddTransient<IVehicleRepository, VehicleRepository>();
            services.AddTransient<IFunnyService, FunnyService>();

            services.AddSingleton<TestSystem>();
            
        }

        public void Configure(IEcsBuilder builder)
        {
            // Enable systems:
            builder.UseSystem<TestSystem>();

            // Load middlewares:
            // Can also be loaded by systems which are IConfiguringSystem
            builder.Use("OnGameModeInit", (ctx, next) =>
            {
                Console.WriteLine("I am middleware for OnGameModeInit!");
                return next();
            });
            
            builder.Use("OnPlayerText", (ctx, next) =>
            {
                // Having to access arguments by an indexed array isn't ideal, might work towards a dictionary structure.
                if (ctx.Arguments[1] is string txt && txt.Contains("I dislike SampSharp"))
                    return null;
                return next();
            });
        }
    }
}