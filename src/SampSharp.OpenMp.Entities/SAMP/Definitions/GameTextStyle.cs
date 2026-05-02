namespace SampSharp.Entities.SAMP;

/// <summary>
/// Specifies the style of a game text, which determines its font and alignment and other characteristics.
/// </summary>
public enum GameTextStyle
{
    /// <summary>
    /// Text appear in the center of the screen. Appears for 9 seconds regardless of time setting. Hides textdraws and any other game text on screen.
    /// </summary>
    Style0,

    /// <summary>
    /// Text appears in the bottom right  corner of the screen. Fades out after 8 seconds, regardless of time set. If you have a time setting longer than that, it will re-appear after fading out and repeat until the time ends.
    /// </summary>
    Style1,

    /// <summary>
    /// Text appear in the center of the screen.
    /// </summary>
    Style2,

    /// <summary>
    /// Text appear in the center of the screen.
    /// </summary>
    Style3,

    /// <summary>
    /// Text appear in the center of the screen.
    /// </summary>
    Style4,

    /// <summary>
    /// Text appear in the center of the screen. Displays for 3 seconds, regardless of what time you set. Will refuse to be shown if it is 'spammed'.
    /// </summary>
    Style5,

    /// <summary>
    /// Appears op the top of the screen.
    /// </summary>
    Style6
}
