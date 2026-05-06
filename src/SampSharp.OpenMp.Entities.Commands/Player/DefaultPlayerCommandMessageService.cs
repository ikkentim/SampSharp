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
    public void SendUsage(Player player, IReadOnlyList<CommandDefinition> overloads)
    {
        var messages = new List<string>();

        if (overloads.Count == 1)
        {
            var overload = overloads[0];
            var text = _formatter.FormatCommandUsage(overload.Name, overload.Group?.ToString(), overload.ParsedParameters, includeSlash: true);
            messages.Add($"Usage: {text}");
        }
        else
        {
            messages.Add("Usage:");
            foreach (var overload in overloads)
            {
                var text = _formatter.FormatCommandUsage(overload.Name, overload.Group?.ToString(), overload.ParsedParameters, includeSlash: true);
                messages.Add($"  {text}");
            }
        }

        foreach (var message in messages)
        {
            player.SendClientMessage(message);
        }
    }

    /// <inheritdoc />
    public bool SendPermissionDenied(Player player, CommandDefinition overload)
    {
        var message = FormatPermissionDenied(""); //  TODO input text
        player.SendClientMessage(message);
        return true;// TODO: option to not print message
    }

    /// <inheritdoc />
    public bool SendCommandNotFound(Player player, string input)
    {
        return false;
    }

    private string FormatCommandNotFound(string commandText)
    {
        return $"Unknown command: {commandText}";
    }

    private string FormatPermissionDenied(string commandText)
    {
        return "You do not have permission to use this command.";
    }
}
