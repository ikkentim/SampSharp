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
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.VehicleDied" />,
    ///     <see cref="BaseMode.PlayerExitVehicle" />, <see cref="BaseMode.VehicleStreamIn" /> or
    ///     <see cref="BaseMode.VehicleStreamOut" /> event.
    /// </summary>
    public class PlayerVehicleEventArgs : PlayerEventArgs
    {
        public PlayerVehicleEventArgs(int playerid, int vehicleid) : base(playerid)
        {
            VehicleId = vehicleid;
        }

        public int VehicleId { get; private set; }

        public Vehicle Vehicle
        {
            get { return VehicleId == Vehicle.InvalidId ? null : Vehicle.Find(VehicleId); }
        }
    }
}