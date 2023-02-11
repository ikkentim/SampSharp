﻿// SampSharp
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

namespace SampSharp.Entities.SAMP;

/// <summary>Represents a component which provides the data and functionality of a player object.</summary>
public class PlayerObject : Component
{
    /// <summary>Constructs an instance of PlayerObject, should be used internally.</summary>
    protected PlayerObject(float drawDistance)
    {
        DrawDistance = drawDistance;
    }


    /// <summary>Gets the rotation of this player object.</summary>
    public virtual Vector3 Rotation
    {
        get
        {
            GetComponent<NativePlayerObject>()
                .GetPlayerObjectRot(out var x, out var y, out var z);
            return new Vector3(x, y, z);
        }
        set => GetComponent<NativePlayerObject>()
            .SetPlayerObjectRot(value.X, value.Y, value.Z);
    }

    /// <summary>Gets the position of this player object.</summary>
    public virtual Vector3 Position
    {
        get
        {
            GetComponent<NativePlayerObject>()
                .GetPlayerObjectPos(out var x, out var y, out var z);
            return new Vector3(x, y, z);
        }
        set => GetComponent<NativePlayerObject>()
            .SetPlayerObjectPos(value.X, value.Y, value.Z);
    }

    /// <summary>Gets whether this player object is moving.</summary>
    public virtual bool IsMoving => GetComponent<NativePlayerObject>()
        .IsPlayerObjectMoving();

    /// <summary>Gets whether this player object is valid.</summary>
    public virtual bool IsValid => GetComponent<NativePlayerObject>()
        .IsValidPlayerObject();

    /// <summary>Gets the model of this player object.</summary>
    public virtual int ModelId => GetComponent<NativePlayerObject>()
        .GetPlayerObjectModel();

    /// <summary>Gets the draw distance of this player object.</summary>
    public virtual float DrawDistance { get; }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        GetComponent<NativePlayerObject>()
            .DestroyPlayerObject();

        base.OnDestroyComponent();
    }

    /// <summary>Moves this player object to the given position and rotation with the given speed.</summary>
    /// <param name="position">The position to which to move this player object.</param>
    /// <param name="speed">The speed at which to move this player object.</param>
    /// <param name="rotation">The rotation to which to move this player object.</param>
    /// <returns>The time it will take for the object to move in milliseconds.</returns>
    public virtual int Move(Vector3 position, float speed, Vector3 rotation)
    {
        return GetComponent<NativePlayerObject>()
            .MovePlayerObject(position.X, position.Y, position.Z, speed, rotation.X, rotation.Y, rotation.Z);
    }

    /// <summary>Moves this player object to the given position with the given speed.</summary>
    /// <param name="position">The position to which to move this player object.</param>
    /// <param name="speed">The speed at which to move this player object.</param>
    /// <returns>The time it will take for the object to move in milliseconds.</returns>
    public virtual int Move(Vector3 position, float speed)
    {
        return GetComponent<NativePlayerObject>()
            .MovePlayerObject(position.X, position.Y, position.Z, speed, -1000, -1000, -1000);
    }

    /// <summary>Stop this player object from moving any further.</summary>
    public virtual void Stop()
    {
        GetComponent<NativePlayerObject>()
            .StopPlayerObject();
    }

    /// <summary>Sets the material of this player object.</summary>
    /// <param name="materialIndex">The material index on the object to change.</param>
    /// <param name="modelId">
    /// The model ID on which the replacement texture is located. Use 0 for alpha. Use -1 to change the material color without altering the
    /// texture.
    /// </param>
    /// <param name="txdName">The name of the txd file which contains the replacement texture (use "none" if not required).</param>
    /// <param name="textureName">The name of the texture to use as the replacement (use "none" if not required).</param>
    /// <param name="materialColor">The object color to set (use default(Color) to keep the existing material color).</param>
    public virtual void SetMaterial(int materialIndex, int modelId, string txdName, string textureName, Color materialColor)
    {
        GetComponent<NativePlayerObject>()
            .SetPlayerObjectMaterial(materialIndex, modelId, txdName, textureName, materialColor.ToInteger(ColorFormat.ARGB));
    }

    /// <summary>Sets the material text of this player object.</summary>
    /// <param name="materialIndex">The material index on the object to change.</param>
    /// <param name="text">The text to show on the object. (MAX 2048 characters)</param>
    /// <param name="materialSize">The object's material index to replace with text.</param>
    /// <param name="fontface">The font to use.</param>
    /// <param name="fontSize">The size of the text (max 255).</param>
    /// <param name="bold">Whether to write in bold.</param>
    /// <param name="foreColor">The color of the text.</param>
    /// <param name="backColor">The background color of the text.</param>
    /// <param name="textAlignment">The alignment of the text.</param>
    public virtual void SetMaterialText(int materialIndex, string text, ObjectMaterialSize materialSize, string fontface, int fontSize, bool bold, Color foreColor,
        Color backColor, ObjectMaterialTextAlign textAlignment)
    {
        GetComponent<NativePlayerObject>()
            .SetPlayerObjectMaterialText(text, materialIndex, (int)materialSize, fontface, fontSize, bold, foreColor.ToInteger(ColorFormat.ARGB),
                backColor.ToInteger(ColorFormat.ARGB), (int)textAlignment);
    }

    /// <summary>Disable collisions between players' cameras and this player object.</summary>
    public virtual void DisableCameraCollisions()
    {
        GetComponent<NativePlayerObject>()
            .SetPlayerObjectNoCameraCol();
    }

    /// <summary>Attaches this player object to the specified player object or vehicle.</summary>
    /// <param name="target">The player.</param>
    /// <param name="offset">The offset.</param>
    /// <param name="rotation">The rotation.</param>
    public virtual void AttachTo(EntityId target, Vector3 offset, Vector3 rotation)
    {
        if (!target.IsOfAnyType(SampEntities.PlayerType, SampEntities.VehicleType))
            throw new InvalidEntityArgumentException(nameof(target), SampEntities.PlayerType, SampEntities.VehicleType);

        if (target.IsOfType(SampEntities.PlayerType))
            GetComponent<NativePlayerObject>()
                .AttachPlayerObjectToPlayer(target, offset.X, offset.Y, offset.Z, rotation.X, rotation.Y, rotation.Z);
        else
            GetComponent<NativePlayerObject>()
                .AttachPlayerObjectToVehicle(target, offset.X, offset.Y, offset.Z, rotation.X, rotation.Y, rotation.Z);
    }
}
