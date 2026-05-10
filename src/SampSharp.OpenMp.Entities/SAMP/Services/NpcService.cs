using System.Numerics;
using SampSharp.OpenMp.Core.Std;
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

    public void DestroyAllPaths()
    {
        _npcs.DestroyAllPaths();
    }

    public Size GetPathCount()
    {
        return _npcs.GetPathCount();
    }

    public bool AddPointToPath(int pathId, Vector3 position, float stopRange)
    {
        return _npcs.AddPointToPath(pathId, position, stopRange);
    }

    public bool RemovePointFromPath(int pathId, Size pointIndex)
    {
        return _npcs.RemovePointFromPath(pathId, pointIndex);
    }

    public bool ClearPath(int pathId)
    {
        return _npcs.ClearPath(pathId);
    }

    public Size GetPathPointCount(int pathId)
    {
        return _npcs.GetPathPointCount(pathId);
    }

    public bool GetPathPoint(int pathId, Size pointIndex, out Vector3 position, out float stopRange)
    {
        return _npcs.GetPathPoint(pathId, pointIndex, out position, out stopRange);
    }

    public bool HasPathPointInRange(int pathId, Vector3 position, float radius)
    {
        return _npcs.HasPathPointInRange(pathId, position, radius);
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

    public bool IsValidRecord(int recordId)
    {
        return _npcs.IsValidRecord(recordId);
    }

    public Size GetRecordCount()
    {
        return _npcs.GetRecordCount();
    }

    public void UnloadAllRecords()
    {
        _npcs.UnloadAllRecords();
    }

    public bool OpenNode(int nodeId)
    {
        return _npcs.OpenNode(nodeId);
    }

    public void CloseNode(int nodeId)
    {
        _npcs.CloseNode(nodeId);
    }

    public bool IsNodeOpen(int nodeId)
    {
        return _npcs.IsNodeOpen(nodeId);
    }

    public byte GetNodeType(int nodeId)
    {
        return _npcs.GetNodeType(nodeId);
    }

    public bool SetNodePoint(int nodeId, ushort pointId)
    {
        return _npcs.SetNodePoint(nodeId, pointId);
    }

    public bool GetNodePointPosition(int nodeId, out Vector3 position)
    {
        return _npcs.GetNodePointPosition(nodeId, out position);
    }

    public int GetNodePointCount(int nodeId)
    {
        return _npcs.GetNodePointCount(nodeId);
    }

    public bool GetNodeInfo(int nodeId, out uint vehicleNodes, out uint pedNodes, out uint naviNodes)
    {
        return _npcs.GetNodeInfo(nodeId, out vehicleNodes, out pedNodes, out naviNodes);
    }
}
