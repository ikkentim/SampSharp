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

public partial class Pickup
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class PickupInternal : NativeObjectSingleton<PickupInternal>
    {
        [NativeMethod]
        public virtual int AddStaticPickup(int model, int type, float x, float y, float z, int virtualworld)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int CreatePickup(int model, int type, float x, float y, float z, int virtualworld)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool DestroyPickup(int pickupid)
        {
            throw new NativeNotImplementedException();
        }
    }
#pragma warning restore
}