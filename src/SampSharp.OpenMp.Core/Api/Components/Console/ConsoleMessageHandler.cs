namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="ConsoleMessageHandler" /> interface.
/// </summary>
[OpenMpApi]
public readonly partial struct ConsoleMessageHandler
{
    /// <summary>
    /// Handles a console message.
    /// </summary>
    /// <param name="message">The console message to handle.</param>
    public partial void HandleConsoleMessage(string message);
}