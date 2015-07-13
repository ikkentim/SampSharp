// SampSharp
// Copyright 2015 Tim Potze
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

using SampSharp.GameMode.API;

namespace SampSharp.GameMode.World
{
    public partial class GtaVehicle
    {
        private static class Internal
        {
            public delegate int AddStaticVehicleExImpl(
                int modelid, float spawnX, float spawnY, float spawnZ, float zAngle, int color1, int color2,
                int respawnDelay, bool addsiren = false);

            public delegate int AddStaticVehicleImpl(
                int modelid, float spawnX, float spawnY, float spawnZ, float zAngle, int color1, int color2);

            public delegate bool AddVehicleComponentImpl(int vehicleid, int componentid);

            public delegate bool AttachTrailerToVehicleImpl(int trailerid, int vehicleid);

            public delegate bool ChangeVehicleColorImpl(int vehicleid, int color1, int color2);

            public delegate bool ChangeVehiclePaintjobImpl(int vehicleid, int paintjobid);

            public delegate int CreateVehicleImpl(
                int vehicletype, float x, float y, float z, float rotation, int color1,
                int color2, int respawnDelay, bool addsiren = false);

            public delegate bool DestroyVehicleImpl(int vehicleid);

            public delegate bool DetachTrailerFromVehicleImpl(int vehicleid);

            public delegate int GetVehicleComponentInSlotImpl(int vehicleid, int slot);

            public delegate int GetVehicleComponentTypeImpl(int component);

            public delegate bool GetVehicleDamageStatusImpl(
                int vehicleid, out int panels, out int doors, out int lights,
                out int tires);

            public delegate float GetVehicleDistanceFromPointImpl(int vehicleid, float x, float y, float z);

            public delegate bool GetVehicleHealthImpl(int vehicleid, out float health);

            public delegate int GetVehicleModelImpl(int vehicleid);

            public delegate bool GetVehicleModelInfoImpl(int model, int infotype, out float x, out float y, out float z);

            public delegate bool GetVehicleParamsCarDoorsImpl(int vehicleid, out int driver, out int passenger,
                out int backleft, out int backright);

            public delegate bool GetVehicleParamsCarWindowsImpl(int vehicleid, out int driver, out int passenger,
                out int backleft, out int backright);

            public delegate bool GetVehicleParamsExImpl(int vehicleid, out int engine, out int lights, out int alarm,
                out int doors, out int bonnet, out int boot, out int objective);

            public delegate int GetVehicleParamsSirenStateImpl(int vehicleid);

            public delegate int GetVehiclePoolSizeImpl();

            public delegate bool GetVehiclePosImpl(int vehicleid, out float x, out float y, out float z);

            public delegate bool GetVehicleRotationQuatImpl(int vehicleid, out float w, out float x, out float y,
                out float z);

            public delegate int GetVehicleTrailerImpl(int vehicleid);

            public delegate bool GetVehicleVelocityImpl(int vehicleid, out float x, out float y, out float z);

            public delegate int GetVehicleVirtualWorldImpl(int vehicleid);

            public delegate bool GetVehicleZAngleImpl(int vehicleid, out float zAngle);

            public delegate bool IsTrailerAttachedToVehicleImpl(int vehicleid);

            public delegate bool IsValidVehicleImpl(int vehicleid);

            public delegate bool IsVehicleStreamedInImpl(int vehicleid, int forplayerid);

            public delegate bool LinkVehicleToInteriorImpl(int vehicleid, int interiorid);

            public delegate bool RemoveVehicleComponentImpl(int vehicleid, int componentid);

            public delegate bool RepairVehicleImpl(int vehicleid);

            public delegate bool SetVehicleAngularVelocityImpl(int vehicleid, float x, float y, float z);

            public delegate bool SetVehicleHealthImpl(int vehicleid, float health);

            public delegate bool SetVehicleNumberPlateImpl(int vehicleid, string numberplate);

            public delegate bool SetVehicleParamsCarDoorsImpl(int vehicleid, int driver, int passenger, int backleft,
                int backright);

            public delegate bool SetVehicleParamsCarWindowsImpl(int vehicleid, int driver, int passenger, int backleft,
                int backright);

            public delegate bool SetVehicleParamsExImpl(int vehicleid, int engine, int lights, int alarm, int doors,
                int bonnet, int boot, int objective);

            public delegate bool SetVehicleParamsForPlayerImpl(int vehicleid, int playerid, bool objective,
                bool doorslocked);

            public delegate bool SetVehiclePosImpl(int vehicleid, float x, float y, float z);

            public delegate bool SetVehicleToRespawnImpl(int vehicleid);

            public delegate bool SetVehicleVelocityImpl(int vehicleid, float x, float y, float z);

            public delegate bool SetVehicleVirtualWorldImpl(int vehicleid, int worldid);

            public delegate bool SetVehicleZAngleImpl(int vehicleid, float zAngle);

            public delegate bool UpdateVehicleDamageStatusImpl(
                int vehicleid, int panels, int doors, int lights, int tires);

            [Native("IsValidVehicle")] public static readonly IsValidVehicleImpl IsValidVehicle = null;

            [Native("GetVehicleDistanceFromPoint")] public static readonly GetVehicleDistanceFromPointImpl
                GetVehicleDistanceFromPoint = null;

