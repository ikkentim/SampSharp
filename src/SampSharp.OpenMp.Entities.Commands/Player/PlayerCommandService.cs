using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands.Async;
using SampSharp.Entities.SAMP.Commands.Core;
using SampSharp.Entities.SAMP.Commands.Core.Execution;
using SampSharp.Entities.SAMP.Commands.Core.Scanning;
using SampSharp.Entities.SAMP.Commands.Services;
using SampSharp.Entities.Utilities;

namespace SampSharp.Entities.SAMP.Commands.Player;

/// <summary>
/// Dispatches player commands from in-game chat input.
/// Handles / prefix stripping, component resolution, and permission checking.
/// </summary>
public class PlayerCommandService : IPlayerCommandService
{
    private readonly CommandRegistry _registry = new();
    private readonly CommandDispatcher _dispatcher = new();
    private readonly CommandExecutor _executor;
    private readonly IEntityManager _entityManager;
    private readonly ICommandNameProvider _nameProvider;
    private readonly IPermissionChecker _permissionChecker;
    private readonly ICommandNotFoundHandler _notFoundHandler;
    private readonly ISystemRegistry _systemRegistry;

    public PlayerCommandService(
        IEntityManager entityManager,
        ISystemRegistry systemRegistry,
        ICommandNameProvider? nameProvider = null,
        IPermissionChecker? permissionChecker = null,
        ICommandNotFoundHandler? notFoundHandler = null)
    {
        _entityManager = entityManager ?? throw new ArgumentNullException(nameof(entityManager));
        _systemRegistry = systemRegistry ?? throw new ArgumentNullException(nameof(systemRegistry));

        _executor = new CommandExecutor(entityManager);
        _nameProvider = nameProvider ?? new DefaultCommandNameProvider();
        _permissionChecker = permissionChecker ?? new DefaultPermissionChecker();
        _notFoundHandler = notFoundHandler ?? new DefaultCommandNotFoundHandler();

        // Scan for player commands
        var scanner = new CommandScanner(entityManager, systemRegistry);
        var parserFactory = new DefaultCommandParameterParserFactory();
        scanner.ScanPlayerCommands(_registry, parserFactory);
    }

    public bool Invoke(IServiceProvider services, EntityId player, string inputText)
    {
        if (string.IsNullOrWhiteSpace(inputText))
        {
            return false;
        }

        // Strip leading /
        if (!inputText.StartsWith('/'))
        {
            return false;
        }

        var commandText = inputText[1..].Trim();
        if (string.IsNullOrWhiteSpace(commandText))
        {
            return false;
        }

        // Dispatch the command to find matching overload
        var dispatchResult = _dispatcher.Dispatch(_registry, commandText, new object[] { player });

        // Handle the dispatch result
        switch (dispatchResult.Response)
        {
            case DispatchResponse.Success:
                // We found a matching command, now execute it
                return ExecuteCommand(services, player, dispatchResult);

            case DispatchResponse.InvalidArguments:
                var usageMsg = dispatchResult.UsageMessage ?? "Invalid arguments";
                _entityManager.GetComponent<SampSharp.Entities.SAMP.Player>(player)?.SendClientMessage(usageMsg);
                return true;

            case DispatchResponse.PermissionDenied:
                var permMsg = dispatchResult.Message ?? "You do not have permission to use this command.";
                _entityManager.GetComponent<SampSharp.Entities.SAMP.Player>(player)?.SendClientMessage(permMsg);
                return true;

            case DispatchResponse.Error:
                var errMsg = dispatchResult.Message ?? "An error occurred while executing the command.";
                _entityManager.GetComponent<SampSharp.Entities.SAMP.Player>(player)?.SendClientMessage(errMsg);
                return true;

            case DispatchResponse.CommandNotFound:
            default:
                return false;
        }
    }

    /// <summary>Executes the matched command.</summary>
    private bool ExecuteCommand(IServiceProvider services, EntityId player, DispatchResult dispatchResult)
    {
        var command = dispatchResult.CommandDefinition;
        var overload = dispatchResult.CommandOverload;
        var parsedArgs = dispatchResult.ParsedArguments ?? Array.Empty<object?>();

        if (command == null || overload == null)
        {
            return false;
        }

        // Check permissions
        var permAttr = overload.Method.GetCustomAttributes(typeof(Attributes.RequiresPermissionAttribute), false)
            .FirstOrDefault() as Attributes.RequiresPermissionAttribute;

        if (permAttr != null)
        {
            if (!_permissionChecker.HasPermission(player, permAttr.Permissions))
            {
                var permMsg = _notFoundHandler.GetPermissionDeniedMessage(command.Name);
                _entityManager.GetComponent<SampSharp.Entities.SAMP.Player>(player)?.SendClientMessage(permMsg);
                return true;
            }
        }

        // Get the system instance
        var system = services.GetService(overload.DeclaringSystemType) as ISystem;
        if (system == null)
        {
            return false;
        }

        try
        {
            // Execute the command
            var result = _executor.Execute(
                overload,
                new object[] { player },
                parsedArgs,
                services,
                system);

            // Handle async results
            if (overload.IsAsync)
            {
                var asyncResult = AsyncCommandExecutor.ExecuteAsync(result);
                result = AsyncCommandExecutor.ToSuccessValue(asyncResult);
            }

            // Interpret the result
            var success = result switch
            {
                bool b => b,
                int i => i != 0,
                _ => true
            };

            return success;
        }
        catch (Exception ex)
        {
            var errMsg = $"Error executing command: {ex.Message}";
            _entityManager.GetComponent<SampSharp.Entities.SAMP.Player>(player)?.SendClientMessage(errMsg);
            return true;
        }
    }

    /// <summary>Gets the command registry for access to registered commands.</summary>
    public CommandRegistry GetRegistry() => _registry;

    /// <summary>Gets the command dispatcher.</summary>
    public CommandDispatcher GetDispatcher() => _dispatcher;
}
