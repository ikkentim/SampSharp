using System.Linq;
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
        ArgumentNullException.ThrowIfNull(registry);

        if (string.IsNullOrWhiteSpace(inputText))
        {
            return DispatchResult.CreateNotFound();
        }

        inputText = inputText.Trim();

        // Split input into tokens and try to find the command by matching from longest to shortest path
        var tokens = inputText.Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries); // TODO - optimize
        if (tokens.Length == 0)
        {
            return DispatchResult.CreateNotFound();
        }

        // Try to find the command in the registry
        // TryFindByPath returns how many tokens were consumed
        var command = registry.TryFindByPath(tokens, out var consumedTokenCount);
        if (command == null)
        {
            return DispatchResult.CreateNotFound();
        }

        // Remaining tokens become the arguments
        var remainingTokens = tokens.Skip(consumedTokenCount).ToArray();
        var remainingArgs = remainingTokens.Length > 0
            ? string.Join(" ", remainingTokens)
            : "";

        // Try to match parameters for each overload
        string? bestUsageMessage = null;

        foreach (var overload in command.Overloads)
        {
            var matchResult = TryMatchParameters(overload, remainingArgs);
            if (matchResult.matched)
            {
                // Successfully matched this overload
                var result = DispatchResult.CreateSuccess();
                result.CommandDefinition = command;
                result.CommandOverload = overload;
                result.ParsedArguments = matchResult.parsedArguments;
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
    /// Tries to match the remaining arguments against the overload's parameters.
    /// </summary>
    private (bool matched, string? usageMessage, object?[]? parsedArguments) TryMatchParameters(
        CommandOverload overload,
        string remainingArgs)
    {
        var parameters = overload.ParsedParameters;

        // If no parameters, check if no remaining args or all optional
        if (parameters.Length == 0)
        {
            if (string.IsNullOrWhiteSpace(remainingArgs))
            {
                return (true, null, []);
            }

            // Has args but command takes none - invalid
            return (false, GenerateUsageMessage(overload), null);
        }

        // Count required vs optional parameters
        var requiredCount = parameters.Count(p => p.IsRequired);
        var optionalCount = parameters.Length - requiredCount;

        // Parse tokens from remaining args
        var tokens = remainingArgs.Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries).ToList(); // todo - optimize

        // Check if we have enough tokens (minimum required)
        if (tokens.Count < requiredCount)
        {
            return (false, GenerateUsageMessage(overload), null);
        }

        // Check if we have too many tokens (maximum required+optional)
        // Note: For the last parameter, if it's a StringParser, it consumes all remaining
        var isLastParamString = parameters.Length > 0 &&
            parameters[^1].Parser.GetType().Name == "StringParser";

        if (!isLastParamString && tokens.Count > parameters.Length)
        {
            return (false, GenerateUsageMessage(overload), null);
        }

        // Try to parse all parameters
        var remaining = remainingArgs;
        var parsedValues = new List<object?>();

        foreach (var param in parameters)
        {
            if (param.Parser.TryParse(new ServiceCollection().BuildServiceProvider(), ref remaining, out var value))
            {
                parsedValues.Add(value);
            }
            else if (param.IsRequired)
            {
                return (false, GenerateUsageMessage(overload), null);
            }
            else
            {
                // Optional parameter - use default
                parsedValues.Add(param.DefaultValue);
            }
        }

        // Successfully matched
        return (true, null, parsedValues.ToArray());
    }

    /// <summary>Generates a usage message for a command overload.</summary>
    private static string GenerateUsageMessage(CommandOverload overload)
    {
        var command = overload.Method.Name.ToLowerInvariant();
        if (command.EndsWith("command"))
        {
            command = command[..^7];
        }

        if (overload.ParsedParameters.Length == 0)
        {
            return $"Usage: /{command}";
        }

        var args = string.Join(" ", overload.ParsedParameters.Select(p =>
            p.IsRequired ? $"<{p.Name}>" : $"[{p.Name}]"));

        return $"Usage: /{command} {args}";
    }
}
