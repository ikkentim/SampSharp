namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Specifies the style/type of dialog to display to a player.
/// </summary>
public enum DialogStyle
{
    /// <summary>
    /// Message box dialog with two buttons.
    /// </summary>
    DialogStyle_MSGBOX = 0,

    /// <summary>
    /// Input dialog with a text input field.
    /// </summary>
    DialogStyle_INPUT,

    /// <summary>
    /// List dialog with selectable items.
    /// </summary>
    DialogStyle_LIST,

    /// <summary>
    /// Password input dialog (text is masked).
    /// </summary>
    DialogStyle_PASSWORD,

    /// <summary>
    /// Tabular list dialog with columns.
    /// </summary>
    DialogStyle_TABLIST,

    /// <summary>
    /// Tabular list dialog with headers.
    /// </summary>
    DialogStyle_TABLIST_HEADERS
}