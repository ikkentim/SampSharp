using System.Text;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.OpenMp.Core;

/// <summary>
/// A text writer that writes to the open.mp logger.
/// </summary>
/// <param name="logger">The logger to write to.</param>
/// <param name="logLevel">The log level at which to write to the logger.</param>
public class LoggerTextWriter(ILogger logger, LogLevel logLevel) : TextWriter
{
    private readonly StringBuilder _buffer = new();

    /// <inheritdoc />
    public override Encoding Encoding => Encoding.UTF8;
    
    /// <inheritdoc />
    public override void Write(char value)
    {
        if (value == '\n')
        {
            WriteBuffer();
        }
        else
        {
            _buffer.Append(value);
        }
    }
    
    /// <inheritdoc />
    public override void Write(string? value)
    {
        if (value == null)
        {
            return;
        }

        if (value.Contains('\n'))
        {
            foreach (var ch in value)
            {
                Write(ch);
            }
        }
        else
        {
            _buffer.Append(value);
        }
    }
    
    /// <inheritdoc />
    public override void WriteLine(string? value)
    {
        if (value == null)
        {
            WriteBuffer();
            return;
        }

        if (value.Contains('\n'))
        {
            foreach (var line in value.Split('\n'))
            {
                WriteLine(line.Trim('\r'));
            }
            return;
        }

        if (_buffer.Length > 0)
        {
            _buffer.Append(value);
            WriteBuffer();
        }
        else
        {
            WriteLineToLogger(value);
        }
    }
    
    /// <inheritdoc />
    public override void WriteLine()
    {
        WriteBuffer();
    }
    
    /// <inheritdoc />
    public override void Flush()
    {
        if (_buffer.Length > 0)
        {
            WriteBuffer();
        }
    }

    private void WriteBuffer()
    {
        WriteLineToLogger(_buffer.ToString());
        _buffer.Clear();
    }

    private void WriteLineToLogger(string? line)
    {
        logger.LogLine(logLevel, line ?? string.Empty);
    }
}