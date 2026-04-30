#include <sdk.hpp>
#include <Server/Components/Vehicles/vehicle_components.hpp>
#include <Server/Components/Vehicles/vehicle_models.hpp>
#include <Server/Components/Vehicles/vehicle_colours.hpp>
#include <Server/Components/Vehicles/vehicle_seats.hpp>

extern "C" SDK_EXPORT bool __CDECL vehicles_isValidComponentForVehicleModel(int vehicleModel, int componentId)
{
    return Impl::isValidComponentForVehicleModel(vehicleModel, componentId);
}
extern "C" SDK_EXPORT bool __CDECL vehicles_getVehicleComponentSlot(int component)
{
    return Impl::getVehicleComponentSlot(component);
}
extern "C" SDK_EXPORT bool vehicles_getVehicleModelInfo(int model, VehicleModelInfoType type, Vector3& out)
{
    return Impl::getVehicleModelInfo(model, type, out);
}
extern "C" SDK_EXPORT void vehicles_getRandomVehicleColour(int modelid, int& colour1, int& colour2, int& colour3, int& colour4)
{
    return Impl::getRandomVehicleColour(modelid, colour1, colour2, colour3, colour4);
}
extern "C" SDK_EXPORT uint32_t vehicles_carColourIndexToColour(int index, uint32_t alpha = 0xFF)
{
    return Impl::carColourIndexToColour(index, alpha);
}

extern "C" SDK_EXPORT uint8_t vehicles_getVehiclePassengerSeats(int model)
{
    return Impl::getVehiclePassengerSeats(model);
}