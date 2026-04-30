namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IMenusComponent.GetEventDispatcher" />.
/// </summary>
[OpenMpEventHandler]
public partial interface IMenuEventHandler
{
    /// <summary>
    /// Called when a player selects a row in a menu.
    /// </summary>
    /// <param name="player">The player who selected the row.</param>
    /// <param name="row">The row index that was selected.</param>
    void OnPlayerSelectedMenuRow(IPlayer player, byte row);

    /// <summary>
    /// Called when a player exits a menu (usually by pressing Escape).
    /// </summary>
    /// <param name="player">The player who exited the menu.</param>
    void OnPlayerExitedMenu(IPlayer player);
}