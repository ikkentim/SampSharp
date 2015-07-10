using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode.Natives;

namespace SampSharp.GameMode.Display
{
    public partial class TextDraw
    {
        private static class Internal
        {
            [Native("TextDrawCreate")]
            public static readonly TextDrawCreateImpl TextDrawCreate = null;
            [Native("TextDrawDestroy")]
            public static readonly TextDrawDestroyImpl TextDrawDestroy = null;
            [Native("TextDrawLetterSize")]
            public static readonly TextDrawLetterSizeImpl TextDrawLetterSize = null;
            [Native("TextDrawTextSize")]
            public static readonly TextDrawTextSizeImpl TextDrawTextSize = null;
            [Native("TextDrawAlignment")]
            public static readonly TextDrawAlignmentImpl TextDrawAlignment = null;
            [Native("TextDrawColor")]
            public static readonly TextDrawColorImpl TextDrawColor = null;
            [Native("TextDrawUseBox")]
            public static readonly TextDrawUseBoxImpl TextDrawUseBox = null;
            [Native("TextDrawBoxColor")]
            public static readonly TextDrawBoxColorImpl TextDrawBoxColor = null;
            [Native("TextDrawSetShadow")]
            public static readonly TextDrawSetShadowImpl TextDrawSetShadow = null;
            [Native("TextDrawSetOutline")]
            public static readonly TextDrawSetOutlineImpl TextDrawSetOutline = null;

            [Native("TextDrawBackgroundColor")]
            public static readonly TextDrawBackgroundColorImpl TextDrawBackgroundColor
                =
                null;

            [Native("TextDrawFont")]
            public static readonly TextDrawFontImpl TextDrawFont = null;

            [Native("TextDrawSetProportional")]
            public static readonly TextDrawSetProportionalImpl TextDrawSetProportional
                =
                null;

            [Native("TextDrawSetSelectable")]
            public static readonly TextDrawSetSelectableImpl TextDrawSetSelectable = null;
            [Native("TextDrawShowForPlayer")]
            public static readonly TextDrawShowForPlayerImpl TextDrawShowForPlayer = null;
            [Native("TextDrawHideForPlayer")]
            public static readonly TextDrawHideForPlayerImpl TextDrawHideForPlayer = null;
            [Native("TextDrawShowForAll")]
            public static readonly TextDrawShowForAllImpl TextDrawShowForAll = null;
            [Native("TextDrawHideForAll")]
            public static readonly TextDrawHideForAllImpl TextDrawHideForAll = null;
            [Native("TextDrawSetString")]
            public static readonly TextDrawSetStringImpl TextDrawSetString = null;

            [Native("TextDrawSetPreviewModel")]
            public static readonly TextDrawSetPreviewModelImpl TextDrawSetPreviewModel
                =
                null;

            [Native("TextDrawSetPreviewRot")]
            public static readonly TextDrawSetPreviewRotImpl TextDrawSetPreviewRot = null;

            [Native("TextDrawSetPreviewVehCol")]
            public static readonly TextDrawSetPreviewVehColImpl
                TextDrawSetPreviewVehCol = null;

            public delegate bool TextDrawAlignmentImpl(int text, int alignment);

            public delegate bool TextDrawBackgroundColorImpl(int text, int color);

            public delegate bool TextDrawBoxColorImpl(int text, int color);

            public delegate bool TextDrawColorImpl(int text, int color);

            public delegate int TextDrawCreateImpl(float x, float y, string text);

            public delegate bool TextDrawDestroyImpl(int text);

            public delegate bool TextDrawFontImpl(int text, int font);

            public delegate bool TextDrawHideForAllImpl(int text);

            public delegate bool TextDrawHideForPlayerImpl(int playerid, int text);

            public delegate bool TextDrawLetterSizeImpl(int text, float x, float y);

            public delegate bool TextDrawSetOutlineImpl(int text, int size);

            public delegate bool TextDrawSetPreviewModelImpl(int text, int modelindex);

            public delegate bool TextDrawSetPreviewRotImpl(int text, float rotX, float rotY, float rotZ, float zoom);

            public delegate bool TextDrawSetPreviewVehColImpl(int text, int color1, int color2);

            public delegate bool TextDrawSetProportionalImpl(int text, bool set);

            public delegate bool TextDrawSetSelectableImpl(int text, bool set);

            public delegate bool TextDrawSetShadowImpl(int text, int size);

            public delegate bool TextDrawSetStringImpl(int text, string str);

            public delegate bool TextDrawShowForAllImpl(int text);

            public delegate bool TextDrawShowForPlayerImpl(int playerid, int text);

            public delegate bool TextDrawTextSizeImpl(int text, float x, float y);

            public delegate bool TextDrawUseBoxImpl(int text, bool use);


        }
    }
}
