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

using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;
using SampSharp.Streamer.World;

namespace SampSharp.Streamer.Events
{
    public class PlayerShootDynamicObjectEventArgs : PlayerClickMapEventArgs
    {
        public PlayerShootDynamicObjectEventArgs(int playerid, int objectid, Weapon weapon, Vector position)
            : base(playerid, position)
        {
            ObjectId = objectid;
            Weapon = weapon;
        }

        public int ObjectId { get; private set; }

        public DynamicObject DynamicObject
        {
            get
            {
                return ObjectId == GlobalObject.InvalidId ? null : DynamicObject.FindOrCreate(ObjectId);
            }
        }


        public Weapon Weapon { get; private set; }
    }
}