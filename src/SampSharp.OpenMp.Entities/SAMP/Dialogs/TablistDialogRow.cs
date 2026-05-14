namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a row in a <see cref="TablistDialog" />.
/// </summary>
public class TablistDialogRow : IDialogRow
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TablistDialogRow" /> class.
    /// </summary>
    /// <param name="columns">The columns of the row.</param>
    public TablistDialogRow(params string[] columns)
    {
        Columns = columns ?? throw new ArgumentNullException(nameof(columns));
    }

    /// <summary>
    /// Gets the columns of this tablist dialog row.
    /// </summary>
    public string[] Columns { get; }

    /// <summary>
    /// Gets the number of columns in this row.
    /// </summary>
    public int ColumnCount => Columns.Length;

    /// <summary>
    /// Gets or sets the tag. The tag can be used to associate data with this row which can be retrieved when the user responds to the dialog.
    /// </summary>
    public object? Tag { get; set; }

    string IDialogRow.RawText => string.Join("\t", Columns);
}