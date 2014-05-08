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

namespace SampSharp.GameMode.Events
{
    /// <summary>
    /// Provides data for the <see cref="BaseMode.VehicleResprayed" /> event.
    /// </summary>
    public class VehicleResprayedEventArgs : PlayerVehicleEventArgs
    {
        public VehicleResprayedEventArgs(int playerid, int vehicleid, int color1, int color2)
            : base(playerid, vehicleid)
        {
            Color1 = color1;
            Color2 = color2;
        }

        public int Color1 { get; private set; }

        public int Color2 { get; private set; }
    }
}