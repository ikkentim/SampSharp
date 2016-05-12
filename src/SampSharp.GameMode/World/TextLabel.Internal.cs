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

namespace SampSharp.GameMode.World
{
    public partial class TextLabel
    {
        private static class Internal
        {
            public delegate bool Attach3DTextLabelToPlayerImpl(
                int id, int playerid, float offsetX, float offsetY, float offsetZ);

            public delegate bool Attach3DTextLabelToVehicleImpl(
                int id, int vehicleid, float offsetX, float offsetY, float offsetZ);

            public delegate int Create3DTextLabelImpl(
                string text, int color, float x, float y, float z, float drawDistance, int virtualWorld, bool testLOS);

            public delegate bool Delete3DTextLabelImpl(int id);

            public delegate bool Update3DTextLabelTextImpl(int id, int color, string text);

            [Native("Create3DTextLabel")] public static readonly Create3DTextLabelImpl Create3DTextLabel = null;
            [Native("Delete3DTextLabel")] public static readonly Delete3DTextLabelImpl Delete3DTextLabel = null;

            [Native("Attach3DTextLabelToPlayer")] public static readonly Attach3DTextLabelToPlayerImpl
                Attach3DTextLabelToPlayer = null;

            [Native("Attach3DTextLabelToVehicle")] public static readonly Attach3DTextLabelToVehicleImpl
                Attach3DTextLabelToVehicle = null;

            [Native("Update3DTextLabelText")] public static readonly Update3DTextLabelTextImpl Update3DTextLabelText =
                null;
        }
    }
}