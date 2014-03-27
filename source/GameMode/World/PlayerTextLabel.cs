using System;
using GameMode.Exceptions;

namespace GameMode.World
{
    public class PlayerTextLabel : IDisposable
    {

        #region Fields

        private Color _color;
        private string _text;
        private Vector _position;
        private float _drawDistance;
        private bool _testLOS;
        private Player _attachedPlayer;
        private Vehicle _attachedVehicle;

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
