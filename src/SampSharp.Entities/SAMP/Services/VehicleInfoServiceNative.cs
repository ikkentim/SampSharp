// SampSharp
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

#pragma warning disable 1591

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Provides SA:MP natives for the <see cref="VehicleInfoService" />.
/// </summary>
public class VehicleInfoServiceNative
{
    [NativeMethod]
    public virtual int GetVehicleComponentType(int component)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetVehicleModelInfo(int model, int infoType, out float x, out float y, out float z)
    {
        throw new NativeNotImplementedException();
    }
}