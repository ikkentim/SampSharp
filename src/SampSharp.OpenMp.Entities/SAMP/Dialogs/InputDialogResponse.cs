namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a response to a <see cref="InputDialog" />.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="MessageDialogResponse" /> struct.
/// </remarks>
/// <param name="response">The way in which the player has responded to the dialog.</param>
/// <param name="inputText">The text the player has entered into the input field.</param>
public struct InputDialogResponse(DialogResponse response, string? inputText)
{

    /// <summary>
    /// Gets the way in which the player has responded to the dialog.
    /// </summary>
    public DialogResponse Response { get; } = response;

    /// <summary>
    /// Gets the text the player has entered into the input field.
    /// </summary>
    public string? InputText { get; } = inputText;
}