using System.Numerics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TestMode.OpenMp.Entities.Components;

namespace TestMode.OpenMp.Entities.Systems;

public class BasicsSystem : ISystem
{
    [Event]
    public void OnGameModeInit(IServerService svr, IConfiguration configuration, IOptions<TestSampSharpOptions> options)
    {
        var cls1 = new PlayerSpawnData(0, 3, new Vector3(0, 0, 10), 0,
            [new PlayerWeaponSlot(Weapon.Colt45, 14)]); // Andre

        var cls2 = new PlayerSpawnData(0, 6, new Vector3(0, 0, 10), 0,
            [new PlayerWeaponSlot(Weapon.Deagle, 14)]); // Emmet

        svr.AddPlayerClass(cls1).AddComponent(new ClassNameComponent("Andre"));
        svr.AddPlayerClass(cls2).AddComponent(new ClassNameComponent("Emmet"));

        var art = configuration["artwork:enable"];
        Console.WriteLine($"artwork enabled: {art}");

        Console.WriteLine($"Directory: {options.Value.Assembly}");
    }

    [Event]
    public void OnPlayerRequestClass(Player player, Class klass)
    {
        var className = klass.GetComponent<ClassNameComponent>();

        if (className is not null)
        {
            player.SendClientMessage($"Class: {className.Name}");
        }
    }
}
