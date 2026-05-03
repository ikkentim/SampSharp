using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP.Commands.Async;

namespace SampSharp.Entities.SAMP.Commands.Core.Execution;

/// <summary>
/// Executes a command by invoking the associated method with parsed parameters.
/// Handles DI, component resolution, and async results.
/// </summary>
public class CommandExecutor
{
    private readonly IEntityManager _entityManager;
    private readonly Lazy<Dictionary<MethodInfo, MethodInvoker>> _invokerCache;

    public CommandExecutor(IEntityManager entityManager)
    {
        _entityManager = entityManager ?? throw new ArgumentNullException(nameof(entityManager));
        _invokerCache = new Lazy<Dictionary<MethodInfo, MethodInvoker>>(() => new());
    }

    /// <summary>
    /// Executes a command overload with the given parameters.
    /// </summary>
    /// <param name="overload">The command overload to execute.</param>
    /// <param name="prefixArgs">Prefix arguments (e.g., Player for player commands).</param>
    /// <param name="parsedArgs">Parsed arguments from command input.</param>
    /// <param name="services">Service provider for DI resolution.</param>
    /// <param name="system">The ISystem instance to invoke on.</param>
    /// <returns>The result of the method invocation.</returns>
    public object? Execute(
        CommandOverload overload,
        object?[] prefixArgs,
        object?[] parsedArgs,
        IServiceProvider services,
        ISystem system)
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

        var method = overload.Method;
        var parameters = overload.MethodParameters;

        // Build the argument array for the invoker
        var args = new object?[parameters.Length];

        // Copy prefix arguments
        for (int i = 0; i < prefixArgs.Length; i++)
        {
            args[i] = prefixArgs[i];
        }

        // Copy parsed arguments
        int parsedIdx = 0;
        foreach (var parsedParam in overload.ParsedParameters)
        {
            args[parsedParam.ParameterIndex] = parsedIdx < parsedArgs.Length ? parsedArgs[parsedIdx++] : null;
        }

        // Fill in DI parameters
        for (int i = 0; i < parameters.Length; i++)
        {
            if (args[i] != null)
            {
                continue; // Already filled (prefix or parsed)
            }

            var paramType = parameters[i].ParameterType;

            // Check if it's a component type attached to the first parameter (player)
            if (typeof(Component).IsAssignableFrom(paramType) && prefixArgs.Length > 0 &&
                prefixArgs[0] is EntityId playerId)
            {
                // Use reflection to call GetComponent<T>(EntityId)
                var getComponentMethod = typeof(IEntityManager).GetMethod("GetComponent");
                var genericMethod = getComponentMethod!.MakeGenericMethod(paramType);
                var component = genericMethod.Invoke(_entityManager, new object[] { playerId });

                if (component == null)
                {
                    // Component not found - command unavailable
                    return null;
                }
                args[i] = component;
            }
            else
            {
                // Try to resolve from DI container
                try
                {
                    args[i] = services.GetService(paramType);
                }
                catch
                {
                    // Cannot resolve - leave as null
                }
            }
        }

        // Get or compile the invoker
        var invoker = GetOrCompileInvoker(method, parameters);

        // Invoke the method
        var result = invoker(system, args, services, _entityManager);

        return result;
    }

    /// <summary>
    /// Gets or compiles a MethodInvoker for the given method.
    /// </summary>
    private MethodInvoker GetOrCompileInvoker(MethodInfo method, ParameterInfo[] parameters)
    {
        var cache = _invokerCache.Value;

        if (cache.TryGetValue(method, out var invoker))
        {
            return invoker;
        }

        // Compile new invoker using expression trees
        var sources = parameters.Select((p, i) =>
            new MethodParameterSource(p)
            {
                ParameterIndex = i,
                IsComponent = typeof(Component).IsAssignableFrom(p.ParameterType)
            }).ToArray();

        var compiled = MethodInvokerFactory.Compile(method, sources);
        cache[method] = compiled;

        return compiled;
    }
}
