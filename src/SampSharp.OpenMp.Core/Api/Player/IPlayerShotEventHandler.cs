namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IPlayerPool.GetPlayerShotDispatcher"/>.
/// </summary>
[OpenMpEventHandler]
public partial interface IPlayerShotEventHandler
{

    /// <summary>
    /// Called when a player fires a shot that does not hit any target.
    /// </summary>
    /// <param name="player">The player who fired the shot.</param>
    /// <param name="bulletData">The data of the bullet fired.</param>
    /// <returns><see langword="true" /> if the event should be processed; otherwise, <see langword="false" /> to ignore it.</returns>
    bool OnPlayerShotMissed(IPlayer player, ref PlayerBulletData bulletData);

    /// <summary>
    /// Called when a player fires a shot that hits another player.
    /// </summary>
    /// <param name="player">The player who fired the shot.</param>
    /// <param name="target">The player who was hit by the shot.</param>
    /// <param name="bulletData">The data of the bullet fired.</param>
    /// <returns><see langword="true" /> if the event should be processed; otherwise, <see langword="false" /> to ignore it.</returns>
    bool OnPlayerShotPlayer(IPlayer player, IPlayer target, ref PlayerBulletData bulletData);

    /// <summary>
    /// Called when a player fires a shot that hits a vehicle.
    /// </summary>
    /// <param name="player">The player who fired the shot.</param>
    /// <param name="target">The vehicle that was hit by the shot.</param>
    /// <param name="bulletData">The data of the bullet fired.</param>
    /// <returns><see langword="true" /> if the event should be processed; otherwise, <see langword="false" /> to ignore it.</returns>
    bool OnPlayerShotVehicle(IPlayer player, IVehicle target, ref PlayerBulletData bulletData);

    /// <summary>
    /// Called when a player fires a shot that hits an object.
    /// </summary>
    /// <param name="player">The player who fired the shot.</param>
    /// <param name="target">The object that was hit by the shot.</param>
    /// <param name="bulletData">The data of the bullet fired.</param>
    /// <returns><see langword="true" /> if the event should be processed; otherwise, <see langword="false" /> to ignore it.</returns>
    bool OnPlayerShotObject(IPlayer player, IObject target, ref PlayerBulletData bulletData);

    /// <summary>
    /// Called when a player fires a shot that hits a player-owned object.
    /// </summary>
    /// <param name="player">The player who fired the shot.</param>
    /// <param name="target">The player-owned object that was hit by the shot.</param>
    /// <param name="bulletData">The data of the bullet fired.</param>
    /// <returns><see langword="true" /> if the event should be processed; otherwise, <see langword="false" /> to ignore it.</returns>
    bool OnPlayerShotPlayerObject(IPlayer player, IPlayerObject target, ref PlayerBulletData bulletData);
}
