// SampSharp
// Copyright 2017 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Definitions
{
    /// <summary>
    ///     Contains all camera modes.
    /// </summary>
    /// <remarks>
    ///     See <see href="http://wiki.sa-mp.com/wiki/CameraModes">http://wiki.sa-mp.com/wiki/CameraModes</see>.
    /// </remarks>
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
        ///     Camera is set to a fixed point (e.g. after setting <see cref="BasePlayer.CameraPosition" />)
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