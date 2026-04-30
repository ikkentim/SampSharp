using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IPlayerPool.GetPlayerClickDispatcher"/>.
/// </summary>
[OpenMpEventHandler]
public partial interface IPlayerClickEventHandler
{
    /// <summary>
    /// Called when a player clicks on the map.
    /// </summary>
    /// <param name="player">The player who clicked on the map.</param>
    /// <param name="pos">The position on the map that was clicked.</param>
    void OnPlayerClickMap(IPlayer player, Vector3 pos);

    /// <summary>
    /// Called when a player clicks on another player.
    /// </summary>
    /// <param name="player">The player who performed the click.</param>
    /// <param name="clicked">The player who was clicked.</param>
    /// <param name="source">The source of the click event.</param>
    void OnPlayerClickPlayer(IPlayer player, IPlayer clicked, PlayerClickSource source);
}
