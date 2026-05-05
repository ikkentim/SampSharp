namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Provides formatted text for commands. Generates strings like "/cmd &lt;arg1&gt; &lt;arg2&gt;" for display.
/// Can be customized for localization.
/// </summary>
public interface ICommandTextFormatter
{
    /// <summary>
    /// Formats a command usage text with parameters.
    /// </summary>
    /// <param name="commandName">The command name (without group prefix).</param>
    /// <param name="group">The command group, if any (e.g., "admin money").</param>
    /// <param name="parameters">The parsed parameters of the command.</param>
    /// <param name="includeSlash">Whether to include the leading slash (true for player commands, false for console).</param>
    /// <returns>Formatted text like "/cmd &lt;arg1&gt; [arg2]".</returns>
    string FormatCommandUsage(string commandName, string? group, CommandParameterInfo[] parameters, bool includeSlash = true);

    /// <summary>
    /// Formats the "command not found" message.
    /// </summary>
    /// <param name="commandText">The command text that was entered.</param>
    /// <returns>A localized error message.</returns>
    string FormatCommandNotFound(string commandText);

    /// <summary>
    /// Formats the "permission denied" message.
    /// </summary>
    /// <param name="commandText">The command text that was entered.</param>
    /// <returns>A localized error message.</returns>
    string FormatPermissionDenied(string commandText);
}
