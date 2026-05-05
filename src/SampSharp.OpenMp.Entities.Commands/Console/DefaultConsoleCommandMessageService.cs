namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Default implementation of IConsoleCommandMessageService.
/// Sends formatted messages to the console via SendMessage.
/// </summary>
public class DefaultConsoleCommandMessageService : IConsoleCommandMessageService
{
    private readonly ICommandTextFormatter _formatter;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultConsoleCommandMessageService"/> class with the specified command text formatter.
    /// </summary>
    /// <param name="formatter">A formatter used to format command text.</param>
    public DefaultConsoleCommandMessageService(ICommandTextFormatter formatter)
    {
        ArgumentNullException.ThrowIfNull(formatter);

        _formatter = formatter;
    }

    /// <inheritdoc />
    public bool SendUsage(ConsoleCommandDispatchContext context, CommandDefinition command)
    {
        if (command.Overloads.Count == 1)
        {
            var overload = command.Overloads.First();
            var text = _formatter.FormatCommandUsage(command.Name, command.Group?.ToString(), overload.ParsedParameters, includeSlash: false);

            context.SendMessage($"Usage: {text}");
        }
        else
        {
            context.SendMessage("Usage:");
            foreach (var overload in command.Overloads)
            {
                var text = _formatter.FormatCommandUsage(command.Name, command.Group?.ToString(), overload.ParsedParameters, includeSlash: false);
                
                context.SendMessage($"  {text}");
            }
        }

        return true;
    }
}
