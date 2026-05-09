using Microsoft.Extensions.Options;

namespace SampSharp.Entities.SAMP.Commands;

internal class PlayerCommandService : IPlayerCommandService
{
    private readonly CommandDispatcher _dispatcher = new();
    private readonly IEntityManager _entityManager;
    private readonly CommandExecutor _executor;
    private readonly IPermissionChecker _permissionChecker;
    private readonly CommandRegistry _registry;
    private readonly IUnhandledExceptionHandler _unhandledExceptionHandler;
    private readonly IPlayerCommandMessageService _messageService;

    public PlayerCommandService(IEntityManager entityManager, ISystemRegistry systemRegistry, IPlayerCommandMessageService messageService,
        IPermissionChecker permissionChecker, IUnhandledExceptionHandler unhandledExceptionHandler, ICommandParameterParserFactory parserFactory,
        IOptions<PlayerCommandServiceOptions> options)
    {
        _entityManager = entityManager;
        _unhandledExceptionHandler = unhandledExceptionHandler;
        _messageService = messageService;
        _permissionChecker = permissionChecker;

        _registry = new CommandRegistry(options.Value.StringComparison);
        _executor = new CommandExecutor(entityManager);

        // Scan for player commands into the shared registry
        var scanner = new CommandScanner(systemRegistry, unhandledExceptionHandler);
        scanner.ScanPlayerCommands(_registry, parserFactory);
    }

    public ICommandRegistry Registry => _registry;

    public bool Invoke(IServiceProvider services, EntityId player, string inputText)
    {
        if (string.IsNullOrEmpty(inputText))
        {
            return false;
        }

        var span = StringSpan.For(inputText).TrimStart();

        // Require leading /
        if (span.Length == 0 || span[0] != '/')
        {
            return false;
        }

        // Skip the / and any whitespace immediately after it
        span = span.Skip(1).TrimStart();

        if (span.Length == 0)
        {
            return false;
        }

        // Dispatch the command to find matching overload
        var dispatchResult = _dispatcher.Dispatch(_registry, services, span, [player], _permissionChecker);

        // Handle the dispatch result
        switch (dispatchResult.Response)
        {
            case DispatchResponse.Success:
                // We found a matching command, now execute it
                return ExecuteCommand(services, player, dispatchResult);

            case DispatchResponse.InvalidArguments:
                // Send usage message via message service
                try
                {
                    var playerComponent = _entityManager.GetComponent<Player>(player);
                    if (playerComponent != null && dispatchResult.AllOverloads != null)
                    {
                        _messageService.SendUsage(playerComponent, dispatchResult.AllOverloads, dispatchResult.UsedCommandName);
                    }
                }
                catch (Exception ex)
                {
                    _unhandledExceptionHandler.Handle("player-command-usage-format", ex);
                }

                return true;

            case DispatchResponse.PermissionDenied:
                // Send permission denied message via message service
                // If it returns true, treat as command not found
                try
                {
                    var playerComponent = _entityManager.GetComponent<Player>(player);
                    if (playerComponent != null && dispatchResult.CommandOverload != null)
                    {
                        var messageShown = _messageService.SendPermissionDenied(playerComponent, dispatchResult.CommandOverload);
                        if (!messageShown)
                        {
                            // Fall through to command not found logic
                            return HandleCommandNotFound(player, inputText);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _unhandledExceptionHandler.Handle("player-command-permission-format", ex);
                }

                return true;

            case DispatchResponse.CommandNotFound:
            default:
                return HandleCommandNotFound(player, inputText);
        }
    }

    private bool HandleCommandNotFound(EntityId player, string input)
    {
        try
        {
            var playerComponent = _entityManager.GetComponent<Player>(player);
            if (playerComponent != null)
            {
                return _messageService.SendCommandNotFound(playerComponent, input);
            }
        }
        catch (Exception ex)
        {
            _unhandledExceptionHandler.Handle("player-command-notfound-format", ex);
        }

        return false;
    }

    /// <summary>Executes the matched command.</summary>
    private bool ExecuteCommand(IServiceProvider services, EntityId playerId, DispatchResult dispatchResult)
    {
        var overload = dispatchResult.CommandOverload;
        var parsedArgs = dispatchResult.ParsedArguments ?? [];

        if (overload == null)
        {
            return false;
        }

        // Get the system instance
        overload.System ??= services.GetService(overload.DeclaringSystemType) as ISystem;

        if (overload.System == null)
        {
            return false;
        }

        try
        {
            // Execute the command with the Player component as prefix argument
            return _executor.Execute(overload, [playerId], parsedArgs, services, overload.System);
        }
        catch (Exception ex)
        {
            _unhandledExceptionHandler.Handle("player-command", ex);
            return true;
        }
    }
}