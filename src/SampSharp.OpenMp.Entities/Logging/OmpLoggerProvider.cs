using System.Collections.Concurrent;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using ILogger = SampSharp.OpenMp.Core.Api.ILogger;
using OmpLogLevel = SampSharp.OpenMp.Core.Api.LogLevel;

namespace SampSharp.Entities.Logging;

internal sealed class OmpLoggerProvider(ILogger innerLogger, Dictionary<LogLevel, OmpLogLevel> logLevelMapping, LogLevel minLogLevel) : ILoggerProvider
{
    private readonly ConcurrentDictionary<string, Microsoft.Extensions.Logging.ILogger> _loggers = [];
    private readonly ObjectPool<StringBuilder> _stringBuilders = new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy());

    public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
    {
        return _loggers.GetOrAdd(categoryName, CreateNewLogger);
    }

    public void Dispose()
    {
        //
    }

    private Microsoft.Extensions.Logging.ILogger CreateNewLogger(string name)
    {
        return new OmpLogger(innerLogger, logLevelMapping, minLogLevel, name, _stringBuilders);
    }
}