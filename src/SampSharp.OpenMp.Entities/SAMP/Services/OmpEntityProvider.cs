using SampSharp.OpenMp.Core.Api;
using INPC = SampSharp.OpenMp.Core.Api.INPC;
using INPCComponent = SampSharp.OpenMp.Core.Api.INPCComponent;

namespace SampSharp.Entities.SAMP;

internal sealed class OmpEntityProvider(SampSharpEnvironment environment, IEntityManager entityManager) : IOmpEntityProvider
{
    private readonly SafeComponentHandle<IActorsComponent> _actors = environment.SafeComponentHandleProvider.Get<IActorsComponent>();
    private readonly SafeComponentHandle<IClassesComponent> _classes = environment.SafeComponentHandleProvider.Get<IClassesComponent>();
    private readonly SafeComponentHandle<IGangZonesComponent> _gangZones = environment.SafeComponentHandleProvider.Get<IGangZonesComponent>();
    private readonly SafeComponentHandle<IMenusComponent> _menus = environment.SafeComponentHandleProvider.Get<IMenusComponent>();
    private readonly SafeComponentHandle<INPCComponent> _npcs = environment.SafeComponentHandleProvider.Get<INPCComponent>();
    private readonly SafeComponentHandle<IObjectsComponent> _objects = environment.SafeComponentHandleProvider.Get<IObjectsComponent>();
    private readonly SafeComponentHandle<IPickupsComponent> _pickups = environment.SafeComponentHandleProvider.Get<IPickupsComponent>();
    private readonly SafeComponentHandle<ITextDrawsComponent> _textDraws = environment.SafeComponentHandleProvider.Get<ITextDrawsComponent>();
    private readonly SafeComponentHandle<ITextLabelsComponent> _textLabels = environment.SafeComponentHandleProvider.Get<ITextLabelsComponent>();
    private readonly SafeComponentHandle<IVehiclesComponent> _vehicles = environment.SafeComponentHandleProvider.Get<IVehiclesComponent>();

    private IActorsComponent Actors => _actors;
    private IClassesComponent Classes => _classes;
    private IGangZonesComponent GangZones => _gangZones;
    private IMenusComponent Menus => _menus;
    private INPCComponent Npcs => _npcs;
    private IObjectsComponent Objects => _objects;
    private IPickupsComponent Pickups => _pickups;
    private IPlayerPool Players { get; } = environment.Core.GetPlayers();
    private ITextDrawsComponent TextDraws => _textDraws;
    private ITextLabelsComponent TextLabels => _textLabels;
    private IVehiclesComponent Vehicles => _vehicles;

    public EntityId GetEntity(IActor actor)
    {
        return GetComponent(actor)?.Entity ?? default;
    }

    public EntityId GetEntity(INPC npc)
    {
        return GetComponent(npc)?.Entity ?? default;
    }

    public EntityId GetEntity(IGangZone gangZone)
    {
        return GetComponent(gangZone)?.Entity ?? default;
    }

    public EntityId GetEntity(IMenu menu)
    {
        return GetComponent(menu)?.Entity ?? default;
    }

    public EntityId GetEntity(IObject @object)
    {
        return GetComponent(@object)?.Entity ?? default;
    }

    public EntityId GetEntity(IPickup pickup)
    {
        return GetComponent(pickup)?.Entity ?? default;
    }

    public EntityId GetEntity(IPlayer player)
    {
        return GetComponent(player)?.Entity ?? default;
    }

    public EntityId GetEntity(IPlayerObject playerObject, IPlayer player = default)
    {
        return GetComponent(playerObject, player)?.Entity ?? default;
    }

    public EntityId GetEntity(IPlayerTextDraw playerTextDraw, IPlayer player = default)
    {
        return GetComponent(playerTextDraw, player)?.Entity ?? default;
    }

    public EntityId GetEntity(IPlayerTextLabel playerTextLabel, IPlayer player = default)
    {
        return GetComponent(playerTextLabel, player)?.Entity ?? default;
    }

    public EntityId GetEntity(ITextDraw textDraw)
    {
        return GetComponent(textDraw)?.Entity ?? default;
    }

    public EntityId GetEntity(ITextLabel textLabel)
    {
        return GetComponent(textLabel)?.Entity ?? default;
    }

    public EntityId GetEntity(IVehicle vehicle)
    {
        return GetComponent(vehicle)?.Entity ?? default;
    }

    public EntityId GetEntity(IClass playerClass)
    {
        return GetComponent(playerClass)?.Entity ?? default;
    }

    public Class? GetComponent(IClass playerClass)
    {
        if (playerClass == null)
        {
            return null;
        }

        var ext = playerClass.TryGetExtension<ComponentExtension>();
        if (ext == null)
        {
            var component = entityManager.AddComponent<Class>(EntityId.NewEntityId(), Classes, playerClass);
            ext = new ComponentExtension(component);
            playerClass.AddExtension(ext);

            return component;
        }

        return (Class)ext.Component;
    }

