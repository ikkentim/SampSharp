namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IRecordingsComponent" /> interface.
/// </summary>
[OpenMpApi(typeof(IComponent))]
public readonly partial struct IRecordingsComponent
{
    /// <inheritdoc />
    public static UID ComponentId => new(0x871144D399F5F613);
}