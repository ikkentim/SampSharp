namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IPlayerPool.GetPlayerStreamDispatcher"/>.
/// </summary>
[OpenMpEventHandler]
public partial interface IPlayerStreamEventHandler
{
    /// <summary>
    /// Called when a player is streamed in for another player.
    /// </summary>
    /// <param name="player">The player being streamed in.</param>
    /// <param name="forPlayer">The player for whom the other player is being streamed in.</param>
    void OnPlayerStreamIn(IPlayer player, IPlayer forPlayer);

    /// <summary>
    /// Called when a player is streamed out for another player.
    /// </summary>
    /// <param name="player">The player being streamed out.</param>
    /// <param name="forPlayer">The player for whom the other player is being streamed out.</param>
    void OnPlayerStreamOut(IPlayer player, IPlayer forPlayer);
}