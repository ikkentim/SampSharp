namespace SampSharp.Entities.SAMP;

/// <summary>
/// Contains all PlayerMarker modes.
/// </summary>
public enum PlayerMarkersMode
{
    /// <summary>
    /// No makers.
    /// </summary>
    Off = 0,

    /// <summary>
    /// All markers.
    /// </summary>
    Global = 1,

    /// <summary>
    /// All markers within the streamed area.
    /// </summary>
    Streamed = 2
}