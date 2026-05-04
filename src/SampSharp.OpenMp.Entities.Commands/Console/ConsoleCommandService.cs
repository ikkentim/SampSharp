using Microsoft.Extensions.DependencyInjection;
using SampSharp.OpenMp.Core.Api;
using SampSharp.Entities.SAMP.Commands.Core;
using SampSharp.Entities.SAMP.Commands.Core.Execution;
using SampSharp.Entities.SAMP.Commands.Core.Scanning;
using SampSharp.Entities.SAMP.Commands.Services;

namespace SampSharp.Entities.SAMP.Commands.Console;

/// <summary>
/// Dispatches console commands from the open.mp console.
/// Handles ConsoleCommandSender context and command execution.
/// </summary>
public class ConsoleCommandService
{
    private readonly CommandRegistry _registry = new();
    private readonly CommandDispatcher _dispatcher = new();
    private readonly CommandExecutor _executor;
    private readonly IEntityManager _entityManager;
    private readonly ISystemRegistry _systemRegistry;
    private readonly IUnhandledExceptionHandler _unhandledExceptionHandler;
    private readonly ICommandNotFoundHandler _notFoundHandler;

    public ConsoleCommandService(
        IEntityManager entityManager,
        ISystemRegistry systemRegistry,
        ICommandNotFoundHandler? notFoundHandler,
        IUnhandledExceptionHandler unhandledExceptionHandler)
    {
        if (entityManager == null)
        {
            throw new ArgumentNullException(nameof(entityManager));
        }

        if (systemRegistry == null)
        {
            throw new ArgumentNullException(nameof(systemRegistry));
        }

        _entityManager = entityManager;
        _systemRegistry = systemRegistry;
        _unhandledExceptionHandler = unhandledExceptionHandler;
        _executor = new CommandExecutor(entityManager);
        _notFoundHandler = notFoundHandler ?? new DefaultCommandNotFoundHandler();

        // Scan for console commands
        var scanner = new CommandScanner(entityManager, systemRegistry);
        var parserFactory = new DefaultCommandParameterParserFactory();
        scanner.ScanConsoleCommands(_registry, parserFactory);
    }

    /// <summary>
    /// Invokes a console command.
    /// </summary>
    /// <param name="services">Service provider for DI.</param>
    /// <param name="senderData">Console command sender information from open.mp API.</param>
    /// <param name="inputText">The command text (without leading slash).</param>
    /// <returns>The response message to send back, or null for success.</returns>
    public string? Invoke(IServiceProvider services, ConsoleCommandSender senderData, string inputText)
    {
        if (string.IsNullOrWhiteSpace(inputText))
        {
            return "Invalid command.";
        }

        inputText = inputText.Trim();

        // Dispatch the command
        var result = _dispatcher.Dispatch(_registry, inputText, new object[] { senderData });

        // Handle the result
        switch (result.Response)
        {
            case DispatchResponse.Success:
                // Execute the command
                return ExecuteCommand(services, result, senderData) ? null : "Failed to execute command.";

            case DispatchResponse.InvalidArguments:
                return result.UsageMessage ?? "Invalid arguments";

            case DispatchResponse.Error:
                return result.Message ?? "An error occurred while executing the command.";

            case DispatchResponse.CommandNotFound:
            default:
                return _notFoundHandler.GetCommandNotFoundMessage(inputText);
        }
    }

    private bool ExecuteCommand(IServiceProvider services, DispatchResult dispatchResult, ConsoleCommandSender senderData)
    {
        var command = dispatchResult.CommandDefinition;
        var overload = dispatchResult.CommandOverload;
        var parsedArgs = dispatchResult.ParsedArguments ?? Array.Empty<object?>();

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
            // Execute the command
            var result = _executor.Execute(
                overload, [senderData],
                parsedArgs,
                services,
                system);

            // Interpret the result
            var success = result switch
            {
                bool b => b,
                int i => i != 0,
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
    public CommandRegistry GetRegistry() => _registry;

    /// <summary>Gets the command dispatcher.</summary>
    public CommandDispatcher GetDispatcher() => _dispatcher;
}
