// SampSharp
// Copyright 2020 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using SampSharp.Core.Logging;

namespace SampSharp.Entities
{
    /// <summary>
    /// Represents the entity manager.
    /// </summary>
    /// <seealso cref="IEntityManager" />
    public sealed class EntityManager : IEntityManager
    {
        private readonly Dictionary<EntityId, Entity> _entities = new Dictionary<EntityId, Entity>();

        /// <inheritdoc />
        public Entity Create(Entity parent, EntityId id)
        {
            var entity = new Entity(this, parent, id);

            if (_entities.ContainsKey(id))
                throw new EntityCreationException($"Duplicate identity {id} in entities container.");

            _entities.Add(id, entity);

            CoreLog.LogVerbose("Entity added, {0} entities in registry.", _entities.Count);
            return entity;
        }

        /// <inheritdoc />
        public Entity Get(EntityId id)
        {
            _entities.TryGetValue(id, out var entity);
            return entity;
        }

        /// <inheritdoc />
        public void Destroy(Entity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            if (_entities.Remove(entity.Id))
            {
                entity.Destroy();

                CoreLog.LogVerbose("Entity removed, {0} entities remaining.", _entities.Count);
            }
        }

        internal void Remove(Entity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            if (_entities.Remove(entity.Id))
                CoreLog.LogVerbose("Entity removed, {0} entities remaining.", _entities.Count);
        }
    }
}