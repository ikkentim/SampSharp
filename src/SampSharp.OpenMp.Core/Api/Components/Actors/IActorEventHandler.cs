namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IActorsComponent.GetEventDispatcher" />.
/// </summary>
[OpenMpEventHandler]
public partial interface IActorEventHandler
{
    /// <summary>
    /// Called when a player gives damage to an actor.
    /// </summary>
    /// <param name="player">The player who gave the damage.</param>
    /// <param name="actor">The actor that received the damage.</param>
    /// <param name="amount">The amount of damage dealt.</param>
    /// <param name="weapon">The weapon used to deal the damage.</param>
    /// <param name="part">The body part of the actor that was hit.</param>
    void OnPlayerGiveDamageActor(IPlayer player, IActor actor, float amount, uint weapon, BodyPart part);

    /// <summary>
    /// Called when an actor streams out for a player.
    /// </summary>
    /// <param name="actor">The actor that streamed out.</param>
    /// <param name="forPlayer">The player for whom the actor streamed out.</param>
    void OnActorStreamOut(IActor actor, IPlayer forPlayer);

    /// <summary>
    /// Called when an actor streams in for a player.
    /// </summary>
    /// <param name="actor">The actor that streamed in.</param>
    /// <param name="forPlayer">The player for whom the actor streamed in.</param>
    void OnActorStreamIn(IActor actor, IPlayer forPlayer);
}
