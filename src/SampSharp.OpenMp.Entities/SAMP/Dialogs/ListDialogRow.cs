namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a row in a <see cref="ListDialog" />.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ListDialogRow" /> class.
/// </remarks>
/// <param name="text">The text.</param>
public class ListDialogRow(string text) : IDialogRow
{

    /// <summary>
    /// Gets the text.
    /// </summary>
    public string Text { get; } = text ?? throw new ArgumentNullException(nameof(text));

    /// <summary>
    /// Gets or sets the tag. The tag can be used to associate data with this row which can be retrieved when the user responds to the dialog.
    /// </summary>
    public object? Tag { get; set; }

    string IDialogRow.RawText => Text;
}