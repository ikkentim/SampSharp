using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal sealed class CustomModelsService(SampSharpEnvironment environment) : ICustomModelsService
{
    private readonly SafeComponentHandle<ICustomModelsComponent> _customModels = environment.SafeComponentHandleProvider.Get<ICustomModelsComponent>();

    private ICustomModelsComponent CustomModels => _customModels;

    public bool AddCustomModel(ModelType type, int id, int baseId, string dffName, string txdName, int virtualWorld = -1, byte timeOn = 0, byte timeOff = 0)
    {
        ArgumentNullException.ThrowIfNull(dffName);
        ArgumentNullException.ThrowIfNull(txdName);
        return CustomModels.AddCustomModel(type, id, baseId, dffName, txdName, virtualWorld, timeOn, timeOff);
    }

    public uint? GetBaseModel(uint customModelId)
    {
        uint baseId = 0;
        var custom = customModelId;
        return CustomModels.GetBaseModel(ref baseId, ref custom) ? baseId : null;
    }

    public string? GetModelNameFromChecksum(uint checksum)
    {
        return CustomModels.GetModelNameFromChecksum(checksum);
    }

    public bool IsValidCustomModel(int modelId)
    {
        return CustomModels.IsValidCustomModel(modelId);
    }

    public bool GetCustomModelPath(int modelId, out string? dffPath, out string? txdPath)
    {
        return CustomModels.GetCustomModelPath(modelId, out dffPath, out txdPath);
    }
}
