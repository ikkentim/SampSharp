// SampSharp
// Copyright (C) 2015 Tim Potze
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
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP
{
    public class PlayerTextLabel : IdentifiedOwnedPool<PlayerTextLabel>, IIdentifiable, IOwnable<GtaPlayer>
    {
        #region Fields

        /// <summary>
        ///     Gets an ID commonly returned by methods to point out that no PlayerTextLabel matched the requirements.
        /// </summary>
        public const int InvalidId = Misc.Invalid_3DTextId;

        private GtaPlayer _attachedPlayer;
        private GtaVehicle _attachedVehicle;

        private Color _color;
        private float _drawDistance;
        private Vector _position;
        private bool _testLOS;
        private string _text;

        #endregion

        #region Properties

        public virtual Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                Native.UpdatePlayer3DTextLabelText(Owner.Id, Id, Color, Text);
            }
        }

        public virtual string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                Native.UpdatePlayer3DTextLabelText(Owner.Id, Id, Color, Text);
            }
        }

        public virtual Vector Position
        {
            get { return _position; }
            set
            {
                _position = value;
                Dispose();
                Id = Native.CreatePlayer3DTextLabel(Owner.Id, Text, Color, Position, DrawDistance,
                    AttachedPlayer == null ? GtaPlayer.InvalidId : AttachedPlayer.Id,
                    AttachedVehicle == null ? GtaVehicle.InvalidId : AttachedVehicle.Id, TestLOS);
            }
        }

        public virtual float DrawDistance
        {
            get { return _drawDistance; }
            set
            {
                _drawDistance = value;
                Dispose();
                Id = Native.CreatePlayer3DTextLabel(Owner.Id, Text, Color, Position, DrawDistance,
                    AttachedPlayer == null ? GtaPlayer.InvalidId : AttachedPlayer.Id,
                    AttachedVehicle == null ? GtaVehicle.InvalidId : AttachedVehicle.Id, TestLOS);
            }
        }

        public virtual bool TestLOS
        {
            get { return _testLOS; }
            set
            {
                _testLOS = value;
                Dispose();
                Id = Native.CreatePlayer3DTextLabel(Owner.Id, Text, Color, Position, DrawDistance,
                    AttachedPlayer == null ? GtaPlayer.InvalidId : AttachedPlayer.Id,
                    AttachedVehicle == null ? GtaVehicle.InvalidId : AttachedVehicle.Id, TestLOS);
            }
        }

        public virtual GtaPlayer AttachedPlayer
        {
            get { return _attachedPlayer; }
            set
            {
                _attachedPlayer = value;
                Dispose();
                Id = Native.CreatePlayer3DTextLabel(Owner.Id, Text, Color, Position, DrawDistance,
                    AttachedPlayer == null ? GtaPlayer.InvalidId : AttachedPlayer.Id,
                    AttachedVehicle == null ? GtaVehicle.InvalidId : AttachedVehicle.Id, TestLOS);
            }
        }

        public virtual GtaVehicle AttachedVehicle
        {
            get { return _attachedVehicle; }
            set
            {
                _attachedVehicle = value;
                Dispose();
                Id = Native.CreatePlayer3DTextLabel(Owner.Id, Text, Color, Position, DrawDistance,
                    AttachedPlayer == null ? GtaPlayer.InvalidId : AttachedPlayer.Id,
                    AttachedVehicle == null ? GtaVehicle.InvalidId : AttachedVehicle.Id, TestLOS);
            }
        }

        public virtual int Id { get; private set; }
        public virtual GtaPlayer Owner { get; private set; }

        #endregion

        #region Constructors

        public PlayerTextLabel(GtaPlayer owner, int id)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            Owner = owner;
            Id = id;
        }

        public PlayerTextLabel(GtaPlayer owner, string text, Color color, Vector position, float drawDistance,
            bool testLOS)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            Owner = owner;
            _color = color;
            _position = position;
            _drawDistance = drawDistance;
            _testLOS = testLOS;

            Id = Native.CreatePlayer3DTextLabel(owner.Id, text, color, position, drawDistance,
                GtaPlayer.InvalidId, GtaVehicle.InvalidId, testLOS);
        }

        public PlayerTextLabel(GtaPlayer owner, string text, Color color, Vector position, float drawDistance)
            : this(owner, text, color, position, drawDistance, true)
        {
        }

        public PlayerTextLabel(GtaPlayer owner, string text, Color color, Vector position, float drawDistance,
            bool testLOS, GtaPlayer attachedPlayer)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            if (attachedPlayer == null)
                throw new ArgumentNullException("attachedPlayer");

            Owner = owner;
            _color = color;
            _position = position;
            _drawDistance = drawDistance;
            _testLOS = testLOS;

            Id = Native.CreatePlayer3DTextLabel(owner.Id, text, color, position, drawDistance,
                attachedPlayer.Id, GtaVehicle.InvalidId, testLOS);
        }

        public PlayerTextLabel(GtaPlayer owner, string text, Color color, Vector position, float drawDistance,
            GtaPlayer attachedPlayer) : this(owner, text, color, position, drawDistance, true, attachedPlayer)
        {
        }

        public PlayerTextLabel(GtaPlayer owner, string text, Color color, Vector position, float drawDistance,
            bool testLOS, GtaVehicle attachedVehicle)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            if (attachedVehicle == null)
                throw new ArgumentNullException("attachedVehicle");

            Owner = owner;
            _color = color;
            _position = position;
            _drawDistance = drawDistance;
            _testLOS = testLOS;

            Id = Native.CreatePlayer3DTextLabel(owner.Id, text, color, position, drawDistance,
                GtaPlayer.InvalidId, attachedVehicle.Id, testLOS);
        }

        public PlayerTextLabel(GtaPlayer owner, string text, Color color, Vector position, float drawDistance,
            GtaVehicle attachedVehicle)
            : this(owner, text, color, position, drawDistance, true, attachedVehicle)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Native.DeletePlayer3DTextLabel(Owner.Id, Id);
        }

        #endregion
    }
}