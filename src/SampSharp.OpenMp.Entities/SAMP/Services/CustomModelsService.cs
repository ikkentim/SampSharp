using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class CustomModelsService(SampSharpEnvironment environment) : ICustomModelsService
{
    private readonly ICustomModelsComponent _customModels = environment.Components.QueryComponent<ICustomModelsComponent>();

    public bool AddCustomModel(ModelType type, int id, int baseId, string dffName, string txdName, int virtualWorld = -1, byte timeOn = 0, byte timeOff = 0)
    {
        ArgumentNullException.ThrowIfNull(dffName);
        ArgumentNullException.ThrowIfNull(txdName);
        return _customModels.AddCustomModel(type, id, baseId, dffName, txdName, virtualWorld, timeOn, timeOff);
    }

    public uint? GetBaseModel(uint customModelId)
    {
        uint baseId = 0;
        var custom = customModelId;
        return _customModels.GetBaseModel(ref baseId, ref custom) ? baseId : null;
    }

    public string? GetModelNameFromChecksum(uint checksum)
    {
        return _customModels.GetModelNameFromChecksum(checksum);
    }

    public bool IsValidCustomModel(int modelId)
    {
        return _customModels.IsValidCustomModel(modelId);
    }

    public bool GetCustomModelPath(int modelId, out string? dffPath, out string? txdPath)
    {
        return _customModels.GetCustomModelPath(modelId, out dffPath, out txdPath);
    }
}
