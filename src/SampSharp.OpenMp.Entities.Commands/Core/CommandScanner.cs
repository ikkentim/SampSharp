using System.Reflection;
using SampSharp.Entities.Utilities;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Scans ISystem types for command methods marked with [PlayerCommand] or [ConsoleCommand].
/// Builds CommandDefinition objects and registers them in a registry.
/// </summary>
internal class CommandScanner
{
    private readonly ISystemRegistry _systemRegistry;

    public CommandScanner(ISystemRegistry systemRegistry)
    {
        _systemRegistry = systemRegistry;
    }

    public void ScanPlayerCommands(CommandRegistry registry, ICommandParameterParserFactory parserFactory)
    {
        var scanner = ClassScanner.Create().IncludeTypes(_systemRegistry.GetSystemTypes().Span).IncludeNonPublicMembers();

        var methods = scanner.ScanMethods<PlayerCommandAttribute>();

        foreach (var (systemType, method, attribute) in methods)
        {
            // Group attributes from class and method
            var classGroups = systemType.GetCustomAttributes<CommandGroupAttribute>();
            var methodGroups = method.GetCustomAttributes<CommandGroupAttribute>();
            var commandGroup = BuildCommandGroup(classGroups, methodGroups);

            // Aliases and tags are collected per overload
            var aliases = method.GetCustomAttributes<AliasAttribute>().SelectMany(a => a.Aliases).Select(a => new CommandAlias(a)).ToArray();
            var tags = method.GetCustomAttributes<CommandTagAttribute>().Select(t => new CommandTag(t.Key, t.Value)).ToArray();

            // Each [PlayerCommand] attribute is a separate overload
            var commandName = attribute.Name ?? GetCommandName(method);
            if (string.IsNullOrWhiteSpace(commandName))
            {
                continue;
            }

            if (method.Name == "PingCommand")
            {
                Console.WriteLine();
            }

            if (!TryBuildOverload(method, systemType, parserFactory, 1, aliases, tags, out var overload))
            {
                continue;
            }

            var definition = new CommandDefinition(commandName, commandGroup, [
                overload
            ]);

            registry.Register(definition);
        }
    }

    public void ScanConsoleCommands(CommandRegistry registry, ICommandParameterParserFactory parserFactory)
    {
        var scanner = ClassScanner.Create().IncludeTypes(_systemRegistry.GetSystemTypes().Span).IncludeNonPublicMembers();

        var methods = scanner.ScanMethods<ConsoleCommandAttribute>();

        foreach (var (systemType, method, attribute) in methods)
        {
            // Group attributes from class and method
            var classGroups = systemType.GetCustomAttributes<CommandGroupAttribute>();
            var methodGroups = method.GetCustomAttributes<CommandGroupAttribute>();
            var commandGroup = BuildCommandGroup(classGroups, methodGroups);

            // Aliases and tags are collected per overload
            var aliases = method.GetCustomAttributes<AliasAttribute>().SelectMany(a => a.Aliases).Select(a => new CommandAlias(a)).ToArray();
            var tags = method.GetCustomAttributes<CommandTagAttribute>().Select(t => new CommandTag(t.Key, t.Value)).ToArray();

            // Console commands: check if first param is ConsoleCommandDispatchContext
            var prefixParams = 0;
            if (method.GetParameters().Length > 0)
            {
                var firstParam = method.GetParameters()[0];
                if (firstParam.ParameterType == typeof(ConsoleCommandDispatchContext))
                {
                    prefixParams = 1;
                }
            }

            // Build the overload
            var commandName = attribute.Name ?? GetCommandName(method);
            if (string.IsNullOrWhiteSpace(commandName))
            {
                continue;
            }

            if (!TryBuildOverload(method, systemType, parserFactory, prefixParams, aliases, tags, out var overload))
            {
                continue;
            }

            var definition = new CommandDefinition(commandName, commandGroup, [
                overload
            ]);

            registry.Register(definition);
        }
    }

    private CommandGroup? BuildCommandGroup(IEnumerable<CommandGroupAttribute> classGroups, IEnumerable<CommandGroupAttribute> methodGroups)
    {
        var allParts = classGroups.SelectMany(g => g.Parts).Concat(methodGroups.SelectMany(g => g.Parts)).ToList();

        return allParts.Count > 0 ? new CommandGroup(allParts) : null;
    }

