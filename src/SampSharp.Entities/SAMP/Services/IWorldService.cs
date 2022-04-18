// SampSharp
// Copyright 2022 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using SampSharp.Entities.Annotations;

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
    /// The delay until the car is respawned without a driver in seconds. Using -1 will prevent the
    /// vehicle from respawning.
    /// </param>
    /// <param name="addSiren">If true, enables the vehicle to have a siren, providing the vehicle has a horn.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns> The created vehicle.</returns>
    Vehicle CreateVehicle(VehicleModelType type, Vector3 position, float rotation, int color1, int color2,
        int respawnDelay = -1, bool addSiren = false, EntityId parent = default);

    /// <summary>
    /// Creates a static vehicle in the world.
    /// </summary>
    /// <param name="type">The model for the vehicle.</param>
    /// <param name="position">The coordinates for the vehicle.</param>
    /// <param name="rotation">The facing angle for the vehicle.</param>
    /// <param name="color1">The primary color ID.</param>
    /// <param name="color2">The secondary color ID.</param>
    /// <param name="respawnDelay">
    /// The delay until the car is respawned without a driver in seconds. Using -1 will prevent the
    /// vehicle from respawning.
    /// </param>
    /// <param name="addSiren">If true, enables the vehicle to have a siren, providing the vehicle has a horn.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns> The created vehicle.</returns>
    Vehicle CreateStaticVehicle(VehicleModelType type, Vector3 position, float rotation, int color1, int color2,
        int respawnDelay = -1, bool addSiren = false, EntityId parent = default);

    /// <summary>
    /// Creates a gang zone in the world.
    /// </summary>
    /// <param name="minX">The minimum x.</param>
    /// <param name="minY">The minimum y.</param>
    /// <param name="maxX">The maximum x.</param>
    /// <param name="maxY">The maximum y.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns>The created gang zone.</returns>
    GangZone CreateGangZone(float minX, float minY, float maxX, float maxY, EntityId parent = default);

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
    /// Adds a 'static' pickup to the world. These pickups support weapons, health, armor etc., with the ability to function
    /// without scripting them (weapons/health/armor will be given automatically).
    /// </summary>
    /// <param name="model">The model of the pickup.</param>
    /// <param name="type">The pickup spawn type.</param>
    /// <param name="position">The position where the pickup should be spawned.</param>
    /// <param name="virtualWorld">The virtual world ID of the pickup. Use -1 for all worlds.</param>
    /// <returns>A value indicating whether the pickup has been created successfully.</returns>
    bool AddStaticPickup(int model, PickupType type, Vector3 position, int virtualWorld = -1);

    /// <summary>
    /// Creates an object in the world.
    /// </summary>
    /// <param name="modelId">The model ID.</param>
    /// <param name="position">The position.</param>
    /// <param name="rotation">The rotation.</param>
    /// <param name="drawDistance">The draw distance.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns>The created object.</returns>
    GlobalObject CreateObject(int modelId, Vector3 position, Vector3 rotation, float drawDistance, EntityId parent = default);

    /// <summary>
    /// Creates a player object in the world.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="modelId">The model ID.</param>
    /// <param name="position">The position.</param>
    /// <param name="rotation">The rotation.</param>
    /// <param name="drawDistance">The draw distance.</param>
    /// <returns>The created player object.</returns>
    PlayerObject CreatePlayerObject(EntityId player, int modelId, Vector3 position, Vector3 rotation,
        float drawDistance);

    /// <summary>
    /// Creates a text label in the world.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="color">The color.</param>
    /// <param name="position">The position.</param>
    /// <param name="drawDistance">The draw distance.</param>
    /// <param name="virtualWorld">The virtual world.</param>
    /// <param name="testLos">if set to <c>true</c> the line of sight is tested to decide whether the label is drawn.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns>The created text label.</returns>
    TextLabel CreateTextLabel(string text, Color color, Vector3 position, float drawDistance,
        int virtualWorld = 0, bool testLos = true, EntityId parent = default);

    /// <summary>
    /// Creates a player text label in the world.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="text">The text.</param>
    /// <param name="color">The color.</param>
    /// <param name="position">The position.</param>
    /// <param name="drawDistance">The draw distance.</param>
    /// <param name="testLos">if set to <c>true</c> the line of sight is tested to decide whether the label is drawn.</param>
    /// <param name="attachedTo">A player or vehicle to attach the text label to.</param>
    /// <returns>The created text label.</returns>
    PlayerTextLabel CreatePlayerTextLabel(EntityId player, string text, Color color, Vector3 position,
        float drawDistance, bool testLos = true, EntityId attachedTo = default);

    /// <summary>
    /// Creates a textdraw in the world.
    /// </summary>
    /// <param name="position">The position of the textdraw.</param>
    /// <param name="text">The text of the textdraw.</param>
    /// <param name="parent">The parent of the entity to be created.</param>
    /// <returns>The created textdraw.</returns>
    TextDraw CreateTextDraw(Vector2 position, string text, EntityId parent = default);

    /// <summary>
    /// Creates the player textdraw in the world.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="position">The position of the textdraw.</param>
    /// <param name="text">The text of the textdraw.</param>
    /// <returns>The created player textdraw.</returns>
    PlayerTextDraw CreatePlayerTextDraw(EntityId player, Vector2 position, string text);

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
    /// This function sends a message to all players with a chosen color in the chat. The whole line in the chat box will be in
    /// the set color unless color embedding is used.
    /// </summary>
    /// <param name="color">The color of the message.</param>
    /// <param name="message">The text that will be displayed.</param>
    void SendClientMessage(Color color, string message);

    /// <summary>
    /// This function sends a message to all players with a chosen color in the chat. The whole line in the chat box will be in
    /// the set color unless color embedding is used.
    /// </summary>
    /// <param name="color">The color of the message.</param>
    /// <param name="messageFormat">The composite format string of the text that will be displayed (max 144 characters).</param>
    /// <param name="args">An object array that contains zero or more objects to format.</param>
    [StringFormatMethod("messageFormat")]
    void SendClientMessage(Color color, string messageFormat, params object[] args);

    /// <summary>
    /// This function sends a message to all players in white in the chat. The whole line in the chat box will be in the set
    /// color unless color embedding is used.
    /// </summary>
    /// <param name="message">The text that will be displayed.</param>
    void SendClientMessage(string message);

    /// <summary>
    /// This function sends a message to all players in white in the chat. The whole line in the chat box will be in the set
    /// color unless color embedding is used.
    /// </summary>
    /// <param name="messageFormat">The composite format string of the text that will be displayed (max 144 characters).</param>
    /// <param name="args">An object array that contains zero or more objects to format.</param>
    [StringFormatMethod("messageFormat")]
    void SendClientMessage(string messageFormat, params object[] args);

    /// <summary>
    /// Sends a message in the name the specified <paramref name="sender" /> to all players. The message will appear in the
    /// chat box and can be seen by all player. The line will start with the the sender's name in their color, followed
    /// by the <paramref name="message" /> in white.
    /// </summary>
    /// <param name="sender">The player which has sent the message.</param>
    /// <param name="message">The message that will be sent.</param>
    void SendPlayerMessageToPlayer(EntityId sender, string message);

    /// <summary>
    /// Adds a death to the kill feed on the right-hand side of the screen of all players.
    /// </summary>
    /// <param name="killer">The player that killer the <paramref name="player" />.</param>
    /// <param name="player">The player that has been killed.</param>
    /// <param name="weapon">The reason for this player's death.</param>
    void SendDeathMessage(EntityId killer, EntityId player, Weapon weapon);

    /// <summary>
    /// Shows 'game text' (on-screen text) for a certain length of time for all players.
    /// </summary>
    /// <param name="text">The text to be displayed.</param>
    /// <param name="time">The duration of the text being shown in milliseconds.</param>
    /// <param name="style">The style of text to be displayed.</param>
    void GameText(string text, int time, int style);

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