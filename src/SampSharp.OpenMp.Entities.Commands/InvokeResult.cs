namespace SampSharp.Entities.SAMP.Commands;

/// <summary>Result of a command invocation.</summary>
public readonly struct InvokeResult
{
    /// <summary>Singleton "command not found" result.</summary>
    public static readonly InvokeResult CommandNotFound = new(InvokeResponse.CommandNotFound);

    /// <summary>Singleton "success" result.</summary>
    public static readonly InvokeResult Success = new(InvokeResponse.Success);

    /// <summary>Initializes a new instance.</summary>
    /// <param name="response">The response.</param>
    /// <param name="usageMessage">Usage message — only meaningful for <see cref="InvokeResponse.InvalidArguments" />.</param>
    public InvokeResult(InvokeResponse response, string? usageMessage = null)
    {
        Response = response;
        UsageMessage = usageMessage;
    }

    /// <summary>The response code.</summary>
    public InvokeResponse Response { get; }

    /// <summary>Usage hint text shown to the player when arguments are invalid.</summary>
    public string? UsageMessage { get; }
}
