using SampSharp.EntityComponentSystem.Entities;
using SampSharp.EntityComponentSystem.SAMP.Components;
using SampSharp.EntityComponentSystem.SAMP.NativeComponents;

namespace SampSharp.EntityComponentSystem.SAMP
{
    public class WorldService : IWorldService
    {
        private readonly IEntityManager _entityManager;

        public WorldService(IEntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        public Actor CreateActor(int modelId, Vector3 position, float rotation)
        {
            var world = _entityManager.Get(WorldSystem.WorldId);

            var id = world.GetComponent<NativeWorld>().CreateActor(modelId, position.X, position.Y, position.Z, rotation);

            var entity = _entityManager.Create(world, SampEntities.GetActorId(id));

            entity.AddComponent<NativeActor>();
            return entity.AddComponent<Actor>();
        }
    }
}