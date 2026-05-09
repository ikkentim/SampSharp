using System.Numerics;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.OpenMp.Entities;

/// <summary>
/// Demonstrates the new Commands system with simple player and console commands.
/// </summary>
public class SampleCommandsSystem : ISystem
{
    private readonly IEntityManager _entityManager;

    public SampleCommandsSystem(IEntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    /// <summary>
    /// Player command: /kill or /k - kills the player
    /// </summary>
    [PlayerCommand(Name = "kill")]
    [Alias("k")]
    public void KillPlayer(Player player)
    {
        player.Health = 0;
        player.SendClientMessage("You have been killed!");
    }

    /// <summary>
    /// Player command: /spawn - spawns a vehicle at the player
    /// </summary>
    [PlayerCommand(Name = "spawn")]
    public void SpawnPlayer(Player player, VehicleModelType model, IWorldService worldService)
    {
        player.SendClientMessage($"Spawned a {model}!");
        
        var vehicle = worldService.CreateVehicle(model, player.Position + GtaVector.Up, player.Angle, -1, -1);

        player.PutInVehicle(vehicle);
    }

    /// <summary>
    /// Player command: /slap [player] - damages a player (requires admin permission)
    /// </summary>
    [PlayerCommand(Name = "slap")]
    [CommandGroup("admin")]
    [RequiresPermission("admin")]
    public void SlapPlayer(Player player, Player target, int damage = 10)
    {
        target.Health -= damage;
        player.SendClientMessage($"You slapped {target.Name} for {damage} damage!");
        target.SendClientMessage($"{player.Name} slapped you for {damage} damage!");
    }

    /// <summary>
    /// Player command: /money - displays or sets player money
    /// </summary>
    [PlayerCommand(Name = "money")]
    [Alias("$", "cash")]
    public void MoneyCommand(Player player, int? amount = null)
    {
        if (amount.HasValue)
        {
            player.Money = amount.Value;
            player.SendClientMessage($"Money set to ${amount.Value}");
        }
        else
        {
            player.SendClientMessage($"Current money: ${player.Money}");
        }
    }

    /// <summary>
    /// Player command: /teleport [x] [y] [z] - teleports to location
    /// </summary>
    [PlayerCommand(Name = "teleport")]
    [Alias("tp", "goto")]
    public void TeleportCommand(Player player, float x, float y, float z)
    {
        player.Position = new Vector3(x, y, z);
        player.SendClientMessage($"Teleported to ({x}, {y}, {z})");
    }

    /// <summary>
    /// Console command: list_players - lists all active players
    /// </summary>
    [ConsoleCommand(Name = "list_players")]
    public void ConsoleListPlayers()
    {
        var players = _entityManager.GetComponents<Player>();
        Console.WriteLine($"Active players: {players.Count()}");

        foreach (var player in players.Where(p => p.IsComponentAlive))
        {
            Console.WriteLine($"  [{player.Entity}] {player.Name} (Health: {player.Health:F0}, Armor: {player.Armour:F0})");
        }
    }

    /// <summary>
    /// Console command: server_info - displays server information
    /// </summary>
    [ConsoleCommand(Name = "server_info")]
    public void ConsoleServerInfo()
    {
        var playerCount = _entityManager.GetComponents<Player>().Count(p => p.IsComponentAlive);
        Console.WriteLine("=== Server Info ===");
        Console.WriteLine($"Active Players: {playerCount}");
        Console.WriteLine($"Current Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine("===================");
    }

    /// <summary>
    /// Player command: /help - shows available player commands (uses command enumeration API)
    /// </summary>
    [PlayerCommand(Name = "help")]
    public void HelpCommand(Player player, IPlayerCommandService commands)
    {
        player.SendClientMessage("--- Available Commands ---");

        var playerCommands = commands.Registry.GetAll()
            .OrderBy(c => c.Name)
            .ToList();

        if (playerCommands.Count == 0)
        {
            player.SendClientMessage("No commands available.");
            return;
        }

        foreach (var cmd in playerCommands)
        {
            var aliases = cmd.Aliases.Count > 0 ? $" ({string.Join(", ", cmd.Aliases.Select(a => $"/{a.Name}"))})" : "";
            player.SendClientMessage($"/{cmd.Name}{aliases}");
        }
    }

    /// <summary>
    /// Console command: time - displays current server time (demonstrates DI - IServerService injected)
    /// </summary>
    [ConsoleCommand(Name = "time")]
    public void ConsoleTime(IServerService server)
    {
        Console.WriteLine($"Server tick count: {server.TickCount}ms");
        Console.WriteLine($"Server tick rate: {server.TickRate}");
        Console.WriteLine($"Max players: {server.MaxPlayers}");
        Console.WriteLine($"Player pool size: {server.PlayerPoolSize}");
    }

    /// <summary>
    /// Player command: /ping - shows player ping (demonstrates DI parameter - IEntityManager injected)
    /// </summary>
    [PlayerCommand(Name = "ping")]
    public void PingCommand(Player player)
    {
        player.SendClientMessage($"Your ping: {player.Ping}ms");
    }

    [PlayerCommand("announce")]
    public void AnnounceCommand(Player player, IWorldService server)
    {
        server.SendClientMessage("Hello everyone!");
    }

    [ConsoleCommand(Name = "add_numbers")]
    [Alias("add")]
    public void AddCommand(int a, int b)
    {
        Console.WriteLine($"{a} + {b} = {a + b}");
    }

    [PlayerCommand(Name = "add_numbers")]
    [Alias("add")]
    public void AddCommand(Player player, int a, int b)
    {
        player.SendClientMessage($"{a} + {b} = {a + b}");
    }

    [CommandGroup("test")]
    [PlayerCommand("overloads")]
    public void OverloadsCommand(Player player, int a)
    {
        player.SendClientMessage($"Overload a:{a}");
    }

    [CommandGroup("test")]
    [PlayerCommand("overloads")]
    public void OverloadsCommand(Player player, int a, int b)
    {
        player.SendClientMessage($"Overload a:{a} b:{b}");
    }

    [CommandGroup("test")]
    [PlayerCommand("overloads")]
    [Alias("abc")]
    public void OverloadsCommand(Player player, int a, int b, string c)
    {
        player.SendClientMessage($"Overload a:{a} b:{b} c:{c}");
    }

    [CommandGroup("test")]
    [PlayerCommand("error")]
    public void ErrorCommand(Player player)
    {
        player.SendClientMessage("an error will be thrown");
        throw new InvalidOperationException("test error");
    }

    [CommandGroup("test")]
    [PlayerCommand("asyncerror")]
    public async Task AsyncErrorCommand(Player player)
    {
        player.SendClientMessage("an error will be thrown in a bit");
        await Task.Delay(10);
        throw new InvalidOperationException("test error");
    }

    [CommandGroup("test")]
    [PlayerCommand("help")]
    public void HelpTestCommand(Player player, IPlayerCommandService commandService, ICommandTextFormatter commandFormatter, string? filter = null)
    {
        var help = new DefaultCommandHelpProvider(commandService.Registry);
        var cmds = help.GetCommandsInGroup(new CommandGroup("test"));

        foreach (var cmd in cmds)
        {
            var commandFormatted = commandFormatter.FormatCommandUsage(cmd.Name, cmd.Group?.FullName, cmd.ParsedParameters);

            if (filter is null || commandFormatted.Contains(filter))
            {
                player.SendClientMessage(commandFormatted);
            }
        }
    }

    [CommandGroup("test")]
    [PlayerCommand("admin")]
    public void AdminTestCommand(AdminComponent admin)
    {
        admin.GetComponent<Player>()!.SendClientMessage("Yup, you're an admin");
    }
}
