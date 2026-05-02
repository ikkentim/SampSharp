using System.Numerics;
using JetBrains.Annotations;

namespace SampSharp.Entities.SAMP;


/// <summary>
/// Provides functionality for adding entities to and controlling the SA:MP world.
/// </summary>
public interface IWorldService
{
    /// <summary>
    /// Gets or sets the gravity.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if value is not between -50.0 and 50.0.</exception>
    float Gravity { get; set; }

    /// <summary>
    /// Creates a new actor in the world.
    /// </summary>
    /// <param name="modelId">The model identifier.</param>
    /// <param name="position">The position of the actor.</param>
    /// <param name="rotation">The rotation of the actor.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns>The actor component of the newly created entity.</returns>
    Actor CreateActor(int modelId, Vector3 position, float rotation, EntityId parent = default);

    /// <summary>
    /// Creates a vehicle in the world.
    /// </summary>
    /// <param name="type">The model for the vehicle.</param>
    /// <param name="position">The coordinates for the vehicle.</param>
    /// <param name="rotation">The facing angle for the vehicle.</param>
    /// <param name="color1">The primary color ID.</param>
    /// <param name="color2">The secondary color ID.</param>
    /// <param name="respawnDelay">
    /// The delay until the car is respawned without a driver in seconds. Using -1 will
    /// prevent the vehicle from respawning.
    /// </param>
    /// <param name="addSiren">If true, enables the vehicle to have a siren, providing the vehicle has a horn.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns> The created vehicle.</returns>
    Vehicle CreateVehicle(VehicleModelType type, Vector3 position, float rotation, int color1, int color2, int respawnDelay = -1, bool addSiren = false,
        EntityId parent = default);

    /// <summary>
    /// Creates a static vehicle in the world.
    /// </summary>
    /// <param name="type">The model for the vehicle.</param>
    /// <param name="position">The coordinates for the vehicle.</param>
    /// <param name="rotation">The facing angle for the vehicle.</param>
    /// <param name="color1">The primary color ID.</param>
    /// <param name="color2">The secondary color ID.</param>
    /// <param name="respawnDelay">
    /// The delay until the car is respawned without a driver in seconds. Using -1 will
    /// prevent the vehicle from respawning.
    /// </param>
    /// <param name="addSiren">If true, enables the vehicle to have a siren, providing the vehicle has a horn.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns> The created vehicle.</returns>
    Vehicle CreateStaticVehicle(VehicleModelType type, Vector3 position, float rotation, int color1, int color2, int respawnDelay = -1, bool addSiren = false,
        EntityId parent = default);

    /// <summary>
    /// Creates a gang zone in the world.
    /// </summary>
    /// <param name="minX">The minimum x.</param>
    /// <param name="minY">The minimum y.</param>
    /// <param name="maxX">The maximum x.</param>
    /// <param name="maxY">The maximum y.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns>The created gang zone.</returns>
    [Obsolete("Deprecated. Use CreateGangZone(Vector2, Vector2, EntityId) instead.")]
    GangZone CreateGangZone(float minX, float minY, float maxX, float maxY, EntityId parent = default);

    /// <summary>
    /// Creates a gang zone in the world.
    /// </summary>
    /// <param name="min">The minimum position.</param>
    /// <param name="max">The maximum position.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns>The created gang zone.</returns>
    GangZone CreateGangZone(Vector2 min, Vector2 max, EntityId parent = default);

    /// <summary>
    /// Creates a gang zone logically scoped to a single player. The gang zone is created in the global pool and
    /// bound to the specified <paramref name="owner" /> via <c>SetLegacyPlayer</c>. Use
    /// <see cref="BaseGangZone.Show(Player)" /> / <see cref="BaseGangZone.Hide(Player)" /> to control per-player visibility.
    /// </summary>
    /// <param name="owner">The player that owns this gang zone.</param>
    /// <param name="min">The minimum position.</param>
    /// <param name="max">The maximum position.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns>The created player gang zone.</returns>
    PlayerGangZone CreatePlayerGangZone(Player owner, Vector2 min, Vector2 max, EntityId parent = default);

    /// <summary>
    /// Enables or disables enter/leave checking for the specified <paramref name="zone" />. When enabled,
    /// <see cref="BaseGangZone.IsPlayerInside(Player)" /> returns the live containment state and the gang-zone
    /// enter/leave events fire.
    /// </summary>
    /// <param name="zone">The gang zone to configure.</param>
    /// <param name="enable"><see langword="true" /> to enable checking; <see langword="false" /> to disable.</param>
    void UseGangZoneCheck(BaseGangZone zone, bool enable);

