// SampSharp
// Copyright 2020 Tim Potze
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
using SampSharp.Entities.SAMP.NativeComponents;

namespace SampSharp.Entities.SAMP.Components
{
    /// <summary>
    /// Represents a component which provides the data and functionality of a 3D text label.
    /// </summary>
    public sealed class TextLabel : Component
    {
        private Entity _attachedEntity;
        private string _text;
        private Color _color;

        private TextLabel(string text, Color color, Vector3 position, float drawDistance, int virtualWorld,
            bool testLos)
        {
            _text = text;
            _color = color;
            Position = position;
            DrawDistance = drawDistance;
            VirtualWorld = virtualWorld;
            TestLos = testLos;
        }

        /// <summary>
        /// Gets or sets the color of this text label.
        /// </summary>
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                GetComponent<NativeTextLabel>().Update3DTextLabelText(value, Text);
            }
        }

        /// <summary>
        /// Gets or sets the text of this text label.
        /// </summary>
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                GetComponent<NativeTextLabel>().Update3DTextLabelText(Color, value ?? string.Empty);
            }
        }

        /// <summary>
        /// Gets the position of this text label.
        /// </summary>
        public Vector3 Position { get; }

        /// <summary>
        /// Gets the draw distance of this text label.
        /// </summary>
        public float DrawDistance { get; }

        /// <summary>
        /// Gets the virtual world of this text label.
        /// </summary>
        public int VirtualWorld { get; }

        /// <summary>
        /// Gets a value indicating whether to test the line of sight.
        /// </summary>
        public bool TestLos { get; }

        /// <summary>
        /// Gets or sets the offset at which this text label is attached to an entity.
        /// </summary>
        public Vector3 AttachOffset { get; set; }

        /// <summary>
        /// Gets or sets the attached entity (player or vehicle).
        /// </summary>
        public Entity AttachedEntity
        {
            get =>  _attachedEntity;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value)); // TODO: Can detach maybe?
                
                if (!value.IsOfAnyType(SampEntities.PlayerType, SampEntities.VehicleType))
                    throw new InvalidEntityArgumentException(nameof(value), SampEntities.PlayerType, SampEntities.VehicleType);

                if (value.IsOfType(SampEntities.PlayerType))
                    GetComponent<NativeTextLabel>()
                        .Attach3DTextLabelToPlayer(value.Id, AttachOffset.X, AttachOffset.Y, AttachOffset.Z);
                else
                    GetComponent<NativeTextLabel>()
                        .Attach3DTextLabelToVehicle(value.Id, AttachOffset.X, AttachOffset.Y, AttachOffset.Z);
                _attachedEntity = value;
            }
        }

        /// <inheritdoc />
        protected override void OnDestroyComponent()
        {
            GetComponent<NativeTextLabel>().Delete3DTextLabel();
        }
    }
}