using Microsoft.Extensions.DependencyInjection;
using SampSharp.OpenMp.Core.Api;
using SampSharp.Entities.SAMP.Commands.Core;
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
    private readonly ICommandNotFoundHandler _notFoundHandler;

    public ConsoleCommandService(
        IEntityManager entityManager,
        ISystemRegistry systemRegistry,
        ICommandNotFoundHandler? notFoundHandler = null)
    {
        if (entityManager == null)
        {
            throw new ArgumentNullException(nameof(entityManager));
        }

        if (systemRegistry == null)
        {
            throw new ArgumentNullException(nameof(systemRegistry));
        }

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

        // Wrap the sender data
        // Dispatch the command
        var result = _dispatcher.Dispatch(_registry, inputText, new object[] { senderData });

        // Handle the result
        switch (result.Response)
        {
            case DispatchResponse.Success:
                return null; // Success - no message needed

            case DispatchResponse.InvalidArguments:
                return result.UsageMessage ?? "Invalid arguments";

            case DispatchResponse.Error:
                return result.Message ?? "An error occurred while executing the command.";

            case DispatchResponse.CommandNotFound:
            default:
                return _notFoundHandler.GetCommandNotFoundMessage(inputText);
        }
    }

    /// <summary>Gets the command registry for access to registered commands.</summary>
    public CommandRegistry GetRegistry() => _registry;

    /// <summary>Gets the command dispatcher.</summary>
    public CommandDispatcher GetDispatcher() => _dispatcher;
}
