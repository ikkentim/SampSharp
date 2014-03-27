using System;
using GameMode.Definitions;

namespace GameMode.World
{
    public class TextLabel : IDisposable
    {

        #region Fields

        private Color _color;
        private string _text;
        private Vector _position;
        private float _drawDistance;
        private int _virtualWorld;
        private bool _testLOS;

        /// <summary>
        /// Gets an ID commonly returned by methods to point out that no TextLabel matched the requirements.
        /// </summary>
        public const int InvalidId = Misc.Invalid_3DTextId;

        #endregion

        #region Properties

        public virtual Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                Native.Update3DTextLabelText(LabelId, Color, Text);
            }
        }

        public virtual string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                Native.Update3DTextLabelText(LabelId, Color, Text);
            }
        }

        public virtual Vector Position
        {
            get { return _position; }
            set
            {
                _position = value;
                Dispose();
                LabelId = Native.Create3DTextLabel(Text, Color, Position, DrawDistance, VirtualWorld, TestLOS);
            }
        }

        public virtual float DrawDistance
        {
            get { return _drawDistance; }
            set
            {
                _drawDistance = value;
                Dispose();
                LabelId = Native.Create3DTextLabel(Text, Color, Position, DrawDistance, VirtualWorld, TestLOS);
            }
        }

        public virtual int VirtualWorld
        {
            get { return _virtualWorld; }
            set
            {
                _virtualWorld = value;
                Dispose();
                LabelId = Native.Create3DTextLabel(Text, Color, Position, DrawDistance, VirtualWorld, TestLOS);
            }
        }

        public virtual bool TestLOS
        {
            get { return _testLOS; }
            set
            {
                _testLOS = value;
                Dispose();
                LabelId = Native.Create3DTextLabel(Text, Color, Position, DrawDistance, VirtualWorld, TestLOS);
            }
        }

        public virtual int LabelId { get; private set; }

        #endregion

        #region Constructors

        public TextLabel(string text, Color color, Vector position, float drawDistance, int virtualWorld, bool testLOS)
        {
            _text = text;
            _color = color;
            _position = position;
            _drawDistance = drawDistance;
            _virtualWorld = virtualWorld;
            _testLOS = testLOS;
            LabelId = Native.Create3DTextLabel(text, color, position, drawDistance, virtualWorld, testLOS);
        }

        public TextLabel(string text, Color color, Vector position, float drawDistance, int virtualWorld) : this(text,color,position,drawDistance,virtualWorld,true)
        {
            
        }

        public TextLabel(string text, Color color, Vector position, float drawDistance)
            : this(text, color, position, drawDistance, -1, true)
        {

        }

        #endregion

        #region Methods

        public virtual void AttachToPlayer(Player player, Vector offset)
        {
            if (player == null) return;
            Native.Attach3DTextLabelToPlayer(LabelId, player.PlayerId, offset.X, offset.Y, offset.Z);
        }

        public virtual void AttachToVehicle(Vehicle vehicle, Vector offset)
        {
            if (vehicle == null) return;
            Native.Attach3DTextLabelToVehicle(LabelId, vehicle.VehicleId, offset.X, offset.Y, offset.Z);
        }

        public virtual void Dispose()
        {
            Native.Delete3DTextLabel(LabelId);
        }

        #endregion

    }
}
