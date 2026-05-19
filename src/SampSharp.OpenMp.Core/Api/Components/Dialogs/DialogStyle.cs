namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Specifies the style/type of dialog to display to a player.
/// </summary>
public enum DialogStyle
{
    /// <summary>
    /// Message box dialog with two buttons.
    /// </summary>
    MSGBOX = 0,

    /// <summary>
    /// Input dialog with a text input field.
    /// </summary>
    INPUT,

    /// <summary>
    /// List dialog with selectable items.
    /// </summary>
    LIST,

    /// <summary>
    /// Password input dialog (text is masked).
    /// </summary>
    PASSWORD,

    /// <summary>
    /// Tabular list dialog with columns.
    /// </summary>
    TABLIST,

    /// <summary>
    /// Tabular list dialog with headers.
    /// </summary>
    TABLIST_HEADERS
}