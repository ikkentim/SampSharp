namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IClassesComponent.GetEventDispatcher" />.
/// </summary>
[OpenMpEventHandler]
public partial interface IClassEventHandler
{
    /// <summary>
    /// Called when a player requests to spawn with a class.
    /// </summary>
    /// <param name="player">The player who requested the class.</param>
    /// <param name="classId">The ID of the class being requested.</param>
    /// <returns><c>true</c> to allow the class selection; otherwise, <c>false</c> to deny it.</returns>
    bool OnPlayerRequestClass(IPlayer player, uint classId);
}