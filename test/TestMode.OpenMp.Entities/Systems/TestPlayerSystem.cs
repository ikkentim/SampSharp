using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;
using TestMode.OpenMp.Entities.Components;

namespace TestMode.OpenMp.Entities.Systems;

[CommandGroup("player")]
public class TestPlayerSystem : ISystem
{
    [PlayerCommand("help")]
    public void Help(Player player, HelpService help)
    {
        help.Send(player, new CommandGroup("player"));
    }

    [PlayerCommand]
    public void NetCommand(Player player)
    {
        var n = player.GetNetworkStats();
        player.SendClientMessage(n.MessagesSent.ToString());
    }

    [PlayerCommand]
    [Alias("ak")]
    public void AkCommand(Player player)
    {
        player.GiveWeapon(Weapon.AK47, 200);
    }

    [PlayerCommand]
    public void RefTestCommand(Player player)
    {
        var weaponState = player.WeaponState;
        var anim = player.AnimationIndex;
        var cfv = player.CameraFrontVector;
        var cm = player.CameraMode;
        player.GetKeys(out var keys, out var ud, out var lr);
        player.PlaySound(5408); // 5408 - "No more bets please!"
        player.GetAnimationName(out var lib, out var name);

        player.SendClientMessage($"Weapon state: {weaponState}, anim: {anim}, cfv: {cfv}, cm: {cm}, keys: {keys}, ud: {ud}, lr: {lr}, lib: {lib}, name: {name}");
    }

    [Event]
    public void OnRconLoginAttempt(Player player, string password, bool success)
    {
        if (success)
        {
            player.AddComponent<AdminComponent>();
        }
    }
}