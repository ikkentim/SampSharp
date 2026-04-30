using System.Net;
using System.Numerics;
using SampSharp.OpenMp.Core;
using SampSharp.OpenMp.Core.Api;
using SampSharp.OpenMp.Core.RobinHood;
using SampSharp.OpenMp.Core.Std;
using SampSharp.OpenMp.Core.Std.Chrono;

namespace TestMode.OpenMp.Core;

public class Startup : IStartup,
    ICoreEventHandler,
    IConsoleEventHandler, 
    IPlayerConnectEventHandler,
    IPoolEventHandler<IPlayer>
{
    public void Initialize(IStartupContext context)
    {
        context.UseOpenMpLogger();

        Console.WriteLine("OnInit from managed c# code!");

        Console.WriteLine($"Component version: {context.Info.Version}");
        Console.WriteLine($"size {context.Info.Size.Value} api {context.Info.ApiVersion}");
        Console.WriteLine($"Network bit stream version: {context.Core.GetNetworkBitStreamVersion()}");

        Console.WriteLine($"core version: {context.Core.GetVersion()}");

        var cfg = context.Core.GetConfig();

        context.Core.GetPlayers().GetPlayerConnectDispatcher().AddEventHandler(this);

        // test config
        var name = cfg.GetString("name");
        var announce = cfg.GetBool("announce");
        var lanMode = cfg.GetBool("network.use_lan_mode");
        var inputFilter = cfg.GetBool("chat_input_filter");
        Console.WriteLine($"name: {name} announce: {announce} use_lan_mode: {lanMode} chat_input_filter: {inputFilter}");

        // test bans

        cfg.AddBan(new BanEntry("1.2.3.5", DateTimeOffset.UtcNow, "name", "reason"));
        cfg.AddBan(new BanEntry("1.2.3.6", DateTimeOffset.UtcNow, "name", "reason2"));
        cfg.WriteBans();

        Console.WriteLine("written ban");

        for (nint i = 0; i < cfg.GetBansCount().Value; i++)
        {
            var b = cfg.GetBan(new Size(i))!;
            Console.WriteLine($"ban: {b.Name} {b.Address} {b.Reason} {b.Time}");
        }

        var alias = cfg.GetNameFromAlias("rcon");
        Console.WriteLine($"rcon alias: {alias}");

        var vehiclesComponent = context.ComponentList.QueryComponent<IVehiclesComponent>();

        // test handlers
        var dispatcher = context.Core.GetEventDispatcher();
        Console.WriteLine($"COUNT before:::::::::::::::::::::::::::: {dispatcher.Count()}");
        dispatcher.AddEventHandler(this);
        Console.WriteLine($"COUNT after:::::::::::::::::::::::::::: {dispatcher.Count()}");

        context.ComponentList.QueryComponent<IConsoleComponent>().GetEventDispatcher().AddEventHandler(this);

        var v = vehiclesComponent.Create(false, 401, new Vector3(5, 0, 10));
        v.GetColour(out var vcol);
        Console.WriteLine($"vehicle color: <1: {vcol.First}, 2: {vcol.Second}>");

        Console.WriteLine("add extension");
        v.AddExtension(new Nickname("Brum"));

        Console.WriteLine("get extension");
        var nick = v.GetExtension<Nickname>();
        Console.WriteLine((nick.ToString()));

        Console.WriteLine("remove extension");
        v.RemoveExtension(nick);

        Console.WriteLine("get extension");
        nick = v.TryGetExtension<Nickname>();
        Console.WriteLine((nick?.ToString() ?? "null"));

        
        var playerPoolEventDispatcher = context.Core.GetPlayers().GetPoolEventDispatcher();
        Console.WriteLine("count before " + playerPoolEventDispatcher.Count());
        playerPoolEventDispatcher.AddEventHandler(this);
        Console.WriteLine("count after " + playerPoolEventDispatcher.Count());

        var tds = context.ComponentList.QueryComponent<ITextDrawsComponent>();
        var txd = tds.Create(new Vector2(0, 0), 98);
        var txd2 = tds.Create(new Vector2(0, 0), 99);
        var txt = txd.GetText();
        Console.WriteLine($"textdraw text: '{txt ?? "<<null>>"}'");
        Console.WriteLine($"default plate: '{v.GetPlate() ?? "<<null>>"}'");

        Console.WriteLine($"id of textdraw: {txd.GetID()}, {txd2.GetID()}");


        Console.WriteLine("<write>");
        context.Core.PrintLine("Hello, World!");
        context.Core.LogLine(LogLevel.Error, "Hello, World!");
        Console.WriteLine("</write>");
        // try
        // {
        //     throw new Exception("awful");
        // }
        // catch(Exception e)
        // {
        //     SampSharpExceptionHandler.HandleException("test", e);
        // }

        var pool = vehiclesComponent.AsPool();

        var vehCount = pool.Count();
        
        Console.WriteLine($"Vehicle count: {vehCount.ToInt32()}");

        Console.WriteLine("Vehicle iterator begin");
        foreach (var vehicle in pool)
        {
            Console.WriteLine($"id: {vehicle.GetID()} model: {vehicle.GetModel()} @ {vehicle.GetPosition()}");
        }
        Console.WriteLine("Vehicle iterator end");

        var tdPool = tds.AsPool();

        foreach (var td in tdPool)
        {
            Console.WriteLine($"TD: {td.GetID()}, prev mdl: {td.GetPreviewModel()}");
        }
    }

    public void OnTick(Microseconds elapsed, TimePoint now)
    {
    }
    
    public bool OnConsoleText(string command, string parameters, ref ConsoleCommandSenderData sender)
    {
        if (command == "banana")
        {
            Console.WriteLine($"BANANA!!! {parameters}");
            return true;
        }
        
        Console.WriteLine($"cmd: {command}; params: {parameters}");

        
        return false;
    }

    public void OnRconLoginAttempt(IPlayer player, string password, bool success)
    {
        Console.WriteLine($"login attempt by player {player.GetName()}({player.Handle:X}) w/pw {password}; {success}");
    }

    public void OnConsoleCommandListRequest(FlatHashSetStringView commands)
    {
        commands.Emplace("banana");
    }

    public void OnIncomingConnection(IPlayer player, string ipAddress, ushort port)
    {
    }

    public void OnPlayerConnect(IPlayer player)
    {
        Console.WriteLine($"Player connected: {player.GetNetworkData().Value.networkID.address.ToAddress()}");
    }

    public void OnPlayerDisconnect(IPlayer player, PeerDisconnectReason reason)
    {
    }

    public void OnPlayerClientInit(IPlayer player)
    {
    }

    public void OnPoolEntryCreated(IPlayer entry)
    {
    }

    public void OnPoolEntryDestroyed(IPlayer entry)
    {
    }
}