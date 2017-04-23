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
    public partial class TextDraw
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public class TextDrawInternal : NativeObjectSingleton<TextDrawInternal>
        {
            [NativeMethod]
            public virtual int TextDrawCreate(float x, float y, string text)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawDestroy(int text)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawLetterSize(int text, float x, float y)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawTextSize(int text, float x, float y)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawAlignment(int text, int alignment)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawColor(int text, int color)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawUseBox(int text, bool use)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawBoxColor(int text, int color)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawSetShadow(int text, int size)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawSetOutline(int text, int size)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawBackgroundColor(int text, int color)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawFont(int text, int font)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawSetProportional(int text, bool set)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawSetSelectable(int text, bool set)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawShowForPlayer(int playerid, int text)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawHideForPlayer(int playerid, int text)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawShowForAll(int text)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawHideForAll(int text)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawSetString(int text, string str)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawSetPreviewModel(int text, int modelindex)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawSetPreviewRot(int text, float rotX, float rotY, float rotZ, float zoom)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TextDrawSetPreviewVehCol(int text, int color1, int color2)
            {
                throw new NativeNotImplementedException();
            }
        }
#pragma warning restore CS1591
    }
}