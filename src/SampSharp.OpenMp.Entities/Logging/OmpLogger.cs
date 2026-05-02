using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using SampSharp.OpenMp.Core;
using ILogger = SampSharp.OpenMp.Core.Api.ILogger;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;
using OmpLogLevel = SampSharp.OpenMp.Core.Api.LogLevel;

namespace SampSharp.Entities.Logging;

internal class OmpLogger(ILogger inner, LogLevel minLogLevel, string name, ObjectPool<StringBuilder> objectPool)
    : Microsoft.Extensions.Logging.ILogger
{
    private readonly Dictionary<OmpLogLevel, LoggerTextWriter> _writers = new()
    {
        [OmpLogLevel.Debug] = new LoggerTextWriter(inner, OmpLogLevel.Debug),
        [OmpLogLevel.Message] = new LoggerTextWriter(inner, OmpLogLevel.Message),
        [OmpLogLevel.Warning] = new LoggerTextWriter(inner, OmpLogLevel.Warning),
        [OmpLogLevel.Error] = new LoggerTextWriter(inner, OmpLogLevel.Error)
    };

    private static OmpLogLevel Convert(LogLevel level)
    {
        return level switch
        {
            LogLevel.Trace or LogLevel.Debug => OmpLogLevel.Debug,
            LogLevel.Warning => OmpLogLevel.Warning,
            LogLevel.Error or LogLevel.Critical => OmpLogLevel.Error,
            _ => OmpLogLevel.Message
        };
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var sb = objectPool.Get();

        try
        {
            if (eventId.Id != 0)
            {
                sb.Append($"[{eventId.Id,2}]`");
            }

            if (logLevel is LogLevel.Trace or LogLevel.Critical)
            {
                sb.Append($" [{logLevel}]");
            }

            sb.Append($"{name} - {formatter(state, exception)}");

            if (exception != null)
            {
                sb.AppendLine();
                sb.Append(exception);
            }

            _writers[Convert(logLevel)].WriteLine(sb.ToString());
        }
        finally
        {
            objectPool.Return(sb);
        }
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= minLogLevel;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }
}