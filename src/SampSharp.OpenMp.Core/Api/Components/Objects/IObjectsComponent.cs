using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IObjectsComponent" /> interface.
/// </summary>
[OpenMpApi(typeof(IPoolComponent<IObject>))]
public readonly partial struct IObjectsComponent
{
    /// <inheritdoc />
    public static UID ComponentId => new(0x59f8415f72da6160);

    /// <summary>
    /// Gets the event dispatcher for handling object events.
    /// </summary>
    /// <returns>An event dispatcher for <see cref="IObjectEventHandler" />.</returns>
    public partial IEventDispatcher<IObjectEventHandler> GetEventDispatcher();

    /// <summary>
    /// Sets the default camera collision state.
    /// </summary>
    /// <param name="collision">A value indicating whether camera collision is enabled by default.</param>
    public partial void SetDefaultCameraCollision(bool collision);

    /// <summary>
    /// Gets the default camera collision state.
    /// </summary>
    /// <returns><see langword="true" /> if camera collision is enabled by default; otherwise, <see langword="false" />.</returns>
    public partial bool GetDefaultCameraCollision();

    /// <summary>
    /// Creates a new object with the specified model ID, position, and rotation.
    /// </summary>
    /// <param name="modelID">The model ID of the object.</param>
    /// <param name="position">The position of the object.</param>
    /// <param name="rotation">The rotation of the object.</param>
    /// <param name="drawDist">The draw distance of the object. Defaults to 0.</param>
    /// <returns>A new instance of <see cref="IObject" />.</returns>
    public partial IObject Create(int modelID, Vector3 position, Vector3 rotation, float drawDist = 0);
}