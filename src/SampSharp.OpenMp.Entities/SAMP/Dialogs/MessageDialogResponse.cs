namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a response to a <see cref="MessageDialog" />.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="MessageDialogResponse" /> struct.
/// </remarks>
/// <param name="response">The way in which the player has responded to the dialog.</param>
public struct MessageDialogResponse(DialogResponse response)
{

    /// <summary>
    /// Gets the way in which the player has responded to the dialog.
    /// </summary>
    public DialogResponse Response { get; } = response;
}