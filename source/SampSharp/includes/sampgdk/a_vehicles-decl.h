SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_IsValidVehicle(int vehicleid);
SAMPGDK_NATIVE_EXPORT float SAMPGDK_NATIVE_CALL sampgdk_GetVehicleDistanceFromPoint(int vehicleid, float x, float y, float z);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_CreateVehicle(int vehicletype, float x, float y, float z, float rotation, int color1, int color2, int respawn_delay);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_DestroyVehicle(int vehicleid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_IsVehicleStreamedIn(int vehicleid, int forplayerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetVehiclePos(int vehicleid, float * x, float * y, float * z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetVehiclePos(int vehicleid, float x, float y, float z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetVehicleZAngle(int vehicleid, float * z_angle);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetVehicleRotationQuat(int vehicleid, float * w, float * x, float * y, float * z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetVehicleZAngle(int vehicleid, float z_angle);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetVehicleParamsForPlayer(int vehicleid, int playerid, bool objective, bool doorslocked);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_ManualVehicleEngineAndLights();
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetVehicleParamsEx(int vehicleid, bool engine, bool lights, bool alarm, bool doors, bool bonnet, bool boot, bool objective);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetVehicleParamsEx(int vehicleid, bool * engine, bool * lights, bool * alarm, bool * doors, bool * bonnet, bool * boot, bool * objective);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetVehicleToRespawn(int vehicleid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_LinkVehicleToInterior(int vehicleid, int interiorid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_AddVehicleComponent(int vehicleid, int componentid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_RemoveVehicleComponent(int vehicleid, int componentid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_ChangeVehicleColor(int vehicleid, int color1, int color2);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_ChangeVehiclePaintjob(int vehicleid, int paintjobid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetVehicleHealth(int vehicleid, float health);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetVehicleHealth(int vehicleid, float * health);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_AttachTrailerToVehicle(int trailerid, int vehicleid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_DetachTrailerFromVehicle(int vehicleid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_IsTrailerAttachedToVehicle(int vehicleid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetVehicleTrailer(int vehicleid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetVehicleNumberPlate(int vehicleid, const char * numberplate);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetVehicleModel(int vehicleid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetVehicleComponentInSlot(int vehicleid, int slot);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetVehicleComponentType(int component);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_RepairVehicle(int vehicleid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetVehicleVelocity(int vehicleid, float * X, float * Y, float * Z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetVehicleVelocity(int vehicleid, float X, float Y, float Z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetVehicleAngularVelocity(int vehicleid, float X, float Y, float Z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetVehicleDamageStatus(int vehicleid, int * panels, int * doors, int * lights, int * tires);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_UpdateVehicleDamageStatus(int vehicleid, int panels, int doors, int lights, int tires);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetVehicleVirtualWorld(int vehicleid, int worldid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetVehicleVirtualWorld(int vehicleid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetVehicleModelInfo(int model, int infotype, float * X, float * Y, float * Z);

#ifndef __cplusplus

#define CARMODTYPE_SPOILER (0)
#define CARMODTYPE_HOOD (1)
#define CARMODTYPE_ROOF (2)
#define CARMODTYPE_SIDESKIRT (3)
#define CARMODTYPE_LAMPS (4)
#define CARMODTYPE_NITRO (5)
#define CARMODTYPE_EXHAUST (6)
#define CARMODTYPE_WHEELS (7)
#define CARMODTYPE_STEREO (8)
#define CARMODTYPE_HYDRAULICS (9)
#define CARMODTYPE_FRONT_BUMPER (10)
#define CARMODTYPE_REAR_BUMPER (11)
#define CARMODTYPE_VENT_RIGHT (12)
#define CARMODTYPE_VENT_LEFT (13)
#define VEHICLE_PARAMS_UNSET (-1)
#define VEHICLE_PARAMS_OFF (0)
#define VEHICLE_PARAMS_ON (1)
#define VEHICLE_MODEL_INFO_SIZE (1)
#define VEHICLE_MODEL_INFO_FRONTSEAT (2)
#define VEHICLE_MODEL_INFO_REARSEAT (3)
#define VEHICLE_MODEL_INFO_PETROLCAP (4)
#define VEHICLE_MODEL_INFO_WHEELSFRONT (5)
#define VEHICLE_MODEL_INFO_WHEELSREAR (6)
#define VEHICLE_MODEL_INFO_WHEELSMID (7)
#define VEHICLE_MODEL_INFO_FRONT_BUMPER_Z (8)
#define VEHICLE_MODEL_INFO_REAR_BUMPER_Z (9)

#undef  IsValidVehicle
#define IsValidVehicle sampgdk_IsValidVehicle
#undef  GetVehicleDistanceFromPoint
#define GetVehicleDistanceFromPoint sampgdk_GetVehicleDistanceFromPoint
#undef  CreateVehicle
#define CreateVehicle sampgdk_CreateVehicle
#undef  DestroyVehicle
#define DestroyVehicle sampgdk_DestroyVehicle
#undef  IsVehicleStreamedIn
#define IsVehicleStreamedIn sampgdk_IsVehicleStreamedIn
#undef  GetVehiclePos
#define GetVehiclePos sampgdk_GetVehiclePos
#undef  SetVehiclePos
#define SetVehiclePos sampgdk_SetVehiclePos
#undef  GetVehicleZAngle
#define GetVehicleZAngle sampgdk_GetVehicleZAngle
#undef  GetVehicleRotationQuat
#define GetVehicleRotationQuat sampgdk_GetVehicleRotationQuat
#undef  SetVehicleZAngle
#define SetVehicleZAngle sampgdk_SetVehicleZAngle
#undef  SetVehicleParamsForPlayer
#define SetVehicleParamsForPlayer sampgdk_SetVehicleParamsForPlayer
#undef  ManualVehicleEngineAndLights
#define ManualVehicleEngineAndLights sampgdk_ManualVehicleEngineAndLights
#undef  SetVehicleParamsEx
#define SetVehicleParamsEx sampgdk_SetVehicleParamsEx
#undef  GetVehicleParamsEx
#define GetVehicleParamsEx sampgdk_GetVehicleParamsEx
#undef  SetVehicleToRespawn
#define SetVehicleToRespawn sampgdk_SetVehicleToRespawn
#undef  LinkVehicleToInterior
#define LinkVehicleToInterior sampgdk_LinkVehicleToInterior
#undef  AddVehicleComponent
#define AddVehicleComponent sampgdk_AddVehicleComponent
#undef  RemoveVehicleComponent
#define RemoveVehicleComponent sampgdk_RemoveVehicleComponent
#undef  ChangeVehicleColor
#define ChangeVehicleColor sampgdk_ChangeVehicleColor
#undef  ChangeVehiclePaintjob
#define ChangeVehiclePaintjob sampgdk_ChangeVehiclePaintjob
#undef  SetVehicleHealth
#define SetVehicleHealth sampgdk_SetVehicleHealth
#undef  GetVehicleHealth
#define GetVehicleHealth sampgdk_GetVehicleHealth
#undef  AttachTrailerToVehicle
#define AttachTrailerToVehicle sampgdk_AttachTrailerToVehicle
#undef  DetachTrailerFromVehicle
#define DetachTrailerFromVehicle sampgdk_DetachTrailerFromVehicle
#undef  IsTrailerAttachedToVehicle
#define IsTrailerAttachedToVehicle sampgdk_IsTrailerAttachedToVehicle
#undef  GetVehicleTrailer
#define GetVehicleTrailer sampgdk_GetVehicleTrailer
#undef  SetVehicleNumberPlate
#define SetVehicleNumberPlate sampgdk_SetVehicleNumberPlate
#undef  GetVehicleModel
#define GetVehicleModel sampgdk_GetVehicleModel
#undef  GetVehicleComponentInSlot
#define GetVehicleComponentInSlot sampgdk_GetVehicleComponentInSlot
#undef  GetVehicleComponentType
#define GetVehicleComponentType sampgdk_GetVehicleComponentType
#undef  RepairVehicle
#define RepairVehicle sampgdk_RepairVehicle
#undef  GetVehicleVelocity
#define GetVehicleVelocity sampgdk_GetVehicleVelocity
#undef  SetVehicleVelocity
#define SetVehicleVelocity sampgdk_SetVehicleVelocity
#undef  SetVehicleAngularVelocity
#define SetVehicleAngularVelocity sampgdk_SetVehicleAngularVelocity
#undef  GetVehicleDamageStatus
#define GetVehicleDamageStatus sampgdk_GetVehicleDamageStatus
#undef  UpdateVehicleDamageStatus
#define UpdateVehicleDamageStatus sampgdk_UpdateVehicleDamageStatus
#undef  SetVehicleVirtualWorld
#define SetVehicleVirtualWorld sampgdk_SetVehicleVirtualWorld
#undef  GetVehicleVirtualWorld
#define GetVehicleVirtualWorld sampgdk_GetVehicleVirtualWorld
#undef  GetVehicleModelInfo
#define GetVehicleModelInfo sampgdk_GetVehicleModelInfo

#else /* __cplusplus */

SAMPGDK_BEGIN_NAMESPACE

const int CARMODTYPE_SPOILER = 0;
const int CARMODTYPE_HOOD = 1;
const int CARMODTYPE_ROOF = 2;
const int CARMODTYPE_SIDESKIRT = 3;
const int CARMODTYPE_LAMPS = 4;
const int CARMODTYPE_NITRO = 5;
const int CARMODTYPE_EXHAUST = 6;
const int CARMODTYPE_WHEELS = 7;
const int CARMODTYPE_STEREO = 8;
const int CARMODTYPE_HYDRAULICS = 9;
const int CARMODTYPE_FRONT_BUMPER = 10;
const int CARMODTYPE_REAR_BUMPER = 11;
const int CARMODTYPE_VENT_RIGHT = 12;
const int CARMODTYPE_VENT_LEFT = 13;
const int VEHICLE_PARAMS_UNSET = -1;
const int VEHICLE_PARAMS_OFF = 0;
const int VEHICLE_PARAMS_ON = 1;
const int VEHICLE_MODEL_INFO_SIZE = 1;
const int VEHICLE_MODEL_INFO_FRONTSEAT = 2;
const int VEHICLE_MODEL_INFO_REARSEAT = 3;
const int VEHICLE_MODEL_INFO_PETROLCAP = 4;
const int VEHICLE_MODEL_INFO_WHEELSFRONT = 5;
const int VEHICLE_MODEL_INFO_WHEELSREAR = 6;
const int VEHICLE_MODEL_INFO_WHEELSMID = 7;
const int VEHICLE_MODEL_INFO_FRONT_BUMPER_Z = 8;
const int VEHICLE_MODEL_INFO_REAR_BUMPER_Z = 9;

static inline bool IsValidVehicle(int vehicleid) {
  return ::sampgdk_IsValidVehicle(vehicleid);
}
static inline float GetVehicleDistanceFromPoint(int vehicleid, float x, float y, float z) {
  return ::sampgdk_GetVehicleDistanceFromPoint(vehicleid, x, y, z);
}
static inline int CreateVehicle(int vehicletype, float x, float y, float z, float rotation, int color1, int color2, int respawn_delay) {
  return ::sampgdk_CreateVehicle(vehicletype, x, y, z, rotation, color1, color2, respawn_delay);
}
static inline bool DestroyVehicle(int vehicleid) {
  return ::sampgdk_DestroyVehicle(vehicleid);
}
static inline bool IsVehicleStreamedIn(int vehicleid, int forplayerid) {
  return ::sampgdk_IsVehicleStreamedIn(vehicleid, forplayerid);
}
static inline bool GetVehiclePos(int vehicleid, float * x, float * y, float * z) {
  return ::sampgdk_GetVehiclePos(vehicleid, x, y, z);
}
static inline bool SetVehiclePos(int vehicleid, float x, float y, float z) {
  return ::sampgdk_SetVehiclePos(vehicleid, x, y, z);
}
static inline bool GetVehicleZAngle(int vehicleid, float * z_angle) {
  return ::sampgdk_GetVehicleZAngle(vehicleid, z_angle);
}
static inline bool GetVehicleRotationQuat(int vehicleid, float * w, float * x, float * y, float * z) {
  return ::sampgdk_GetVehicleRotationQuat(vehicleid, w, x, y, z);
}
static inline bool SetVehicleZAngle(int vehicleid, float z_angle) {
  return ::sampgdk_SetVehicleZAngle(vehicleid, z_angle);
}
static inline bool SetVehicleParamsForPlayer(int vehicleid, int playerid, bool objective, bool doorslocked) {
  return ::sampgdk_SetVehicleParamsForPlayer(vehicleid, playerid, objective, doorslocked);
}
static inline bool ManualVehicleEngineAndLights() {
  return ::sampgdk_ManualVehicleEngineAndLights();
}
static inline bool SetVehicleParamsEx(int vehicleid, bool engine, bool lights, bool alarm, bool doors, bool bonnet, bool boot, bool objective) {
  return ::sampgdk_SetVehicleParamsEx(vehicleid, engine, lights, alarm, doors, bonnet, boot, objective);
}
static inline bool GetVehicleParamsEx(int vehicleid, bool * engine, bool * lights, bool * alarm, bool * doors, bool * bonnet, bool * boot, bool * objective) {
  return ::sampgdk_GetVehicleParamsEx(vehicleid, engine, lights, alarm, doors, bonnet, boot, objective);
}
static inline bool SetVehicleToRespawn(int vehicleid) {
  return ::sampgdk_SetVehicleToRespawn(vehicleid);
}
static inline bool LinkVehicleToInterior(int vehicleid, int interiorid) {
  return ::sampgdk_LinkVehicleToInterior(vehicleid, interiorid);
}
static inline bool AddVehicleComponent(int vehicleid, int componentid) {
  return ::sampgdk_AddVehicleComponent(vehicleid, componentid);
}
static inline bool RemoveVehicleComponent(int vehicleid, int componentid) {
  return ::sampgdk_RemoveVehicleComponent(vehicleid, componentid);
}
static inline bool ChangeVehicleColor(int vehicleid, int color1, int color2) {
  return ::sampgdk_ChangeVehicleColor(vehicleid, color1, color2);
}
static inline bool ChangeVehiclePaintjob(int vehicleid, int paintjobid) {
  return ::sampgdk_ChangeVehiclePaintjob(vehicleid, paintjobid);
}
static inline bool SetVehicleHealth(int vehicleid, float health) {
  return ::sampgdk_SetVehicleHealth(vehicleid, health);
}
static inline bool GetVehicleHealth(int vehicleid, float * health) {
  return ::sampgdk_GetVehicleHealth(vehicleid, health);
}
static inline bool AttachTrailerToVehicle(int trailerid, int vehicleid) {
  return ::sampgdk_AttachTrailerToVehicle(trailerid, vehicleid);
}
static inline bool DetachTrailerFromVehicle(int vehicleid) {
  return ::sampgdk_DetachTrailerFromVehicle(vehicleid);
}
static inline bool IsTrailerAttachedToVehicle(int vehicleid) {
  return ::sampgdk_IsTrailerAttachedToVehicle(vehicleid);
}
static inline int GetVehicleTrailer(int vehicleid) {
  return ::sampgdk_GetVehicleTrailer(vehicleid);
}
static inline bool SetVehicleNumberPlate(int vehicleid, const char * numberplate) {
  return ::sampgdk_SetVehicleNumberPlate(vehicleid, numberplate);
}
static inline int GetVehicleModel(int vehicleid) {
  return ::sampgdk_GetVehicleModel(vehicleid);
}
static inline int GetVehicleComponentInSlot(int vehicleid, int slot) {
  return ::sampgdk_GetVehicleComponentInSlot(vehicleid, slot);
}
static inline int GetVehicleComponentType(int component) {
  return ::sampgdk_GetVehicleComponentType(component);
}
static inline bool RepairVehicle(int vehicleid) {
  return ::sampgdk_RepairVehicle(vehicleid);
}
static inline bool GetVehicleVelocity(int vehicleid, float * X, float * Y, float * Z) {
  return ::sampgdk_GetVehicleVelocity(vehicleid, X, Y, Z);
}
static inline bool SetVehicleVelocity(int vehicleid, float X, float Y, float Z) {
  return ::sampgdk_SetVehicleVelocity(vehicleid, X, Y, Z);
}
static inline bool SetVehicleAngularVelocity(int vehicleid, float X, float Y, float Z) {
  return ::sampgdk_SetVehicleAngularVelocity(vehicleid, X, Y, Z);
}
static inline bool GetVehicleDamageStatus(int vehicleid, int * panels, int * doors, int * lights, int * tires) {
  return ::sampgdk_GetVehicleDamageStatus(vehicleid, panels, doors, lights, tires);
}
static inline bool UpdateVehicleDamageStatus(int vehicleid, int panels, int doors, int lights, int tires) {
  return ::sampgdk_UpdateVehicleDamageStatus(vehicleid, panels, doors, lights, tires);
}
static inline bool SetVehicleVirtualWorld(int vehicleid, int worldid) {
  return ::sampgdk_SetVehicleVirtualWorld(vehicleid, worldid);
}
static inline int GetVehicleVirtualWorld(int vehicleid) {
  return ::sampgdk_GetVehicleVirtualWorld(vehicleid);
}
static inline bool GetVehicleModelInfo(int model, int infotype, float * X, float * Y, float * Z) {
  return ::sampgdk_GetVehicleModelInfo(model, infotype, X, Y, Z);
}

SAMPGDK_END_NAMESPACE

#endif /* __cplusplus */

