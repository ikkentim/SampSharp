// SampSharp
// Copyright 2015 Tim Potze
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

using SampSharp.GameMode.Natives;

namespace SampSharp.GameMode.World
{
    public partial class PlayerTextLabel
    {
        private static class Internal
        {
            public delegate int CreatePlayer3DTextLabelImpl(
                int playerid, string text, int color, float x, float y, float z, float drawDistance, int attachedplayer,
                int attachedvehicle, bool testLOS);

            public delegate bool DeletePlayer3DTextLabelImpl(int playerid, int id);

            public delegate bool UpdatePlayer3DTextLabelTextImpl(int playerid, int id, int color, string text);

            [Native("CreatePlayer3DTextLabel")] public static readonly CreatePlayer3DTextLabelImpl
                CreatePlayer3DTextLabel =
                    null;

            [Native("DeletePlayer3DTextLabel")] public static readonly DeletePlayer3DTextLabelImpl
                DeletePlayer3DTextLabel =
                    null;

            [Native("UpdatePlayer3DTextLabelText")] public static readonly UpdatePlayer3DTextLabelTextImpl
                UpdatePlayer3DTextLabelText = null;
        }
    }
}