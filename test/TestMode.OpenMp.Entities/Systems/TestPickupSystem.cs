using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.OpenMp.Entities.Systems;

[CommandGroup("pickup")]
public class TestPickupSystem : ISystem
{
    [PlayerCommand("help")]
    public void Help(Player player, HelpService help)
    {
        help.Send(player, new CommandGroup("pickup"));
    }


    [PlayerCommand("create")]
    public void PickupCmd(Player player, IWorldService worldService)
    {
        worldService.CreatePlayerPickup(player, 19522, PickupType.ScriptedActionsOnlyEveryFewSeconds, player.Position, parent: player);
    }

}