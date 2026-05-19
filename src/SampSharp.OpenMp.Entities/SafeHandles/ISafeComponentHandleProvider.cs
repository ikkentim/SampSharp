using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities;

/// <summary>
/// Provides handles to open.mp components which are automatically cleared when the component is freed. This allows for safe access to components without risking access to freed components, which can lead to crashes or undefined behavior.
/// </summary>
public interface ISafeComponentHandleProvider
{
    /// <summary>
    /// Retrieves a handle to a component of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <returns>A <see cref="SafeComponentHandle{T}"/> representing a handle to the requested component. The handle will be cleared when the open.mp component is freed.</returns>
    SafeComponentHandle<T> Get<T>() where T : unmanaged, IComponent.IManagedInterface;
}