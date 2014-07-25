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
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;
using SampSharp.Streamer.Definitions;
using SampSharp.Streamer.Natives;

namespace SampSharp.Streamer.World
{
    public class DynamicObject : DynamicWorldObject<DynamicObject>, IGameObject
    {
        #region Fields

        public const int InvalidId = Misc.InvalidObjectId;

        #endregion

        #region Constructor

        public DynamicObject(int id)
        {
            Id = id;
        }

        public DynamicObject(int modelid, Vector position, Vector rotation, float drawDistance)
        {
            ModelId = modelid;
            DrawDistance = drawDistance;

            Id = Native.CreateObject(modelid, position, rotation, drawDistance);
        }

        public DynamicObject(int modelid, Vector position, Vector rotation)
            : this(modelid, position, rotation, 0)
        {
        }

        #endregion

        #region Properties

        public virtual Vector Rotation
        {
            get { return StreamerNative.GetDynamicObjectRot(Id); }
            set { StreamerNative.SetDynamicObjectRot(Id, value); }
        }

        public override StreamType StreamType
        {
            get { return StreamType.Object; }
        }

        public override Vector Position
        {
            get { return StreamerNative.GetDynamicObjectPos(Id); }
            set { StreamerNative.SetDynamicObjectPos(Id, value); }
        }

        public virtual bool IsMoving
        {
            get { return StreamerNative.IsDynamicObjectMoving(Id); }
        }

        public virtual bool IsValid
        {
            get { return StreamerNative.IsValidDynamicObject(Id); }
        }

        public virtual int ModelId { get; private set; }

        public virtual float DrawDistance { get; private set; }

        #endregion

        #region Events

        /*
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
        */

        #endregion

        #region Methods

        public virtual int Move(Vector position, float speed, Vector rotation)
        {
            return StreamerNative.MoveDynamicObject(Id, position, speed, rotation);
        }

        public virtual int Move(Vector position, float speed)
        {
            return StreamerNative.MoveDynamicObject(Id, position, speed);
        }

        public virtual void Stop()
        {
            StreamerNative.StopDynamicObject(Id);
        }

        public virtual void SetMaterial(int materialindex, int modelid, string txdname, string texturename,
            Color materialcolor)
        {
            StreamerNative.SetDynamicObjectMaterial(Id, materialindex, modelid, txdname, texturename,
                materialcolor.GetColorValue(ColorFormat.ARGB));
        }

        public virtual void SetMaterialText(string text, int materialindex, ObjectMaterialSize materialsize,
            string fontface, int fontsize, bool bold, Color foreColor, Color backColor,
            ObjectMaterialTextAlign textalignment)
        {
            StreamerNative.SetDynamicObjectMaterialText(Id, materialindex, text, materialsize, fontface, fontsize, bold,
                foreColor.GetColorValue(ColorFormat.ARGB), backColor.GetColorValue(ColorFormat.ARGB),
                textalignment);
        }

        public virtual void AttachTo(Vehicle vehicle, Vector offset, Vector rotation)
        {
            if (vehicle == null)
                throw new NullReferenceException("vehicle cannot be null");

            StreamerNative.AttachDynamicObjectToVehicle(Id, vehicle.Id, offset, rotation);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            StreamerNative.DestroyDynamicObject(Id);
        }

        public virtual void Edit(Player player)
        {
            if (player == null)
                throw new NullReferenceException("player cannot be null");

            StreamerNative.EditDynamicObject(player.Id, Id);
        }

        public static void Select(Player player)
        {
            if (player == null)
                throw new NullReferenceException("player cannot be null");

            Native.SelectObject(player.Id);
        }

        #endregion

        #region Event raisers

        /*
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
        */

        #endregion
    }
}