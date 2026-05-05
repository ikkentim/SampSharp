using PlayerComponent = SampSharp.Entities.SAMP.Player;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Default implementation of IPlayerCommandMessageService.
/// Sends formatted messages to players via SendClientMessage.
/// </summary>
public class DefaultPlayerCommandMessageService : IPlayerCommandMessageService
{
    private readonly ICommandTextFormatter _formatter;

    public DefaultPlayerCommandMessageService(ICommandTextFormatter? formatter = null)
    {
        _formatter = formatter ?? new DefaultCommandTextFormatter();
    }

    public bool SendUsage(PlayerComponent player, CommandDefinition command)
    {
        var messages = new List<string>();

        if (command.Overloads.Count == 1)
        {
            var overload = command.Overloads.First();
            var text = _formatter.FormatCommandUsage(command.Name, command.Group?.ToString(), overload.ParsedParameters, includeSlash: true);
            messages.Add($"Usage: {text}");
        }
        else
        {
            messages.Add("Usage:");
            foreach (var overload in command.Overloads)
            {
                var text = _formatter.FormatCommandUsage(command.Name, command.Group?.ToString(), overload.ParsedParameters, includeSlash: true);
                messages.Add($"  {text}");
            }
        }

        foreach (var message in messages)
        {
            player.SendClientMessage(message);
        }

        return true;
    }

    public bool SendPermissionDenied(PlayerComponent player, CommandDefinition command)
    {
        var message = _formatter.FormatPermissionDenied("");
        player.SendClientMessage(message);
        return true; // Treat as command not found (continue with SendCommandNotFound)
    }

    public bool SendCommandNotFound(PlayerComponent player, string input)
    {
        var message = _formatter.FormatCommandNotFound(input);
        player.SendClientMessage(message);
        return false; // Return false to stop processing
    }
}
