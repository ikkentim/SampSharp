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

namespace SampSharp.GameMode.Natives
{
    /// <summary>
    ///     Contains all native methods.
    /// </summary>
    public static partial class Native
    {
        public delegate bool AttachObjectToObjectImpl(int objectid, int attachtoid, float offsetX, float offsetY,
            float offsetZ, float rotX, float rotY, float rotZ, bool syncRotation);

        public delegate bool AttachObjectToPlayerImpl(int objectid, int playerid, float offsetX, float offsetY,
            float offsetZ, float rotX, float rotY, float rotZ);

        public delegate bool AttachObjectToVehicleImpl(int objectid, int vehicleid, float offsetX, float offsetY,
            float offsetZ, float rotX, float rotY, float rotZ);

        public delegate bool AttachPlayerObjectToPlayerImpl(int objectplayer, int objectid, int attachplayerid,
            float offsetX, float offsetY, float offsetZ, float rX, float rY, float rZ);

        public delegate bool AttachPlayerObjectToVehicleImpl(int playerid, int objectid, int vehicleid, float offsetX,
            float offsetY, float offsetZ, float rotX, float rotY, float rotZ);

        public delegate bool CancelEditImpl(int playerid);

        public delegate int CreateObjectImpl(int modelid, float x, float y, float z, float rX, float rY, float rZ,
            float drawDistance);

        public delegate int CreatePlayerObjectImpl(int playerid, int modelid, float x, float y, float z, float rX,
            float rY, float rZ, float drawDistance);

        public delegate bool DestroyObjectImpl(int objectid);

        public delegate bool DestroyPlayerObjectImpl(int playerid, int objectid);

        public delegate bool EditObjectImpl(int playerid, int objectid);

        public delegate bool EditPlayerObjectImpl(int playerid, int objectid);

        public delegate int GetObjectModelImpl(int objectid);

        public delegate bool GetObjectPosImpl(int objectid, out float x, out float y, out float z);

        public delegate bool GetObjectRotImpl(int objectid, out float rotX, out float rotY, out float rotZ);

        public delegate int GetPlayerObjectModelImpl(int playerid, int objectid);

        public delegate bool GetPlayerObjectPosImpl(int playerid, int objectid, out float x, out float y, out float z);

        public delegate bool GetPlayerObjectRotImpl(int playerid, int objectid, out float rotX, out float rotY,
            out float rotZ);

        public delegate bool IsObjectMovingImpl(int objectid);

        public delegate bool IsPlayerObjectMovingImpl(int playerid, int objectid);

        public delegate bool IsValidObjectImpl(int objectid);

        public delegate bool IsValidPlayerObjectImpl(int playerid, int objectid);

        public delegate int MoveObjectImpl(int objectid, float x, float y, float z, float speed, float rotX, float rotY,
            float rotZ);

        public delegate int MovePlayerObjectImpl(int playerid, int objectid, float x, float y, float z, float speed,
            float rotX, float rotY, float rotZ);

        public delegate bool SelectObjectImpl(int playerid);

        public delegate bool SetObjectMaterialImpl(int objectid, int materialindex, int modelid, string txdname,
            string texturename, int materialcolor);

        public delegate bool SetObjectMaterialTextImpl(int objectid, string text, int materialindex, int materialsize,
            string fontface, int fontsize, bool bold, int fontcolor, int backcolor, int textalignment);

        public delegate bool SetObjectNoCameraColImpl(int objectid);

        public delegate bool SetObjectPosImpl(int objectid, float x, float y, float z);

        public delegate bool SetObjectRotImpl(int objectid, float rotX, float rotY, float rotZ);

        public delegate bool SetObjectsDefaultCameraColImpl(bool disable);

        public delegate bool SetPlayerObjectMaterialImpl(int playerid, int objectid, int materialindex, int modelid,
            string txdname, string texturename, int materialcolor);

        public delegate bool SetPlayerObjectMaterialTextImpl(int playerid, int objectid, string text, int materialindex,
            int materialsize, string fontface, int fontsize, bool bold, int fontcolor, int backcolor, int textalignment);

        public delegate bool SetPlayerObjectNoCameraColImpl(int playerid, int objectid);

        public delegate bool SetPlayerObjectPosImpl(int playerid, int objectid, float x, float y, float z);

        public delegate bool SetPlayerObjectRotImpl(int playerid, int objectid, float rotX, float rotY, float rotZ);

        public delegate bool StopObjectImpl(int objectid);

        public delegate bool StopPlayerObjectImpl(int playerid, int objectid);

