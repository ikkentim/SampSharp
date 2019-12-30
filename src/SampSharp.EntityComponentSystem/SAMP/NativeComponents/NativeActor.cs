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

using System.Diagnostics.CodeAnalysis;
using SampSharp.Core.Natives.NativeObjects;

#pragma warning disable 1591

namespace SampSharp.EntityComponentSystem.SAMP.NativeComponents
{
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public class NativeActor : BaseNativeComponent
    {
        /// <summary>
        /// Identifier indicating the handle is invalid.
        /// </summary>
        public const int InvalidId = 0xFFFF;

        [NativeMethod]
        public virtual bool DestroyActor()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool IsActorStreamedIn(int forplayerid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetActorVirtualWorld(int vworld)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int GetActorVirtualWorld()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool ClearActorAnimations()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetActorPos(float x, float y, float z)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool GetActorPos(out float x, out float y, out float z)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetActorFacingAngle(float angle)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool GetActorFacingAngle(out float angle)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetActorHealth(float health)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool GetActorHealth(out float health)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetActorInvulnerable(bool invulnerable = true)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool IsActorInvulnerable()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool IsValidActor()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool ApplyActorAnimation(string animlib, string animname, float fDelta,
            bool loop, bool lockx, bool locky, bool freeze, int time)
        {
            throw new NativeNotImplementedException();
        }
    }
}