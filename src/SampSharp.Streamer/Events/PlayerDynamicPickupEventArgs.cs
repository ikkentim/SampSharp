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

using System;
using SampSharp.GameMode.Events;
using SampSharp.Streamer.World;

namespace SampSharp.Streamer.Events
{
    public class PlayerDynamicPickupEventArgs : PlayerEventArgs
    {
        public PlayerDynamicPickupEventArgs(int playerid, int pickupid) : base(playerid)
        {
            PickupId = pickupid;
        }

        public int PickupId { get; private set; }

        public DynamicPickup DynamicPickup
        {
            get
            {
                return PickupId == -1 ? null : DynamicPickup.FindOrCreate(PickupId);
            }
        }
    }
}