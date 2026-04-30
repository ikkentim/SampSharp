namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IPlayerPool.GetPlayerSpawnDispatcher"/>.
/// </summary>
[OpenMpEventHandler]
public partial interface IPlayerSpawnEventHandler
{
    /// <summary>
    /// Called when a player requests to spawn.
    /// </summary>
    /// <param name="player">The player requesting to spawn.</param>
    /// <returns><see langword="true" /> if the player is allowed to spawn; otherwise, <see langword="false" />.</returns>
    bool OnPlayerRequestSpawn(IPlayer player);

    /// <summary>
    /// Called when a player successfully spawns.
    /// </summary>
    /// <param name="player">The player who has spawned.</param>
    void OnPlayerSpawn(IPlayer player);
}