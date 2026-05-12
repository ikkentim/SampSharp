using System.Globalization;
using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal sealed class WorldService(SampSharpEnvironment environment, IEntityManager entityManager, IOmpEntityProvider entityProvider) : IWorldService
{
    private readonly SafeComponentHandle<IActorsComponent> _actors = environment.SafeComponentHandleProvider.Get<IActorsComponent>();
    private readonly SafeComponentHandle<IGangZonesComponent> _gangZones = environment.SafeComponentHandleProvider.Get<IGangZonesComponent>();
    private readonly SafeComponentHandle<IMenusComponent> _menus = environment.SafeComponentHandleProvider.Get<IMenusComponent>();
    private readonly SafeComponentHandle<IObjectsComponent> _objects = environment.SafeComponentHandleProvider.Get<IObjectsComponent>();
    private readonly SafeComponentHandle<IPickupsComponent> _pickups = environment.SafeComponentHandleProvider.Get<IPickupsComponent>();
    private readonly SafeComponentHandle<ITextDrawsComponent> _textDraws = environment.SafeComponentHandleProvider.Get<ITextDrawsComponent>();
    private readonly SafeComponentHandle<ITextLabelsComponent> _textLabels = environment.SafeComponentHandleProvider.Get<ITextLabelsComponent>();
    private readonly SafeComponentHandle<IVehiclesComponent> _vehicles = environment.SafeComponentHandleProvider.Get<IVehiclesComponent>();
    private readonly SafeComponentHandle<INPCComponent> _npcs = environment.SafeComponentHandleProvider.Get<INPCComponent>();

    private ICore Core { get; } = environment.Core;
    private IPlayerPool Players { get; } = environment.Core.GetPlayers();

    private IActorsComponent Actors => _actors;
    private IGangZonesComponent GangZones => _gangZones;
    private IMenusComponent Menus => _menus;
    private IObjectsComponent Objects => _objects;
    private IPickupsComponent Pickups => _pickups;
    private ITextDrawsComponent TextDraws => _textDraws;
    private ITextLabelsComponent TextLabels => _textLabels;
    private IVehiclesComponent Vehicles => _vehicles;
    private INPCComponent Npcs => _npcs;

    public float Gravity
    {
        get => Core.GetGravity();
        set
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(value, -50.0f, nameof(value));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 50.0f, nameof(value));
            Core.SetGravity(value);
        }
    }

    public Actor CreateActor(int modelId, Vector3 position, float rotation, EntityId parent = default)
    {
        var native = Actors.Create(modelId, position, rotation);

        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<Actor>(entityId, parent, Actors, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public Npc CreateNpc(string name, EntityId parent = default)
    {
        if(Npcs == null)
        {
            throw new InvalidOperationException("NPC component not loaded.");
        }

        var native = Npcs.Create(name);

        if (!native.HasValue)
        {
            throw new InvalidOperationException("Failed to create NPC.");
        }

        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<Npc>(entityId, parent, Npcs, native);

        var extension = native.TryGetExtension<ComponentExtension>();
        if (extension is null)
        {
            // Extension should have already been added through OnNPCCreate event
            extension = new ComponentExtension(component);
            native.AddExtension(extension);
        }

        return component;
    }

    public Vehicle CreateVehicle(VehicleModelType type, Vector3 position, float rotation, int color1, int color2, int respawnDelay = -1, bool addSiren = false, EntityId parent = default)
    {
        return CreateVehicle(false, type, position, rotation, color1, color2, respawnDelay, addSiren, parent);
    }

    public Vehicle CreateStaticVehicle(VehicleModelType type, Vector3 position, float rotation, int color1, int color2, int respawnDelay = -1, bool addSiren = false,
        EntityId parent = default)
    {
        return CreateVehicle(true, type, position, rotation, color1, color2, respawnDelay, addSiren, parent);
    }

    public GangZone CreateGangZone(float minX, float minY, float maxX, float maxY, EntityId parent = default)
    {
        return CreateGangZone(new Vector2(minX, minY), new Vector2(maxX, maxY), parent);
    }

    public GangZone CreateGangZone(Vector2 min, Vector2 max, EntityId parent = default)
    {
        var native = GangZones.Create(new GangZonePos(min, max));
        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<GangZone>(entityId, parent, entityProvider, GangZones, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public PlayerGangZone CreatePlayerGangZone(Player owner, Vector2 min, Vector2 max, EntityId parent = default)
    {
        ArgumentNullException.ThrowIfNull(owner);

        var native = GangZones.Create(new GangZonePos(min, max));
        native.SetLegacyPlayer(owner);

        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<PlayerGangZone>(entityId, parent, entityProvider, GangZones, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public void UseGangZoneCheck(BaseGangZone zone, bool enable)
    {
        ArgumentNullException.ThrowIfNull(zone);
        GangZones.UseGangZoneCheck(zone, enable);
    }

    public Pickup CreatePickup(int model, PickupType type, Vector3 position, int virtualWorld = -1, EntityId parent = default)
    {
        var native = Pickups.Create(model, (byte)type, position, (uint)virtualWorld, false);
        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<Pickup>(entityId, parent, Pickups, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public PlayerPickup CreatePlayerPickup(Player owner, int model, PickupType type, Vector3 position, int virtualWorld = -1, EntityId parent = default)
    {
        ArgumentNullException.ThrowIfNull(owner);

        var native = Pickups.Create(model, (byte)type, position, (uint)virtualWorld, false);
        native.SetLegacyPlayer(owner);

        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<PlayerPickup>(entityId, parent, Pickups, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public Pickup CreateStaticPickup(int model, PickupType type, Vector3 position, int virtualWorld = -1, EntityId parent = default)
    {
        var native = Pickups.Create(model, (byte)type, position, (uint)virtualWorld, true);
        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<Pickup>(entityId, parent, Objects, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public GlobalObject CreateObject(int modelId, Vector3 position, Vector3 rotation, float drawDistance = 0, EntityId parent = default)
    {
        var native = Objects.Create(modelId, position, rotation, drawDistance);
        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<GlobalObject>(entityId, parent, entityProvider, Objects, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public PlayerObject CreatePlayerObject(Player player, int modelId, Vector3 position, Vector3 rotation, float drawDistance = 0, EntityId parent = default)
    {
        IPlayer nativePlayer = player;
        if (!nativePlayer.TryQueryExtension<IPlayerObjectData>(out var playerObjectData))
        {
            throw new InvalidOperationException("Missing object data");
        }

        var native = playerObjectData.Create(modelId, position, rotation, drawDistance);
        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<PlayerObject>(entityId, parent, entityProvider, playerObjectData, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public TextLabel CreateTextLabel(string text, Color color, Vector3 position, float drawDistance, int virtualWorld = 0, bool testLos = true, EntityId parent = default)
    {
        ArgumentNullException.ThrowIfNull(text);

        var native = TextLabels.Create(text, color, position, drawDistance, virtualWorld, testLos);
        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<TextLabel>(entityId, parent, entityProvider, TextLabels, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public PlayerTextLabel CreatePlayerTextLabel(Player player, string text, Color color, Vector3 position, float drawDistance, bool testLos = true,
        EntityId parent = default)
    {
        ArgumentNullException.ThrowIfNull(player);
        ArgumentNullException.ThrowIfNull(text);

        IPlayer nativePlayer = player;
        if (!nativePlayer.TryQueryExtension<IPlayerTextLabelData>(out var playerTextLabels))
        {
            throw new InvalidOperationException("Missing text label data");
        }

        var native = playerTextLabels.Create(text, color, position, drawDistance, testLos);
        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<PlayerTextLabel>(entityId, parent, entityProvider, playerTextLabels, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public TextDraw CreateTextDraw(Vector2 position, string text, EntityId parent = default)
    {
        ArgumentNullException.ThrowIfNull(text);

        var native = TextDraws.Create(position, text);
        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<TextDraw>(entityId, parent, TextDraws, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public PlayerTextDraw CreatePlayerTextDraw(Player player, Vector2 position, string text, EntityId parent = default)
    {
        ArgumentNullException.ThrowIfNull(player);
        ArgumentNullException.ThrowIfNull(text);

        IPlayer nativePlayer = player;
        if (!nativePlayer.TryQueryExtension<IPlayerTextDrawData>(out var playerTextDrawData))
        {
            throw new InvalidOperationException("Missing text draw data");
        }

        var native = playerTextDrawData.Create(position, text);
        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<PlayerTextDraw>(entityId, parent, playerTextDrawData, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public Menu CreateMenu(string title, Vector2 position, float col0Width, float? col1Width = null, EntityId parent = default)
    {
        ArgumentNullException.ThrowIfNull(title);

        var native = Menus.Create(title, position, col1Width.HasValue ? (byte)2 : (byte)1, col0Width, col1Width ?? 0);
        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<Menu>(entityId, parent, Menus, native, title);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public void SetObjectsDefaultCameraCollision(bool disable)
    {
        Objects.SetDefaultCameraCollision(disable);
    }

    public void SendClientMessage(Color color, string message)
    {
        ArgumentNullException.ThrowIfNull(message);

        Colour clr = color;
        Players.SendClientMessageToAll(ref clr, message);
    }

    public void SendClientMessage(Color color, string messageFormat, params object[] args)
    {
        var message = string.Format(CultureInfo.InvariantCulture, messageFormat, args);
        SendClientMessage(color, message);
    }

    public void SendClientMessage(string message)
    {
        SendClientMessage(Color.White, message);
    }

    public void SendClientMessage(string messageFormat, params object[] args)
    {
        var message = string.Format(CultureInfo.InvariantCulture, messageFormat, args);
        SendClientMessage(message);
    }

    public void SendPlayerMessageToPlayer(Player sender, string message)
    {
        ArgumentNullException.ThrowIfNull(sender);
        ArgumentNullException.ThrowIfNull(message);
        Players.SendChatMessageToAll(sender, message);
    }

    public void SendDeathMessage(Player killer, Player killee, Weapon weapon)
    {
        Players.SendDeathMessageToAll(killer, killee, (int)weapon);
    }

    public void GameText(string text, int time, int style)
    {
        GameText(text, TimeSpan.FromMilliseconds(time), (GameTextStyle)style);
    }

    public void GameText(string text, TimeSpan time, GameTextStyle style)
    {
        Players.SendGameTextToAll(text, time, (int)style);
    }

    public void HideGameText(GameTextStyle style)
    {
        Players.HideGameTextForAll((int)style);
    }

    public void CreateExplosion(Vector3 position, ExplosionType type, float radius)
    {
        Players.CreateExplosionForAll(position, (int)type, radius);
    }

    public void SetWeather(int weather)
    {
        Core.SetWeather(weather);
    }

    private Vehicle CreateVehicle(bool isStatic, VehicleModelType type, Vector3 position, float rotation, int color1, int color2, int respawnDelay = -1, bool addSiren = false,
        EntityId parent = default)
    {
        var respawnDelaySpan = respawnDelay < 0 ? TimeSpan.Zero : TimeSpan.FromSeconds(respawnDelay);
        var native = Vehicles.Create(isStatic, (int)type, position, rotation, color1, color2, respawnDelaySpan, addSiren);

        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<Vehicle>(entityId, parent, entityProvider, Vehicles, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }
}