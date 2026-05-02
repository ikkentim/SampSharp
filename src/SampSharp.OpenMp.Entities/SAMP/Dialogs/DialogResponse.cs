namespace SampSharp.Entities.SAMP;

/// <summary>
/// Contains types of responses to a dialog.
/// </summary>
public enum DialogResponse
{
    /// <summary>
    /// The player responded by selecting the left button.
    /// </summary>
    LeftButton = 1,

    /// <summary>
    /// The player responded by selecting the right button or closing the dialog.
    /// </summary>
    RightButtonOrCancel = 0,

    /// <summary>
    /// The player disconnected while the dialog was still open.
    /// </summary>
    Disconnected = -1
}