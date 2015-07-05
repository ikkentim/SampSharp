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

namespace SampSharp.GameMode.Natives
{
    /// <summary>
    ///     Contains all native methods.
    /// </summary>
    public static partial class Native
    {
        public delegate bool ApplyActorAnimationImpl(int actorid, string animlib, string animname, float fDelta,
            bool loop, bool lockx, bool locky, bool freeze, int time);

        public delegate bool ClearActorAnimationsImpl(int actorid);

        public delegate int CreateActorImpl(int modelid, float x, float y, float z, float rotattion);

        public delegate bool DestroyActorImpl(int actorid);

        public delegate bool GetActorFacingAngleImpl(int actorid, out float angle);

        public delegate bool GetActorHealthImpl(int actorid, out float health);

        public delegate bool GetActorPosImpl(int actorid, out float x, out float y, out float z);

        public delegate int GetActorVirtualWorldImpl(int actorid);

        public delegate bool IsActorInvulnerableImpl(int actorid);

        public delegate bool IsActorStreamedInImpl(int actorid, int forplayerid);

        public delegate bool IsValidActorImpl(int actorid);

        public delegate bool SetActorFacingAngleImpl(int actorid, float angle);

        public delegate bool SetActorHealthImpl(int actorid, float health);

        public delegate bool SetActorInvulnerableImpl(int actorid, bool invulnerable = true);

        public delegate bool SetActorPosImpl(int actorid, float x, float y, float z);

        public delegate bool SetActorVirtualWorldImpl(int actorid, int vworld);

        [Native("CreateActor")] public static readonly CreateActorImpl CreateActor = null;
        [Native("DestroyActor")] public static readonly DestroyActorImpl DestroyActor = null;
        [Native("IsActorStreamedIn")] public static readonly IsActorStreamedInImpl IsActorStreamedIn = null;
        [Native("SetActorVirtualWorld")] public static readonly SetActorVirtualWorldImpl SetActorVirtualWorld = null;
        [Native("GetActorVirtualWorld")] public static readonly GetActorVirtualWorldImpl GetActorVirtualWorld = null;
        [Native("ClearActorAnimations")] public static readonly ClearActorAnimationsImpl ClearActorAnimations = null;
        [Native("SetActorPos")] public static readonly SetActorPosImpl SetActorPos = null;
        [Native("GetActorPos")] public static readonly GetActorPosImpl GetActorPos = null;
        [Native("SetActorFacingAngle")] public static readonly SetActorFacingAngleImpl SetActorFacingAngle = null;
        [Native("GetActorFacingAngle")] public static readonly GetActorFacingAngleImpl GetActorFacingAngle = null;
        [Native("SetActorHealth")] public static readonly SetActorHealthImpl SetActorHealth = null;
        [Native("GetActorHealth")] public static readonly GetActorHealthImpl GetActorHealth = null;
        [Native("SetActorInvulnerable")] public static readonly SetActorInvulnerableImpl SetActorInvulnerable = null;
        [Native("IsActorInvulnerable")] public static readonly IsActorInvulnerableImpl IsActorInvulnerable = null;
        [Native("IsValidActor")] public static readonly IsValidActorImpl IsValidActor = null;
        [Native("ApplyActorAnimation")] public static readonly ApplyActorAnimationImpl ApplyActorAnimation = null;
    }
}