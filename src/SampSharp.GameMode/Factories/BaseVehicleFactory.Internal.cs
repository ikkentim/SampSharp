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

namespace SampSharp.GameMode.Factories
{
    public partial class BaseVehicleFactory
    {
        private static class Internal
        {
            public delegate int AddStaticVehicleExImpl(
                int modelid, float spawnX, float spawnY, float spawnZ, float zAngle, int color1, int color2,
                int respawnDelay, bool addsiren = false);

            public delegate int AddStaticVehicleImpl(
                int modelid, float spawnX, float spawnY, float spawnZ, float zAngle, int color1, int color2);

            public delegate int CreateVehicleImpl(
                int vehicletype, float x, float y, float z, float rotation, int color1,
                int color2, int respawnDelay, bool addsiren = false);

            [Native("CreateVehicle")] public static readonly CreateVehicleImpl CreateVehicle = null;

            [Native("AddStaticVehicle")] public static readonly AddStaticVehicleImpl AddStaticVehicle = null;
            [Native("AddStaticVehicleEx")] public static readonly AddStaticVehicleExImpl AddStaticVehicleEx = null;
        }
    }
}