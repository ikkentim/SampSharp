using System.Reflection;
using SampSharp.Entities.Utilities;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Default <see cref="IPlayerCommandService"/> implementation. Scans every
/// loaded <see cref="ISystem"/> for methods marked
/// <see cref="PlayerCommandAttribute"/> and dispatches matching chat input
/// (slash-prefixed) to them. The first parameter of the handler is the
/// invoking <see cref="Player"/>.
/// </summary>
public class PlayerCommandService : CommandServiceBase, IPlayerCommandService
{
    private readonly IEntityManager _entityManager;

    /// <summary>Initializes a new instance.</summary>
    public PlayerCommandService(IEntityManager entityManager, ISystemRegistry systemRegistry)
        : base(entityManager, systemRegistry, prefixParameters: 1)
    {
        _entityManager = entityManager;
    }

    /// <inheritdoc />
    public bool Invoke(IServiceProvider services, EntityId player, string inputText)
    {
        var result = Invoke(services, [(object)player], inputText);

        if (result.Response != InvokeResponse.InvalidArguments)
            return result.Response == InvokeResponse.Success;

        // Auto-print usage hint to the invoking player on InvalidArguments.
        _entityManager.GetComponent<Player>(player)?.SendClientMessage(result.UsageMessage ?? "Invalid arguments.");
        return true;
    }

    /// <inheritdoc />
    protected override IEnumerable<(MethodInfo method, ICommandMethodInfo commandInfo)> ScanMethods()
    {
        // Same enumeration mode as EventDispatcher: instance + non-public + ISystem-types from registry.
        var scanner = ClassScanner.Create()
            .IncludeTypes(SystemRegistry.GetSystemTypes().Span)
            .IncludeNonPublicMembers();

        return scanner.ScanMethods<PlayerCommandAttribute>()
            .Select(t => (t.method, (ICommandMethodInfo)t.attribute));
    }

    /// <inheritdoc />
    protected override bool ValidateInputText(ref string inputText)
    {
        if (!base.ValidateInputText(ref inputText)) return false;
        if (!inputText.StartsWith('/') || inputText.Length <= 1) return false;
        inputText = inputText[1..];
        return true;
    }

    /// <inheritdoc />
    protected override string GetUsageMessage(CommandInfo[] commands)
        => commands.Length == 1
            ? CommandText(commands[0])
            : $"Usage: {string.Join(" -or- ", commands.Select(CommandText))}";

    private static string CommandText(CommandInfo command)
    {
        if (command.Parameters.Length == 0)
            return $"Usage: /{command.Name}";
        var args = string.Join(" ",
            command.Parameters.Select(a => a.IsRequired ? $"[{a.Name}]" : $"<{a.Name}>"));
        return $"Usage: /{command.Name} {args}";
    }
}
