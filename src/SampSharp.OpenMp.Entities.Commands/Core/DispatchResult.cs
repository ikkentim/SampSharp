namespace SampSharp.Entities.SAMP.Commands.Core;

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

/// <summary>Result of a command dispatch operation.</summary>
public class DispatchResult
{
    /// <summary>Creates a successful result.</summary>
    public static DispatchResult CreateSuccess()
        => new(DispatchResponse.Success);

    /// <summary>Creates a "command not found" result.</summary>
    public static DispatchResult CreateNotFound()
        => new(DispatchResponse.CommandNotFound);

    /// <summary>Creates an "invalid arguments" result.</summary>
    public static DispatchResult CreateInvalidArguments(string? usageMessage = null)
        => new(DispatchResponse.InvalidArguments) { UsageMessage = usageMessage };

    /// <summary>Creates a "permission denied" result.</summary>
    public static DispatchResult CreatePermissionDenied(string? message = null)
        => new(DispatchResponse.PermissionDenied) { Message = message };

    /// <summary>Creates an error result.</summary>
    public static DispatchResult CreateError(string? message = null)
        => new(DispatchResponse.Error) { Message = message };

    private DispatchResult(DispatchResponse response)
    {
        Response = response;
    }

    /// <summary>The response code.</summary>
    public DispatchResponse Response { get; }

    /// <summary>General message (for errors, permission denied, etc.).</summary>
    public string? Message { get; set; }

    /// <summary>Usage message (for invalid arguments).</summary>
    public string? UsageMessage { get; set; }

    /// <summary>The executed command definition (if found).</summary>
    public CommandDefinition? CommandDefinition { get; set; }

    /// <summary>The executed command overload (if matched).</summary>
    public CommandOverload? CommandOverload { get; set; }
}
