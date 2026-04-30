namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the possible responses to an object editing request.
/// </summary>
public enum ObjectEditResponse
{
    /// <summary>
    /// Indicates that the object editing process was canceled.
    /// </summary>
    Cancel,

    /// <summary>
    /// Indicates that the object editing process was completed and finalized.
    /// </summary>
    Final,

    /// <summary>
    /// Indicates that the object editing process is in progress and has been updated.
    /// </summary>
    Update
}