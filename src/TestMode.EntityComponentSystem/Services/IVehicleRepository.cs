using SampSharp.EntityComponentSystem.Entities;

namespace TestMode.Ecs.Services
{
    public interface IVehicleRepository
    {
        void Foo();
        void FooForPlayer(Entity player);
    }
}