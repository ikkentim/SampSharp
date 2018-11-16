﻿// SampSharp
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
using SampSharp.GameMode.Helpers;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.VehicleDied" /> event.
    /// </summary>
    public class VehicleDiedEventArgs : VehicleEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VehicleSpawnedEventArgs" /> class.
        /// </summary>
        /// <param name="killer">The killer.</param>
        /// <param name="vehicle">The vehicle.</param>
        public VehicleDiedEventArgs(BasePlayer killer, BaseVehicle vehicle) : base(vehicle)
        {
            Killer = Optional<BasePlayer>.From(killer);
        }
        
        /// <summary>
        ///     Gets the killer involved or null.
        /// </summary>
        public Optional<BasePlayer> Killer { get; private set; }
    }
}