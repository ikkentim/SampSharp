// SampSharp
// Copyright 2020 Tim Potze
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
using SampSharp.Entities.SAMP.Definitions;

namespace SampSharp.Entities.SAMP
{
    /// <summary>
    /// Provides functionality for getting information about vehicle models and components.
    /// </summary>
    public interface IVehicleInfoService
    {
        /// <summary>
        /// Gets the car mod type of the specified <paramref name="componentId" />.
        /// </summary>
        /// <param name="componentId">The identifier of the component.</param>
        /// <returns>The car mod type of the component.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified <paramref name="componentId" /> is invalid.</exception>
        CarModType GetComponentType(int componentId);

        /// <summary>
        /// Gets information of type specified by <paramref name="infoType" /> for the specified <paramref name="vehicleModel" />.
        /// </summary>
        /// <param name="vehicleModel">The model of the vehicle.</param>
        /// <param name="infoType">The type of information to get.</param>
        /// <returns>The information about the vehicle model.</returns>
        Vector3 GetModelInfo(VehicleModelType vehicleModel, VehicleModelInfoType infoType);
    }
}