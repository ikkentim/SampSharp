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

namespace SampSharp.Entities
{
    /// <summary>
    /// Provides functionality for the creation, modification and destruction of entities.
    /// </summary>
    public interface IEntityManager
    {
        /// <summary>
        /// Creates an entity with the specified specified entity.
        /// </summary>
        /// <param name="entity">The identifier of the entity to be created.</param>
        /// <param name="parent">The parent of the entity to be created.</param>
        void Create(EntityId entity, EntityId parent = default);

        /// <summary>
        /// Adds a component of the specified type <typeparamref name="T" /> to the specified <paramref name="entity" /> with the
        /// specified constructor <paramref name="args" />.
        /// </summary>
        /// <typeparam name="T">The type of the component to add.</typeparam>
        /// <param name="entity">The entity to add the component to.</param>
        /// <param name="args">The arguments of the constructor of the component.</param>
        /// <returns>The created component.</returns>
        T AddComponent<T>(EntityId entity, params object[] args) where T : Component;

        /// <summary>
        /// Adds a component of the specified type <typeparamref name="T" /> to the specified <paramref name="entity" />.
        /// </summary>
        /// <typeparam name="T">The type of the component to add.</typeparam>
        /// <param name="entity">The entity to add the component to.</param>
        /// <returns>The created component.</returns>
        T AddComponent<T>(EntityId entity) where T : Component;

        /// <summary>
        /// Destroys the specified <paramref name="component" />.
        /// </summary>
        /// <param name="component">The component to destroy.</param>
        void Destroy(Component component);

        /// <summary>
        /// Destroys the specified <paramref name="entity" />.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Destroy(EntityId entity);

        /// <summary>
        /// Destroys the components of the specified type <typeparamref name="T" /> attached to the specified
        /// <paramref name="entity" />.
        /// </summary>
        /// <typeparam name="T">The type of the components to destroy.</typeparam>
        /// <param name="entity">The entity of which to destroy its components of the specified type <typeparamref name="T" />.</param>
        void Destroy<T>(EntityId entity) where T : Component;

        /// <summary>
        /// Gets the children of the specified <paramref name="entity" />.
        /// </summary>
        /// <param name="entity">The entity to get the children of.</param>
        /// <returns>An array with the entities of which the parent is the specified <paramref name="entity" />.</returns>
        EntityId[] GetChildren(EntityId entity);

        /// <summary>
        /// Gets all root entities with no parent.
        /// </summary>
        /// <returns>An array with all entities without a parent.</returns>
        EntityId[] GetRootEntities();

        /// <summary>
        /// Gets a component of the specified type <typeparamref name="T" /> attached to any entity.
        /// </summary>
        /// <typeparam name="T">The type of the component to find.</typeparam>
        /// <returns>The found component or <c>null</c> if no component of the specified type could be found.</returns>
        T GetComponent<T>() where T : Component;

        /// <summary>
        /// Gets a component of the specified type <typeparamref name="T" /> attached to the specified <paramref name="entity" />.
        /// </summary>
        /// <typeparam name="T">The type of the component to find.</typeparam>
        /// <returns>The found component or <c>null</c> if no component of the specified type could be found.</returns>
        T GetComponent<T>(EntityId entity) where T : Component;

        /// <summary>
        /// Gets a component of the specified type <typeparamref name="T" /> attached to a child entity of the specified
        /// <paramref name="entity" /> using a depth first search.
        /// </summary>
        /// <typeparam name="T">The type of the component to find.</typeparam>
        /// <returns>The found component or <c>null</c> if no component of the specified type could be found.</returns>
        T GetComponentInChildren<T>(EntityId entity) where T : Component;

        /// <summary>
        /// Gets a component of the specified type <typeparamref name="T" /> attached to a parent entity of the specified
        /// <paramref name="entity" />.
        /// </summary>
        /// <typeparam name="T">The type of the component to find.</typeparam>
        /// <returns>The found component or <c>null</c> if no component of the specified type could be found.</returns>
        T GetComponentInParent<T>(EntityId entity) where T : Component;

        /// <summary>
        /// Gets all components of the specified type <typeparamref name="T" /> attached to any entity.
        /// </summary>
        /// <typeparam name="T">The type of the components to find.</typeparam>
        /// <returns>A collection of the found components.</returns>
        T[] GetComponents<T>() where T : Component;

        /// <summary>
        /// Gets all components of the specified type <typeparamref name="T" /> attached to the specified
        /// <paramref name="entity" />.
        /// </summary>
        /// <typeparam name="T">The type of the components to find.</typeparam>
        /// <returns>A collection of the found components.</returns>
        T[] GetComponents<T>(EntityId entity) where T : Component;

        /// <summary>
        /// Gets all components of the specified type <typeparamref name="T" /> attached to a child entity of the specified
        /// <paramref name="entity" />.
        /// </summary>
        /// <typeparam name="T">The type of the components to find.</typeparam>
        /// <returns>A collection of the found components.</returns>
        T[] GetComponentsInChildren<T>(EntityId entity) where T : Component;

        /// <summary>
        /// Gets all components of the specified type <typeparamref name="T" /> attached to a parent entity of the specified
        /// <paramref name="entity" />.
        /// </summary>
        /// <typeparam name="T">The type of the components to find.</typeparam>
        /// <returns>A collection of the found components.</returns>
        T[] GetComponentsInParent<T>(EntityId entity) where T : Component;

        /// <summary>
        /// Gets the parent entity of the specified <paramref name="entity" />.
        /// </summary>
        /// <param name="entity">The entity of which to get its parent.</param>
        /// <returns>
        /// The parent entity of the specified <paramref name="entity" />. <see cref="EntityId.Empty" /> is returned if
        /// the specified <paramref name="entity" /> does not have a parent.
        /// </returns>
        EntityId GetParent(EntityId entity);

        /// <summary>
        /// Returns a value indicating whether the specified <paramref name="entity" /> exists.
        /// </summary>
        /// <param name="entity">The entity to check its existence of.</param>
        /// <returns><c>true</c> if the specified <paramref name="entity" /> exists; otherwise <c>false</c>.</returns>
        bool Exists(EntityId entity);
    }
}