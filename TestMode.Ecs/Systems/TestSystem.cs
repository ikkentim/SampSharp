using System;
using SampSharp.EntityComponentSystem.Entities;
using SampSharp.EntityComponentSystem.Events;
using SampSharp.EntityComponentSystem.Systems;
using TestMode.Ecs.Components;
using TestMode.Ecs.Services;

namespace TestMode.Ecs.Systems
{
    public class TestSystem : ISystem
    {
        [Event]
        public void OnGameModeInit(IVehicleRepository vehiclesRepository) // Event methods have dependency injection alongside the arguments
        {
            Console.WriteLine("Do game mode loading goodies");

            vehiclesRepository.Foo();
        }

        [Event]
        public void OnPlayerConnect(Entity player, IVehicleRepository vehiclesRepository)
        {
            Console.WriteLine("I connected! " + player.Id);

            vehiclesRepository.FooForPlayer(player);

            player.AddComponent<TestComponent>();

            // TODO: Each entity will have a component with characteristics, eg, player with have a Player component:
            // var playerComponent = player.GetComponent<Player>()
            // playerComponent.SendClientMessage($"Hello {playerComponent.Name!}");
            //
            // In the parameters of an event you will be able to replace the entity type with an component of the entity, eg:
            // OnPlayerConnect(Player player) { ... }
            // which would simply get the component of the entity and put it in the argument of the event.
        }

        [Event]
        public void OnPlayerText(TestComponent compo, string text)
        {
            Console.WriteLine(compo.Hi + ":::" + text);
        }
    }
}