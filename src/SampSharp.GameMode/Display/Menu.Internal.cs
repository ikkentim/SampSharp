using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode.Natives;

namespace SampSharp.GameMode.Display
{
    public partial class Menu
    {

        private static class Internal
        {
            [Native("CreateMenu")]
            public static readonly CreateMenuImpl CreateMenu = null;
            [Native("DestroyMenu")]
            public static readonly DestroyMenuImpl DestroyMenu = null;
            [Native("AddMenuItem")]
            public static readonly AddMenuItemImpl AddMenuItem = null;
            [Native("SetMenuColumnHeader")]
            public static readonly SetMenuColumnHeaderImpl SetMenuColumnHeader = null;
            [Native("ShowMenuForPlayer")]
            public static readonly ShowMenuForPlayerImpl ShowMenuForPlayer = null;
            [Native("HideMenuForPlayer")]
            public static readonly HideMenuForPlayerImpl HideMenuForPlayer = null;
            //        [Native("IsValidMenu")]
            //        public static readonly IsValidMenuImpl IsValidMenu = null;
            //        [Native("DisableMenu")]
            //        public static readonly DisableMenuImpl DisableMenu = null;
            [Native("DisableMenuRow")]
            public static readonly DisableMenuRowImpl DisableMenuRow = null;

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

            //        [Native("GetPlayerMenu")]
            //        public static readonly GetPlayerMenuImpl GetPlayerMenu = null;
        }

    }
}
