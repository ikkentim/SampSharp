using System.Numerics;
using Microsoft.Extensions.Logging;
using SampSharp.OpenMp.Core;
using SampSharp.OpenMp.Core.Api;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace SampSharp.Entities.SAMP;

internal sealed partial class ServerService(SampSharpEnvironment environment, IEntityManager entityManager, ILogger<ServerService> logger) : IServerService
{
    private readonly SafeComponentHandle<IActorsComponent> _actors = environment.SafeComponentHandleProvider.Get<IActorsComponent>();
    private readonly SafeComponentHandle<IClassesComponent> _classes = environment.SafeComponentHandleProvider.Get<IClassesComponent>();
    private readonly SafeComponentHandle<IConsoleComponent> _console = environment.SafeComponentHandleProvider.Get<IConsoleComponent>();
    private readonly SafeComponentHandle<IVehiclesComponent> _vehicles = environment.SafeComponentHandleProvider.Get<IVehiclesComponent>();


    private IConfig Config { get; } = environment.Core.GetConfig();
    private ICore Core { get; } = environment.Core;
    private IPlayerPool Players { get; } = environment.Core.GetPlayers();

    private IActorsComponent Actors => _actors;
    private IClassesComponent Classes => _classes;
    private IConsoleComponent Console => _console;
    private IVehiclesComponent Vehicles => _vehicles;


    public int ActorPoolSize
    {
        get
        {
            var max = -1;

            foreach (var actor in Actors.AsPool())
            {
                var id = actor.GetID();
                if (id > max)
                {
                    max = id;
                }
            }

            return max;
        }
    }

    public int MaxPlayers => Config.GetInt("max_players").Value;

    public int PlayerPoolSize
    {
        get
        {
            var max = -1;

            foreach (var player in Players.Entries())
            {
                var id = player.GetID();
                if (id > max)
                {
                    max = id;
                }
            }

            return max;
        }
    }

    public int TickCount => (int)Core.GetTickCount();
    public int TickRate => (int)Core.TickRate();

    public int VehiclePoolSize
    {
        get
        {
            var max = -1;

            foreach (var vehicle in Vehicles.AsPool())
            {
                var id = vehicle.GetID();
                if (id > max)
                {
                    max = id;
                }
            }

            return max;
        }
    }

    public Class AddPlayerClass(int teamId, int modelId, Vector3 spawnPosition, float angle, Weapon weapon1 = Weapon.None, int weapon1Ammo = 0, Weapon weapon2 = Weapon.None,
        int weapon2Ammo = 0, Weapon weapon3 = Weapon.None, int weapon3Ammo = 0)
    {
        var slots = new WeaponSlotData[WeaponSlots.MAX_WEAPON_SLOTS];
        
        slots[0] = new WeaponSlotData((byte)weapon1, weapon1Ammo);
        slots[1] = new WeaponSlotData((byte)weapon2, weapon2Ammo);
        slots[2] = new WeaponSlotData((byte)weapon3, weapon3Ammo);

        var weapons = new WeaponSlots(slots);

        var @class = Classes.Create(modelId, teamId, spawnPosition, angle, ref weapons);

        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<Class>(entityId, Classes, @class);

        var extension = new ComponentExtension(component);
        @class.AddExtension(extension);

        return component;
    }

    public Class AddPlayerClass(int modelId, Vector3 spawnPosition, float angle, Weapon weapon1 = Weapon.None, int weapon1Ammo = 0, Weapon weapon2 = Weapon.None, int weapon2Ammo = 0,
        Weapon weapon3 = Weapon.None, int weapon3Ammo = 0)
    {
        return AddPlayerClass(OpenMpConstants.TEAM_NONE, modelId, spawnPosition, angle, weapon1, weapon1Ammo, weapon2, weapon2Ammo, weapon3, weapon3Ammo);
    }

    public Class AddPlayerClass(PlayerSpawnData spawnData)
    {
        ArgumentNullException.ThrowIfNull(spawnData);

        var weapons = spawnData.Weapons.ToOmpData();
        var @class = Classes.Create(spawnData.Skin, spawnData.Team, spawnData.Location, spawnData.Angle, ref weapons);

        var entityId = EntityId.NewEntityId();
        var component = entityManager.AddComponent<Class>(entityId, Classes, @class);

        var extension = new ComponentExtension(component);
        @class.AddExtension(extension);

        return component;
    }

