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

using GameMode.Definitions;
using GameMode.World;

namespace GameMode.Events
{
    public class PlayerEditObjectEventArgs : PlayerClickMapEventArgs
    {
        public PlayerEditObjectEventArgs(int playerid, bool playerobject, int objectid, EditObjectResponse response,
            Vector position, Vector rotation) : base(playerid, position)
        {
            PlayerObject = playerobject;
            ObjectId = objectid;
            EditObjectResponse = response;
            Rotation = rotation;
        }

        public bool PlayerObject { get; private set; }

        public int ObjectId { get; private set; }

        public EditObjectResponse EditObjectResponse { get; private set; }

        public Vector Rotation { get; private set; }
    }
}