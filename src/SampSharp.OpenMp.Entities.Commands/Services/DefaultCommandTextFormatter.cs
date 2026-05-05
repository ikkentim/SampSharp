namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Default implementation of ICommandTextFormatter.
/// Generates standard formatted command text.
/// </summary>
public class DefaultCommandTextFormatter : ICommandTextFormatter
{
    public string FormatCommandUsage(string commandName, string? group, CommandParameterInfo[] parameters, bool includeSlash = true)
    {
        var prefix = group != null ? $"{group} {commandName}" : commandName;
        var slash = includeSlash ? "/" : "";

        if (parameters.Length == 0)
        {
            return $"{slash}{prefix}";
        }

        var args = string.Join(" ", parameters.Select(p => p.IsRequired ? $"<{p.Name}>" : $"[{p.Name}]"));

        return $"{slash}{prefix} {args}";
    }

    public string FormatCommandNotFound(string commandText)
    {
        return $"Unknown command: {commandText}";
    }

    public string FormatPermissionDenied(string commandText)
    {
        return "You do not have permission to use this command.";
    }
}
