using System;
using System.Linq;
using SampSharp.GameMode.World;
using SampSharp.Streamer.Definitions;
using SampSharp.Streamer.Natives;

namespace SampSharp.Streamer.World
{
    public class DynamicCheckpoint : DynamicWorldObject<DynamicCheckpoint>
    {
        public DynamicCheckpoint(int id)
        {
            Id = id;
        }

        public DynamicCheckpoint(Vector position, float size = 1.0f, int worldid = -1, int interiorid = -1,
            Player player = null, float streamdistance = 100.0f)
        {
            Id = StreamerNative.CreateDynamicCP(position.X, position.Y, position.Z, size, worldid, interiorid, player == null ? -1 : player.Id, streamdistance);
        }

        public DynamicCheckpoint(Vector position, float size = 1.0f, int[] worlds = null, int[] interiors = null,
            Player[] players = null, float streamdistance = 100.0f)
        {
            Id =StreamerNative.CreateDynamicCPEx(position.X, position.Y, position.Z, size, streamdistance, worlds, interiors,
                players == null ? null : players.Select(p => p.Id).ToArray());
        }

        public bool IsValid
        {
            get { return StreamerNative.IsValidDynamicCP(Id); }
        }

        public override StreamType StreamType
        {
            get { return StreamType.Checkpoint; }
        }

        public void ToggleForPlayer(Player player, bool toggle)
        {
            CheckDisposure();

            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            StreamerNative.TogglePlayerDynamicCP(player.Id, Id, toggle);
        }

        public bool IsPlayerInCheckpoint(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            return StreamerNative.IsPlayerInDynamicCP(player.Id, Id);
        }

        public static void ToggleAllForPlayer(Player player, bool toggle)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            StreamerNative.TogglePlayerAllDynamicCPs(player.Id, toggle);
        }

        public static DynamicCheckpoint GetPlayerVisibleDynamicCheckpoint(Player player)
        {
            int id = StreamerNative.GetPlayerVisibleDynamicCP(player.Id);

            return id < 0 ? null : FindOrCreate(id);
        }
    }
}
