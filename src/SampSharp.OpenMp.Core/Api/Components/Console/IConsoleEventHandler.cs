using SampSharp.OpenMp.Core.RobinHood;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IConsoleComponent.GetEventDispatcher" />.
/// </summary>
[OpenMpEventHandler]
public partial interface IConsoleEventHandler
{
    /// <summary>
    /// Called when console text/command is entered.
    /// </summary>
    /// <param name="command">The command name.</param>
    /// <param name="parameters">The command parameters.</param>
    /// <param name="sender">Information about the command sender.</param>
    /// <returns><c>true</c> to prevent further processing of the command; otherwise, <c>false</c>.</returns>
    bool OnConsoleText(string command, string parameters, ref ConsoleCommandSenderData sender);

    /// <summary>
    /// Called when an RCON login attempt is made.
    /// </summary>
    /// <param name="player">The player attempting to login.</param>
    /// <param name="password">The password used in the login attempt.</param>
    /// <param name="success">Whether the login was successful.</param>
    void OnRconLoginAttempt(IPlayer player, string password, bool success);

    /// <summary>
    /// Called when a list of console commands is requested.
    /// </summary>
    /// <param name="commands">The set of available commands.</param>
    void OnConsoleCommandListRequest(FlatHashSetStringView commands);
}