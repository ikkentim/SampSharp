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
    public partial class PlayerObject
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public class PlayerObjectInternal : NativeObjectSingleton<PlayerObjectInternal>
        {
            [NativeMethod]
            public virtual bool AttachCameraToPlayerObject(int playerid, int playerobjectid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool EditPlayerObject(int playerid, int objectid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int CreatePlayerObject(int playerid, int modelid, float x, float y, float z, float rX,
                float rY, float rZ, float drawDistance)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool AttachPlayerObjectToPlayer(int objectplayer, int objectid, int attachplayerid,
                float offsetX, float offsetY, float offsetZ, float rX, float rY, float rZ)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool AttachPlayerObjectToVehicle(int playerid, int objectid, int vehicleid, float offsetX,
                float offsetY, float offsetZ, float rotX, float rotY, float rotZ)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerObjectPos(int playerid, int objectid, float x, float y, float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetPlayerObjectPos(int playerid, int objectid, out float x, out float y, out float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerObjectRot(int playerid, int objectid, float rotX, float rotY, float rotZ)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetPlayerObjectRot(int playerid, int objectid, out float rotX, out float rotY,
                out float rotZ)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerObjectModel(int playerid, int objectid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerObjectNoCameraCol(int playerid, int objectid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsValidPlayerObject(int playerid, int objectid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool DestroyPlayerObject(int playerid, int objectid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int MovePlayerObject(int playerid, int objectid, float x, float y, float z, float speed,
                float rotX, float rotY, float rotZ)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool StopPlayerObject(int playerid, int objectid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsPlayerObjectMoving(int playerid, int objectid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerObjectMaterial(int playerid, int objectid, int materialindex, int modelid,
                string txdname, string texturename, int materialcolor)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerObjectMaterialText(int playerid, int objectid, string text, int materialindex,
                int materialsize, string fontface, int fontsize, bool bold, int fontcolor, int backcolor,
                int textalignment)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SelectObject(int playerid)
            {
                throw new NativeNotImplementedException();
            }
        }
#pragma warning restore CS1591
    }
}