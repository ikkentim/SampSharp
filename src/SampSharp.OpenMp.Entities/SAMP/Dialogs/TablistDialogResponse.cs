namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a response to a <see cref="TablistDialog" />.
/// </summary>
public struct TablistDialogResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MessageDialogResponse" /> struct.
    /// </summary>
    /// <param name="response">The way in which the player has responded to the dialog.</param>
    /// <param name="itemIndex">The index of the item the player selected in the dialog.</param>
    /// <param name="item">The item the player selected in the dialog.</param>
    public TablistDialogResponse(DialogResponse response, int itemIndex, TablistDialogRow? item)
    {
        Response = response;
        ItemIndex = itemIndex;
        Item = item;
    }

    /// <summary>
    /// Gets the way in which the player has responded to the dialog.
    /// </summary>
    public DialogResponse Response { get; }

    /// <summary>
    /// Gets the index of the item the player selected in the dialog.
    /// </summary>
    public int ItemIndex { get; }

    /// <summary>
    /// Gets the item the player selected in the dialog.
    /// </summary>
    public TablistDialogRow? Item { get; }
}