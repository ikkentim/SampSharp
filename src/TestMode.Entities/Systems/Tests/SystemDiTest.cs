using System;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TestMode.Entities.Components;
using TestMode.Entities.Services;

namespace TestMode.Entities.Systems.Tests
{
    public class SystemDiTest : ISystem
    {
        [Event]
        public void OnGameModeInit(IVehicleRepository vehiclesRepository)
        {
            vehiclesRepository.Foo();
        }

        [Event]
        public void OnPlayerConnect(Player player, IVehicleRepository vehiclesRepository)
        {
            player.AddComponent<TestComponent>();

            vehiclesRepository.FooForPlayer(player.Entity);
        }


        [Event]
        public void OnPlayerConnect(Player player, IScopedFunnyService scoped, IFunnyService transient,
            IServiceProvider serviceProvider)
        {
            Console.WriteLine("T: " + transient.FunnyGuid);
            Console.WriteLine("S: " + scoped.FunnyGuid);
            var s2 = serviceProvider.GetRequiredService<IScopedFunnyService>().FunnyGuid;
            var t2 = serviceProvider.GetRequiredService<IFunnyService>().FunnyGuid;
            Console.WriteLine("T2: " + t2);
            Console.WriteLine("S2: " + s2);
        }

        [Event]
        public void OnPlayerText(TestComponent test, string text, IScopedFunnyService scoped, IFunnyService transient,
            IServiceProvider serviceProvider)
        {
            Console.WriteLine("T: " + transient.FunnyGuid);
            Console.WriteLine("S: " + scoped.FunnyGuid);
            var s2 = serviceProvider.GetRequiredService<IScopedFunnyService>().FunnyGuid;
            var t2 = serviceProvider.GetRequiredService<IFunnyService>().FunnyGuid;
            Console.WriteLine("T2: " + t2);
            Console.WriteLine("S2: " + s2);
            Console.WriteLine(test.WelcomingMessage);
        }
    }
}