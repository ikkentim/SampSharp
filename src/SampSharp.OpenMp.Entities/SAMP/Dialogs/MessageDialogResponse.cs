namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a response to a <see cref="MessageDialog" />.
/// </summary>
public struct MessageDialogResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MessageDialogResponse" /> struct.
    /// </summary>
    /// <param name="response">The way in which the player has responded to the dialog.</param>
    public MessageDialogResponse(DialogResponse response)
    {
        Response = response;
    }

    /// <summary>
    /// Gets the way in which the player has responded to the dialog.
    /// </summary>
    public DialogResponse Response { get; }
}