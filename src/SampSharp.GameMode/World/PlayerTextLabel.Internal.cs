using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode.Natives;

namespace SampSharp.GameMode.World
{
    public partial class PlayerTextLabel
    {
        private static class Internal
        {
            public delegate bool UpdatePlayer3DTextLabelTextImpl(int playerid, int id, int color, string text);

            public delegate int CreatePlayer3DTextLabelImpl(
                int playerid, string text, int color, float x, float y, float z, float drawDistance, int attachedplayer,
                int attachedvehicle, bool testLOS);

            public delegate bool DeletePlayer3DTextLabelImpl(int playerid, int id);

            [Native("CreatePlayer3DTextLabel")]
            public static readonly CreatePlayer3DTextLabelImpl CreatePlayer3DTextLabel =
                null;

            [Native("DeletePlayer3DTextLabel")]
            public static readonly DeletePlayer3DTextLabelImpl DeletePlayer3DTextLabel =
                null;

            [Native("UpdatePlayer3DTextLabelText")]
            public static readonly UpdatePlayer3DTextLabelTextImpl
                UpdatePlayer3DTextLabelText = null;
        }
    }
}
