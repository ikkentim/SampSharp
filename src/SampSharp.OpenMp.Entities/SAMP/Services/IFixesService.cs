namespace SampSharp.Entities.SAMP;

/// <summary>Provides access to the open.mp <c>Fixes</c> component (legacy SA:MP behaviour shims).</summary>
public interface IFixesService
{
    /// <summary>Broadcasts a game-text message to every player using the Fixes pathway.</summary>
    /// <param name="message">The message.</param>
    /// <param name="time">The display duration.</param>
    /// <param name="style">The game text style/slot.</param>
    /// <returns><see langword="true" /> on success.</returns>
    bool SendGameTextToAll(string message, TimeSpan time, int style);

    /// <summary>Hides the game text in the specified style/slot for every player using the Fixes pathway.</summary>
    /// <param name="style">The game text style/slot.</param>
    /// <returns><see langword="true" /> on success.</returns>
    bool HideGameTextForAll(int style);

    /// <summary>Clears the active animation of the specified player or actor using the Fixes pathway.</summary>
    /// <param name="player">The player whose animation should be cleared, or <see langword="null" /> if targeting an actor.</param>
    /// <param name="actor">The actor whose animation should be cleared, or <see langword="null" /> if targeting a player.</param>
    void ClearAnimation(Player? player, Actor? actor);
}