    public void BlockIpAddress(string ipAddress, TimeSpan time = default)
    {
        ArgumentNullException.ThrowIfNull(ipAddress);

        var entry = new BanEntry(ipAddress);
        foreach (var network in Core.GetNetworks())
        {
            network.Ban(entry, time);
        }
    }

    public void ConnectNpc(string name, string script)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(script);

        Core.ConnectBot(name, script);
    }

    public void DisableInteriorEnterExits()
    {
        ref var fld = ref Core.GetConfig().GetBool("game.use_entry_exit_markers").Value;
        fld = false;
    }

    public void EnableStuntBonus(bool enable)
    {
        Core.UseStuntBonuses(enable);
    }

    public void EnableVehicleFriendlyFire()
    {
        ref var fld = ref Core.GetConfig().GetBool("game.use_vehicle_friendly_fire").Value;
        fld = false;
    }

    public void GameModeExit()
    {
        SendRconCommand("gmx");
    }

    public bool GetConsoleVarAsBool(string variableName)
    {
        ArgumentNullException.ThrowIfNull(variableName);

        var res = Config.GetNameFromAlias(variableName);

        BlittableRef<bool> v0;
        BlittableRef<int> v1 = default;
        if (!string.IsNullOrEmpty(res.Item2))
        {
            if (res.Item1)
            {
                LogDeprecatedConsoleVariable(variableName, res.Item2);
            }

            v0 = Config.GetBool(res.Item2);

            if (!v0.HasValue)
            {
                v1 = Config.GetInt(res.Item2);
            }
        }
        else
        {
            v0 = Config.GetBool(variableName);

            if (!v0.HasValue)
            {
                v1 = Config.GetInt(variableName);
            }
        }

        if (v0.HasValue)
        {
            return v0.Value;
        }

        if (v1.HasValue)
        {
            LogIntegerRetrievedAsBoolean(variableName);
            return v1.Value != 0;
        }

        return false;
    }

    public int GetConsoleVarAsInt(string variableName)
    {
        ArgumentNullException.ThrowIfNull(variableName);

        var res = Config.GetNameFromAlias(variableName);

        BlittableRef<bool> v0 = default;
        BlittableRef<int> v1;
        if (!string.IsNullOrEmpty(res.Item2))
        {
            if (res.Item1)
            {
                LogDeprecatedConsoleVariable(variableName, res.Item2);
            }

            v1 = Config.GetInt(res.Item2);
            

            if (!v1.HasValue)
            {
                v0 = Config.GetBool(res.Item2);
            }
        }
        else
        {
            v1 = Config.GetInt(variableName);
            
            if (!v1.HasValue)
            {
                v0 = Config.GetBool(variableName);
            }
        }

        if (v1.HasValue)
        {
            return v1.Value;
        }

        if (v0.HasValue)
        {
            LogBooleanRetrievedAsInteger(variableName);
            return v0.Value ? 1 : 0;
        }

        return 0;
    }

    public string? GetConsoleVarAsString(string variableName)
    {
        ArgumentNullException.ThrowIfNull(variableName);

        var gm = variableName.StartsWith("gamemode", StringComparison.Ordinal);
        var res = Config.GetNameFromAlias(gm ? "gamemode" : variableName);

        if (!string.IsNullOrEmpty(res.Item2))
        {
            if (res.Item1)
            {
                LogDeprecatedConsoleVariable(variableName, res.Item2);
            }

            if (gm)
            {
                if (int.TryParse(variableName[8..], out var num))
                {
                    var mainScripts = Config.GetStrings(res.Item2);
                    if (num < mainScripts.Length)
                    {
                        return mainScripts[num];
                    }
                }
            }
            else
            {
                return Config.GetString(res.Item2);
            }
        }

        return Config.GetString(variableName);
    }

    public void LimitGlobalChatRadius(float chatRadius)
    {
        ref var use =  ref Config.GetBool("game.use_chat_radius").Value;
        use = true;
        ref var radius = ref Config.GetFloat("game.chat_radius").Value;
        radius = chatRadius;
    }

    public void LimitPlayerMarkerRadius(float markerRadius)
    {
        ref var use = ref Config.GetBool("game.use_player_marker_draw_radius").Value;
        use = true;
        ref var radius = ref Config.GetFloat("game.player_marker_draw_radius").Value;
        radius = markerRadius;
    }

    public void ManualVehicleEngineAndLights()
    {
        ref var use = ref Config.GetBool("game.use_manual_engine_and_lights").Value;
        use = true;
    }

    public void SendRconCommand(string command)
    {
        ArgumentNullException.ThrowIfNull(command);

        var snd = new ConsoleCommandSenderData(OpenMp.Core.Api.ConsoleCommandSender.Console, 0);
        Console.Send(command, ref snd);
    }

    public void SetGameModeText(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        Core.SetData(SettableCoreDataType.ModeText, text);
    }

    public void SetServerName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        Core.SetData(SettableCoreDataType.ServerName, name);
    }

    public void SetMapName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        Core.SetData(SettableCoreDataType.MapName, name);
    }

    public void SetLanguage(string language)
    {
        ArgumentNullException.ThrowIfNull(language);
        Core.SetData(SettableCoreDataType.Language, language);
    }

    public void SetWebsiteUrl(string url)
    {
        ArgumentNullException.ThrowIfNull(url);
        Core.SetData(SettableCoreDataType.URL, url);
    }

    public void SetServerPassword(string? password)
    {
        Core.SetData(SettableCoreDataType.Password, password ?? string.Empty);
    }

    public void SetAdminPassword(string? password)
    {
        Core.SetData(SettableCoreDataType.AdminPassword, password ?? string.Empty);
    }

    public void SetNameTagDrawDistance(float distance = 70)
    {
        ref var fld = ref Config.GetFloat("game.nametag_draw_radius").Value;
        fld = distance;
    }

    public void SetWorldTime(int hour)
    {
        Core.SetWorldTime(TimeSpan.FromHours(hour));
    }

    public void ShowNameTags(bool show)
    {
        ref var fld = ref Config.GetBool("game.use_nametags").Value;
        fld = show;
    }

    public void ShowPlayerMarkers(PlayerMarkersMode mode)
    {
        ref var fld = ref Config.GetInt("game.player_marker_mode").Value;
        fld = (int)mode;
    }

    public void UnBlockIpAddress(string ipAddress)
    {
        var entry = new BanEntry(ipAddress);
        foreach (var network in Core.GetNetworks())
        {
            network.Unban(entry);
        }
    }

    public void UsePlayerPedAnims()
    {
        ref var fld = ref Config.GetBool("game.use_player_ped_anims").Value;
        fld = true;
    }

    public void SendEmptyDeathMessage()
    {
        Players.SendEmptyDeathMessageToAll();
    }

    public bool IsNameTaken(string name, Player? skip = null)
    {
        ArgumentNullException.ThrowIfNull(name);
        return Players.IsNameTaken(name, skip ?? default(IPlayer));
    }

    public bool IsNameValid(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        return Players.IsNameValid(name);
    }

    public void AllowNickNameCharacter(char character, bool allow)
    {
        Players.AllowNickNameCharacter(character, allow);
    }

    public bool IsNickNameCharacterAllowed(char character)
    {
        return Players.IsNickNameCharacterAllowed(character);
    }

    public Color GetDefaultColor(int playerId)
    {
        return Players.GetDefaultColour(playerId);
    }

    [LoggerMessage(LogLevel.Warning, "Deprecated console variable \"{Old}\", use \"{New}\" instead.")]
    private partial void LogDeprecatedConsoleVariable(string old, string @new);

    [LoggerMessage(LogLevel.Warning, "Integer console variable \"{Name}\" retrieved as boolean.")]
    private partial void LogIntegerRetrievedAsBoolean(string name);

    [LoggerMessage(LogLevel.Warning, "Boolean console variable \"{Name}\" retrieved as integer.")]
    private partial void LogBooleanRetrievedAsInteger(string name);
}