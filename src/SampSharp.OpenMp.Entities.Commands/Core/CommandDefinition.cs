using System.Reflection;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Represents a single overload of a command (one specific method implementation).
/// Multiple overloads can exist for the same command with different parameter types.
/// </summary>
public class CommandDefinition
{
    private readonly CommandAlias[] _aliases;
    private readonly Dictionary<string, string> _tags;

    /// <summary>Initializes a new instance.</summary>
    public CommandDefinition(string name, CommandGroup? group, MethodInfo method, ParameterInfo[] parameters, Type declaringSystemType, CommandParameterInfo[] parsedParameters, CommandInvoker invoker,
        int prefixParameterCount, CommandAlias[] aliases, CommandTag[] tags)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Command name cannot be empty.", nameof(name));
        }

        ArgumentNullException.ThrowIfNull(method);
        ArgumentNullException.ThrowIfNull(parameters);
        ArgumentNullException.ThrowIfNull(declaringSystemType);
        ArgumentNullException.ThrowIfNull(parsedParameters);

        Name = name;
        Group = group;
        Method = method;
        MethodParameters = parameters;
        DeclaringSystemType = declaringSystemType;
        ParsedParameters = parsedParameters;
        CompiledInvoker = invoker;
        PrefixParameterCount = prefixParameterCount;
        _aliases = aliases;
        _tags = new Dictionary<string, string>();
        foreach (var tag in tags)
        {
            _tags[tag.Key] = tag.Value;
        }
    }

    /// <summary>The command name (without leading slash or group prefix).</summary>
    public string Name { get; }

    /// <summary>The command group, if any (e.g., ["admin", "money"]).</summary>
    public CommandGroup? Group { get; }

    /// <summary>The full command path (group + name), e.g., "admin money give".</summary>
    public string FullName => Group.HasValue ? $"{Group.Value.FullName} {Name}" : Name;

    /// <summary>The method that implements this command overload.</summary>
    public MethodInfo Method { get; }

    /// <summary>All parameters of the method (including prefix and DI).</summary>
    public ParameterInfo[] MethodParameters { get; }

    /// <summary>The type of the ISystem that declares this command.</summary>
    public Type DeclaringSystemType { get; }

    internal ISystem? System { get; set; }

    /// <summary>
    /// Parameters that are parsed from command input (excludes prefix and DI parameters).
    /// These are in the order they appear in the method signature.
    /// </summary>
    public CommandParameterInfo[] ParsedParameters { get; }

    /// <summary>The pre-compiled method invoker (compiled at discovery time).</summary>
    public CommandInvoker CompiledInvoker { get; }

    /// <summary>The number of prefix parameters (e.g., Player for player commands, ConsoleCommandSender for console commands).</summary>
    public int PrefixParameterCount { get; }

    /// <summary>Aliases for this overload.</summary>
    public IReadOnlyList<CommandAlias> Aliases => _aliases;

    /// <summary>Custom metadata tags attached to this overload.</summary>
    public IReadOnlyDictionary<string, string> Tags => _tags;
}