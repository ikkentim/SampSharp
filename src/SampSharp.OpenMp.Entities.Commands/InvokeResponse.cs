namespace SampSharp.Entities.SAMP.Commands;

/// <summary>Possible responses from a command invocation.</summary>
public enum InvokeResponse
{
    /// <summary>Command executed successfully.</summary>
    Success,
    /// <summary>No command matches the input text.</summary>
    CommandNotFound,
    /// <summary>Command was found but its arguments could not be parsed.</summary>
    InvalidArguments
}
