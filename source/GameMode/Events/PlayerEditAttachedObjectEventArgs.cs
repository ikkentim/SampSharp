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
    public class PlayerEditAttachedObjectEventArgs : PlayerClickMapEventArgs
    {
        public PlayerEditAttachedObjectEventArgs(int playerid, EditObjectResponse response, int index, int modelid,
            int boneid, Vector position, Rotation rotation, Vector offset) : base(playerid, position)
        {
            EidEditObjectResponse = response;
            Index = index;
            ModelId = modelid;
            BoneId = boneid;
            Rotation = rotation;
            Offset = offset;
        }

        public EditObjectResponse EidEditObjectResponse { get; private set; }

        public int Index { get; private set; }

        public int ModelId { get; private set; }

        public int BoneId { get; private set; }

        public Rotation Rotation { get; private set; }

        public Vector Offset { get; private set; }
    }
}