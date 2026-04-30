namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IPickupsComponent.GetEventDispatcher" />.
/// </summary>
[OpenMpEventHandler]
public partial interface IPickupEventHandler
{
    /// <summary>
    /// Called when a player picks up a pickup.
    /// </summary>
    /// <param name="player">The player who picked up the pickup.</param>
    /// <param name="pickup">The pickup that was picked up.</param>
    void OnPlayerPickUpPickup(IPlayer player, IPickup pickup);
}