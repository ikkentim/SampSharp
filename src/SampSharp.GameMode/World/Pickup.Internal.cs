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

using SampSharp.GameMode.Natives;

namespace SampSharp.GameMode.World
{
    public partial class Pickup
    {
        private static class Internal
        {
            public delegate int AddStaticPickupImpl(int model, int type, float x, float y, float z, int virtualworld);

            public delegate int CreatePickupImpl(int model, int type, float x, float y, float z, int virtualworld);

            public delegate bool DestroyPickupImpl(int pickupid);

            [Native("AddStaticPickup")] public static readonly AddStaticPickupImpl AddStaticPickup = null;
            [Native("CreatePickup")] public static readonly CreatePickupImpl CreatePickup = null;
            [Native("DestroyPickup")] public static readonly DestroyPickupImpl DestroyPickup = null;
        }
    }
}