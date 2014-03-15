SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_CreateObject(int modelid, float x, float y, float z, float rX, float rY, float rZ, float DrawDistance);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_AttachObjectToVehicle(int objectid, int vehicleid, float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_AttachObjectToObject(int objectid, int attachtoid, float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ, bool SyncRotation);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_AttachObjectToPlayer(int objectid, int playerid, float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetObjectPos(int objectid, float x, float y, float z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetObjectPos(int objectid, float * x, float * y, float * z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetObjectRot(int objectid, float rotX, float rotY, float rotZ);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetObjectRot(int objectid, float * rotX, float * rotY, float * rotZ);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_IsValidObject(int objectid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_DestroyObject(int objectid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_MoveObject(int objectid, float X, float Y, float Z, float Speed, float RotX, float RotY, float RotZ);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_StopObject(int objectid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_IsObjectMoving(int objectid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_EditObject(int playerid, int objectid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_EditPlayerObject(int playerid, int objectid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SelectObject(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_CancelEdit(int playerid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_CreatePlayerObject(int playerid, int modelid, float x, float y, float z, float rX, float rY, float rZ, float DrawDistance);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_AttachPlayerObjectToPlayer(int objectplayer, int objectid, int attachplayer, float OffsetX, float OffsetY, float OffsetZ, float rX, float rY, float rZ);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_AttachPlayerObjectToVehicle(int playerid, int objectid, int vehicleid, float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float RotZ);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerObjectPos(int playerid, int objectid, float x, float y, float z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetPlayerObjectPos(int playerid, int objectid, float * x, float * y, float * z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerObjectRot(int playerid, int objectid, float rotX, float rotY, float rotZ);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetPlayerObjectRot(int playerid, int objectid, float * rotX, float * rotY, float * rotZ);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_IsValidPlayerObject(int playerid, int objectid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_DestroyPlayerObject(int playerid, int objectid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_MovePlayerObject(int playerid, int objectid, float x, float y, float z, float Speed, float RotX, float RotY, float RotZ);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_StopPlayerObject(int playerid, int objectid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_IsPlayerObjectMoving(int playerid, int objectid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetObjectMaterial(int objectid, int materialindex, int modelid, const char * txdname, const char * texturename, int materialcolor);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerObjectMaterial(int playerid, int objectid, int materialindex, int modelid, const char * txdname, const char * texturename, int materialcolor);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetObjectMaterialText(int objectid, const char * text, int materialindex, int materialsize, const char * fontface, int fontsize, bool bold, int fontcolor, int backcolor, int textalignment);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerObjectMaterialText(int playerid, int objectid, const char * text, int materialindex, int materialsize, const char * fontface, int fontsize, bool bold, int fontcolor, int backcolor, int textalignment);

#ifndef __cplusplus

#define OBJECT_MATERIAL_SIZE_32x32 (10)
#define OBJECT_MATERIAL_SIZE_64x32 (20)
#define OBJECT_MATERIAL_SIZE_64x64 (30)
#define OBJECT_MATERIAL_SIZE_128x32 (40)
#define OBJECT_MATERIAL_SIZE_128x64 (50)
#define OBJECT_MATERIAL_SIZE_128x128 (60)
#define OBJECT_MATERIAL_SIZE_256x32 (70)
#define OBJECT_MATERIAL_SIZE_256x64 (80)
#define OBJECT_MATERIAL_SIZE_256x128 (90)
#define OBJECT_MATERIAL_SIZE_256x256 (100)
#define OBJECT_MATERIAL_SIZE_512x64 (110)
#define OBJECT_MATERIAL_SIZE_512x128 (120)
#define OBJECT_MATERIAL_SIZE_512x256 (130)
#define OBJECT_MATERIAL_SIZE_512x512 (140)
#define OBJECT_MATERIAL_TEXT_ALIGN_LEFT (0)
#define OBJECT_MATERIAL_TEXT_ALIGN_CENTER (1)
#define OBJECT_MATERIAL_TEXT_ALIGN_RIGHT (2)

#undef  CreateObject
#define CreateObject sampgdk_CreateObject
#undef  AttachObjectToVehicle
#define AttachObjectToVehicle sampgdk_AttachObjectToVehicle
#undef  AttachObjectToObject
#define AttachObjectToObject sampgdk_AttachObjectToObject
#undef  AttachObjectToPlayer
#define AttachObjectToPlayer sampgdk_AttachObjectToPlayer
#undef  SetObjectPos
#define SetObjectPos sampgdk_SetObjectPos
#undef  GetObjectPos
#define GetObjectPos sampgdk_GetObjectPos
#undef  SetObjectRot
#define SetObjectRot sampgdk_SetObjectRot
#undef  GetObjectRot
#define GetObjectRot sampgdk_GetObjectRot
#undef  IsValidObject
#define IsValidObject sampgdk_IsValidObject
#undef  DestroyObject
#define DestroyObject sampgdk_DestroyObject
#undef  MoveObject
#define MoveObject sampgdk_MoveObject
#undef  StopObject
#define StopObject sampgdk_StopObject
#undef  IsObjectMoving
#define IsObjectMoving sampgdk_IsObjectMoving
#undef  EditObject
#define EditObject sampgdk_EditObject
#undef  EditPlayerObject
#define EditPlayerObject sampgdk_EditPlayerObject
#undef  SelectObject
#define SelectObject sampgdk_SelectObject
#undef  CancelEdit
#define CancelEdit sampgdk_CancelEdit
#undef  CreatePlayerObject
#define CreatePlayerObject sampgdk_CreatePlayerObject
#undef  AttachPlayerObjectToPlayer
#define AttachPlayerObjectToPlayer sampgdk_AttachPlayerObjectToPlayer
#undef  AttachPlayerObjectToVehicle
#define AttachPlayerObjectToVehicle sampgdk_AttachPlayerObjectToVehicle
#undef  SetPlayerObjectPos
#define SetPlayerObjectPos sampgdk_SetPlayerObjectPos
#undef  GetPlayerObjectPos
#define GetPlayerObjectPos sampgdk_GetPlayerObjectPos
#undef  SetPlayerObjectRot
#define SetPlayerObjectRot sampgdk_SetPlayerObjectRot
#undef  GetPlayerObjectRot
#define GetPlayerObjectRot sampgdk_GetPlayerObjectRot
#undef  IsValidPlayerObject
#define IsValidPlayerObject sampgdk_IsValidPlayerObject
#undef  DestroyPlayerObject
#define DestroyPlayerObject sampgdk_DestroyPlayerObject
#undef  MovePlayerObject
#define MovePlayerObject sampgdk_MovePlayerObject
#undef  StopPlayerObject
#define StopPlayerObject sampgdk_StopPlayerObject
#undef  IsPlayerObjectMoving
#define IsPlayerObjectMoving sampgdk_IsPlayerObjectMoving
#undef  SetObjectMaterial
#define SetObjectMaterial sampgdk_SetObjectMaterial
#undef  SetPlayerObjectMaterial
#define SetPlayerObjectMaterial sampgdk_SetPlayerObjectMaterial
#undef  SetObjectMaterialText
#define SetObjectMaterialText sampgdk_SetObjectMaterialText
#undef  SetPlayerObjectMaterialText
#define SetPlayerObjectMaterialText sampgdk_SetPlayerObjectMaterialText

#else /* __cplusplus */

SAMPGDK_BEGIN_NAMESPACE

const int OBJECT_MATERIAL_SIZE_32x32 = 10;
const int OBJECT_MATERIAL_SIZE_64x32 = 20;
const int OBJECT_MATERIAL_SIZE_64x64 = 30;
const int OBJECT_MATERIAL_SIZE_128x32 = 40;
const int OBJECT_MATERIAL_SIZE_128x64 = 50;
const int OBJECT_MATERIAL_SIZE_128x128 = 60;
const int OBJECT_MATERIAL_SIZE_256x32 = 70;
const int OBJECT_MATERIAL_SIZE_256x64 = 80;
const int OBJECT_MATERIAL_SIZE_256x128 = 90;
const int OBJECT_MATERIAL_SIZE_256x256 = 100;
const int OBJECT_MATERIAL_SIZE_512x64 = 110;
const int OBJECT_MATERIAL_SIZE_512x128 = 120;
const int OBJECT_MATERIAL_SIZE_512x256 = 130;
const int OBJECT_MATERIAL_SIZE_512x512 = 140;
const int OBJECT_MATERIAL_TEXT_ALIGN_LEFT = 0;
const int OBJECT_MATERIAL_TEXT_ALIGN_CENTER = 1;
const int OBJECT_MATERIAL_TEXT_ALIGN_RIGHT = 2;

static inline int CreateObject(int modelid, float x, float y, float z, float rX, float rY, float rZ, float DrawDistance) {
  return ::sampgdk_CreateObject(modelid, x, y, z, rX, rY, rZ, DrawDistance);
}
static inline bool AttachObjectToVehicle(int objectid, int vehicleid, float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ) {
  return ::sampgdk_AttachObjectToVehicle(objectid, vehicleid, fOffsetX, fOffsetY, fOffsetZ, fRotX, fRotY, fRotZ);
}
static inline bool AttachObjectToObject(int objectid, int attachtoid, float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ, bool SyncRotation) {
  return ::sampgdk_AttachObjectToObject(objectid, attachtoid, fOffsetX, fOffsetY, fOffsetZ, fRotX, fRotY, fRotZ, SyncRotation);
}
static inline bool AttachObjectToPlayer(int objectid, int playerid, float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ) {
  return ::sampgdk_AttachObjectToPlayer(objectid, playerid, fOffsetX, fOffsetY, fOffsetZ, fRotX, fRotY, fRotZ);
}
static inline bool SetObjectPos(int objectid, float x, float y, float z) {
  return ::sampgdk_SetObjectPos(objectid, x, y, z);
}
static inline bool GetObjectPos(int objectid, float * x, float * y, float * z) {
  return ::sampgdk_GetObjectPos(objectid, x, y, z);
}
static inline bool SetObjectRot(int objectid, float rotX, float rotY, float rotZ) {
  return ::sampgdk_SetObjectRot(objectid, rotX, rotY, rotZ);
}
static inline bool GetObjectRot(int objectid, float * rotX, float * rotY, float * rotZ) {
  return ::sampgdk_GetObjectRot(objectid, rotX, rotY, rotZ);
}
static inline bool IsValidObject(int objectid) {
  return ::sampgdk_IsValidObject(objectid);
}
static inline bool DestroyObject(int objectid) {
  return ::sampgdk_DestroyObject(objectid);
}
static inline int MoveObject(int objectid, float X, float Y, float Z, float Speed, float RotX, float RotY, float RotZ) {
  return ::sampgdk_MoveObject(objectid, X, Y, Z, Speed, RotX, RotY, RotZ);
}
static inline bool StopObject(int objectid) {
  return ::sampgdk_StopObject(objectid);
}
static inline bool IsObjectMoving(int objectid) {
  return ::sampgdk_IsObjectMoving(objectid);
}
static inline bool EditObject(int playerid, int objectid) {
  return ::sampgdk_EditObject(playerid, objectid);
}
static inline bool EditPlayerObject(int playerid, int objectid) {
  return ::sampgdk_EditPlayerObject(playerid, objectid);
}
static inline bool SelectObject(int playerid) {
  return ::sampgdk_SelectObject(playerid);
}
static inline bool CancelEdit(int playerid) {
  return ::sampgdk_CancelEdit(playerid);
}
static inline int CreatePlayerObject(int playerid, int modelid, float x, float y, float z, float rX, float rY, float rZ, float DrawDistance) {
  return ::sampgdk_CreatePlayerObject(playerid, modelid, x, y, z, rX, rY, rZ, DrawDistance);
}
static inline bool AttachPlayerObjectToPlayer(int objectplayer, int objectid, int attachplayer, float OffsetX, float OffsetY, float OffsetZ, float rX, float rY, float rZ) {
  return ::sampgdk_AttachPlayerObjectToPlayer(objectplayer, objectid, attachplayer, OffsetX, OffsetY, OffsetZ, rX, rY, rZ);
}
static inline bool AttachPlayerObjectToVehicle(int playerid, int objectid, int vehicleid, float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float RotZ) {
  return ::sampgdk_AttachPlayerObjectToVehicle(playerid, objectid, vehicleid, fOffsetX, fOffsetY, fOffsetZ, fRotX, fRotY, RotZ);
}
static inline bool SetPlayerObjectPos(int playerid, int objectid, float x, float y, float z) {
  return ::sampgdk_SetPlayerObjectPos(playerid, objectid, x, y, z);
}
static inline bool GetPlayerObjectPos(int playerid, int objectid, float * x, float * y, float * z) {
  return ::sampgdk_GetPlayerObjectPos(playerid, objectid, x, y, z);
}
static inline bool SetPlayerObjectRot(int playerid, int objectid, float rotX, float rotY, float rotZ) {
  return ::sampgdk_SetPlayerObjectRot(playerid, objectid, rotX, rotY, rotZ);
}
static inline bool GetPlayerObjectRot(int playerid, int objectid, float * rotX, float * rotY, float * rotZ) {
  return ::sampgdk_GetPlayerObjectRot(playerid, objectid, rotX, rotY, rotZ);
}
static inline bool IsValidPlayerObject(int playerid, int objectid) {
  return ::sampgdk_IsValidPlayerObject(playerid, objectid);
}
static inline bool DestroyPlayerObject(int playerid, int objectid) {
  return ::sampgdk_DestroyPlayerObject(playerid, objectid);
}
static inline int MovePlayerObject(int playerid, int objectid, float x, float y, float z, float Speed, float RotX, float RotY, float RotZ) {
  return ::sampgdk_MovePlayerObject(playerid, objectid, x, y, z, Speed, RotX, RotY, RotZ);
}
static inline bool StopPlayerObject(int playerid, int objectid) {
  return ::sampgdk_StopPlayerObject(playerid, objectid);
}
static inline bool IsPlayerObjectMoving(int playerid, int objectid) {
  return ::sampgdk_IsPlayerObjectMoving(playerid, objectid);
}
static inline bool SetObjectMaterial(int objectid, int materialindex, int modelid, const char * txdname, const char * texturename, int materialcolor) {
  return ::sampgdk_SetObjectMaterial(objectid, materialindex, modelid, txdname, texturename, materialcolor);
}
static inline bool SetPlayerObjectMaterial(int playerid, int objectid, int materialindex, int modelid, const char * txdname, const char * texturename, int materialcolor) {
  return ::sampgdk_SetPlayerObjectMaterial(playerid, objectid, materialindex, modelid, txdname, texturename, materialcolor);
}
static inline bool SetObjectMaterialText(int objectid, const char * text, int materialindex, int materialsize, const char * fontface, int fontsize, bool bold, int fontcolor, int backcolor, int textalignment) {
  return ::sampgdk_SetObjectMaterialText(objectid, text, materialindex, materialsize, fontface, fontsize, bold, fontcolor, backcolor, textalignment);
}
static inline bool SetPlayerObjectMaterialText(int playerid, int objectid, const char * text, int materialindex, int materialsize, const char * fontface, int fontsize, bool bold, int fontcolor, int backcolor, int textalignment) {
  return ::sampgdk_SetPlayerObjectMaterialText(playerid, objectid, text, materialindex, materialsize, fontface, fontsize, bold, fontcolor, backcolor, textalignment);
}

SAMPGDK_END_NAMESPACE

#endif /* __cplusplus */

