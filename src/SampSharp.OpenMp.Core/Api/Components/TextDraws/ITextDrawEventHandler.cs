namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="ITextDrawsComponent.GetEventDispatcher" />.
/// </summary>
[OpenMpEventHandler]
public partial interface ITextDrawEventHandler
{
    /// <summary>
    /// Called when a player clicks on a text draw.
    /// </summary>
    /// <param name="player">The player who clicked the text draw.</param>
    /// <param name="td">The text draw that was clicked.</param>
    void OnPlayerClickTextDraw(IPlayer player, ITextDraw td);

    /// <summary>
    /// Called when a player clicks on a player-specific text draw.
    /// </summary>
    /// <param name="player">The player who clicked the text draw.</param>
    /// <param name="td">The player-specific text draw that was clicked.</param>
    void OnPlayerClickPlayerTextDraw(IPlayer player, IPlayerTextDraw td);

    /// <summary>
    /// Called when a player cancels text draw selection.
    /// </summary>
    /// <param name="player">The player who canceled the selection.</param>
    /// <returns><see langword="true" /> if the cancellation was handled; otherwise, <see langword="false" />.</returns>
    bool OnPlayerCancelTextDrawSelection(IPlayer player);

    /// <summary>
    /// Called when a player cancels player-specific text draw selection.
    /// </summary>
    /// <param name="player">The player who canceled the selection.</param>
    /// <returns><see langword="true" /> if the cancellation was handled; otherwise, <see langword="false" />.</returns>
    bool OnPlayerCancelPlayerTextDrawSelection(IPlayer player);
}