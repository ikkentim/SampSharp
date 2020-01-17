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

namespace SampSharp.Entities.SAMP
{
    [NativeObjectIdentifiers("PlayerId", "Id")]
    public class NativePlayerTextDraw : NativeComponent
    {
        public const int InvalidId = 0xFFFF;

        public int Id { get; private set; }

        public int PlayerId { get; private set; }

        protected override void OnInitializeComponent()
        {
            Id = Entity.Id;
            PlayerId = Entity.Parent.Id;
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawDestroy()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawLetterSize(float x, float y)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawTextSize(float x, float y)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawAlignment(int alignment)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawColor(int color)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawUseBox(bool use)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawBoxColor(int color)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawSetShadow(int size)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawSetOutline(int size)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawBackgroundColor(int color)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawFont(int font)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawSetProportional(bool set)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawSetSelectable(bool set)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawShow()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawHide()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawSetString(string str)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawSetPreviewModel(int modelindex)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawSetPreviewRot(float fRotX, float fRotY, float fRotZ, float fZoom = 1.0f)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool PlayerTextDrawSetPreviewVehCol(int color1, int color2)
        {
            throw new NativeNotImplementedException();
        }
    }
}