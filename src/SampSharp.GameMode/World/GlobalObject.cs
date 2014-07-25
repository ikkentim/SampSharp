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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;

namespace SampSharp.GameMode.World
{
    public class GlobalObject : IdentifiedPool<GlobalObject>, IGameObject, IIdentifyable
    {
        #region Fields

        public const int InvalidId = Misc.InvalidObjectId;

        #endregion

        #region Properties

        public virtual Vector Rotation
        {
            get { return Native.GetObjectRot(Id); }
            set { Native.SetObjectRot(Id, value); }
        }

        public virtual Vector Position
        {
            get { return Native.GetObjectPos(Id); }
            set { Native.SetObjectPos(Id, value); }
        }

        public virtual bool IsMoving
        {
            get { return Native.IsObjectMoving(Id); }
        }

        public virtual bool IsValid
        {
            get { return Native.IsValidObject(Id); }
        }

        public virtual int ModelId { get; private set; }

        public virtual float DrawDistance { get; private set; }

        public virtual int Id { get; private set; }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnObjectMoved" /> callback is being called.
        ///     This callback is called when an object is moved after <see cref="Move" /> (when it stops moving).
        /// </summary>
        public event EventHandler<ObjectEventArgs> Moved;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerSelectObject" /> callback is being called.
        ///     This callback is called when a player selects an object after <see cref="Native.SelectObject" /> has been used.
        /// </summary>
        public event EventHandler<PlayerSelectObjectEventArgs> Selected;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerEditObject" /> callback is being called.
        ///     This callback is called when a player ends object edition mode.
        /// </summary>
        public event EventHandler<PlayerEditObjectEventArgs> Edited;

        #endregion

        #region Constructor

        public GlobalObject(int id)
        {
            Id = id;
        }

        public GlobalObject(int modelid, Vector position, Vector rotation, float drawDistance)
        {
            ModelId = modelid;
            DrawDistance = drawDistance;

            Id = Native.CreateObject(modelid, position, rotation, drawDistance);
        }

        public GlobalObject(int modelid, Vector position, Vector rotation) : this(modelid, position, rotation, 0)
        {
        }

        #endregion

        #region Methods

        public virtual int Move(Vector position, float speed, Vector rotation)
        {
            return Native.MoveObject(Id, position, speed, rotation);
        }

        public virtual int Move(Vector position, float speed)
        {
            return Native.MoveObject(Id, position.X, position.Y, position.Z, speed, -1000, -1000, -1000);
        }

        public virtual void Stop()
        {
            Native.StopObject(Id);
        }

        public virtual void SetMaterial(int materialindex, int modelid, string txdname, string texturename,
            Color materialcolor)
        {
            Native.SetObjectMaterial(Id, materialindex, modelid, txdname, texturename,
                materialcolor.GetColorValue(ColorFormat.ARGB));
        }

        public virtual void SetMaterialText(string text, int materialindex, ObjectMaterialSize materialsize,
            string fontface, int fontsize, bool bold, Color foreColor, Color backColor,
            ObjectMaterialTextAlign textalignment)
        {
            Native.SetObjectMaterialText(Id, text, materialindex, (int) materialsize, fontface, fontsize, bold,
                foreColor.GetColorValue(ColorFormat.ARGB), backColor.GetColorValue(ColorFormat.ARGB),
                (int) textalignment);
        }

        public virtual void AttachTo(Player player, Vector offset, Vector rotation)
        {
            if (player == null)
                throw new ArgumentNullException("player");

            Native.AttachObjectToPlayer(Id, player.Id, offset, rotation);
        }

        public virtual void AttachTo(Vehicle vehicle, Vector offset, Vector rotation)
        {
            if (vehicle == null)
                throw new ArgumentNullException("vehicle");

            Native.AttachObjectToVehicle(Id, vehicle.Id, offset, rotation);
        }

        public static void Remove(Player player, int modelid, Vector position, float radius)
        {
            if (player == null)
                throw new ArgumentNullException("player");

            Native.RemoveBuildingForPlayer(player.Id, modelid, position.X, position.Y, position.Z, radius);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Native.DestroyObject(Id);
        }

        public virtual void AttachTo(GlobalObject globalObject, Vector offset, Vector rotation,
            bool syncRotation = false)
        {
            if (globalObject == null)
                throw new ArgumentNullException("globalObject");

            Native.AttachObjectToObject(Id, globalObject.Id, offset, rotation, syncRotation);
        }

        public virtual void Edit(Player player)
        {
            if (player == null)
                throw new ArgumentNullException("player");

            Native.EditObject(player.Id, Id);
        }

        public static void Select(Player player)
        {
            if (player == null)
                throw new ArgumentNullException("player");

            Native.SelectObject(player.Id);
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