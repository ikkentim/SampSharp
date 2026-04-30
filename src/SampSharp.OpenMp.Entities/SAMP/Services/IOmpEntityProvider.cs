using SampSharp.OpenMp.Core.Api;
using INPC = SampSharp.OpenMp.Core.Api.INPC;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Provides methods for getting ECS entities/components for open.mp entities. For entities created through
/// <c>SampSharp.Entities</c>, the existing entities and components are returned. For foreign entities (entities
/// created
/// through other scripts or open.mp components) new ECS entities and components are created and returned where
/// possible.
/// </summary>
public interface IOmpEntityProvider
{
    /// <summary>
    /// Gets the entity for the specified actor.
    /// </summary>
    /// <param name="actor">The actor to get the entity for.</param>
    /// <returns>The actor entity.</returns>
    EntityId GetEntity(IActor actor);

    /// <summary>
    /// Gets the entity for the specified NPC.
    /// </summary>
    /// <param name="npc">The NPC to get the entity for.</param>
    /// <returns>The NPC entity.</returns>
    EntityId GetEntity(INPC npc);

    /// <summary>
    /// Gets the entity for the specified gang zone.
    /// </summary>
    /// <param name="gangZone">The gang zone to get the entity for.</param>
    /// <returns>The gang zone entity.</returns>
    EntityId GetEntity(IGangZone gangZone);

    /// <summary>
    /// Gets the entity for the specified menu.
    /// </summary>
    /// <param name="menu">The menu to get the entity for.</param>
    /// <returns>The menu entity.</returns>
    EntityId GetEntity(IMenu menu);

    /// <summary>
    /// Gets the entity for the specified object.
    /// </summary>
    /// <param name="object">The object to get the entity for.</param>
    /// <returns>The object entity.</returns>
    EntityId GetEntity(IObject @object);

    /// <summary>
    /// Gets the entity for the specified pickup.
    /// </summary>
    /// <param name="pickup">The pickup to get the entity for.</param>
    /// <returns>The pickup entity.</returns>
    EntityId GetEntity(IPickup pickup);

    /// <summary>
    /// Gets the entity for the specified player.
    /// </summary>
    /// <param name="player">The player to get the entity for.</param>
    /// <returns>The player entity.</returns>
    EntityId GetEntity(IPlayer player);

    /// <summary>
    /// Gets the entity for the specified player object.
    /// </summary>
    /// <param name="player">The owner of the player object.</param>
    /// <param name="playerObject">The player object to get the entity for.</param>
    /// <returns>The player object entity.</returns>
    EntityId GetEntity(IPlayerObject playerObject, IPlayer player = default);

    /// <summary>
    /// Gets the entity for the specified player text draw.
    /// </summary>
    /// <param name="player">The owner of the player text draw.</param>
    /// <param name="playerTextDraw">The player text draw to get the entity for.</param>
    /// <returns>The player text draw entity.</returns>
    EntityId GetEntity(IPlayerTextDraw playerTextDraw, IPlayer player = default);

    /// <summary>
    /// Gets the entity for the specified player text label.
    /// </summary>
    /// <param name="player">The owner of the player text label.</param>
    /// <param name="playerTextLabel">The player text label to get the entity for.</param>
    /// <returns>The player text label entity.</returns>
    EntityId GetEntity(IPlayerTextLabel playerTextLabel, IPlayer player = default);

    /// <summary>
    /// Gets the entity for the specified text draw.
    /// </summary>
    /// <param name="textDraw">The text draw to get the entity for.</param>
    /// <returns>The text draw entity.</returns>
    EntityId GetEntity(ITextDraw textDraw);

    /// <summary>
    /// Gets the entity for the specified text label.
    /// </summary>
    /// <param name="textLabel">The text label to get the entity for.</param>
    /// <returns>The text label entity.</returns>
    EntityId GetEntity(ITextLabel textLabel);

    /// <summary>
    /// Gets the entity for the specified vehicle.
    /// </summary>
    /// <param name="vehicle">The vehicle to get the entity for.</param>
    /// <returns>The vehicle entity.</returns>
    EntityId GetEntity(IVehicle vehicle);

    /// <summary>
    /// Gets the component for the specified actor.
    /// </summary>
    /// <param name="actor">The actor to get the component for.</param>
    /// <returns>The actor component.</returns>
    Actor? GetComponent(IActor actor);

    /// <summary>
    /// Gets the component for the specified NPC.
    /// </summary>
    /// <param name="npc">The NPC to get the component for.</param>
    /// <returns>The NPC component.</returns>
    Npc? GetComponent(INPC npc);

    /// <summary>
    /// Gets the component for the specified gang zone.
    /// </summary>
    /// <param name="gangZone">The gang zone to get the component for.</param>
    /// <returns>The gang zone component.</returns>
    GangZone? GetComponent(IGangZone gangZone);

    /// <summary>
    /// Gets the component for the specified menu.
    /// </summary>
    /// <param name="menu">The menu to get the component for.</param>
    /// <returns>The menu component.</returns>
    Menu? GetComponent(IMenu menu);

    /// <summary>
    /// Gets the component for the specified object.
    /// </summary>
    /// <param name="object">The object to get the component for.</param>
    /// <returns>The object component.</returns>
    GlobalObject? GetComponent(IObject @object);

    /// <summary>
    /// Gets the component for the specified pickup.
    /// </summary>
    /// <param name="pickup">The pickup to get the component for.</param>
    /// <returns>The pickup component.</returns>
    Pickup? GetComponent(IPickup pickup);

