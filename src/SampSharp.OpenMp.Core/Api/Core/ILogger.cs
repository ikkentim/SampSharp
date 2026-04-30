using System.Text;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="ILogger" /> interface.
/// </summary>
[OpenMpApi]
public readonly unsafe partial struct ILogger
{
    /// <summary>
    /// Prints a new line to the console.
    /// </summary>
    /// <param name="msg">The message to log.</param>
    public partial void PrintLn(byte* msg);

    /// <summary>
    /// Prints a new line to the console with the specified severity level.
    /// </summary>
    /// <param name="level">The severity level at which to log the line.</param>
    /// <param name="msg">The message to log.</param>
    public partial void LogLn(LogLevel level, byte* msg);

    /// <summary>
    /// Prints a new line to the console in UTF-8 encoding.
    /// </summary>
    /// <param name="msg">The message to log.</param>
    public partial void PrintLnU8(byte* msg);
    /// <summary>
    /// Prints a new line to the console of the specified log type in UTF-8 encoding.
    /// </summary>
    /// <param name="level">The severity level at which to log the line.</param>
    /// <param name="msg">The message to log.</param>
    public partial void LogLnU8(LogLevel level, byte* msg);

    /// <summary>
    /// Logs a new line to the console with the specified severity level.
    /// </summary>
    /// <param name="level">The severity level at which to log the line.</param>
    /// <param name="msg">The message to log.</param>
    public void LogLine(LogLevel level, string msg)
    {
        ArgumentNullException.ThrowIfNull(msg);

        var arr = new byte[Encoding.UTF8.GetByteCount(msg) + 1];
        Encoding.UTF8.GetBytes(msg, arr);

        fixed (byte* msgPtr = arr)
        {
            LogLn(level, msgPtr);
        }
    }

    /// <summary>
    /// Prints a new line to the console.
    /// </summary>
    /// <param name="msg">The message to log.</param>
    public void PrintLine(string msg)
    {
        ArgumentNullException.ThrowIfNull(msg);

        var arr = new byte[Encoding.UTF8.GetByteCount(msg) + 1];
        Encoding.UTF8.GetBytes(msg, arr);

        fixed (byte* msgPtr = arr)
        {
            PrintLn(msgPtr);
        }
    }
}