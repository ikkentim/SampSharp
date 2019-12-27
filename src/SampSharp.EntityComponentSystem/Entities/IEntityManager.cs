namespace SampSharp.EntityComponentSystem.Entities
{
    public interface IEntityManager
    {
        Entity Create(Entity parent, EntityId id);
        void Destroy(Entity entity);
        Entity Get(EntityId id);
    }
}