    /// <summary>
    /// Creates a pickup in the world.
    /// </summary>
    /// <param name="model">The model of the pickup.</param>
    /// <param name="type">The pickup spawn type.</param>
    /// <param name="position">The position where the pickup should be spawned.</param>
    /// <param name="virtualWorld">The virtual world ID of the pickup. Use -1 for all worlds.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns>The created pickup.</returns>
    Pickup CreatePickup(int model, PickupType type, Vector3 position, int virtualWorld = -1, EntityId parent = default);

    /// <summary>
    /// Creates a pickup logically scoped to a single player. The pickup is created in the global pool and bound
    /// to the specified <paramref name="owner" /> via <c>SetLegacyPlayer</c>. Per-player visibility (hiding from
    /// other players) is the caller's responsibility through <see cref="BasePickup.SetHiddenForPlayer" />.
    /// </summary>
    /// <param name="owner">The player that owns this pickup.</param>
    /// <param name="model">The model of the pickup.</param>
    /// <param name="type">The pickup spawn type.</param>
    /// <param name="position">The position where the pickup should be spawned.</param>
    /// <param name="virtualWorld">The virtual world ID of the pickup. Use -1 for all worlds.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns>The created player pickup.</returns>
    PlayerPickup CreatePlayerPickup(Player owner, int model, PickupType type, Vector3 position, int virtualWorld = -1, EntityId parent = default);

    /// <summary>
    /// Adds a 'static' pickup to the world. These pickups support weapons, health, armor etc., with the ability to
    /// function without scripting them (weapons/health/armor will be given automatically).
    /// </summary>
    /// <param name="model">The model of the pickup.</param>
    /// <param name="type">The pickup spawn type.</param>
    /// <param name="position">The position where the pickup should be spawned.</param>
    /// <param name="virtualWorld">The virtual world ID of the pickup. Use -1 for all worlds.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns>The created pickup.</returns>
    Pickup CreateStaticPickup(int model, PickupType type, Vector3 position, int virtualWorld = -1, EntityId parent = default);

    /// <summary>
    /// Creates an object in the world.
    /// </summary>
    /// <param name="modelId">The model ID.</param>
    /// <param name="position">The position.</param>
    /// <param name="rotation">The rotation.</param>
    /// <param name="drawDistance">The draw distance.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns>The created object.</returns>
    GlobalObject CreateObject(int modelId, Vector3 position, Vector3 rotation, float drawDistance = 0, EntityId parent = default);

    /// <summary>
    /// Creates a player object in the world.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="modelId">The model ID.</param>
    /// <param name="position">The position.</param>
    /// <param name="rotation">The rotation.</param>
    /// <param name="drawDistance">The draw distance.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns>The created player object.</returns>
    PlayerObject CreatePlayerObject(Player player, int modelId, Vector3 position, Vector3 rotation, float drawDistance = 0, EntityId parent = default);

    /// <summary>
    /// Creates a text label in the world.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="color">The color.</param>
    /// <param name="position">The position.</param>
    /// <param name="drawDistance">The draw distance.</param>
    /// <param name="virtualWorld">The virtual world.</param>
    /// <param name="testLos">if set to <see langword="true" /> the line of sight is tested to decide whether the label is drawn.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns>The created text label.</returns>
    TextLabel CreateTextLabel(string text, Color color, Vector3 position, float drawDistance, int virtualWorld = 0, bool testLos = true,
        EntityId parent = default);

    /// <summary>
    /// Creates a player text label in the world.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="text">The text.</param>
    /// <param name="color">The color.</param>
    /// <param name="position">The position.</param>
    /// <param name="drawDistance">The draw distance.</param>
    /// <param name="testLos">if set to <see langword="true" /> the line of sight is tested to decide whether the label is drawn.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns>The created text label.</returns>
    PlayerTextLabel CreatePlayerTextLabel(Player player, string text, Color color, Vector3 position, float drawDistance, bool testLos = true, EntityId parent = default);

    /// <summary>
    /// Creates a text draw in the world.
    /// </summary>
    /// <param name="position">The position of the text draw.</param>
    /// <param name="text">The text of the text draw.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns>The created text draw.</returns>
    TextDraw CreateTextDraw(Vector2 position, string text, EntityId parent = default);

    /// <summary>
    /// Creates the player text draw in the world.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="position">The position of the text draw.</param>
    /// <param name="text">The text of the text draw.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns>The created player text draw.</returns>
    PlayerTextDraw CreatePlayerTextDraw(Player player, Vector2 position, string text, EntityId parent = default);

