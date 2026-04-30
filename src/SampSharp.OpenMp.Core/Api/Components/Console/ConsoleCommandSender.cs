namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Specifies the source of a console command.
/// </summary>
public enum ConsoleCommandSender
{
    /// <summary>
    /// Command sent from the server console.
    /// </summary>
    Console,

    /// <summary>
    /// Command sent by a player via rcon.
    /// </summary>
    Player,

    /// <summary>
    /// Command sent from a custom source.
    /// </summary>
    Custom
}