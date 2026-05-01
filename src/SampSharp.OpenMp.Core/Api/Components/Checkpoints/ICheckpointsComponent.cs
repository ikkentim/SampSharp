namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="ICheckpointsComponent" /> interface
/// (the source of <see cref="IPlayerCheckpointEventHandler" /> events).
/// </summary>
[OpenMpApi(typeof(IComponent))]
public readonly partial struct ICheckpointsComponent
{
    /// <inheritdoc />
    public static UID ComponentId => new(0x44a937350d611dde);

    /// <summary>
    /// Gets the event dispatcher for checkpoint enter/leave events.
    /// </summary>
    public partial IEventDispatcher<IPlayerCheckpointEventHandler> GetEventDispatcher();
}