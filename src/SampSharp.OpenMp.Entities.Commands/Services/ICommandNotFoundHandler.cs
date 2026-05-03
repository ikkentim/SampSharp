namespace SampSharp.Entities.SAMP.Commands.Services;

/// <summary>
/// Handles "command not found" and "invalid arguments" responses.
/// Can be customized for localization and custom error messages.
/// </summary>
public interface ICommandNotFoundHandler
{
    /// <summary>
    /// Gets the "command not found" message.
    /// </summary>
    /// <param name="commandText">The command text that was entered (without slash for player commands).</param>
    /// <returns>A localized error message.</returns>
    string GetCommandNotFoundMessage(string commandText);

    /// <summary>
    /// Gets the "invalid arguments" message.
    /// </summary>
    /// <param name="commandText">The command text that was entered (without slash for player commands).</param>
    /// <param name="usageMessage">The usage message for the command.</param>
    /// <returns>A localized error message combining usage information.</returns>
    string GetInvalidArgumentsMessage(string commandText, string usageMessage)
        => usageMessage;

    /// <summary>
    /// Gets the "permission denied" message (player commands only).
    /// </summary>
    /// <param name="commandText">The command text that was entered (without slash for player commands).</param>
    /// <returns>A localized error message.</returns>
    string GetPermissionDeniedMessage(string commandText)
        => "You do not have permission to use this command.";
}
