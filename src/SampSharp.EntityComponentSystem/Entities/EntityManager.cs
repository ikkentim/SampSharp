using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace SampSharp.EntityComponentSystem.Entities
{
    public class EntityManager : IEntityManager
    {
        private readonly Dictionary<EntityId, Entity> _entities = new Dictionary<EntityId, Entity>();
        
        public Entity Create(Entity parent, EntityId id)
        {
            var entity = new Entity(parent, id);

            if (_entities.ContainsKey(id))
            {
                throw new EntityCreationException($"Duplicate identity {id} in entities container.");
            }

            _entities.Add(id, entity);

            return entity;
        }

        public Entity Get(EntityId id)
        {
            _entities.TryGetValue(id, out var entity);
            return entity;
        }

        public void Destroy(Entity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.Destroy();
            _entities.Remove(entity.Id);
        }
    }
}