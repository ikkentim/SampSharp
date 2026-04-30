namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IConsoleComponent" /> interface.
/// </summary>
[OpenMpApi(typeof(IComponent))]
public readonly partial struct IConsoleComponent
{
    /// <inheritdoc />
    public static UID ComponentId => new(0xbfa24e49d0c95ee4);

    /// <summary>
    /// Gets the event dispatcher for console-related events.
    /// </summary>
    /// <returns>The event dispatcher instance.</returns>
    public partial IEventDispatcher<IConsoleEventHandler> GetEventDispatcher();

    /// <summary>
    /// Sends a command from a console source.
    /// </summary>
    /// <param name="command">The command to send.</param>
    /// <param name="sender">Information about the command sender.</param>
    public partial void Send(string command, ref ConsoleCommandSenderData sender);

    /// <summary>
    /// Sends a message to a console recipient.
    /// </summary>
    /// <param name="recipient">Information about the message recipient.</param>
    /// <param name="message">The message to send.</param>
    public partial void SendMessage(ref ConsoleCommandSenderData recipient, string message);
}