// SampSharp
// Copyright (C) 04 Tim Potze
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
    public class VehiclePaintjobEventArgs : PlayerVehicleEventArgs
    {
        public VehiclePaintjobEventArgs(int playerid, int vehicleid, int paintjobid)
            : base(playerid, vehicleid)
        {
            PaintjobId = paintjobid;
        }

        public int PaintjobId { get; private set; }
    }
}