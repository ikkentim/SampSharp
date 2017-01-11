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
    ///     Contains all special actions.
    /// </summary>
    public enum SpecialAction
    {
        /// <summary>
        ///     Nothing.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Player is ducking.
        /// </summary>
        Duck = 1,

        /// <summary>
        ///     Player is using a jetpack.
        /// </summary>
        Usejetpack = 2,

        /// <summary>
        ///     Player is entering a vehicle.
        /// </summary>
        EnterVehicle = 3,

        /// <summary>
        ///     Player is leaving a vehicle.
        /// </summary>
        ExitVehicle = 4,

        /// <summary>
        ///     Player is dancing. (Style 1)
        /// </summary>
        Dance1 = 5,

        /// <summary>
        ///     Player is dancing. (Style 2)
        /// </summary>
        Dance2 = 6,

        /// <summary>
        ///     Player is dancing. (Style 3)
        /// </summary>
        Dance3 = 7,

        /// <summary>
        ///     Player is dancing. (Style 4)
        /// </summary>
        Dance4 = 8,

        /// <summary>
        ///     Player is holding his hands up.
        /// </summary>
        HandsUp = 10,

        /// <summary>
        ///     Player is using a cellphone.
        /// </summary>
        UseCellphone = 11,

        /// <summary>
        ///     Player is sitting.
        /// </summary>
        Sitting = 12,

        /// <summary>
        ///     Player stops using a cellphone.
        /// </summary>
        StopUseCellphone = 13,

        /// <summary>
        ///     Player is drinking a beer.
        /// </summary>
        DrinkBeer = 20,

        /// <summary>
        ///     Player is smokking a cigarette.
        /// </summary>
        SmokeCiggy = 21,

        /// <summary>
        ///     Player is drinking whine.
        /// </summary>
        DrinkWine = 22,

        /// <summary>
        ///     Player is drinking sprunk.
        /// </summary>
        DrinkSprunk = 23,

        /// <summary>
        ///     Player is cuffed.
        /// </summary>
        Cuffed = 24,

        /// <summary>
        ///     PLayer is carrying.
        /// </summary>
        Carry = 25
    }
}