namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Provides context and message handler for a console command execution.
/// Wraps the open.mp ConsoleCommandSenderData and provides a convenient interface for command handlers.
/// </summary>
public class ConsoleCommandDispatchContext
{
    /// <summary>
    /// Initializes a new instance from console sender data.
    /// </summary>
    /// <param name="player">The player who sent the command (null if sent from server console).</param>
    /// <param name="messageHandler">Optional handler to send response messages.</param>
    public ConsoleCommandDispatchContext(Player? player, Action<string>? messageHandler = null)
    {
        Player = player;
        MessageHandler = messageHandler;
    }

    /// <summary>
    /// The player who invoked this console command, or null if invoked from server console.
    /// </summary>
    public Player? Player { get; }

    /// <summary>
    /// Optional handler to send response messages back to the console/player.
    /// </summary>
    public Action<string>? MessageHandler { get; }

    /// <summary>
    /// Sends a message via the MessageHandler if available.
    /// </summary>
    /// <param name="message">The message to send.</param>
    public void SendMessage(string message)
    {
        MessageHandler?.Invoke(message);
    }
}