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

using System.Runtime.CompilerServices;

namespace SampSharp.GameMode.Natives
{
    /// <summary>
    ///     Contains all native methods.
    /// </summary>
    public static partial class Native
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreateObject(int modelid, float x, float y, float z, float rX, float rY, float rZ,
            float drawDistance);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AttachObjectToVehicle(int objectid, int vehicleid, float offsetX, float offsetY,
            float offsetZ, float rotX, float rotY, float rotZ);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AttachObjectToObject(int objectid, int attachtoid, float offsetX, float offsetY,
            float offsetZ, float rotX, float rotY, float rotZ, bool syncRotation);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AttachObjectToPlayer(int objectid, int playerid, float offsetX, float offsetY,
            float offsetZ, float rotX, float rotY, float rotZ);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetObjectPos(int objectid, float x, float y, float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetObjectPos(int objectid, out float x, out float y, out float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetObjectRot(int objectid, float rotX, float rotY, float rotZ);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetObjectRot(int objectid, out float rotX, out float rotY, out float rotZ);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetObjectModel(int objectid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetObjectNoCameraCol(int objectid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsValidObject(int objectid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DestroyObject(int objectid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int MoveObject(int objectid, float x, float y, float z, float speed, float rotX, float rotY,
            float rotZ);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool StopObject(int objectid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsObjectMoving(int objectid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EditObject(int playerid, int objectid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EditPlayerObject(int playerid, int objectid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SelectObject(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool CancelEdit(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreatePlayerObject(int playerid, int modelid, float x, float y, float z, float rX,
            float rY, float rZ, float drawDistance);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AttachPlayerObjectToPlayer(int objectplayer, int objectid, int attachplayerid,
            float offsetX, float offsetY, float offsetZ, float rX, float rY, float rZ);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AttachPlayerObjectToVehicle(int playerid, int objectid, int vehicleid, float offsetX,
            float offsetY, float offsetZ, float rotX, float rotY, float rotZ);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerObjectPos(int playerid, int objectid, float x, float y, float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerObjectPos(int playerid, int objectid, out float x, out float y, out float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerObjectRot(int playerid, int objectid, float rotX, float rotY, float rotZ);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerObjectRot(int playerid, int objectid, out float rotX, out float rotY,
            out float rotZ);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerObjectModel(int playerid, int objectid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerObjectNoCameraCol(int playerid, int objectid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsValidPlayerObject(int playerid, int objectid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DestroyPlayerObject(int playerid, int objectid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int MovePlayerObject(int playerid, int objectid, float x, float y, float z, float speed,
            float rotX, float rotY, float rotZ);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool StopPlayerObject(int playerid, int objectid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerObjectMoving(int playerid, int objectid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetObjectsDefaultCameraCol(bool disable);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetObjectMaterial(int objectid, int materialindex, int modelid, string txdname,
            string texturename, int materialcolor);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerObjectMaterial(int playerid, int objectid, int materialindex, int modelid,
            string txdname, string texturename, int materialcolor);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetObjectMaterialText(int objectid, string text, int materialindex, int materialsize,
            string fontface, int fontsize, bool bold, int fontcolor, int backcolor, int textalignment);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerObjectMaterialText(int playerid, int objectid, string text, int materialindex,
            int materialsize, string fontface, int fontsize, bool bold, int fontcolor, int backcolor, int textalignment);
    }
}