﻿// SampSharp
// Copyright 2022 Tim Potze
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

namespace SampSharp.GameMode.World;

public readonly partial struct VehicleModelInfo
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class VehicleModelInfoInternal : NativeObjectSingleton<VehicleModelInfoInternal>
    {
        [NativeMethod]
        public virtual bool GetVehicleModelInfo(int model, int infotype, out float x, out float y, out float z)
        {
            throw new NativeNotImplementedException();
        }
    }
#pragma warning restore CS1591
}