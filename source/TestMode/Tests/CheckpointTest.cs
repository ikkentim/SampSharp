using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    class CheckpointTest : ITest
    {
        public void Start(GameMode gameMode)
        {
        
        }

        [Command("checkpoint")]
        public static bool CheckpointCommand(Player player)
        {
            var cp = new Checkpoint(player.Position, 2.5f);
            cp.Show(player);

            cp.Enter += (sender, args) => args.Player.SendClientMessage(Color.BlueViolet, "Entered checkpoint");
            cp.Leave += (sender, args) => args.Player.SendClientMessage(Color.BlueViolet, "Left checkpoint");

            player.SendClientMessage(Color.Tan, "Checkpoint created at " + player.Position);

            return true;
        }

        [Command("mycheckpoint")]
        public static bool MyCheckpointCommand(Player player)
        {
            var cp = new Checkpoint(player.Position, 2.5f);
            cp.Show(player);

            cp.Enter += (sender, args) => args.Player.SendClientMessage(Color.BlueViolet, "Entered 2 checkpoint");
            cp.Leave += (sender, args) => args.Player.SendClientMessage(Color.BlueViolet, "Left 2 checkpoint");

            player.SendClientMessage(Color.Tan, "Checkpoint created for you at " + player.Position);

            return true;
        }

        [Command("forcedcheckpoint")]
        public static bool ForcedCheckpointCommand(Player player)
        {
            var cp = new Checkpoint(player.Position, 2.5f);
            cp.Force(player);

            cp.Enter += (sender, args) => args.Player.SendClientMessage(Color.BlueViolet, "Entered 3 checkpoint");
            cp.Leave += (sender, args) => args.Player.SendClientMessage(Color.BlueViolet, "Left 3 checkpoint");

            player.SendClientMessage(Color.Tan, "Forced checkpoint created at " + player.Position);

            return true;
        }

    }
}
