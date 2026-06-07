using Microsoft.Extensions.Options;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Default implementation of IPlayerCommandMessageService.
/// Sends formatted messages to players via SendClientMessage.
/// </summary>
public class DefaultPlayerCommandMessageService : IPlayerCommandMessageService
{
    private readonly ICommandTextFormatter _formatter;
    private readonly PlayerCommandServiceOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultPlayerCommandMessageService"/> class with the specified command text formatter.
    /// </summary>
    /// <param name="formatter">A formatter used to format command text.</param>
    /// <param name="options">The command service options.</param>
    public DefaultPlayerCommandMessageService(ICommandTextFormatter formatter, IOptions<PlayerCommandServiceOptions> options)
    {
        ArgumentNullException.ThrowIfNull(formatter);
        ArgumentNullException.ThrowIfNull(options);

        _formatter = formatter;
        _options = options.Value;
    }

    /// <inheritdoc />
    public virtual void SendUsage(Player player, IReadOnlyList<CommandDefinition> overloads, string usedCommandName = "")
    {
        ArgumentNullException.ThrowIfNull(player);
        ArgumentNullException.ThrowIfNull(overloads);
        ArgumentNullException.ThrowIfNull(usedCommandName);

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
            messages.Add($"{_options.UsageMessagePrefix} {text}");
        }
        else
        {
            messages.Add(_options.UsageMessagePrefix);
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
            player.SendClientMessage(_options.UsageMessageColor, message);
        }
    }

    /// <inheritdoc />
    public virtual bool SendPermissionDenied(Player player, CommandDefinition overload)
    {
        ArgumentNullException.ThrowIfNull(player);
        ArgumentNullException.ThrowIfNull(overload);

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
