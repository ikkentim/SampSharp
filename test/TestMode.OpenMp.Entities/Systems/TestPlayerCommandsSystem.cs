using System.Numerics;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;
using TestMode.OpenMp.Entities.Components;

namespace TestMode.OpenMp.Entities.Systems;

public class TestPlayerCommandsSystem : ISystem
{
    [PlayerCommand(Name = "help")]
    public void HelpCommand(Player player, IPlayerCommandService commands, ICommandTextFormatter commandTextFormatter, HelpService help)
    {
        help.Send(player);
    }

    [PlayerCommand(Name = "kill")]
    [Alias("k")]
    public void KillPlayer(Player player)
    {
        player.Health = 0;
        player.SendClientMessage("You have been killed!");
    }

    [PlayerCommand(Name = "spawn")]
    public void SpawnPlayer(Player player, VehicleModelType model, IWorldService worldService)
    {
        player.SendClientMessage($"Spawned a {model}!");
        
        var vehicle = worldService.CreateVehicle(model, player.Position + GtaVector.Up, player.Angle, -1, -1);

        player.PutInVehicle(vehicle);
    }

    [PlayerCommand(Name = "slap")]
    [CommandGroup("admin")]
    [RequiresPermission("admin")]
    public void SlapPlayer(Player player, Player target, int damage = 10)
    {
        target.Health -= damage;
        player.SendClientMessage($"You slapped {target.Name} for {damage} damage!");
        target.SendClientMessage($"{player.Name} slapped you for {damage} damage!");
    }

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

    [CommandGroup("teleport")]
    [PlayerCommand(Name = "player")]
    public void TeleportCommand(Player player, Player target)
    {
        player.Position = target.Position;
        player.SendClientMessage($"Teleported to {target.Name}");
    }

    [CommandGroup("teleport")]
    [PlayerCommand(Name = "player")]
    [Alias("tp")]
    public void TeleportCommand(Player player, float x, float y, float z)
    {
        player.Position = new Vector3(x, y, z);
        player.SendClientMessage($"Teleported to ({x}, {y}, {z})");
    }

    [CommandGroup("teleport")]
    [PlayerCommand(Name = "player")]
    public void TeleportCommand(Player player, Player target, float x, float y, float z)
    {
        target.Position = new Vector3(x, y, z);
        player.SendClientMessage($"Teleported {target.Name} to ({x}, {y}, {z})");
    }
}