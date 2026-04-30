namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IDialogsComponent.GetEventDispatcher" />.
/// </summary>
[OpenMpEventHandler]
public partial interface IPlayerDialogEventHandler
{
    /// <summary>
    /// Called when a player responds to a dialog.
    /// </summary>
    /// <param name="player">The player who responded to the dialog.</param>
    /// <param name="dialogId">The ID of the dialog.</param>
    /// <param name="response">The button that was clicked.</param>
    /// <param name="listItem">The selected list item (for list dialogs), or -1 for other types.</param>
    /// <param name="inputText">The text entered (for input dialogs), or empty string for other types.</param>
    void OnDialogResponse(IPlayer player, int dialogId, DialogResponse response, int listItem, string inputText);
}