using System.Reflection;
using SampSharp.Entities.SAMP.Commands.Parsers;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Base class for command dispatchers. Subclasses provide the source of methods
/// to scan (<see cref="ScanMethods" />) and the format of the usage message
/// (<see cref="GetUsageMessage" />); everything else — argument parsing,
/// invocation, dependency injection, entity-to-component coercion — is shared.
/// </summary>
/// <remarks>
/// The dispatcher reflects every <see cref="ISystem" /> in the registry once at
/// construction and compiles an expression-tree invoker per command method, so
/// dispatch at runtime costs only a dictionary lookup + parser run.
/// </remarks>
public abstract class CommandServiceBase
{
    private readonly Dictionary<string, List<CommandData>> _commands = new();
    private readonly IEntityManager _entityManager;
    private readonly int _prefixParameters;

    /// <summary>Initializes a new instance.</summary>
    /// <param name="entityManager">ECS entity manager.</param>
    /// <param name="systemRegistry">Source of <see cref="ISystem" /> types to scan for commands.</param>
    /// <param name="prefixParameters">
    /// Number of "prefix" parameters that are supplied by the caller (not parsed from chat input).
    /// For player commands this is 1 (the invoking player).
    /// </param>
    protected CommandServiceBase(IEntityManager entityManager, ISystemRegistry systemRegistry, int prefixParameters)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(prefixParameters);
        _entityManager = entityManager;
        SystemRegistry = systemRegistry;
        _prefixParameters = prefixParameters;