    public Actor? GetComponent(IActor actor)
    {
        if (actor == null)
        {
            return null;
        }

        var ext = actor.TryGetExtension<ComponentExtension>();
        if (ext == null)
        {
            var component = entityManager.AddComponent<Actor>(EntityId.NewEntityId(), Actors, actor);
            ext = new ComponentExtension(component);
            actor.AddExtension(ext);

            return component;
        }

        return (Actor)ext.Component;
    }

    public Npc? GetComponent(INPC npc)
    {
        if (npc == null)
        {
            return null;
        }

        var ext = npc.TryGetExtension<ComponentExtension>();
        if (ext != null)
        {
            return (Npc)ext.Component;
        }

        var component = entityManager.AddComponent<Npc>(EntityId.NewEntityId(), Npcs, npc);
        ext = new ComponentExtension(component);
        npc.AddExtension(ext);
        return component;
    }

    public BaseGangZone? GetComponent(IGangZone gangZone)
    {
        if (gangZone == null)
        {
            return null;
        }

        var ext = gangZone.TryGetExtension<ComponentExtension>();
        if (ext == null)
        {
            BaseGangZone component = gangZone.GetLegacyPlayer().HasValue
                ? entityManager.AddComponent<PlayerGangZone>(EntityId.NewEntityId(), this, GangZones, gangZone)
                : entityManager.AddComponent<GangZone>(EntityId.NewEntityId(), this, GangZones, gangZone);
            ext = new ComponentExtension(component);
            gangZone.AddExtension(ext);

            return component;
        }

        return (BaseGangZone)ext.Component;
    }

    public Menu? GetComponent(IMenu menu)
    {
        if (menu == null)
        {
            return null;
        }

        var ext = menu.TryGetExtension<ComponentExtension>();
        if (ext == null)
        {
            // don't know the title of the menu (which cannot be retrieved through open.mp api) - cannot create a component for the foreign entity.
            return null;
        }

        return (Menu)ext.Component;
    }

    public GlobalObject? GetComponent(IObject @object)
    {
        if (@object == null)
        {
            return null;
        }

        var ext = @object.TryGetExtension<ComponentExtension>();
        if (ext == null)
        {
            var component = entityManager.AddComponent<GlobalObject>(EntityId.NewEntityId(), this, Objects, @object);
            ext = new ComponentExtension(component);
            @object.AddExtension(ext);

            return component;
        }

        return (GlobalObject)ext.Component;
    }

    public BasePickup? GetComponent(IPickup pickup)
    {
        if (pickup == null)
        {
            return null;
        }

        var ext = pickup.TryGetExtension<ComponentExtension>();
        if (ext == null)
        {
            var legacyPlayer = pickup.GetLegacyPlayer();
            BasePickup component = legacyPlayer.HasValue
                ? entityManager.AddComponent<PlayerPickup>(EntityId.NewEntityId(), Pickups, pickup, GetComponent(legacyPlayer)!)
                : entityManager.AddComponent<Pickup>(EntityId.NewEntityId(), Pickups, pickup);
            ext = new ComponentExtension(component);
            pickup.AddExtension(ext);

            return component;
        }

        return (BasePickup)ext.Component;
    }

    public Player? GetComponent(IPlayer player)
    {
        if (player == null)
        {
            return null;
        }

        var ext = player.TryGetExtension<ComponentExtension>();
        if (ext == null)
        {
            var component = entityManager.AddComponent<Player>(EntityId.NewEntityId(), this, player);
            ext = new ComponentExtension(component);
            player.AddExtension(ext);
            return component;
        }

        return (Player)ext.Component;
    }

    public PlayerObject? GetComponent(IPlayerObject playerObject, IPlayer player = default)
    {
        if (playerObject == null)
        {
            return null;
        }

        var ext = playerObject.TryGetExtension<ComponentExtension>();
        if (ext == null)
        {
            if (player == null)
            {
                // don't know for which player this object is created - cannot create a component for the foreign entity.
                return null;
            }

            if (!player.TryQueryExtension<IPlayerObjectData>(out var data))
            {
                return null;
            }

            var component = entityManager.AddComponent<PlayerObject>(EntityId.NewEntityId(), this, data, playerObject);
            ext = new ComponentExtension(component);
            playerObject.AddExtension(ext);
            return component;
        }

        return (PlayerObject)ext.Component;
    }

