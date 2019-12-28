using System;
using SampSharp.EntityComponentSystem.Components;
using SampSharp.EntityComponentSystem.Events;
using TestMode.Ecs.Services;

namespace TestMode.Ecs.Components
{
    public class TestComponent : Component
    {
        public string Hi => $"Hi, {Entity}";

        [Event]
        public void OnText(string text, IVehicleRepository vehicleRepository)
        {
            Console.WriteLine("Player send text!!!!!! from component!!!!! " + Hi + ":::" + text);

            vehicleRepository.FooForPlayer(Entity);
        }
    }
}