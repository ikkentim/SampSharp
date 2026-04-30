namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPlayerConsoleData" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtension))]
public readonly partial struct IPlayerConsoleData
{
    /// <inheritdoc />
    public static UID ExtensionId => new(0x9f8d20f2f471cbae);

    /// <summary>
    /// Checks whether the player has console (RCON) access.
    /// </summary>
    /// <returns><c>true</c> if the player has console access; otherwise, <c>false</c>.</returns>
    public partial bool HasConsoleAccess();

    /// <summary>
    /// Sets whether the player has console (RCON) access.
    /// </summary>
    /// <param name="set"><c>true</c> to grant console access; <c>false</c> to revoke it.</param>
    public partial void SetConsoleAccessibility(bool set);
}