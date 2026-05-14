namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a dialog with a message.
/// </summary>
public class MessageDialog : IDialog<MessageDialogResponse>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MessageDialog" /> class.
    /// </summary>
    /// <param name="caption">The caption.</param>
    /// <param name="content">The content.</param>
    /// <param name="button1">The text on the left button.</param>
    /// <param name="button2">The text on the right button. If the value is <see langword="null" />, the right button is hidden.</param>
    public MessageDialog(string? caption, string? content, string? button1, string? button2 = null)
    {
        Caption = caption;
        Content = content;
        Button1 = button1;
        Button2 = button2;
    }

    DialogStyle IDialog.Style => DialogStyle.MessageBox;


    /// <summary>
    /// Gets or sets the caption of this message dialog.
    /// </summary>
    public string? Caption { get; set; }

    /// <summary>
    /// Gets or sets the content of this message dialog.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Gets or sets the text on the left button of this message dialog.
    /// </summary>
    public string? Button1 { get; set; }

    /// <summary>
    /// Gets or sets the text on the right button of this message dialog. If the value is <see langword="null" />, the right button is hidden.
    /// </summary>
    public string? Button2 { get; set; }

    MessageDialogResponse IDialog<MessageDialogResponse>.Translate(DialogResult dialogResult)
    {
        return new MessageDialogResponse(dialogResult.Response);
    }
}