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

using SampSharp.Core.Natives.NativeObjects;

#pragma warning disable 1591

namespace SampSharp.Entities.SAMP;

public class WorldServiceNative
{
    [NativeMethod]
    public virtual int CreateActor(int modelId, float x, float y, float z, float rotation)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int CreateVehicle(int vehicleType, float x, float y, float z, float rotation, int color1,
        int color2, int respawnDelay, bool addSiren = false)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int AddStaticVehicleEx(int vehicleType, float x, float y, float z, float rotation, int color1,
        int color2, int respawnDelay, bool addSiren = false)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int AddStaticVehicle(int vehicleType, float x, float y, float z, float rotation, int color1,
        int color2)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GangZoneCreate(float minX, float minY, float maxX, float maxY)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int CreatePickup(int model, int type, float x, float y, float z, int virtualWorld)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool AddStaticPickup(int model, int type, float x, float y, float z, int virtualWorld)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int CreateObject(int modelId, float x, float y, float z, float rX, float rY, float rZ,
        float drawDistance)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int CreatePlayerObject(int playerId, int modelId, float x, float y, float z, float rX, float rY,
        float rZ, float drawDistance)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int Create3DTextLabel(string text, int color, float x, float y, float z, float drawDistance,
        int virtualWorld, bool testLos)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int CreatePlayer3DTextLabel(int playerId, string text, int color, float x, float y, float z,
        float drawDistance, int attachedPlayer, int attachedVehicle, bool testLos)
    {
        throw new NativeNotImplementedException();
    }
        
    [NativeMethod]
    public virtual int TextDrawCreate(float x, float y, string text)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int CreatePlayerTextDraw(int playerId, float x, float y, string text)
    {
        throw new NativeNotImplementedException();
    }
        
    [NativeMethod]
    public virtual int CreateMenu(string title, int columns, float x, float y, float col1Width, float col2Width = 0.0f)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetObjectsDefaultCameraCol(bool disable)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SendClientMessageToAll(int color, string message)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SendPlayerMessageToAll(int senderId, string message)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SendDeathMessage(int killerId, int killeeId, int weapon)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GameTextForAll(string text, int time, int style)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool CreateExplosion(float x, float y, float z, int type, float radius)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetWeather(int weatherId)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetGravity(float gravity)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual float GetGravity()
    {
        throw new NativeNotImplementedException();
    }
}