            [Native("CreateVehicle")] public static readonly CreateVehicleImpl CreateVehicle = null;
            [Native("DestroyVehicle")] public static readonly DestroyVehicleImpl DestroyVehicle = null;
            [Native("IsVehicleStreamedIn")] public static readonly IsVehicleStreamedInImpl IsVehicleStreamedIn = null;
            [Native("GetVehiclePos")] public static readonly GetVehiclePosImpl GetVehiclePos = null;
            [Native("SetVehiclePos")] public static readonly SetVehiclePosImpl SetVehiclePos = null;
            [Native("GetVehicleZAngle")] public static readonly GetVehicleZAngleImpl GetVehicleZAngle = null;

            [Native("GetVehicleRotationQuat")] public static readonly GetVehicleRotationQuatImpl GetVehicleRotationQuat
                =
                null;

            [Native("SetVehicleZAngle")] public static readonly SetVehicleZAngleImpl SetVehicleZAngle = null;

            [Native("SetVehicleParamsForPlayer")] public static readonly SetVehicleParamsForPlayerImpl
                SetVehicleParamsForPlayer = null;

            [Native("SetVehicleParamsEx")] public static readonly SetVehicleParamsExImpl SetVehicleParamsEx = null;
            [Native("GetVehicleParamsEx")] public static readonly GetVehicleParamsExImpl GetVehicleParamsEx = null;

            [Native("GetVehicleParamsSirenState")] public static readonly GetVehicleParamsSirenStateImpl
                GetVehicleParamsSirenState = null;

            [Native("SetVehicleParamsCarDoors")] public static readonly SetVehicleParamsCarDoorsImpl
                SetVehicleParamsCarDoors = null;

            [Native("GetVehicleParamsCarDoors")] public static readonly GetVehicleParamsCarDoorsImpl
                GetVehicleParamsCarDoors = null;

            [Native("SetVehicleParamsCarWindows")] public static readonly SetVehicleParamsCarWindowsImpl
                SetVehicleParamsCarWindows = null;

            [Native("GetVehicleParamsCarWindows")] public static readonly GetVehicleParamsCarWindowsImpl
                GetVehicleParamsCarWindows = null;

            [Native("SetVehicleToRespawn")] public static readonly SetVehicleToRespawnImpl SetVehicleToRespawn = null;

            [Native("LinkVehicleToInterior")] public static readonly LinkVehicleToInteriorImpl LinkVehicleToInterior =
                null;

            [Native("AddVehicleComponent")] public static readonly AddVehicleComponentImpl AddVehicleComponent = null;

            [Native("RemoveVehicleComponent")] public static readonly RemoveVehicleComponentImpl RemoveVehicleComponent
                =
                null;

            [Native("ChangeVehicleColor")] public static readonly ChangeVehicleColorImpl ChangeVehicleColor = null;

            [Native("ChangeVehiclePaintjob")] public static readonly ChangeVehiclePaintjobImpl ChangeVehiclePaintjob =
                null;

            [Native("SetVehicleHealth")] public static readonly SetVehicleHealthImpl SetVehicleHealth = null;
            [Native("GetVehicleHealth")] public static readonly GetVehicleHealthImpl GetVehicleHealth = null;

            [Native("AttachTrailerToVehicle")] public static readonly AttachTrailerToVehicleImpl AttachTrailerToVehicle
                =
                null;

            [Native("DetachTrailerFromVehicle")] public static readonly DetachTrailerFromVehicleImpl
                DetachTrailerFromVehicle = null;

            [Native("IsTrailerAttachedToVehicle")] public static readonly IsTrailerAttachedToVehicleImpl
                IsTrailerAttachedToVehicle = null;

            [Native("GetVehicleTrailer")] public static readonly GetVehicleTrailerImpl GetVehicleTrailer = null;

            [Native("SetVehicleNumberPlate")] public static readonly SetVehicleNumberPlateImpl SetVehicleNumberPlate =
                null;

            [Native("GetVehicleModel")] public static readonly GetVehicleModelImpl GetVehicleModel = null;

            [Native("GetVehicleComponentInSlot")] public static readonly GetVehicleComponentInSlotImpl
                GetVehicleComponentInSlot = null;

            [Native("GetVehicleComponentType")] public static readonly GetVehicleComponentTypeImpl
                GetVehicleComponentType =
                    null;

            [Native("RepairVehicle")] public static readonly RepairVehicleImpl RepairVehicle = null;
            [Native("GetVehicleVelocity")] public static readonly GetVehicleVelocityImpl GetVehicleVelocity = null;
            [Native("SetVehicleVelocity")] public static readonly SetVehicleVelocityImpl SetVehicleVelocity = null;

            [Native("SetVehicleAngularVelocity")] public static readonly SetVehicleAngularVelocityImpl
                SetVehicleAngularVelocity = null;

            [Native("GetVehicleDamageStatus")] public static readonly GetVehicleDamageStatusImpl GetVehicleDamageStatus
                =
                null;

            [Native("UpdateVehicleDamageStatus")] public static readonly UpdateVehicleDamageStatusImpl
                UpdateVehicleDamageStatus = null;

            [Native("SetVehicleVirtualWorld")] public static readonly SetVehicleVirtualWorldImpl SetVehicleVirtualWorld
                =
                null;

            [Native("GetVehicleVirtualWorld")] public static readonly GetVehicleVirtualWorldImpl GetVehicleVirtualWorld
                =
                null;

            [Native("GetVehicleModelInfo")] public static readonly GetVehicleModelInfoImpl GetVehicleModelInfo = null;

            [Native("AddStaticVehicle")] public static readonly AddStaticVehicleImpl AddStaticVehicle = null;
            [Native("AddStaticVehicleEx")] public static readonly AddStaticVehicleExImpl AddStaticVehicleEx = null;

            [Native("GetVehiclePoolSize")] public static readonly GetVehiclePoolSizeImpl GetVehiclePoolSize = null;
        }
    }
}