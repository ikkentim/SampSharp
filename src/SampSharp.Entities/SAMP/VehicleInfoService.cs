using System;
using System.Collections.Generic;
using SampSharp.Core.Natives.NativeObjects;
using SampSharp.Entities.SAMP.Definitions;

namespace SampSharp.Entities.SAMP
{
    /// <summary>
    /// Represents a provider of information about vehicles using the natives provided by SA:MP in combination with a local info cache.
    /// </summary>
    /// <seealso cref="IVehicleInfoService" />
    public class VehicleInfoService : IVehicleInfoService
    {
        private readonly VehicleInfoServiceNative _native;
        private readonly Dictionary<int, int> _componentType = new Dictionary<int, int>();

        private readonly Dictionary<(VehicleModelType, VehicleModelInfoType), Vector3> _modelInfo =
            new Dictionary<(VehicleModelType, VehicleModelInfoType), Vector3>();

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

            return (CarModType)result;
        }

        /// <inheritdoc />
        public Vector3 GetModelInfo(VehicleModelType vehicleModel, VehicleModelInfoType infoType)
        {
            if (!_modelInfo.TryGetValue((vehicleModel, infoType), out var result))
            {
                _native.GetVehicleModelInfo((int)vehicleModel, (int)infoType, out var x, out var y, out var z);
                _modelInfo[(vehicleModel, infoType)] = result = new Vector3(x, y, z);
            }

            return result;
        }
    }
}
