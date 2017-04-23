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

namespace SampSharp.GameMode.World
{
    public partial class Actor
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public class ActorInternal : NativeObjectSingleton<ActorInternal>
        {
            [NativeMethod]
            public virtual int CreateActor(int modelid, float x, float y, float z, float rotattion)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool DestroyActor(int actorid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsActorStreamedIn(int actorid, int forplayerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetActorVirtualWorld(int actorid, int vworld)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetActorVirtualWorld(int actorid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool ClearActorAnimations(int actorid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetActorPos(int actorid, float x, float y, float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetActorPos(int actorid, out float x, out float y, out float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetActorFacingAngle(int actorid, float angle)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetActorFacingAngle(int actorid, out float angle)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetActorHealth(int actorid, float health)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetActorHealth(int actorid, out float health)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetActorInvulnerable(int actorid, bool invulnerable = true)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsActorInvulnerable(int actorid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsValidActor(int actorid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool ApplyActorAnimation(int actorid, string animlib, string animname, float fDelta,
                bool loop, bool lockx, bool locky, bool freeze, int time)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetActorPoolSize()
            {
                throw new NativeNotImplementedException();
            }
        }
#pragma warning restore CS1591
    }
}