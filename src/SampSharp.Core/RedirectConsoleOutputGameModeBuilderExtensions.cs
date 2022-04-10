using System;

namespace SampSharp.Core;

/// <summary>
/// Provides extended functionality for <see cref="GameModeBuilder"/> for configuring console output redirection.
/// </summary>
public static class RedirectConsoleOutputGameModeBuilderExtensions
{
    /// <summary>
    ///     Redirect the console output to the server.
    /// </summary>
    /// <param name="builder">The game mode builder.</param>
    /// <returns>The updated game mode configuration builder.</returns>
    public static GameModeBuilder RedirectConsoleOutput(this GameModeBuilder builder) =>
        builder.AddBuildAction(next =>
        {
            var runner = next();
            Console.SetOut(new ServerLogWriter(runner.Client));
            return runner;
        });
}