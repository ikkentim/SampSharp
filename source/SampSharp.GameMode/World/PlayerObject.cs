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

using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Exceptions;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.SAMP;

namespace SampSharp.GameMode.World
{
    public class PlayerObject : InstanceKeeper<PlayerObject>, IGameObject
    {
        #region Fields

        public const int InvalidId = Misc.InvalidObjectId;

        #endregion

        #region Properties

        public virtual Player Player { get; private set; }
        public virtual int ObjectId { get; private set; }

        public virtual Vector Position
        {
            get { return Native.GetPlayerObjectPos(Player.Id, ObjectId); }
            set { Native.SetPlayerObjectPos(Player.Id, ObjectId, value); }
        }

        public virtual Vector Rotation
        {
            get { return Native.GetPlayerObjectRot(Player.Id, ObjectId); }
            set { Native.SetPlayerObjectRot(Player.Id, ObjectId, value); }
        }

        public virtual bool IsMoving
        {
            get { return Native.IsPlayerObjectMoving(Player.Id, ObjectId); }
        }

        public virtual bool IsValid
        {
            get { return Native.IsValidPlayerObject(Player.Id, ObjectId); }
        }

        public virtual int ModelId { get; private set; }

        public virtual float DrawDistance { get; private set; }

        public virtual int Id
        {
            get { return Player.Id*(Limits.MaxObjects + 1) + ObjectId; }
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnObjectMoved" /> callback is being called.
        ///     This callback is called when an object is moved after <see cref="Move" /> (when it stops moving).
        /// </summary>
        public event PlayerObjectHandler Moved;

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

        public PlayerObject(int id) : this(Player.Find(id/(Limits.MaxObjects + 1)), id%(Limits.MaxObjects + 1))
        {
        }

        public PlayerObject(Player player, int id)
        {
            if (player == null)
                throw new PlayerNotConnectedException();

            Player = player;
            ObjectId = id;
        }

        public PlayerObject(Player player, int modelid, Vector position, Vector rotation)
            : this(player, modelid, position, rotation, 0)
        {
        }

        public PlayerObject(Player player, int modelid, Vector position, Vector rotation, float drawDistance)
        {
            if (player == null)
                throw new PlayerNotConnectedException();

            Player = player;
            ModelId = modelid;
            DrawDistance = drawDistance;

            ObjectId = Native.CreatePlayerObject(player.Id, modelid, position, rotation, drawDistance);
        }

        #endregion

        #region Methods

        public virtual void AttachTo(Player player, Vector offset, Vector rotation)
        {
            Native.AttachPlayerObjectToPlayer(Player.Id, ObjectId, player.Id, offset, rotation);
        }

        public virtual void AttachTo(Vehicle vehicle, Vector offset, Vector rotation)
        {
            Native.AttachPlayerObjectToVehicle(Player.Id, ObjectId, vehicle.Id, offset, rotation);
        }

        public virtual int Move(Vector position, float speed, Vector rotation)
        {
            return Native.MovePlayerObject(Player.Id, ObjectId, position, speed, rotation);
        }

        public virtual int Move(Vector position, float speed)
        {
            return Native.MovePlayerObject(Player.Id, ObjectId, position.X, position.Y, position.Z, speed, -1000,
                -1000, -1000);
        }

        public virtual void Stop()
        {
            Native.StopPlayerObject(Player.Id, ObjectId);
        }

        public virtual void SetMaterial(int materialindex, int modelid, string txdname, string texturename,
            Color materialcolor)
        {
            Native.SetPlayerObjectMaterial(Player.Id, ObjectId, materialindex, modelid, txdname, texturename,
                materialcolor.GetColorValue(ColorFormat.ARGB));
        }

        public virtual void SetMaterialText(string text, int materialindex, ObjectMaterialSize materialsize,
            string fontface, int fontsize, bool bold, Color foreColor, Color backColor,
            ObjectMaterialTextAlign textalignment)
        {
            Native.SetPlayerObjectMaterialText(Player.Id, ObjectId, text, materialindex, (int) materialsize,
                fontface, fontsize, bold,
                foreColor.GetColorValue(ColorFormat.ARGB), backColor.GetColorValue(ColorFormat.ARGB),
                (int) textalignment);
        }

        public override void Dispose()
        {
            Native.DestroyObject(ObjectId);

            base.Dispose();
        }

        public virtual void Edit()
        {
            Native.EditPlayerObject(Player.Id, ObjectId);
        }

        public static void Select(Player player)
        {
            Native.SelectObject(player.Id);
        }

        public static PlayerObject Find(Player player, int id)
        {
            return Find(player.Id*(Limits.MaxObjects + 1) + id);
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