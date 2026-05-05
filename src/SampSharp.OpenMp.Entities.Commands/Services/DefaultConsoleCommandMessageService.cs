namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Default implementation of IConsoleCommandMessageService.
/// Sends formatted messages to the console via SendMessage.
/// </summary>
public class DefaultConsoleCommandMessageService : IConsoleCommandMessageService
{
    private readonly ICommandTextFormatter _formatter;

    public DefaultConsoleCommandMessageService(ICommandTextFormatter formatter)
    {
        _formatter = formatter;
    }

    /// <inheritdoc />
    public bool SendUsage(ConsoleCommandDispatchContext context, CommandDefinition command)
    {
        var messages = new List<string>();

        if (command.Overloads.Count == 1)
        {
            var overload = command.Overloads.First();
            var text = _formatter.FormatCommandUsage(command.Name, command.Group?.ToString(), overload.ParsedParameters, includeSlash: false);
            messages.Add($"Usage: {text}");
        }
        else
        {
            messages.Add("Usage:");
            foreach (var overload in command.Overloads)
            {
                var text = _formatter.FormatCommandUsage(command.Name, command.Group?.ToString(), overload.ParsedParameters, includeSlash: false);
                messages.Add($"  {text}");
            }
        }

        foreach (var message in messages)
        {
            context.SendMessage(message);
        }

        return true;
    }
}
