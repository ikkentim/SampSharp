using System.Diagnostics.Contracts;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IComponentList" /> interface.
/// </summary>
[OpenMpApi]
public readonly partial struct IComponentList
{
    /// <summary>
    /// Gets a component by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the component.</param>
    /// <returns>The component or <see langword="null" /> if not found.</returns>
    [Pure]
    public partial IComponent QueryComponent(UID id);

    /// <summary>
    /// Gets a component by its type.
    /// </summary>
    /// <typeparam name="T">The type of the component.</typeparam>
    /// <returns>The component or <see langword="null" /> if not found.</returns>
    [Pure]
    public T QueryComponent<T>() where T : unmanaged, IComponent.IManagedInterface
    {
        var component = QueryComponent(T.ComponentId);

        var componentHandle = component.Handle;

        componentHandle = T.FromComponentHandle(componentHandle);
        return StructPointer.AsStruct<T>(componentHandle);
    }
}