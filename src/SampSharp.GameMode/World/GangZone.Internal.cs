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
    public partial class GangZone
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public class GangZoneInternal : NativeObjectSingleton<GangZoneInternal>
        {
            [NativeMethod]
            public virtual int GangZoneCreate(float minx, float miny, float maxx, float maxy)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GangZoneDestroy(int zone)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GangZoneShowForPlayer(int playerid, int zone, int color)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GangZoneShowForAll(int zone, int color)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GangZoneHideForPlayer(int playerid, int zone)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GangZoneHideForAll(int zone)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GangZoneFlashForPlayer(int playerid, int zone, int flashcolor)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GangZoneFlashForAll(int zone, int flashcolor)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GangZoneStopFlashForPlayer(int playerid, int zone)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GangZoneStopFlashForAll(int zone)
            {
                throw new NativeNotImplementedException();
            }
        }
    }
#pragma warning restore CS1591
}