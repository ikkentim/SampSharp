using System;
using SampSharp.Entities;

namespace TestMode.Entities.Services
{
    public class VehicleRepository : IVehicleRepository
    {
        public void Foo()
        {
            Console.WriteLine("Foo vehicles");
        }
        public void FooForPlayer(Entity player)
        {
            Console.WriteLine($"Foo vehicles for {player}");
        }
    }
}