namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IPlayerPool.GetPlayerDamageDispatcher"/>.
/// </summary>
[OpenMpEventHandler]
public partial interface IPlayerDamageEventHandler
{
    /// <summary>
    /// Called when a player dies.
    /// </summary>
    /// <param name="player">The player who died.</param>
    /// <param name="killer">The player who killed the other player, or <c>null</c> if no killer.</param>
    /// <param name="reason">The reason for the death (e.g., weapon ID).</param>
    void OnPlayerDeath(IPlayer player, IPlayer killer, int reason);

    /// <summary>
    /// Called when a player takes damage.
    /// </summary>
    /// <param name="player">The player who took damage.</param>
    /// <param name="from">The player who caused the damage, or <c>null</c> if no specific source.</param>
    /// <param name="amount">The amount of damage taken.</param>
    /// <param name="weapon">The weapon ID used to cause the damage.</param>
    /// <param name="part">The body part that was hit.</param>
    void OnPlayerTakeDamage(IPlayer player, IPlayer from, float amount, uint weapon, BodyPart part);

    /// <summary>
    /// Called when a player gives damage to another player.
    /// </summary>
    /// <param name="player">The player who caused the damage.</param>
    /// <param name="to">The player who received the damage.</param>
    /// <param name="amount">The amount of damage dealt.</param>
    /// <param name="weapon">The weapon ID used to deal the damage.</param>
    /// <param name="part">The body part that was hit.</param>
    void OnPlayerGiveDamage(IPlayer player, IPlayer to, float amount, uint weapon, BodyPart part);
}
