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
        public static extern int CreateActor(int modelid, float x, float y, float z, float rotattion);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DestroyActor(int actorid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsActorStreamedIn(int actorid, int forplayerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetActorVirtualWorld(int actorid, int vworld);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetActorVirtualWorld(int actorid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ApplyActorAnimation(int actorid, string animlib, string animname, float fDelta,
            bool loop, bool lockx, bool locky, bool freeze, int time);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ClearActorAnimations(int actorid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetActorPos(int actorid, float x, float y, float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetActorPos(int actorid, out float x, out float y, out float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetActorFacingAngle(int actorid, float angle);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetActorFacingAngle(int actorid, out float angle);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetActorHealth(int actorid, float health);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetActorHealth(int actorid, out float health);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetActorInvulnerable(int actorid, bool invulnerable = true);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsActorInvulnerable(int actorid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsValidActor(int actorid);
    }
}