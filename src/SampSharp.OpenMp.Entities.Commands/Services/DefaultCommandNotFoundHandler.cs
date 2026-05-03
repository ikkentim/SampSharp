namespace SampSharp.Entities.SAMP.Commands.Services;

/// <summary>
/// Default implementation of ICommandNotFoundHandler.
/// Provides basic error messages.
/// </summary>
public class DefaultCommandNotFoundHandler : ICommandNotFoundHandler
{
    public string GetCommandNotFoundMessage(string commandText)
    {
        return $"Unknown command: /{commandText}";
    }

    public string GetInvalidArgumentsMessage(string commandText, string usageMessage)
    {
        return usageMessage;
    }

    public string GetPermissionDeniedMessage(string commandText)
    {
        return "You do not have permission to use this command.";
    }
}
