namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a raw response value to a dialog.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DialogResult" /> struct.
/// </remarks>
/// <param name="response">The way in which the player has responded to the dialog.</param>
/// <param name="listItem">The index of item selected by the player in the dialog.</param>
/// <param name="inputText">The text entered by the player in the dialog.</param>
public struct DialogResult(DialogResponse response, int listItem, string? inputText)
{
    /// <summary>
    /// Gets the way in which the player has responded to the dialog.
    /// </summary>
    public DialogResponse Response { get; } = response;

    /// <summary>
    /// Gets the index of item selected by the player in the dialog.
    /// </summary>
    public int ListItem { get; } = listItem;

    /// <summary>
    /// Gets the text entered by the player in the dialog.
    /// </summary>
    public string? InputText { get; } = inputText;
}
