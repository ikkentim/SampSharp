using System.Numerics;

namespace SampSharp.Entities.SAMP;

/// <summary>Provides access to global NPC infrastructure: path containers and movement recordings.</summary>
public interface INpcService
{
    /// <summary>Creates a new (empty) NPC path container.</summary>
    /// <returns>The newly allocated path ID.</returns>
    int CreatePath();

    /// <summary>Destroys a previously created path container.</summary>
    /// <param name="pathId">The path ID.</param>
    /// <returns><see langword="true" /> on success.</returns>
    bool DestroyPath(int pathId);

    /// <summary>Appends a waypoint to an existing path container.</summary>
    /// <param name="pathId">The path ID.</param>
    /// <param name="position">The waypoint position.</param>
    /// <param name="stopRange">The radius within which the NPC is considered to have reached this waypoint.</param>
    /// <returns><see langword="true" /> on success.</returns>
    bool AddPointToPath(int pathId, Vector3 position, float stopRange);

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
}
