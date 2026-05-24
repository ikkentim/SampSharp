using System.Numerics;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.OpenMp.Entities.Systems;

[CommandGroup("gangzone")]
public class TestGangZoneSystem : ISystem
{
    [PlayerCommand("help")]
    public void Help(Player player, HelpService help)
    {
        help.Send(player, new CommandGroup("gangzone"));
    }

    [PlayerCommand("create")]
    public void GangZoneCmd(Player player, IWorldService worldService)
    {
        var gz = worldService.CreatePlayerGangZone(player, new Vector2(-50, -50), new Vector2(50, 50), player);
        gz.Color = Color.Blue;
        gz.Show();
    }

}