namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a response to a <see cref="TablistDialog" />.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="MessageDialogResponse" /> struct.
/// </remarks>
/// <param name="response">The way in which the player has responded to the dialog.</param>
/// <param name="itemIndex">The index of the item the player selected in the dialog.</param>
/// <param name="item">The item the player selected in the dialog.</param>
public struct TablistDialogResponse(DialogResponse response, int itemIndex, TablistDialogRow? item)
{
    /// <summary>
    /// Gets the way in which the player has responded to the dialog.
    /// </summary>
    public DialogResponse Response { get; } = response;

    /// <summary>
    /// Gets the index of the item the player selected in the dialog.
    /// </summary>
    public int ItemIndex { get; } = itemIndex;

    /// <summary>
    /// Gets the item the player selected in the dialog.
    /// </summary>
    public TablistDialogRow? Item { get; } = item;
}
