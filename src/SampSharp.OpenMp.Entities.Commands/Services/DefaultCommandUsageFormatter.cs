using PlayerComponent = SampSharp.Entities.SAMP.Player;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Default implementation of command usage message formatting and delivery.
/// Sends messages via player.SendClientMessage() or ConsoleCommandDispatchContext.SendMessage().
/// </summary>
public class DefaultCommandUsageFormatter : ICommandUsageFormatter
{
    public void FormatUsage(PlayerComponent player, CommandDefinition command)
    {
        var message = FormatUsageMessage(command);
        player.SendClientMessage(message);
    }

    public void FormatNotFound(PlayerComponent player, string input)
    {
        var message = $"Unknown command: {input}";
        player.SendClientMessage(message);
    }

    public void FormatPermissionDenied(PlayerComponent player, CommandDefinition command)
    {
        var message = "You don't have permission to use this command.";
        player.SendClientMessage(message);
    }

    public void FormatUsage(ConsoleCommandDispatchContext context, CommandDefinition command)
    {
        var message = FormatUsageMessage(command);
        context.SendMessage(message);
    }

    public void FormatNotFound(ConsoleCommandDispatchContext context, string input)
    {
        var message = $"Unknown command: {input}";
        context.SendMessage(message);
    }

    /// <summary>
    /// Formats a usage message for a command with all its overloads.
    /// </summary>
    private string FormatUsageMessage(CommandDefinition command)
    {
        if (command.Overloads.Count == 1)
        {
            return FormatSingleOverloadUsage(command);
        }

        return FormatMultipleOverloadsUsage(command);
    }

    /// <summary>
    /// Formats usage for a single overload command.
    /// </summary>
    private string FormatSingleOverloadUsage(CommandDefinition command)
    {
        var overload = command.Overloads.First();
        var parameterPart = FormatParametersUsage(overload.ParsedParameters);
        return $"Usage: /{command.FullName}{(string.IsNullOrEmpty(parameterPart) ? "" : " " + parameterPart)}";
    }

    /// <summary>
    /// Formats usage for multiple overloads command.
    /// </summary>
    private string FormatMultipleOverloadsUsage(CommandDefinition command)
    {
        var lines = new List<string>
        {
            "Usage:"
        };
        foreach (var overload in command.Overloads)
        {
            var parameterPart = FormatParametersUsage(overload.ParsedParameters);
            lines.Add($"  /{command.FullName}{(string.IsNullOrEmpty(parameterPart) ? "" : " " + parameterPart)}");
        }

        return string.Join("\n", lines);
    }

    /// <summary>
    /// Formats parameter list with &lt;required&gt; and [optional] notation.
    /// </summary>
    private string FormatParametersUsage(CommandParameterInfo[] parameters)
    {
        if (parameters.Length == 0)
        {
            return "";
        }

        var parts = new List<string>();
        foreach (var param in parameters)
        {
            var wrapper = param.IsRequired ? $"<{param.Name}>" : $"[{param.Name}]";
            parts.Add(wrapper);
        }

        return string.Join(" ", parts);
    }
}