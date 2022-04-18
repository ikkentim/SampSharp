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

using System.Diagnostics.CodeAnalysis;
using SampSharp.Core.Natives.NativeObjects;

#pragma warning disable 1591

namespace SampSharp.Entities.SAMP;

[SuppressMessage("ReSharper", "IdentifierTypo")]
public class NativeGangZone : BaseNativeComponent
{
    public const int InvalidId = -1;

    [NativeMethod]
    public virtual bool GangZoneDestroy()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod(IdentifiersIndex = 1)]
    public virtual bool GangZoneShowForPlayer(int playerid, int color)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GangZoneShowForAll(int color)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod(IdentifiersIndex = 1)]
    public virtual bool GangZoneHideForPlayer(int playerid)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GangZoneHideForAll()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod(IdentifiersIndex = 1)]
    public virtual bool GangZoneFlashForPlayer(int playerid, int flashcolor)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GangZoneFlashForAll(int flashcolor)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod(IdentifiersIndex = 1)]
    public virtual bool GangZoneStopFlashForPlayer(int playerid)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GangZoneStopFlashForAll()
    {
        throw new NativeNotImplementedException();
    }
}