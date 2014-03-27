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
    public class PlayerSelectObjectEventArgs : PlayerClickMapEventArgs
    {
        public PlayerSelectObjectEventArgs(int playerid, ObjectType type, int objectid, int modelid, Vector position)
            : base(playerid, position)
        {
            ObjectType = type;
            ObjectId = objectid;
            ModelId = modelid;
        }

        public ObjectType ObjectType { get; private set; }

        public int ObjectId { get; private set; }

        public int ModelId { get; private set; }
    }
}