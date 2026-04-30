namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="ICustomModelsComponent.GetEventDispatcher" />.
/// </summary>
[OpenMpEventHandler]
public partial interface IPlayerModelsEventHandler
{
    /// <summary>
    /// Called when a player has finished downloading all custom models.
    /// </summary>
    /// <param name="player">The player who finished downloading.</param>
    void OnPlayerFinishedDownloading(IPlayer player);

    /// <summary>
    /// Called when a player requests to download a custom model.
    /// </summary>
    /// <param name="player">The player requesting the download.</param>
    /// <param name="type">The type of model being requested.</param>
    /// <param name="checksum">The checksum of the model.</param>
    /// <returns><c>true</c> to allow the download; <c>false</c> to deny it.</returns>
    bool OnPlayerRequestDownload(IPlayer player, ModelDownloadType type, uint checksum);
}