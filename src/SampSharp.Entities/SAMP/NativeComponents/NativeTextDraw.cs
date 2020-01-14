// SampSharp
// Copyright 2020 Tim Potze
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

#pragma warning disable 1591

namespace SampSharp.Entities.SAMP.NativeComponents
{
    public class NativeTextDraw : BaseNativeComponent
    {
        [NativeMethod]
        public virtual bool TextDrawDestroy()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool TextDrawLetterSize(float x, float y)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool TextDrawTextSize(float x, float y)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool TextDrawAlignment(int alignment)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool TextDrawColor(int color)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool TextDrawUseBox(bool use)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool TextDrawBoxColor(int color)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool TextDrawSetShadow(int size)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool TextDrawSetOutline(int size)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool TextDrawBackgroundColor(int color)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool TextDrawFont(int font)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool TextDrawSetProportional(bool set)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool TextDrawSetSelectable(bool set)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(IdentifiersIndex = 1)]
        public virtual bool TextDrawShowForPlayer(int playerid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(IdentifiersIndex = 1)]
        public virtual bool TextDrawHideForPlayer(int playerid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool TextDrawSetString(string str)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool TextDrawSetPreviewModel(int modelindex)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool TextDrawSetPreviewRot(float fRotX, float fRotY, float fRotZ, float fZoom = 1.0f)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool TextDrawSetPreviewVehCol(int color1, int color2)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool TextDrawShowForAll()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool TextDrawHideForAll()
        {
            throw new NativeNotImplementedException();
        }
    }
}