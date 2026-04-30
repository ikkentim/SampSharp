using System.Numerics;
using System.Runtime.InteropServices;

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

    /// <summary>Adds a point with stop range to a path container.</summary>
    public partial bool AddPointToPath(int pathId, Vector3 position, float stopRange);

    /// <summary>Checks if a path id is valid.</summary>
    public partial bool IsValidPath(int pathId);

    /// <summary>Loads a record file for playback. Returns the record id (or -1 on failure).</summary>
    public partial int LoadRecord(string filePath);

    /// <summary>Unloads a previously loaded record.</summary>
    public partial bool UnloadRecord(int recordId);
}