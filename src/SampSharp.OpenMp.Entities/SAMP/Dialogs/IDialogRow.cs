namespace SampSharp.Entities.SAMP;

/// <summary>
/// Provides the functionality of a dialog row
/// </summary>
public interface IDialogRow
{
    /// <summary>
    /// Gets the raw text as it is sent to SA:MP.
    /// </summary>
    string RawText { get; }
}