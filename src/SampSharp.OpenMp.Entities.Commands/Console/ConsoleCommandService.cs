namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Dispatches console commands from the open.mp console.
/// Handles ConsoleCommandDispatchContext and command execution.
/// </summary>
internal class ConsoleCommandService
{
    private readonly CommandDispatcher _dispatcher = new();
    private readonly CommandExecutor _executor;
    private readonly CommandRegistry _registry = new();
    private readonly IUnhandledExceptionHandler _unhandledExceptionHandler;
    private readonly IConsoleCommandMessageService _messageService;

    public ConsoleCommandService(IEntityManager entityManager, ISystemRegistry systemRegistry, IConsoleCommandMessageService messageService,
        IUnhandledExceptionHandler unhandledExceptionHandler)
    {
        _unhandledExceptionHandler = unhandledExceptionHandler;
        _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));

        _executor = new CommandExecutor(entityManager);

        // Scan for console commands
        var scanner = new CommandScanner(systemRegistry);
        var parserFactory = new DefaultCommandParameterParserFactory();
        scanner.ScanConsoleCommands(_registry, parserFactory);
    }

    /// <summary>
    /// Invokes a console command.
    /// </summary>
    /// <param name="services">Service provider for DI.</param>
    /// <param name="context">Console command dispatch context.</param>
    /// <param name="inputText">The command text (without leading slash).</param>
    /// <returns>True if command was found and executed; false otherwise.</returns>
    public bool Invoke(IServiceProvider services, ConsoleCommandDispatchContext context, string inputText)
    {
        if (string.IsNullOrWhiteSpace(inputText))
        {
            return false;
        }

        inputText = inputText.Trim();

        // Dispatch the command (no permission checks for console)
        var result = _dispatcher.Dispatch(_registry, services, inputText, [context]);

        // Handle the result
        switch (result.Response)
        {
            case DispatchResponse.Success:
                // Execute the command
                return ExecuteCommand(services, result, context);

            case DispatchResponse.InvalidArguments:
            case DispatchResponse.CommandNotFound:
            default:
                if (result.CommandDefinition != null)
                {
                    try
                    {
                        _messageService.SendUsage(context, result.CommandDefinition);
                    }
                    catch (Exception ex)
                    {
                        _unhandledExceptionHandler.Handle("console-command-usage-format", ex);
                    }
                }

                return result.Response == DispatchResponse.Success; // Only return true for Success

            case DispatchResponse.Error:
                context.SendMessage(result.Message ?? "An error occurred while executing the command.");
                return true;
        }
    }

    private bool ExecuteCommand(IServiceProvider services, DispatchResult dispatchResult, ConsoleCommandDispatchContext context)
    {
        var command = dispatchResult.CommandDefinition;
        var overload = dispatchResult.CommandOverload;
        var parsedArgs = dispatchResult.ParsedArguments ?? [];

        if (command == null || overload == null)
        {
            return false;
        }

        // Get the system instance
        if (services.GetService(overload.DeclaringSystemType) is not ISystem system)
        {
            return false;
        }

        try
        {
            // Execute the command
            var result = _executor.Execute(overload, [context], parsedArgs, services, system);

            // Interpret the result
            var success = result switch
            {
                bool b => b,
                _ => true
            };

            return success;
        }
        catch (Exception e)
        {
            _unhandledExceptionHandler.Handle("console-command", e);
            return false;
        }
    }

    /// <summary>Gets the command registry for access to registered commands.</summary>
    public ICommandRegistry GetRegistry()
    {
        return _registry;
    }

    /// <summary>Gets the command dispatcher.</summary>
    public CommandDispatcher GetDispatcher()
    {
        return _dispatcher;
    }
}