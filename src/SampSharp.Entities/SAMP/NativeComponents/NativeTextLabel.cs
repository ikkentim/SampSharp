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

using System.Diagnostics.CodeAnalysis;
using SampSharp.Core.Natives.NativeObjects;

#pragma warning disable 1591

namespace SampSharp.Entities.SAMP;

[SuppressMessage("ReSharper", "IdentifierTypo")]
public class NativeTextLabel : BaseNativeComponent
{
    public const int InvalidId = 0xFFFF;

    [NativeMethod]
    public virtual int Delete3DTextLabel()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int Update3DTextLabelText(int color, string text)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int Attach3DTextLabelToPlayer(int playerid, float offsetX, float offsetY, float offsetZ)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int Attach3DTextLabelToVehicle(int vehicleid, float offsetX, float offsetY, float offsetZ)
    {
        throw new NativeNotImplementedException();
    }
}