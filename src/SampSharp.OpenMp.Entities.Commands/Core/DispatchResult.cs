namespace SampSharp.Entities.SAMP.Commands;

/// <summary>Result of a command dispatch operation.</summary>
public class DispatchResult
{
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

    /// <summary>The executed command overload (if matched).</summary>
    public CommandDefinition? CommandOverload { get; set; }

    /// <summary>All overloads of the command (for usage display).</summary>
    internal IReadOnlyList<CommandDefinition>? AllOverloads { get; set; }

    /// <summary>Parsed argument values for the command (if successfully matched).</summary>
    public object?[]? ParsedArguments { get; set; }

    /// <summary>Creates a successful result.</summary>
    public static DispatchResult CreateSuccess()
    {
        return new DispatchResult(DispatchResponse.Success);
    }

    /// <summary>Creates a "command not found" result.</summary>
    public static DispatchResult CreateNotFound()
    {
        return new DispatchResult(DispatchResponse.CommandNotFound);
    }

    /// <summary>Creates an "invalid arguments" result.</summary>
    public static DispatchResult CreateInvalidArguments(string? usageMessage = null)
    {
        return new DispatchResult(DispatchResponse.InvalidArguments)
        {
            UsageMessage = usageMessage
        };
    }

    /// <summary>Creates a "permission denied" result.</summary>
    public static DispatchResult CreatePermissionDenied(string? message = null)
    {
        return new DispatchResult(DispatchResponse.PermissionDenied)
        {
            Message = message
        };
    }

    /// <summary>Creates an error result.</summary>
    public static DispatchResult CreateError(string? message = null)
    {
        return new DispatchResult(DispatchResponse.Error)
        {
            Message = message
        };
    }
}