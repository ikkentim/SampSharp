namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a collection of dialog rows of type <see cref="ListDialogRow" />.
/// </summary>
public class ListDialogRowCollection : DialogRowCollection<ListDialogRow>
{
    /// <summary>
    /// Adds a row to the list with the specified <paramref name="text" />.
    /// </summary>
    /// <param name="text">The text of the row to add.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="text" /> is null.</exception>
    public void Add(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        Add(new ListDialogRow(text));
    }
}