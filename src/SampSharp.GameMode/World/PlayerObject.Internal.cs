// SampSharp
// Copyright 2016 Tim Potze
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

using SampSharp.GameMode.API;

namespace SampSharp.GameMode.World
{
    public partial class PlayerObject
    {
        private static class Internal
        {
            public delegate bool AttachCameraToPlayerObjectImpl(int playerid, int playerobjectid);

            public delegate bool AttachPlayerObjectToPlayerImpl(int objectplayer, int objectid, int attachplayerid,
                float offsetX, float offsetY, float offsetZ, float rX, float rY, float rZ);

            public delegate bool AttachPlayerObjectToVehicleImpl(
                int playerid, int objectid, int vehicleid, float offsetX,
                float offsetY, float offsetZ, float rotX, float rotY, float rotZ);

            public delegate int CreatePlayerObjectImpl(int playerid, int modelid, float x, float y, float z, float rX,
                float rY, float rZ, float drawDistance);

            public delegate bool DestroyPlayerObjectImpl(int playerid, int objectid);

            public delegate bool EditPlayerObjectImpl(int playerid, int objectid);

            public delegate int GetPlayerObjectModelImpl(int playerid, int objectid);

            public delegate bool GetPlayerObjectPosImpl(
                int playerid, int objectid, out float x, out float y, out float z);

            public delegate bool GetPlayerObjectRotImpl(int playerid, int objectid, out float rotX, out float rotY,
                out float rotZ);

            public delegate bool IsPlayerObjectMovingImpl(int playerid, int objectid);

            public delegate bool IsValidPlayerObjectImpl(int playerid, int objectid);

            public delegate int MovePlayerObjectImpl(int playerid, int objectid, float x, float y, float z, float speed,
                float rotX, float rotY, float rotZ);

            public delegate bool SelectObjectImpl(int playerid);

            public delegate bool SetPlayerObjectMaterialImpl(int playerid, int objectid, int materialindex, int modelid,
                string txdname, string texturename, int materialcolor);

            public delegate bool SetPlayerObjectMaterialTextImpl(
                int playerid, int objectid, string text, int materialindex,
                int materialsize, string fontface, int fontsize, bool bold, int fontcolor, int backcolor,
                int textalignment);

            public delegate bool SetPlayerObjectNoCameraColImpl(int playerid, int objectid);

            public delegate bool SetPlayerObjectPosImpl(int playerid, int objectid, float x, float y, float z);

            public delegate bool SetPlayerObjectRotImpl(int playerid, int objectid, float rotX, float rotY, float rotZ);

            public delegate bool StopPlayerObjectImpl(int playerid, int objectid);


            [Native("AttachCameraToPlayerObject")] public static readonly AttachCameraToPlayerObjectImpl
                NativeAttachCameraToPlayerObject = null;

            [Native("EditPlayerObject")] public static readonly EditPlayerObjectImpl EditPlayerObject = null;

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

            [Native("SetPlayerObjectMaterial")] public static readonly SetPlayerObjectMaterialImpl
                SetPlayerObjectMaterial =
                    null;

            [Native("SetPlayerObjectMaterialText")] public static readonly SetPlayerObjectMaterialTextImpl
                SetPlayerObjectMaterialText = null;

            [Native("SelectObject")] public static readonly SelectObjectImpl SelectObject = null;
        }
    }
}