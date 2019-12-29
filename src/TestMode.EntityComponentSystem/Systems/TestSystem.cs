using System;
using SampSharp.EntityComponentSystem;
using SampSharp.EntityComponentSystem.Entities;
using SampSharp.EntityComponentSystem.Events;
using SampSharp.EntityComponentSystem.SAMP;
using SampSharp.EntityComponentSystem.SAMP.Components;
using SampSharp.EntityComponentSystem.SAMP.NativeComponents;
using SampSharp.EntityComponentSystem.Systems;
using TestMode.EntityComponentSystem.Components;
using TestMode.EntityComponentSystem.Services;

namespace TestMode.EntityComponentSystem.Systems
{

    public class TestSystem : ISystem
    {
        [Event]
        public void OnGameModeInit(IVehicleRepository vehiclesRepository, IWorldService worldService) // Event methods have dependency injection alongside the arguments
        {
            Console.WriteLine("Do game mode loading goodies");

            vehiclesRepository.Foo();

            worldService.CreateActor(101, new Vector3(0, 0, 20), 0);
        }

        [Event]
        public void OnPlayerCommandText(Player player, string text, IWorldService worldService)
        {
            if (text == "/actor")
            {
                worldService.CreateActor(0, player.Position + Vector3.Up, 0);
                player.SendClientMessage(-1, "Actor created!");
            }

            if (text == "/pos")
            {
                player.SendClientMessage(-1, $"You are at {player.Position}");
            }
        }

        [Event]
        public void OnPlayerConnect(Entity player, IVehicleRepository vehiclesRepository)
        {
            Console.WriteLine("I connected! " + player.Id);

            player.AddComponent<TestComponent>();
            
            vehiclesRepository.FooForPlayer(player);
        }

        [Event]
        public void OnPlayerConnect(Player player)
        {
            player.SendClientMessage($"Hey there, {player.Name}");
        }

        [Event]
        public void OnPlayerText(TestComponent test, string text)
        {
            Console.WriteLine(test.WelcomingMessage + ":::" + text);
        }
    }
}