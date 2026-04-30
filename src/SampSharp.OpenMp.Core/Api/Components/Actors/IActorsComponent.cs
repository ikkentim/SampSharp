using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IActorsComponent" /> interface.
/// </summary>
[OpenMpApi(typeof(IPoolComponent<IActor>))]
public readonly partial struct IActorsComponent
{
    /// <inheritdoc />
    public static UID ComponentId => new(0xc81ca021eae2ad5c);

    /// <summary>
    /// Gets the event dispatcher for actor-related events.
    /// </summary>
    /// <returns>An event dispatcher for actor-related events.</returns>
    public partial IEventDispatcher<IActorEventHandler> GetEventDispatcher();

    /// <summary>
    /// Creates a new actor in the game world.
    /// </summary>
    /// <param name="skin">The skin ID of the actor.</param>
    /// <param name="pos">The position where the actor will be created.</param>
    /// <param name="angle">The facing angle of the actor.</param>
    /// <returns>The created actor instance.</returns>
    public partial IActor Create(int skin, Vector3 pos, float angle);
}