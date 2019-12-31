using System;
using SampSharp.Entities;
using SampSharp.Entities.Events;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Components;
using TestMode.Entities.Components;
using TestMode.Entities.Services;

namespace TestMode.Entities.Systems
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