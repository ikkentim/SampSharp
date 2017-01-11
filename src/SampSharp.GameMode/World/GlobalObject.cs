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
using System;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;

namespace SampSharp.GameMode.World
{
    /// <summary>
    ///     Represents a global object
    /// </summary>
    public partial class GlobalObject : IdentifiedPool<GlobalObject>, IGameObject
    {
        /// <summary>
        ///     Identifier indicating the handle is invalid.
        /// </summary>
        public const int InvalidId = 0xFFFF;

        /// <summary>
        ///     Maximum number of objects which can exist.
        /// </summary>
        public const int Max = 1000;

        #region Properties

        /// <summary>
        ///     Gets the rotation of this IGameObject.
        /// </summary>
        public virtual Vector3 Rotation
        {
            get
            {
                float x, y, z;
                GlobalObjectInternal.Instance.GetObjectRot(Id, out x, out y, out z);
                return new Vector3(x, y, z);
            }
            set { GlobalObjectInternal.Instance.SetObjectRot(Id, value.X, value.Y, value.Z); }
        }

        /// <summary>
        ///     Gets the position of this IWorldObject.
        /// </summary>
        public virtual Vector3 Position
        {
            get
            {
                float x, y, z;
                GlobalObjectInternal.Instance.GetObjectPos(Id, out x, out y, out z);
                return new Vector3(x, y, z);
            }
            set { GlobalObjectInternal.Instance.SetObjectPos(Id, value.X, value.Y, value.Z); }
        }

        /// <summary>
        ///     Gets whether this IGameObject is moving.
        /// </summary>
        public virtual bool IsMoving => GlobalObjectInternal.Instance.IsObjectMoving(Id);

        /// <summary>
        ///     Gets whether this IGameObject is valid.
        /// </summary>
        public virtual bool IsValid => GlobalObjectInternal.Instance.IsValidObject(Id);

        /// <summary>
        ///     Gets the model of this IGameObject.
        /// </summary>
        public virtual int ModelId
        {
            get
            {
                AssertNotDisposed();
                return GlobalObjectInternal.Instance.GetObjectModel(Id);
            }
        }

        /// <summary>
        ///     Gets the draw distance of this IGameObject.
        /// </summary>
        public virtual float DrawDistance { get; }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the <see cref="OnMoved" /> callback is being called.
        ///     This callback is called when an object is moved after <see cref="Move(Vector3,float)" /> (when it stops moving).
        /// </summary>
        public event EventHandler<EventArgs> Moved;

        /// <summary>
        ///     Occurs when the <see cref="OnSelected" /> callback is being called.
        ///     This callback is called when a player selects an object after <see cref="Select" /> has been used.
        /// </summary>
        public event EventHandler<SelectGlobalObjectEventArgs> Selected;

        /// <summary>
        ///     Occurs when the <see cref="OnEdited" /> callback is being called.
        ///     This callback is called when a player ends object edition mode.
        /// </summary>
        public event EventHandler<EditGlobalObjectEventArgs> Edited;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="GlobalObject" /> class.
        /// </summary>
        public GlobalObject()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GlobalObject" /> class.
        /// </summary>
        /// <param name="modelid">The modelid.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="drawDistance">The draw distance.</param>
        public GlobalObject(int modelid, Vector3 position, Vector3 rotation, float drawDistance)
        {
            DrawDistance = drawDistance;

            Id = GlobalObjectInternal.Instance.CreateObject(modelid, position.X, position.Y, position.Z, rotation.X, rotation.Y, rotation.Z,
                drawDistance);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GlobalObject" /> class.
        /// </summary>
        /// <param name="modelid">The modelid.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        public GlobalObject(int modelid, Vector3 position, Vector3 rotation) : this(modelid, position, rotation, 0)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves this IGameObject to the given position and rotation with the given speed.
        /// </summary>
        /// <param name="position">The position to which to move this IGameObject.</param>
        /// <param name="speed">The speed at which to move this IGameObject.</param>
        /// <param name="rotation">The rotation to which to move this IGameObject.</param>
        /// <returns>
        ///     The time it will take for the object to move in milliseconds.
        /// </returns>
        public virtual int Move(Vector3 position, float speed, Vector3 rotation)
        {
            return GlobalObjectInternal.Instance.MoveObject(Id, position.X, position.Y, position.Z, speed, rotation.X, rotation.Y, rotation.Z);
        }

        /// <summary>
        ///     Moves this IGameObject to the given position with the given speed.
        /// </summary>
        /// <param name="position">The position to which to move this IGameObject.</param>
        /// <param name="speed">The speed at which to move this IGameObject.</param>
        /// <returns>
        ///     The time it will take for the object to move in milliseconds.
        /// </returns>
        public virtual int Move(Vector3 position, float speed)
        {
            return GlobalObjectInternal.Instance.MoveObject(Id, position.X, position.Y, position.Z, speed, -1000, -1000, -1000);
        }

        /// <summary>
        ///     Stop this IGameObject from moving any further.
        /// </summary>
        public virtual void Stop()
        {
            GlobalObjectInternal.Instance.StopObject(Id);
        }

        /// <summary>
        ///     Sets the material of this IGameObject.
        /// </summary>
        /// <param name="materialindex">The material index on the object to change.</param>
        /// <param name="modelid">
        ///     The modelid on which the replacement texture is located. Use 0 for alpha. Use -1 to change the
        ///     material color without altering the texture.
        /// </param>
        /// <param name="txdname">The name of the txd file which contains the replacement texture (use "none" if not required).</param>
        /// <param name="texturename">The name of the texture to use as the replacement (use "none" if not required).</param>
        /// <param name="materialcolor">The object color to set (use default(Color) to keep the existing material color).</param>
        public virtual void SetMaterial(int materialindex, int modelid, string txdname, string texturename,
            Color materialcolor)
        {
            GlobalObjectInternal.Instance.SetObjectMaterial(Id, materialindex, modelid, txdname, texturename,
                materialcolor.ToInteger(ColorFormat.ARGB));
        }

        /// <summary>
        ///     Sets the material text of this IGameObject.
        /// </summary>
        /// <param name="materialindex">The material index on the object to change.</param>
        /// <param name="text">The text to show on the object. (MAX 2048 characters)</param>
        /// <param name="materialsize">The object's material index to replace with text.</param>
        /// <param name="fontface">The font to use.</param>
        /// <param name="fontsize">The size of the text (max 255).</param>
        /// <param name="bold">Whether to write in bold.</param>
        /// <param name="foreColor">The color of the text.</param>
        /// <param name="backColor">The background color of the text.</param>
        /// <param name="textalignment">The alignment of the text.</param>
        public virtual void SetMaterialText(int materialindex, string text, ObjectMaterialSize materialsize,
            string fontface, int fontsize, bool bold, Color foreColor, Color backColor,
            ObjectMaterialTextAlign textalignment)
        {
            GlobalObjectInternal.Instance.SetObjectMaterialText(Id, text, materialindex, (int) materialsize, fontface, fontsize, bold,
                foreColor.ToInteger(ColorFormat.ARGB), backColor.ToInteger(ColorFormat.ARGB),
                (int) textalignment);
        }

        /// <summary>
        ///     Disable collisions between players' cameras and this <see cref="GlobalObject" />.
        /// </summary>
        public virtual void DisableCameraCollisions()
        {
            AssertNotDisposed();
            GlobalObjectInternal.Instance.SetObjectNoCameraCol(Id);
        }

        /// <summary>
        ///     Toggles the default camera collisions.
        /// </summary>
        /// <param name="toggle">If set to <c>true</c> the camera will be able to collide with objects by default.</param>
        public static void ToggleDefaultCameraCollisions(bool toggle)
        {
            GlobalObjectInternal.Instance.SetObjectsDefaultCameraCol(!toggle);
        }

        /// <summary>
        ///     Attaches this <see cref="GlobalObject" /> to the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="rotation">The rotation.</param>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public virtual void AttachTo(BasePlayer player, Vector3 offset, Vector3 rotation)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            GlobalObjectInternal.Instance.AttachObjectToPlayer(Id, player.Id, offset.X, offset.Y, offset.Z, rotation.X, rotation.Y,
                rotation.Z);
        }

