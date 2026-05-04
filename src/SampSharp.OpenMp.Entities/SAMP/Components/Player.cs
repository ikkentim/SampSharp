using System.Net;
using System.Numerics;
using JetBrains.Annotations;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a component which provides the data and functionality of a player.
/// </summary>
public class Player : WorldEntity
{
    private static readonly PlayerState[] _deadStates = [PlayerState.None, PlayerState.Spectating, PlayerState.Wasted];
    private readonly IOmpEntityProvider _entityProvider;
    private readonly IPlayer _rawPlayer;

    /// <summary>
    /// Constructs an instance of <see cref="Player" />, should be used internally.
    /// </summary>
    protected Player(IOmpEntityProvider entityProvider, IPlayer player) : base((IEntity)player)
    {
        _entityProvider = entityProvider;
        _rawPlayer = player;
    }

    /// <summary>
    /// Safe accessor for the underlying <see cref="IPlayer" /> handle. Throws
    /// <see cref="ObjectDisposedException" /> if the component has been destroyed,
    /// which means open.mp already fired <see cref="ComponentExtension.Cleanup" />
    /// and the native pointer is (or is about to be) freed. Without this guard,
    /// P/Invokes against a stale handle AV the process (0xC0000005) when gamemode
    /// code holds onto a <see cref="Player" /> reference across disconnect (e.g. via
    /// an async continuation).
    /// </summary>
    private IPlayer _player
    {
        get
        {
            if (!IsComponentAlive)
                throw new ObjectDisposedException(nameof(Player),
                    "Player has disconnected; native IPlayer handle is no longer valid.");
            return _rawPlayer;
        }
    }

    private IPlayerCheckpointData CheckpointData
    {
        get
        {
            var data = _player.QueryExtension<IPlayerCheckpointData>();
            if (data == null)
            {
                throw new InvalidOperationException("Missing checkpoint data");
            }
            return data;
        }
    }


    private IPlayerVehicleData VehicleData
    {
        get
        {
            var data = _player.QueryExtension<IPlayerVehicleData>();
            if (data == null)
            {
                throw new InvalidOperationException("Missing vehicle data");
            }
            return data;
        }
    }

    private IPlayerObjectData ObjectData
    {
        get
        {
            var data = _player.QueryExtension<IPlayerObjectData>();

            if (data == null)
            {
                throw new InvalidOperationException("Missing object data");
            }
            return data;
        }
    }

    private IPlayerMenuData MenuData
    {
        get
        {
            var data = _player.QueryExtension<IPlayerMenuData>();

            if (data == null)
            {
                throw new InvalidOperationException("Missing menu data");
            }
            return data;
        }
    }

    private IPlayerConsoleData ConsoleData
    {
        get
        {
            var data = _player.QueryExtension<IPlayerConsoleData>();

            if (data == null)
            {
                throw new InvalidOperationException("Missing console data");
            }
            return data;
        }
    }

    private IPlayerTextDrawData TextDrawData
    {
        get
        {
            var data = _player.QueryExtension<IPlayerTextDrawData>();
            if (data == null)
            {
                throw new InvalidOperationException("Missing text draw data");
            }
            return data;
        }
    }

    private IPlayerClassData ClassData
    {
        get
        {
            var data = _player.QueryExtension<IPlayerClassData>();
            if (data == null)
            {
                throw new InvalidOperationException("Missing class data");
            }
            return data;
        }
    }

