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
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP
{
    public class PlayerTextLabel : IdentifiedOwnedPool<PlayerTextLabel>, IIDentifiable, IOwnable
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
                Native.UpdatePlayer3DTextLabelText(Player.Id, Id, Color, Text);
            }
        }

        public virtual string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                Native.UpdatePlayer3DTextLabelText(Player.Id, Id, Color, Text);
            }
        }

        public virtual Vector Position
        {
            get { return _position; }
            set
            {
                _position = value;
                Dispose();
                Id = Native.CreatePlayer3DTextLabel(Player.Id, Text, Color, Position, DrawDistance,
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
                Id = Native.CreatePlayer3DTextLabel(Player.Id, Text, Color, Position, DrawDistance,
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
                Id = Native.CreatePlayer3DTextLabel(Player.Id, Text, Color, Position, DrawDistance,
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
                Id = Native.CreatePlayer3DTextLabel(Player.Id, Text, Color, Position, DrawDistance,
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
                Id = Native.CreatePlayer3DTextLabel(Player.Id, Text, Color, Position, DrawDistance,
                    AttachedPlayer == null ? GtaPlayer.InvalidId : AttachedPlayer.Id,
                    AttachedVehicle == null ? GtaVehicle.InvalidId : AttachedVehicle.Id, TestLOS);
            }
        }

        public virtual int Id { get; private set; }
        public virtual GtaPlayer Player { get; private set; }

        #endregion

        #region Constructors

        public PlayerTextLabel(GtaPlayer player, int id)
        {
            if (player == null)
                throw new ArgumentNullException("player");

            Player = player;
            Id = id;
        }

        public PlayerTextLabel(GtaPlayer player, string text, Color color, Vector position, float drawDistance,
            bool testLOS)
        {
            if (player == null)
                throw new ArgumentNullException("player");

            Player = player;
            _color = color;
            _position = position;
            _drawDistance = drawDistance;
            _testLOS = testLOS;

            Id = Native.CreatePlayer3DTextLabel(player.Id, text, color, position, drawDistance,
                GtaPlayer.InvalidId, GtaVehicle.InvalidId, testLOS);
        }

        public PlayerTextLabel(GtaPlayer player, string text, Color color, Vector position, float drawDistance)
            : this(player, text, color, position, drawDistance, true)
        {
        }

        public PlayerTextLabel(GtaPlayer player, string text, Color color, Vector position, float drawDistance,
            bool testLOS, GtaPlayer attachedPlayer)
        {
            if (player == null)
                throw new ArgumentNullException("player");

            if (attachedPlayer == null)
                throw new ArgumentNullException("attachedPlayer");

            Player = player;
            _color = color;
            _position = position;
            _drawDistance = drawDistance;
            _testLOS = testLOS;

            Id = Native.CreatePlayer3DTextLabel(player.Id, text, color, position, drawDistance,
                attachedPlayer.Id, GtaVehicle.InvalidId, testLOS);
        }

        public PlayerTextLabel(GtaPlayer player, string text, Color color, Vector position, float drawDistance,
            GtaPlayer attachedPlayer) : this(player, text, color, position, drawDistance, true, attachedPlayer)
        {
        }

        public PlayerTextLabel(GtaPlayer player, string text, Color color, Vector position, float drawDistance,
            bool testLOS, GtaVehicle attachedVehicle)
        {
            if (player == null)
                throw new ArgumentNullException("player");

            if (attachedVehicle == null)
                throw new ArgumentNullException("attachedVehicle");

            Player = player;
            _color = color;
            _position = position;
            _drawDistance = drawDistance;
            _testLOS = testLOS;

            Id = Native.CreatePlayer3DTextLabel(player.Id, text, color, position, drawDistance,
                GtaPlayer.InvalidId, attachedVehicle.Id, testLOS);
        }

        public PlayerTextLabel(GtaPlayer player, string text, Color color, Vector position, float drawDistance,
            GtaVehicle attachedVehicle)
            : this(player, text, color, position, drawDistance, true, attachedVehicle)
        {
        }

        #endregion

        #region Methods

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Native.DeletePlayer3DTextLabel(Player.Id, Id);
        }

        #endregion
    }
}