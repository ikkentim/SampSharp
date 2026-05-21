using System.Collections.Concurrent;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using ILogger = SampSharp.OpenMp.Core.Api.ILogger;

namespace SampSharp.Entities;

/// <summary>
/// Provides an implementation of <see cref="ILoggerProvider"/> that creates loggers which write to the open.mp logging infrastructure.
/// </summary>
[ProviderAlias("Omp")]
public sealed class OmpLoggerProvider : ILoggerProvider
{
    private readonly ConcurrentDictionary<string, OmpLogger> _loggers = [];
    private readonly ObjectPool<StringBuilder> _stringBuilders = new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy());

    private readonly IDisposable? _optionsReloadToken;
    private readonly IOptionsMonitor<OmpLoggerOptions> _options;
    private readonly SampSharpEnvironment _environment;

    /// <summary>
    /// Initializes a new instance of the <see cref="OmpLoggerProvider"/> class.
    /// </summary>
    /// <param name="options">The options monitor for <see cref="OmpLoggerOptions"/>.</param>
    /// <param name="environment">The SampSharp environment.</param>
    public OmpLoggerProvider(IOptionsMonitor<OmpLoggerOptions> options, SampSharpEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(environment);

        _options = options;
        _environment = environment;

        ReloadLoggerOptions(options.CurrentValue);
        _optionsReloadToken = _options.OnChange(ReloadLoggerOptions);
    }

    private void ReloadLoggerOptions(OmpLoggerOptions options)
    {
        foreach (var logger in _loggers)
        {
            logger.Value.Options = options;
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _optionsReloadToken?.Dispose();
    }

    /// <inheritdoc />
    public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
    {
        return _loggers.GetOrAdd(categoryName, CreateNewLogger);
    }

    private OmpLogger CreateNewLogger(string name)
    {
        return new OmpLogger((ILogger)_environment.Core, _options.CurrentValue, name, _stringBuilders);
    }
}