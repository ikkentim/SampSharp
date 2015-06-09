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
    ///     Represents a player text label.
    /// </summary>
    public class PlayerTextLabel : IdentifiedOwnedPool<PlayerTextLabel>, IIdentifiable, IOwnable<GtaPlayer>
    {
        /// <summary>
        ///     Identifier indicating the handle is invalid.
        /// </summary>
        public const int InvalidId = 0xFFFF;

        /// <summary>
        ///     Maximum number of per-player text labels which can exist.
        /// </summary>
        public const int Max = 1024;

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

        #region Fields

        private GtaPlayer _attachedPlayer;
        private GtaVehicle _attachedVehicle;

        private Color _color;
        private float _drawDistance;
        private Vector3 _position;
        private bool _testLOS;
        private string _text;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the color of this <see cref="PlayerTextLabel" />.
        /// </summary>
        public virtual Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                Native.UpdatePlayer3DTextLabelText(Owner.Id, Id, Color, Text);
            }
        }

        /// <summary>
        ///     Gets or sets the text of this <see cref="PlayerTextLabel" />.
        /// </summary>
        public virtual string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                Native.UpdatePlayer3DTextLabelText(Owner.Id, Id, Color, Text);
            }
        }

        /// <summary>
        ///     Gets or sets the position of this <see cref="PlayerTextLabel" />.
        /// </summary>
        public virtual Vector3 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                Dispose();
                Id = Native.CreatePlayer3DTextLabel(Owner.Id, Text, Color, Position.X, Position.Y, Position.Z,
                    DrawDistance,
                    AttachedPlayer == null ? GtaPlayer.InvalidId : AttachedPlayer.Id,
                    AttachedVehicle == null ? GtaVehicle.InvalidId : AttachedVehicle.Id, TestLOS);
            }
        }

        /// <summary>
        ///     Gets or sets the draw distance.
        /// </summary>
        public virtual float DrawDistance
        {
            get { return _drawDistance; }
            set
            {
                _drawDistance = value;
                Dispose();
                Id = Native.CreatePlayer3DTextLabel(Owner.Id, Text, Color, Position.X, Position.Y, Position.Z,
                    DrawDistance,
                    AttachedPlayer == null ? GtaPlayer.InvalidId : AttachedPlayer.Id,
                    AttachedVehicle == null ? GtaVehicle.InvalidId : AttachedVehicle.Id, TestLOS);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether to test the line of sight.
        /// </summary>
        public virtual bool TestLOS
        {
            get { return _testLOS; }
            set
            {
                _testLOS = value;
                Dispose();
                Id = Native.CreatePlayer3DTextLabel(Owner.Id, Text, Color, Position.X, Position.Y, Position.Z,
                    DrawDistance,
                    AttachedPlayer == null ? GtaPlayer.InvalidId : AttachedPlayer.Id,
                    AttachedVehicle == null ? GtaVehicle.InvalidId : AttachedVehicle.Id, TestLOS);
            }
        }

        /// <summary>
        ///     Gets or sets the attached player.
        /// </summary>
        public virtual GtaPlayer AttachedPlayer
        {
            get { return _attachedPlayer; }
            set
            {
                _attachedPlayer = value;
                Dispose();
                Id = Native.CreatePlayer3DTextLabel(Owner.Id, Text, Color, Position.X, Position.Y, Position.Z,
                    DrawDistance,
                    AttachedPlayer == null ? GtaPlayer.InvalidId : AttachedPlayer.Id,
                    AttachedVehicle == null ? GtaVehicle.InvalidId : AttachedVehicle.Id, TestLOS);
            }
        }

        /// <summary>
        ///     Gets or sets the attached vehicle.
        /// </summary>
        public virtual GtaVehicle AttachedVehicle
        {
            get { return _attachedVehicle; }
            set
            {
                _attachedVehicle = value;
                Dispose();
                Id = Native.CreatePlayer3DTextLabel(Owner.Id, Text, Color, Position.X, Position.Y, Position.Z,
                    DrawDistance,
                    AttachedPlayer == null ? GtaPlayer.InvalidId : AttachedPlayer.Id,
                    AttachedVehicle == null ? GtaVehicle.InvalidId : AttachedVehicle.Id, TestLOS);
            }
        }

        /// <summary>
        ///     Gets the Identity of this instance.
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        ///     Gets the owner of this IOwnable.
        /// </summary>
        public virtual GtaPlayer Owner { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextLabel" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="id">The identifier.</param>
        /// <exception cref="System.ArgumentNullException">owner</exception>
        public PlayerTextLabel(GtaPlayer owner, int id)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            Owner = owner;
            Id = id;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextLabel" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="color">The color.</param>
        /// <param name="position">The position.</param>
        /// <param name="drawDistance">The draw distance.</param>
        /// <param name="testLOS">if set to <c>true</c> [test los].</param>
        /// <exception cref="System.ArgumentNullException">owner</exception>
        public PlayerTextLabel(GtaPlayer owner, string text, Color color, Vector3 position, float drawDistance,
            bool testLOS)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            Owner = owner;
            _color = color;
            _position = position;
            _drawDistance = drawDistance;
            _testLOS = testLOS;

            Id = Native.CreatePlayer3DTextLabel(owner.Id, text, color, position.X, position.Y, position.Z, drawDistance,
                GtaPlayer.InvalidId, GtaVehicle.InvalidId, testLOS);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextLabel" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="color">The color.</param>
        /// <param name="position">The position.</param>
        /// <param name="drawDistance">The draw distance.</param>
        public PlayerTextLabel(GtaPlayer owner, string text, Color color, Vector3 position, float drawDistance)
            : this(owner, text, color, position, drawDistance, true)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextLabel" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="color">The color.</param>
        /// <param name="position">The position.</param>
        /// <param name="drawDistance">The draw distance.</param>
        /// <param name="testLOS">if set to <c>true</c> [test los].</param>
        /// <param name="attachedPlayer">The attached player.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     owner
        ///     or
        ///     attachedPlayer
        /// </exception>
        public PlayerTextLabel(GtaPlayer owner, string text, Color color, Vector3 position, float drawDistance,
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

            Id = Native.CreatePlayer3DTextLabel(owner.Id, text, color, position.X, position.Y, position.Z, drawDistance,
                attachedPlayer.Id, GtaVehicle.InvalidId, testLOS);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextLabel" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="color">The color.</param>
        /// <param name="position">The position.</param>
        /// <param name="drawDistance">The draw distance.</param>
        /// <param name="attachedPlayer">The attached player.</param>
        public PlayerTextLabel(GtaPlayer owner, string text, Color color, Vector3 position, float drawDistance,
            GtaPlayer attachedPlayer) : this(owner, text, color, position, drawDistance, true, attachedPlayer)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextLabel" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="color">The color.</param>
        /// <param name="position">The position.</param>
        /// <param name="drawDistance">The draw distance.</param>
        /// <param name="testLOS">if set to <c>true</c> [test los].</param>
        /// <param name="attachedVehicle">The attached vehicle.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     owner
        ///     or
        ///     attachedVehicle
        /// </exception>
        public PlayerTextLabel(GtaPlayer owner, string text, Color color, Vector3 position, float drawDistance,
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

            Id = Native.CreatePlayer3DTextLabel(owner.Id, text, color, position.X, position.Y, position.Z, drawDistance,
                GtaPlayer.InvalidId, attachedVehicle.Id, testLOS);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextLabel" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="color">The color.</param>
        /// <param name="position">The position.</param>
        /// <param name="drawDistance">The draw distance.</param>
        /// <param name="attachedVehicle">The attached vehicle.</param>
        public PlayerTextLabel(GtaPlayer owner, string text, Color color, Vector3 position, float drawDistance,
            GtaVehicle attachedVehicle)
            : this(owner, text, color, position, drawDistance, true, attachedVehicle)
        {
        }

        #endregion
    }
}