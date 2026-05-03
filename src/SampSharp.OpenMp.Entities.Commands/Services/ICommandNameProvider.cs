using SampSharp.Entities.SAMP.Commands.Core;

namespace SampSharp.Entities.SAMP.Commands.Services;

/// <summary>
/// Provides usage messages for commands. Can be customized for localization.
/// </summary>
public interface ICommandNameProvider
{
    /// <summary>
    /// Gets the usage message for a command overload.
    /// </summary>
    /// <param name="commandName">The command name (without group prefix or leading slash).</param>
    /// <param name="group">The command group, if any (e.g., "admin money").</param>
    /// <param name="parameters">The parsed parameters of the command.</param>
    /// <param name="usageMessageKey">Optional localization key from the attribute (e.g., "message_send_usage").</param>
    /// <returns>A human-readable usage string.</returns>
    string GetUsageMessage(
        string commandName,
        string? group,
        CommandParameterInfo[] parameters,
        string? usageMessageKey = null);

    /// <summary>
    /// Gets the usage message for multiple command overloads.
    /// </summary>
    string GetUsageMessage(string commandName, string? group, CommandParameterInfo[][] allOverloads, string? usageMessageKey = null)
    {
        if (allOverloads.Length == 1)
            return GetUsageMessage(commandName, group, allOverloads[0], usageMessageKey);

        var messages = allOverloads.Select(p => GetUsageMessage(commandName, group, p, usageMessageKey)).ToList();
        return $"Usage: {string.Join(" -or- ", messages)}";
    }
}
