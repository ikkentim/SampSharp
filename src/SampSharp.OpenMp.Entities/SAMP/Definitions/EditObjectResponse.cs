namespace SampSharp.Entities.SAMP;

/// <summary>
/// Contains all object editing responses.
/// </summary>
public enum EditObjectResponse
{
    /// <summary>
    /// Editing has been canceled.
    /// </summary>
    Cancel = 0,

    /// <summary>
    /// The current is the final edit state.
    /// </summary>
    Final = 1,

    /// <summary>
    /// The current is an updated edit state.
    /// </summary>
    Update = 2
}