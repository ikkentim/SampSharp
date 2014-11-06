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
using SampSharp.GameMode.World;

namespace RiverShell.World
{
    public class Vehicle : GtaVehicle
    {
        public Vehicle(int id) : base(id)
        {
        }

        public override void OnStreamIn(PlayerVehicleEventArgs e)
        {
            var player = e.Player as Player;

            if (this == GameMode.BlueTeam.TargetVehicle)
                e.Vehicle.SetParamsForPlayer(player, true, player.Team == GameMode.GreenTeam);
            else if (this == GameMode.GreenTeam.TargetVehicle)
                e.Vehicle.SetParamsForPlayer(player, true, player.Team == GameMode.BlueTeam);

            base.OnStreamIn(e);
        }
    }
}