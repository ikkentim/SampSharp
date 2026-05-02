using System.Numerics;
using INPCComponent = SampSharp.OpenMp.Core.Api.INPCComponent;

namespace SampSharp.Entities.SAMP;

internal class NpcService : INpcService
{
    private readonly INPCComponent _npcs;

    public NpcService(SampSharpEnvironment environment)
    {
        _npcs = environment.Components.QueryComponent<INPCComponent>();
    }

    public int CreatePath()
    {
        return _npcs.CreatePath();
    }

    public bool DestroyPath(int pathId)
    {
        return _npcs.DestroyPath(pathId);
    }

    public bool AddPointToPath(int pathId, Vector3 position, float stopRange)
    {
        return _npcs.AddPointToPath(pathId, position, stopRange);
    }

    public bool IsValidPath(int pathId)
    {
        return _npcs.IsValidPath(pathId);
    }

    public int LoadRecord(string filePath)
    {
        ArgumentNullException.ThrowIfNull(filePath);
        return _npcs.LoadRecord(filePath);
    }

    public bool UnloadRecord(int recordId)
    {
        return _npcs.UnloadRecord(recordId);
    }
}
