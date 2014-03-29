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
using GameMode.Exceptions;

namespace GameMode.World
{
    public class PlayerTextLabel : IDisposable
    {
        #region Fields

        /// <summary>
        ///     Gets an ID commonly returned by methods to point out that no PlayerTextLabel matched the requirements.
        /// </summary>
        public const int InvalidId = Misc.Invalid_3DTextId;

        private Player _attachedPlayer;
        private Vehicle _attachedVehicle;

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
                Native.UpdatePlayer3DTextLabelText(Player.PlayerId, LabelId, Color, Text);
            }
        }

        public virtual string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                Native.UpdatePlayer3DTextLabelText(Player.PlayerId, LabelId, Color, Text);
            }
        }

        public virtual Vector Position
        {
            get { return _position; }
            set
            {
                _position = value;
                Dispose();
                LabelId = Native.CreatePlayer3DTextLabel(Player.PlayerId, Text, Color, Position, DrawDistance,
                    AttachedPlayer == null ? Player.InvalidId : AttachedPlayer.PlayerId,
                    AttachedVehicle == null ? Vehicle.InvalidId : AttachedVehicle.VehicleId, TestLOS);
            }
        }

        public virtual float DrawDistance
        {
            get { return _drawDistance; }
            set
            {
                _drawDistance = value;
                Dispose();
                LabelId = Native.CreatePlayer3DTextLabel(Player.PlayerId, Text, Color, Position, DrawDistance,
                    AttachedPlayer == null ? Player.InvalidId : AttachedPlayer.PlayerId,
                    AttachedVehicle == null ? Vehicle.InvalidId : AttachedVehicle.VehicleId, TestLOS);
            }
        }

        public virtual bool TestLOS
        {
            get { return _testLOS; }
            set
            {
                _testLOS = value;
                Dispose();
                LabelId = Native.CreatePlayer3DTextLabel(Player.PlayerId, Text, Color, Position, DrawDistance,
                    AttachedPlayer == null ? Player.InvalidId : AttachedPlayer.PlayerId,
                    AttachedVehicle == null ? Vehicle.InvalidId : AttachedVehicle.VehicleId, TestLOS);
            }
        }

        public virtual Player AttachedPlayer
        {
            get { return _attachedPlayer; }
            set
            {
                _attachedPlayer = value;
                Dispose();
                LabelId = Native.CreatePlayer3DTextLabel(Player.PlayerId, Text, Color, Position, DrawDistance,
                    AttachedPlayer == null ? Player.InvalidId : AttachedPlayer.PlayerId,
                    AttachedVehicle == null ? Vehicle.InvalidId : AttachedVehicle.VehicleId, TestLOS);
            }
        }

        public virtual Vehicle AttachedVehicle
        {
            get { return _attachedVehicle; }
            set
            {
                _attachedVehicle = value;
                Dispose();
                LabelId = Native.CreatePlayer3DTextLabel(Player.PlayerId, Text, Color, Position, DrawDistance,
                    AttachedPlayer == null ? Player.InvalidId : AttachedPlayer.PlayerId,
                    AttachedVehicle == null ? Vehicle.InvalidId : AttachedVehicle.VehicleId, TestLOS);
            }
        }

        public virtual int LabelId { get; private set; }

        public virtual Player Player { get; private set; }

        #endregion

        #region Constructors

        public PlayerTextLabel(Player player, string text, Color color, Vector position, float drawDistance,
            bool testLOS)
        {
            if (player == null)
                throw new PlayerNotConnectedException();

            Player = player;
            _color = color;
            _position = position;
            _drawDistance = drawDistance;
            _testLOS = testLOS;

            LabelId = Native.CreatePlayer3DTextLabel(player.PlayerId, text, color, position, drawDistance,
                Player.InvalidId, Vehicle.InvalidId, testLOS);
        }

        public PlayerTextLabel(Player player, string text, Color color, Vector position, float drawDistance)
            : this(player, text, color, position, drawDistance, true)
        {
        }

        public PlayerTextLabel(Player player, string text, Color color, Vector position, float drawDistance,
            bool testLOS, Player attachedPlayer)
        {
            if (player == null)
                throw new PlayerNotConnectedException();

            if (attachedPlayer == null)
                throw new PlayerNotConnectedException();

            Player = player;
            _color = color;
            _position = position;
            _drawDistance = drawDistance;
            _testLOS = testLOS;

            LabelId = Native.CreatePlayer3DTextLabel(player.PlayerId, text, color, position, drawDistance,
                attachedPlayer.PlayerId, Vehicle.InvalidId, testLOS);
        }

        public PlayerTextLabel(Player player, string text, Color color, Vector position, float drawDistance,
            Player attachedPlayer) : this(player, text, color, position, drawDistance, true, attachedPlayer)
        {
        }

        public PlayerTextLabel(Player player, string text, Color color, Vector position, float drawDistance,
            bool testLOS, Vehicle attachedVehicle)
        {
            if (player == null)
                throw new PlayerNotConnectedException();

            if (attachedVehicle == null)
                throw new VehicleDoesNotExistException();

            Player = player;
            _color = color;
            _position = position;
            _drawDistance = drawDistance;
            _testLOS = testLOS;

            LabelId = Native.CreatePlayer3DTextLabel(player.PlayerId, text, color, position, drawDistance,
                Player.InvalidId, attachedVehicle.VehicleId, testLOS);
        }

        public PlayerTextLabel(Player player, string text, Color color, Vector position, float drawDistance,
            Vehicle attachedVehicle)
            : this(player, text, color, position, drawDistance, true, attachedVehicle)
        {
        }

        #endregion

        #region Methods

        public virtual void Dispose()
        {
            Native.DeletePlayer3DTextLabel(Player.PlayerId, LabelId);
        }

        #endregion
    }
}