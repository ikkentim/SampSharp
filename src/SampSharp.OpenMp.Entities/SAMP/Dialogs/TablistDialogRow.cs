namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a row in a <see cref="TablistDialog" />.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TablistDialogRow" /> class.
/// </remarks>
/// <param name="columns">The columns of the row.</param>
public class TablistDialogRow(params string[] columns) : IDialogRow
{

    /// <summary>
    /// Gets the columns of this tablist dialog row.
    /// </summary>
    public string[] Columns { get; } = columns ?? throw new ArgumentNullException(nameof(columns));

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