// SampSharp
// Copyright (C) 2015 Tim Potze
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

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.VehicleMod" /> event.
    /// </summary>
    public class VehicleModEventArgs : PlayerVehicleEventArgs
    {
        public VehicleModEventArgs(int playerid, int vehicleid, int componentid)
            : base(playerid, vehicleid)
        {
            ComponentId = componentid;
        }

        public int ComponentId { get; set; }

        /// <summary>
        ///     Gets or sets whether to desync the mod (or an invalid mod) from propagating and / or crashing players.
        /// </summary>
        public bool PreventPropagation { get; set; }
    }
}