        /// <summary>
        ///     Attaches this <see cref="GlobalObject" /> to the specified <paramref name="vehicle" />.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="rotation">The rotation.</param>
        /// <exception cref="System.ArgumentNullException">vehicle</exception>
        public virtual void AttachTo(BaseVehicle vehicle, Vector3 offset, Vector3 rotation)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));

            GlobalObjectInternal.Instance.AttachObjectToVehicle(Id, vehicle.Id, offset.X, offset.Y, offset.Z, rotation.X, rotation.Y,
                rotation.Z);
        }

        /// <summary>
        ///     Attaches a player's camera to this GlobalObject.
        /// </summary>
        /// <param name="player">The player whose camera to attach to this GlobalObject.</param>
        public virtual void AttachCameraToObject(BasePlayer player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            AssertNotDisposed();

            GlobalObjectInternal.Instance.AttachCameraToObject(player.Id, Id);
        }

        /// <summary>
        ///     Removes the specified object.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="modelid">The modelid.</param>
        /// <param name="position">The position.</param>
        /// <param name="radius">The radius.</param>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public static void Remove(BasePlayer player, int modelid, Vector3 position, float radius)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            GlobalObjectInternal.Instance.RemoveBuildingForPlayer(player.Id, modelid, position.X, position.Y, position.Z, radius);
        }

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            GlobalObjectInternal.Instance.DestroyObject(Id);
        }

        /// <summary>
        ///     Attaches this <see cref="GlobalObject" /> to the specified <paramref name="globalObject" />.
        /// </summary>
        /// <param name="globalObject">The global object.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="syncRotation">if set to <c>true</c> synchronize rotation.</param>
        /// <exception cref="System.ArgumentNullException">globalObject</exception>
        public virtual void AttachTo(GlobalObject globalObject, Vector3 offset, Vector3 rotation,
            bool syncRotation = false)
        {
            if (globalObject == null)
                throw new ArgumentNullException(nameof(globalObject));

            GlobalObjectInternal.Instance.AttachObjectToObject(Id, globalObject.Id, offset.X, offset.Y, offset.Z, rotation.X, rotation.Y,
                rotation.Z, syncRotation);
        }

        /// <summary>
        ///     Lets the specified <paramref name="player" /> edit this <see cref="PlayerObject" />.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public virtual void Edit(BasePlayer player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            GlobalObjectInternal.Instance.EditObject(player.Id, Id);
        }

        /// <summary>
        ///     Lets the specified <paramref name="player" /> select an object.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public static void Select(BasePlayer player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            GlobalObjectInternal.Instance.SelectObject(player.Id);
        }

        #endregion

        #region Event raisers

        /// <summary>
        ///     Raises the <see cref="Moved" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        public virtual void OnMoved(EventArgs e)
        {
            Moved?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Selected" /> event.
        /// </summary>
        /// <param name="e">An <see cref="SelectGlobalObjectEventArgs" /> that contains the event data. </param>
        public virtual void OnSelected(SelectGlobalObjectEventArgs e)
        {
            Selected?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Edited" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EditGlobalObjectEventArgs" /> that contains the event data. </param>
        public virtual void OnEdited(EditGlobalObjectEventArgs e)
        {
            Edited?.Invoke(this, e);
        }

        #endregion
    }
}