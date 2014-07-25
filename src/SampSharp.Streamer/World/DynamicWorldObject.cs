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

        public int Id { get; protected set; }

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
    }
}