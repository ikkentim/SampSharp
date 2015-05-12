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

using System.Runtime.CompilerServices;

namespace SampSharp.GameMode.Natives
{
    /// <summary>
    ///     Contains all native methods.
    /// </summary>
    public static partial class Native
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsValidVehicle(int vehicleid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern float GetVehicleDistanceFromPoint(int vehicleid, float x, float y, float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreateVehicle(int vehicletype, float x, float y, float z, float rotation, int color1,
            int color2, int respawnDelay, bool addsiren = false);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DestroyVehicle(int vehicleid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsVehicleStreamedIn(int vehicleid, int forplayerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetVehiclePos(int vehicleid, out float x, out float y, out float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehiclePos(int vehicleid, float x, float y, float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetVehicleZAngle(int vehicleid, out float zAngle);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetVehicleRotationQuat(int vehicleid, out float w, out float x, out float y,
            out float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleZAngle(int vehicleid, float zAngle);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleParamsForPlayer(int vehicleid, int playerid, bool objective,
            bool doorslocked);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ManualVehicleEngineAndLights();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleParamsEx(int vehicleid, int engine, int lights, int alarm, int doors,
            int bonnet, int boot, int objective);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetVehicleParamsEx(int vehicleid, out int engine, out int lights, out int alarm,
            out int doors, out int bonnet, out int boot, out int objective);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetVehicleParamsSirenState(int vehicleid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleParamsCarDoors(int vehicleid, int driver, int passenger, int backleft,
            int backright);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetVehicleParamsCarDoors(int vehicleid, out int driver, out int passenger,
            out int backleft, out int backright);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleParamsCarWindows(int vehicleid, int driver, int passenger, int backleft,
            int backright);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetVehicleParamsCarWindows(int vehicleid, out int driver, out int passenger,
            out int backleft, out int backright);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleToRespawn(int vehicleid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool LinkVehicleToInterior(int vehicleid, int interiorid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AddVehicleComponent(int vehicleid, int componentid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RemoveVehicleComponent(int vehicleid, int componentid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ChangeVehicleColor(int vehicleid, int color1, int color2);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ChangeVehiclePaintjob(int vehicleid, int paintjobid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleHealth(int vehicleid, float health);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetVehicleHealth(int vehicleid, out float health);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AttachTrailerToVehicle(int trailerid, int vehicleid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DetachTrailerFromVehicle(int vehicleid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsTrailerAttachedToVehicle(int vehicleid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetVehicleTrailer(int vehicleid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleNumberPlate(int vehicleid, string numberplate);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetVehicleModel(int vehicleid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetVehicleComponentInSlot(int vehicleid, int slot);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetVehicleComponentType(int component);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RepairVehicle(int vehicleid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetVehicleVelocity(int vehicleid, out float x, out float y, out float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleVelocity(int vehicleid, float x, float y, float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleAngularVelocity(int vehicleid, float x, float y, float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetVehicleDamageStatus(int vehicleid, out int panels, out int doors, out int lights,
            out int tires);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool UpdateVehicleDamageStatus(int vehicleid, int panels, int doors, int lights, int tires);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleVirtualWorld(int vehicleid, int worldid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetVehicleVirtualWorld(int vehicleid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetVehicleModelInfo(int model, int infotype, out float x, out float y, out float z);
    }
}