    /// <summary>
    /// Creates the menu in this world.
    /// </summary>
    /// <param name="title">The title of the menu.</param>
    /// <param name="position">The position of the menu.</param>
    /// <param name="col0Width">Width of the left column.</param>
    /// <param name="col1Width">Width of the right column or null if the menu should only have one column.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns>The created menu.</returns>
    Menu CreateMenu(string title, Vector2 position, float col0Width, float? col1Width = null, EntityId parent = default);

    /// <summary>
    /// Allows camera collisions with newly created objects to be disabled by default.
    /// </summary>
    /// <param name="disable">A value indicating whether camera collision with new objects should be disabled.</param>
    void SetObjectsDefaultCameraCollision(bool disable);

    /// <summary>
    /// This function sends a message to all players with a chosen color in the chat. The whole line in the chat box will be in the set color unless color
    /// embedding is used.
    /// </summary>
    /// <param name="color">The color of the message.</param>
    /// <param name="message">The text that will be displayed.</param>
    void SendClientMessage(Color color, string message);

    /// <summary>
    /// This function sends a message to all players with a chosen color in the chat. The whole line in the chat box will be in the set color unless color
    /// embedding is used.
    /// </summary>
    /// <param name="color">The color of the message.</param>
    /// <param name="messageFormat">The composite format string of the text that will be displayed (max 144 characters).</param>
    /// <param name="args">An object array that contains zero or more objects to format.</param>
    [StringFormatMethod("messageFormat")]
    void SendClientMessage(Color color, string messageFormat, params object[] args);

    /// <summary>
    /// This function sends a message to all players in white in the chat. The whole line in the chat box will be in the set color unless color embedding is
    /// used.
    /// </summary>
    /// <param name="message">The text that will be displayed.</param>
    void SendClientMessage(string message);

    /// <summary>
    /// This function sends a message to all players in white in the chat. The whole line in the chat box will be in the set color unless color embedding is
    /// used.
    /// </summary>
    /// <param name="messageFormat">The composite format string of the text that will be displayed (max 144 characters).</param>
    /// <param name="args">An object array that contains zero or more objects to format.</param>
    [StringFormatMethod("messageFormat")]
    void SendClientMessage(string messageFormat, params object[] args);

    /// <summary>
    /// Sends a message in the name the specified <paramref name="sender" /> to all players. The message will appear in the chat box and can be seen by all
    /// player. The line will start with the the sender's name in their color, followed by the <paramref name="message" /> in white.
    /// </summary>
    /// <param name="sender">The player which has sent the message.</param>
    /// <param name="message">The message that will be sent.</param>
    void SendPlayerMessageToPlayer(Player sender, string message);

    /// <summary>
    /// Adds a death to the kill feed on the right-hand side of the screen of all players.
    /// </summary>
    /// <param name="killer">The player that killer the <paramref name="killee" />.</param>
    /// <param name="killee">The player that has been killed.</param>
    /// <param name="weapon">The reason for this player's death.</param>
    void SendDeathMessage(Player killer, Player killee, Weapon weapon);

    /// <summary>
    /// Shows 'game text' (on-screen text) for a certain length of time for all players.
    /// </summary>
    /// <param name="text">The text to be displayed.</param>
    /// <param name="time">The duration of the text being shown in milliseconds.</param>
    /// <param name="style">The style of text to be displayed.</param>
    [Obsolete("Use GameText(string, TimeSpan, GameTextStyle) instead.")]
    void GameText(string text, int time, int style);

    /// <summary>
    /// Shows 'game text' (on-screen text) for a certain length of time for all players.
    /// </summary>
    /// <param name="text">The text to be displayed.</param>
    /// <param name="time">The duration of the text being shown.</param>
    /// <param name="style">The style of text to be displayed.</param>
    void GameText(string text, TimeSpan time, GameTextStyle style);

    /// <summary>
    /// Hides the game text in the specified style/slot for all players.
    /// </summary>
    /// <param name="style">The style/slot of the game text to hide.</param>
    void HideGameText(GameTextStyle style);

    /// <summary>
    /// Creates an explosion for all players.
    /// </summary>
    /// <param name="position">The position of the explosion.</param>
    /// <param name="type">The explosion type.</param>
    /// <param name="radius">The radius of the explosion.</param>
    void CreateExplosion(Vector3 position, ExplosionType type, float radius);

    /// <summary>
    /// Set the weather for all players.
    /// </summary>
    /// <param name="weather">The weather to set.</param>
    void SetWeather(int weather);
}