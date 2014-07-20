using System;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;
using SampSharp.Streamer.Definitions;
using SampSharp.Streamer.Natives;

namespace SampSharp.Streamer.World
{
    public class DynamicMapIcon : IdentifiedPool<DynamicMapIcon>, IIdentifyable, IWorldObject
    {
        public DynamicMapIcon(int id)
        {
            Id = id;
        }

        public DynamicMapIcon(Vector position, int type, MapIconType mapIconType = MapIconType.Local, int worldid = -1, int interiorid = -1,
            Player player = null, float streamDistance = 100.0f)
        {
            Id = StreamerNative.CreateDynamicMapIcon(position.X, position.Y, position.Z, type, 0, worldid, interiorid,
                player == null ? -1 : player.Id, streamDistance, mapIconType);
        }

        public DynamicMapIcon(Vector position, Color color, MapIconType mapIconType = MapIconType.Local, int worldid = -1, int interiorid = -1,
            Player player = null, float streamDistance = 100.0f)
        {
            Id = StreamerNative.CreateDynamicMapIcon(position.X, position.Y, position.Z, 0, color, worldid, interiorid,
                player == null ? -1 : player.Id, streamDistance, mapIconType);
        }

        public int Id { get; private set; }

        public bool IsValid
        {
            get { return StreamerNative.IsValidDynamicMapIcon(Id); }
        }

        public Vector Position
        {
            get
            {
                float x = Streamer.ItemType[StreamType.MapIcon].GetFloat(Id, StreamerDataType.X);
                float y = Streamer.ItemType[StreamType.MapIcon].GetFloat(Id, StreamerDataType.Y);
                float z = Streamer.ItemType[StreamType.MapIcon].GetFloat(Id, StreamerDataType.Z);

                return new Vector(x, y, z);
            }
            set
            {
                Streamer.ItemType[StreamType.MapIcon].SetFloat(Id, StreamerDataType.X, value.X);
                Streamer.ItemType[StreamType.MapIcon].SetFloat(Id, StreamerDataType.Y, value.Y);
                Streamer.ItemType[StreamType.MapIcon].SetFloat(Id, StreamerDataType.Z, value.Z);
            }
        }

        protected override void Dispose(bool disposing)
        {
            StreamerNative.DestroyDynamicMapIcon(Id);

            base.Dispose(disposing);
        }
    }
}
