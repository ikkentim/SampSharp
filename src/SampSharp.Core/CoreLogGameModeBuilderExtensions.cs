using System.IO;
using SampSharp.Core.Logging;

namespace SampSharp.Core;

/// <summary>
/// Provides extended functionality for <see cref="GameModeBuilder"/> for configuring the internal SampSharp logger.
/// </summary>
public static class CoreLogGameModeBuilderExtensions
{
    /// <summary>
    ///     Uses the specified log level as the maximum level which is written to the log by SampSharp.
    /// </summary>
    /// <param name="builder">The game mode builder.</param>
    /// <param name="logLevel">The log level.</param>
    /// <returns>The updated game mode configuration builder.</returns>
    public static GameModeBuilder UseLogLevel(this GameModeBuilder builder, CoreLogLevel logLevel) =>
        builder.AddBuildAction(next =>
        {
            CoreLog.LogLevel = logLevel;
            return next();
        });

    /// <summary>
    ///     Uses the specified text writer to log SampSharp log messages to.
    /// </summary>
    /// <param name="builder">The game mode builder.</param>
    /// <param name="textWriter">The text writer to log SampSharp log messages to.</param>
    /// <remarks>If a null value is specified as text writer, no log messages will appear.</remarks>
    /// <returns>The updated game mode configuration builder.</returns>
    public static GameModeBuilder UseLogWriter(this GameModeBuilder builder, TextWriter textWriter) =>
        builder.AddBuildAction(next =>
        {
            CoreLog.TextWriter = textWriter;
            return next();
        });
}