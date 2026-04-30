namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Movement type for an NPC.
/// </summary>
public enum NPCMoveType
{
    /// <summary>Not moving.</summary>
    None = 0,

    /// <summary>Walk speed.</summary>
    Walk = 1,

    /// <summary>Jog speed.</summary>
    Jog = 2,

    /// <summary>Sprint speed.</summary>
    Sprint = 3,

    /// <summary>Drive (in vehicle).</summary>
    Drive = 4,

    /// <summary>Pick a sensible default automatically.</summary>
    Auto = 5
}