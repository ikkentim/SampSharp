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

using SampSharp.Core.Natives.NativeObjects;

#pragma warning disable 1591

namespace SampSharp.Entities.SAMP.NativeComponents
{
    public class NativeWorld : NativeComponent
    {
        [NativeMethod]
        public virtual int CreateActor(int modelId, float x, float y, float z, float rotation)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int CreateVehicle(int vehicleType, float x, float y, float z, float rotation, int color1,
            int color2, int respawnDelay, bool addSiren = false)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int AddStaticVehicleEx(int vehicleType, float x, float y, float z, float rotation, int color1,
            int color2, int respawnDelay, bool addSiren = false)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int AddStaticVehicle(int vehicleType, float x, float y, float z, float rotation, int color1,
            int color2)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int GangZoneCreate(float minX, float minY, float maxX, float maxY)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int CreatePickup(int model, int type, float x, float y, float z, int virtualWorld)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int CreateObject(int modelId, float x, float y, float z, float rX, float rY, float rZ,
            float drawDistance)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int CreatePlayerObject(int playerid, int modelid, float x, float y, float z, float rX, float rY,
            float rZ, float drawDistance)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int Create3DTextLabel(string text, int color, float x, float y, float z, float drawDistance,
            int virtualWorld, bool testLos)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int CreatePlayer3DTextLabel(int playerid, string text, int color, float x, float y, float z,
            float drawDistance, int attachedplayer, int attachedvehicle, bool testLOS)
        {
            throw new NativeNotImplementedException();
        }
    }
}