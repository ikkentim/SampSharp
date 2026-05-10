namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Default implementation of IPlayerCommandMessageService.
/// Sends formatted messages to players via SendClientMessage.
/// </summary>
public class DefaultPlayerCommandMessageService : IPlayerCommandMessageService
{
    private readonly ICommandTextFormatter _formatter;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultPlayerCommandMessageService"/> class with the specified command text formatter.
    /// </summary>
    /// <param name="formatter">A formatter used to format command text.</param>
    public DefaultPlayerCommandMessageService(ICommandTextFormatter formatter)
    {
        ArgumentNullException.ThrowIfNull(formatter);

        _formatter = formatter;
    }

    /// <inheritdoc />
    public virtual void SendUsage(Player player, IReadOnlyList<CommandDefinition> overloads, string usedCommandName = "")
    {
        var messages = new List<string>();

        if (overloads.Count == 1)
        {
            var overload = overloads[0];
            // If usedCommandName is provided (e.g., an alias), use it as the complete path without the group
            // Otherwise, use the canonical command name with its group
            string commandName;
            string? group;

            if (usedCommandName.Length > 0)
            {
                commandName = usedCommandName;
                group = null; // Alias is the complete path
            }
            else
            {
                commandName = overload.Name;
                group = overload.Group?.ToString();
            }

            var text = _formatter.FormatCommandUsage(commandName, group, overload.ParsedParameters, includeSlash: true);
            messages.Add($"Usage: {text}");
        }
        else
        {
            messages.Add("Usage:");
            foreach (var overload in overloads)
            {
                // If usedCommandName is provided (e.g., an alias), use it as the complete path without the group
                // Otherwise, use the canonical command name with its group
                string commandName;
                string? group;

                if (usedCommandName.Length > 0)
                {
                    commandName = usedCommandName;
                    group = null; // Alias is the complete path
                }
                else
                {
                    commandName = overload.Name;
                    group = overload.Group?.ToString();
                }

                var text = _formatter.FormatCommandUsage(commandName, group, overload.ParsedParameters, includeSlash: true);
                messages.Add($"  {text}");
            }
        }

        foreach (var message in messages)
        {
            player.SendClientMessage(message);
        }
    }

    /// <inheritdoc />
    public virtual bool SendPermissionDenied(Player player, CommandDefinition overload)
    {
        const string message = "You do not have permission to use this command.";
        player.SendClientMessage(message);
        return true;
    }

    /// <inheritdoc />
    public virtual bool SendCommandNotFound(Player player, string input)
    {
        return false;
    }
}
