// SampSharp
// Copyright 2019 Tim Potze
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

using System.Diagnostics.CodeAnalysis;
using SampSharp.Core.Natives.NativeObjects;

#pragma warning disable 1591

namespace SampSharp.Entities.SAMP.NativeComponents
{
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public class NativeVehicle : BaseNativeComponent
    {
        /// <summary>
        /// Identifier indicating the handle is invalid.
        /// </summary>
        public const int InvalidId = 0xFFFF;

        [NativeMethod]
        public virtual bool IsValidVehicle()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual float GetVehicleDistanceFromPoint(float x, float y, float z)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool DestroyVehicle()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool IsVehicleStreamedIn(int forplayerid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool GetVehiclePos(out float x, out float y, out float z)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetVehiclePos(float x, float y, float z)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool GetVehicleZAngle(out float zAngle)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool GetVehicleRotationQuat(out float w, out float x, out float y, out float z)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetVehicleZAngle(float zAngle)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetVehicleParamsForPlayer(int playerid, bool objective, bool doorslocked)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetVehicleParamsEx(int engine, int lights, int alarm, int doors,
            int bonnet, int boot, int objective)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool GetVehicleParamsEx(out int engine, out int lights, out int alarm,
            out int doors, out int bonnet, out int boot, out int objective)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int GetVehicleParamsSirenState()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetVehicleParamsCarDoors(int driver, int passenger, int backleft,
            int backright)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool GetVehicleParamsCarDoors(out int driver, out int passenger,
            out int backleft, out int backright)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetVehicleParamsCarWindows(int driver, int passenger, int backleft,
            int backright)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool GetVehicleParamsCarWindows(out int driver, out int passenger,
            out int backleft, out int backright)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetVehicleToRespawn()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool LinkVehicleToInterior(int interiorid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool AddVehicleComponent(int componentid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool RemoveVehicleComponent(int componentid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool ChangeVehicleColor(int color1, int color2)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool ChangeVehiclePaintjob(int paintjobid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetVehicleHealth(float health)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool GetVehicleHealth(out float health)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool AttachTrailerToVehicle(int vehicleid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool DetachTrailerFromVehicle()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool IsTrailerAttachedToVehicle()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int GetVehicleTrailer()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetVehicleNumberPlate(string numberplate)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int GetVehicleModel()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int GetVehicleComponentInSlot(int slot)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool RepairVehicle()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool GetVehicleVelocity(out float x, out float y, out float z)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetVehicleVelocity(float x, float y, float z)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetVehicleAngularVelocity(float x, float y, float z)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool GetVehicleDamageStatus(out int panels, out int doors, out int lights,
            out int tires)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool UpdateVehicleDamageStatus(int panels, int doors, int lights, int tires)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetVehicleVirtualWorld(int worldid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int GetVehicleVirtualWorld()
        {
            throw new NativeNotImplementedException();
        }
    }
}