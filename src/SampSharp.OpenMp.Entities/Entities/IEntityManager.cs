using System.Diagnostics.Contracts;

namespace SampSharp.Entities;

/// <summary>Provides functionality for the creation, modification and destruction of entities.</summary>
public interface IEntityManager
{
    /// <summary>
    /// Adds a component of the specified type <typeparamref name="T" /> to the specified <paramref name="entity" />
    /// with the specified constructor <paramref name="args" />.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <param name="entity">The entity to add the component to.</param>
    /// <param name="args">The arguments of the constructor of the component.</param>
    /// <returns>The created component.</returns>
    T AddComponent<T>(EntityId entity, params object[] args) where T : Component;

    /// <summary>
    /// Adds a component of the specified type <typeparamref name="T" /> to the specified <paramref name="entity" />
    /// with the specified constructor <paramref name="args" />. The entity is attached as a child to the specified
    /// <paramref name="parent" />.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <param name="entity">The entity to add the component to.</param>
    /// <param name="parent">The parent of the entity to which the entity is to be attached.</param>
    /// <param name="args">The arguments of the constructor of the component.</param>
    /// <returns>The created component.</returns>
    T AddComponent<T>(EntityId entity, EntityId parent, params object[] args) where T : Component;

    /// <summary>Adds a component of the specified type <typeparamref name="T" /> to the specified <paramref
    /// name="entity" />.</summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <param name="entity">The entity to add the component to.</param>
    /// <returns>The created component.</returns>
    T AddComponent<T>(EntityId entity) where T : Component;

    /// <summary>
    /// Adds a component of the specified type <typeparamref name="T" /> to the specified <paramref name="entity" />.
    /// The entity is attached as a child to the specified <paramref name="parent" />.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <param name="entity">The entity to add the component to.</param>
    /// <param name="parent">The parent of the entity to which the entity is to be attached.</param>
    /// <returns>The created component.</returns>
    T AddComponent<T>(EntityId entity, EntityId parent) where T : Component;

    /// <summary>Adds a component of the specified type <typeparamref name="T" /> to the specified <paramref
    /// name="entity" />.</summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <param name="entity">The entity to add the component to.</param>
    /// <param name="component">The instance of the component to be added.</param>
    void AddComponent<T>(EntityId entity, T component) where T : Component;

    /// <summary>
    /// Adds a component of the specified type <typeparamref name="T" /> to the specified <paramref name="entity" />.
    /// The entity is attached as a child to the specified <paramref name="parent" />.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <param name="entity">The entity to add the component to.</param>
    /// <param name="parent">The parent of the entity to which the entity is to be attached.</param>
    /// <param name="component">The instance of the component to be added.</param>
    void AddComponent<T>(EntityId entity, EntityId parent, T component) where T : Component;

    /// <summary>Destroys the specified <paramref name="component" />.</summary>
    /// <param name="component">The component to destroy.</param>
    void Destroy(Component component);

    /// <summary>Destroys the specified <paramref name="entity" />.</summary>
    /// <param name="entity">The entity.</param>
    void Destroy(EntityId entity);

    /// <summary>Destroys the components of the specified type <typeparamref name="T" /> attached to the specified
    /// <paramref name="entity" />.</summary>
    /// <typeparam name="T">The type of the components to destroy.</typeparam>
    /// <param name="entity">The entity of which to destroy its components of the specified type <typeparamref name="T"
    /// />.</param>
    void Destroy<T>(EntityId entity) where T : Component;

    /// <summary>Gets the children of the specified <paramref name="entity" />.</summary>
    /// <param name="entity">The entity to get the children of.</param>
    /// <returns>An array with the entities of which the parent is the specified <paramref name="entity" />.</returns>
    [Pure]
    EntityId[] GetChildren(EntityId entity);

    /// <summary>Gets all root entities with no parent.</summary>
    /// <returns>An array with all entities without a parent.</returns>
    [Pure]
    EntityId[] GetRootEntities();

