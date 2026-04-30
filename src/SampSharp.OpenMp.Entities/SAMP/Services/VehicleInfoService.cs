using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class VehicleInfoService : IVehicleInfoService
{
    public CarModType GetComponentType(int componentId)
    {
        return (CarModType)VehicleData.GetVehicleComponentSlot(componentId);
    }

    public bool IsValidComponentForVehicle(VehicleModelType vehicleModel, int componentId)
    {
        return VehicleData.IsValidComponentForVehicleModel((int)vehicleModel, componentId);
    }

    public Vector3 GetModelInfo(VehicleModelType vehicleModel, VehicleModelInfoType infoType)
    {
        VehicleData.GetVehicleModelInfo((int)vehicleModel, (SampSharp.OpenMp.Core.Api.VehicleModelInfoType)infoType, out var outInfo);
        return outInfo;
    }

    public (int, int, int, int) GetRandomVehicleColor(VehicleModelType vehicleModel)
    {
        VehicleData.GetRandomVehicleColour((int)vehicleModel, out var a, out var b, out var c, out var d);
        return (a, b, c, d);
    }

    public Color GetColorFromVehicleColor(int vehicleColor, uint alpha = 0xff)
    {
        return VehicleData.CarColourIndexToColour((int)vehicleColor, alpha);
    }

    public int GetPassengerSeatCount(VehicleModelType vehicleModel)
    {
        return VehicleData.GetVehiclePassengerSeats((int)vehicleModel);
    }
}