using SampSharp.OpenMp.Core.RobinHood;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IGangZonesComponent" /> interface.
/// </summary>
[OpenMpApi(typeof(IPoolComponent<IGangZone>))]
public readonly partial struct IGangZonesComponent
{
    /// <inheritdoc />
    public static UID ComponentId => new(0xb3351d11ee8d8056);

    /// <summary>
    /// Gets the event dispatcher for gang zone events.
    /// </summary>
    /// <returns>An event dispatcher for <see cref="IGangZoneEventHandler" /> events.</returns>
    public partial IEventDispatcher<IGangZoneEventHandler> GetEventDispatcher();

    /// <summary>
    /// Creates a new gang zone.
    /// </summary>
    /// <param name="pos">The position/boundaries of the gang zone.</param>
    /// <returns>The created gang zone, or <c>null</c> if creation failed.</returns>
    public partial IGangZone Create(GangZonePos pos);

    /// <summary>
    /// Gets the set of gang zones that have enter/leave checking enabled.
    /// </summary>
    /// <returns>A collection of gang zones with checking enabled.</returns>
    public partial FlatPtrHashSet<IGangZone> GetCheckingGangZones();

    /// <summary>
    /// Enables or disables enter/leave checking for a gang zone.
    /// </summary>
    /// <param name="zone">The gang zone to configure.</param>
    /// <param name="enable"><c>true</c> to enable checking; <c>false</c> to disable it.</param>
    public partial void UseGangZoneCheck(IGangZone zone, bool enable);

    /// <summary>
    /// Converts a real gang zone ID to its legacy ID equivalent.
    /// </summary>
    /// <param name="real">The real gang zone ID.</param>
    /// <returns>The legacy gang zone ID.</returns>
    public partial int ToLegacyID(int real);

    /// <summary>
    /// Converts a legacy gang zone ID to its real ID equivalent.
    /// </summary>
    /// <param name="legacy">The legacy gang zone ID.</param>
    /// <returns>The real gang zone ID.</returns>
    public partial int FromLegacyID(int legacy);

    /// <summary>
    /// Releases a legacy gang zone ID that was previously reserved.
    /// </summary>
    /// <param name="legacy">The legacy gang zone ID to release.</param>
    public partial void ReleaseLegacyID(int legacy);

    /// <summary>
    /// Reserves an unused legacy gang zone ID.
    /// </summary>
    /// <returns>A reserved legacy gang zone ID.</returns>
    public partial int ReserveLegacyID();

    /// <summary>
    /// Assigns a real gang zone ID to a previously reserved legacy ID.
    /// </summary>
    /// <param name="legacy">The legacy gang zone ID.</param>
    /// <param name="real">The real gang zone ID to assign.</param>
    public partial void SetLegacyID(int legacy, int real);
}