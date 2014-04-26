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

using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    public class UnoccupiedVehicleEventArgs : PlayerVehicleEventArgs
    {
        public UnoccupiedVehicleEventArgs(int playerid, int vehicleid, int passengerSeat, Vector newPosition) : base(playerid, vehicleid)
        {
            PassengerSeat = passengerSeat;
            NewPosition = newPosition;
        }

        public int PassengerSeat { get; private set; }

        public Vector NewPosition { get; private set; }
    }
}