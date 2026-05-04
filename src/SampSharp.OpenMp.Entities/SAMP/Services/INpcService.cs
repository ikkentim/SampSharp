using System.Numerics;
using SampSharp.OpenMp.Core.Std;

namespace SampSharp.Entities.SAMP;

/// <summary>Provides access to global NPC infrastructure: path containers, movement recordings, and node navigation.</summary>
public interface INpcService
{
    /// <summary>Creates a new (empty) NPC path container.</summary>
    /// <returns>The newly allocated path ID.</returns>
    int CreatePath();

    /// <summary>Destroys a previously created path container.</summary>
    /// <param name="pathId">The path ID.</param>
    /// <returns><see langword="true" /> on success.</returns>
    bool DestroyPath(int pathId);

    /// <summary>Destroys all path containers.</summary>
    void DestroyAllPaths();

    /// <summary>Gets the total number of active path containers.</summary>
    Size GetPathCount();

    /// <summary>Appends a waypoint to an existing path container.</summary>
    /// <param name="pathId">The path ID.</param>
    /// <param name="position">The waypoint position.</param>
    /// <param name="stopRange">The radius within which the NPC is considered to have reached this waypoint.</param>
    /// <returns><see langword="true" /> on success.</returns>
    bool AddPointToPath(int pathId, Vector3 position, float stopRange);

    /// <summary>Removes the waypoint at the specified index from a path container.</summary>
    /// <param name="pathId">The path ID.</param>
    /// <param name="pointIndex">The zero-based index of the waypoint to remove.</param>
    /// <returns><see langword="true" /> on success.</returns>
    bool RemovePointFromPath(int pathId, Size pointIndex);

    /// <summary>Removes all waypoints from a path container.</summary>
    /// <param name="pathId">The path ID.</param>
    /// <returns><see langword="true" /> on success.</returns>
    bool ClearPath(int pathId);

    /// <summary>Gets the number of waypoints in a path container.</summary>
    /// <param name="pathId">The path ID.</param>
    Size GetPathPointCount(int pathId);

    /// <summary>Gets the position and stop range of a specific waypoint in a path.</summary>
    /// <param name="pathId">The path ID.</param>
    /// <param name="pointIndex">The zero-based index of the waypoint.</param>
    /// <param name="position">The position of the waypoint.</param>
    /// <param name="stopRange">The stop range of the waypoint.</param>
    /// <returns><see langword="true" /> on success.</returns>
    bool GetPathPoint(int pathId, Size pointIndex, out Vector3 position, out float stopRange);

    /// <summary>Checks whether any waypoint in the path is within the given radius of the position.</summary>
    /// <param name="pathId">The path ID.</param>
    /// <param name="position">The reference position.</param>
    /// <param name="radius">The search radius.</param>
    /// <returns><see langword="true" /> if a matching waypoint exists.</returns>
    bool HasPathPointInRange(int pathId, Vector3 position, float radius);

    /// <summary>Checks whether the given path ID refers to a live path container.</summary>
    /// <param name="pathId">The path ID.</param>
    /// <returns><see langword="true" /> if valid.</returns>
    bool IsValidPath(int pathId);

    /// <summary>Loads a recorded movement file (<c>.rec</c>) for playback by NPCs.</summary>
    /// <param name="filePath">The path to the recording file (relative to scriptfiles).</param>
    /// <returns>The record ID, or <c>-1</c> on failure.</returns>
    int LoadRecord(string filePath);

    /// <summary>Unloads a previously loaded recording.</summary>
    /// <param name="recordId">The record ID returned by <see cref="LoadRecord" />.</param>
    /// <returns><see langword="true" /> on success.</returns>
    bool UnloadRecord(int recordId);

    /// <summary>Checks whether the given record ID is valid.</summary>
    /// <param name="recordId">The record ID.</param>
    /// <returns><see langword="true" /> if valid.</returns>
    bool IsValidRecord(int recordId);

    /// <summary>Gets the total number of loaded recordings.</summary>
    Size GetRecordCount();

    /// <summary>Unloads all loaded recordings.</summary>
    void UnloadAllRecords();

    /// <summary>Opens a node file for NPC path navigation.</summary>
    /// <param name="nodeId">The node ID.</param>
    /// <returns><see langword="true" /> on success.</returns>
    bool OpenNode(int nodeId);

    /// <summary>Closes a previously opened node file.</summary>
    /// <param name="nodeId">The node ID.</param>
    void CloseNode(int nodeId);

    /// <summary>Checks whether the specified node file is currently open.</summary>
    /// <param name="nodeId">The node ID.</param>
    /// <returns><see langword="true" /> if open.</returns>
    bool IsNodeOpen(int nodeId);

    /// <summary>Gets the type of a node.</summary>
    /// <param name="nodeId">The node ID.</param>
    byte GetNodeType(int nodeId);

    /// <summary>Sets the current point within a node.</summary>
    /// <param name="nodeId">The node ID.</param>
    /// <param name="pointId">The point ID to set.</param>
    /// <returns><see langword="true" /> on success.</returns>
    bool SetNodePoint(int nodeId, ushort pointId);

    /// <summary>Gets the position of the current point in a node.</summary>
    /// <param name="nodeId">The node ID.</param>
    /// <param name="position">The position of the current point.</param>
    /// <returns><see langword="true" /> on success.</returns>
    bool GetNodePointPosition(int nodeId, out Vector3 position);

    /// <summary>Gets the total number of points in a node.</summary>
    /// <param name="nodeId">The node ID.</param>
    int GetNodePointCount(int nodeId);

    /// <summary>Gets information about vehicle nodes, pedestrian nodes, and navigation nodes in a node file.</summary>
    /// <param name="nodeId">The node ID.</param>
    /// <param name="vehicleNodes">The number of vehicle nodes.</param>
    /// <param name="pedNodes">The number of pedestrian nodes.</param>
    /// <param name="naviNodes">The number of navigation nodes.</param>
    /// <returns><see langword="true" /> on success.</returns>
    bool GetNodeInfo(int nodeId, out uint vehicleNodes, out uint pedNodes, out uint naviNodes);
}
