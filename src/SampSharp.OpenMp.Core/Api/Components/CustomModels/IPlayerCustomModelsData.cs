namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPlayerCustomModelsData" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtension))]
public readonly partial struct IPlayerCustomModelsData
{
    /// <inheritdoc />
    public static UID ExtensionId => new(0xD3E2F572B38FB3F2);

    /// <summary>
    /// Gets the custom skin model ID for the player.
    /// </summary>
    /// <returns>The custom skin model ID, or 0 if no custom skin is set.</returns>
    public partial uint GetCustomSkin();

    /// <summary>
    /// Sets the custom skin model for the player.
    /// </summary>
    /// <param name="skinModel">The custom skin model ID to set.</param>
    public partial void SetCustomSkin(uint skinModel);

    /// <summary>
    /// Sends a download URL to the player for custom models.
    /// </summary>
    /// <param name="url">The URL to send to the player.</param>
    /// <returns><c>true</c> if the URL was sent successfully; otherwise, <c>false</c>.</returns>
    public partial bool SendDownloadUrl(string url);
}