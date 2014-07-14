// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System;
using System.CodeDom;
using System.Configuration;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.SAMP;
using IMPLTYPEFLAGS = System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS;

namespace SampSharp.Streamer.Natives
{
    public static partial class StreamerNative
    {
        public static int CreateDynamicObject(int modelid, float x, float y, float z, float rx, float ry, float rz,
            int worldid = -1, int interiorid = -1, int playerid = -1, float streamdistance = 200.0f,
            float drawdistance = 0.0f)
        {
            return Native.CallNative("CreateDynamicObject",
                __arglist(modelid, x, y, z, rx, ry, rz, worldid, interiorid, playerid, streamdistance));
        }

        public static int DestroyDynamicObject(int objectid)
        {
            return Native.CallNative("DestroyDynamicObject", __arglist(objectid));
        }

        public static bool IsValidDynamicObject(int objectid)
        {
            return Native.CallNativeBool("IsValidDynamicObject", __arglist(objectid));
        }

        public static int SetDynamicObjectPos(int objectid, float x, float y, float z)
        {
            return Native.CallNative("SetDynamicObjectPos", __arglist(objectid, x, y, z));
        }

        public static int GetDynamicObjectPos(int objectid, out float x, out float y, out float z)
        {
            return Native.CallNative("GetDynamicObjectPos", __arglist(objectid, out x, out y, out z));
        }

        public static int SetDynamicObjectRot(int objectid, float rx, float ry, float rz)
        {
            return Native.CallNative("SetDynamicObjectRot", __arglist(objectid, rx, ry, rz));
        }

        public static int GetDynamicObjectRot(int objectid, out float rx, out float ry, out float rz)
        {
            return Native.CallNative("GetDynamicObjectRot", __arglist(objectid, out rx, out ry, out rz));
        }

        public static int MoveDynamicObject(int objectid, float x, float y, float z, float speed, float rx = -1000.0f,
            float ry = -1000.0f, float rz = -1000.0f)
        {
            return Native.CallNative("MoveDynamicObject", __arglist(objectid, x, y, z, speed, rx, ry, rz));
        }

        public static int StopDynamicObject(int objectid)
        {
            return Native.CallNative("StopDynamicObject", __arglist(objectid));
        }

        public static bool IsDynamicObjectMoving(int objectid)
        {
            return Native.CallNativeBool("IsDynamicObjectMoving", __arglist(objectid));
        }

        public static int AttachCameraToDynamicObject(int playerid, int objectid)
        {
            return Native.CallNative("AttachCameraToDynamicObject", __arglist(playerid, objectid));
        }

        public static int AttachDynamicObjectToVehicle(int objectid, int vehicleid, float offsetx, float offsety,
            float offsetz, float rx, float ry, float rz)
        {
            return Native.CallNative("AttachDynamicObjectToVehicle",
                __arglist(objectid, vehicleid, offsetx, offsety, offsetz, rx, ry, rz));
        }

        public static int EditDynamicObject(int playerid, int objectid)
        {
            return Native.CallNative("EditDynamicObject", __arglist(playerid, objectid));
        }

        public static int GetDynamicObjectMaterial(int objectid, int materialindex, out int modelid, out string txdname,
            out string texturename, out Color materialcolor, int maxtxdname, int maxtexturename)
        {
            //TODO: Plugin does not yet support string-size args not located after the output string itself
            throw new NotImplementedException();

            int holderMaterialColor;
            int response = Native.CallNative("GetDynamicObjectMaterial",
                __arglist(
                    objectid, materialindex, out modelid, out txdname, out texturename, out holderMaterialColor,
                    maxtxdname,
                    maxtexturename));

            materialcolor = holderMaterialColor;
            return response;
        }

        public static int SetDynamicObjectMaterial(int objectid, int materialindex, int modelid, string txdname,
            string texturename, Color materialcolor = new Color())
        {
            return Native.CallNative("SetDynamicObjectMaterial",
                __arglist(
                    objectid, materialindex, modelid, txdname, texturename,
                    materialcolor.GetColorValue(ColorFormat.ARGB)));
        }

        public static int GetDynamicObjectMaterialText(int objectid, int materialindex, out string text,
            out ObjectMaterialSize materialsize, out string fontface, out int fontsize, out bool bold,
            out Color fontcolor, out Color backcolor, out ObjectMaterialTextAlign textalignment, int maxtext,
            int maxfontface)
        {
            //TODO: Plugin does not yet support string-size args not located after the output string itself
            throw new NotImplementedException();

            int holderMaterialSize, holderTextAlignment, holderFontColor, holderBackColor;
            return Native.CallNative("GetDynamicObjectMaterialText",
                __arglist(
                    objectid, materialindex, out text, out holderMaterialSize, out fontface, out fontsize, out bold,
                    out holderFontColor, out holderBackColor, out holderTextAlignment, maxtext, maxfontface));

            materialsize = (ObjectMaterialSize) holderMaterialSize;
            textalignment = (ObjectMaterialTextAlign) holderTextAlignment;
            fontcolor = holderFontColor;
            backcolor = holderBackColor;
        }

        public static int SetDynamicObjectMaterialText(int objectid, int materialindex, string text,
            ObjectMaterialSize materialsize = ObjectMaterialSize.X256X128, string fontface = "Arial", int fontsize = 24,
            bool bold = true, Color fontcolor = new Color(), Color backcolor = new Color(),
            ObjectMaterialTextAlign textalignment = ObjectMaterialTextAlign.Center)
        {
            //TODO: Default fontcolor should be white.
            return Native.CallNative("SetDynamicObjectMaterialText",
                __arglist(
                    objectid, materialindex, text, (int) materialsize, fontface, fontsize, bold, (int) fontcolor,
                    (int) backcolor, (int) textalignment));
        }
    }
}