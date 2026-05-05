namespace SampSharp.Entities.SAMP.Commands;

/// <summary>Response codes for command dispatch.</summary>
public enum DispatchResponse
{
    /// <summary>Command executed successfully.</summary>
    Success,

    /// <summary>Command was not found.</summary>
    CommandNotFound,

    /// <summary>Command was found but arguments were invalid.</summary>
    InvalidArguments,

    /// <summary>Player does not have permission (player commands only).</summary>
    PermissionDenied,

    /// <summary>An error occurred during execution.</summary>
    Error
}