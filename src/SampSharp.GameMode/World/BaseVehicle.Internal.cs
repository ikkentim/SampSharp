// SampSharp
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
using SampSharp.Core.Natives.NativeObjects;

namespace SampSharp.GameMode.World
{
    public partial class BaseVehicle
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public class VehicleInternal : NativeObjectSingleton<VehicleInternal>
        {
            [NativeMethod]
            public virtual bool IsValidVehicle(int vehicleid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual float GetVehicleDistanceFromPoint(int vehicleid, float x, float y, float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool DestroyVehicle(int vehicleid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsVehicleStreamedIn(int vehicleid, int forplayerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetVehiclePos(int vehicleid, out float x, out float y, out float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetVehiclePos(int vehicleid, float x, float y, float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetVehicleZAngle(int vehicleid, out float zAngle)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetVehicleRotationQuat(int vehicleid, out float w, out float x, out float y, out float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetVehicleZAngle(int vehicleid, float zAngle)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetVehicleParamsForPlayer(int vehicleid, int playerid, bool objective, bool doorslocked)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetVehicleParamsEx(int vehicleid, int engine, int lights, int alarm, int doors,
                int bonnet, int boot, int objective)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetVehicleParamsEx(int vehicleid, out int engine, out int lights, out int alarm,
                out int doors, out int bonnet, out int boot, out int objective)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetVehicleParamsSirenState(int vehicleid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetVehicleParamsCarDoors(int vehicleid, int driver, int passenger, int backleft,
                int backright)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetVehicleParamsCarDoors(int vehicleid, out int driver, out int passenger,
                out int backleft, out int backright)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetVehicleParamsCarWindows(int vehicleid, int driver, int passenger, int backleft,
                int backright)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetVehicleParamsCarWindows(int vehicleid, out int driver, out int passenger,
                out int backleft, out int backright)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetVehicleToRespawn(int vehicleid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool LinkVehicleToInterior(int vehicleid, int interiorid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool AddVehicleComponent(int vehicleid, int componentid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool RemoveVehicleComponent(int vehicleid, int componentid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool ChangeVehicleColor(int vehicleid, int color1, int color2)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool ChangeVehiclePaintjob(int vehicleid, int paintjobid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetVehicleHealth(int vehicleid, float health)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetVehicleHealth(int vehicleid, out float health)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool AttachTrailerToVehicle(int trailerid, int vehicleid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool DetachTrailerFromVehicle(int vehicleid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsTrailerAttachedToVehicle(int vehicleid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetVehicleTrailer(int vehicleid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetVehicleNumberPlate(int vehicleid, string numberplate)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetVehicleModel(int vehicleid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetVehicleComponentInSlot(int vehicleid, int slot)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetVehicleComponentType(int component)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool RepairVehicle(int vehicleid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetVehicleVelocity(int vehicleid, out float x, out float y, out float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetVehicleVelocity(int vehicleid, float x, float y, float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetVehicleAngularVelocity(int vehicleid, float x, float y, float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetVehicleDamageStatus(int vehicleid, out int panels, out int doors, out int lights,
                out int tires)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool UpdateVehicleDamageStatus(int vehicleid, int panels, int doors, int lights, int tires)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetVehicleVirtualWorld(int vehicleid, int worldid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetVehicleVirtualWorld(int vehicleid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetVehicleModelInfo(int model, int infotype, out float x, out float y, out float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetVehiclePoolSize()
            {
                throw new NativeNotImplementedException();
            }
        }
#pragma warning restore CS1591
    }
}