// SampSharp
// Copyright 2020 Tim Potze
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

using System;
using SampSharp.Entities.SAMP.Definitions;
using SampSharp.Entities.SAMP.NativeComponents;

namespace SampSharp.Entities.SAMP.Components
{
    /// <summary>
    /// Represents a component which provides the data and functionality of an object.
    /// </summary>
    public class GlobalObject : Component
    {
        private GlobalObject(float drawDistance)
        {
            DrawDistance = drawDistance;
        }

        /// <summary>
        /// Gets the rotation of this object.
        /// </summary>
        public Vector3 Rotation
        {
            get
            {
                GetComponent<NativeObject>().GetObjectRot(out var x, out var y, out var z);
                return new Vector3(x, y, z);
            }
            set => GetComponent<NativeObject>().SetObjectRot(value.X, value.Y, value.Z);
        }

        /// <summary>
        /// Gets the position of this object.
        /// </summary>
        public Vector3 Position
        {
            get
            {
                GetComponent<NativeObject>().GetObjectPos(out var x, out var y, out var z);
                return new Vector3(x, y, z);
            }
            set => GetComponent<NativeObject>().SetObjectPos(value.X, value.Y, value.Z);
        }

        /// <summary>
        /// Gets whether this object is moving.
        /// </summary>
        public bool IsMoving => GetComponent<NativeObject>().IsObjectMoving();

        /// <summary>
        /// Gets the model of this object.
        /// </summary>
        public int ModelId => GetComponent<NativeObject>().GetObjectModel();

        /// <summary>
        /// Gets the draw distance of this object.
        /// </summary>
        public float DrawDistance { get; }

        /// <inheritdoc />
        protected override void OnDestroyComponent()
        {
            GetComponent<NativeObject>().DestroyObject();
        }

        /// <summary>
        /// Moves this object to the given position and rotation with the given speed.
        /// </summary>
        /// <param name="position">The position to which to move this object.</param>
        /// <param name="speed">The speed at which to move this object.</param>
        /// <param name="rotation">The rotation to which to move this object.</param>
        /// <returns>
        /// The time it will take for the object to move in milliseconds.
        /// </returns>
        public int Move(Vector3 position, float speed, Vector3 rotation)
        {
            return GetComponent<NativeObject>().MoveObject(position.X, position.Y, position.Z, speed, rotation.X,
                rotation.Y, rotation.Z);
        }

        /// <summary>
        /// Moves this object to the given position with the given speed.
        /// </summary>
        /// <param name="position">The position to which to move this object.</param>
        /// <param name="speed">The speed at which to move this object.</param>
        /// <returns>
        /// The time it will take for the object to move in milliseconds.
        /// </returns>
        public int Move(Vector3 position, float speed)
        {
            return GetComponent<NativeObject>()
                .MoveObject(position.X, position.Y, position.Z, speed, -1000, -1000, -1000);
        }

        /// <summary>
        /// Stop this object from moving any further.
        /// </summary>
        public void Stop()
        {
            GetComponent<NativeObject>().StopObject();
        }

        /// <summary>
        /// Sets the material of this object.
        /// </summary>
        /// <param name="materialIndex">The material index on the object to change.</param>
        /// <param name="modelId">
        /// The model ID on which the replacement texture is located. Use 0 for alpha. Use -1 to change the
        /// material color without altering the texture.
        /// </param>
        /// <param name="txdName">The name of the txd file which contains the replacement texture (use "none" if not required).</param>
        /// <param name="textureName">The name of the texture to use as the replacement (use "none" if not required).</param>
        /// <param name="materialColor">The object color to set (use default(Color) to keep the existing material color).</param>
        public void SetMaterial(int materialIndex, int modelId, string txdName, string textureName,
            Color materialColor)
        {
            GetComponent<NativeObject>().SetObjectMaterial(materialIndex, modelId, txdName, textureName,
                materialColor.ToInteger(ColorFormat.ARGB));
        }

        /// <summary>
        /// Sets the material text of this object.
        /// </summary>
        /// <param name="materialIndex">The material index on the object to change.</param>
        /// <param name="text">The text to show on the object. (MAX 2048 characters)</param>
        /// <param name="materialSize">The object's material index to replace with text.</param>
        /// <param name="fontface">The font to use.</param>
        /// <param name="fontSize">The size of the text (max 255).</param>
        /// <param name="bold">Whether to write in bold.</param>
        /// <param name="foreColor">The color of the text.</param>
        /// <param name="backColor">The background color of the text.</param>
        /// <param name="textAlignment">The alignment of the text.</param>
        public void SetMaterialText(int materialIndex, string text, ObjectMaterialSize materialSize,
            string fontface, int fontSize, bool bold, Color foreColor, Color backColor,
            ObjectMaterialTextAlign textAlignment)
        {
            GetComponent<NativeObject>().SetObjectMaterialText(text, materialIndex, (int) materialSize, fontface,
                fontSize, bold,
                foreColor.ToInteger(ColorFormat.ARGB), backColor.ToInteger(ColorFormat.ARGB),
                (int) textAlignment);
        }

        /// <summary>
        /// Disable collisions between players' cameras and this <see cref="GlobalObject" />.
        /// </summary>
        public void DisableCameraCollisions()
        {
            GetComponent<NativeObject>().SetObjectNoCameraCol();
        }

        /// <summary>
        /// Attaches this object to the specified player, vehicle or object.
        /// </summary>
        /// <param name="target">The player or vehicle.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="syncRotation">if set to <c>true</c> synchronize rotation with objects attached to.</param>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public void AttachTo(Entity target, Vector3 offset, Vector3 rotation, bool syncRotation = false)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (target.GetComponent<NativePlayer>() != null)
                GetComponent<NativeObject>().AttachObjectToPlayer(target.Id, offset.X, offset.Y, offset.Z, rotation.X,
                    rotation.Y,
                    rotation.Z);
            else if (target.GetComponent<NativePlayer>() != null)
                GetComponent<NativeObject>().AttachObjectToVehicle(target.Id, offset.X, offset.Y, offset.Z, rotation.X,
                    rotation.Y,
                    rotation.Z);
            else if (target.GetComponent<NativeObject>() != null)
                GetComponent<NativeObject>().AttachObjectToObject(target.Id, offset.X, offset.Y, offset.Z, rotation.X,
                    rotation.Y,
                    rotation.Z, syncRotation);
            else
                throw new ArgumentException("Target must be an object, player or vehicle.", nameof(target));
        }
    }
}