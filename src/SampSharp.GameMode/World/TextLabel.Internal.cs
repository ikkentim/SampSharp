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

namespace SampSharp.GameMode.World
{
    public partial class TextLabel
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public class TextLabelInternal : NativeObjectSingleton<TextLabelInternal>
        {
            [NativeMethod]
            public virtual int Create3DTextLabel(string text, int color, float x, float y, float z, float drawDistance,
                int virtualWorld, bool testLOS)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int Delete3DTextLabel(int id)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int Update3DTextLabelText(int id, int color, string text)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int Attach3DTextLabelToPlayer(int id, int playerid, float offsetX, float offsetY,
                float offsetZ)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int Attach3DTextLabelToVehicle(int id, int vehicleid, float offsetX, float offsetY,
                float offsetZ)
            {
                throw new NativeNotImplementedException();
            }
        }
#pragma warning restore CS1591
    }
}