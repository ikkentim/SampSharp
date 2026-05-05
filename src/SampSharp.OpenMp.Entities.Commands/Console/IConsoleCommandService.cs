namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Defines a service for managing and invoking console commands. This service provides access to the available commands and handles the invocation logic based on input text.
/// </summary>
public interface IConsoleCommandService
{
    /// <summary>
    /// Gets an enumerator that iterates through the available commands.
    /// </summary>
    ICommandEnumerator Commands { get; }

    /// <summary>
    /// Invokes a console command from the given input text.
    /// </summary>
    /// <param name="services">The service provider for dependency injection.</param>
    /// <param name="context">The context for the console command dispatch.</param>
    /// <param name="inputText">The input text representing the command to invoke.</param>
    /// <returns>True if the command was successfully invoked; otherwise, false.</returns>
    bool Invoke(IServiceProvider services, ConsoleCommandDispatchContext context, string inputText);
}