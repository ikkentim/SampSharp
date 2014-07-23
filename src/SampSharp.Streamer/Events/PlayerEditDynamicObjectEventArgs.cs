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
    public class PlayerEditDynamicObjectEventArgs : PlayerClickMapEventArgs
    {
        public PlayerEditDynamicObjectEventArgs(int playerid, int objectid, EditObjectResponse response, Vector position,
            Vector rotation)
            : base(playerid, position)
        {
            ObjectId = objectid;
            EditObjectResponse = response;
            Rotation = rotation;
        }

        public int ObjectId { get; private set; }

        public DynamicObject DynamicObject
        {
            get
            {
                return ObjectId == GlobalObject.InvalidId ? null : DynamicObject.FindOrCreate(ObjectId);
            }
        }

        public EditObjectResponse EditObjectResponse { get; private set; }

        public Vector Rotation { get; private set; }
    }
}