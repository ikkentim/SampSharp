namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="ICustomModelsComponent" /> interface.
/// </summary>
[OpenMpApi(typeof(IComponent))]
public readonly partial struct ICustomModelsComponent
{
    /// <inheritdoc />
    public static UID ComponentId => new(0x15E3CB1E7C77FFFF);

    /// <summary>
    /// Adds a custom model to the server.
    /// </summary>
    /// <param name="type">The type of model being added.</param>
    /// <param name="id">The custom model ID to assign.</param>
    /// <param name="baseId">The base model ID to replace.</param>
    /// <param name="dffName">The name of the DFF file.</param>
    /// <param name="txdName">The name of the TXD file.</param>
    /// <param name="virtualWorld">The virtual world for the model (-1 for all).</param>
    /// <param name="timeOn">Time when the model is active (hour).</param>
    /// <param name="timeOff">Time when the model is inactive (hour).</param>
    /// <returns><c>true</c> if the model was added successfully; otherwise, <c>false</c>.</returns>
    public partial bool AddCustomModel(ModelType type, int id, int baseId, string dffName, string txdName, int virtualWorld = -1, byte timeOn = 0, byte timeOff = 0);

    /// <summary>
    /// Gets the relationship between a base model and custom model.
    /// </summary>
    /// <param name="baseModelIdOrInput">Input/output parameter for model IDs.</param>
    /// <param name="customModel">The custom model ID.</param>
    /// <returns><c>true</c> if the mapping exists; otherwise, <c>false</c>.</returns>
    public partial bool GetBaseModel(ref uint baseModelIdOrInput, ref uint customModel);

    /// <summary>
    /// Gets the event dispatcher for custom model events.
    /// </summary>
    /// <returns>The event dispatcher instance.</returns>
    public partial IEventDispatcher<IPlayerModelsEventHandler> GetEventDispatcher();

    /// <summary>
    /// Gets the model name from its checksum.
    /// </summary>
    /// <param name="checksum">The model checksum.</param>
    /// <returns>The model name, or <c>null</c> if not found.</returns>
    public partial string? GetModelNameFromChecksum(uint checksum);

    /// <summary>
    /// Checks whether a model ID is a valid custom model.
    /// </summary>
    /// <param name="modelId">The model ID to check.</param>
    /// <returns><c>true</c> if the model is a valid custom model; otherwise, <c>false</c>.</returns>
    public partial bool IsValidCustomModel(int modelId);

    /// <summary>
    /// Gets the file paths for a custom model.
    /// </summary>
    /// <param name="modelId">The custom model ID.</param>
    /// <param name="dffPath">When the method returns, contains the DFF file path.</param>
    /// <param name="txdPath">When the method returns, contains the TXD file path.</param>
    /// <returns><c>true</c> if the paths were retrieved successfully; otherwise, <c>false</c>.</returns>
    public partial bool GetCustomModelPath(int modelId, out string? dffPath, out string? txdPath);
}