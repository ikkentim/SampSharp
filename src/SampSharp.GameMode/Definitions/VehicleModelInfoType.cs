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
namespace SampSharp.GameMode.Definitions
{
    /// <summary>
    ///     Contains all vehiclemodel info types.
    /// </summary>
    public enum VehicleModelInfoType
    {
        /// <summary>
        ///     Vehicle size
        /// </summary>
        Size = 1,

        /// <summary>
        ///     Position of the front seat. (calculated from the center of the vehicle)
        /// </summary>
        FrontSeat = 2,

        /// <summary>
        ///     Position of the rear seat. (calculated from the center of the vehicle)
        /// </summary>
        RearSeat = 3,

        /// <summary>
        ///     Position of the fuel cap. (calculated from the center of the vehicle)
        /// </summary>
        PetrolCap = 4,

        /// <summary>
        ///     Position of the front wheels. (calculated from the center of the vehicle)
        /// </summary>
        WheelsFront = 5,

        /// <summary>
        ///     Position of the rear wheels. (calculated from the center of the vehicle)
        /// </summary>
        WheelsRear = 6,

        /// <summary>
        ///     Position of the middle wheels, applies to vehicles with 3 axes. (calculated from the center of the vehicle)
        /// </summary>
        WheelsMiddle = 7,

        /// <summary>
        ///     Height of the front bumper. (calculated from the center of the vehicle)
        /// </summary>
        FrontBumperZ = 8,

        /// <summary>
        ///     Height of the rear bumper. (calculated from the center of the vehicle)
        /// </summary>
        RearBumperZ = 9
    }
}