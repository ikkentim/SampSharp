using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.OpenMp.Entities.Systems;

public class TestConsoleCommandsSystem(IEntityManager entityManager) : ISystem
{
    [ConsoleCommand(Name = "list_players")]
    public void ConsoleListPlayers()
    {
        var players = entityManager.GetComponents<Player>();
        Console.WriteLine($"Active players: {players.Count()}");

        foreach (var player in players.Where(p => p.IsComponentAlive))
        {
            Console.WriteLine($"  [{player.Entity}] {player.Name} (Health: {player.Health:F0}, Armor: {player.Armour:F0})");
        }
    }

    [ConsoleCommand(Name = "server_info")]
    public void ConsoleServerInfo()
    {
        var playerCount = entityManager.GetComponents<Player>().Length;
        Console.WriteLine("=== Server Info ===");
        Console.WriteLine($"Active Players: {playerCount}");
        Console.WriteLine($"Current Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine("===================");
    }

    [ConsoleCommand(Name = "time")]
    public void ConsoleTime(IServerService server)
    {
        Console.WriteLine($"Server tick count: {server.TickCount}ms");
        Console.WriteLine($"Server tick rate: {server.TickRate}");
        Console.WriteLine($"Max players: {server.MaxPlayers}");
        Console.WriteLine($"Player pool size: {server.PlayerPoolSize}");
    }

    [ConsoleCommand(Name = "add_numbers")]
    [Alias("add")]
    public void AddCommand(int a, int b)
    {
        Console.WriteLine($"{a} + {b} = {a + b}");
    }

    [ConsoleCommand(Name = "double_number")]
    public void DoubleValueCommand([CommandParameter("value", typeof(DoubleIntParser))] int v)
    {
        Console.WriteLine(v);
    }

    private class DoubleIntParser : ICommandParameterParser
    {
        private readonly IntParser _inner = new();

        public bool TryParse(IServiceProvider services, ref StringSpan inputText, out object? result)
        {
            if (_inner.TryParse(services, ref inputText, out result) && result is int num)
            {
                result = num * 2;
                return true;
            }

            return false;
        }
    }
}
