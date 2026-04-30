namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPlayerGangZoneData" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtension))]
public readonly partial struct IPlayerGangZoneData
{
    /// <inheritdoc />
    public static UID ExtensionId => new(0xee8d8056b3351d11);

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

    /// <summary>
    /// Converts a real gang zone ID to its client ID equivalent.
    /// </summary>
    /// <param name="real">The real gang zone ID.</param>
    /// <returns>The client gang zone ID.</returns>
    public partial int ToClientID(int real);

    /// <summary>
    /// Converts a client gang zone ID to its real ID equivalent.
    /// </summary>
    /// <param name="legacy">The client gang zone ID.</param>
    /// <returns>The real gang zone ID.</returns>
    public partial int FromClientID(int legacy);

    /// <summary>
    /// Releases a client gang zone ID that was previously reserved.
    /// </summary>
    /// <param name="legacy">The client gang zone ID to release.</param>
    public partial void ReleaseClientID(int legacy);

    /// <summary>
    /// Reserves an unused client gang zone ID.
    /// </summary>
    /// <returns>A reserved client gang zone ID.</returns>
    public partial int ReserveClientID();

    /// <summary>
    /// Assigns a real gang zone ID to a previously reserved client ID.
    /// </summary>
    /// <param name="legacy">The client gang zone ID.</param>
    /// <param name="real">The real gang zone ID to assign.</param>
    public partial void SetClientID(int legacy, int real);
}