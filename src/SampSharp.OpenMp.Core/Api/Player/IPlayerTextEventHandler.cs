namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IPlayerPool.GetPlayerTextDispatcher"/>.
/// </summary>
[OpenMpEventHandler]
public partial interface IPlayerTextEventHandler
{
    /// <summary>
    /// Called when a player sends a text message.
    /// </summary>
    /// <param name="player">The player who sent the message.</param>
    /// <param name="message">The text message sent by the player.</param>
    /// <returns><see langword="true" /> if the message should be displayed; otherwise, <see langword="false" />.</returns>
    bool OnPlayerText(IPlayer player, string message);

    /// <summary>
    /// Called when a player sends a command text.
    /// </summary>
    /// <param name="player">The player who sent the command.</param>
    /// <param name="message">The command text sent by the player.</param>
    /// <returns><see langword="true" /> if the command has been processed; otherwise, <see langword="false" />.</returns>
    bool OnPlayerCommandText(IPlayer player, string message);
}