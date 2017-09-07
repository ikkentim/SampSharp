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
    public partial class GlobalObject
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public class GlobalObjectInternal : NativeObjectSingleton<GlobalObjectInternal>
        {
            [NativeMethod]
            public virtual bool AttachCameraToObject(int playerid, int objectid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool RemoveBuildingForPlayer(int playerid, int modelid, float x, float y, float z,
                float radius)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int CreateObject(int modelid, float x, float y, float z, float rX, float rY, float rZ,
                float drawDistance)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool AttachObjectToVehicle(int objectid, int vehicleid, float offsetX, float offsetY,
                float offsetZ, float rotX, float rotY, float rotZ)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool AttachObjectToObject(int objectid, int attachtoid, float offsetX, float offsetY,
                float offsetZ, float rotX, float rotY, float rotZ, bool syncRotation)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool AttachObjectToPlayer(int objectid, int playerid, float offsetX, float offsetY,
                float offsetZ, float rotX, float rotY, float rotZ)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetObjectPos(int objectid, float x, float y, float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetObjectPos(int objectid, out float x, out float y, out float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetObjectRot(int objectid, float rotX, float rotY, float rotZ)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetObjectRot(int objectid, out float rotX, out float rotY, out float rotZ)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetObjectModel(int objectid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetObjectNoCameraCol(int objectid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsValidObject(int objectid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool DestroyObject(int objectid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int MoveObject(int objectid, float x, float y, float z, float speed, float rotX, float rotY,
                float rotZ)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool StopObject(int objectid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsObjectMoving(int objectid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool EditObject(int playerid, int objectid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SelectObject(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetObjectsDefaultCameraCol(bool disable)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetObjectMaterial(int objectid, int materialindex, int modelid, string txdname,
                string texturename, int materialcolor)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetObjectMaterialText(int objectid, string text, int materialindex, int materialsize,
                string fontface, int fontsize, bool bold, int fontcolor, int backcolor, int textalignment)
            {
                throw new NativeNotImplementedException();
            }
        }
#pragma warning restore CS1591
    }
}