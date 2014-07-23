using System;
using System.Linq;
using SampSharp.GameMode.World;
using SampSharp.Streamer.Definitions;
using SampSharp.Streamer.Natives;

namespace SampSharp.Streamer.World
{
    public class DynamicRaceCheckpoint : DynamicWorldObject<DynamicRaceCheckpoint>
    {
        public DynamicRaceCheckpoint(int id)
        {
            Id = id;
        }

        public DynamicRaceCheckpoint(int type, Vector position, Vector nextPosition = new Vector(), float size = 3.0f, int worldid = -1,
            int interiorid = -1, Player player = null, float streamdistance = 100.0f)
        {
            Id = StreamerNative.CreateDynamicRaceCP(type, position.X, position.Y, position.Z, nextPosition.X,
                nextPosition.Y, nextPosition.Z, size, worldid, interiorid, player == null ? -1 : player.Id,
                streamdistance);
        }

        public DynamicRaceCheckpoint(int type, Vector position, Vector nextPosition = new Vector(), float size = 3.0f, int[] worlds = null, int[] interiors = null,
            Player[] players = null, float streamdistance = 100.0f)
        {
            Id = StreamerNative.CreateDynamicRaceCPEx(type, position.X, position.Y, position.Z, nextPosition.X,
                nextPosition.Y, nextPosition.Z, size, streamdistance, worlds, interiors,
                players == null ? null : players.Select(p => p.Id).ToArray());
        }

        public bool IsValid
        {
            get { return StreamerNative.IsValidDynamicRaceCP(Id); }
        }

        public override StreamType StreamType
        {
            get { return StreamType.RaceCheckpoint; }
        }

        public float Size
        {
            get { return Streamer.ItemType[StreamType].GetFloat(Id, StreamerDataType.Size); }
            set { Streamer.ItemType[StreamType].SetFloat(Id, StreamerDataType.Size, value); }
        }

        public virtual Vector NextPosition
        {
            get
            {
                float x = Streamer.ItemType[StreamType].GetFloat(Id, StreamerDataType.NextX);
                float y = Streamer.ItemType[StreamType].GetFloat(Id, StreamerDataType.NextY);
                float z = Streamer.ItemType[StreamType].GetFloat(Id, StreamerDataType.NextZ);

                return new Vector(x, y, z);
            }
            set
            {
                Streamer.ItemType[StreamType].SetFloat(Id, StreamerDataType.NextX, value.X);
                Streamer.ItemType[StreamType].SetFloat(Id, StreamerDataType.NextY, value.Y);
                Streamer.ItemType[StreamType].SetFloat(Id, StreamerDataType.NextZ, value.Z);
            }
        }
        public void ToggleForPlayer(Player player, bool toggle)
        {
            CheckDisposure();

            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            StreamerNative.TogglePlayerDynamicRaceCP(player.Id, Id, toggle);
        }

        public bool IsPlayerInCheckpoint(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            return StreamerNative.IsPlayerInDynamicRaceCP(player.Id, Id);
        }

        public static void ToggleAllForPlayer(Player player, bool toggle)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            StreamerNative.TogglePlayerAllDynamicRaceCPs(player.Id, toggle);
        }

        public static DynamicRaceCheckpoint GetPlayerVisibleDynamicCheckpoint(Player player)
        {
            int id = StreamerNative.GetPlayerVisibleDynamicRaceCP(player.Id);

            return id < 0 ? null : FindOrCreate(id);
        }
    }
}
