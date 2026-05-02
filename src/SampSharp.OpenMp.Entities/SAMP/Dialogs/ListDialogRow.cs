namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a row in a <see cref="ListDialog" />.
/// </summary>
public class ListDialogRow : IDialogRow
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListDialogRow" /> class.
    /// </summary>
    /// <param name="text">The text.</param>
    public ListDialogRow(string text)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
    }

    /// <summary>
    /// Gets the text.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Gets or sets the tag. The tag can be used to associate data with this row which can be retrieved when the user responds to the dialog.
    /// </summary>
    public object? Tag { get; set; }

    string IDialogRow.RawText => Text;
}