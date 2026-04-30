namespace SampSharp.Entities;

/// <summary>Provides the functionality for a registry of system types.</summary>
public interface ISystemRegistry
{
    /// <summary>Gets all systems of the specified <paramref name="type" />.</summary>
    /// <param name="type">The type of the systems to get.</param>
    /// <returns>A segment of memory containing  instances of systems of the specified type.</returns>
    ReadOnlyMemory<ISystem> Get(Type type);

    /// <summary>Gets all systems of the specified <typeparamref name="TSystem" />.</summary>
    /// <typeparam name="TSystem">The type of the systems to get.</typeparam>
    /// <returns>A segment of memory containing instances of systems of the specified type.</returns>
    ReadOnlyMemory<ISystem> Get<TSystem>() where TSystem : ISystem;
    
    /// <summary>
    /// Gets all implementation types of registered systems in this system registry.
    /// </summary>
    /// <returns>A segment of memory containing all implementation types of registered systems.</returns>
    ReadOnlyMemory<Type> GetSystemTypes();

    /// <summary>
    /// Registers a handler to be called when the system registry has been loaded. The handler will be called immediately if the system registry has already been loaded.
    /// </summary>
    /// <param name="handler">The handler to call when the system registry has been loaded.</param>
    void Register(Action handler);
}