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
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;

namespace SampSharp.GameMode.World
{
    /// <summary>
    ///     Represents a 3d text label.
    /// </summary>
    public class TextLabel : IdentifiedPool<TextLabel>, IIdentifiable
    {
        /// <summary>
        ///     Identifier indicating the handle is invalid.
        /// </summary>
        public const int InvalidId = 0xFFFF;

        /// <summary>
        ///     Maximum number of per-player text labels which can exist.
        /// </summary>
        public const int Max = 1024;

        #region Fields

        private Color _color;
        private float _drawDistance;
        private Vector3 _position;
        private bool _testLOS;
        private string _text;
        private int _virtualWorld;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the color of this <see cref="TextLabel" />.
        /// </summary>
        public virtual Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                Update3DTextLabelText(Id, Color, Text);
            }
        }

        /// <summary>
        ///     Gets or sets the text of this <see cref="TextLabel" />.
        /// </summary>
        public virtual string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                Update3DTextLabelText(Id, Color, Text);
            }
        }

        /// <summary>
        ///     Gets or sets the position of this <see cref="TextLabel" />.
        /// </summary>
        public virtual Vector3 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                Dispose();
                Id = Create3DTextLabel(Text, Color, Position.X, Position.Y, Position.Z, DrawDistance,
                    VirtualWorld, TestLOS);
            }
        }

        /// <summary>
        ///     Gets or sets the draw distance of this <see cref="TextLabel" />.
        /// </summary>
        public virtual float DrawDistance
        {
            get { return _drawDistance; }
            set
            {
                _drawDistance = value;
                Dispose();
                Id = Create3DTextLabel(Text, Color, Position.X, Position.Y, Position.Z, DrawDistance,
                    VirtualWorld, TestLOS);
            }
        }

        /// <summary>
        ///     Gets or sets the virtual world of this <see cref="TextLabel" />.
        /// </summary>
        public virtual int VirtualWorld
        {
            get { return _virtualWorld; }
            set
            {
                _virtualWorld = value;
                Dispose();
                Id = Create3DTextLabel(Text, Color, Position.X, Position.Y, Position.Z, DrawDistance,
                    VirtualWorld, TestLOS);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the line of sight should be tested before drawing this
        ///     <see cref="TextLabel" />.
        /// </summary>
        public virtual bool TestLOS
        {
            get { return _testLOS; }
            set
            {
                _testLOS = value;
                Dispose();
                Id = Create3DTextLabel(Text, Color, Position.X, Position.Y, Position.Z, DrawDistance,
                    VirtualWorld, TestLOS);
            }
        }

        /// <summary>
        ///     Gets the Identity of this <see cref="TextLabel" />.
        /// </summary>
        public virtual int Id { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextLabel" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="color">The color.</param>
        /// <param name="position">The position.</param>
        /// <param name="drawDistance">The draw distance.</param>
        /// <param name="virtualWorld">The virtual world.</param>
        /// <param name="testLOS">if set to <c>true</c> the line of sight should be tested before drawing.</param>
        public TextLabel(string text, Color color, Vector3 position, float drawDistance, int virtualWorld, bool testLOS)
        {
            _text = text;
            _color = color;
            _position = position;
            _drawDistance = drawDistance;
            _virtualWorld = virtualWorld;
            _testLOS = testLOS;
            Id = Create3DTextLabel(text, color, position.X, position.Y, position.Z, drawDistance, virtualWorld,
                testLOS);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextLabel" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="color">The color.</param>
        /// <param name="position">The position.</param>
        /// <param name="drawDistance">The draw distance.</param>
        /// <param name="virtualWorld">The virtual world.</param>
        public TextLabel(string text, Color color, Vector3 position, float drawDistance, int virtualWorld)
            : this(text, color, position, drawDistance, virtualWorld, true)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextLabel" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="color">The color.</param>
        /// <param name="position">The position.</param>
        /// <param name="drawDistance">The draw distance.</param>
        public TextLabel(string text, Color color, Vector3 position, float drawDistance)
            : this(text, color, position, drawDistance, -1, true)
        {
        }

        #endregion

        #region Natives

        private delegate bool Attach3DTextLabelToPlayerImpl(
            int id, int playerid, float offsetX, float offsetY, float offsetZ);

        private delegate bool Attach3DTextLabelToVehicleImpl(
            int id, int vehicleid, float offsetX, float offsetY, float offsetZ);

        private delegate int Create3DTextLabelImpl(
            string text, int color, float x, float y, float z, float drawDistance, int virtualWorld, bool testLOS);

        private delegate bool Update3DTextLabelTextImpl(int id, int color, string text);

        private delegate bool Delete3DTextLabelImpl(int id);

        [Native("Create3DTextLabel")]
        private static readonly Create3DTextLabelImpl Create3DTextLabel = null;
        [Native("Delete3DTextLabel")]
        private static readonly Delete3DTextLabelImpl Delete3DTextLabel = null;

        [Native("Attach3DTextLabelToPlayer")]
        private static readonly Attach3DTextLabelToPlayerImpl
            Attach3DTextLabelToPlayer = null;

        [Native("Attach3DTextLabelToVehicle")]
        private static readonly Attach3DTextLabelToVehicleImpl
            Attach3DTextLabelToVehicle = null;

        [Native("Update3DTextLabelText")]
        private static readonly Update3DTextLabelTextImpl Update3DTextLabelText = null;

        #endregion

        #region Methods

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Delete3DTextLabel(Id);
        }

        /// <summary>
        ///     Attaches this <see cref="TextLabel" /> to the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="offset">The offset.</param>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public virtual void AttachTo(GtaPlayer player, Vector3 offset)
        {
            AssertNotDisposed();

            if (player == null)
                throw new ArgumentNullException("player");

            Attach3DTextLabelToPlayer(Id, player.Id, offset.X, offset.Y, offset.Z);
        }

        /// <summary>
        ///     Attaches this <see cref="TextLabel" /> to the specified <paramref name="vehicle" />.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        /// <param name="offset">The offset.</param>
        /// <exception cref="System.ArgumentNullException">vehicle</exception>
        public virtual void AttachTo(GtaVehicle vehicle, Vector3 offset)
        {
            AssertNotDisposed();

            if (vehicle == null)
                throw new ArgumentNullException("vehicle");

            Attach3DTextLabelToVehicle(Id, vehicle.Id, offset.X, offset.Y, offset.Z);
        }

        #endregion
    }
}