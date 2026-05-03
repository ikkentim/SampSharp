using System.Reflection;
using SampSharp.Entities.SAMP.Commands.Attributes;
using SampSharp.Entities.Utilities;

namespace SampSharp.Entities.SAMP.Commands.Core.Scanning;

/// <summary>
/// Scans ISystem types for command methods marked with [PlayerCommand] or [ConsoleCommand].
/// Builds CommandDefinition objects and registers them in a registry.
/// </summary>
public class CommandScanner
{
    private readonly IEntityManager _entityManager;
    private readonly ISystemRegistry _systemRegistry;

    public CommandScanner(IEntityManager entityManager, ISystemRegistry systemRegistry)
    {
        _entityManager = entityManager ?? throw new ArgumentNullException(nameof(entityManager));
        _systemRegistry = systemRegistry ?? throw new ArgumentNullException(nameof(systemRegistry));
    }

    /// <summary>
    /// Scans all registered ISystem types for player commands and populates the registry.
    /// </summary>
    public void ScanPlayerCommands(CommandRegistry registry, ICommandParameterParserFactory parserFactory)
    {
        var scanner = ClassScanner.Create()
            .IncludeTypes(_systemRegistry.GetSystemTypes().Span)
            .IncludeNonPublicMembers();

        var methods = scanner.ScanMethods<PlayerCommandAttribute>();

        foreach (var (systemType, method, attribute) in methods)
        {
            // Group attributes from class and method
            var classGroups = systemType.GetCustomAttributes<CommandGroupAttribute>();
            var methodGroups = method.GetCustomAttributes<CommandGroupAttribute>();
            var commandGroup = BuildCommandGroup(classGroups, methodGroups);

            // Aliases
            var aliases = method.GetCustomAttributes<AliasAttribute>()
                .SelectMany(a => a.Aliases)
                .Select(a => new CommandAlias(a))
                .ToArray();

            // Permission requirements
            var permAttr = method.GetCustomAttribute<RequiresPermissionAttribute>();
            var permissions = permAttr?.Permissions ?? [];

            // Each [PlayerCommand] attribute is a separate overload
            var commandName = attribute.Name ?? GetCommandName(method);
            if (string.IsNullOrWhiteSpace(commandName))
                continue;

            if (!TryBuildOverload(method, systemType, parserFactory, 1, out var overload))
                continue;

            var definition = new CommandDefinition(
                name: commandName,
                group: commandGroup,
                overloads: new[] { overload },
                aliases: aliases,
                playerCommand: true,
                consoleCommand: false);

            registry.Register(definition);
        }
    }

    /// <summary>
    /// Scans all registered ISystem types for console commands and populates the registry.
    /// </summary>
    public void ScanConsoleCommands(CommandRegistry registry, ICommandParameterParserFactory parserFactory)
    {
        var scanner = ClassScanner.Create()
            .IncludeTypes(_systemRegistry.GetSystemTypes().Span)
            .IncludeNonPublicMembers();

        var methods = scanner.ScanMethods<ConsoleCommandAttribute>();

        foreach (var (systemType, method, attribute) in methods)
        {
            // Group attributes from class and method
            var classGroups = systemType.GetCustomAttributes<CommandGroupAttribute>();
            var methodGroups = method.GetCustomAttributes<CommandGroupAttribute>();
            var commandGroup = BuildCommandGroup(classGroups, methodGroups);

            // Aliases
            var aliases = method.GetCustomAttributes<AliasAttribute>()
                .SelectMany(a => a.Aliases)
                .Select(a => new CommandAlias(a))
                .ToArray();

            // Console commands: check if first param is ConsoleCommandSender
            int prefixParams = 0;
            if (method.GetParameters().Length > 0)
            {
                var firstParam = method.GetParameters()[0];
                if (firstParam.ParameterType.Name == "ConsoleCommandSender")
                    prefixParams = 1;
            }

            // Build the overload
            var commandName = attribute.Name ?? GetCommandName(method);
            if (string.IsNullOrWhiteSpace(commandName))
                continue;

            if (!TryBuildOverload(method, systemType, parserFactory, prefixParams, out var overload))
                continue;

            var definition = new CommandDefinition(
                name: commandName,
                group: commandGroup,
                overloads: new[] { overload },
                aliases: aliases,
                playerCommand: false,
                consoleCommand: true);

            registry.Register(definition);
        }
    }

    /// <summary>Builds the command group by stacking class and method groups.</summary>
    private CommandGroup? BuildCommandGroup(
        IEnumerable<CommandGroupAttribute> classGroups,
        IEnumerable<CommandGroupAttribute> methodGroups)
    {
        var allParts = classGroups
            .SelectMany(g => g.Parts)
            .Concat(methodGroups.SelectMany(g => g.Parts))
            .ToList();

        return allParts.Count > 0 ? new CommandGroup(allParts) : null;
    }

    /// <summary>Tries to build a CommandOverload from a method.</summary>
    private bool TryBuildOverload(
        MethodInfo method,
        Type systemType,
        ICommandParameterParserFactory parserFactory,
        int prefixParameters,
        out CommandOverload? overload)
    {
        overload = null;

        var parameters = method.GetParameters();
        if (parameters.Length < prefixParameters)
            return false;

        // Validate return type: bool, int, void, Task, Task<T>
        if (!IsValidReturnType(method.ReturnType))
            return false;

        // Collect parsed parameters (skip prefix, handle DI)
        if (!TryCollectParameters(parameters, prefixParameters, parserFactory, out var parsedParams))
            return false;

        overload = new CommandOverload(
            method: method,
            parameters: parameters,
            declaringSystemType: systemType,
            parsedParameters: parsedParams!);

        return true;
    }

    /// <summary>Validates that a return type is supported by the command system.</summary>
    private bool IsValidReturnType(Type returnType)
    {
        // void, bool, int
        if (returnType == typeof(void) || returnType == typeof(bool) || returnType == typeof(int))
            return true;

        // Task, ValueTask
        if (returnType == typeof(Task) || returnType == typeof(ValueTask))
            return true;

        // Task<T>, ValueTask<T>
        if (returnType.IsGenericType)
        {
            var genericDef = returnType.GetGenericTypeDefinition();
            if (genericDef == typeof(Task<>) || genericDef == typeof(ValueTask<>))
                return true;
        }

        return false;
    }

    /// <summary>Collects parameter information for a method (excluding prefix, handling DI).</summary>
    private bool TryCollectParameters(
        ParameterInfo[] parameters,
        int prefixParameters,
        ICommandParameterParserFactory parserFactory,
        out CommandParameterInfo[]? result)
    {
        result = null;

        if (parameters.Length < prefixParameters)
            return false;

        var list = new List<CommandParameterInfo>();
        int parameterIndex = prefixParameters;
        bool optionalSeen = false;

        for (int i = prefixParameters; i < parameters.Length; i++)
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
            bool isRequired = !param.HasDefaultValue;
            if (!isRequired && optionalSeen == false)
            {
                optionalSeen = true;
            }
            else if (isRequired && optionalSeen)
            {
                // Required parameter after optional - invalid
                return false;
            }

            var cmdParamInfo = new CommandParameterInfo(
                name: paramName,
                parser: parser,
                isRequired: isRequired,
                defaultValue: param.DefaultValue,
                parameterIndex: parameterIndex++);

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
            name = name[..^7];
        return name;
    }
}
