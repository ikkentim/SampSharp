using System.Collections.Generic;
using System.Linq;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    class CheckpointController : IEventListener
    {
        private int _tick;

        public CheckpointController()
        {
            TickRate = 40;
        }

        public int TickRate { get; set; }

        public void RegisterEvents(BaseMode gameMode)
        {
            gameMode.Tick += (sender, args) =>
            {
                if (++_tick < TickRate) return;

                _tick = 0;
                Update();
            };

            gameMode.PlayerEnterCheckpoint += (sender, args) =>
            {
                var player = args.Player;

                var checkpoint = Checkpoint.All.FirstOrDefault(cp => cp.IsActive(player));

                if (checkpoint != null)
                    checkpoint.OnEnter(args);
            };

            gameMode.PlayerLeaveCheckpoint += (sender, args) =>
            {
                var player = args.Player;

                var checkpoint = Checkpoint.All.FirstOrDefault(cp => cp.IsActive(player));

                if (checkpoint != null)
                    checkpoint.OnLeave(args);
            };

        }

        public static void Update()
        {
            foreach (Player player in Player.All)
            {
                Vector position = player.Position;

                Checkpoint active = null;
                Checkpoint forced = null;
                Dictionary<Checkpoint, float> checkpoints = new Dictionary<Checkpoint, float>();

                foreach (Checkpoint checkpoint in Checkpoint.All)
                {
                    if (checkpoint.IsVisible(player))
                        checkpoints.Add(checkpoint, checkpoint.Position.DistanceTo(position));

                    if (checkpoint.IsForced(player))
                        forced = checkpoint;

                    if (checkpoint.IsActive(player))
                        active = checkpoint;
                }

                if (forced != null)
                {
                    if (active == forced) break;

                    if (active != null)
                        active.Deactivate(player);

                    forced.Activate(player);

                    break;
                }

                var nearest = checkpoints.OrderBy(p => p.Value).FirstOrDefault().Key;

                if (active == nearest) break;

                if (active != null)
                    active.Deactivate(player);

                if (nearest != null)
                    nearest.Activate(player);
            }
        }
    }
}