    /// <summary>Gets a component of the specified type <typeparamref name="T" /> attached to any entity.</summary>
    /// <typeparam name="T">The type of the component to find.</typeparam>
    /// <returns>The found component or <see langword="null" /> if no component of the specified type could be found.</returns>
    [Pure]
    T? GetComponent<T>() where T : Component;

    /// <summary>Gets a component of the specified type <typeparamref name="T" /> attached to the specified <paramref
    /// name="entity" />.</summary>
    /// <typeparam name="T">The type of the component to find.</typeparam>
    /// <param name="entity">The entity to get the component from.</param>
    /// <returns>The found component or <see langword="null" /> if no component of the specified type could be found.</returns>
    [Pure]
    T? GetComponent<T>(EntityId entity) where T : Component;

    /// <summary>
    /// Gets a component of the specified type <typeparamref name="T" /> attached to a child entity of the specified
    /// <paramref name="entity" /> using a depth-first search.
    /// </summary>
    /// <typeparam name="T">The type of the component to find.</typeparam>
    /// <param name="entity">The entity to get the component from.</param>
    /// <returns>The found component or <see langword="null" /> if no component of the specified type could be found.</returns>
    [Pure]
    T? GetComponentInChildren<T>(EntityId entity) where T : Component;

    /// <summary>Gets a component of the specified type <typeparamref name="T" /> attached to a parent entity of the
    /// specified <paramref name="entity" />.</summary>
    /// <typeparam name="T">The type of the component to find.</typeparam>
    /// <param name="entity">The entity to get the component from.</param>
    /// <returns>The found component or <see langword="null" /> if no component of the specified type could be found.</returns>
    [Pure]
    T? GetComponentInParent<T>(EntityId entity) where T : Component;

    /// <summary>Gets all components of the specified type <typeparamref name="T" /> attached to any entity.</summary>
    /// <typeparam name="T">The type of the components to find.</typeparam>
    /// <returns>A collection of the found components.</returns>
    [Pure]
    T[] GetComponents<T>() where T : Component;

    /// <summary>Gets all components of the specified type <typeparamref name="T" /> attached to the specified <paramref
    /// name="entity" />.</summary>
    /// <typeparam name="T">The type of the components to find.</typeparam>
    /// <param name="entity">The entity to get the components from.</param>
    /// <returns>A collection of the found components.</returns>
    [Pure]
    T[] GetComponents<T>(EntityId entity) where T : Component;

    /// <summary>Gets all components of the specified type <typeparamref name="T" /> attached to a child entity of the
    /// specified <paramref name="entity" />.</summary>
    /// <typeparam name="T">The type of the components to find.</typeparam>
    /// <param name="entity">The entity to get the components from.</param>
    /// <returns>A collection of the found components.</returns>
    [Pure]
    T[] GetComponentsInChildren<T>(EntityId entity) where T : Component;

    /// <summary>Gets all components of the specified type <typeparamref name="T" /> attached to a parent entity of the
    /// specified <paramref name="entity" />.</summary>
    /// <typeparam name="T">The type of the components to find.</typeparam>
    /// <param name="entity">The entity to get the components from.</param>
    /// <returns>A collection of the found components.</returns>
    [Pure]
    T[] GetComponentsInParent<T>(EntityId entity) where T : Component;

    /// <summary>Gets the parent entity of the specified <paramref name="entity" />.</summary>
    /// <param name="entity">The entity to get its parent.</param>
    /// <returns>
    /// The parent entity of the specified <paramref name="entity" />. <see cref="EntityId.Empty" /> is returned if the
    /// specified <paramref name="entity" /> does not have a parent.
    /// </returns>
    [Pure]
    EntityId GetParent(EntityId entity);

    /// <summary>Returns a value indicating whether the specified <paramref name="entity" /> exists.</summary>
    /// <param name="entity">The entity to check its existence.</param>
    /// <returns><see langword="true" /> if the specified <paramref name="entity" /> exists; otherwise, <see langword="false" />.</returns>
    [Pure]
    bool Exists(EntityId entity);
}
