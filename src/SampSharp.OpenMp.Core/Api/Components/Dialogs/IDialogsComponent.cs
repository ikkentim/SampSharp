namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IDialogsComponent" /> interface.
/// </summary>
[OpenMpApi(typeof(IComponent))]
public readonly partial struct IDialogsComponent
{
    /// <inheritdoc />
    public static UID ComponentId => new(0x44a111350d611dde);

    /// <summary>
    /// Gets the event dispatcher for dialog events.
    /// </summary>
    /// <returns>An event dispatcher for <see cref="IPlayerDialogEventHandler" /> events.</returns>
    public partial IEventDispatcher<IPlayerDialogEventHandler> GetEventDispatcher();
}