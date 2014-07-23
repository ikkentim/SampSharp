using SampSharp.GameMode.Pools;
using SampSharp.GameMode.World;
using SampSharp.Streamer.Definitions;

namespace SampSharp.Streamer.World
{
    public abstract class DynamicWorldObject<T> : IdentifiedPool<T>, IIdentifyable, IWorldObject where T : DynamicWorldObject<T>
    {
        public int Id { get; protected set; }

        public abstract StreamType StreamType { get; }

        public virtual Vector Position
        {
            get
            {
                return new Vector(Streamer.ItemType[StreamType].GetFloat(Id, StreamerDataType.X),
                    Streamer.ItemType[StreamType].GetFloat(Id, StreamerDataType.Y),
                    Streamer.ItemType[StreamType].GetFloat(Id, StreamerDataType.Z));
            }
            set
            {
                Streamer.ItemType[StreamType].SetFloat(Id, StreamerDataType.X, value.X);
                Streamer.ItemType[StreamType].SetFloat(Id, StreamerDataType.Y, value.Y);
                Streamer.ItemType[StreamType].SetFloat(Id, StreamerDataType.Z, value.Z);
            }
        }

        public virtual int Interior
        {
            get { return Streamer.ItemType[StreamType].GetInteger(Id, StreamerDataType.InteriorId); }
            set { Streamer.ItemType[StreamType].SetInteger(Id, StreamerDataType.InteriorId, value); }
        }

        public virtual int World
        {
            get { return Streamer.ItemType[StreamType].GetInteger(Id, StreamerDataType.WorldId); }
            set { Streamer.ItemType[StreamType].SetInteger(Id, StreamerDataType.WorldId, value); }
        }

        public virtual float StreamDistance
        {
            get { return Streamer.ItemType[StreamType].GetFloat(Id, StreamerDataType.StreamDistance); }
            set { Streamer.ItemType[StreamType].SetFloat(Id, StreamerDataType.StreamDistance, value); }
        }
    }
}
