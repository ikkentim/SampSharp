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

namespace SampSharp.GameMode.Definitions
{
    /// <summary>
    ///     Contains all camera modes.
    /// </summary>
    public enum CameraMode
    {
        /// <summary>
        ///     Invalid mode.
        /// </summary>
        Invalid = -1,

        /// <summary>
        ///     Camera is behind a car.
        /// </summary>
        BehindCar = 3,

        /// <summary>
        ///     Camera is behind a Ped.
        /// </summary>
        FollowPed = 4,

        /// <summary>
        ///     Sniper view.
        /// </summary>
        SniperAiming = 7,

        /// <summary>
        ///     Rocket launcher view.
        /// </summary>
        RocketLauncherAiming = 8,

        /// <summary>
        ///     Camera is set to a fixed point (e.g. after setting <see cref="Player.CameraPosition" />)
        /// </summary>
        Fixed = 15,

        /// <summary>
        ///     Camera is in first person mode (e.g. when looking from inside the vehicle)
        /// </summary>
        FirstPerson = 16,

        /// <summary>
        ///     Camera 'normally' behind a car.
        /// </summary>
        NormalCar = 18,

        /// <summary>
        ///     Camera behind a boat.
        /// </summary>
        BehindBoat = 22,

        /// <summary>
        ///     Camera when aiming.
        /// </summary>
        CameraWeaponAiming = 46,

        /// <summary>
        ///     Heatseeking rochet launcher view.
        /// </summary>
        HeatseekingRocketLauncher = 51,

        /// <summary>
        ///     Aiming a weapon.
        /// </summary>
        AimingWeapon = 53,

        /// <summary>
        ///     Drive by view.
        /// </summary>
        VehicleDriveBy = 55,

        /// <summary>
        ///     Helicopter chase view.
        /// </summary>
        HelicopterChaseCam = 56
    }
}