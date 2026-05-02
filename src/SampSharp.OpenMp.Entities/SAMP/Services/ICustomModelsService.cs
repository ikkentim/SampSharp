using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>Provides access to the open.mp custom-models component.</summary>
public interface ICustomModelsService
{
    /// <summary>Registers a new custom model with the server.</summary>
    /// <param name="type">The custom model type.</param>
    /// <param name="id">The custom model ID to assign.</param>
    /// <param name="baseId">The base GTA model ID to replace.</param>
    /// <param name="dffName">The DFF (mesh) file name.</param>
    /// <param name="txdName">The TXD (texture archive) file name.</param>
    /// <param name="virtualWorld">The virtual world (-1 for all).</param>
    /// <param name="timeOn">The hour at which the model becomes visible.</param>
    /// <param name="timeOff">The hour at which the model is hidden.</param>
    /// <returns><see langword="true" /> on success.</returns>
    bool AddCustomModel(ModelType type, int id, int baseId, string dffName, string txdName, int virtualWorld = -1, byte timeOn = 0, byte timeOff = 0);

    /// <summary>Resolves a custom model ID to its base GTA model.</summary>
    /// <param name="customModelId">The custom model ID.</param>
    /// <returns>The base model ID, or <see langword="null" /> if no mapping exists.</returns>
    uint? GetBaseModel(uint customModelId);

    /// <summary>Looks up a model name by its open.mp checksum.</summary>
    /// <param name="checksum">The model checksum.</param>
    /// <returns>The model name, or <see langword="null" /> if not found.</returns>
    string? GetModelNameFromChecksum(uint checksum);

    /// <summary>Checks whether the given model ID refers to a registered custom model.</summary>
    /// <param name="modelId">The model ID.</param>
    /// <returns><see langword="true" /> if it is a custom model.</returns>
    bool IsValidCustomModel(int modelId);

    /// <summary>Gets the on-disk paths for a custom model's DFF and TXD files.</summary>
    /// <param name="modelId">The custom model ID.</param>
    /// <param name="dffPath">When this method returns, contains the DFF path or <see langword="null" />.</param>
    /// <param name="txdPath">When this method returns, contains the TXD path or <see langword="null" />.</param>
    /// <returns><see langword="true" /> on success.</returns>
    bool GetCustomModelPath(int modelId, out string? dffPath, out string? txdPath);
}
