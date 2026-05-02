using System.Numerics;
using Microsoft.Extensions.Logging;
using SampSharp.OpenMp.Core;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class ServerService : IServerService
{
    private readonly ILogger<ServerService> _logger;
    private readonly IActorsComponent _actors;
    private readonly IPlayerPool _players;
    private readonly IConfig _config;
    private readonly ICore _core;
    private readonly IVehiclesComponent _vehicles;
    private readonly IClassesComponent _classes;
    private readonly IConsoleComponent _console;

    public ServerService(SampSharpEnvironment environment, ILogger<ServerService> logger)
    {
        _logger = logger;
        _actors = environment.Components.QueryComponent<IActorsComponent>();
        _config = environment.Core.GetConfig();
        _players = environment.Core.GetPlayers();
        _vehicles = environment.Components.QueryComponent<IVehiclesComponent>();
        _classes = environment.Components.QueryComponent<IClassesComponent>();
        _console = environment.Components.QueryComponent<IConsoleComponent>();
        _core = environment.Core;
    }

    public int ActorPoolSize
    {
        get
        {
            var max = -1;

            foreach (var actor in _actors.AsPool())
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

    public int MaxPlayers => _config.GetInt("max_players").Value;

    public int PlayerPoolSize
    {
        get
        {
            var max = -1;

            foreach (var player in _players.Entries())
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

    public int TickCount => (int)_core.GetTickCount();
    public int TickRate => (int)_core.TickRate();

    public int VehiclePoolSize
    {
        get
        {
            var max = -1;

            foreach (var vehicle in _vehicles.AsPool())
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

    // TODO: convert classes to components

    public int AddPlayerClass(int teamId, int modelId, Vector3 spawnPosition, float angle, Weapon weapon1 = Weapon.None, int weapon1Ammo = 0, Weapon weapon2 = Weapon.None,
        int weapon2Ammo = 0, Weapon weapon3 = Weapon.None, int weapon3Ammo = 0)
    {
        var slots = new WeaponSlotData[WeaponSlots.MAX_WEAPON_SLOTS];
        
        slots[0] = new WeaponSlotData((byte)weapon1, weapon1Ammo);
        slots[1] = new WeaponSlotData((byte)weapon2, weapon2Ammo);
        slots[2] = new WeaponSlotData((byte)weapon3, weapon3Ammo);

        var weapons = new WeaponSlots(slots);

        var @class = _classes.Create(modelId, teamId, spawnPosition, angle, ref weapons);

        return @class.GetID();
    }

    public int AddPlayerClass(int modelId, Vector3 spawnPosition, float angle, Weapon weapon1 = Weapon.None, int weapon1Ammo = 0, Weapon weapon2 = Weapon.None, int weapon2Ammo = 0,
        Weapon weapon3 = Weapon.None, int weapon3Ammo = 0)
    {
        return AddPlayerClass(OpenMpConstants.TEAM_NONE, modelId, spawnPosition, angle, weapon1, weapon1Ammo, weapon2, weapon2Ammo, weapon3, weapon3Ammo);
    }

    public void BlockIpAddress(string ipAddress, TimeSpan time = default)
    {
        ArgumentNullException.ThrowIfNull(ipAddress);

        var entry = new BanEntry(ipAddress);
        foreach (var network in _core.GetNetworks())
        {
            network.Ban(entry, time);
        }
    }

    public void ConnectNpc(string name, string script)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(script);

        _core.ConnectBot(name, script);
    }

    public void DisableInteriorEnterExits()
    {
        ref var fld = ref _core.GetConfig().GetBool("game.use_entry_exit_markers").Value;
        fld = false;
    }

    public void EnableStuntBonus(bool enable)
    {
        _core.UseStuntBonuses(enable);
    }

    public void EnableVehicleFriendlyFire()
    {
        ref var fld = ref _core.GetConfig().GetBool("game.use_vehicle_friendly_fire").Value;
        fld = false;
    }

    public void GameModeExit()
    {
        SendRconCommand("gmx");
    }

    public bool GetConsoleVarAsBool(string variableName)
    {
        ArgumentNullException.ThrowIfNull(variableName);

        var res = _config.GetNameFromAlias(variableName);

        BlittableRef<bool> v0;
        BlittableRef<int> v1 = default;
        if (!string.IsNullOrEmpty(res.Item2))
        {
            if (res.Item1)
            {
                _logger.LogWarning("Deprecated console variable \"{old}\", use \"{new}\" instead.", variableName, res.Item2);
            }

            v0 = _config.GetBool(res.Item2);

            if (!v0.HasValue)
            {
                v1 = _config.GetInt(res.Item2);
            }
        }
        else
        {
            v0 = _config.GetBool(variableName);

            if (!v0.HasValue)
            {
                v1 = _config.GetInt(variableName);
            }
        }

        if (v0.HasValue)
        {
            return v0.Value;
        }

        if (v1.HasValue)
        {
            _logger.LogWarning( "Integer console variable \"{name}\" retrieved as boolean.", variableName);
            return v1.Value != 0;
        }

        return false;
    }

    public int GetConsoleVarAsInt(string variableName)
    {
        ArgumentNullException.ThrowIfNull(variableName);

        var res = _config.GetNameFromAlias(variableName);

        BlittableRef<bool> v0 = default;
        BlittableRef<int> v1;
        if (!string.IsNullOrEmpty(res.Item2))
        {
            if (res.Item1)
            {
                _logger.LogWarning("Deprecated console variable \"{old}\", use \"{new}\" instead.", variableName, res.Item2);
            }

            v1 = _config.GetInt(res.Item2);
            

            if (!v1.HasValue)
            {
                v0 = _config.GetBool(res.Item2);
            }
        }
        else
        {
            v1 = _config.GetInt(variableName);
            
            if (!v1.HasValue)
            {
                v0 = _config.GetBool(variableName);
            }
        }

        if (v1.HasValue)
        {
            return v1.Value;
        }

        if (v0.HasValue)
        {
            _logger.LogWarning( "Boolean console variable \"{name}\" retrieved as integer.", variableName);
            return v0.Value ? 1 : 0;
        }

        return 0;
    }

    public string? GetConsoleVarAsString(string variableName)
    {
        ArgumentNullException.ThrowIfNull(variableName);

        var gm = variableName.StartsWith("gamemode");
        var res = _config.GetNameFromAlias(gm ? "gamemode" : variableName);

        if (!string.IsNullOrEmpty(res.Item2))
        {
            if (res.Item1)
            {
                _logger.LogWarning("Deprecated console variable \"{old}\", use \"{new}\" instead.", variableName, res.Item2);
            }

            if (gm)
            {
                if (int.TryParse(variableName[8..], out var num))
                {
                    var mainScripts = _config.GetStrings(res.Item2);
                    if (num < mainScripts.Length)
                    {
                        return mainScripts[num];
                    }
                }
            }
            else
            {
                return _config.GetString(res.Item2);
            }
        }

        return _config.GetString(variableName);
    }

    public void LimitGlobalChatRadius(float chatRadius)
    {
        ref var use =  ref _config.GetBool("game.use_chat_radius").Value;
        use = true;
        ref var radius = ref _config.GetFloat("game.chat_radius").Value;
        radius = chatRadius;
    }

    public void LimitPlayerMarkerRadius(float markerRadius)
    {
        ref var use = ref _config.GetBool("game.use_player_marker_draw_radius").Value;
        use = true;
        ref var radius = ref _config.GetFloat("game.player_marker_draw_radius").Value;
        radius = markerRadius;
    }

    public void ManualVehicleEngineAndLights()
    {
        ref var use = ref _config.GetBool("game.use_manual_engine_and_lights").Value;
        use = true;
    }

    public void SendRconCommand(string command)
    {
        ArgumentNullException.ThrowIfNull(command);

        var snd = new ConsoleCommandSenderData(OpenMp.Core.Api.ConsoleCommandSender.Console, 0);
        _console.Send(command, ref snd);
    }

    public void SetGameModeText(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        _core.SetData(SettableCoreDataType.ModeText, text);
    }

    public void SetServerName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        _core.SetData(SettableCoreDataType.ServerName, name);
    }

    public void SetMapName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        _core.SetData(SettableCoreDataType.MapName, name);
    }

    public void SetLanguage(string language)
    {
        ArgumentNullException.ThrowIfNull(language);
        _core.SetData(SettableCoreDataType.Language, language);
    }

    public void SetWebsiteUrl(string url)
    {
        ArgumentNullException.ThrowIfNull(url);
        _core.SetData(SettableCoreDataType.URL, url);
    }

    public void SetServerPassword(string? password)
    {
        _core.SetData(SettableCoreDataType.Password, password ?? string.Empty);
    }

    public void SetAdminPassword(string? password)
    {
        _core.SetData(SettableCoreDataType.AdminPassword, password ?? string.Empty);
    }

    public void SetNameTagDrawDistance(float distance = 70)
    {
        ref var fld = ref _config.GetFloat("game.nametag_draw_radius").Value;
        fld = distance;
    }

    public void SetWorldTime(int hour)
    {
        _core.SetWorldTime(TimeSpan.FromHours(hour));
    }

    public void ShowNameTags(bool show)
    {
        ref var fld = ref _config.GetBool("game.use_nametags").Value;
        fld = show;
    }

    public void ShowPlayerMarkers(PlayerMarkersMode mode)
    {
        ref var fld = ref _config.GetInt("game.player_marker_mode").Value;
        fld = (int)mode;
    }

    public void UnBlockIpAddress(string ipAddress)
    {
        var entry = new BanEntry(ipAddress);
        foreach (var network in _core.GetNetworks())
        {
            network.Unban(entry);
        }
    }

    public void UsePlayerPedAnims()
    {
        ref var fld = ref _config.GetBool("game.use_player_ped_anims").Value;
        fld = true;
    }

    public void HideGameText(int style)
    {
        _players.HideGameTextForAll(style);
    }

    public void SendEmptyDeathMessage()
    {
        _players.SendEmptyDeathMessageToAll();
    }

    public bool IsNameTaken(string name, Player? skip = null)
    {
        ArgumentNullException.ThrowIfNull(name);
        return _players.IsNameTaken(name, skip ?? default(IPlayer));
    }

    public bool IsNameValid(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        return _players.IsNameValid(name);
    }

    public void AllowNickNameCharacter(char character, bool allow)
    {
        _players.AllowNickNameCharacter(character, allow);
    }

    public bool IsNickNameCharacterAllowed(char character)
    {
        return _players.IsNickNameCharacterAllowed(character);
    }

    public Color GetDefaultColor(int playerId)
    {
        return _players.GetDefaultColour(playerId);
    }
}