    public PlayerTextDraw? GetComponent(IPlayerTextDraw playerTextDraw, IPlayer player = default)
    {
        if (playerTextDraw == null)
        {
            return null;
        }

        var ext = playerTextDraw.TryGetExtension<ComponentExtension>();
        if (ext == null)
        {
            if (player == null)
            {
                // don't know for which player this text draw is created - cannot create a component for the foreign entity.
                return null;
            }

            if (!player.TryQueryExtension<IPlayerTextDrawData>(out var data))
            {
                return null;
            }

            var component = entityManager.AddComponent<PlayerTextDraw>(EntityId.NewEntityId(), data, playerTextDraw);
            ext = new ComponentExtension(component);
            playerTextDraw.AddExtension(ext);
            return component;
        }

        return (PlayerTextDraw)ext.Component;
    }

    public PlayerTextLabel? GetComponent(IPlayerTextLabel playerTextLabel, IPlayer player = default)
    {
        if (playerTextLabel == null)
        {
            return null;
        }

        var ext = playerTextLabel.TryGetExtension<ComponentExtension>();
        if (ext == null)
        {
            if (player == null)
            {
                // don't know for which player this text label is created - cannot create a component for the foreign entity.
                return null;
            }

            if (!player.TryQueryExtension<IPlayerTextLabelData>(out var data))
            {
                return null;
            }

            var component =
                entityManager.AddComponent<PlayerTextLabel>(EntityId.NewEntityId(), this, data, playerTextLabel);
            ext = new ComponentExtension(component);
            playerTextLabel.AddExtension(ext);
            return component;
        }

        return (PlayerTextLabel)ext.Component;
    }

    public TextDraw? GetComponent(ITextDraw textDraw)
    {
        if (textDraw == null)
        {
            return null;
        }

        var ext = textDraw.TryGetExtension<ComponentExtension>();
        if (ext == null)
        {
            var component = entityManager.AddComponent<TextDraw>(EntityId.NewEntityId(), TextDraws, textDraw);
            ext = new ComponentExtension(component);
            textDraw.AddExtension(ext);

            return component;
        }

        return (TextDraw)ext.Component;
    }

    public TextLabel? GetComponent(ITextLabel textLabel)
    {
        if (textLabel == null)
        {
            return null;
        }

        var ext = textLabel.TryGetExtension<ComponentExtension>();
        if (ext == null)
        {
            var component = entityManager.AddComponent<TextLabel>(EntityId.NewEntityId(), this, TextLabels, textLabel);
            ext = new ComponentExtension(component);
            textLabel.AddExtension(ext);

            return component;
        }

        return (TextLabel)ext.Component;
    }

    public Vehicle? GetComponent(IVehicle vehicle)
    {
        if (vehicle == null)
        {
            return null;
        }

        var ext = vehicle.TryGetExtension<ComponentExtension>();

        if (ext == null)
        {
            var component = entityManager.AddComponent<Vehicle>(EntityId.NewEntityId(), this, Vehicles, vehicle);
            ext = new ComponentExtension(component);
            vehicle.AddExtension(ext);

            return component;
        }

        return (Vehicle)ext.Component;
    }

    public Class? GetPlayerClass(int id)
    {
        return GetComponent(Classes.AsPool().Get(id));
    }

    public Actor? GetActor(int id)
    {
        return GetComponent(Actors.AsPool().Get(id));
    }

    public Npc? GetNpc(int id)
    {
        if (!Npcs.HasValue)
        {
            return null;
        }

        return GetComponent(Npcs.Get(id));
    }

    public BaseGangZone? GetGangZone(int id)
    {
        return GetComponent(GangZones.AsPool().Get(id));
    }

    public BasePickup? GetPickup(int id)
    {
        return GetComponent(Pickups.AsPool().Get(id));
    }

    public Player? GetPlayer(int id)
    {
        return GetComponent(Players.Get(id));
    }

    public PlayerObject? GetPlayerObject(IPlayer player, int id)
    {
        if (!player.TryQueryExtension<IPlayerObjectData>(out var data))
        {
            return null;
        }
        return GetComponent(data.Get(id), player);
    }

    public PlayerTextDraw? GetPlayerTextDraw(IPlayer player, int id)
    {
        if (!player.TryQueryExtension<IPlayerTextDrawData>(out var data))
        {
            return null;
        }
        return GetComponent(data.Get(id), player);
    }

    public PlayerTextLabel? GetPlayerTextLabel(IPlayer player, int id)
    {
        if (!player.TryQueryExtension<IPlayerTextLabelData>(out var data))
        {
            return null;
        }
        return GetComponent(data.Get(id), player);
    }

    public TextDraw? GetTextDraw(int id)
    {
        return GetComponent(TextDraws.AsPool().Get(id));
    }

    public TextLabel? GetTextLabel(int id)
    {
        return GetComponent(TextLabels.AsPool().Get(id));
    }

    public Vehicle? GetVehicle(int id)
    {
        return GetComponent(Vehicles.AsPool().Get(id));
    }

    public GlobalObject? GetObject(int id)
    {
        return GetComponent(Objects.AsPool().Get(id));
    }

    public Menu? GetMenu(int id)
    {
        return GetComponent(Menus.AsPool().Get(id));
    }
}