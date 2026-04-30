using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

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
    /// <param name="minLogLevel">The minimum log level to write to the open.mp logger.</param>
    public static void AddOpenMp(this ILoggingBuilder builder, LogLevel minLogLevel = LogLevel.Trace)
    {
        builder.Services.TryAddSingleton<ILoggerProvider>(sp => 
            new OmpLoggerProvider((OpenMp.Core.Api.ILogger)sp.GetRequiredService<SampSharpEnvironment>().Core, minLogLevel));
    }
}