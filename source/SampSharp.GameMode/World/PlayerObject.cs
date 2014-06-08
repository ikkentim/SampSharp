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
    public class PlayerObject : IdentifiedOwnedPool<PlayerObject>, IGameObject, IOwnable, IIdentifyable
    {
        #region Fields

        public const int InvalidId = Misc.InvalidObjectId;

        #endregion

        #region Properties

        public virtual int Id { get; private set; }

        public virtual Vector Position
        {
            get { return Native.GetPlayerObjectPos(Player.Id, Id); }
            set { Native.SetPlayerObjectPos(Player.Id, Id, value); }
        }

        public virtual Vector Rotation
        {
            get { return Native.GetPlayerObjectRot(Player.Id, Id); }
            set { Native.SetPlayerObjectRot(Player.Id, Id, value); }
        }

        public virtual bool IsMoving
        {
            get { return Native.IsPlayerObjectMoving(Player.Id, Id); }
        }

        public virtual bool IsValid
        {
            get { return Native.IsValidPlayerObject(Player.Id, Id); }
        }

        public virtual int ModelId { get; private set; }

        public virtual float DrawDistance { get; private set; }
        public virtual Player Player { get; private set; }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnObjectMoved" /> callback is being called.
        ///     This callback is called when an object is moved after <see cref="Move" /> (when it stops moving).
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

        public PlayerObject(int id) : this(Player.Find(id/(Limits.MaxObjects + 1)), id%(Limits.MaxObjects + 1))
        {
        }

        public PlayerObject(Player player, int id)
        {
            if (player == null)
                throw new NullReferenceException("player cannot be null");

            Player = player;
            Id = id;
        }

        public PlayerObject(Player player, int modelid, Vector position, Vector rotation)
            : this(player, modelid, position, rotation, 0)
        {
        }

        public PlayerObject(Player player, int modelid, Vector position, Vector rotation, float drawDistance)
        {
            if (player == null)
                throw new NullReferenceException("player cannot be null");

            Player = player;
            ModelId = modelid;
            DrawDistance = drawDistance;

            Id = Native.CreatePlayerObject(player.Id, modelid, position, rotation, drawDistance);
        }

        #endregion

        #region Methods

        public virtual void AttachTo(Player player, Vector offset, Vector rotation)
        {
            CheckDisposure();

            if (player == null)
                throw new NullReferenceException("player cannot be null");

            Native.AttachPlayerObjectToPlayer(Player.Id, Id, player.Id, offset, rotation);
        }

        public virtual void AttachTo(Vehicle vehicle, Vector offset, Vector rotation)
        {
            CheckDisposure();

            if (vehicle == null)
                throw new NullReferenceException("vehicle cannot be null");

            Native.AttachPlayerObjectToVehicle(Player.Id, Id, vehicle.Id, offset, rotation);
        }

        public virtual int Move(Vector position, float speed, Vector rotation)
        {
            CheckDisposure();

            return Native.MovePlayerObject(Player.Id, Id, position, speed, rotation);
        }

        public virtual int Move(Vector position, float speed)
        {
            CheckDisposure();

            return Native.MovePlayerObject(Player.Id, Id, position.X, position.Y, position.Z, speed, -1000,
                -1000, -1000);
        }

        public virtual void Stop()
        {
            CheckDisposure();

            Native.StopPlayerObject(Player.Id, Id);
        }

        public virtual void SetMaterial(int materialindex, int modelid, string txdname, string texturename,
            Color materialcolor)
        {
            CheckDisposure();

            Native.SetPlayerObjectMaterial(Player.Id, Id, materialindex, modelid, txdname, texturename,
                materialcolor.GetColorValue(ColorFormat.ARGB));
        }

        public virtual void SetMaterialText(string text, int materialindex, ObjectMaterialSize materialsize,
            string fontface, int fontsize, bool bold, Color foreColor, Color backColor,
            ObjectMaterialTextAlign textalignment)
        {
            CheckDisposure();

            Native.SetPlayerObjectMaterialText(Player.Id, Id, text, materialindex, (int) materialsize,
                fontface, fontsize, bold,
                foreColor.GetColorValue(ColorFormat.ARGB), backColor.GetColorValue(ColorFormat.ARGB),
                (int) textalignment);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Native.DestroyPlayerObject(Player.Id, Id);
        }

        public virtual void Edit()
        {
            CheckDisposure();

            Native.EditPlayerObject(Player.Id, Id);
        }

        public static void Select(Player player)
        {
            if (player == null)
                throw new NullReferenceException("player cannot be null");

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