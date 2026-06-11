using Microsoft.Extensions.Options;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Default implementation of IConsoleCommandMessageService.
/// Sends formatted messages to the console via SendMessage.
/// </summary>
internal class DefaultConsoleCommandMessageService : IConsoleCommandMessageService
{
    private readonly ICommandTextFormatter _formatter;
    private readonly ConsoleCommandServiceOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultConsoleCommandMessageService"/> class with the specified command text formatter.
    /// </summary>
    /// <param name="formatter">A formatter used to format command text.</param>
    /// <param name="options">The command service options.</param>
    public DefaultConsoleCommandMessageService(ICommandTextFormatter formatter, IOptions<ConsoleCommandServiceOptions> options)
    {
        ArgumentNullException.ThrowIfNull(formatter);
        ArgumentNullException.ThrowIfNull(options);

        _formatter = formatter;
        _options = options.Value;
    }

    /// <inheritdoc />
    public virtual bool SendUsage(ConsoleCommandDispatchContext context, IReadOnlyList<CommandDefinition> overloads, string usedCommandName = "")
    {
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

            var text = _formatter.FormatCommandUsage(commandName, group, overload.ParsedParameters, includeSlash: false);

            context.SendMessage($"{_options.UsageMessagePrefix} {text}");
        }
        else
        {
            context.SendMessage(_options.UsageMessagePrefix);
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

                var text = _formatter.FormatCommandUsage(commandName, group, overload.ParsedParameters, includeSlash: false);

                context.SendMessage($"  {text}");
            }
        }

        return true;
    }
}
