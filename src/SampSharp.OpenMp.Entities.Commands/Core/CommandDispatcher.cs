namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Core command dispatcher. Handles parsing command input and matching to registered commands.
/// Used by both PlayerCommandService and ConsoleCommandService.
/// </summary>
internal class CommandDispatcher
{
    /// <summary>
    /// Dispatches a command from input with full overload matching and permission checking.
    /// </summary>
    /// <param name="registry">The command registry containing all registered commands.</param>
    /// <param name="services">The service provider for DI and permission checking.</param>
    /// <param name="input">The input span to parse (without leading / for player commands).</param>
    /// <param name="prefixArgs">Prefix arguments (e.g., [EntityId] for player commands, [ConsoleCommandDispatchContext] for console commands).</param>
    /// <param name="permissionChecker">Optional permission checker (for player commands only).</param>
    /// <returns>The dispatch result.</returns>
    public DispatchResult Dispatch(CommandRegistry registry, IServiceProvider services, StringSpan input, object[] prefixArgs, IPermissionChecker? permissionChecker = null)
    {
        ArgumentNullException.ThrowIfNull(registry);
        ArgumentNullException.ThrowIfNull(services);

        input = input.TrimStart();

        if (input.Length == 0)
        {
            return DispatchResult.CreateNotFound();
        }

        // Save position before lookup so we can reconstruct the used command name
        var beforeLookup = input;

        // Advance 'input' past the consumed command path words
        var overloads = registry.GetCommandGroupByPath(ref input);
        if (overloads == null)
        {
            return DispatchResult.CreateNotFound();
        }

        // The used command name is the portion consumed during lookup (trim trailing whitespace)
        var usedCommandName = beforeLookup.Take(beforeLookup.Length - input.Length).ToString().TrimEnd();

        // Remaining args start after any leading whitespace that follows the command name
        var remainingArgs = input.TrimStart();

        // Try to match parameters for each overload
        var bestMatch = FindBestOverload(overloads, remainingArgs, services);

        // Check permission if a permission checker is provided
        if (bestMatch.overload is not null &&
            permissionChecker is not null &&
            prefixArgs is [EntityId entityId, ..])
        {
            var entityManager = services.GetService(typeof(IEntityManager)) as IEntityManager
                ?? throw new InvalidOperationException($"{nameof(IEntityManager)} is not registered in the service provider but is required for permission checking.");
            var playerComponent = entityManager.GetComponent<Player>(entityId);
            if (playerComponent != null && !permissionChecker.HasPermission(playerComponent, bestMatch.overload))
            {
                var permDenied = DispatchResult.CreatePermissionDenied();
                permDenied.CommandOverload = bestMatch.overload;
                permDenied.AllOverloads = overloads;
                permDenied.UsedCommandName = usedCommandName;
                return permDenied;
            }
        }

        if (bestMatch.matched)
        {
            // Successfully matched this overload
            var result = DispatchResult.CreateSuccess();
            result.CommandOverload = bestMatch.overload;
            result.AllOverloads = overloads;
            result.UsedCommandName = usedCommandName;
            result.ParsedArguments = bestMatch.parsedArguments;
            return result;
        }

        // No overload matched
        {
            var result = DispatchResult.CreateInvalidArguments();
            result.AllOverloads = overloads;
            result.UsedCommandName = usedCommandName;
            return result;
        }
    }

    /// <summary>
    /// Finds the best matching overload for the given arguments.
    /// </summary>
    private (bool matched, CommandDefinition? overload, object?[]? parsedArguments) FindBestOverload(IReadOnlyList<CommandDefinition> overloads, StringSpan remainingArgs,
        IServiceProvider services)
    {
        var bestMatch = (matched: false, overload: (CommandDefinition?)null, parsedArguments: (object?[]?)null);

        foreach (var overload in overloads)
        {
            var matchResult = TryMatchParameters(overload, remainingArgs, services);
            if (matchResult.matched)
            {
                bestMatch = (true, overload, matchResult.parsedArguments);
                break; // First matching overload wins; caller registers overloads in preferred order
            }
        }

        return bestMatch;
    }

    /// <summary>
    /// Tries to match the remaining arguments against the overload's parameters.
    /// </summary>
    private (bool matched, object?[]? parsedArguments) TryMatchParameters(CommandDefinition overload, StringSpan remainingArgs,
        IServiceProvider services)
    {
        var parameters = overload.ParsedParameters;

        // If no parameters, succeed only when there are no remaining args
        if (parameters.Length == 0)
        {
            return (remainingArgs.TrimStart().Length == 0, []);
        }

        // Try to parse all parameters
        var remaining = remainingArgs;
        var parsedValues = new List<object?>();

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
                    return (false, null);
                }
                else
                {
                    // Optional parameter - only use default when there is no remaining input
                    if (remaining.TrimStart().Length > 0)
                    {
                        // There is remaining input that could not be parsed - fail this overload
                        return (false, null);
                    }

                    parsedValues.Add(param.DefaultValue);
                }
            }
            catch (Exception)
            {
                // Parser threw exception - treat as parse failure
                if (param.IsRequired)
                {
                    return (false, null);
                }

                if (remaining.TrimStart().Length > 0)
                {
                    return (false, null);
                }

                parsedValues.Add(param.DefaultValue);
            }
        }

        // Reject overloads that have unconsumed trailing input
        if (remaining.TrimStart().Length > 0)
        {
            return (false, null);
        }

        return (true, parsedValues.ToArray());
    }
}