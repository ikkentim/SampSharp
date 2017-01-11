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
    ///     Contains all modification types of vehicles.
    /// </summary>
    /// <remarks>
    ///     See <see href="http://wiki.sa-mp.com/wiki/Componentslots">http://wiki.sa-mp.com/wiki/Componentslots</see>.
    /// </remarks>
    public enum CarModType
    {
        /// <summary>
        ///     Car spoiler.
        /// </summary>
        Spoiler = 0,

        /// <summary>
        ///     Car hood.
        /// </summary>
        Hood = 1,

        /// <summary>
        ///     Car roof.
        /// </summary>
        Roof = 2,

        /// <summary>
        ///     Car sideskirts.
        /// </summary>
        Sideskirt = 3,

        /// <summary>
        ///     Car lamps.
        /// </summary>
        Lamps = 4,

        /// <summary>
        ///     Nitrogen.
        /// </summary>
        Nitro = 5,

        /// <summary>
        ///     Car exhaust.
        /// </summary>
        Exhaust = 6,

        /// <summary>
        ///     Car wheels.
        /// </summary>
        Wheels = 7,

        /// <summary>
        ///     Car stereo.
        /// </summary>
        Stereo = 8,

        /// <summary>
        ///     Car hydraulics.
        /// </summary>
        Hydraulics = 9,

        /// <summary>
        ///     Front car bumper.
        /// </summary>
        FrontBumper = 10,

        /// <summary>
        ///     Rear car bumper.
        /// </summary>
        RearBumper = 11,

        /// <summary>
        ///     Right car vent.
        /// </summary>
        VentRight = 12,

        /// <summary>
        ///     Left car vent.
        /// </summary>
        VentLeft = 13
    }
}