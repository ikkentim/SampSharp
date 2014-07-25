// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using SampSharp.GameMode.Pools;
using SampSharp.GameMode.World;
using SampSharp.Streamer.Definitions;

namespace SampSharp.Streamer.World
{
    public abstract class DynamicWorldObject<T> : IdentifiedPool<T>, IIdentifyable, IWorldObject
        where T : DynamicWorldObject<T>
    {
        public abstract StreamType StreamType { get; }

        public virtual int Interior
        {
            get { return GetInteger(StreamerDataType.InteriorId); }
            set { SetInteger(StreamerDataType.InteriorId, value); }
        }

        public virtual int World
        {
            get { return GetInteger(StreamerDataType.WorldId); }
            set { SetInteger(StreamerDataType.WorldId, value); }
        }

        public virtual float StreamDistance
        {
            get { return GetFloat(StreamerDataType.StreamDistance); }
            set { SetFloat(StreamerDataType.StreamDistance, value); }
        }

        public int Id { get; protected set; }

        public virtual Vector Position
        {
            get
            {
                return new Vector(GetFloat(StreamerDataType.X),
                    GetFloat(StreamerDataType.Y),
                    GetFloat(StreamerDataType.Z));
            }
            set
            {
                SetFloat(StreamerDataType.X, value.X);
                SetFloat(StreamerDataType.Y, value.Y);
                SetFloat(StreamerDataType.Z, value.Z);
            }
        }


        protected int GetInteger(StreamerDataType data)
        {
            return Streamer.ItemType[StreamType].GetInteger(Id, data);
        }

        protected float GetFloat(StreamerDataType data)
        {
            return Streamer.ItemType[StreamType].GetFloat(Id, data);
        }

        protected int[] GetArray(StreamerDataType data, int maxlength)
        {
            return Streamer.ItemType[StreamType].GetArray(Id, data, maxlength);
        }

        protected void AppendToArray(StreamerDataType data, int value)
        {
            Streamer.ItemType[StreamType].AppendToArray(Id, data, value);
        }

        protected bool IsInArray(StreamerDataType data, int value)
        {
            return Streamer.ItemType[StreamType].IsInArray(Id, data, value);
        }

        protected void SetInteger(StreamerDataType data, int value)
        {
            Streamer.ItemType[StreamType].SetInteger(Id, data, value);
        }

        protected void SetFloat(StreamerDataType data, float value)
        {
            Streamer.ItemType[StreamType].SetFloat(Id, data, value);
        }

        protected void SetArray(StreamerDataType data, int[] value)
        {
            Streamer.ItemType[StreamType].SetArray(Id, data, value);
        }
    }
}