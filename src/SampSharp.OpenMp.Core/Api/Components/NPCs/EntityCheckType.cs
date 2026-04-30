namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Bitfield flags for NPC AimAt / Shoot 'in-between' entity checks.
/// </summary>
[Flags]
public enum EntityCheckType : byte
{
    // TODO validate documentation.

    /// <summary>
    /// No checks.
    /// </summary>
    None = 0,
    /// <summary>
    /// Check for players.
    /// </summary>
    Player = 1,
    /// <summary>
    /// Check for NPCs.
    /// </summary>
    NPC = 2,
    /// <summary>
    /// Check for actors.
    /// </summary>
    Actor = 4,
    /// <summary>
    /// Check for vehicles.
    /// </summary>
    Vehicle = 8,
    /// <summary>
    /// Check for objects.
    /// </summary>
    Object = 16,
    /// <summary>
    /// Projection origins check.
    /// </summary>
    ProjectOrig = 32,
    /// <summary>
    /// Projection target check.
    /// </summary>
    ProjectTarg = 64,
    /// <summary>
    /// Check for world geometry.
    /// </summary>
    Map = 128,
    /// <summary>
    /// Run all checks.
    /// </summary>
    All = 255
}