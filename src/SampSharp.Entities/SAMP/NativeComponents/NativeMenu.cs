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
public class NativeMenu : BaseNativeComponent
{
    public const int InvalidId = 255;

    [NativeMethod]
    public virtual bool DestroyMenu()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int AddMenuItem(int column, string menutext)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetMenuColumnHeader(int column, string columnheader)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool ShowMenuForPlayer(int playerid)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool HideMenuForPlayer(int playerid)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool IsValidMenu()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool DisableMenu()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool DisableMenuRow(int row)
    {
        throw new NativeNotImplementedException();
    }
}