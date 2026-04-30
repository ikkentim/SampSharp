namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPlayerDialogData" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtension))]
public readonly partial struct IPlayerDialogData
{
    /// <inheritdoc />
    public static UID ExtensionId => new(0xbc03376aa3591a11);

    /// <summary>
    /// Hides any dialog currently shown to the player.
    /// </summary>
    /// <param name="player">The player to hide the dialog for.</param>
    public partial void Hide(IPlayer player);

    /// <summary>
    /// Shows a dialog to the player.
    /// </summary>
    /// <param name="player">The player to show the dialog to.</param>
    /// <param name="id">The ID of the dialog.</param>
    /// <param name="style">The style/type of dialog.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="body">The body/content text of the dialog.</param>
    /// <param name="button1">The text for the first (right) button.</param>
    /// <param name="button2">The text for the second (left) button.</param>
    public partial void Show(IPlayer player, int id, DialogStyle style, string title, string body, string button1, string button2);

    /// <summary>
    /// Gets the information of the current dialog shown to the player.
    /// </summary>
    /// <param name="id">When the method returns, contains the ID of the dialog.</param>
    /// <param name="style">When the method returns, contains the style of the dialog.</param>
    /// <param name="title">When the method returns, contains the title of the dialog.</param>
    /// <param name="body">When the method returns, contains the body text of the dialog.</param>
    /// <param name="button1">When the method returns, contains the text of the first button.</param>
    /// <param name="button2">When the method returns, contains the text of the second button.</param>
    public partial void Get(out int id, out DialogStyle style, out string? title, out string? body, out string? button1, out string? button2);

    /// <summary>
    /// Gets the ID of the currently active dialog for the player.
    /// </summary>
    /// <returns>The ID of the active dialog, or -1 if no dialog is active.</returns>
    public partial int GetActiveID();
}