using Microsoft.Extensions.Options;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Dispatches console commands from the open.mp console.
/// Handles ConsoleCommandDispatchContext and command execution.
/// </summary>
internal class ConsoleCommandService : IConsoleCommandService
{
    private readonly CommandDispatcher _dispatcher = new();
    private readonly CommandExecutor _executor;
    private readonly CommandRegistry _registry;
    private readonly IUnhandledExceptionHandler _unhandledExceptionHandler;
    private readonly IConsoleCommandMessageService _messageService;

    public ConsoleCommandService(IEntityManager entityManager, ISystemRegistry systemRegistry, IConsoleCommandMessageService messageService,
        IUnhandledExceptionHandler unhandledExceptionHandler, ICommandParameterParserFactory parserFactory, IOptions<ConsoleCommandServiceOptions> options)
    {
        _unhandledExceptionHandler = unhandledExceptionHandler;
        _messageService = messageService;

        _executor = new CommandExecutor(entityManager);
        _registry = new CommandRegistry(options.Value.StringComparison);

        // Scan for console commands
        var scanner = new CommandScanner(systemRegistry, unhandledExceptionHandler);
        scanner.ScanConsoleCommands(_registry, parserFactory);
    }

    public ICommandRegistry Registry => _registry;

    public bool Invoke(IServiceProvider services, ConsoleCommandDispatchContext context, string inputText)
    {
        if (string.IsNullOrEmpty(inputText))
        {
            return false;
        }

        var span = StringSpan.For(inputText).TrimStart();

        if (span.Length == 0)
        {
            return false;
        }

        // Dispatch the command (no permission checks for console)
        var result = _dispatcher.Dispatch(_registry, services, span, [context]);

        // Handle the result
        switch (result.Response)
        {
            case DispatchResponse.Success:
                // Execute the command
                return ExecuteCommand(services, result, context);

            case DispatchResponse.InvalidArguments:
                if (result.AllOverloads != null)
                {
                    try
                    {
                        _messageService.SendUsage(context, result.AllOverloads, result.UsedCommandName);
                    }
                    catch (Exception ex)
                    {
                        _unhandledExceptionHandler.Handle("console-command-usage-format", ex);
                    }
                }

                return true;

            case DispatchResponse.CommandNotFound:
            default:
                return false;
        }
    }

    private bool ExecuteCommand(IServiceProvider services, DispatchResult dispatchResult, ConsoleCommandDispatchContext context)
    {
        var overload = dispatchResult.CommandOverload;
        var parsedArgs = dispatchResult.ParsedArguments ?? [];

        if (overload == null)
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
            return _executor.Execute(overload, [context], parsedArgs, services, system);
        }
        catch (Exception e)
        {
            _unhandledExceptionHandler.Handle("console-command", e);
            return false;
        }
    }
}