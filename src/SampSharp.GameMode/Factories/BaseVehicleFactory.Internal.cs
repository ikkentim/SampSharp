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

namespace SampSharp.GameMode.Factories
{
    public partial class BaseVehicleFactory
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public class BaseVehicleFactoryInternal : NativeObjectSingleton<BaseVehicleFactoryInternal>
        {
            [NativeMethod]
            public virtual int CreateVehicle(int vehicletype, float x, float y, float z, float rotation, int color1,
                int color2, int respawnDelay, bool addsiren = false)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int AddStaticVehicle(int modelid, float spawnX, float spawnY, float spawnZ, float zAngle,
                int color1, int color2)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int AddStaticVehicleEx(int modelid, float spawnX, float spawnY, float spawnZ, float zAngle,
                int color1, int color2, int respawnDelay, bool addsiren = false)
            {
                throw new NativeNotImplementedException();
            }
        }
#pragma warning restore CS1591
    }
}