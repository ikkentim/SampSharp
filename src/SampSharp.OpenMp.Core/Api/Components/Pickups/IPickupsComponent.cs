using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPickupsComponent" /> interface.
/// </summary>
[OpenMpApi(typeof(IPoolComponent<IPickup>))]
public readonly partial struct IPickupsComponent
{
    /// <inheritdoc />
    public static UID ComponentId => new(0xcf304faa363dd971);

    /// <summary>
    /// Gets the event dispatcher for pickup events.
    /// </summary>
    /// <returns>An event dispatcher for <see cref="IPickupEventHandler"/> events.</returns>
    public partial IEventDispatcher<IPickupEventHandler> GetEventDispatcher();

    /// <summary>
    /// Creates a new pickup.
    /// </summary>
    /// <param name="modelId">The model ID of the pickup.</param>
    /// <param name="type">The type of the pickup.</param>
    /// <param name="pos">The position where the pickup will be created.</param>
    /// <param name="virtualWorld">The virtual world ID for the pickup.</param>
    /// <param name="isStatic">Whether the pickup is static and cannot be moved.</param>
    /// <returns>The created pickup, or <c>null</c> if creation failed.</returns>
    public partial IPickup Create(int modelId, byte type, Vector3 pos, uint virtualWorld, bool isStatic);

    /// <summary>
    /// Converts a real pickup ID to its legacy ID equivalent.
    /// </summary>
    /// <param name="real">The real pickup ID.</param>
    /// <returns>The legacy pickup ID.</returns>
    public partial int ToLegacyID(int real);

    /// <summary>
    /// Converts a legacy pickup ID to its real ID equivalent.
    /// </summary>
    /// <param name="legacy">The legacy pickup ID.</param>
    /// <returns>The real pickup ID.</returns>
    public partial int FromLegacyID(int legacy);

    /// <summary>
    /// Releases a legacy pickup ID that was previously reserved.
    /// </summary>
    /// <param name="legacy">The legacy pickup ID to release.</param>
    public partial void ReleaseLegacyID(int legacy);

    /// <summary>
    /// Reserves an unused legacy pickup ID.
    /// </summary>
    /// <returns>A reserved legacy pickup ID.</returns>
    public partial int ReserveLegacyID();

    /// <summary>
    /// Assigns a real pickup ID to a previously reserved legacy ID.
    /// </summary>
    /// <param name="legacy">The legacy pickup ID.</param>
    /// <param name="real">The real pickup ID to assign.</param>
    public partial void SetLegacyID(int legacy, int real);
}