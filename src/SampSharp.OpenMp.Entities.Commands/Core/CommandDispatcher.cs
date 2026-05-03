using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SampSharp.Entities.SAMP.Commands.Core;

/// <summary>
/// Core command dispatcher. Handles parsing command input and matching to registered commands.
/// Used by both PlayerCommandService and ConsoleCommandService.
/// </summary>
public class CommandDispatcher
{
    /// <summary>
    /// Dispatches a command from input text.
    /// </summary>
    /// <param name="registry">The command registry containing all registered commands.</param>
    /// <param name="inputText">The input text to parse (without leading / for player commands).</param>
    /// <param name="prefix">Prefix arguments (e.g., [Player] for player commands, [ConsoleCommandSender] for console commands).</param>
    /// <returns>The dispatch result.</returns>
    public DispatchResult Dispatch(CommandRegistry registry, string inputText, object[] prefix)
    {
        if (registry == null)
            throw new ArgumentNullException(nameof(registry));

        if (string.IsNullOrWhiteSpace(inputText))
            return DispatchResult.CreateNotFound();

        inputText = inputText.Trim();

        // Parse the command line to extract command path and remaining arguments
        var (commandPath, remainingArgs) = ParseCommandLine(inputText);
        if (string.IsNullOrWhiteSpace(commandPath))
            return DispatchResult.CreateNotFound();

        // Look up the command in the registry
        var pathParts = commandPath.Split(' ');
        var command = registry.TryFindByPath(pathParts);
        if (command == null)
            return DispatchResult.CreateNotFound();

        // Try to match parameters for each overload
        string? bestUsageMessage = null;

        foreach (var overload in command.Overloads)
        {
            var matchResult = TryMatchParameters(overload, remainingArgs.Trim());
            if (matchResult.matched)
            {
                // Successfully matched this overload
                var result = DispatchResult.CreateSuccess();
                result.CommandDefinition = command;
                result.CommandOverload = overload;
                // Store the parsed arguments for later use
                result.Message = null; // Can be used to store parsed args later if needed
                return result;
            }

            if (matchResult.usageMessage != null)
            {
                bestUsageMessage = matchResult.usageMessage;
            }
        }

        // No overload matched
        if (bestUsageMessage != null)
        {
            return DispatchResult.CreateInvalidArguments(bestUsageMessage);
        }

        return DispatchResult.CreateNotFound();
    }

    /// <summary>
    /// Parses the command line into command path and remaining arguments.
    /// Handles multi-word command groups and aliases.
    /// </summary>
    private (string commandPath, string remainingArgs) ParseCommandLine(string inputText)
    {
        // Split by whitespace to get individual tokens
        var parts = inputText.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
            return ("", "");

        // Try to match the longest possible command path
        // This allows multi-word commands like "admin money give"
        for (int i = parts.Length; i > 0; i--)
        {
            var potentialCommand = string.Join(" ", parts.Take(i));

            var remaining = i < parts.Length
                ? string.Join(" ", parts.Skip(i))
                : "";

            // Return the potential command; the registry lookup will verify if it exists
            return (potentialCommand, remaining);
        }

        return ("", "");
    }

    /// <summary>
    /// Tries to match the remaining arguments against the overload's parameters.
    /// </summary>
    private (bool matched, string? usageMessage) TryMatchParameters(
        CommandOverload overload,
        string remainingArgs)
    {
        var parameters = overload.ParsedParameters;

        // If no parameters, check if no remaining args or all optional
        if (parameters.Length == 0)
        {
            if (string.IsNullOrWhiteSpace(remainingArgs))
                return (true, null);

            // Has args but command takes none - invalid
            return (false, GenerateUsageMessage(overload));
        }

        // Count required vs optional parameters
        var requiredCount = parameters.Count(p => p.IsRequired);
        var optionalCount = parameters.Length - requiredCount;

        // Parse tokens from remaining args
        var tokens = remainingArgs.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();

        // Check if we have enough tokens (minimum required)
        if (tokens.Count < requiredCount)
            return (false, GenerateUsageMessage(overload));

        // Check if we have too many tokens (maximum required+optional)
        // Note: For the last parameter, if it's a StringParser, it consumes all remaining
        var isLastParamString = parameters.Length > 0 &&
            parameters[^1].Parser.GetType().Name == "StringParser";

        if (!isLastParamString && tokens.Count > parameters.Length)
            return (false, GenerateUsageMessage(overload));

        // Try to parse all parameters
        string remaining = remainingArgs;
        var parsedValues = new List<object?>();

        foreach (var param in parameters)
        {
            if (param.Parser.TryParse(new ServiceCollection().BuildServiceProvider(), ref remaining, out var value))
            {
                parsedValues.Add(value);
            }
            else if (param.IsRequired)
            {
                return (false, GenerateUsageMessage(overload));
            }
            else
            {
                // Optional parameter - use default
                parsedValues.Add(param.DefaultValue);
            }
        }

        // Successfully matched
        return (true, null);
    }

    /// <summary>Generates a usage message for a command overload.</summary>
    private string GenerateUsageMessage(CommandOverload overload)
    {
        var command = overload.Method.Name.ToLowerInvariant();
        if (command.EndsWith("command"))
            command = command[..^7];

        if (overload.ParsedParameters.Length == 0)
            return $"Usage: /{command}";

        var args = string.Join(" ", overload.ParsedParameters.Select(p =>
            p.IsRequired ? $"<{p.Name}>" : $"[{p.Name}]"));

        return $"Usage: /{command} {args}";
    }
}
