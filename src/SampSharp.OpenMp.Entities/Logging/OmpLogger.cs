using System.Globalization;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using SampSharp.OpenMp.Core;
using ILogger = SampSharp.OpenMp.Core.Api.ILogger;
using OmpLogLevel = SampSharp.OpenMp.Core.Api.LogLevel;

namespace SampSharp.Entities;

internal sealed class OmpLogger(ILogger inner, OmpLoggerOptions options, string name, ObjectPool<StringBuilder> objectPool)
    : Microsoft.Extensions.Logging.ILogger
{
    private readonly Dictionary<OmpLogLevel, LoggerTextWriter> _writers = new()
    {
        [OmpLogLevel.Debug] = new LoggerTextWriter(inner, OmpLogLevel.Debug),
        [OmpLogLevel.Message] = new LoggerTextWriter(inner, OmpLogLevel.Message),
        [OmpLogLevel.Warning] = new LoggerTextWriter(inner, OmpLogLevel.Warning),
        [OmpLogLevel.Error] = new LoggerTextWriter(inner, OmpLogLevel.Error)
    };

    public OmpLoggerOptions Options { get; set; } = options;

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
                sb.Append(CultureInfo.InvariantCulture, $"[{eventId.Id,2}] ");
            }

            if (logLevel is not LogLevel.Information and not LogLevel.Warning and not LogLevel.Error)
            {
                sb.Append(CultureInfo.InvariantCulture, $"[{logLevel}] ");
            }

            sb.Append(CultureInfo.InvariantCulture, $"{name} - {formatter(state, exception)}");

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
        return logLevel != LogLevel.None;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    private OmpLogLevel Convert(LogLevel level)
    {
        switch (level)
        {
            case LogLevel.Trace:
                return Options.TraceLevel;

            case LogLevel.Debug:
                return Options.DebugLevel;
            case LogLevel.Information:
                return Options.InformationLevel;
            case LogLevel.Warning:
                return Options.WarningLevel;
            case LogLevel.Error:
                return Options.ErrorLevel;
            case LogLevel.Critical:
                return Options.CriticalLevel;
            case LogLevel.None:
            default:
                return OmpLogLevel.Message;
        }
    }
}