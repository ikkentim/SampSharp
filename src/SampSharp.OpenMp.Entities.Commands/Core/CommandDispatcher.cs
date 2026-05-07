namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Core command dispatcher. Handles parsing command input and matching to registered commands.
/// Used by both PlayerCommandService and ConsoleCommandService.
/// </summary>
internal class CommandDispatcher
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
    public DispatchResult Dispatch(CommandRegistry registry, IServiceProvider services, string inputText, object[] prefixArgs, IPermissionChecker? permissionChecker = null)
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

        // Try to find the command group in the registry (all overloads with same name/group)
        var commandGroup = registry.GetCommandGroupByPath(tokens, out var consumedTokenCount);
        if (commandGroup == null)
        {
            return DispatchResult.CreateNotFound();
        }

        // Build the used command name from the consumed tokens
        var usedCommandName = string.Join(" ", tokens.Take(consumedTokenCount));

        // Remaining tokens become the arguments
        var remainingTokens = tokens.Skip(consumedTokenCount).ToArray();
        var remainingArgs = remainingTokens.Length > 0 ? string.Join(" ", remainingTokens) : "";

        // Try to match parameters for each overload
        var bestMatch = FindBestOverload(commandGroup, remainingArgs, services);

        // Check permission if a permission checker is provided
        if (bestMatch.overload is not null &&
            permissionChecker is not null &&
            prefixArgs is [Player player, ..] &&
            !permissionChecker.HasPermission(player, bestMatch.overload))
        {
            return DispatchResult.CreatePermissionDenied();
        }

        if (bestMatch.matched)
        {
            // Successfully matched this overload
            var result = DispatchResult.CreateSuccess();
            result.CommandOverload = bestMatch.overload;
            result.AllOverloads = commandGroup.Overloads;
            result.UsedCommandName = usedCommandName;
            result.ParsedArguments = bestMatch.parsedArguments;
            return result;
        }

        // No overload matched
        {
            var result = DispatchResult.CreateInvalidArguments();
            result.AllOverloads = commandGroup.Overloads;
            result.UsedCommandName = usedCommandName;
            return result;
        }
    }

    /// <summary>
    /// Finds the best matching overload for the given arguments.
    /// Tries each overload and returns the one that consumes the least remaining input.
    /// </summary>
    private (bool matched, CommandDefinition? overload, object?[]? parsedArguments) FindBestOverload(CommandSet command, string remainingArgs,
        IServiceProvider services)
    {
        var bestMatch = (matched: false, overload: (CommandDefinition?)null, parsedArguments: (object?[]?)null, remainingUnconsumed: int.MaxValue);

        foreach (var overload in command.Overloads)
        {
            var matchResult = TryMatchParameters(overload, remainingArgs, services);
            if (matchResult.matched)
            {
                // Check if this is a better match (less remaining input)
                if (matchResult.remainingUnconsumed < bestMatch.remainingUnconsumed)
                {
                    bestMatch = (true, overload, matchResult.parsedArguments, matchResult.remainingUnconsumed);
                }
            }
        }

        return (bestMatch.matched, bestMatch.overload, bestMatch.parsedArguments);
    }

    /// <summary>
    /// Tries to match the remaining arguments against the overload's parameters.
    /// Returns how many characters were unconsumed (for best-match selection).
    /// </summary>
    private (bool matched, object?[]? parsedArguments, int remainingUnconsumed) TryMatchParameters(CommandDefinition overload, string remainingArgs,
        IServiceProvider services)
    {
        var parameters = overload.ParsedParameters;

        // If no parameters, check if no remaining args or all optional
        if (parameters.Length == 0)
        {
            if (string.IsNullOrWhiteSpace(remainingArgs))
            {
                return (true, [], 0);
            }

            // Has args but command takes none - invalid
            return (false, null, remainingArgs.Length);
        }

        // Count required vs optional parameters
        var requiredCount = parameters.Count(p => p.IsRequired);

        // Try to parse all parameters
        var remaining = StringSpan.For(remainingArgs);
        var parsedValues = new List<object?>();
        var initialRemaining = remainingArgs.Length;

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
                    return (false, null, initialRemaining);
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
                    return (false, null, initialRemaining);
                }

                parsedValues.Add(param.DefaultValue);
            }
        }

        // Check if we have required minimum arguments before parsing
        var requiredValid = true;
        var testRemaining = StringSpan.For(remainingArgs);
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
            return (false, null, remainingArgs.Length);
        }

        // Successfully matched - calculate unconsumed length
        var unconsumedLength = Math.Max(0, remaining.Length);
        return (true, parsedValues.ToArray(), unconsumedLength);
    }
}