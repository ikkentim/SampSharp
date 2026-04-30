namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="ICheckpointData" /> interface.
/// </summary>
[OpenMpApi(typeof(ICheckpointDataBase))]
public readonly partial struct ICheckpointData;