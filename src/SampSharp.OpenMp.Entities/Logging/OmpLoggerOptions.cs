using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities;

/// <summary>
/// Provides options for <see cref="OmpLoggerProvider"/>.
/// </summary>
public class OmpLoggerOptions
{
    /// <summary>
    /// The open.mp log level for messages with severity <see cref="Microsoft.Extensions.Logging.LogLevel.Trace"/>.
    /// </summary>
    public LogLevel TraceLevel { get; set; } = LogLevel.Message;
    /// <summary>
    /// The open.mp log level for messages with severity <see cref="Microsoft.Extensions.Logging.LogLevel.Debug"/>.
    /// </summary>
    public LogLevel DebugLevel { get; set; } = LogLevel.Message;
    /// <summary>
    /// The open.mp log level for messages with severity <see cref="Microsoft.Extensions.Logging.LogLevel.Information"/>.
    /// </summary>
    public LogLevel InformationLevel { get; set; } = LogLevel.Message;
    /// <summary>
    /// The open.mp log level for messages with severity <see cref="Microsoft.Extensions.Logging.LogLevel.Warning"/>.
    /// </summary>
    public LogLevel WarningLevel { get; set; } = LogLevel.Warning;
    /// <summary>
    /// The open.mp log level for messages with severity <see cref="Microsoft.Extensions.Logging.LogLevel.Error"/>.
    /// </summary>
    public LogLevel ErrorLevel { get; set; } = LogLevel.Error;
    /// <summary>
    /// The open.mp log level for messages with severity <see cref="Microsoft.Extensions.Logging.LogLevel.Critical"/>.
    /// </summary>
    public LogLevel CriticalLevel { get; set; } = LogLevel.Error;
}