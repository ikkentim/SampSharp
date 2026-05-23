using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.OpenMp.Entities.Systems;

[CommandGroup("dialog")]
public class TestDialogSystem : ISystem
{
    [PlayerCommand("help")]
    public void Help(Player player, HelpService help)
    {
        help.Send(player, new CommandGroup("dialog"));
    }


    [PlayerCommand("input")]
    public void DialogInputCommand(Player player, IDialogService dialogService)
    {
        var diag = new InputDialog("Input", "Enter your name", "OK", "Cancel");

        dialogService.Show(player, diag, r => player.SendClientMessage($"response: {r.Response}, {r.InputText ?? "<<NULL>>"}"));
    }

    [PlayerCommand("message")]
    public void DialogMessageCommand(Player player, IDialogService dialogService)
    {
        var diag = new MessageDialog("Message", "This is a message dialog", "OK");

        dialogService.Show(player, diag, r => player.SendClientMessage($"response: {r.Response}"));
    }

    [PlayerCommand("list")]
    public void DialogListCommand(Player player, IDialogService dialogService)
    {
        var diag = new ListDialog("List", "OK")
        {
            "A", "B", "C"
        };

        dialogService.Show(player, diag, r => player.SendClientMessage($"response: {r.Response} {r.ItemIndex} {r.Item?.Text ?? "<<NULL>>"}"));
    }

    [Event]
    public void OnVehicleSpawn(Vehicle vehicle)
    {
        Console.WriteLine($"Vehicle {vehicle.Id} spawned!");
    }

    [Event]
    public void OnVehicleStreamIn(Vehicle vehicle, Player player)
    {
        Console.WriteLine($"Vehicle {vehicle.Id} streams in for player {player}");
    }

    [Event]
    public void OnVehicleStreamOut(Vehicle vehicle, Player player)
    {
        Console.WriteLine($"Vehicle {vehicle.Id} streams out for player {player}");
    }
}