        SystemRegistry.Register(LoadCommands);
    }

    /// <summary>Source of registered <see cref="ISystem" /> types for the scanner.</summary>
    protected ISystemRegistry SystemRegistry { get; }

    /// <summary>Validates and (optionally) rewrites the input text. Default: rejects empty strings.</summary>
    protected virtual bool ValidateInputText(ref string inputText)
    {
        return !string.IsNullOrEmpty(inputText);
    }

    /// <summary>Invokes a command from <paramref name="inputText" /> with the given <paramref name="prefix" /> arguments.</summary>
    protected InvokeResult Invoke(IServiceProvider services, object[] prefix, string inputText)
    {
        if (_prefixParameters > 0)
        {
            ArgumentNullException.ThrowIfNull(prefix);
            if (prefix.Length != _prefixParameters)
            {
                throw new ArgumentException($"prefix must contain {_prefixParameters} values", nameof(prefix));
            }
        }

        if (!ValidateInputText(ref inputText))
        {
            return InvokeResult.CommandNotFound;
        }

        // First word == command name. (TODO: command groups would carry spaces; not supported yet.)
        var spaceIndex = inputText.IndexOf(' ');
        var name = spaceIndex < 0 ? inputText : inputText[..spaceIndex];
        inputText = inputText[name.Length..];

        if (!_commands.TryGetValue(name, out var commands))
        {
            return InvokeResult.CommandNotFound;
        }

        var invalidParameters = false;
        var success = false;

        foreach (var command in commands)
        {
            var cmdInput = inputText;
            var accept = true;
            var useDefault = false;

            foreach (var p in command.Info.Parameters)
            {
                if (useDefault)
                {
                    command.Arguments[p.Index] = p.DefaultValue;
                    continue;
                }

                if (p.Parser.TryParse(services, ref cmdInput, out var parsed))
                {
                    command.Arguments[p.Index] = parsed;
                    continue;
                }

                if (p.IsRequired)
                {
                    accept = false;
                    break;
                }

                useDefault = true;
                command.Arguments[p.Index] = p.DefaultValue;
            }

            if (accept)
            {
                if (_prefixParameters > 0)
                {
                    Array.Copy(prefix!, command.Arguments, _prefixParameters);
                }

                if (services.GetService(command.SystemType) is ISystem system)
                {
                    var result = command.Invoke(system, command.Arguments, services, _entityManager);
                    // void / null and any non-(false|0) return value count as "handled".
                    // Matches legacy SampSharp semantics: explicit `false` / `0` opts out;
                    // everything else (including void → null) wins. This is critical for
                    // the common `[PlayerCommand] public void Foo(Player p)` style.
                    success = result switch
                    {
                        bool b => b,
                        int i => i != 0,
                        MethodResult mr => mr.Value,
                        _ => true
                    };
                }
            }
            else
            {
                invalidParameters = true;
            }

            Array.Clear(command.Arguments, 0, command.Arguments.Length);

            if (success)
            {
                return InvokeResult.Success;
            }
        }

        if (!invalidParameters)
        {
            return InvokeResult.CommandNotFound;
        }

        var usage = GetUsageMessage([..commands.Select(c => c.Info)]);
        return new InvokeResult(InvokeResponse.InvalidArguments, usage);
    }

    /// <summary>
    /// Provides the methods to register as commands. Override to filter or augment.
    /// Each yielded tuple is <c>(method, info-from-attribute)</c>.
    /// </summary>
    protected abstract IEnumerable<(MethodInfo method, ICommandMethodInfo commandInfo)> ScanMethods();

    /// <summary>Returns a human-readable usage message for one or more command overloads.</summary>
    protected virtual string GetUsageMessage(CommandInfo[] commands)
    {
        return commands.Length == 1
            ? CommandText(commands[0])
            : $"Usage: {string.Join(" -or- ", commands.Select(CommandText))}";
    }

    /// <summary>Builds a parser for the given parameter. Override to support custom types.</summary>
    protected virtual ICommandParameterParser? CreateParameterParser(ParameterInfo[] parameters, int index)
    {
        var p = parameters[index];
        var t = p.ParameterType;

        if (t == typeof(int))
        {
            return new IntParser();
        }

        if (t == typeof(string))
        {
            return index == parameters.Length - 1 ? new StringParser() : new WordParser();
        }

        if (t == typeof(float))
        {
            return new FloatParser();
        }

        if (t == typeof(double))
        {
            return new DoubleParser();
        }

        if (t == typeof(Player) || t == typeof(EntityId))
        {
            return new PlayerParser(); // @TODO: support some other components
        }

        return t.IsEnum ? new EnumParser(t) : null;
    }

    /// <summary>
    /// Tries to collect parameter info. Default impl walks parameters skipping the prefix,
    /// asks <see cref="CreateParameterParser" /> for each; parameters without a parser are
    /// treated as DI services.
    /// </summary>
    protected virtual bool TryCollectParameters(ParameterInfo[] parameters, int prefixParameters,
        out CommandParameterInfo[]? result)
    {
        if (parameters.Length < prefixParameters)
        {
            result = null;
            return false;
        }

        var list = new List<CommandParameterInfo>();
        var parameterIndex = prefixParameters;
        var optionalSeen = false;

        for (var i = prefixParameters; i < parameters.Length; i++)
        {
            var parameter = parameters[i];
            var attribute = parameter.GetCustomAttribute<CommandParameterAttribute>();

            var name = !string.IsNullOrWhiteSpace(attribute?.Name) ? attribute!.Name! : parameter.Name!;
            ICommandParameterParser? parser = null;

            if (attribute?.Parser != null && typeof(ICommandParameterParser).IsAssignableFrom(attribute.Parser))
            {
                parser = Activator.CreateInstance(attribute.Parser) as ICommandParameterParser;
            }

            parser ??= CreateParameterParser(parameters, i);

            if (parser == null)
            {
                parameterIndex++;
                continue;
            }

            var optional = parameter.HasDefaultValue;
            if (!optional && optionalSeen)
            {
                // Required after optional is illegal
                result = null;
                return false;
            }

            optionalSeen |= optional;

            list.Add(new CommandParameterInfo(name, parser, !optional, parameter.DefaultValue, parameterIndex++));
        }

        result = list.ToArray();
        return true;
    }

    /// <summary>Extracts the command name from a method when the attribute didn't override it.</summary>
    protected virtual string GetCommandName(MethodInfo method)
    {
        var name = method.Name.ToLowerInvariant();
        if (name.EndsWith("command", StringComparison.Ordinal))
        {
            name = name[..^7];
        }

        return name;
    }

    private void LoadCommands()
    {
        foreach (var (method, info) in ScanMethods())
        {
            var name = info.Name ?? GetCommandName(method);
            if (string.IsNullOrEmpty(name))
            {
                continue;
            }

            // Only bool / int / void are accepted (matching the legacy convention).
            if (method.ReturnType != typeof(bool) && method.ReturnType != typeof(int) &&
                method.ReturnType != typeof(void))
            {
                continue;
            }

            var methodParameters = method.GetParameters();
            if (!TryCollectParameters(methodParameters, _prefixParameters, out var parameters))
            {
                continue;
            }

            var commandInfo = new CommandInfo(name, parameters!);

            // Build per-parameter sources for MethodInvokerFactory.
            var argsPtr = 0;
            var sources = methodParameters.Select(p => new MethodParameterSource(p)).ToArray();
            for (var i = 0; i < sources.Length; i++)
            {
                var src = sources[i];
                var t = src.Info.ParameterType;

                if (typeof(Component).IsAssignableFrom(t))
                {
                    src.ParameterIndex = argsPtr++;
                    src.IsComponent = true;
                }
                else if (parameters!.FirstOrDefault(p => p.Index == i) != null)
                {
                    src.ParameterIndex = argsPtr++;
                }
                else
                {
                    src.IsService = true;
                }
            }

            var data = new CommandData(
                commandInfo,
                MethodInvokerFactory.Compile(method, sources),
                method.DeclaringType!,
                new object?[parameters!.Length + _prefixParameters]);

            if (!_commands.TryGetValue(commandInfo.Name, out var list))
            {
                _commands[commandInfo.Name] = list = [];
            }

            list.Add(data);
        }
    }

    private static string CommandText(CommandInfo command)
    {
        if (command.Parameters.Length == 0)
        {
            return $"Usage: /{command.Name}";
        }

        var args = string.Join(" ",
            command.Parameters.Select(a => a.IsRequired ? $"[{a.Name}]" : $"<{a.Name}>"));
        return $"Usage: /{command.Name} {args}";
    }

    private sealed record CommandData(CommandInfo Info, MethodInvoker Invoke, Type SystemType, object?[] Arguments);
}