using System;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.EntityComponentSystem;
using TestMode.Ecs.Services;
using TestMode.Ecs.Systems;

namespace TestMode.Ecs
{
    public class TestStartup : IStartup
    {
        public void Configure(IServiceCollection services)
        {
            // Load services (e.g. repositories), systems, etc.
            services.AddTransient<IVehicleRepository, VehicleRepository>();

            services.AddSingleton<TestSystem>();
        }

        public void Configure(IEcsBuilder builder)
        {
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

    // TODO:
    // Entities are hierarchical, each entity can have a parent. For example the player entity will be a child of a connection, A player object will be a child of a player.
    // Destroying an entity will destroy all children.
}