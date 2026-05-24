using System.Numerics;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TestMode.OpenMp.Entities.Components;

namespace TestMode.OpenMp.Entities.Systems;

public class TestClassSystem : ISystem
{
    [Event]
    public void OnGameModeInit(IServerService server)
    {
        var cls1 = new PlayerSpawnData(0, 3, new Vector3(0, 0, 10), 0,
            [new PlayerWeaponSlot(Weapon.Colt45, 14)]); // Andre

        var cls2 = new PlayerSpawnData(0, 6, new Vector3(0, 0, 10), 0,
            [new PlayerWeaponSlot(Weapon.Deagle, 14)]); // Emmet

        server.AddPlayerClass(cls1).AddComponent(new ClassNameComponent("Andre"));
        server.AddPlayerClass(cls2).AddComponent(new ClassNameComponent("Emmet"));
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
