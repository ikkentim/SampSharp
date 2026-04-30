namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPlayerMenuData" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtension))]
public readonly partial struct IPlayerMenuData
{
    /// <inheritdoc />
    public static UID ExtensionId => new(0x01d8e934e9791b99);

    /// <summary>
    /// Gets the ID of the menu currently shown to the player.
    /// </summary>
    /// <returns>The menu ID, or 0xFF if no menu is currently shown.</returns>
    public partial byte GetMenuID();

    /// <summary>
    /// Sets the menu ID for the player.
    /// </summary>
    /// <param name="id">The menu ID to set.</param>
    public partial void SetMenuID(byte id);
}