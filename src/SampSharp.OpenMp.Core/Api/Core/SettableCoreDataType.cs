namespace SampSharp.OpenMp.Core.Api;

/// <summary>
///  Types of data can be set in core during runtime.
/// </summary>
public enum SettableCoreDataType
{
    /// <summary>
    /// The server name.
    /// </summary>
    ServerName,

    /// <summary>
    /// The server mode text.
    /// </summary>
    ModeText,

    /// <summary>
    /// The active map name.
    /// </summary>
    MapName,

    /// <summary>
    /// The language of the server.
    /// </summary>
    Language,

    /// <summary>
    /// The website URL of the server.
    /// </summary>
    URL,

    /// <summary>
    /// The password users must enter to join the server.
    /// </summary>
    Password,

    /// <summary>
    /// The password users must enter to log in as an admin.
    /// </summary>
    AdminPassword
}