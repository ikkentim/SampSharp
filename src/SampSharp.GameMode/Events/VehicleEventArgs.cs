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
using System;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the _not_yet_ event.
    /// </summary>
    public class VehicleEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VehicleEventArgs" /> class.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        public VehicleEventArgs(BaseVehicle vehicle)
        {
            Vehicle = vehicle;
        }

        /// <summary>
        ///     Gets the vehicle involved.
        /// </summary>
        public BaseVehicle Vehicle { get; private set; }
    }
}