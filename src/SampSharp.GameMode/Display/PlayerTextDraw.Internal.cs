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
    public partial class PlayerTextDraw
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public class PlayerTextDrawInternal : NativeObjectSingleton<PlayerTextDrawInternal>
        {
            [NativeMethod]
            public virtual int CreatePlayerTextDraw(int playerid, float x, float y, string text)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawDestroy(int playerid, int text)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawLetterSize(int playerid, int text, float x, float y)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawTextSize(int playerid, int text, float x, float y)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawAlignment(int playerid, int text, int alignment)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawColor(int playerid, int text, int color)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawUseBox(int playerid, int text, bool use)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawBoxColor(int playerid, int text, int color)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawSetShadow(int playerid, int text, int size)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawSetOutline(int playerid, int text, int size)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawBackgroundColor(int playerid, int text, int color)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawFont(int playerid, int text, int font)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawSetProportional(int playerid, int text, bool set)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawSetSelectable(int playerid, int text, bool set)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawShow(int playerid, int text)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawHide(int playerid, int text)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawSetString(int playerid, int text, string str)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawSetPreviewModel(int playerid, int text, int modelindex)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawSetPreviewRot(int playerid, int text, float rotX, float rotY, float rotZ,
                float zoom)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerTextDrawSetPreviewVehCol(int playerid, int text, int color1, int color2)
            {
                throw new NativeNotImplementedException();
            }
        }
#pragma warning restore CS1591
    }
}