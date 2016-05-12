// SampSharp
// Copyright 2016 Tim Potze
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

using SampSharp.GameMode.API;

namespace SampSharp.GameMode.Display
{
    public partial class Menu
    {
        private static class Internal
        {
            public delegate int AddMenuItemImpl(int menuid, int column, string menutext);

            public delegate int CreateMenuImpl(
                string title, int columns, float x, float y, float col1Width, float col2Width
                );

            public delegate bool DestroyMenuImpl(int menuid);

            //        public delegate bool DisableMenuImpl(int menuid);

            public delegate bool DisableMenuRowImpl(int menuid, int row);

            //        public delegate int GetPlayerMenuImpl(int playerid);

            public delegate bool HideMenuForPlayerImpl(int menuid, int playerid);

            //        public delegate bool IsValidMenuImpl(int menuid);

            public delegate bool SetMenuColumnHeaderImpl(int menuid, int column, string columnheader);

            public delegate bool ShowMenuForPlayerImpl(int menuid, int playerid);

            [Native("CreateMenu")] public static readonly CreateMenuImpl CreateMenu = null;
            [Native("DestroyMenu")] public static readonly DestroyMenuImpl DestroyMenu = null;
            [Native("AddMenuItem")] public static readonly AddMenuItemImpl AddMenuItem = null;
            [Native("SetMenuColumnHeader")] public static readonly SetMenuColumnHeaderImpl SetMenuColumnHeader = null;
            [Native("ShowMenuForPlayer")] public static readonly ShowMenuForPlayerImpl ShowMenuForPlayer = null;
            [Native("HideMenuForPlayer")] public static readonly HideMenuForPlayerImpl HideMenuForPlayer = null;
            //        [Native("IsValidMenu")]
            //        public static readonly IsValidMenuImpl IsValidMenu = null;
            //        [Native("DisableMenu")]
            //        public static readonly DisableMenuImpl DisableMenu = null;
            [Native("DisableMenuRow")] public static readonly DisableMenuRowImpl DisableMenuRow = null;
            //        public static readonly GetPlayerMenuImpl GetPlayerMenu = null;

            //        [Native("GetPlayerMenu")]
        }
    }
}