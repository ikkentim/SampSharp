namespace SampSharp.Entities.SAMP.Commands;

/// <summary>Describes a registered command.</summary>
public class CommandInfo
{
    /// <summary>Initializes a new instance.</summary>
    public CommandInfo(string name, CommandParameterInfo[] parameters)
    {
        Name = name;
        Parameters = parameters;
    }

    /// <summary>Command name (without leading slash).</summary>
    public string Name { get; }

    /// <summary>Parameter descriptors for the command (excluding the prefix and DI parameters).</summary>
    public CommandParameterInfo[] Parameters { get; }
}
