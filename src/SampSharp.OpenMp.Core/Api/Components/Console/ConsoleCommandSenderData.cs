using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Contains information about the source of a console command.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct ConsoleCommandSenderData
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleCommandSenderData" /> struct.
    /// </summary>
    /// <param name="sender">The type of sender.</param>
    /// <param name="handle">The unmanaged handle to the sender object.</param>
    public ConsoleCommandSenderData(ConsoleCommandSender sender, IntPtr handle)
    {
        Sender = sender;
        _handle = handle;
    }

    /// <summary>
    /// The type of command sender.
    /// </summary>
    public readonly ConsoleCommandSender Sender;
    private readonly nint _handle;

    /// <summary>
    /// Gets the player if the sender is a player; otherwise, <c>null</c>.
    /// </summary>
    public IPlayer? Player => Sender == ConsoleCommandSender.Player ? new IPlayer(_handle) : null;

    /// <summary>
    /// Gets the message handler if the sender is custom; otherwise, <c>null</c>.
    /// </summary>
    public ConsoleMessageHandler? Handler => Sender == ConsoleCommandSender.Custom ? new ConsoleMessageHandler(_handle) : null;
}