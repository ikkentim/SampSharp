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

namespace SampSharp.GameMode.Display
{
    public partial class Menu
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public class MenuInternal : NativeObjectSingleton<MenuInternal>
        {
            [NativeMethod]
            public virtual int CreateMenu(string title, int columns, float x, float y, float col1Width, float col2Width)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool DestroyMenu(int menuid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int AddMenuItem(int menuid, int column, string menutext)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetMenuColumnHeader(int menuid, int column, string columnheader)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool ShowMenuForPlayer(int menuid, int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool HideMenuForPlayer(int menuid, int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsValidMenu(int menuid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool DisableMenu(int menuid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool DisableMenuRow(int menuid, int row)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerMenu(int playerid)
            {
                throw new NativeNotImplementedException();
            }
        }
#pragma warning restore CS1591
    }
}