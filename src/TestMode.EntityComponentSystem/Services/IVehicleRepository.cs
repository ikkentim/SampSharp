using SampSharp.EntityComponentSystem.Entities;

namespace TestMode.EntityComponentSystem.Services
{
    public interface IVehicleRepository
    {
        void Foo();
        void FooForPlayer(Entity player);
    }
}