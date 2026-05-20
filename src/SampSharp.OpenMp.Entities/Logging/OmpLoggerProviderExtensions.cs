using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using ILogger = SampSharp.OpenMp.Core.Api.ILogger;
using OmpLogLevel = SampSharp.OpenMp.Core.Api.LogLevel;

namespace SampSharp.Entities.Logging;

/// <summary>
/// Provides extension methods for adding an open.mp logger to an <see cref="ILoggingBuilder" />.
/// </summary>
public static class OmpLoggerProviderExtensions
{
    /// <summary>
    /// Adds an open.mp logger to the logging builder.
    /// </summary>
    /// <param name="builder">The logger builder</param>
    /// <param name="logLevelMapping">Customized mapping of Microsoft.Extensions.Logging.LogLevel to open.mp LogLevel.</param>
    /// <param name="minLogLevel">The minimum log level to write to the open.mp logger.</param>
    public static void AddOpenMp(this ILoggingBuilder builder, Dictionary<LogLevel, OmpLogLevel> logLevelMapping, LogLevel minLogLevel = LogLevel.Trace)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.TryAddSingleton<ILoggerProvider>(sp => 
            new OmpLoggerProvider((ILogger)sp.GetRequiredService<SampSharpEnvironment>().Core, logLevelMapping, minLogLevel));
    }
}