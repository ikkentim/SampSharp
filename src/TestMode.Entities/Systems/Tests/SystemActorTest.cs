using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.Entities.Systems.Tests
{
    public class SystemActorTest : ISystem
    {
        [Event]
        public void OnGameModeInit(IWorldService worldService)
        {
            worldService.CreateActor(101, new Vector3(0, 0, 20), 0);
        }


        [PlayerCommand]
        public void ActorCommand(Player player, IWorldService worldService)
        {
            worldService.CreateActor(0, player.Position + Vector3.Up, 0);
            player.SendClientMessage("Actor created!");
            player.PlaySound(1083);
        }
    }
}