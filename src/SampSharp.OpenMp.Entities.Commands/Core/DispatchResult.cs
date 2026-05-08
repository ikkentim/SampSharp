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

    /// <summary>The executed command overload (if matched).</summary>
    public CommandDefinition? CommandOverload { get; set; }

    /// <summary>All overloads of the command (for usage display).</summary>
    internal IReadOnlyList<CommandDefinition>? AllOverloads { get; set; }

    /// <summary>The actual command name/path used as input (e.g., "msg" if an alias was used).</summary>
    public string UsedCommandName { get; set; } = string.Empty;

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
    public static DispatchResult CreateInvalidArguments()
    {
        return new DispatchResult(DispatchResponse.InvalidArguments);
    }

    /// <summary>Creates a "permission denied" result.</summary>
    public static DispatchResult CreatePermissionDenied()
    {
        return new DispatchResult(DispatchResponse.PermissionDenied);
    }
}