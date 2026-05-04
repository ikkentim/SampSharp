using System.Numerics;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;
using SampSharp.Entities.SAMP.Commands.Attributes;
using SampSharp.Entities.SAMP.Commands.Help;

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
    /// Player command: /spawn - spawns the player at a location
    /// </summary>
    [PlayerCommand(Name = "spawn")]
    public void SpawnPlayer(Player player, float x = 0, float y = 0, float z = 5)
    {
        player.Position = new Vector3(x, y, z);
        player.SendClientMessage($"Spawned at ({x}, {y}, {z})");
    }

    /// <summary>
    /// Player command: /slap [player] - damages a player (requires admin permission)
    /// </summary>
    [PlayerCommand(Name = "slap")]
    [CommandGroup("admin")]
    [RequiresPermission("admin", "moderator")]
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
        System.Console.WriteLine($"Active players: {players.Count()}");

        foreach (var player in players.Where(p => p.IsComponentAlive))
        {
            System.Console.WriteLine($"  [{player.Entity}] {player.Name} (Health: {player.Health:F0}, Armor: {player.Armour:F0})");
        }
    }

    /// <summary>
    /// Console command: server_info - displays server information
    /// </summary>
    [ConsoleCommand(Name = "server_info")]
    public void ConsoleServerInfo()
    {
        var playerCount = _entityManager.GetComponents<Player>().Count(p => p.IsComponentAlive);
        System.Console.WriteLine("=== Server Info ===");
        System.Console.WriteLine($"Active Players: {playerCount}");
        System.Console.WriteLine($"Current Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");
        System.Console.WriteLine("===================");
    }

    /// <summary>
    /// Player command: /help - shows available player commands (uses command enumeration API)
    /// </summary>
    [PlayerCommand(Name = "help")]
    public void HelpCommand(Player player, IPlayerCommandService commands)
    {
        player.SendClientMessage("--- Available Commands ---");

        var playerCommands = commands.GetCommands().GetAllCommands()
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
        System.Console.WriteLine($"Server tick count: {server.TickCount}ms");
        System.Console.WriteLine($"Server tick rate: {server.TickRate}");
        System.Console.WriteLine($"Max players: {server.MaxPlayers}");
        System.Console.WriteLine($"Player pool size: {server.PlayerPoolSize}");
    }

    /// <summary>
    /// Player command: /ping - shows player ping (demonstrates DI parameter - IEntityManager injected)
    /// </summary>
    [PlayerCommand(Name = "ping")]
    public void PingCommand(Player player, IEntityManager entityManager)
    {
        player.SendClientMessage($"Your ping: {player.Ping}ms");
    }

    [ConsoleCommand(Name = "add_numbers")]
    [Alias("add")]
    public void AddCommand(int a, int b)
    {
        Console.WriteLine($"{a} + {b} = {a + b}");
    }
}
