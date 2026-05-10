namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="INPCComponent.GetEventDispatcher" />.
/// </summary>
[OpenMpEventHandler]
public partial interface INPCEventHandler
{
    /// <summary>Fired when an NPC finishes a Move() command (reaches the target position).</summary>
    void OnNPCFinishMove(INPC npc);

    /// <summary>Fired right after an NPC is created via <see cref="INPCComponent.Create" />.</summary>
    void OnNPCCreate(INPC npc);

    /// <summary>Fired when the NPC is destroyed.</summary>
    void OnNPCDestroy(INPC npc);

    /// <summary>Fired after the NPC spawns into the world.</summary>
    void OnNPCSpawn(INPC npc);

    /// <summary>Fired after the NPC respawns.</summary>
    void OnNPCRespawn(INPC npc);

    /// <summary>Fired when the NPC's weapon state changes.</summary>
    void OnNPCWeaponStateChange(INPC npc, PlayerWeaponState newState, PlayerWeaponState oldState);

    /// <summary>Fired when the NPC takes damage from a player. Return <see langword="false" /> to cancel the damage.</summary>
    bool OnNPCTakeDamage(INPC npc, IPlayer damager, float damage, byte weapon, BodyPart bodyPart);

    /// <summary>Fired when the NPC gives damage to a player. Return <see langword="false" /> to cancel the damage.</summary>
    bool OnNPCGiveDamage(INPC npc, IPlayer damaged, float damage, byte weapon, BodyPart bodyPart);

    /// <summary>Fired when the NPC dies.</summary>
    void OnNPCDeath(INPC npc, IPlayer killer, int reason);

    /// <summary>Fired when the NPC fires a shot that misses all targets. Return <see langword="false" /> to cancel.</summary>
    bool OnNPCShotMissed(INPC npc, ref PlayerBulletData bulletData);

    /// <summary>Fired when the NPC shoots and hits a player. Return <see langword="false" /> to cancel.</summary>
    bool OnNPCShotPlayer(INPC npc, IPlayer target, ref PlayerBulletData bulletData);

    /// <summary>Fired when the NPC shoots and hits another NPC. Return <see langword="false" /> to cancel.</summary>
    bool OnNPCShotNPC(INPC npc, INPC target, ref PlayerBulletData bulletData);

    /// <summary>Fired when the NPC shoots and hits a vehicle. Return <see langword="false" /> to cancel.</summary>
    bool OnNPCShotVehicle(INPC npc, IVehicle target, ref PlayerBulletData bulletData);

    /// <summary>Fired when the NPC shoots and hits a world object. Return <see langword="false" /> to cancel.</summary>
    bool OnNPCShotObject(INPC npc, IObject target, ref PlayerBulletData bulletData);

    /// <summary>Fired when the NPC shoots and hits a player-object. Return <see langword="false" /> to cancel.</summary>
    bool OnNPCShotPlayerObject(INPC npc, IPlayerObject target, ref PlayerBulletData bulletData);

    /// <summary>Fired when the NPC starts playing a recording.</summary>
    void OnNPCPlaybackStart(INPC npc, int recordId);

    /// <summary>Fired when the NPC finishes playing a recording.</summary>
    void OnNPCPlaybackEnd(INPC npc, int recordId);

    /// <summary>Fired when the NPC finishes a waypoint in node-based movement.</summary>
    void OnNPCFinishNodePoint(INPC npc, int nodeId, ushort pointId);

    /// <summary>Fired when the NPC finishes traversing all points in a node.</summary>
    void OnNPCFinishNode(INPC npc, int nodeId);

    /// <summary>Fired when the NPC changes to a different node. Return <see langword="false" /> to cancel the change.</summary>
    bool OnNPCChangeNode(INPC npc, int newNodeId, int oldNodeId);

    /// <summary>Fired when the NPC reaches a waypoint in path-based movement.</summary>
    void OnNPCFinishMovePathPoint(INPC npc, int pathId, int pointId);

    /// <summary>Fired when the NPC finishes traversing all waypoints in a path.</summary>
    void OnNPCFinishMovePath(INPC npc, int pathId);
}