    /// <summary>
    /// Gets the component for the specified player.
    /// </summary>
    /// <param name="player">The player to get the component for.</param>
    /// <returns>The player component.</returns>
    Player? GetComponent(IPlayer player);

    /// <summary>
    /// Gets the component for the specified player object.
    /// </summary>
    /// <param name="player">The owner of the player object.</param>
    /// <param name="playerObject">The player object to get the component for.</param>
    /// <returns>The player object component.</returns>
    PlayerObject? GetComponent(IPlayerObject playerObject, IPlayer player = default);

    /// <summary>
    /// Gets the component for the specified player text draw.
    /// </summary>
    /// <param name="player">The owner of the player text draw.</param>
    /// <param name="playerTextDraw">The player text draw to get the component for.</param>
    /// <returns>The player text draw component.</returns>
    PlayerTextDraw? GetComponent(IPlayerTextDraw playerTextDraw, IPlayer player = default);

    /// <summary>
    /// Gets the component for the specified player text label.
    /// </summary>
    /// <param name="player">The owner of the player text label.</param>
    /// <param name="playerTextLabel">The player text label to get the component for.</param>
    /// <returns>The player text label component.</returns>
    PlayerTextLabel? GetComponent(IPlayerTextLabel playerTextLabel, IPlayer player = default);

    /// <summary>
    /// Gets the component for the specified text draw.
    /// </summary>
    /// <param name="textDraw">The text draw to get the component for.</param>
    /// <returns>The text draw component.</returns>
    TextDraw? GetComponent(ITextDraw textDraw);

    /// <summary>
    /// Gets the component for the specified text label.
    /// </summary>
    /// <param name="textLabel">The text label to get the component for.</param>
    /// <returns>The text label component.</returns>
    TextLabel? GetComponent(ITextLabel textLabel);

    /// <summary>
    /// Gets the component for the specified vehicle.
    /// </summary>
    /// <param name="vehicle">The vehicle to get the component for.</param>
    /// <returns>The vehicle component.</returns>
    Vehicle? GetComponent(IVehicle vehicle);

    /// <summary>
    /// Gets the actor with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the actor.</param>
    /// <returns>The actor with the specified identifier or <see langword="null" /> if no actor could be found.</returns>
    Actor? GetActor(int id);

    /// <summary>
    /// Gets the NPC with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the NPC.</param>
    /// <returns>The NPC with the specified identifier or <see langword="null" /> if no NPC could be found.</returns>
    Npc? GetNpc(int id);

    /// <summary>
    /// Creates a new server-controlled NPC with the given name and returns its ECS component.
    /// You still need to call <see cref="Npc.Spawn" /> for it to appear in the world.
    /// </summary>
    /// <param name="name">The NPC name (must follow the same rules as normal player names).</param>
    /// <returns>The created NPC component, or <see langword="null" /> if creation failed.</returns>
    Npc? CreateNpc(string name);

    /// <summary>
    /// Gets the gang zone with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the gang zone.</param>
    /// <returns>The gang zone with the specified identifier or <see langword="null" /> if no gang zone could be found.</returns>
    GangZone? GetGangZone(int id);

    /// <summary>
    /// Gets the menu with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the menu.</param>
    /// <returns>The menu with the specified identifier or <see langword="null" /> if no menu could be found.</returns>
    Menu? GetMenu(int id);

    /// <summary>
    /// Gets the object with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the object.</param>
    /// <returns>The object with the specified identifier or <see langword="null" /> if no object could be found.</returns>
    GlobalObject? GetObject(int id);

    /// <summary>
    /// Gets the pickup with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the pickup.</param>
    /// <returns>The pickup with the specified identifier or <see langword="null" /> if no pickup could be found.</returns>
    Pickup? GetPickup(int id);

    /// <summary>
    /// Gets the player with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the player.</param>
    /// <returns>The player with the specified identifier or <see langword="null" /> if no player could be found.</returns>
    Player? GetPlayer(int id);

    /// <summary>
    /// Gets the player object with the specified identifier.
    /// </summary>
    /// <param name="player">The owner of the player object.</param>
    /// <param name="id">The identifier of the player object.</param>
    /// <returns>The player object with the specified identifier or <see langword="null" /> if no player object could be found.</returns>
    PlayerObject? GetPlayerObject(IPlayer player, int id);

    /// <summary>
    /// Gets the actor with the specified identifier.
    /// </summary>
    /// <param name="player">The owner of the player text draw.</param>
    /// <param name="id">The identifier of the actor.</param>
    /// <returns>The actor with the specified identifier or <see langword="null" /> if no actor could be found.</returns>
    PlayerTextDraw? GetPlayerTextDraw(IPlayer player, int id);

    /// <summary>
    /// Gets the actor with the specified identifier.
    /// </summary>
    /// <param name="player">The owner of the player text label.</param>
    /// <param name="id">The identifier of the actor.</param>
    /// <returns>The actor with the specified identifier or <see langword="null" /> if no actor could be found.</returns>
    PlayerTextLabel? GetPlayerTextLabel(IPlayer player, int id);

    /// <summary>
    /// Gets the actor with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the actor.</param>
    /// <returns>The actor with the specified identifier or <see langword="null" /> if no actor could be found.</returns>
    TextDraw? GetTextDraw(int id);

    /// <summary>
    /// Gets the actor with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the actor.</param>
    /// <returns>The actor with the specified identifier or <see langword="null" /> if no actor could be found.</returns>
    TextLabel? GetTextLabel(int id);

    /// <summary>
    /// Gets the actor with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the actor.</param>
    /// <returns>The actor with the specified identifier or <see langword="null" /> if no actor could be found.</returns>
    Vehicle? GetVehicle(int id);
}