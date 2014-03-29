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
using GameMode.Definitions;
using GameMode.Events;

namespace GameMode.World
{
    public class GlobalObject : IGameObject, IDisposable
    {
        #region Fields

        public const int InvalidId = Misc.InvalidObjectId;

        #endregion

        #region Properties

        public virtual Vector Position
        {
            get { return Native.GetObjectPos(ObjectId); }
            set { Native.SetObjectPos(ObjectId, value); }
        }

        public virtual Vector Rotation
        {
            get { return Native.GetObjectRot(ObjectId); }
            set { Native.SetObjectRot(ObjectId, value); }
        }

        public virtual bool IsMoving
        {
            get { return Native.IsObjectMoving(ObjectId); }
        }

        public virtual bool IsValid
        {
            get { return Native.IsValidObject(ObjectId); }
        }

        public virtual int ModelId { get; private set; }

        public virtual float DrawDistance { get; private set; }

        public virtual int ObjectId { get; private set; }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnObjectMoved" /> callback is being called.
        ///     This callback is called when an object is moved after <see cref="Move" /> (when it stops moving).
        /// </summary>
        public event ObjectHandler Moved;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerSelectObject" /> callback is being called.
        ///     This callback is called when a player selects an object after <see cref="Native.SelectObject" /> has been used.
        /// </summary>
        public event PlayerSelectObjectHandler Selected;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerEditObject" /> callback is being called.
        ///     This callback is called when a player ends object edition mode.
        /// </summary>
        public event PlayerEditObjectHandler Edited;

        #endregion

        #region Constructor

        public GlobalObject(int modelid, Vector position, Vector rotation, float drawDistance)
        {
            ModelId = modelid;
            DrawDistance = drawDistance;

            ObjectId = Native.CreateObject(modelid, position, rotation, drawDistance);
        }

        public GlobalObject(int modelid, Vector position, Vector rotation) : this(modelid, position, rotation, 0)
        {
        }

        #endregion

        #region Methods

        public virtual void Dispose()
        {
            Native.DestroyObject(ObjectId);
        }

        public virtual void AttachTo(Player player, Vector offset, Vector rotation)
        {
            Native.AttachObjectToPlayer(ObjectId, player.PlayerId, offset, rotation);
        }

        public virtual void AttachTo(Vehicle vehicle, Vector offset, Vector rotation)
        {
            Native.AttachObjectToVehicle(ObjectId, vehicle.VehicleId, offset, rotation);
        }

        public virtual int Move(Vector position, float speed, Vector rotation)
        {
            return Native.MoveObject(ObjectId, position, speed, rotation);
        }

        public virtual int Move(Vector position, float speed)
        {
            return Native.MoveObject(ObjectId, position.X, position.Y, position.Z, speed, -1000, -1000, -1000);
        }

        public virtual void Stop()
        {
            Native.StopObject(ObjectId);
        }

        public virtual void SetMaterial(int materialindex, int modelid, string txdname, string texturename,
            Color materialcolor)
        {
            Native.SetObjectMaterial(ObjectId, materialindex, modelid, txdname, texturename,
                materialcolor.GetColorValue(ColorFormat.ARGB));
        }

        public virtual void SetMaterialText(string text, int materialindex, ObjectMaterialSize materialsize,
            string fontface, int fontsize, bool bold, Color foreColor, Color backColor,
            ObjectMaterialTextAlign textalignment)
        {
            Native.SetObjectMaterialText(ObjectId, text, materialindex, (int) materialsize, fontface, fontsize, bold,
                foreColor.GetColorValue(ColorFormat.ARGB), backColor.GetColorValue(ColorFormat.ARGB),
                (int) textalignment);
        }

        public virtual void AttachTo(GlobalObject globalObject, Vector offset, Vector rotation)
        {
            Native.AttachObjectToVehicle(ObjectId, globalObject.ObjectId, offset, rotation);
        }

        public virtual void Edit(Player player)
        {
            Native.EditObject(player.PlayerId, ObjectId);
        }

        public static void Select(Player player)
        {
            Native.SelectObject(player.PlayerId);
        }

        #endregion

        #region Event raisers

        /// <summary>
        ///     Raises the <see cref="Moved" /> event.
        /// </summary>
        /// <param name="e">An <see cref="ObjectEventArgs" /> that contains the event data. </param>
        public virtual void OnMoved(ObjectEventArgs e)
        {
            if (Moved != null)
                Moved(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Selected" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerSelectObjectEventArgs" /> that contains the event data. </param>
        public virtual void OnSelected(PlayerSelectObjectEventArgs e)
        {
            if (Selected != null)
                Selected(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Edited" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEditObjectEventArgs" /> that contains the event data. </param>
        public virtual void OnEdited(PlayerEditObjectEventArgs e)
        {
            if (Edited != null)
                Edited(this, e);
        }

        #endregion
    }
}