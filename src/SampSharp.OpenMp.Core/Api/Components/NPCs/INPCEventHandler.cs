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

    /// <summary>Fired when the NPC dies.</summary>
    void OnNPCDeath(INPC npc, IPlayer killer, int reason);
}