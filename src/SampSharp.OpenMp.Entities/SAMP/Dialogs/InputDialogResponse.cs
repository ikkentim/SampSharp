namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a response to a <see cref="InputDialog" />.
/// </summary>
public struct InputDialogResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MessageDialogResponse" /> struct.
    /// </summary>
    /// <param name="response">The way in which the player has responded to the dialog.</param>
    /// <param name="inputText">The text the player has entered into the input field.</param>
    public InputDialogResponse(DialogResponse response, string? inputText)
    {
        Response = response;
        InputText = inputText;
    }

    /// <summary>
    /// Gets the way in which the player has responded to the dialog.
    /// </summary>
    public DialogResponse Response { get; }

    /// <summary>
    /// Gets the text the player has entered into the input field.
    /// </summary>
    public string? InputText { get; }
}