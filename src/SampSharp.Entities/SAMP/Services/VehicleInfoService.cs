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
using System.Collections.Generic;
using SampSharp.Core.Natives.NativeObjects;

namespace SampSharp.Entities.SAMP
{
    /// <summary>
    /// Represents a provider of information about vehicles using the natives provided by SA:MP in combination with a local
    /// info cache.
    /// </summary>
    /// <seealso cref="IVehicleInfoService" />
    public class VehicleInfoService : IVehicleInfoService
    {
        private readonly Dictionary<int, int> _componentType = new Dictionary<int, int>();

        private readonly Dictionary<(VehicleModelType, VehicleModelInfoType), Vector3> _modelInfo =
            new Dictionary<(VehicleModelType, VehicleModelInfoType), Vector3>();

        private readonly VehicleInfoServiceNative _native;

        /// <inheritdoc />
        public VehicleInfoService()
        {
            // TODO: Use some form of DI for native wrappers
            _native = NativeObjectProxyFactory.CreateInstance<VehicleInfoServiceNative>();
        }

        /// <inheritdoc />
        public CarModType GetComponentType(int componentId)
        {
            if (!_componentType.TryGetValue(componentId, out var result))
                _componentType[componentId] = result = _native.GetVehicleComponentType(componentId);

            if (result < 0)
                throw new ArgumentOutOfRangeException(nameof(componentId), componentId,
                    "component is not a valid component identifier.");

            return (CarModType) result;
        }

        /// <inheritdoc />
        public Vector3 GetModelInfo(VehicleModelType vehicleModel, VehicleModelInfoType infoType)
        {
            if (!_modelInfo.TryGetValue((vehicleModel, infoType), out var result))
            {
                _native.GetVehicleModelInfo((int) vehicleModel, (int) infoType, out var x, out var y, out var z);
                _modelInfo[(vehicleModel, infoType)] = result = new Vector3(x, y, z);
            }

            return result;
        }
    }
}