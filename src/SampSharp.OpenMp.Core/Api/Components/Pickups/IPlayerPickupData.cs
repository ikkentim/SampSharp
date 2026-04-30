namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPlayerPickupData" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtension))]
public readonly partial struct IPlayerPickupData
{
    /// <inheritdoc />
    public static UID ExtensionId => new(0x98376F4428D7B70B);

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

    /// <summary>
    /// Converts a real pickup ID to its client ID equivalent.
    /// </summary>
    /// <param name="real">The real pickup ID.</param>
    /// <returns>The client pickup ID.</returns>
    public partial int ToClientID(int real);

    /// <summary>
    /// Converts a client pickup ID to its real ID equivalent.
    /// </summary>
    /// <param name="legacy">The client pickup ID.</param>
    /// <returns>The real pickup ID.</returns>
    public partial int FromClientID(int legacy);

    /// <summary>
    /// Releases a client pickup ID that was previously reserved.
    /// </summary>
    /// <param name="legacy">The client pickup ID to release.</param>
    public partial void ReleaseClientID(int legacy);

    /// <summary>
    /// Reserves an unused client pickup ID.
    /// </summary>
    /// <returns>A reserved client pickup ID.</returns>
    public partial int ReserveClientID();

    /// <summary>
    /// Assigns a real pickup ID to a previously reserved client ID.
    /// </summary>
    /// <param name="legacy">The client pickup ID.</param>
    /// <param name="real">The real pickup ID to assign.</param>
    public partial void SetClientID(int legacy, int real);
}