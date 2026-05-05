namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Executes a command by invoking the associated method with parsed parameters.
/// Uses pre-compiled MethodInvoker from CommandOverload for high performance.
/// </summary>
public class CommandExecutor
{
    private readonly IEntityManager _entityManager;

    public CommandExecutor(IEntityManager entityManager)
    {
        _entityManager = entityManager ?? throw new ArgumentNullException(nameof(entityManager));
    }

    /// <summary>
    /// Executes a command overload with the given parameters.
    /// </summary>
    /// <param name="overload">The command overload to execute (with pre-compiled invoker).</param>
    /// <param name="prefixArgs">Prefix arguments (e.g., Player for player commands).</param>
    /// <param name="parsedArgs">Parsed arguments from command input.</param>
    /// <param name="services">Service provider for DI resolution.</param>
    /// <param name="system">The ISystem instance to invoke on.</param>
    /// <returns>The result of the method invocation.</returns>
    public object? Execute(CommandOverload overload, object?[] prefixArgs, object?[] parsedArgs, IServiceProvider services, ISystem system)
    {
        if (overload == null)
        {
            throw new ArgumentNullException(nameof(overload));
        }

        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (system == null)
        {
            throw new ArgumentNullException(nameof(system));
        }

        if (overload.CompiledInvoker == null)
        {
            throw new InvalidOperationException("Command overload has no compiled invoker. Invoker should be compiled at discovery time.");
        }

        var parameters = overload.MethodParameters;

        // Build the combined argument array (prefix + parsed)
        var args = new object?[parameters.Length];

        // Copy prefix arguments (player/console sender) - only copy as many as the command expects
        var prefixCount = Math.Min(overload.PrefixParameterCount, prefixArgs.Length);
        for (var i = 0; i < prefixCount; i++)
        {
            args[i] = prefixArgs[i];
        }

        // Copy parsed arguments after prefix
        var parsedIdx = 0;
        var argStartIndex = overload.PrefixParameterCount;
        while (parsedIdx < parsedArgs.Length && argStartIndex < parameters.Length)
        {
            args[argStartIndex++] = parsedArgs[parsedIdx++];
        }

        // Invoke using the pre-compiled MethodInvoker
        var result = overload.CompiledInvoker(system, args, services, _entityManager);

        return result;
    }
}