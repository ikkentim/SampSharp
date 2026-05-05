namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Default implementation of ICommandNameProvider.
/// Generates standard usage messages.
/// </summary>
public class DefaultCommandNameProvider : ICommandNameProvider
{
    public string GetUsageMessage(string commandName, string? group, CommandParameterInfo[] parameters, string? usageMessageKey = null)
    {
        var prefix = group != null ? $"{group} {commandName}" : commandName;

        if (parameters.Length == 0)
        {
            return $"Usage: /{prefix}";
        }

        var args = string.Join(" ", parameters.Select(p => p.IsRequired ? $"<{p.Name}>" : $"[{p.Name}]"));

        return $"Usage: /{prefix} {args}";
    }
}