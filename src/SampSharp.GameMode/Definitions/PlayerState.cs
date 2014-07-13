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

namespace SampSharp.GameMode.Definitions
{
    /// <summary>
    ///     Contains all player states.
    /// </summary>
    public enum PlayerState
    {
        /// <summary>
        ///     No state.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Player is on foot.
        /// </summary>
        OnFoot = 1,

        /// <summary>
        ///     Player is driving a vehicle.
        /// </summary>
        Driving = 2,

        /// <summary>
        ///     Player is in a vehicle as passenger.
        /// </summary>
        Passenger = 3,

        /// <summary>
        ///     Player is exiting a vehicle.
        /// </summary>
        ExitVehicle = 4,

        /// <summary>
        ///     Player is entering a vehicle as driver.
        /// </summary>
        EnterVehicleDriver = 5,

        /// <summary>
        ///     Player is entering a vehicle as passenger.
        /// </summary>
        EnterVehiclePassenger = 6,

        /// <summary>
        ///     Player is dead.
        /// </summary>
        Wasted = 7,

        /// <summary>
        ///     Player has spawned.
        /// </summary>
        Spawned = 8,

        /// <summary>
        ///     Player is spectating.
        /// </summary>
        Spectating = 9
    }
}