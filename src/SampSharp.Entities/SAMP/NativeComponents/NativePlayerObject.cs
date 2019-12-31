// SampSharp
// Copyright 2019 Tim Potze
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

using System.Diagnostics.CodeAnalysis;
using SampSharp.Core.Natives.NativeObjects;

#pragma warning disable 1591

namespace SampSharp.Entities.SAMP.NativeComponents
{
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    [NativeObjectIdentifiers("PlayerId", "Id")]
    public class NativePlayerObject : NativeComponent
    {
        /// <summary>
        /// Identifier indicating the handle is invalid.
        /// </summary>
        public const int InvalidId = 0xFFFF;

        public int Id { get; private set; }

        public int PlayerId { get; private set; }

        protected override void OnInitializeComponent()
        {
            Id = Entity.Id;
            PlayerId = Entity.Parent.Id;
        }

        [NativeMethod]
        public virtual bool AttachPlayerObjectToPlayer(int attachplayerid,
            float offsetX, float offsetY, float offsetZ, float rX, float rY, float rZ)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool AttachPlayerObjectToVehicle(int vehicleid, float offsetX,
            float offsetY, float offsetZ, float rotX, float rotY, float rotZ)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetPlayerObjectPos(float x, float y, float z)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool GetPlayerObjectPos(out float x, out float y, out float z)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetPlayerObjectRot(float rotX, float rotY, float rotZ)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool GetPlayerObjectRot(out float rotX, out float rotY,
            out float rotZ)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int GetPlayerObjectModel()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetPlayerObjectNoCameraCol()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool IsValidPlayerObject()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool DestroyPlayerObject()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int MovePlayerObject(float x, float y, float z, float speed,
            float rotX, float rotY, float rotZ)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool StopPlayerObject()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool IsPlayerObjectMoving()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetPlayerObjectMaterial(int materialindex, int modelid,
            string txdname, string texturename, int materialcolor)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool SetPlayerObjectMaterialText(string text, int materialindex,
            int materialsize, string fontface, int fontsize, bool bold, int fontcolor, int backcolor,
            int textalignment)
        {
            throw new NativeNotImplementedException();
        }
    }
}