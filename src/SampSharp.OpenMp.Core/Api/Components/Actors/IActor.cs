namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IActor" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtensible), typeof(IEntity))]
public readonly partial struct IActor
{
    /// <summary>
    /// Sets the actor's skin.
    /// </summary>
    /// <param name="id">The ID of the skin to set.</param>
    public partial void SetSkin(int id);

    /// <summary>
    /// Gets the actor's skin ID.
    /// </summary>
    /// <returns>The ID of the actor's skin.</returns>
    public partial int GetSkin();

    /// <summary>
    /// Applies an animation to the actor.
    /// </summary>
    /// <param name="animation">The animation data to apply to the actor.</param>
    public partial void ApplyAnimation(AnimationData animation);

    /// <summary>
    /// Gets the animation currently applied to the actor.
    /// </summary>
    /// <returns>The animation data currently applied to the actor, or <c>null</c> if no animation is applied.</returns>
    public partial AnimationData? GetAnimation();

    /// <summary>
    /// Clears all animations applied to the actor.
    /// </summary>
    public partial void ClearAnimations();

    /// <summary>
    /// Sets the actor's health.
    /// </summary>
    /// <param name="health">The health value to set for the actor.</param>
    public partial void SetHealth(float health);

    /// <summary>
    /// Gets the actor's current health.
    /// </summary>
    /// <returns>The current health of the actor.</returns>
    public partial float GetHealth();

    /// <summary>
    /// Sets whether the actor is invulnerable.
    /// </summary>
    /// <param name="invuln"><c>true</c> to make the actor invulnerable; otherwise, <c>false</c>.</param>
    public partial void SetInvulnerable(bool invuln);

    /// <summary>
    /// Checks whether the actor is invulnerable.
    /// </summary>
    /// <returns><c>true</c> if the actor is invulnerable; otherwise, <c>false</c>.</returns>
    public partial bool IsInvulnerable();

    /// <summary>
    /// Checks if the actor is streamed in for a specific player.
    /// </summary>
    /// <param name="player">The player to check streaming status for.</param>
    /// <returns><c>true</c> if the actor is streamed in for the player; otherwise, <c>false</c>.</returns>
    public partial bool IsStreamedInForPlayer(IPlayer player);

    /// <summary>
    /// Streams the actor in for a specific player.
    /// </summary>
    /// <param name="player">The player for whom the actor should be streamed in.</param>
    public partial void StreamInForPlayer(IPlayer player);

    /// <summary>
    /// Streams the actor out for a specific player.
    /// </summary>
    /// <param name="player">The player for whom the actor should be streamed out.</param>
    public partial void StreamOutForPlayer(IPlayer player);

    /// <summary>
    /// Gets the spawn data of the actor.
    /// </summary>
    /// <returns>The spawn data of the actor, including position, facing angle, and skin.</returns>
    public partial ref ActorSpawnData GetSpawnData();
}