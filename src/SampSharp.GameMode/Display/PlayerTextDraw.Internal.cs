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
    public partial class PlayerTextDraw
    {
        private static class Internal
        {
            public delegate int CreatePlayerTextDrawImpl(int playerid, float x, float y, string text);

            public delegate bool PlayerTextDrawAlignmentImpl(int playerid, int text, int alignment);

            public delegate bool PlayerTextDrawBackgroundColorImpl(int playerid, int text, int color);

            public delegate bool PlayerTextDrawBoxColorImpl(int playerid, int text, int color);

            public delegate bool PlayerTextDrawColorImpl(int playerid, int text, int color);

            public delegate bool PlayerTextDrawDestroyImpl(int playerid, int text);

            public delegate bool PlayerTextDrawFontImpl(int playerid, int text, int font);

            public delegate bool PlayerTextDrawHideImpl(int playerid, int text);

            public delegate bool PlayerTextDrawLetterSizeImpl(int playerid, int text, float x, float y);

            public delegate bool PlayerTextDrawSetOutlineImpl(int playerid, int text, int size);

            public delegate bool PlayerTextDrawSetPreviewModelImpl(int playerid, int text, int modelindex);

            public delegate bool PlayerTextDrawSetPreviewRotImpl(int playerid, int text, float rotX, float rotY,
                float rotZ, float zoom);

            public delegate bool PlayerTextDrawSetPreviewVehColImpl(int playerid, int text, int color1, int color2);

            public delegate bool PlayerTextDrawSetProportionalImpl(int playerid, int text, bool set);

            public delegate bool PlayerTextDrawSetSelectableImpl(int playerid, int text, bool set);

            public delegate bool PlayerTextDrawSetShadowImpl(int playerid, int text, int size);

            public delegate bool PlayerTextDrawSetStringImpl(int playerid, int text, string str);

            public delegate bool PlayerTextDrawShowImpl(int playerid, int text);

            public delegate bool PlayerTextDrawTextSizeImpl(int playerid, int text, float x, float y);

            public delegate bool PlayerTextDrawUseBoxImpl(int playerid, int text, bool use);

            [Native("CreatePlayerTextDraw")] public static readonly CreatePlayerTextDrawImpl CreatePlayerTextDraw =
                null;

            [Native("PlayerTextDrawDestroy")] public static readonly PlayerTextDrawDestroyImpl PlayerTextDrawDestroy =
                null;

            [Native("PlayerTextDrawLetterSize")] public static readonly PlayerTextDrawLetterSizeImpl
                PlayerTextDrawLetterSize = null;

            [Native("PlayerTextDrawTextSize")] public static readonly PlayerTextDrawTextSizeImpl PlayerTextDrawTextSize
                =
                null;

            [Native("PlayerTextDrawAlignment")] public static readonly PlayerTextDrawAlignmentImpl
                PlayerTextDrawAlignment
                    =
                    null;

            [Native("PlayerTextDrawColor")] public static readonly PlayerTextDrawColorImpl PlayerTextDrawColor = null;

            [Native("PlayerTextDrawUseBox")] public static readonly PlayerTextDrawUseBoxImpl PlayerTextDrawUseBox =
                null;

            [Native("PlayerTextDrawBoxColor")] public static readonly PlayerTextDrawBoxColorImpl PlayerTextDrawBoxColor
                =
                null;

            [Native("PlayerTextDrawSetShadow")] public static readonly PlayerTextDrawSetShadowImpl
                PlayerTextDrawSetShadow
                    =
                    null;

            [Native("PlayerTextDrawSetOutline")] public static readonly PlayerTextDrawSetOutlineImpl
                PlayerTextDrawSetOutline = null;

            [Native("PlayerTextDrawBackgroundColor")] public static readonly PlayerTextDrawBackgroundColorImpl
                PlayerTextDrawBackgroundColor = null;

            [Native("PlayerTextDrawFont")] public static readonly PlayerTextDrawFontImpl PlayerTextDrawFont = null;

            [Native("PlayerTextDrawSetProportional")] public static readonly PlayerTextDrawSetProportionalImpl
                PlayerTextDrawSetProportional = null;

            [Native("PlayerTextDrawSetSelectable")] public static readonly PlayerTextDrawSetSelectableImpl
                PlayerTextDrawSetSelectable = null;

            [Native("PlayerTextDrawShow")] public static readonly PlayerTextDrawShowImpl PlayerTextDrawShow = null;
            [Native("PlayerTextDrawHide")] public static readonly PlayerTextDrawHideImpl PlayerTextDrawHide = null;

            [Native("PlayerTextDrawSetString")] public static readonly PlayerTextDrawSetStringImpl
                PlayerTextDrawSetString
                    =
                    null;

            [Native("PlayerTextDrawSetPreviewModel")] public static readonly PlayerTextDrawSetPreviewModelImpl
                PlayerTextDrawSetPreviewModel = null;

            [Native("PlayerTextDrawSetPreviewRot")] public static readonly PlayerTextDrawSetPreviewRotImpl
                PlayerTextDrawSetPreviewRot = null;

            [Native("PlayerTextDrawSetPreviewVehCol")] public static readonly PlayerTextDrawSetPreviewVehColImpl
                PlayerTextDrawSetPreviewVehCol = null;
        }
    }
}