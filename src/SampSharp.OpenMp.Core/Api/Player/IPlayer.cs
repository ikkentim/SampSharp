using System.Numerics;
using System.Runtime.InteropServices.Marshalling;
using SampSharp.OpenMp.Core.RobinHood;
using SampSharp.OpenMp.Core.Std;
using SampSharp.OpenMp.Core.Std.Chrono;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPlayer" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtensible), typeof(IEntity))]
public readonly partial struct IPlayer
{
    /// <summary>
    /// Kicks the player from the server.
    /// </summary>
    public partial void Kick();

    /// <summary>
    /// Bans the player from the server.
    /// </summary>
    /// <param name="reason">The reason for banning the player.</param>
    public partial void Ban(string reason);

    /// <summary>
    /// Checks if the player is a bot (NPC).
    /// </summary>
    /// <returns><see langword="true" /> if the player is a bot; otherwise, <see langword="false" />.</returns>
    public partial bool IsBot();

    /// <summary>
    /// Gets the player's network data.
    /// </summary>
    /// <returns>A reference to the player's network data.</returns>
    public partial BlittableStructRef<PeerNetworkData> GetNetworkData();

    /// <summary>
    /// Gets the player's ping.
    /// </summary>
    /// <returns>The player's ping in milliseconds.</returns>
    public partial uint GetPing();

    /// <summary>
    /// Sends a packet to the player.
    /// </summary>
    /// <param name="data">The data to send.</param>
    /// <param name="channel">The channel to send the packet on.</param>
    /// <param name="dispatchEvents">Whether to dispatch events for the packet.</param>
    /// <returns><see langword="true" /> if the packet was sent successfully; otherwise, <see langword="false" />.</returns>
    public partial bool SendPacket(SpanLite<byte> data, int channel, bool dispatchEvents = true);

    /// <summary>
    /// Sends an RPC to the player.
    /// </summary>
    /// <param name="id">The RPC ID.</param>
    /// <param name="data">The data to send.</param>
    /// <param name="channel">The channel to send the RPC on.</param>
    /// <param name="dispatchEvents">Whether to dispatch events for the RPC.</param>
    /// <returns><see langword="true" /> if the RPC was sent successfully; otherwise, <see langword="false" />.</returns>
    public partial bool SendRPC(int id, SpanLite<byte> data, int channel, bool dispatchEvents = true);

    /// <summary>
    /// Broadcasts an RPC to the player's streamed peers.
    /// </summary>
    /// <param name="id">The RPC ID.</param>
    /// <param name="data">The data to broadcast.</param>
    /// <param name="channel">The channel to broadcast the RPC on.</param>
    /// <param name="skipFrom">Whether to skip broadcasting to the player.</param>
    public partial void BroadcastRPCToStreamed(int id, SpanLite<byte> data, int channel, bool skipFrom = false);

    /// <summary>
    /// Broadcasts a packet to the player's streamed peers.
    /// </summary>
    /// <param name="data">The data to broadcast.</param>
    /// <param name="channel">The channel to broadcast the packet on.</param>
    /// <param name="skipFrom">Whether to skip broadcasting to the player.</param>
    public partial void BroadcastPacketToStreamed(SpanLite<byte> data, int channel, bool skipFrom = true);

    /// <summary>
    /// Broadcasts a sync packet to the player's streamed peers.
    /// </summary>
    /// <param name="data">The data to broadcast.</param>
    /// <param name="channel">The channel to broadcast the sync packet on.</param>
    public partial void BroadcastSyncPacket(SpanLite<byte> data, int channel);

    /// <summary>
    /// Spawns the player.
    /// </summary>
    public partial void Spawn();

    /// <summary>
    /// Gets the player's client version.
    /// </summary>
    /// <returns>The player's client version.</returns>
    public partial ClientVersion GetClientVersion();

    /// <summary>
    /// Gets the player's client version name.
    /// </summary>
    /// <returns>The player's client version name.</returns>
    public partial string GetClientVersionName();

    /// <summary>
    /// Sets the player's position and finds the proper Z coordinate for the map.
    /// </summary>
    /// <param name="pos">The position to set.</param>
    public partial void SetPositionFindZ(Vector3 pos);

    /// <summary>
    /// Sets the player's camera position.
    /// </summary>
    /// <param name="pos">The position to set the camera to.</param>
    public partial void SetCameraPosition(Vector3 pos);

    /// <summary>
    /// Gets the player's camera position.
    /// </summary>
    /// <returns>The player's camera position.</returns>
    public partial Vector3 GetCameraPosition();

    /// <summary>
    /// Sets the direction the player's camera looks at.
    /// </summary>
    /// <param name="pos">The position to look at.</param>
    /// <param name="cutType">The type of camera cut.</param>
    public partial void SetCameraLookAt(Vector3 pos, int cutType);

    /// <summary>
    /// Gets the direction the player's camera looks at.
    /// </summary>
    /// <returns>The position the camera is looking at.</returns>
    public partial Vector3 GetCameraLookAt();

    /// <summary>
    /// Sets the camera to a position behind the player.
    /// </summary>
    public partial void SetCameraBehind();

    /// <summary>
    /// Interpolates the player's camera position.
    /// </summary>
    /// <param name="from">The starting position.</param>
    /// <param name="to">The ending position.</param>
    /// <param name="time">The duration of the interpolation in milliseconds.</param>
    /// <param name="cutType">The type of camera cut.</param>
    public partial void InterpolateCameraPosition(Vector3 from, Vector3 to, int time, PlayerCameraCutType cutType);

    /// <summary>
    /// Interpolates the player's camera look-at position.
    /// </summary>
    /// <param name="from">The starting position.</param>
    /// <param name="to">The ending position.</param>
    /// <param name="time">The duration of the interpolation in milliseconds.</param>
    /// <param name="cutType">The type of camera cut.</param>
    public partial void InterpolateCameraLookAt(Vector3 from, Vector3 to, int time, PlayerCameraCutType cutType);

    /// <summary>
    /// Attaches the player's camera to an object.
    /// </summary>
    /// <param name="obj">The object to attach the camera to.</param>
    public partial void AttachCameraToObject(IObject obj);

    /// <summary>
    /// Attaches the player's camera to a player object.
    /// </summary>
    /// <param name="obj">The player object to attach the camera to.</param>
    [OpenMpApiOverload("_player")]
    public partial void AttachCameraToObject(IPlayerObject obj);

    /// <summary>
    /// Sets the player's name.
    /// </summary>
    /// <param name="name">The new name for the player.</param>
    /// <returns>The status of the name change.</returns>
    public partial EPlayerNameStatus SetName(string name);

    /// <summary>
    /// Gets the player's name.
    /// </summary>
    /// <returns>The player's name.</returns>
    public partial string GetName();

    /// <summary>
    /// Gets the player's serial (GPCI).
    /// </summary>
    /// <returns>The player's serial.</returns>
    public partial string GetSerial();

    /// <summary>
    /// Gives a weapon to the player.
    /// </summary>
    /// <param name="weapon">The weapon to give.</param>
    public partial void GiveWeapon(WeaponSlotData weapon);

    /// <summary>
    /// Removes a weapon from the player.
    /// </summary>
    /// <param name="weapon">The weapon to remove.</param>
    public partial void RemoveWeapon(byte weapon);

    /// <summary>
    /// Sets the ammunition for a specific weapon slot.
    /// </summary>
    /// <param name="data">The weapon slot data containing the weapon and ammunition information.</param>
    public partial void SetWeaponAmmo(WeaponSlotData data);

    /// <summary>
    /// Gets all weapons and their associated data for the player.
    /// </summary>
    /// <returns>A reference to the player's weapon slots.</returns>
    public partial BlittableStructRef<WeaponSlots> GetWeapons();

    private partial void GetWeaponSlot(int slot, out WeaponSlotData data);

    /// <summary>
    /// Gets the weapon data for a specific weapon slot.
    /// </summary>
    /// <param name="slot">The weapon slot index.</param>
    /// <returns>The weapon slot data.</returns>
    public WeaponSlotData GetWeaponSlot(int slot)
    {
        GetWeaponSlot(slot, out var data);
        return data;
    }

    /// <summary>
    /// Resets all weapons for the player.
    /// </summary>
    public partial void ResetWeapons();

    /// <summary>
    /// Sets the player's currently armed weapon.
    /// </summary>
    /// <param name="weapon">The weapon ID to set as armed.</param>
    public partial void SetArmedWeapon(int weapon);

    /// <summary>
    /// Gets the player's currently armed weapon.
    /// </summary>
    /// <returns>The weapon ID of the currently armed weapon.</returns>
    public partial int GetArmedWeapon();

    /// <summary>
    /// Gets the ammunition count for the player's currently armed weapon.
    /// </summary>
    /// <returns>The ammunition count for the armed weapon.</returns>
    public partial int GetArmedWeaponAmmo();

    /// <summary>
    /// Sets the player's shop name.
    /// </summary>
    /// <param name="name">The name of the shop.</param>
    public partial void SetShopName(string name);

    /// <summary>
    /// Gets the player's shop name.
    /// </summary>
    /// <returns>The name of the shop.</returns>
    public partial string GetShopName();

    /// <summary>
    /// Sets the player's drunk level.
    /// </summary>
    /// <param name="level">The drunk level to set.</param>
    public partial void SetDrunkLevel(int level);

    /// <summary>
    /// Gets the player's drunk level.
    /// </summary>
    /// <returns>The player's drunk level.</returns>
    public partial int GetDrunkLevel();

    /// <summary>
    /// Sets the player's color.
    /// </summary>
    /// <param name="colour">The color to set.</param>
    public partial void SetColour(Colour colour);

    /// <summary>
    /// Gets the player's color.
    /// </summary>
    /// <returns>A reference to the player's color.</returns>
    public partial ref Colour GetColour();

    /// <summary>
    /// Sets the color of another player as seen by this player.
    /// </summary>
    /// <param name="other">The other player.</param>
    /// <param name="colour">The color to set for the other player.</param>
    public partial void SetOtherColour(IPlayer other, Colour colour);

    /// <summary>
    /// Gets the color of another player as seen by this player.
    /// </summary>
    /// <param name="other">The other player.</param>
    /// <param name="colour">When this method returns, contains the color of the other player.</param>
    /// <returns><see langword="true" /> if the color was successfully retrieved; otherwise, <see langword="false" />.</returns>
    public partial bool GetOtherColour(IPlayer other, out Colour colour);

    /// <summary>
    /// Sets whether the player is controllable.
    /// </summary>
    /// <param name="controllable"><see langword="true" /> to make the player controllable; otherwise, <see langword="false" />.</param>
    public partial void SetControllable(bool controllable);

    /// <summary>
    /// Gets whether the player is controllable.
    /// </summary>
    /// <returns><see langword="true" /> if the player is controllable; otherwise, <see langword="false" />.</returns>
    public partial bool GetControllable();

    /// <summary>
    /// Sets whether the player is spectating.
    /// </summary>
    /// <param name="spectating"><see langword="true" /> to make the player spectate; otherwise, <see langword="false" />.</param>
    public partial void SetSpectating(bool spectating);

    /// <summary>
    /// Sets the player's wanted level.
    /// </summary>
    /// <param name="level">The wanted level to set.</param>
    public partial void SetWantedLevel(uint level);

    /// <summary>
    /// Gets the player's wanted level.
    /// </summary>
    /// <returns>The player's wanted level.</returns>
    public partial uint GetWantedLevel();

    /// <summary>
    /// Plays a sound for the player at a specific position.
    /// </summary>
    /// <param name="sound">The sound ID to play.</param>
    /// <param name="pos">The position where the sound should be played.</param>
    public partial void PlaySound(int sound, Vector3 pos);

    /// <summary>
    /// Gets the ID of the last sound played for the player.
    /// </summary>
    /// <returns>The ID of the last played sound.</returns>
    public partial int LastPlayedSound();

    /// <summary>
    /// Plays an audio stream for the player.
    /// </summary>
    /// <param name="url">The URL of the audio stream.</param>
    /// <param name="usePos"><see langword="true" /> to play the audio at a specific position; otherwise, <see langword="false" />.</param>
    /// <param name="pos">The position where the audio should be played.</param>
    /// <param name="distance">The maximum distance at which the audio can be heard.</param>
    public partial void PlayAudio(string url, bool usePos = false, Vector3 pos = default, float distance = 0);

    /// <summary>
    /// Plays a crime report for the player with the specified <paramref name="suspect" /> and <paramref name="crime"/>.
    /// </summary>
    /// <param name="suspect">The player who committed the crime.</param>
    /// <param name="crime">The crime ID.</param>
    /// <returns><see langword="true" /> if the suspect is in a state for which a crime report could be played; otherwise <see langword="false"/>.</returns>
    public partial bool PlayerCrimeReport(IPlayer suspect, int crime);

    /// <summary>
    /// Stops the currently playing audio for the player.
    /// </summary>
    public partial void StopAudio();

    /// <summary>
    /// Gets the URL of the last played audio stream.
    /// </summary>
    /// <returns>The URL of the last played audio stream.</returns>
    public partial string LastPlayedAudio();

    /// <summary>
    /// Creates an explosion at a specific position for the player.
    /// </summary>
    /// <param name="vec">The position of the explosion.</param>
    /// <param name="type">The type of explosion.</param>
    /// <param name="radius">The radius of the explosion.</param>
    public partial void CreateExplosion(Vector3 vec, int type, float radius);

    /// <summary>
    /// Sends a death message to the player.
    /// </summary>
    /// <param name="player">The player who died.</param>
    /// <param name="killer">The player who killed the other player.</param>
    /// <param name="weapon">The weapon ID used to kill the player.</param>
    public partial void SendDeathMessage(IPlayer player, IPlayer killer, int weapon);

    /// <summary>
    /// Sends an empty death message to the player.
    /// </summary>
    public partial void SendEmptyDeathMessage();

    /// <summary>
    /// Removes default objects near a specific position for the player.
    /// </summary>
    /// <param name="model">The model ID of the objects to remove.</param>
    /// <param name="pos">The position near which to remove objects.</param>
    /// <param name="radius">The radius within which to remove objects.</param>
    public partial void RemoveDefaultObjects(uint model, Vector3 pos, float radius);

    /// <summary>
    /// Forces the player to reselect their class.
    /// </summary>
    public partial void ForceClassSelection();

    /// <summary>
    /// Sets the player's money.
    /// </summary>
    /// <param name="money">The amount of money to set.</param>
    public partial void SetMoney(int money);

    /// <summary>
    /// Gives the player additional money.
    /// </summary>
    /// <param name="money">The amount of money to give.</param>
    public partial void GiveMoney(int money);

    /// <summary>
    /// Resets the player's money to zero.
    /// </summary>
    public partial void ResetMoney();

    /// <summary>
    /// Gets the player's current money.
    /// </summary>
    /// <returns>The amount of money the player has.</returns>
    public partial int GetMoney();

    /// <summary>
    /// Sets a map icon for the player.
    /// </summary>
    /// <param name="id">The ID of the map icon.</param>
    /// <param name="pos">The position of the map icon.</param>
    /// <param name="type">The type of the map icon.</param>
    /// <param name="colour">The color of the map icon.</param>
    /// <param name="style">The style of the map icon.</param>
    public partial void SetMapIcon(int id, Vector3 pos, int type, Colour colour, MapIconStyle style);

    /// <summary>
    /// Removes a map icon for the player.
    /// </summary>
    /// <param name="id">The ID of the map icon to remove.</param>
    public partial void UnsetMapIcon(int id);

    /// <summary>
    /// Enables or disables stunt bonuses for the player.
    /// </summary>
    /// <param name="enable"><see langword="true" /> to enable stunt bonuses; otherwise, <see langword="false" />.</param>
    public partial void UseStuntBonuses(bool enable);

    /// <summary>
    /// Toggles the visibility of another player's name tag for this player.
    /// </summary>
    /// <param name="other">The other player.</param>
    /// <param name="toggle"><see langword="true" /> to show the name tag; <see langword="false" /> to hide it.</param>
    public partial void ToggleOtherNameTag(IPlayer other, bool toggle);

    /// <summary>
    /// Sets the in-game time for the player.
    /// </summary>
    /// <param name="hr">The hour to set (0-23), truncated to whole hours on the native side.</param>
    /// <param name="min">The minute to set (0-59), truncated to whole minutes on the native side.</param>
    public partial void SetTime(
        [MarshalUsing(typeof(HoursMarshaller))] TimeSpan hr,
        [MarshalUsing(typeof(MinutesMarshaller))] TimeSpan min);

    private partial void GetTime(out Pair<Hours, Minutes> result);

    /// <summary>
    /// Gets the in-game time for the player.
    /// </summary>
    /// <returns>A tuple containing the hour and minute.</returns>
    public (int hour, int minutes) GetTime()
    {
        GetTime(out var result);
        return ((int)((TimeSpan)result.First).TotalHours, (int)((TimeSpan)result.Second).TotalMinutes);
    }

    /// <summary>
    /// Enables or disables the in-game clock for the player.
    /// </summary>
    /// <param name="enable"><see langword="true" /> to enable the clock; otherwise, <see langword="false" />.</param>
    public partial void UseClock(bool enable);

    /// <summary>
    /// Checks if the in-game clock is enabled for the player.
    /// </summary>
    /// <returns><see langword="true" /> if the clock is enabled; otherwise, <see langword="false" />.</returns>
    public partial bool HasClock();

    /// <summary>
    /// Enables or disables widescreen mode for the player.
    /// </summary>
    /// <param name="enable"><see langword="true" /> to enable widescreen mode; otherwise, <see langword="false" />.</param>
    public partial void UseWidescreen(bool enable);

    /// <summary>
    /// Checks if widescreen mode is enabled for the player.
    /// </summary>
    /// <returns><see langword="true" /> if widescreen mode is enabled; otherwise, <see langword="false" />.</returns>
    public partial bool HasWidescreen();

    /// <summary>
    /// Sets the player's transformation matrix.
    /// </summary>
    /// <param name="tm">The transformation matrix to set.</param>
    public partial void SetTransform(GTAQuat tm);

    /// <summary>
    /// Sets the player's health.
    /// </summary>
    /// <param name="health">The health value to set.</param>
    public partial void SetHealth(float health);

    /// <summary>
    /// Gets the player's health.
    /// </summary>
    /// <returns>The player's current health.</returns>
    public partial float GetHealth();

    /// <summary>
    /// Sets the player's score.
    /// </summary>
    /// <param name="score">The score value to set.</param>
    public partial void SetScore(int score);

    /// <summary>
    /// Gets the player's score.
    /// </summary>
    /// <returns>The player's current score.</returns>
    public partial int GetScore();

    /// <summary>
    /// Sets the player's armor value.
    /// </summary>
    /// <param name="armour">The armor value to set.</param>
    public partial void SetArmour(float armour);

    /// <summary>
    /// Gets the player's armor value.
    /// </summary>
    /// <returns>The player's current armor value.</returns>
    public partial float GetArmour();

    /// <summary>
    /// Sets the player's gravity.
    /// </summary>
    /// <param name="gravity">The gravity value to set.</param>
    public partial void SetGravity(float gravity);

    /// <summary>
    /// Gets the player's gravity.
    /// </summary>
    /// <returns>The player's current gravity value.</returns>
    public partial float GetGravity();

    /// <summary>
    /// Sets the in-game world time for the player.
    /// </summary>
    /// <param name="time">The world time to set, truncated to whole hours on the native side.</param>
    public partial void SetWorldTime([MarshalUsing(typeof(HoursMarshaller))] TimeSpan time);

    /// <summary>
    /// Applies an animation to the player.
    /// </summary>
    /// <param name="animation">The animation data to apply.</param>
    /// <param name="syncType">The synchronization type for the animation.</param>
    public partial void ApplyAnimation(AnimationData animation, PlayerAnimationSyncType syncType);

    /// <summary>
    /// Clears all animations for the player.
    /// </summary>
    /// <param name="syncType">The synchronization type for clearing animations.</param>
    public partial void ClearAnimations(PlayerAnimationSyncType syncType);

    /// <summary>
    /// Gets the player's current animation data.
    /// </summary>
    /// <returns>The player's current animation data.</returns>
    public partial PlayerAnimationData GetAnimationData();

    /// <summary>
    /// Gets the player's surfing data.
    /// </summary>
    /// <returns>The player's current surfing data.</returns>
    public partial PlayerSurfingData GetSurfingData();

    /// <summary>
    /// Streams this player in for another player.
    /// </summary>
    /// <param name="other">The player for whom this player will be streamed in.</param>
    public partial void StreamInForPlayer(IPlayer other);

    /// <summary>
    /// Checks if another player is streamed in for this player.
    /// </summary>
    /// <param name="other">The player to check.</param>
    /// <returns><see langword="true" /> if the other player is streamed in for this player; otherwise, <see langword="false" />.</returns>
    public partial bool IsStreamedInForPlayer(IPlayer other);

    /// <summary>
    /// Streams another player out for this player.
    /// </summary>
    /// <param name="other">The player to stream out.</param>
    public partial void StreamOutForPlayer(IPlayer other);

    /// <summary>
    /// Gets the set of players for whom this player is currently streamed in.
    /// </summary>
    /// <returns>A set of players for whom this player is streamed in.</returns>
    public partial FlatPtrHashSet<IPlayer> StreamedForPlayers();

    /// <summary>
    /// Gets the current state of the player.
    /// </summary>
    /// <returns>The player's current state.</returns>
    public partial PlayerState GetState();

    /// <summary>
    /// Sets the player's team.
    /// </summary>
    /// <param name="team">The team ID to set.</param>
    public partial void SetTeam(int team);

    /// <summary>
    /// Gets the player's team.
    /// </summary>
    /// <returns>The team ID of the player.</returns>
    public partial int GetTeam();

    /// <summary>
    /// Sets the player's skin.
    /// </summary>
    /// <param name="skin">The skin ID to set.</param>
    /// <param name="send"><see langword="true" /> to send the update to other players; otherwise, <see langword="false" />.</param>
    public partial void SetSkin(int skin, bool send = true);

    /// <summary>
    /// Gets the player's skin.
    /// </summary>
    /// <returns>The skin ID of the player.</returns>
    public partial int GetSkin();

    /// <summary>
    /// Sets a chat bubble for the player.
    /// </summary>
    /// <param name="text">The text to display in the chat bubble.</param>
    /// <param name="colour">The color of the chat bubble.</param>
    /// <param name="drawDist">The draw distance for the chat bubble.</param>
    /// <param name="expire">The duration for which the chat bubble will be displayed.</param>
    public partial void SetChatBubble(string text, ref Colour colour, float drawDist, [MarshalUsing(typeof(MillisecondsMarshaller))] TimeSpan expire);

    /// <summary>
    /// Sends a client message to the player.
    /// </summary>
    /// <param name="colour">The color of the message.</param>
    /// <param name="message">The message to send.</param>
    public partial void SendClientMessage(ref Colour colour, string message);

    /// <summary>
    /// Sends a chat message to the player.
    /// </summary>
    /// <param name="sender">The player sending the message.</param>
    /// <param name="message">The message to send.</param>
    public partial void SendChatMessage(IPlayer sender, string message);

    /// <summary>
    /// Sends a command to the player.
    /// </summary>
    /// <param name="message">The command message to send.</param>
    public partial void SendCommand(string message);

    /// <summary>
    /// Sends game text to the player.
    /// </summary>
    /// <param name="message">The game text to display.</param>
    /// <param name="time">The duration for which the text will be displayed.</param>
    /// <param name="style">The style of the game text.</param>
    public partial void SendGameText(string message, [MarshalUsing(typeof(MillisecondsMarshaller))] TimeSpan time, int style);

    /// <summary>
    /// Hides game text for the player.
    /// </summary>
    /// <param name="style">The style of the game text to hide.</param>
    public partial void HideGameText(int style);

    /// <summary>
    /// Checks if game text is currently displayed for the player.
    /// </summary>
    /// <param name="style">The style of the game text to check.</param>
    /// <returns><see langword="true" /> if game text is displayed; otherwise, <see langword="false" />.</returns>
    public partial bool HasGameText(int style);

    /// <summary>
    /// Gets the currently displayed game text for the player, if any.
    /// </summary>
    /// <param name="style">The style of the game text to retrieve.</param>
    /// <param name="message">The message of the game text.</param>
    /// <param name="time">The duration for which the text is displayed.</param>
    /// <param name="remaining">The remaining time for the text to be displayed.</param>
    /// <returns><see langword="true" /> if game text is currently displayed; otherwise, <see langword="false" />.</returns>
    public partial bool GetGameText(int style, out string? message, [MarshalUsing(typeof(MillisecondsMarshaller))] out TimeSpan time, [MarshalUsing(typeof(MillisecondsMarshaller))] out TimeSpan remaining);

    /// <summary>
    /// Sets the player's weather.
    /// </summary>
    /// <param name="weatherId">The weather ID to set.</param>
    public partial void SetWeather(int weatherId);

    /// <summary>
    /// Gets the player's current weather.
    /// </summary>
    /// <returns>The weather ID of the player.</returns>
    public partial int GetWeather();

    /// <summary>
    /// Sets the player's world bounds.
    /// </summary>
    /// <param name="coords">The coordinates defining the world bounds.</param>
    public partial void SetWorldBounds(Vector4 coords);

    /// <summary>
    /// Gets the player's world bounds.
    /// </summary>
    /// <returns>The coordinates defining the player's world bounds.</returns>
    public partial Vector4 GetWorldBounds();

    /// <summary>
    /// Sets the player's fighting style.
    /// </summary>
    /// <param name="style">The fighting style to set.</param>
    /// <remarks>See https://open.mp/docs/scripting/resources/fightingstyles for more details.</remarks>
    public partial void SetFightingStyle(PlayerFightingStyle style);

    /// <summary>
    /// Gets the player's fighting style.
    /// </summary>
    /// <returns>The player's current fighting style.</returns>
    /// <remarks>See https://open.mp/docs/scripting/resources/fightingstyles for more details.</remarks>
    public partial PlayerFightingStyle GetFightingStyle();

    /// <summary>
    /// Sets the player's skill level for a specific weapon.
    /// </summary>
    /// <param name="skill">The weapon skill to set.</param>
    /// <param name="level">The skill level to set.</param>
    /// <remarks>See https://open.mp/docs/scripting/resources/weaponskills for more details.</remarks>
    public partial void SetSkillLevel(PlayerWeaponSkill skill, int level);

    /// <summary>
    /// Sets the player's special action.
    /// </summary>
    /// <param name="action">The special action to set.</param>
    public partial void SetAction(PlayerSpecialAction action);

    /// <summary>
    /// Gets the player's special action.
    /// </summary>
    /// <returns>The player's current special action.</returns>
    public partial PlayerSpecialAction GetAction();

    /// <summary>
    /// Sets the player's velocity.
    /// </summary>
    /// <param name="velocity">The velocity to set.</param>
    public partial void SetVelocity(Vector3 velocity);

    /// <summary>
    /// Gets the player's velocity.
    /// </summary>
    /// <returns>The player's current velocity.</returns>
    public partial Vector3 GetVelocity();

    /// <summary>
    /// Sets the player's interior.
    /// </summary>
    /// <param name="interior">The interior ID to set.</param>
    public partial void SetInterior(uint interior);

    /// <summary>
    /// Gets the player's interior.
    /// </summary>
    /// <returns>The player's current interior ID.</returns>
    public partial uint GetInterior();

    /// <summary>
    /// Gets the player's key data.
    /// </summary>
    /// <returns>A reference to the player's key data.</returns>
    public partial ref PlayerKeyData GetKeyData();

    /// <summary>
    /// Gets the player's skill levels for all weapon types.
    /// </summary>
    /// <returns>A reference to the player's skill levels.</returns>
    /// <remarks>See https://open.mp/docs/scripting/resources/weaponskills for more details.</remarks>
    public partial ref SkillsArray GetSkillLevels();

    /// <summary>
    /// Gets the player's aim data.
    /// </summary>
    /// <returns>A reference to the player's aim data.</returns>
    public partial ref PlayerAimData GetAimData();

    /// <summary>
    /// Gets the player's bullet data.
    /// </summary>
    /// <returns>A reference to the player's bullet data.</returns>
    public partial ref PlayerBulletData GetBulletData();

    /// <summary>
    /// Enables or disables camera targeting for the player.
    /// </summary>
    /// <param name="enable"><see langword="true" /> to enable camera targeting; otherwise, <see langword="false" />.</param>
    public partial void UseCameraTargeting(bool enable);

    /// <summary>
    /// Checks if camera targeting is enabled for the player.
    /// </summary>
    /// <returns><see langword="true" /> if camera targeting is enabled; otherwise, <see langword="false" />.</returns>
    public partial bool HasCameraTargeting();

    /// <summary>
    /// Removes the player from their vehicle.
    /// </summary>
    /// <param name="force"><see langword="true" /> to forcefully remove the player; otherwise, <see langword="false" />.</param>
    public partial void RemoveFromVehicle(bool force);

    /// <summary>
    /// Gets the player the camera is targeting.
    /// </summary>
    /// <returns>The player the camera is targeting, or <c>null</c> if no player is targeted.</returns>
    public partial IPlayer GetCameraTargetPlayer();

    /// <summary>
    /// Gets the vehicle the camera is targeting.
    /// </summary>
    /// <returns>The vehicle the camera is targeting, or <c>null</c> if no vehicle is targeted.</returns>
    public partial IVehicle GetCameraTargetVehicle();

    /// <summary>
    /// Gets the object the camera is targeting.
    /// </summary>
    /// <returns>The object the camera is targeting, or <c>null</c> if no object is targeted.</returns>
    public partial IObject GetCameraTargetObject();

    /// <summary>
    /// Gets the actor the camera is targeting.
    /// </summary>
    /// <returns>The actor the camera is targeting, or <c>null</c> if no actor is targeted.</returns>
    public partial IActor GetCameraTargetActor();

    /// <summary>
    /// Gets the player the player is targeting.
    /// </summary>
    /// <returns>The player being targeted, or <c>null</c> if no player is targeted.</returns>
    public partial IPlayer GetTargetPlayer();

    /// <summary>
    /// Gets the actor the player is targeting.
    /// </summary>
    /// <returns>The actor being targeted, or <c>null</c> if no actor is targeted.</returns>
    public partial IActor GetTargetActor();

    /// <summary>
    /// Sets whether the player should collide with remote vehicles.
    /// </summary>
    /// <param name="collide"><see langword="true" /> to enable collisions; otherwise, <see langword="false" />.</param>
    public partial void SetRemoteVehicleCollisions(bool collide);

    /// <summary>
    /// Makes the player spectate another player.
    /// </summary>
    /// <param name="target">The player to spectate.</param>
    /// <param name="mode">The spectate mode.</param>
    public partial void SpectatePlayer(IPlayer target, PlayerSpectateMode mode);

    /// <summary>
    /// Makes the player spectate a vehicle.
    /// </summary>
    /// <param name="target">The vehicle to spectate.</param>
    /// <param name="mode">The spectate mode.</param>
    public partial void SpectateVehicle(IVehicle target, PlayerSpectateMode mode);

    /// <summary>
    /// Gets the player's spectate data.
    /// </summary>
    /// <returns>A reference to the player's spectate data.</returns>
    public partial ref PlayerSpectateData GetSpectateData();

    /// <summary>
    /// Sends a client check request to the player.
    /// </summary>
    /// <param name="actionType">The type of action to check.</param>
    /// <param name="address">The memory address to check.</param>
    /// <param name="offset">The offset to check.</param>
    /// <param name="count">The number of bytes to check.</param>
    public partial void SendClientCheck(int actionType, int address, int offset, int count);

    /// <summary>
    /// Toggles ghost mode for the player.
    /// </summary>
    /// <param name="toggle"><see langword="true" /> to enable ghost mode; otherwise, <see langword="false" />.</param>
    public partial void ToggleGhostMode(bool toggle);

    /// <summary>
    /// Checks if ghost mode is enabled for the player.
    /// </summary>
    /// <returns><see langword="true" /> if ghost mode is enabled; otherwise, <see langword="false" />.</returns>
    public partial bool IsGhostModeEnabled();

    /// <summary>
    /// Gets the number of default objects removed for the player.
    /// </summary>
    /// <returns>The number of default objects removed.</returns>
    public partial int GetDefaultObjectsRemoved();

    /// <summary>
    /// Checks if the player is about to be kicked.
    /// </summary>
    /// <returns><see langword="true" /> if the player is about to be kicked; otherwise, <see langword="false" />.</returns>
    public partial bool GetKickStatus();

    /// <summary>
    /// Clears all tasks for the player.
    /// </summary>
    /// <param name="syncType">The synchronization type for clearing tasks.</param>
    public partial void ClearTasks(PlayerAnimationSyncType syncType);

    /// <summary>
    /// Allows or disallows the player to use weapons.
    /// </summary>
    /// <param name="allow"><see langword="true" /> to allow weapons; otherwise, <see langword="false" />.</param>
    public partial void AllowWeapons(bool allow);

    /// <summary>
    /// Checks if the player is allowed to use weapons.
    /// </summary>
    /// <returns><see langword="true" /> if the player is allowed to use weapons; otherwise, <see langword="false" />.</returns>
    public partial bool AreWeaponsAllowed();

    /// <summary>
    /// Allows or disallows the player to teleport by clicking the map.
    /// </summary>
    /// <param name="allow"><see langword="true" /> to allow teleporting; otherwise, <see langword="false" />.</param>
    public partial void AllowTeleport(bool allow);

    /// <summary>
    /// Checks if the player is allowed to teleport by clicking the map.
    /// </summary>
    /// <returns><see langword="true" /> if teleporting is allowed; otherwise, <see langword="false" />.</returns>
    public partial bool IsTeleportAllowed();

    /// <summary>
    /// Checks if the player is using an official client.
    /// </summary>
    /// <returns><see langword="true" /> if the player is using an official client; otherwise, <see langword="false" />.</returns>
    public partial bool IsUsingOfficialClient();
}