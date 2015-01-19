// SampSharp
// Copyright 2015 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;

namespace SampSharp.GameMode.World
{
    public class TextLabel : IdentifiedPool<TextLabel>, IIdentifiable
    {
        #region Fields

        /// <summary>
        ///     Gets an ID commonly returned by methods to point out that no TextLabel matched the requirements.
        /// </summary>
        public const int InvalidId = Misc.Invalid_3DTextId;

        private Color _color;
        private float _drawDistance;
        private Vector _position;
        private bool _testLOS;
        private string _text;
        private int _virtualWorld;

        #endregion

        #region Properties

        public virtual Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                Native.Update3DTextLabelText(Id, Color, Text);
            }
        }

        public virtual string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                Native.Update3DTextLabelText(Id, Color, Text);
            }
        }

        public virtual Vector Position
        {
            get { return _position; }
            set
            {
                _position = value;
                Dispose();
                Id = Native.Create3DTextLabel(Text, Color, Position, DrawDistance, VirtualWorld, TestLOS);
            }
        }

        public virtual float DrawDistance
        {
            get { return _drawDistance; }
            set
            {
                _drawDistance = value;
                Dispose();
                Id = Native.Create3DTextLabel(Text, Color, Position, DrawDistance, VirtualWorld, TestLOS);
            }
        }

        public virtual int VirtualWorld
        {
            get { return _virtualWorld; }
            set
            {
                _virtualWorld = value;
                Dispose();
                Id = Native.Create3DTextLabel(Text, Color, Position, DrawDistance, VirtualWorld, TestLOS);
            }
        }

        public virtual bool TestLOS
        {
            get { return _testLOS; }
            set
            {
                _testLOS = value;
                Dispose();
                Id = Native.Create3DTextLabel(Text, Color, Position, DrawDistance, VirtualWorld, TestLOS);
            }
        }

        public virtual int Id { get; private set; }

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
            Id = Native.Create3DTextLabel(text, color, position, drawDistance, virtualWorld, testLOS);
        }

        public TextLabel(string text, Color color, Vector position, float drawDistance, int virtualWorld)
            : this(text, color, position, drawDistance, virtualWorld, true)
        {
        }

        public TextLabel(string text, Color color, Vector position, float drawDistance)
            : this(text, color, position, drawDistance, -1, true)
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

            Native.Delete3DTextLabel(Id);
        }

        public virtual void AttachTo(GtaPlayer player, Vector offset)
        {
            CheckDisposure();

            if (player == null)
                throw new ArgumentNullException("player");

            Native.Attach3DTextLabelToPlayer(Id, player.Id, offset.X, offset.Y, offset.Z);
        }

        public virtual void AttachTo(GtaVehicle vehicle, Vector offset)
        {
            CheckDisposure();

            if (vehicle == null)
                throw new ArgumentNullException("vehicle");

            Native.Attach3DTextLabelToVehicle(Id, vehicle.Id, offset.X, offset.Y, offset.Z);
        }

        #endregion
    }
}