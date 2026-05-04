using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands.Services;
using PlayerComponent = SampSharp.Entities.SAMP.Player;

namespace SampSharp.Entities.SAMP.Commands.Core;

/// <summary>
/// Core command dispatcher. Handles parsing command input and matching to registered commands.
/// Used by both PlayerCommandService and ConsoleCommandService.
/// </summary>
public class CommandDispatcher
{
    /// <summary>
    /// Dispatches a command from input text with full overload matching and permission checking.
    /// </summary>
    /// <param name="registry">The command registry containing all registered commands.</param>
    /// <param name="services">The service provider for DI and permission checking.</param>
    /// <param name="inputText">The input text to parse (without leading / for player commands).</param>
    /// <param name="prefixArgs">Prefix arguments (e.g., [Player] for player commands, [ConsoleCommandDispatchContext] for console commands).</param>
    /// <param name="permissionChecker">Optional permission checker (for player commands only).</param>
    /// <returns>The dispatch result.</returns>
    public DispatchResult Dispatch(
        CommandRegistry registry,
        IServiceProvider services,
        string inputText,
        object[] prefixArgs,
        IPermissionChecker? permissionChecker = null)
    {
        ArgumentNullException.ThrowIfNull(registry);
        ArgumentNullException.ThrowIfNull(services);

        if (string.IsNullOrWhiteSpace(inputText))
        {
            return DispatchResult.CreateNotFound();
        }

        inputText = inputText.Trim();

        // Split input into tokens and try to find the command by matching from longest to shortest path
        var tokens = inputText.Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries);
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

        // Check permission if a permission checker is provided
        if (permissionChecker != null && prefixArgs.Length > 0 && prefixArgs[0] is PlayerComponent player)
        {
            if (!permissionChecker.HasPermission(player, command))
            {
                return DispatchResult.CreatePermissionDenied();
            }
        }

        // Remaining tokens become the arguments
        var remainingTokens = tokens.Skip(consumedTokenCount).ToArray();
        var remainingArgs = remainingTokens.Length > 0
            ? string.Join(" ", remainingTokens)
            : "";

        // Try to match parameters for each overload
        var bestMatch = FindBestOverload(command, remainingArgs, services);

        if (bestMatch.matched)
        {
            // Successfully matched this overload
            var result = DispatchResult.CreateSuccess();
            result.CommandDefinition = command;
            result.CommandOverload = bestMatch.overload;
            result.ParsedArguments = bestMatch.parsedArguments;
            return result;
        }

        // No overload matched
        if (bestMatch.usageMessage != null)
        {
            var result = DispatchResult.CreateInvalidArguments(bestMatch.usageMessage);
            result.CommandDefinition = command;
            return result;
        }

        return DispatchResult.CreateNotFound();
    }

    /// <summary>
    /// Finds the best matching overload for the given arguments.
    /// Tries each overload and returns the one that consumes the least remaining input.
    /// </summary>
    private (bool matched, CommandOverload? overload, object?[]? parsedArguments, string? usageMessage) FindBestOverload(
        CommandDefinition command,
        string remainingArgs,
        IServiceProvider services)
    {
        var bestMatch = (matched: false, overload: (CommandOverload?)null, parsedArguments: (object?[]?)null, remainingUnconsumed: int.MaxValue, usageMessage: (string?)null);

        foreach (var overload in command.Overloads)
        {
            var matchResult = TryMatchParameters(overload, remainingArgs, services);
            if (matchResult.matched)
            {
                // Check if this is a better match (less remaining input)
                if (matchResult.remainingUnconsumed < bestMatch.remainingUnconsumed)
                {
                    bestMatch = (true, overload, matchResult.parsedArguments, matchResult.remainingUnconsumed, null);
                }
            }
            else if (matchResult.usageMessage != null && bestMatch.usageMessage == null)
            {
                bestMatch.usageMessage = matchResult.usageMessage;
            }
        }

        return (bestMatch.matched, bestMatch.overload, bestMatch.parsedArguments, bestMatch.usageMessage);
    }

    /// <summary>
    /// Tries to match the remaining arguments against the overload's parameters.
    /// Returns how many characters were unconsumed (for best-match selection).
    /// </summary>
    private (bool matched, string? usageMessage, object?[]? parsedArguments, int remainingUnconsumed) TryMatchParameters(
        CommandOverload overload,
        string remainingArgs,
        IServiceProvider services)
    {
        var parameters = overload.ParsedParameters;

        // If no parameters, check if no remaining args or all optional
        if (parameters.Length == 0)
        {
            if (string.IsNullOrWhiteSpace(remainingArgs))
            {
                return (true, null, [], 0);
            }

            // Has args but command takes none - invalid
            return (false, GenerateUsageMessage(overload), null, remainingArgs.Length);
        }

        // Count required vs optional parameters
        var requiredCount = parameters.Count(p => p.IsRequired);

        // Try to parse all parameters
        var remaining = remainingArgs;
        var parsedValues = new List<object?>();
        var initialRemaining = remaining;

        foreach (var param in parameters)
        {
            try
            {
                if (param.Parser.TryParse(services, ref remaining, out var value))
                {
                    parsedValues.Add(value);
                }
                else if (param.IsRequired)
                {
                    return (false, GenerateUsageMessage(overload), null, initialRemaining.Length);
                }
                else
                {
                    // Optional parameter - use default
                    parsedValues.Add(param.DefaultValue);
                    // Don't advance 'remaining' for failed optional parse
                }
            }
            catch (Exception)
            {
                // Parser threw exception - treat as parse failure
                if (param.IsRequired)
                {
                    return (false, GenerateUsageMessage(overload), null, initialRemaining.Length);
                }

                parsedValues.Add(param.DefaultValue);
            }
        }

        // Check if we have required minimum arguments before parsing
        var requiredValid = true;
        var testRemaining = remainingArgs;
        var parsedRequiredCount = 0;

        foreach (var param in parameters.Where(p => p.IsRequired))
        {
            if (param.Parser.TryParse(services, ref testRemaining, out _))
            {
                parsedRequiredCount++;
            }
            else
            {
                requiredValid = false;
                break;
            }
        }

        if (!requiredValid || parsedRequiredCount < requiredCount)
        {
            return (false, GenerateUsageMessage(overload), null, remainingArgs.Length);
        }

        // Successfully matched - calculate unconsumed length
        var unconsumedLength = Math.Max(0, remaining.Length);
        return (true, null, parsedValues.ToArray(), unconsumedLength);
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
