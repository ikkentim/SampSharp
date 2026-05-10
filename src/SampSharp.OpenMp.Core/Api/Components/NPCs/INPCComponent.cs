using System.Numerics;
using SampSharp.OpenMp.Core.Std;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="INPCComponent" /> interface.
/// </summary>
[OpenMpApi(typeof(IPool<INPC>), typeof(INetworkComponent))]
public readonly partial struct INPCComponent
{
    /// <inheritdoc />
    public static UID ComponentId => new(0x3D0E59E87F4E90BC);
    
    /// <summary>
    /// Gets the event dispatcher for NPC-related events.
    /// </summary>
    public partial IEventDispatcher<INPCEventHandler> GetEventDispatcher();

    /// <summary>
    /// Creates a controllable NPC with the given name. The NPC must be spawned via
    /// <see cref="INPC.Spawn" /> separately before it appears in the world.
    /// </summary>
    /// <param name="name">The NPC name (must follow the same rules as normal player names).</param>
    public partial INPC Create(string name);

    /// <summary>
    /// Destroys the given NPC. Required because NPC removal is more than a pool release.
    /// </summary>
    public partial void Destroy(INPC npc);

    /// <summary>Creates a new (empty) path container.</summary>
    public partial int CreatePath();

    /// <summary>Destroys a previously created path container.</summary>
    public partial bool DestroyPath(int pathId);

    /// <summary>Destroys all path containers.</summary>
    public partial void DestroyAllPaths();

    /// <summary>Gets the total number of active path containers.</summary>
    public partial Size GetPathCount();

    /// <summary>Adds a point with stop range to a path container.</summary>
    public partial bool AddPointToPath(int pathId, Vector3 position, float stopRange);

    /// <summary>Removes a waypoint at the specified index from a path container.</summary>
    public partial bool RemovePointFromPath(int pathId, Size pointIndex);

    /// <summary>Removes all waypoints from a path container.</summary>
    public partial bool ClearPath(int pathId);

    /// <summary>Gets the number of waypoints in a path container.</summary>
    public partial Size GetPathPointCount(int pathId);

    /// <summary>Gets the position and stop range of a waypoint at the specified index in a path.</summary>
    public partial bool GetPathPoint(int pathId, Size pointIndex, out Vector3 position, out float stopRange);

    /// <summary>Checks whether any waypoint in the path is within the given radius of the position.</summary>
    public partial bool HasPathPointInRange(int pathId, Vector3 position, float radius);

    /// <summary>Checks if a path id is valid.</summary>
    public partial bool IsValidPath(int pathId);

    /// <summary>Loads a record file for playback. Returns the record id (or -1 on failure).</summary>
    public partial int LoadRecord(string filePath);

    /// <summary>Unloads a previously loaded record.</summary>
    public partial bool UnloadRecord(int recordId);

    /// <summary>Checks if a record ID is valid.</summary>
    public partial bool IsValidRecord(int recordId);

    /// <summary>Gets the total number of loaded records.</summary>
    public partial Size GetRecordCount();

    /// <summary>Unloads all loaded records.</summary>
    public partial void UnloadAllRecords();

    /// <summary>Opens a node file for NPC path navigation.</summary>
    public partial bool OpenNode(int nodeId);

    /// <summary>Closes a previously opened node file.</summary>
    public partial void CloseNode(int nodeId);

    /// <summary>Checks whether the specified node file is currently open.</summary>
    public partial bool IsNodeOpen(int nodeId);

    /// <summary>Gets the type of a node.</summary>
    public partial byte GetNodeType(int nodeId);

    /// <summary>Sets the current point within a node.</summary>
    public partial bool SetNodePoint(int nodeId, ushort pointId);

    /// <summary>Gets the position of the current point in a node.</summary>
    public partial bool GetNodePointPosition(int nodeId, out Vector3 position);

    /// <summary>Gets the total number of points in a node.</summary>
    public partial int GetNodePointCount(int nodeId);

    /// <summary>Gets information about vehicle nodes, pedestrian nodes, and navigation nodes in a node file.</summary>
    public partial bool GetNodeInfo(int nodeId, out uint vehicleNodes, out uint pedNodes, out uint naviNodes);
}