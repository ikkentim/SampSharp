using System.Numerics;
using Microsoft.Extensions.Logging;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.OpenMp.Core.Api;

namespace TestMode.OpenMp.Entities.Systems;

public class TestActorSystem : ISystem
{
    [Event]
    public void OnGameModeInit(IWorldService world, IEntityManager entityManager)
    {
        var actor = world.CreateActor(1, new Vector3(15, 0, 5), 0);

        var spawn = ((IActor)actor).GetSpawnData();
    }

}