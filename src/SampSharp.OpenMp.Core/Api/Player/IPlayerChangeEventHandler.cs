namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IPlayerPool.GetPlayerChangeDispatcher"/>.
/// </summary>
[OpenMpEventHandler]
public partial interface IPlayerChangeEventHandler
{
    /// <summary>
    /// Called when a player's score changes.
    /// </summary>
    /// <param name="player">The player whose score changed.</param>
    /// <param name="score">The new score of the player.</param>
    void OnPlayerScoreChange(IPlayer player, int score);

    /// <summary>
    /// Called when a player's name changes.
    /// </summary>
    /// <param name="player">The player whose name changed.</param>
    /// <param name="oldName">The previous name of the player.</param>
    void OnPlayerNameChange(IPlayer player, string oldName);

    /// <summary>
    /// Called when a player's interior changes.
    /// </summary>
    /// <param name="player">The player whose interior changed.</param>
    /// <param name="newInterior">The new interior ID.</param>
    /// <param name="oldInterior">The previous interior ID.</param>
    void OnPlayerInteriorChange(IPlayer player, uint newInterior, uint oldInterior);

    /// <summary>
    /// Called when a player's state changes.
    /// </summary>
    /// <param name="player">The player whose state changed.</param>
    /// <param name="newState">The new state of the player.</param>
    /// <param name="oldState">The previous state of the player.</param>
    void OnPlayerStateChange(IPlayer player, PlayerState newState, PlayerState oldState);

    /// <summary>
    /// Called when a player's key state changes.
    /// </summary>
    /// <param name="player">The player whose key state changed.</param>
    /// <param name="newKeys">The new key state.</param>
    /// <param name="oldKeys">The previous key state.</param>
    void OnPlayerKeyStateChange(IPlayer player, uint newKeys, uint oldKeys);
}
