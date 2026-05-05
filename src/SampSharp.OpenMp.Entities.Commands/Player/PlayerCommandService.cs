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

    public PlayerCommandService(IEntityManager entityManager, ISystemRegistry systemRegistry,
        IPlayerCommandMessageService messageService,
        IPermissionChecker permissionChecker,
        IUnhandledExceptionHandler unhandledExceptionHandler)
    {
        _entityManager = entityManager;
        _unhandledExceptionHandler = unhandledExceptionHandler;
        _messageService = messageService;
        _permissionChecker = permissionChecker;

        _registry = new CommandRegistry();

        _executor = new CommandExecutor(entityManager);

        // Scan for player commands into the shared registry
        var scanner = new CommandScanner(systemRegistry);
        var parserFactory = new DefaultCommandParameterParserFactory();
        scanner.ScanPlayerCommands(_registry, parserFactory);
    }

    public ICommandEnumerator Commands => field ??= new DefaultCommandEnumerator(_registry);

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
        var dispatchResult = _dispatcher.Dispatch(_registry, services, commandText, [player], _permissionChecker);

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
                    if (playerComponent != null)
                    {
                        _messageService.SendUsage(playerComponent, dispatchResult.CommandDefinition!);
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
                    if (playerComponent != null)
                    {
                        var messageShown = _messageService.SendPermissionDenied(playerComponent, dispatchResult.CommandDefinition!);
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

            case DispatchResponse.Error:
                {
                    try
                    {
                        var playerComponent = _entityManager.GetComponent<Player>(player);
                        if (playerComponent != null)
                        {
                            playerComponent.SendClientMessage(dispatchResult.Message ?? "An error occurred while executing the command.");
                        }
                    }
                    catch (Exception ex)
                    {
                        _unhandledExceptionHandler.Handle("player-command-error-format", ex);
                    }
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
        var command = dispatchResult.CommandDefinition;
        var overload = dispatchResult.CommandOverload;
        var parsedArgs = dispatchResult.ParsedArguments ?? [];

        if (command == null || overload == null)
        {
            return false;
        }

        // Get the system instance
        var system = services.GetService(overload.DeclaringSystemType) as ISystem;
        if (system == null)
        {
            return false;
        }

        try
        {
            // Execute the command with the Player component as prefix argument
            var result = _executor.Execute(overload, [playerId], parsedArgs, services, system);

            // Handle async results
            if (overload.IsAsync)
            {
                result = AsyncCommandExecutor.ExecuteAsync(result);
            }

            // Interpret the result
            var success = result switch
            {
                bool b => b,
                _ => true
            };

            return success;
        }
        catch (Exception ex)
        {
            _unhandledExceptionHandler.Handle("player-command", ex);
            return true;
        }
    }

    /// <summary>Gets the command registry for access to registered commands.</summary>
    public CommandRegistry GetRegistry()
    {
        return _registry;
    }

    /// <summary>Gets the command dispatcher.</summary>
    public CommandDispatcher GetDispatcher()
    {
        return _dispatcher;
    }
}