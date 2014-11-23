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
    public class PlayerObject : IdentifiedOwnedPool<PlayerObject>, IGameObject, IOwnable<GtaPlayer>, IIdentifiable
    {
        #region Fields

        public const int InvalidId = Misc.InvalidObjectId;

        #endregion

        #region Properties

        public virtual Vector Rotation
        {
            get { return Native.GetPlayerObjectRot(Owner.Id, Id); }
            set { Native.SetPlayerObjectRot(Owner.Id, Id, value); }
        }

        public virtual Vector Position
        {
            get { return Native.GetPlayerObjectPos(Owner.Id, Id); }
            set { Native.SetPlayerObjectPos(Owner.Id, Id, value); }
        }

        public virtual bool IsMoving
        {
            get { return Native.IsPlayerObjectMoving(Owner.Id, Id); }
        }

        public virtual bool IsValid
        {
            get { return Native.IsValidPlayerObject(Owner.Id, Id); }
        }

        public virtual int ModelId { get; private set; }

        public virtual float DrawDistance { get; private set; }
        public virtual int Id { get; private set; }
        public virtual GtaPlayer Owner { get; private set; }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnObjectMoved" /> callback is being called.
        ///     This callback is called when an object is moved after <see cref="Move(Vector,float)" /> (when it stops moving).
        /// </summary>
        public event EventHandler<PlayerObjectEventArgs> Moved;

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

        public PlayerObject(int id) : this(GtaPlayer.Find(id/(Limits.MaxObjects + 1)), id%(Limits.MaxObjects + 1))
        {
        }

        public PlayerObject(GtaPlayer owner, int id)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            Owner = owner;
            Id = id;
        }

        public PlayerObject(GtaPlayer owner, int modelid, Vector position, Vector rotation)
            : this(owner, modelid, position, rotation, 0)
        {
        }

        public PlayerObject(GtaPlayer owner, int modelid, Vector position, Vector rotation, float drawDistance)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            Owner = owner;
            ModelId = modelid;
            DrawDistance = drawDistance;

            Id = Native.CreatePlayerObject(owner.Id, modelid, position, rotation, drawDistance);
        }

        #endregion

        #region Methods

        public virtual int Move(Vector position, float speed, Vector rotation)
        {
            CheckDisposure();

            return Native.MovePlayerObject(Owner.Id, Id, position, speed, rotation);
        }

        public virtual int Move(Vector position, float speed)
        {
            CheckDisposure();

            return Native.MovePlayerObject(Owner.Id, Id, position.X, position.Y, position.Z, speed, -1000,
                -1000, -1000);
        }

        public virtual void Stop()
        {
            CheckDisposure();

            Native.StopPlayerObject(Owner.Id, Id);
        }

        public virtual void SetMaterial(int materialindex, int modelid, string txdname, string texturename,
            Color materialcolor)
        {
            CheckDisposure();

            Native.SetPlayerObjectMaterial(Owner.Id, Id, materialindex, modelid, txdname, texturename,
                materialcolor.GetColorValue(ColorFormat.ARGB));
        }

        public virtual void SetMaterialText(int materialindex, string text, ObjectMaterialSize materialsize,
            string fontface, int fontsize, bool bold, Color foreColor, Color backColor,
            ObjectMaterialTextAlign textalignment)
        {
            CheckDisposure();

            Native.SetPlayerObjectMaterialText(Owner.Id, Id, text, materialindex, (int) materialsize,
                fontface, fontsize, bold,
                foreColor.GetColorValue(ColorFormat.ARGB), backColor.GetColorValue(ColorFormat.ARGB),
                (int) textalignment);
        }

        public virtual void AttachTo(GtaPlayer player, Vector offset, Vector rotation)
        {
            CheckDisposure();

            if (player == null)
                throw new ArgumentNullException("player");

            Native.AttachPlayerObjectToPlayer(Owner.Id, Id, player.Id, offset, rotation);
        }

        public virtual void AttachTo(GtaVehicle vehicle, Vector offset, Vector rotation)
        {
            CheckDisposure();

            if (vehicle == null)
                throw new ArgumentNullException("vehicle");

            Native.AttachPlayerObjectToVehicle(Owner.Id, Id, vehicle.Id, offset, rotation);
        }

        /// <summary>
        ///     Attaches the player's camera to this PlayerObject.
        /// </summary>
        /// <remarks>
        ///     This will attach the camera of the player whose object this is to this object.
        /// </remarks>
        public virtual void AttachCameraToObject()
        {
            CheckDisposure();

            Native.AttachCameraToPlayerObject(Owner.Id, Id);
        }

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Native.DestroyPlayerObject(Owner.Id, Id);
        }

        public virtual void Edit()
        {
            CheckDisposure();

            Native.EditPlayerObject(Owner.Id, Id);
        }

        public static void Select(GtaPlayer player)
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
        public virtual void OnMoved(PlayerObjectEventArgs e)
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