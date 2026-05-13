namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Dispatches console input to a method marked with <see cref="ConsoleCommandAttribute" />.
/// </summary>
public interface IConsoleCommandService
{
    /// <summary>
    /// Gets the command registry containing all registered console commands.
    /// </summary>
    ICommandRegistry Registry { get; }

    /// <summary>
    /// Invokes a console command from the given input text.
    /// </summary>
    /// <param name="services">The service provider for dependency injection.</param>
    /// <param name="context">The context for the console command dispatch.</param>
    /// <param name="inputText">The input text representing the command to invoke.</param>
    /// <returns><see langword="true"/> if the command was successfully invoked; otherwise, <see langword="false"/>.</returns>
    bool Invoke(IServiceProvider services, ConsoleCommandDispatchContext context, string inputText);
}