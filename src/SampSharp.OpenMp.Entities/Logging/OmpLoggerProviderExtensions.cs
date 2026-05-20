using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace SampSharp.Entities;

/// <summary>
/// Provides extension methods for adding an open.mp logger to an <see cref="ILoggingBuilder" />.
/// </summary>
public static class OmpLoggerProviderExtensions
{
    /// <summary>
    /// Adds an open.mp logger to the logging builder.
    /// </summary>
    /// <param name="builder">The logger builder</param>
    public static void AddOpenMp(this ILoggingBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);


        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, OmpLoggerProvider>());

        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<OmpLoggerOptions>, OmpLoggerConfigureOptions>());
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IOptionsChangeTokenSource<OmpLoggerOptions>, LoggerProviderOptionsChangeTokenSource<OmpLoggerOptions, OmpLoggerProvider>>());
    }

}