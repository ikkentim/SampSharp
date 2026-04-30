using System.Numerics;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace TestMode.OpenMp.Entities;

public class BasicsSystem : ISystem
{
    [Event]
    public void OnGameModeInit(IServerService svr)
    {
        svr.AddPlayerClass(1, new Vector3(0, 0, 10), 0);
    }

    // [Event]
    // public bool OnPlayerRequestClass(Player player, int classId)
    // {
    //     return true;
    // }
    //
    // [Event]
    // public bool OnPlayerRequestSpawn(Player player)
    // {
    //     return true;
    // }
}