    private IPlayerRecordingData RecordingData
    {
        get
        {
            var data = _player.QueryExtension<IPlayerRecordingData>();
            if (data == null)
            {
                throw new InvalidOperationException("Missing recording data");
            }
            return data;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _player.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

    /// <summary>
    /// Gets or sets the name of this player.
    /// </summary>
    public virtual string Name
    {
        get => _player.GetName();
        [Obsolete("Use SetName(string) instead")]
        set => SetName(value);
    }

    /// <summary>
    /// Gets or sets the facing angle of this player.
    /// </summary>
    public virtual float Angle
    {
        get => float.RadiansToDegrees(MathHelper.GetZAngleFromRotationMatrix(Matrix4x4.CreateFromQuaternion(Rotation)));
        set => Rotation = Quaternion.CreateFromAxisAngle(GtaVector.Up, float.DegreesToRadians(value));
    }

    /// <summary>
    /// Gets or sets the interior of this player.
    /// </summary>
    public virtual int Interior
    {
        get => (int)_player.GetInterior();
        set => _player.SetInterior((uint)value);
    }

    /// <summary>
    /// Gets or sets the health of this player.
    /// </summary>
    public virtual float Health
    {
        get => _player.GetHealth();
        set => _player.SetHealth(value);
    }

    /// <summary>
    /// Gets or sets the armor of this player.
    /// </summary>
    public virtual float Armour
    {
        get => _player.GetArmour();
        set => _player.SetArmour(value);
    }

    /// <summary>
    /// Gets the ammunition of the <see cref="Weapon" /> this player is currently holding.
    /// </summary>
    public virtual int WeaponAmmo => _player.GetArmedWeaponAmmo();

    /// <summary>
    /// Gets the <see cref="WeaponState" /> of the <see cref="Weapon" /> this player is currently holding.
    /// </summary>
    public virtual WeaponState WeaponState => (WeaponState)_player.GetAimData().weaponState;

    /// <summary>
    /// Gets the <see cref="Weapon" /> this player is currently holding.
    /// </summary>
    public virtual Weapon Weapon => (Weapon)_player.GetArmedWeapon();

    /// <summary>
    /// Gets the <see cref="Player" /> this player is aiming at.
    /// </summary>
    public virtual Player? TargetPlayer
    {
        get
        {
            var player = _player.GetTargetPlayer();
            if (!player.HasValue)
            {
                return null;
            }
            return _entityProvider.GetComponent(player);
        }
    }

    /// <summary>
    /// Gets or sets the team this player is in.
    /// </summary>
    public virtual int Team
    {
        get => _player.GetTeam();
        set => _player.SetTeam(value);
    }

    /// <summary>
    /// Gets or sets the score of this player.
    /// </summary>
    public virtual int Score
    {
        get => _player.GetScore();
        set => _player.SetScore(value);
    }

    /// <summary>
    /// Gets or sets the drunkenness level of this player.
    /// </summary>
    public virtual int DrunkLevel
    {
        get => _player.GetDrunkLevel();
        set => _player.SetDrunkLevel(value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> of this player.
    /// </summary>
    public virtual Color Color
    {
        get => _player.GetColour();
        set => _player.SetColour(value);
    }

    /// <summary>
    /// Gets or sets the skin of this player.
    /// </summary>
    public virtual int Skin
    {
        get => _player.GetSkin();
        set => _player.SetSkin(value);
    }

    /// <summary>
    /// Gets or sets the money of this player.
    /// </summary>
    public virtual int Money
    {
        get => _player.GetMoney();
        set => _player.SetMoney(value);
    }

    /// <summary>
    /// Gets the <see cref="PlayerState" /> of this player.
    /// </summary>
    public virtual PlayerState State => (PlayerState)_player.GetState();

    /// <summary>
    /// Gets the IP of this player as a string.
    /// </summary>
    public virtual string Ip => IpAddress.ToString();

    /// <summary>
    /// Gets the <see cref="IPAddress" /> of this player.
    /// </summary>
    public virtual IPAddress IpAddress => _player.GetNetworkData().Value.networkID.address.ToAddress();

    /// <summary>
    /// Gets the end point (<see cref="IPAddress" /> and port) of this player.
    /// </summary>
    public virtual IPEndPoint EndPoint => _player.GetNetworkData().Value.networkID.ToEndpoint();

    /// <summary>
    /// Gets the ping of this player.
    /// </summary>
    public virtual int Ping => (int)_player.GetPing();

    /// <summary>
    /// Gets or sets the wanted level of this player.
    /// </summary>
    public virtual int WantedLevel
    {
        get => (int)_player.GetWantedLevel();
        set => _player.SetWantedLevel((uint)value);
    }

    /// <summary>
    /// Gets or sets the <see cref="FightStyle" /> of this player.
    /// </summary>
    public virtual FightStyle FightStyle
    {
        get => (FightStyle)_player.GetFightingStyle();
        set => _player.SetFightingStyle((PlayerFightingStyle)value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Vector3" /> velocity of this player.
    /// </summary>
    public virtual Vector3 Velocity
    {
        get => _player.GetVelocity();
        set => _player.SetVelocity(value);
    }

    /// <summary>
    /// Gets the vehicle seat this player is occupying.
    /// </summary>
    /// <remarks>Returns -1 if the player is not in a vehicle.</remarks>
    public virtual int VehicleSeat
    {
        get
        {
            var data = VehicleData;

            if (data == null)
            {
                return -1;
            }

            return data.GetSeat();
        }
    }

    /// <summary>
    /// Gets the animation index of the animation this player is currently playing.
    /// </summary>
    public virtual int AnimationIndex => _player.GetAnimationData().ID;

    /// <summary>
    /// Gets or sets the <see cref="SpecialAction" /> of this player.
    /// </summary>
    public virtual SpecialAction SpecialAction
    {
        get => (SpecialAction)_player.GetAction();
        set => _player.SetAction((PlayerSpecialAction)value);
    }

    /// <summary>
    /// Gets or sets the position of the camera of this player as a <see cref="Vector3" />.
    /// </summary>
    /// <remarks>
    /// The getter prefers the real-time client-reported camera position
    /// (<c>aimData.camPos</c>) and only falls back to <c>getCameraPosition()</c>
    /// (the last value explicitly set via <c>setCameraPosition</c>) when aim data
    /// hasn't arrived yet. open.mp's <c>getCameraPosition</c> returns
    /// <c>(0,0,0)</c> when the camera is in default 3rd-person mode (i.e. never
    /// explicitly set), which is what legacy SampSharp users would not expect —
    /// they need the actual visual camera location, not the last server intent.
    /// </remarks>
    public virtual Vector3 CameraPosition
    {
        get
        {
            var camPos = _player.GetAimData().camPos;
            return camPos != Vector3.Zero ? camPos : _player.GetCameraPosition();
        }
        set => _player.SetCameraPosition(value);
    }

    /// <summary>
    /// Gets the front <see cref="Vector3" /> of this player's camera.
    /// </summary>
    public virtual Vector3 CameraFrontVector => _player.GetAimData().camFrontVector;

    /// <summary>
    /// Gets the <see cref="CameraMode" /> of this player's camera.
    /// </summary>
    public virtual CameraMode CameraMode => (CameraMode)_player.GetAimData().camMode;

    /// <summary>
    /// Gets the <see cref="Actor" /> this player is aiming at.
    /// </summary>
    public virtual Actor? TargetActor => _entityProvider.GetComponent(_player.GetTargetActor());

    /// <summary>
    /// Gets the <see cref="GlobalObject" /> the camera of this player is pointing at.
    /// </summary>
    public virtual GlobalObject? CameraTargetGlobalObject => _entityProvider.GetComponent(_player.GetCameraTargetObject());

    /// <summary>
    /// Gets the <see cref="PlayerObject" /> the camera of this player is pointing at.
    /// </summary>
    public virtual PlayerObject? CameraTargetPlayerObject => null; // TODO: broken, see https://github.com/openmultiplayer/open.mp/issues/1070

    /// <summary>
    /// Gets the <see cref="Vehicle" /> the camera of this player is pointing at.
    /// </summary>
    public virtual Vehicle? CameraTargetVehicle => _entityProvider.GetComponent(_player.GetCameraTargetVehicle());

    /// <summary>
    /// Gets the <see cref="Player" /> the camera of this player is pointing at.
    /// </summary>
    public virtual Player? CameraTargetPlayer => _entityProvider.GetComponent(_player.GetCameraTargetPlayer());

    /// <summary>
    /// Gets the <see cref="Actor" /> the camera of this player is pointing at.
    /// </summary>
    public virtual Actor? CameraTargetActor => _entityProvider.GetComponent(_player.GetCameraTargetActor());

    /// <summary>
    /// Gets the entity (<see cref="Player" />, <see cref="PlayerObject" />, object, <see cref="Vehicle" /> or <see cref="Actor" />) the camera of this player is pointing at.
    /// </summary>
    public virtual Component? CameraTargetEntity =>
        CameraTargetPlayer ??
        CameraTargetActor ??
        (Component?)CameraTargetVehicle ??
        CameraTargetGlobalObject;

    /// <summary>
    /// Gets a value indicating whether this player is currently in any vehicle.
    /// </summary>
    public virtual bool InAnyVehicle => Vehicle != null;

    /// <summary>
    /// Gets a value indicating whether this player is in a checkpoint.
    /// </summary>
    public virtual bool InCheckpoint => CheckpointData.GetCheckpoint().IsPlayerInside();

    /// <summary>
    /// Gets a value indicating whether this player is in a race checkpoint.
    /// </summary>
    public virtual bool InRaceCheckpoint => CheckpointData.GetRaceCheckpoint().IsPlayerInside();

    /// <summary>
    /// Gets the <see cref="Component" /> (object or vehicle) that this player is surfing.
    /// </summary>
    public virtual Component? SurfingEntity
    {
        get
        {
            var surf = _player.GetSurfingData();
            return surf.type switch
            {
                PlayerSurfingData.Type.Vehicle => _entityProvider.GetVehicle(surf.ID),
                PlayerSurfingData.Type.Object => _entityProvider.GetObject(surf.ID),
                PlayerSurfingData.Type.PlayerObject => _entityProvider.GetPlayerObject(this, surf.ID),
                _ => null
            };
        }
    }


    /// <summary>
    /// Gets the <see cref="Vehicle" /> this player is currently in.
    /// </summary>
    public virtual Vehicle? Vehicle => _entityProvider.GetComponent(VehicleData.GetVehicle());

    /// <summary>
    /// Gets the <see cref="Menu" /> this player is currently in.
    /// </summary>
    public virtual Menu? Menu
    {
        get
        {
            var menuId = MenuData.GetMenuID();
            return menuId == OpenMpConstants.INVALID_MENU_ID ? null : _entityProvider.GetMenu(menuId);
        }
    }

    /// <summary>
    /// Gets a value indicating whether this player is a bot (NPC).
    /// </summary>
    public virtual bool IsNpc => _player.IsBot();

    /// <summary>
    /// Gets a value indicating whether this player is logged into RCON.
    /// </summary>
    public virtual bool IsAdmin => ConsoleData.HasConsoleAccess();

    /// <summary>
    /// Gets a value indicating whether this player is alive.
    /// </summary>
    public virtual bool IsAlive => !_deadStates.Contains(State);

    /// <summary>
    /// Gets this player's global computer identifier string.
    /// </summary>
    public virtual string Gpci => _player.GetSerial();

    /// <summary>
    /// Gets a value indicating whether this player is selecting a text draw.
    /// </summary>
    public virtual bool IsSelectingTextDraw => TextDrawData.IsSelecting();

    /// <summary>
    /// Gets the <see cref="TimeSpan" /> this player has been connected to the server.
    /// </summary>
    public virtual TimeSpan ConnectedTime
    {
        get
        {
            var stats = _player.GetNetworkData().Value.network.GetStatistics();
            return TimeSpan.FromMilliseconds(stats.ConnectionElapsedTime);
        }
    }

    /// <summary>
    /// Gets the number of messages received from this player.
    /// </summary>
    public virtual int MessagesReceived
    {
        get
        {
            var stats = _player.GetNetworkData().Value.network.GetStatistics();
            return (int)stats.MessagesReceived;
        }
    }

    /// <summary>
    /// Gets the number of messages received from this player per second.
    /// </summary>
    public virtual int MessagesReceivedPerSecond
    {
        get
        {
            var stats = _player.GetNetworkData().Value.network.GetStatistics();
            return (int)stats.MessagesReceivedPerSecond;
        }
    }

    /// <summary>
    /// Gets the number of messages sent to this player.
    /// </summary>
    public virtual int MessagesSent
    {
        get
        {
            var stats = _player.GetNetworkData().Value.network.GetStatistics();
            return (int)stats.MessagesSent;
        }
    }

    /// <summary>
    /// Gets the number of bytes received from this player.
    /// </summary>
    public virtual int BytesReceived
    {
        get
        {
            var stats = _player.GetNetworkData().Value.network.GetStatistics();
            return (int)stats.BytesReceived;
        }
    }

    /// <summary>
    /// Gets the number of bytes sent to this player.
    /// </summary>
    public virtual int BytesSent
    {
        get
        {
            var stats = _player.GetNetworkData().Value.network.GetStatistics();
            return (int)stats.TotalBytesSent;
        }
    }

    /// <summary>
    /// Gets the <see cref="ConnectionStatus" /> of this player.
    /// </summary>
    public virtual ConnectionStatus ConnectionStatus
    {
        get
        {
            var stats = _player.GetNetworkData().Value.network.GetStatistics();
            return (ConnectionStatus)stats.ConnectMode;
        }
    }

    /// <summary>
    /// Gets the aspect ratio of this player's camera.
    /// </summary>
    public virtual float AspectCameraRatio => _player.GetAimData().aspectRatio;

    /// <summary>
    /// Gets the game camera zoom level for this player.
    /// </summary>
    public virtual float CameraZoom => _player.GetAimData().camZoom;

    /// <summary>
    /// Gets or sets this player's gravity.
    /// </summary>
    public virtual float Gravity
    {
        get => _player.GetGravity();
        set => _player.SetGravity(value);
    }

    /// <summary>
    /// Gets a value indicating whether this player is using the official Rockstar/SA-MP client (as opposed to open.mp, mobile/PSP, or an unofficial fork).
    /// </summary>
    public virtual bool IsUsingOfficialClient => _player.IsUsingOfficialClient();

    /// <summary>
    /// Gets a value indicating whether this player is using the open.mp client.
    /// </summary>
    public virtual bool IsUsingOmp => ClientVersion == ClientVersion.openmp;

    /// <summary>
    /// Gets this player's <see cref="SampSharp.OpenMp.Core.Api.ClientVersion" />.
    /// </summary>
    public virtual ClientVersion ClientVersion => _player.GetClientVersion();

    /// <summary>
    /// Gets this player's client version name.
    /// </summary>
    public virtual string ClientVersionName => _player.GetClientVersionName();

    /// <summary>
    /// Gets or sets a value indicating whether ghost mode is enabled for this player.
    /// </summary>
    /// <remarks>When enabled, other players will pass through this player as if they were not there.</remarks>
    public virtual bool IsGhostModeEnabled
    {
        get => _player.IsGhostModeEnabled();
        set => _player.ToggleGhostMode(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this player is allowed to use weapons.
    /// </summary>
    public virtual bool AreWeaponsAllowed
    {
        get => _player.AreWeaponsAllowed();
        set => _player.AllowWeapons(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether map-click teleporting is allowed for this player.
    /// </summary>
    public virtual bool IsTeleportAllowed
    {
        get => _player.IsTeleportAllowed();
        set => _player.AllowTeleport(value);
    }

    /// <summary>
    /// Gets or sets the world bounds for this player as <c>(maxX, minX, maxY, minY)</c>.
    /// </summary>
    public virtual Vector4 WorldBounds
    {
        get => _player.GetWorldBounds();
        set => _player.SetWorldBounds(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether widescreen mode is enabled for this player.
    /// </summary>
    public virtual bool HasWidescreen
    {
        get => _player.HasWidescreen();
        set => _player.UseWidescreen(value);
    }

    /// <summary>
    /// Gets or sets the current weather for this player.
    /// </summary>
    public virtual int Weather
    {
        get => _player.GetWeather();
        set => _player.SetWeather(value);
    }

    /// <summary>
    /// Gets a lazy sequence of players for whom this player is currently streamed in.
    /// </summary>
    public virtual IEnumerable<Player> StreamedForPlayers
    {
        get
        {
            foreach (var raw in _player.StreamedForPlayers())
            {
                var component = _entityProvider.GetComponent(raw);
                if (component != null)
                {
                    yield return component;
                }
            }
        }
    }

    /// <summary>
    /// Gets the number of default world objects that have been removed for this player.
    /// </summary>
    public virtual int DefaultObjectsRemoved => _player.GetDefaultObjectsRemoved();

    /// <summary>
    /// Gets a value indicating whether this player is in the process of being kicked.
    /// </summary>
    public virtual bool IsBeingKicked => _player.GetKickStatus();

    private IPlayerCustomModelsData? CustomModelsData =>
        _player.TryQueryExtension<IPlayerCustomModelsData>(out var data) ? data : null;

    /// <summary>
    /// Gets or sets the active custom skin model ID for this player, or <see langword="null" /> if no custom skin is set.
    /// </summary>
    public virtual uint? CustomSkin
    {
        get
        {
            var skin = CustomModelsData?.GetCustomSkin() ?? 0;
            return skin == 0 ? null : skin;
        }
        set
        {
            if (value.HasValue)
            {
                var data = CustomModelsData ?? throw new InvalidOperationException("Custom models component is not loaded");
                data.SetCustomSkin(value.Value);
            }
        }
    }

    /// <summary>
    /// Sets the name of this player.
    /// </summary>
    /// <param name="name">The new player name.</param>
    /// <exception cref="InvalidPlayerNameException">Thrown if the <paramref name="name" /> is invalid or already in use.</exception>
    public virtual void SetName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        var result = _player.SetName(name);
        switch (result)
        {
            case EPlayerNameStatus.Invalid:
                throw new InvalidPlayerNameException("Player name is invalid");
            case EPlayerNameStatus.Taken:
                throw new InvalidPlayerNameException("Player name is already in use");
        }
    }

    /// <summary>
    /// Configures spawn information for this player, including team, skin, position, and weapons.
    /// </summary>
    /// <param name="team">The team ID for this player.</param>
    /// <param name="skin">The skin this player will spawn with.</param>
    /// <param name="position">The spawn position as a <see cref="Vector3" />.</param>
    /// <param name="rotation">The facing direction after spawning.</param>
    /// <param name="weapon1">The first spawn <see cref="Weapon" />.</param>
    /// <param name="weapon1Ammo">The ammunition for the first <paramref name="weapon1" />.</param>
    /// <param name="weapon2">The second spawn <see cref="Weapon" />.</param>
    /// <param name="weapon2Ammo">The ammunition for the second <paramref name="weapon2" />.</param>
    /// <param name="weapon3">The third spawn <see cref="Weapon" />.</param>
    /// <param name="weapon3Ammo">The ammunition for the third <paramref name="weapon3" />.</param>
    public virtual void SetSpawnInfo(int team, int skin, Vector3 position, float rotation, Weapon weapon1 = Weapon.None, int weapon1Ammo = 0,
        Weapon weapon2 = Weapon.None, int weapon2Ammo = 0, Weapon weapon3 = Weapon.None, int weapon3Ammo = 0)
    {
        var weapons = new WeaponSlotData[OpenMpConstants.MAX_WEAPON_SLOTS];
        weapons[0] = new WeaponSlotData((byte)weapon1, weapon1Ammo);
        weapons[1] = new WeaponSlotData((byte)weapon2, weapon2Ammo);
        weapons[2] = new WeaponSlotData((byte)weapon3, weapon3Ammo);

        var info = new PlayerClass(team, skin, position, rotation, new WeaponSlots(weapons));

        ClassData.SetSpawnInfo(ref info);
    }

    /// <summary>
    /// Gets the network statistics for this player.
    /// </summary>
    /// <returns>A <see cref="NetworkStats" /> object containing the player's network statistics.</returns>
    public NetworkStats GetNetworkStats()
    {
        return new NetworkStats(_player.GetNetworkData().Value.network.GetStatistics());
    }

    /// <summary>(Re)Spawns a player.</summary>
    public virtual void Spawn()
    {
        _player.Spawn();
    }

    /// <summary>
    /// Restores the camera to the default position behind the player after manual camera positioning.
    /// </summary>
    public virtual void PutCameraBehindPlayer()
    {
        _player.SetCameraBehind();
    }

    /// <summary>
    /// Sets this player's position and adjusts the Z-coordinate to the nearest solid ground beneath the position.
    /// </summary>
    /// <param name="position">The position to move this player to as a <see cref="Vector3" />.</param>
    public virtual void SetPositionFindZ(Vector3 position)
    {
        _player.SetPositionFindZ(position);
    }

    /// <summary>
    /// Determines whether this player is in range of a specified point.
    /// </summary>
    /// <param name="range">The maximum distance this player can be from the point to be in range.</param>
    /// <param name="point">The point to check the range to.</param>
    /// <returns><see langword="true" /> if this player is in range of the point; otherwise, <see langword="false" />.</returns>
    public virtual bool IsInRangeOfPoint(float range, Vector3 point)
    {
        return GetDistanceFromPoint(point) <= range;
    }

    /// <summary>
    /// Calculates the distance between this player and a specified point.
    /// </summary>
    /// <param name="point">The point to calculate the distance from as a <see cref="Vector3" />.</param>
    /// <returns>The distance between this player and the <paramref name="point" />.</returns>
    public virtual float GetDistanceFromPoint(Vector3 point)
    {
        var offset = point - Position;
        return offset.Length();
    }

    /// <summary>
    /// Determines whether the specified <paramref name="player" /> is streamed in on this player's client.
    /// </summary>
    /// <remarks>Players are not streamed in on their own client. If this player is the same as the <paramref name="player" />, this method returns <see langword="false" />. Players stream out if they are more than 150 meters away (configurable via server.cfg stream_distance).</remarks>
    /// <param name="player">The <see cref="Player" /> to check.</param>
    /// <returns><see langword="true" /> if the specified <paramref name="player" /> is streamed in for this player; otherwise, <see langword="false" />.</returns>
    public virtual bool IsPlayerStreamedIn(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        return _player.IsStreamedInForPlayer(player);
    }

    /// <summary>
    /// Sets the ammunition for this player's weapon.
    /// </summary>
    /// <param name="weapon">The weapon to set ammunition for.</param>
    /// <param name="ammo">The amount of ammunition to set.</param>
    public virtual void SetAmmo(Weapon weapon, int ammo)
    {
        _player.SetWeaponAmmo(new WeaponSlotData((byte)weapon, ammo));
    }

    /// <summary>
    /// Gives this player a weapon with a specified amount of ammunition.
    /// </summary>
    /// <param name="weapon">The weapon to give to this player.</param>
    /// <param name="ammo">The amount of ammunition to give with the weapon.</param>
    public virtual void GiveWeapon(Weapon weapon, int ammo)
    {
        _player.GiveWeapon(new WeaponSlotData((byte)weapon, ammo));
    }


    /// <summary>
    /// Removes all weapons from this player.
    /// </summary>
    public virtual void ResetWeapons()
    {
        _player.ResetWeapons();
    }

    /// <summary>
    /// Removes a single weapon from this player.
    /// </summary>
    /// <param name="weapon">The weapon to remove.</param>
    public virtual void RemoveWeapon(Weapon weapon)
    {
        _player.RemoveWeapon((byte)weapon);
    }

    /// <summary>
    /// Sets the armed weapon of this player.
    /// </summary>
    /// <param name="weapon">The weapon that the player should be armed with.</param>
    public virtual void SetArmedWeapon(Weapon weapon)
    {
        _player.SetArmedWeapon((int)weapon);
    }

    /// <summary>
    /// Gets the <see cref="Weapon" /> and ammunition in this player's weapon slot.
    /// </summary>
    /// <param name="slot">The weapon slot index (0-12).</param>
    /// <param name="weapon">The <see cref="Weapon" /> in the slot, passed by reference.</param>
    /// <param name="ammo">The ammunition in the slot, passed by reference.</param>
    public virtual void GetWeaponData(int slot, out Weapon weapon, out int ammo)
    {
        var data = _player.GetWeaponSlot(slot);
        weapon = (Weapon)data.Id;
        ammo = data.Ammo;
    }

    /// <summary>
    /// Gives money to this player.
    /// </summary>
    /// <param name="money">The amount of money to give. Use a negative value to take money.</param>
    public virtual void GiveMoney(int money)
    {
        _player.GiveMoney(money);
    }

    /// <summary>
    /// Resets this player's money to zero.
    /// </summary>
    public virtual void ResetMoney()
    {
        _player.ResetMoney();
    }

    /// <summary>
    /// Retrieves the keys this player is currently pressing.
    /// </summary>
    /// <remarks>
    /// Only the function of keys can be detected, not the actual physical keys. For example, you cannot detect if the player presses space, but you can detect if they press sprint (which can be mapped to any key, defaulting to space).
    /// </remarks>
    /// <param name="keys">The keys this player is pressing, passed by reference.</param>
    /// <param name="upDown">The up/down direction value, passed by reference.</param>
    /// <param name="leftRight">The left/right direction value, passed by reference.</param>
    public virtual void GetKeys(out Keys keys, out int upDown, out int leftRight)
    {
        var data = _player.GetKeyData();
        keys = (Keys)data.keys;
        upDown = data.upDown;
        leftRight = data.leftRight;
    }

    /// <summary>
    /// Sets the game clock for this player to a specific time. This also changes the daytime visuals.
    /// </summary>
    /// <param name="hour">The <paramref name="hour" /> to set (0-23).</param>
    /// <param name="minutes">The <paramref name="minutes" /> to set (0-59).</param>
    public virtual void SetTime(int hour, int minutes)
    {
        _player.SetTime(TimeSpan.FromHours(hour), TimeSpan.FromMinutes(minutes));
    }

    /// <summary>
    /// Gets the current game time for this player.
    /// </summary>
    /// <remarks>The time is set by <see cref="IServerService.SetWorldTime" /> or can be overridden per-player with <see cref="SetTime" />.</remarks>
    /// <param name="hour">The current <paramref name="hour" />, passed by reference.</param>
    /// <param name="minutes">The current <paramref name="minutes" />, passed by reference.</param>
    public virtual void GetTime(out int hour, out int minutes)
    {
        (hour, minutes) = _player.GetTime();
    }

    /// <summary>
    /// Toggles the visibility of the in-game clock (top right corner) for this player.
    /// </summary>
    /// <remarks>Time is not synced with other players.</remarks>
    /// <param name="toggle"><see langword="true" /> to show the clock; <see langword="false" /> to hide it.</param>
    public virtual void ToggleClock(bool toggle)
    {
        _player.UseClock(toggle);
    }

    /// <summary>
    /// Sets the weather for this player. If the clock is enabled, weather changes will interpolate gradually; otherwise they change instantly.
    /// </summary>
    /// <param name="weather">The weather ID to set.</param>
    [Obsolete("Use Weather property instead.")]
    public virtual void SetWeather(int weather)
    {
        Weather = weather;
    }

    /// <summary>
    /// Forces this player to go back to class selection.
    /// </summary>
    /// <remarks>The player will not return to class selection until they re-spawn. This can be achieved with <see cref="ToggleSpectating" /></remarks>
    public virtual void ForceClassSelection()
    {
        _player.ForceClassSelection();
    }

    /// <summary>
    /// Displays the text draw selection cursor and enables this player to select a text draw.
    /// </summary>
    /// <param name="hoverColor">The <see cref="Color" /> to display when hovering over a text draw.</param>
    public virtual void SelectTextDraw(Color hoverColor)
    {
        TextDrawData.BeginSelection(hoverColor);
    }

    /// <summary>
    /// Cancels text draw selection mode for this player.
    /// </summary>
    public virtual void CancelSelectTextDraw()
    {
        TextDrawData.EndSelection();
    }

    /// <summary>
    /// Plays a crime report for this player with the specified <paramref name="suspect"/> and <paramref name="crime"/>, similar to single-player when committing a crime.
    /// </summary>
    /// <param name="suspect">The suspect <see cref="Player" /> to be described in the report.</param>
    /// <param name="crime">The crime ID, which will be reported as a 10-code (e.g., 10-16 for crime ID 16).</param>
    /// <returns><see langword="true" /> if the suspect is in a state for which a crime report could be played; otherwise <see langword="false"/>.</returns>
    public virtual bool PlayCrimeReport(Player suspect, int crime)
    {
        ArgumentNullException.ThrowIfNull(suspect);
        return _player.PlayerCrimeReport(suspect, crime);
    }

    /// <summary>
    /// Plays an audio stream for this player. Standard audio files (such as MP3) are also supported.
    /// </summary>
    /// <param name="url">The URL to stream. Valid formats are MP3 and OGG/Vorbis. A link to a .pls file will play that playlist.</param>
    /// <param name="position">The position from which the audio should be heard as a <see cref="Vector3" />.</param>
    /// <param name="distance">The distance over which the audio will be audible.</param>
    public virtual void PlayAudioStream(string url, Vector3 position, float distance)
    {
        ArgumentNullException.ThrowIfNull(url);
        _player.PlayAudio(url, true, position, distance);
    }

    /// <summary>
    /// Plays an audio stream for this player. Standard audio files (such as MP3) are also supported.
    /// </summary>
    /// <param name="url">The URL to stream. Valid formats are MP3 and OGG/Vorbis. A link to a .pls file will play that playlist.</param>
    public virtual void PlayAudioStream(string url)
    {
        ArgumentNullException.ThrowIfNull(url);
        _player.PlayAudio(url);
    }

    /// <summary>
    /// Disables or enables <see cref="Vehicle" /> collisions for this player.
    /// </summary>
    /// <param name="disable">If <see langword="true" />, <see cref="Vehicle" /> collisions are disabled; if <see langword="false" />, they are enabled.</param>
    public virtual void DisableRemoteVehicleCollisions(bool disable)
    {
        _player.SetRemoteVehicleCollisions(!disable);
    }

    /// <summary>
    /// Enables or disables camera targeting functionality for this player.
    /// </summary>
    /// <param name="enable">If <see langword="true" />, the functionality is enabled; if <see langword="false" />, it is disabled.</param>
    public virtual void EnablePlayerCameraTarget(bool enable)
    {
        _player.UseCameraTargeting(enable);
    }

    /// <summary>
    /// Stops the current audio stream for this player.
    /// </summary>
    public virtual void StopAudioStream()
    {
        _player.StopAudio();
    }

    /// <summary>
    /// Loads or unloads an interior script for this player (for example, the Ammunation shop menu).
    /// </summary>
    /// <param name="shopName">The name of the shop. See <see cref="ShopName" /> for available shop names.</param>
    public virtual void SetShopName(string shopName)
    {
        ArgumentNullException.ThrowIfNull(shopName);
        _player.SetShopName(shopName);
    }

    /// <summary>
    /// Sets the skill level of a specific <see cref="Weapon" /> for this player.
    /// </summary>
    /// <remarks>The <paramref name="skill" /> parameter is a weapon skill type, not a weapon ID.</remarks>
    /// <param name="skill">The <see cref="WeaponSkill" /> to set.</param>
    /// <param name="level">The skill level (0-999). Values outside this range will be clamped.</param>
    public virtual void SetSkillLevel(WeaponSkill skill, int level)
    {
        _player.SetSkillLevel((PlayerWeaponSkill)skill, level);
    }

    /// <summary>
    /// Attaches an object to a specific bone on this player.
    /// </summary>
    /// <param name="index">The attachment slot index (0-9).</param>
    /// <param name="modelId">The model ID of the object to attach.</param>
    /// <param name="bone">The bone to attach the object to.</param>
    /// <param name="offset">The offset of the object from the <paramref name="bone" /> as a <see cref="Vector3" />.</param>
    /// <param name="rotation">The rotation of the attached object as a <see cref="Vector3" />.</param>
    /// <param name="scale">The scale of the attached object as a <see cref="Vector3" />.</param>
    /// <param name="materialColor1">The primary object <see cref="Color" />.</param>
    /// <param name="materialColor2">The secondary object <see cref="Color" />.</param>
    /// <returns><see langword="true" /> on success; otherwise, <see langword="false" />.</returns>
    public virtual bool SetAttachedObject(int index, int modelId, Bone bone, Vector3 offset, Vector3 rotation, Vector3 scale, Color materialColor1,
        Color materialColor2)
    {
        if (index is < 0 or >= OpenMpConstants.MAX_ATTACHED_OBJECT_SLOTS)
        {
            return false;
        }

        var obj = new ObjectAttachmentSlotData(modelId, (int)bone, offset, rotation, scale, materialColor1, materialColor2);

        ObjectData.SetAttachedObject(index, ref obj);

        return true;
    }

    /// <summary>
    /// Removes an attached object from this player.
    /// </summary>
    /// <param name="index">The attachment slot index (set via <see cref="SetAttachedObject" />).</param>
    /// <returns><see langword="true" /> on success; otherwise, <see langword="false" />.</returns>
    public virtual bool RemoveAttachedObject(int index)
    {
        if (index is < 0 or >= OpenMpConstants.MAX_ATTACHED_OBJECT_SLOTS)
        {
            return false;
        }

        ObjectData.RemoveAttachedObject(index);
        return true;
    }

    /// <summary>
    /// Determines whether this player has an object attached in the specified <paramref name="index" />.
    /// </summary>
    /// <param name="index">The attachment slot index to check.</param>
    /// <returns><see langword="true" /> if the slot is occupied; otherwise, <see langword="false" />.</returns>
    public virtual bool IsAttachedObjectSlotUsed(int index)
    {
        if (index is < 0 or >= OpenMpConstants.MAX_ATTACHED_OBJECT_SLOTS)
        {
            return false;
        }

        return ObjectData.HasAttachedObject(index);
    }

    /// <summary>
    /// Enables edit mode for an attached object.
    /// </summary>
    /// <param name="index">The attachment slot index to edit.</param>
    /// <returns><see langword="true" /> on success; otherwise, <see langword="false" />.</returns>
    public virtual bool DoEditAttachedObject(int index)
    {
        if (index is < 0 or >= OpenMpConstants.MAX_ATTACHED_OBJECT_SLOTS)
        {
            return false;
        }

        ObjectData.EditAttachedObject(index);

        return true;
    }

    /// <summary>
    /// Creates a chat bubble above this player's name tag.
    /// </summary>
    /// <param name="text">The text to display.</param>
    /// <param name="color">The text color.</param>
    /// <param name="drawDistance">The distance from where players are able to see the chat bubble.</param>
    /// <param name="expireTime">The time in milliseconds the bubble should be displayed for.</param>
    [Obsolete("Use SetChatBubble(string,Color,float,TimeSpan) instead")]
    public virtual void SetChatBubble(string text, Color color, float drawDistance, int expireTime)
    {
        SetChatBubble(text, color, drawDistance, TimeSpan.FromMilliseconds(expireTime));
    }

    /// <summary>
    /// Creates a chat bubble above this player's name tag.
    /// </summary>
    /// <param name="text">The text to display.</param>
    /// <param name="color">The text color.</param>
    /// <param name="drawDistance">The distance from where players are able to see the chat bubble.</param>
    /// <param name="expireTime">The time the bubble should be displayed for.</param>
    public virtual void SetChatBubble(string text, Color color, float drawDistance, TimeSpan expireTime)
    {
        ArgumentNullException.ThrowIfNull(text);

        Colour clr = color;
        _player.SetChatBubble(text, ref clr, drawDistance, expireTime);
    }

    /// <summary>
    /// Puts this player in a vehicle as a <see cref="Vehicle" />.
    /// </summary>
    /// <param name="vehicle">The <see cref="Vehicle" /> to put the player in.</param>
    /// <param name="seatId">The seat index (0 = driver).</param>
    public virtual void PutInVehicle(Vehicle vehicle, int seatId)
    {
        ArgumentNullException.ThrowIfNull(vehicle);

        ((IVehicle)vehicle).PutPlayer(_player, seatId);
    }

    /// <summary>
    /// Puts this player in a <see cref="Vehicle" /> as driver.
    /// </summary>
    /// <param name="vehicle">The <see cref="Vehicle" /> to put the player in.</param>
    public virtual void PutInVehicle(Vehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(vehicle);

        PutInVehicle(vehicle, 0);
    }

    /// <summary>
    /// Removes/ejects this player from their <see cref="Vehicle" />.
    /// </summary>
    /// <param name="force">Force the removal of the player when set to <see langword="true" />.</param>
    /// <remarks>
    /// The exiting animation is not synced for other players. This function will not work when used in the OnPlayerEnterVehicle event, because the player
    /// isn't in the vehicle when the callback is called. Use the OnPlayerStateChanged event instead.
    /// </remarks>
    public virtual void RemoveFromVehicle(bool force = false)
    {
        _player.RemoveFromVehicle(force);
    }

    /// <summary>
    /// Freezes or unfreezes this player, preventing or allowing player control.
    /// </summary>
    /// <param name="toggle"><see langword="true" /> to unfreeze the player; <see langword="false" /> to freeze them.</param>
    public virtual void ToggleControllable(bool toggle)
    {
        _player.SetControllable(toggle);
    }

    /// <summary>
    /// Plays the specified <paramref name="soundId" /> for this player at a specific point.
    /// </summary>
    /// <param name="soundId">The sound to play.</param>
    /// <param name="point">Point for the sound to play at as a <see cref="Vector3" />.</param>
    public virtual void PlaySound(int soundId, Vector3 point)
    {
        _player.PlaySound(soundId, point);
    }

    /// <summary>
    /// Plays the specified <paramref name="soundId" /> for this player.
    /// </summary>
    /// <param name="soundId">The sound to play.</param>
    public virtual void PlaySound(int soundId)
    {
        _player.PlaySound(soundId, new Vector3());
    }

    /// <summary>
    /// Applies an animation to this player.
    /// </summary>
    /// <param name="animationLibrary">The name of the animation library.</param>
    /// <param name="animationName">The name of the animation within the library.</param>
    /// <param name="fDelta">The animation speed (typically 4.1).</param>
    /// <param name="loop"><see langword="true" /> to loop the animation; <see langword="false" /> to play it once.</param>
    /// <param name="lockX">If <see langword="false" />, the player returns to their original X position after the animation completes (for moving animations). If <see langword="true" />, the opposite occurs.</param>
    /// <param name="lockY">If <see langword="false" />, the player returns to their original Y position after the animation completes (for moving animations). If <see langword="true" />, the opposite occurs.</param>
    /// <param name="freeze"><see langword="true" /> to freeze the player in position after the animation finishes.</param>
    /// <param name="time">The animation duration. Use <see cref="TimeSpan.Zero" /> for an infinite loop.</param>
    /// <param name="syncType">
    /// The synchronization type to apply to the animation.
    /// <see cref="PlayerAnimationSyncType.NoSync" /> (default) - No synchronization; the player animates themselves.
    /// <see cref="PlayerAnimationSyncType.Sync" /> - Forces the server to sync the animation with all other players in streaming radius. Useful when the player cannot sync the animation themselves (for example, if they are paused).
    /// <see cref="PlayerAnimationSyncType.SyncOthers" /> - Same as <see cref="PlayerAnimationSyncType.Sync" />, but will ONLY apply the animation to streamed-in players, NOT the actual player being animated. Useful for NPC animations and persistent animations when players are being streamed.
    /// </param>
    public virtual void ApplyAnimation(string animationLibrary, string animationName, float fDelta, bool loop, bool lockX, bool lockY, bool freeze, TimeSpan time,
        PlayerAnimationSyncType syncType = PlayerAnimationSyncType.NoSync)
    {
        ArgumentNullException.ThrowIfNull(animationLibrary);
        ArgumentNullException.ThrowIfNull(animationName);

        var anim = new AnimationData(fDelta, loop, lockX, lockY, freeze, (uint)time.TotalMilliseconds, animationLibrary, animationName);

        _player.ApplyAnimation(anim, (OpenMp.Core.Api.PlayerAnimationSyncType)syncType);
    }

    /// <inheritdoc cref="ApplyAnimation(string, string, float, bool, bool, bool, bool, TimeSpan, PlayerAnimationSyncType)" />
    [Obsolete("Use ApplyAnimation(string, string, float, bool, bool, bool, bool, TimeSpan, PlayerAnimationSyncType) instead.")]
    public virtual void ApplyAnimation(string animationLibrary, string animationName, float fDelta, bool loop, bool lockX, bool lockY, bool freeze, int time,
        bool forceSync)
    {
        ApplyAnimation(animationLibrary, animationName, fDelta, loop, lockX, lockY, freeze, TimeSpan.FromMilliseconds(time),
            forceSync ? PlayerAnimationSyncType.Sync : PlayerAnimationSyncType.NoSync);
    }

    /// <inheritdoc cref="ApplyAnimation(string, string, float, bool, bool, bool, bool, TimeSpan, PlayerAnimationSyncType)" />
    [Obsolete("Use ApplyAnimation(string, string, float, bool, bool, bool, bool, TimeSpan, PlayerAnimationSyncType) instead.")]
    public virtual void ApplyAnimation(string animationLibrary, string animationName, float fDelta, bool loop, bool lockX, bool lockY, bool freeze, int time)
    {
        ApplyAnimation(animationLibrary, animationName, fDelta, loop, lockX, lockY, freeze, TimeSpan.FromMilliseconds(time));
    }

    /// <summary>
    /// Clears all animations for this player.
    /// </summary>
    /// <param name="forceSync">Specifies whether the animation should be shown to streamed in players.</param>
    [Obsolete("Use ClearAnimations(PlayerAnimationSyncType) instead")]
    public virtual void ClearAnimations(bool forceSync)
    {
        ClearAnimations(forceSync ? PlayerAnimationSyncType.Sync : PlayerAnimationSyncType.NoSync);
    }

    /// <summary>
    /// Clears all animations for this player.
    /// </summary>
    /// <param name="syncType">
    /// The synchronization type to apply to the animation.
    /// <see cref="PlayerAnimationSyncType.NoSync" /> (default) - No synchronization; the player animates themselves.
    /// <see cref="PlayerAnimationSyncType.Sync" /> - Forces the server to sync the animation with all other players in streaming radius. Useful when the player cannot sync the animation themselves (for example, if they are paused).
    /// <see cref="PlayerAnimationSyncType.SyncOthers" /> - Same as <see cref="PlayerAnimationSyncType.Sync" />, but will ONLY apply the animation to streamed-in players, NOT the actual player being animated. Useful for NPC animations and persistent animations when players are being streamed.
    /// </param>
    public virtual void ClearAnimations(PlayerAnimationSyncType syncType = PlayerAnimationSyncType.NoSync)
    {
        _player.ClearAnimations((OpenMp.Core.Api.PlayerAnimationSyncType)syncType);
    }

    /// <summary>
    /// Gets the library and name of the animation this player is currently playing.
    /// </summary>
    /// <param name="animationLibrary">The animation library name, passed by reference.</param>
    /// <param name="animationName">The animation name, passed by reference.</param>
    /// <returns><see langword="true" /> on success; otherwise, <see langword="false" />.</returns>
    public virtual bool GetAnimationName(out string? animationLibrary, out string? animationName)
    {
        var anim = _player.GetAnimationData();
        var id = anim.ID;
        (animationLibrary, animationName) = Animation.GetAnimation(id);
        return true;
    }

    /// <summary>
    /// Sets a checkpoint (red circle) for this player and displays a red blip on the radar.
    /// </summary>
    /// <remarks>
    /// Checkpoints created on server-created objects will appear on the ground but will still function correctly. There is no workaround for this issue.
    /// </remarks>
    /// <param name="point">The checkpoint position as a <see cref="Vector3" />.</param>
    /// <param name="size">The checkpoint radius.</param>
    public virtual void SetCheckpoint(Vector3 point, float size)
    {
        var cp = CheckpointData.GetCheckpoint();
        cp.SetPosition(ref point);
        cp.SetRadius(size);
        cp.Enable();
    }

    /// <summary>
    /// Disables the current checkpoint for this player.
    /// </summary>
    public virtual void DisableCheckpoint()
    {
        CheckpointData.GetCheckpoint().Disable();
    }

    /// <summary>
    /// Sets a race checkpoint for this player. The <c>OnPlayerEnterCheckpoint</c> is triggered when the player enters.
    /// </summary>
    /// <param name="type">The <see cref="CheckpointType" />.</param>
    /// <param name="point">The checkpoint position as a <see cref="Vector3" />.</param>
    /// <param name="nextPosition">The position of the next checkpoint as a <see cref="Vector3" />, used for arrow direction.</param>
    /// <param name="size">The checkpoint radius (diameter).</param>
    public virtual void SetRaceCheckpoint(CheckpointType type, Vector3 point, Vector3 nextPosition, float size)
    {
        var cp = CheckpointData.GetRaceCheckpoint();
        cp.SetPosition(ref point);
        cp.SetType((RaceCheckpointType)type);
        cp.SetRadius(size);
        cp.SetNextPosition(ref nextPosition);
        cp.Enable();
    }

    /// <summary>
    /// Disables the current race checkpoint for this player.
    /// </summary>
    public virtual void DisableRaceCheckpoint()
    {
        CheckpointData.GetRaceCheckpoint().Disable();
    }

    /// <summary>
    /// Sets the world boundaries for this player. The player cannot travel outside these boundaries.
    /// </summary>
    /// <remarks>
    /// To reset to default boundaries, set all parameters to their default values: xMax=20000, xMin=-20000, yMax=20000, yMin=-20000.
    /// </remarks>
    /// <param name="xMax">The maximum X coordinate.</param>
    /// <param name="xMin">The minimum X coordinate.</param>
    /// <param name="yMax">The maximum Y coordinate.</param>
    /// <param name="yMin">The minimum Y coordinate.</param>
    [Obsolete("Use WorldBounds property instead. Pass values as Vector4(xMax, xMin, yMax, yMin).")]
    public virtual void SetWorldBounds(float xMax, float xMin, float yMax, float yMin)
    {
        WorldBounds = new Vector4(xMax, xMin, yMax, yMin);
    }

    /// <summary>
    /// Changes the color of the specified <paramref name="player"/>'s name tag and radar blip for this player.
    /// </summary>
    /// <param name="player">The <see cref="Player" /> whose marker color will be changed.</param>
    /// <param name="color">The new marker <see cref="Color" />.</param>
    public virtual void SetPlayerMarker(Player player, Color color)
    {
        ArgumentNullException.ThrowIfNull(player);
        _player.SetOtherColour(player, color);
    }

    /// <summary>
    /// Shows or hides the name tag, health bar, and armor bar for another <see cref="Player" />.
    /// </summary>
    /// <remarks>
    /// <see cref="IServerService.ShowNameTags" /> must be enabled for name tags to be visible with this method.
    /// </remarks>
    /// <param name="player">The <see cref="Player" /> whose name tag will be shown or hidden.</param>
    /// <param name="show"><see langword="true" /> to show the name tag; <see langword="false" /> to hide it.</param>
    public virtual void ShowNameTagForPlayer(Player player, bool show)
    {
        ArgumentNullException.ThrowIfNull(player);
        _player.ToggleOtherNameTag(player, show);
    }

    /// <summary>
    /// Sets the direction this player's camera looks at. Use in combination with <see cref="CameraPosition" />.
    /// </summary>
    /// <param name="point">The point the camera should look at.</param>
    /// <param name="cut">The <see cref="CameraCut" /> transition style.</param>
    public virtual void SetCameraLookAt(Vector3 point, CameraCut cut)
    {
        _player.SetCameraLookAt(point, (int)cut);
    }

    /// <summary>
    /// Sets the direction this player's camera looks at. Use in combination with <see cref="CameraPosition" />.
    /// </summary>
    /// <param name="point">The point the camera should look at.</param>
    public virtual void SetCameraLookAt(Vector3 point)
    {
        SetCameraLookAt(point, CameraCut.Cut);
    }

    /// <summary>
    /// Interpolates this player's camera position between two points over a specified duration.
    /// </summary>
    /// <param name="from">The starting position.</param>
    /// <param name="to">The ending position.</param>
    /// <param name="time">The interpolation duration as a <see cref="TimeSpan" />.</param>
    /// <param name="cut">The <see cref="CameraCut" /> transition style. Set to <see cref="CameraCut.Move" /> for smooth movement.</param>
    public virtual void InterpolateCameraPosition(Vector3 from, Vector3 to, TimeSpan time, CameraCut cut)
    {
        _player.InterpolateCameraPosition(from, to, (int)time.TotalMilliseconds, (PlayerCameraCutType)cut);
    }

    /// <inheritdoc cref="InterpolateCameraPosition(Vector3, Vector3, TimeSpan, CameraCut)" />
    [Obsolete("Use the TimeSpan overload. This int-milliseconds variant is kept for source compatibility and will be removed.")]
    public virtual void InterpolateCameraPosition(Vector3 from, Vector3 to, int time, CameraCut cut)
        => InterpolateCameraPosition(from, to, TimeSpan.FromMilliseconds(time), cut);

    /// <summary>
    /// Interpolates this player's camera's look-at point between two positions over a specified duration.
    /// </summary>
    /// <param name="from">The starting look-at position.</param>
    /// <param name="to">The ending look-at position.</param>
    /// <param name="time">The interpolation duration as a <see cref="TimeSpan" />.</param>
    /// <param name="cut">The <see cref="CameraCut" /> transition style. Set to <see cref="CameraCut.Move" /> for smooth interpolation.</param>
    public virtual void InterpolateCameraLookAt(Vector3 from, Vector3 to, TimeSpan time, CameraCut cut)
    {
        _player.InterpolateCameraLookAt(from, to, (int)time.TotalMilliseconds, (PlayerCameraCutType)cut);
    }

    /// <inheritdoc cref="InterpolateCameraLookAt(Vector3, Vector3, TimeSpan, CameraCut)" />
    [Obsolete("Use the TimeSpan overload. This int-milliseconds variant is kept for source compatibility and will be removed.")]
    public virtual void InterpolateCameraLookAt(Vector3 from, Vector3 to, int time, CameraCut cut)
        => InterpolateCameraLookAt(from, to, TimeSpan.FromMilliseconds(time), cut);

    /// <summary>
    /// Determines whether this player is in a specific <see cref="Vehicle" />.
    /// </summary>
    /// <param name="vehicle">The <see cref="Vehicle" /> to check.</param>
    /// <returns><see langword="true" /> if the player is in the <paramref name="vehicle" />; otherwise, <see langword="false" />.</returns>
    public virtual bool IsInVehicle(Vehicle vehicle)
    {
        return Vehicle == vehicle;
    }

    /// <summary>
    /// Enables or disables stunt bonuses for this player.
    /// </summary>
    /// <param name="enable"><see langword="true" /> to enable stunt bonuses; <see langword="false" /> to disable them.</param>
    public virtual void EnableStuntBonus(bool enable)
    {
        _player.UseStuntBonuses(enable);
    }

    /// <summary>
    /// Toggles spectating mode for this player.
    /// </summary>
    /// <remarks>When spectating is disabled, the <c>OnPlayerSpawn</c> event is automatically triggered.</remarks>
    /// <param name="toggle"><see langword="true" /> to enable spectating; <see langword="false" /> to disable.</param>
    public virtual void ToggleSpectating(bool toggle)
    {
        _player.SetSpectating(toggle);
    }

    /// <summary>
    /// Makes this player spectate another <see cref="Player" />.
    /// </summary>
    /// <remarks>Call <see cref="ToggleSpectating" /> before using this method.</remarks>
    /// <param name="targetPlayer">The <see cref="Player" /> to spectate.</param>
    /// <param name="mode">The <see cref="SpectateMode" /> to use.</param>
    public virtual void SpectatePlayer(Player targetPlayer, SpectateMode mode)
    {
        ArgumentNullException.ThrowIfNull(targetPlayer);
        _player.SpectatePlayer(targetPlayer, (PlayerSpectateMode)mode);
    }

    /// <summary>
    /// Makes this player spectate another <see cref="Player" />.
    /// </summary>
    /// <remarks>Call <see cref="ToggleSpectating" /> before using this method.</remarks>
    /// <param name="targetPlayer">The <see cref="Player" /> to spectate.</param>
    public virtual void SpectatePlayer(Player targetPlayer)
    {
        ArgumentNullException.ThrowIfNull(targetPlayer);
        SpectatePlayer(targetPlayer, SpectateMode.Normal);
    }

    /// <summary>
    /// Makes this player spectate a <see cref="Vehicle" /> (see what its driver sees).
    /// </summary>
    /// <remarks>Call <see cref="ToggleSpectating" /> before using this method.</remarks>
    /// <param name="targetVehicle">The <see cref="Vehicle" /> to spectate.</param>
    /// <param name="mode">The <see cref="SpectateMode" /> to use.</param>
    public virtual void SpectateVehicle(Vehicle targetVehicle, SpectateMode mode)
    {
        ArgumentNullException.ThrowIfNull(targetVehicle);
        _player.SpectateVehicle(targetVehicle, (PlayerSpectateMode)mode);
    }

    /// <summary>
    /// Makes this player spectate a <see cref="Vehicle" /> (see what its driver sees).
    /// </summary>
    /// <remarks>Call <see cref="ToggleSpectating" /> before using this method.</remarks>
    /// <param name="targetVehicle">The <see cref="Vehicle" /> to spectate.</param>
    public virtual void SpectateVehicle(Vehicle targetVehicle)
    {
        ArgumentNullException.ThrowIfNull(targetVehicle);
        SpectateVehicle(targetVehicle, SpectateMode.Normal);
    }

    /// <summary>
    /// Starts recording this player's movements to a file, which can be reproduced by an NPC.
    /// </summary>
    /// <param name="recordingType">The <see cref="PlayerRecordingType" />.</param>
    /// <param name="recordingName">
    /// The name of the file to save the recording to. The file will be saved in the scriptfiles folder with a .rec extension added automatically.
    /// </param>
    public virtual void StartRecordingPlayerData(PlayerRecordingType recordingType, string recordingName)
    {
        ArgumentNullException.ThrowIfNull(recordingName);
        RecordingData.Start((OpenMp.Core.Api.PlayerRecordingType)recordingType, recordingName);
    }

    /// <summary>
    /// Stops all player data recordings for this player.
    /// </summary>
    public virtual void StopRecordingPlayerData()
    {
        RecordingData.Stop();
    }

    /// <summary>
    /// Gets the start and end (hit) position of the last bullet this player fired.
    /// </summary>
    /// <param name="origin">The bullet origin position as a <see cref="Vector3" />, passed by reference.</param>
    /// <param name="hitPosition">The bullet hit position as a <see cref="Vector3" />, passed by reference.</param>
    public virtual void GetLastShot(out Vector3 origin, out Vector3 hitPosition)
    {
        var data = _player.GetBulletData();

        origin = data.origin;
        hitPosition = data.hitPos;
    }

    /// <summary>
    /// Sends a message to this player in the chat with a specified <see cref="Color" />.
    /// </summary>
    /// <remarks>
    /// The entire message will be displayed in the specified <paramref name="color" /> unless color embedding is used. Messages longer than 144 characters are automatically split.
    /// </remarks>
    /// <param name="color">The message <see cref="Color" />.</param>
    /// <param name="message">The message text.</param>
    public virtual void SendClientMessage(Color color, string message)
    {
        ArgumentNullException.ThrowIfNull(message);

        Colour clr = color;
        if (message.Length > 144)
        {
            _player.SendClientMessage(ref clr, message[..144]);
            SendClientMessage(color, message[144..]);
        }
        else
        {
            _player.SendClientMessage(ref clr, message);
        }
    }

    /// <summary>
    /// Sends a formatted message to this player in the chat with a specified <see cref="Color" />.
    /// </summary>
    /// <remarks>
    /// The entire message will be displayed in the specified <paramref name="color" /> unless color embedding is used. Messages longer than 144 characters are automatically split.
    /// </remarks>
    /// <param name="color">The message <see cref="Color" />.</param>
    /// <param name="messageFormat">The composite format string (max 144 characters).</param>
    /// <param name="args">The objects to format.</param>
    [StringFormatMethod("messageFormat")]
    public virtual void SendClientMessage(Color color, string messageFormat, params object[] args)
    {
        SendClientMessage(color, string.Format(messageFormat, args));
    }

    /// <summary>
    /// Sends a message to this player in the chat in white.
    /// </summary>
    /// <remarks>
    /// Messages longer than 144 characters are automatically split. Color embedding can be used to add colored text.
    /// </remarks>
    /// <param name="message">The message text.</param>
    public virtual void SendClientMessage(string message)
    {
        SendClientMessage(Color.White, message);
    }

    /// <summary>
    /// Sends a formatted message to this player in the chat in white.
    /// </summary>
    /// <remarks>
    /// Messages longer than 144 characters are automatically split. Color embedding can be used to add colored text.
    /// </remarks>
    /// <param name="messageFormat">The composite format string (max 144 characters).</param>
    /// <param name="args">The objects to format.</param>
    [StringFormatMethod("messageFormat")]
    public virtual void SendClientMessage(string messageFormat, params object[] args)
    {
        SendClientMessage(Color.White, string.Format(messageFormat, args));
    }

    /// <summary>
    /// Kicks this player from the server.
    /// </summary>
    public virtual void Kick()
    {
        _player.Kick();
    }

    /// <summary>
    /// Bans this player. The ban is IP-based and saved in the samp.ban file in the server's root directory.
    /// </summary>
    public virtual void Ban()
    {
        Ban(string.Empty);
    }

    /// <summary>
    /// Bans this player with a reason. The ban is IP-based and saved in the samp.ban file.
    /// </summary>
    /// <param name="reason">The reason for the ban.</param>
    public virtual void Ban(string reason)
    {
        ArgumentNullException.ThrowIfNull(reason);
        _player.Ban(reason);
    }

    /// <summary>
    /// Sends a message in the name of another <see cref="Player" /> to this player.
    /// </summary>
    /// <remarks>
    /// The message appears in the chat box and can only be seen by this player. It starts with the <paramref name="sender" />'s name in their <see cref="Color" />, followed by the <paramref name="message" /> in white.
    /// </remarks>
    /// <param name="sender">The <see cref="Player" /> sending the message.</param>
    /// <param name="message">The message text.</param>
    public virtual void SendPlayerMessageToPlayer(Player sender, string message)
    {
        ArgumentNullException.ThrowIfNull(sender);
        ArgumentNullException.ThrowIfNull(message);
        _player.SendChatMessage(sender, message);
    }

    /// <summary>
    /// Shows 'game text' (on-screen text) for a certain length of time for this player.
    /// </summary>
    /// <param name="text">The text to be displayed.</param>
    /// <param name="time">The duration of the text being shown in milliseconds.</param>
    /// <param name="style">The style of text to be displayed.</param>
    [Obsolete("Obsolete. Use GameText(string,TimeSpan,GameTextStyle) instead.")]
    public virtual void GameText(string text, int time, int style)
    {
        GameText(text, TimeSpan.FromMilliseconds(time), (GameTextStyle)style);
    }

    /// <summary>
    /// Displays on-screen text (game text) for a specified duration.
    /// </summary>
    /// <param name="text">The text to display.</param>
    /// <param name="time">The display duration as a <see cref="TimeSpan" />.</param>
    /// <param name="style">The text style.</param>
    public virtual void GameText(string text, TimeSpan time, GameTextStyle style)
    {
        ArgumentNullException.ThrowIfNull(text);
        _player.SendGameText(text, time, (int)style);
    }

    /// <summary>
    /// Creates an explosion for this player. Only this player sees and is affected by the explosion.
    /// </summary>
    /// <remarks>
    /// This is useful for isolating explosions to specific players or limiting them to specific virtual worlds.
    /// </remarks>
    /// <param name="position">The explosion position as a <see cref="Vector3" />.</param>
    /// <param name="type">The <see cref="ExplosionType" />.</param>
    /// <param name="radius">The explosion radius.</param>
    public virtual void CreateExplosion(Vector3 position, ExplosionType type, float radius)
    {
        _player.CreateExplosion(position, (int)type, radius);
    }

    /// <summary>
    /// Adds a death message to the kill feed on the right side of the screen.
    /// </summary>
    /// <param name="killer">The <see cref="Player" /> who caused the death.</param>
    /// <param name="player">The <see cref="Player" /> who was killed.</param>
    /// <param name="weapon">The <see cref="Weapon" /> used.</param>
    public virtual void SendDeathMessage(Player killer, Player player, Weapon weapon)
    {
        ArgumentNullException.ThrowIfNull(killer);
        ArgumentNullException.ThrowIfNull(player);
        _player.SendDeathMessage(player, killer, (int)weapon);
    }

    /// <summary>
    /// Attaches this player's camera to a <see cref="GlobalObject" />.
    /// </summary>
    /// <param name="object">The <see cref="GlobalObject" /> to attach the camera to.</param>
    public virtual void AttachCameraToObject(GlobalObject @object)
    {
        ArgumentNullException.ThrowIfNull(@object);
        _player.AttachCameraToObject(@object);
    }

    /// <summary>
    /// Attaches this player's camera to a <see cref="PlayerObject" />.
    /// </summary>
    /// <param name="object">The <see cref="PlayerObject" /> to attach the camera to.</param>
    public virtual void AttachCameraToObject(PlayerObject @object)
    {
        ArgumentNullException.ThrowIfNull(@object);
        _player.AttachCameraToObject(@object);
    }

    /// <summary>
    /// Enables edit mode for a <see cref="GlobalObject" /> so this player can modify it.
    /// </summary>
    /// <param name="object">The <see cref="GlobalObject" /> to edit.</param>
    public virtual void Edit(GlobalObject @object)
    {
        ArgumentNullException.ThrowIfNull(@object);
        ObjectData.BeginEditing(@object);
    }

    /// <summary>
    /// Enables edit mode for a <see cref="PlayerObject" /> so this player can modify it.
    /// </summary>
    /// <param name="object">The <see cref="PlayerObject" /> to edit.</param>
    public virtual void Edit(PlayerObject @object)
    {
        ArgumentNullException.ThrowIfNull(@object);
        ObjectData.BeginEditing(@object);
    }

    /// <summary>
    /// Cancels object editing mode for this player.
    /// </summary>
    public virtual void CancelEdit()
    {
        ObjectData.EndEditing();
    }

    /// <summary>
    /// Enables object selection mode for this player.
    /// </summary>
    public virtual void Select()
    {
        ObjectData.BeginSelecting();
    }

    /// <summary>
    /// Removes a standard San Andreas model for this player within a specified range.
    /// </summary>
    /// <param name="modelId">The model identifier.</param>
    /// <param name="position">The position at which to remove the model.</param>
    /// <param name="radius">The radius in which to remove the model.</param>
    [Obsolete("Deprecated. Use 'RemoveDefaultObjects' instead.")]
    public virtual void RemoveBuilding(int modelId, Vector3 position, float radius)
    {
        _player.RemoveDefaultObjects((uint)modelId, position, radius);
    }

    /// <summary>
    /// Removes a standard San Andreas model for this player within a specified range.
    /// </summary>
    /// <param name="modelId">The model ID to remove.</param>
    /// <param name="position">The center position where the model should be removed as a <see cref="Vector3" />.</param>
    /// <param name="radius">The removal radius.</param>
    public virtual void RemoveDefaultObjects(int modelId, Vector3 position, float radius)
    {
        _player.RemoveDefaultObjects((uint)modelId, position, radius);
    }

    /// <summary>
    /// Places an icon/marker on this player's map.
    /// </summary>
    /// <remarks>
    /// This can be used to mark locations such as banks and hospitals.
    /// </remarks>
    /// <param name="iconId">The icon ID for this player (0-99). Maximum 100 icons per <see cref="Player" />.</param>
    /// <param name="position">The icon position as a <see cref="Vector3" />.</param>
    /// <param name="type">The <see cref="MapIcon" /> type.</param>
    /// <param name="color">The marker <see cref="Color" />.</param>
    /// <param name="style">The <see cref="MapIconType" /> style.</param>
    public virtual void SetMapIcon(int iconId, Vector3 position, MapIcon type, Color color, MapIconType style)
    {
        _player.SetMapIcon(iconId, position, (int)type, color, (MapIconStyle)style);
    }

    /// <summary>
    /// Removes a map icon that was previously set for this player.
    /// </summary>
    /// <param name="iconId">The icon ID to remove.</param>
    public virtual void RemoveMapIcon(int iconId)
    {
        _player.UnsetMapIcon(iconId);
    }

    /// <summary>
    /// Hides the game text in the specified style/slot for this player.
    /// </summary>
    /// <param name="style">The style/slot of the game text to hide.</param>
    public virtual void HideGameText(int style)
    {
        _player.HideGameText(style);
    }

    /// <summary>
    /// Gets a value indicating whether the game text in the specified style/slot is currently displayed for this player.
    /// </summary>
    /// <param name="style">The style/slot of the game text to check.</param>
    /// <returns><see langword="true" /> if game text is displayed; otherwise <see langword="false" />.</returns>
    public virtual bool HasGameText(int style)
    {
        return _player.HasGameText(style);
    }

    /// <summary>
    /// Retrieves the currently displayed game text for this player in the specified style/slot.
    /// </summary>
    /// <param name="style">The style/slot of the game text to retrieve.</param>
    /// <param name="message">When this method returns, contains the message text, or <see langword="null" /> if no game text is shown.</param>
    /// <param name="time">When this method returns, contains the duration the text was scheduled to display.</param>
    /// <param name="remaining">When this method returns, contains the remaining display time.</param>
    /// <returns><see langword="true" /> if game text is currently shown; otherwise <see langword="false" />.</returns>
    public virtual bool GetGameText(int style, out string? message, out TimeSpan time, out TimeSpan remaining)
    {
        return _player.GetGameText(style, out message, out time, out remaining);
    }

    /// <summary>
    /// Clears all tasks for this player.
    /// </summary>
    /// <remarks>This is lower-level function related to animation clearing. Generally you should use <see cref="ClearAnimations(PlayerAnimationSyncType)" /> instead.</remarks>
    /// <param name="syncType">
    /// The synchronization type to apply to the animation.
    /// <see cref="PlayerAnimationSyncType.NoSync" /> (default) - No synchronization; the player animates themselves.
    /// <see cref="PlayerAnimationSyncType.Sync" /> - Forces the server to sync the animation with all other players in streaming radius. Useful when the player cannot sync the animation themselves (for example, if they are paused).
    /// <see cref="PlayerAnimationSyncType.SyncOthers" /> - Same as <see cref="PlayerAnimationSyncType.Sync" />, but will ONLY apply the animation to streamed-in players, NOT the actual player being animated. Useful for NPC animations and persistent animations when players are being streamed.
    /// </param>
    public virtual void ClearTasks(PlayerAnimationSyncType syncType)
    {
        _player.ClearTasks((OpenMp.Core.Api.PlayerAnimationSyncType)syncType);
    }

    /// <summary>
    /// Sets the in-game world time (hours-only resolution on the native side) for this player.
    /// </summary>
    /// <param name="time">The world time to set; only the whole-hour portion is applied.</param>
    public virtual void SetWorldTime(TimeSpan time)
    {
        _player.SetWorldTime(time);
    }

    /// <summary>
    /// Sends a command on behalf of this player as if they had typed it themselves.
    /// </summary>
    /// <param name="message">The command line, including the leading slash.</param>
    public virtual void SendCommand(string message)
    {
        ArgumentNullException.ThrowIfNull(message);
        _player.SendCommand(message);
    }

    /// <summary>
    /// Forces this player to be streamed in for the specified <paramref name="target" /> player.
    /// </summary>
    /// <param name="target">The player for whom this player should be streamed in.</param>
    public virtual void StreamInForPlayer(Player target)
    {
        ArgumentNullException.ThrowIfNull(target);
        _player.StreamInForPlayer(target);
    }

    /// <summary>
    /// Forces this player to be streamed out for the specified <paramref name="target" /> player.
    /// </summary>
    /// <param name="target">The player for whom this player should be streamed out.</param>
    public virtual void StreamOutForPlayer(Player target)
    {
        ArgumentNullException.ThrowIfNull(target);
        _player.StreamOutForPlayer(target);
    }

    /// <summary>
    /// Grants or revokes RCON (console) access for this player at runtime.
    /// </summary>
    /// <param name="enable"><see langword="true" /> to grant access; <see langword="false" /> to revoke.</param>
    public virtual void SetConsoleAccessibility(bool enable)
    {
        ConsoleData.SetConsoleAccessibility(enable);
    }

    /// <summary>
    /// Sends a download URL to this player for resolving custom model assets.
    /// </summary>
    /// <param name="url">The URL.</param>
    public virtual void SendDownloadUrl(string url)
    {
        ArgumentNullException.ThrowIfNull(url);
        var data = CustomModelsData ?? throw new InvalidOperationException("Custom models component is not loaded");
        data.SendDownloadUrl(url);
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!IsOmpEntityDestroyed)
        {
            Kick();
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"(Id: {Id}, Name: {Name})";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="Player" /> to <see cref="IPlayer" />.
    /// </summary>
    public static implicit operator IPlayer(Player player)
    {
        return player._player;
    }
}
