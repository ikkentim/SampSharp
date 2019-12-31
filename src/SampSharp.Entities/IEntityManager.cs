// SampSharp
// Copyright 2019 Tim Potze
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

namespace SampSharp.Entities
{
    /// <summary>
    /// Provides functionality for entity creation, destruction and storage.
    /// </summary>
    public interface IEntityManager
    {
        /// <summary>
        /// Creates the a new <see cref="Entity" />.
        /// </summary>
        /// <param name="parent">The parent of the entity to be created.</param>
        /// <param name="id">The identifier of the entity to be created.</param>
        /// <returns>The newly created entity.</returns>
        Entity Create(Entity parent, EntityId id);

        /// <summary>
        /// Destroys the specified <paramref name="entity" />.
        /// </summary>
        /// <param name="entity">The entity to be destroyed.</param>
        void Destroy(Entity entity);

        /// <summary>
        /// Gets the <see cref="Entity" /> associated with the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>The entity or <c>null</c> if the no entity is associated with the specified <paramref name="id" />.</returns>
        Entity Get(EntityId id);
    }
}