using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class WorldService(SampSharpEnvironment environment, IEntityManager entityManager, IOmpEntityProvider entityProvider) : IWorldService
{
    private readonly IActorsComponent _actors = environment.Components.QueryComponent<IActorsComponent>();
    private readonly ICore _core = environment.Core;
    private readonly IGangZonesComponent _gangZones = environment.Components.QueryComponent<IGangZonesComponent>();
    private readonly IMenusComponent _menus = environment.Components.QueryComponent<IMenusComponent>();
    private readonly IObjectsComponent _objects = environment.Components.QueryComponent<IObjectsComponent>();
    private readonly IPickupsComponent _pickups = environment.Components.QueryComponent<IPickupsComponent>();
    private readonly IPlayerPool _players = environment.Core.GetPlayers();
    private readonly ITextDrawsComponent _textDraws = environment.Components.QueryComponent<ITextDrawsComponent>();
    private readonly ITextLabelsComponent _textLabels = environment.Components.QueryComponent<ITextLabelsComponent>();
    private readonly IVehiclesComponent _vehicles = environment.Components.QueryComponent<IVehiclesComponent>();

    public float Gravity
    {
        get => _core.GetGravity();
        set => _core.SetGravity(value);
    }

    public Actor CreateActor(int modelId, Vector3 position, float rotation, EntityId parent = default)
    {
        var native = _actors.Create(modelId, position, rotation);

        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<Actor>(entityId, parent, _actors, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

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
        var native = _gangZones.Create(new GangZonePos(min, max));
        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<GangZone>(entityId, parent, entityProvider, _gangZones, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public PlayerGangZone CreatePlayerGangZone(Player owner, Vector2 min, Vector2 max, EntityId parent = default)
    {
        ArgumentNullException.ThrowIfNull(owner);

        var native = _gangZones.Create(new GangZonePos(min, max));
        native.SetLegacyPlayer(owner);

        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<PlayerGangZone>(entityId, parent, entityProvider, _gangZones, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public void UseGangZoneCheck(BaseGangZone zone, bool enable)
    {
        ArgumentNullException.ThrowIfNull(zone);
        _gangZones.UseGangZoneCheck(zone, enable);
    }

    public Pickup CreatePickup(int model, PickupType type, Vector3 position, int virtualWorld = -1, EntityId parent = default)
    {
        var native = _pickups.Create(model, (byte)type, position, (uint)virtualWorld, false);
        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<Pickup>(entityId, parent, _pickups, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public PlayerPickup CreatePlayerPickup(Player owner, int model, PickupType type, Vector3 position, int virtualWorld = -1, EntityId parent = default)
    {
        ArgumentNullException.ThrowIfNull(owner);

        var native = _pickups.Create(model, (byte)type, position, (uint)virtualWorld, false);
        native.SetLegacyPlayer(owner);

        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<PlayerPickup>(entityId, parent, _pickups, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public Pickup CreateStaticPickup(int model, PickupType type, Vector3 position, int virtualWorld = -1, EntityId parent = default)
    {
        var native = _pickups.Create(model, (byte)type, position, (uint)virtualWorld, true);
        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<Pickup>(entityId, parent, _objects, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public GlobalObject CreateObject(int modelId, Vector3 position, Vector3 rotation, float drawDistance = 0, EntityId parent = default)
    {
        var native = _objects.Create(modelId, position, rotation, drawDistance);
        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<GlobalObject>(entityId, parent, entityProvider, _objects, native);

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

        var native = _textLabels.Create(text, color, position, drawDistance, virtualWorld, testLos);
        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<TextLabel>(entityId, parent, entityProvider, _textLabels, native);

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

        var native = _textDraws.Create(position, text);
        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<TextDraw>(entityId, parent, _textDraws, native);

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

        var native = _menus.Create(title, position, col1Width.HasValue ? (byte)2 : (byte)1, col0Width, col1Width ?? 0);
        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<Menu>(entityId, parent, _menus, native, title);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }

    public void SetObjectsDefaultCameraCollision(bool disable)
    {
        _objects.SetDefaultCameraCollision(disable);
    }

    public void SendClientMessage(Color color, string message)
    {
        ArgumentNullException.ThrowIfNull(message);

        Colour clr = color;
        _players.SendClientMessageToAll(ref clr, message);
    }

    public void SendClientMessage(Color color, string messageFormat, params object[] args)
    {
        var message = string.Format(messageFormat, args);
        SendClientMessage(color, message);
    }

    public void SendClientMessage(string message)
    {
        SendClientMessage(Color.White, message);
    }

    public void SendClientMessage(string messageFormat, params object[] args)
    {
        var message = string.Format(messageFormat, args);
        SendClientMessage(message);
    }

    public void SendPlayerMessageToPlayer(Player sender, string message)
    {
        ArgumentNullException.ThrowIfNull(sender);
        ArgumentNullException.ThrowIfNull(message);
        _players.SendChatMessageToAll(sender, message);
    }

    public void SendDeathMessage(Player killer, Player killee, Weapon weapon)
    {
        _players.SendDeathMessageToAll(killer, killee, (int)weapon);
    }

    public void GameText(string text, int time, int style)
    {
        GameText(text, TimeSpan.FromMilliseconds(time), (GameTextStyle)style);
    }

    public void GameText(string text, TimeSpan time, GameTextStyle style)
    {
        _players.SendGameTextToAll(text, time, (int)style);
    }

    public void HideGameText(GameTextStyle style)
    {
        _players.HideGameTextForAll((int)style);
    }

    public void CreateExplosion(Vector3 position, ExplosionType type, float radius)
    {
        _players.CreateExplosionForAll(position, (int)type, radius);
    }

    public void SetWeather(int weather)
    {
        _core.SetWeather(weather);
    }

    private Vehicle CreateVehicle(bool isStatic, VehicleModelType type, Vector3 position, float rotation, int color1, int color2, int respawnDelay = -1, bool addSiren = false,
        EntityId parent = default)
    {
        var respawnDelaySpan = respawnDelay < 0 ? TimeSpan.Zero : TimeSpan.FromSeconds(respawnDelay);
        var native = _vehicles.Create(isStatic, (int)type, position, rotation, color1, color2, respawnDelaySpan, addSiren);

        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<Vehicle>(entityId, parent, entityProvider, _vehicles, native);

        var extension = new ComponentExtension(component);
        native.AddExtension(extension);

        return component;
    }
}