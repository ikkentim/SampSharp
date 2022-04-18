// SampSharp
// Copyright 2022 Tim Potze
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

namespace SampSharp.Entities.SAMP;

[SuppressMessage("ReSharper", "IdentifierTypo")]
public class NativeObject : BaseNativeComponent
{
    /// <summary>Identifier indicating the handle is invalid.</summary>
    public const int InvalidId = 0xFFFF;

    [NativeMethod]
    public virtual bool AttachObjectToVehicle(int vehicleid, float offsetX, float offsetY, float offsetZ, float rotX, float rotY, float rotZ)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool AttachObjectToObject(int attachtoid, float offsetX, float offsetY, float offsetZ, float rotX, float rotY, float rotZ, bool syncRotation)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool AttachObjectToPlayer(int playerid, float offsetX, float offsetY, float offsetZ, float rotX, float rotY, float rotZ)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetObjectPos(float x, float y, float z)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetObjectPos(out float x, out float y, out float z)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetObjectRot(float rotX, float rotY, float rotZ)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetObjectRot(out float rotX, out float rotY, out float rotZ)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetObjectModel()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetObjectNoCameraCol()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool IsValidObject()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool DestroyObject()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int MoveObject(float x, float y, float z, float speed, float rotX, float rotY, float rotZ)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool StopObject()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool IsObjectMoving()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetObjectMaterial(int materialindex, int modelid, string txdname, string texturename, int materialcolor)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetObjectMaterialText(string text, int materialindex, int materialsize, string fontface, int fontsize, bool bold, int fontcolor,
        int backcolor, int textalignment)
    {
        throw new NativeNotImplementedException();
    }
}