        [Native("CreateObject")] public static readonly CreateObjectImpl CreateObject = null;
        [Native("AttachObjectToVehicle")] public static readonly AttachObjectToVehicleImpl AttachObjectToVehicle = null;
        [Native("AttachObjectToObject")] public static readonly AttachObjectToObjectImpl AttachObjectToObject = null;
        [Native("AttachObjectToPlayer")] public static readonly AttachObjectToPlayerImpl AttachObjectToPlayer = null;
        [Native("SetObjectPos")] public static readonly SetObjectPosImpl SetObjectPos = null;
        [Native("GetObjectPos")] public static readonly GetObjectPosImpl GetObjectPos = null;
        [Native("SetObjectRot")] public static readonly SetObjectRotImpl SetObjectRot = null;
        [Native("GetObjectRot")] public static readonly GetObjectRotImpl GetObjectRot = null;
        [Native("GetObjectModel")] public static readonly GetObjectModelImpl GetObjectModel = null;
        [Native("SetObjectNoCameraCol")] public static readonly SetObjectNoCameraColImpl SetObjectNoCameraCol = null;
        [Native("IsValidObject")] public static readonly IsValidObjectImpl IsValidObject = null;
        [Native("DestroyObject")] public static readonly DestroyObjectImpl DestroyObject = null;
        [Native("MoveObject")] public static readonly MoveObjectImpl MoveObject = null;
        [Native("StopObject")] public static readonly StopObjectImpl StopObject = null;
        [Native("IsObjectMoving")] public static readonly IsObjectMovingImpl IsObjectMoving = null;
        [Native("EditObject")] public static readonly EditObjectImpl EditObject = null;
        [Native("EditPlayerObject")] public static readonly EditPlayerObjectImpl EditPlayerObject = null;
        [Native("SelectObject")] public static readonly SelectObjectImpl SelectObject = null;
        [Native("CancelEdit")] public static readonly CancelEditImpl CancelEdit = null;
        [Native("CreatePlayerObject")] public static readonly CreatePlayerObjectImpl CreatePlayerObject = null;

        [Native("AttachPlayerObjectToPlayer")] public static readonly AttachPlayerObjectToPlayerImpl
            AttachPlayerObjectToPlayer = null;

        [Native("AttachPlayerObjectToVehicle")] public static readonly AttachPlayerObjectToVehicleImpl
            AttachPlayerObjectToVehicle = null;

        [Native("SetPlayerObjectPos")] public static readonly SetPlayerObjectPosImpl SetPlayerObjectPos = null;
        [Native("GetPlayerObjectPos")] public static readonly GetPlayerObjectPosImpl GetPlayerObjectPos = null;
        [Native("SetPlayerObjectRot")] public static readonly SetPlayerObjectRotImpl SetPlayerObjectRot = null;
        [Native("GetPlayerObjectRot")] public static readonly GetPlayerObjectRotImpl GetPlayerObjectRot = null;
        [Native("GetPlayerObjectModel")] public static readonly GetPlayerObjectModelImpl GetPlayerObjectModel = null;

        [Native("SetPlayerObjectNoCameraCol")] public static readonly SetPlayerObjectNoCameraColImpl
            SetPlayerObjectNoCameraCol = null;

        [Native("IsValidPlayerObject")] public static readonly IsValidPlayerObjectImpl IsValidPlayerObject = null;
        [Native("DestroyPlayerObject")] public static readonly DestroyPlayerObjectImpl DestroyPlayerObject = null;
        [Native("MovePlayerObject")] public static readonly MovePlayerObjectImpl MovePlayerObject = null;
        [Native("StopPlayerObject")] public static readonly StopPlayerObjectImpl StopPlayerObject = null;
        [Native("IsPlayerObjectMoving")] public static readonly IsPlayerObjectMovingImpl IsPlayerObjectMoving = null;

        [Native("SetObjectsDefaultCameraCol")] public static readonly SetObjectsDefaultCameraColImpl
            SetObjectsDefaultCameraCol = null;

        [Native("SetObjectMaterial")] public static readonly SetObjectMaterialImpl SetObjectMaterial = null;

        [Native("SetPlayerObjectMaterial")] public static readonly SetPlayerObjectMaterialImpl SetPlayerObjectMaterial =
            null;

        [Native("SetObjectMaterialText")] public static readonly SetObjectMaterialTextImpl SetObjectMaterialText = null;

        [Native("SetPlayerObjectMaterialText")] public static readonly SetPlayerObjectMaterialTextImpl
            SetPlayerObjectMaterialText = null;
    }
}