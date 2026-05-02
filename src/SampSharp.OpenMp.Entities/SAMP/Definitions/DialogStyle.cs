namespace SampSharp.Entities.SAMP;

/// <summary>
/// Contains all dialog styles.
/// </summary>
public enum DialogStyle
{
    /// <summary>
    /// A box with a caption, text and one or two buttons.
    /// </summary>
    MessageBox = 0,

    /// <summary>
    /// A box with a caption, text, an input box and one or two buttons.
    /// </summary>
    Input = 1,

    /// <summary>
    /// A box with a caption, a bunch of selectable items and one or two buttons.
    /// </summary>
    List = 2,

    /// <summary>
    /// A box with a caption, text, a password input box and one or two buttons.
    /// </summary>
    Password = 3,

    /// <summary>
    /// A box with a caption, a bunch of selectable rows which contain a number of columns and one or two buttons.
    /// </summary>
    Tablist = 4,

    /// <summary>
    /// A box with a caption, a bunch of selectable rows which contain a number of columns with a header and one or two buttons.
    /// </summary>
    TablistHeaders = 5
}