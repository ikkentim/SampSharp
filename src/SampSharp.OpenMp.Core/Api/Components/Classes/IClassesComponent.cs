using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IClassesComponent" /> interface.
/// </summary>
[OpenMpApi(typeof(IPoolComponent<IClass>))]
public readonly partial struct IClassesComponent
{
    /// <inheritdoc />
    public static UID ComponentId => new(0x8cfb3183976da208);

    /// <summary>
    /// Gets the event dispatcher for class-related events.
    /// </summary>
    /// <returns>The event dispatcher instance.</returns>
    public partial IEventDispatcher<IClassEventHandler> GetEventDispatcher();

    /// <summary>
    /// Creates a new player class with the specified properties.
    /// </summary>
    /// <param name="skin">The skin model ID for the class.</param>
    /// <param name="team">The team ID for the class.</param>
    /// <param name="spawn">The spawn position for the class.</param>
    /// <param name="angle">The spawn angle (rotation) for the class.</param>
    /// <param name="weapons">The weapons assigned to the class.</param>
    /// <returns>The newly created class object.</returns>
    public partial IClass Create(int skin, int team, Vector3 spawn, float angle, ref WeaponSlots weapons);
}