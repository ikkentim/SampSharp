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

using SampSharp.GameMode.Events;
using SampSharp.Streamer.World;

namespace SampSharp.Streamer.Events
{
    public class PlayerDynamicCheckpointEventArgs : PlayerEventArgs
    {
        public PlayerDynamicCheckpointEventArgs(int playerid, int checkpointid)
            : base(playerid)
        {
            CheckpointId = checkpointid;
        }

        public int CheckpointId { get; private set; }

        public DynamicCheckpoint DynamicCheckpoint
        {
            get { return CheckpointId == -1 ? null : DynamicCheckpoint.FindOrCreate(CheckpointId); }
        }
    }
}