    private bool TryBuildOverload(MethodInfo method, Type systemType, ICommandParameterParserFactory parserFactory, int prefixParameters, CommandAlias[] aliases, CommandTag[] tags, out CommandOverload? overload)
    {
        overload = null;

        var parameters = method.GetParameters();
        if (parameters.Length < prefixParameters)
        {
            return false;
        }

        // Validate return type: bool, int, void, Task, Task<T>
        if (!IsValidReturnType(method.ReturnType))
        {
            return false;
        }

        // Collect parsed parameters (skip prefix, handle DI)
        if (!TryCollectParameters(parameters, prefixParameters, parserFactory, out var parsedParams))
        {
            return false;
        }

        // Compile the method invoker at discovery time
        var invoker = CompileMethodInvoker(method, parameters, prefixParameters, parsedParams!);

        overload = new CommandOverload(method, parameters, systemType, parsedParams!, invoker, prefixParameters, aliases, tags);

        return true;
    }

    private MethodInvoker CompileMethodInvoker(MethodInfo method, ParameterInfo[] parameters, int prefixParameterCount, CommandParameterInfo[] parsedParameters)
    {
        // Build MethodParameterSource array
        var sources = new MethodParameterSource[parameters.Length];
        var parsedParamsByIndex = parsedParameters.ToDictionary(p => p.ParameterIndex);

        var j = 0; // Counter for args array index

        for (var i = 0; i < parameters.Length; i++)
        {
            var paramInfo = parameters[i];
            var source = new MethodParameterSource(paramInfo);

            // Check if this is a prefix parameter (Player component or ConsoleCommandDispatchContext)
            if (i < prefixParameterCount)
            {
                source.ParameterIndex = j++;
            }
            // Check if this is a parsed parameter
            else if (parsedParamsByIndex.ContainsKey(i))
            {
                source.ParameterIndex = j++;
            }
            else
            {
                // This is a DI service parameter
                source.IsService = true;
            }

            // Mark as component if applicable
            if (paramInfo.ParameterType.IsAssignableTo(typeof(Component)))
            {
                source.IsComponent = true;
            }

            sources[i] = source;
        }

        // Compile using expression trees
        return MethodInvokerFactory.Compile(method, sources);
    }

    private bool IsValidReturnType(Type returnType)
    {
        // void, bool, int
        if (returnType == typeof(void) || returnType == typeof(bool))
        {
            return true;
        }

        // Task, ValueTask
        if (returnType == typeof(Task) || returnType == typeof(ValueTask))
        {
            return true;
        }

        // Task<T>, ValueTask<T>
        if (returnType.IsGenericType)
        {
            var genericDef = returnType.GetGenericTypeDefinition();
            if (genericDef == typeof(Task<>) || genericDef == typeof(ValueTask<>))
            {
                return true;
            }
        }

        return false;
    }

    private bool TryCollectParameters(ParameterInfo[] parameters, int prefixParameters, ICommandParameterParserFactory parserFactory, out CommandParameterInfo[]? result)
    {
        result = null;

        if (parameters.Length < prefixParameters)
        {
            return false;
        }

        var list = new List<CommandParameterInfo>();
        var parameterIndex = prefixParameters;
        var optionalSeen = false;

        for (var i = prefixParameters; i < parameters.Length; i++)
        {
            var param = parameters[i];

            var paramName = param.Name ?? $"param{i}";

            // Try to get a parser for this parameter
            var parser = parserFactory.CreateParser(parameters, i);

            if (parser == null)
            {
                // No parser = this is a DI parameter, not parsed from input
                parameterIndex++;
                continue;
            }

            // This parameter will be parsed from input
            var isRequired = !param.HasDefaultValue;
            if (!isRequired && !optionalSeen)
            {
                optionalSeen = true;
            }
            else if (isRequired && optionalSeen)
            {
                // Required parameter after optional - invalid
                return false;
            }

            var cmdParamInfo = new CommandParameterInfo(paramName, parser, isRequired, param.DefaultValue, parameterIndex++);

            list.Add(cmdParamInfo);
        }

        result = list.ToArray();
        return true;
    }

    /// <summary>Derives command name from method name (lowercase, strip "Command" suffix).</summary>
    private string GetCommandName(MethodInfo method)
    {
        var name = method.Name.ToLowerInvariant();
        if (name.EndsWith("command", StringComparison.Ordinal))
        {
            name = name[..^7];
        }

        return name;
    }
}