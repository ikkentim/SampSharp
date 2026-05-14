using System.Numerics;
using SampSharp.OpenMp.Core.Std;
using INPCComponent = SampSharp.OpenMp.Core.Api.INPCComponent;

namespace SampSharp.Entities.SAMP;

internal sealed class NpcService(SampSharpEnvironment environment) : INpcService
{
    private readonly SafeComponentHandle<INPCComponent> _npcs = environment.SafeComponentHandleProvider.Get<INPCComponent>();

    private INPCComponent Npcs => _npcs;

    public int CreatePath()
    {
        return Npcs.CreatePath();
    }

    public bool DestroyPath(int pathId)
    {
        return Npcs.DestroyPath(pathId);
    }

    public void DestroyAllPaths()
    {
        Npcs.DestroyAllPaths();
    }

    public Size GetPathCount()
    {
        return Npcs.GetPathCount();
    }

    public bool AddPointToPath(int pathId, Vector3 position, float stopRange)
    {
        return Npcs.AddPointToPath(pathId, position, stopRange);
    }

    public bool RemovePointFromPath(int pathId, Size pointIndex)
    {
        return Npcs.RemovePointFromPath(pathId, pointIndex);
    }

    public bool ClearPath(int pathId)
    {
        return Npcs.ClearPath(pathId);
    }

    public Size GetPathPointCount(int pathId)
    {
        return Npcs.GetPathPointCount(pathId);
    }

    public bool GetPathPoint(int pathId, Size pointIndex, out Vector3 position, out float stopRange)
    {
        return Npcs.GetPathPoint(pathId, pointIndex, out position, out stopRange);
    }

    public bool HasPathPointInRange(int pathId, Vector3 position, float radius)
    {
        return Npcs.HasPathPointInRange(pathId, position, radius);
    }

    public bool IsValidPath(int pathId)
    {
        return Npcs.IsValidPath(pathId);
    }

    public int LoadRecord(string filePath)
    {
        ArgumentNullException.ThrowIfNull(filePath);
        return Npcs.LoadRecord(filePath);
    }

    public bool UnloadRecord(int recordId)
    {
        return Npcs.UnloadRecord(recordId);
    }

    public bool IsValidRecord(int recordId)
    {
        return Npcs.IsValidRecord(recordId);
    }

    public Size GetRecordCount()
    {
        return Npcs.GetRecordCount();
    }

    public void UnloadAllRecords()
    {
        Npcs.UnloadAllRecords();
    }

    public bool OpenNode(int nodeId)
    {
        return Npcs.OpenNode(nodeId);
    }

    public void CloseNode(int nodeId)
    {
        Npcs.CloseNode(nodeId);
    }

    public bool IsNodeOpen(int nodeId)
    {
        return Npcs.IsNodeOpen(nodeId);
    }

    public byte GetNodeType(int nodeId)
    {
        return Npcs.GetNodeType(nodeId);
    }

    public bool SetNodePoint(int nodeId, ushort pointId)
    {
        return Npcs.SetNodePoint(nodeId, pointId);
    }

    public bool GetNodePointPosition(int nodeId, out Vector3 position)
    {
        return Npcs.GetNodePointPosition(nodeId, out position);
    }

    public int GetNodePointCount(int nodeId)
    {
        return Npcs.GetNodePointCount(nodeId);
    }

    public bool GetNodeInfo(int nodeId, out uint vehicleNodes, out uint pedNodes, out uint naviNodes)
    {
        return Npcs.GetNodeInfo(nodeId, out vehicleNodes, out pedNodes, out naviNodes);
    }
}
