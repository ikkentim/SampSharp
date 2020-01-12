// SampSharp
// Copyright 2019 Tim Potze
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

using SampSharp.Entities.SAMP.NativeComponents;

namespace SampSharp.Entities.SAMP.Components
{
    /// <summary>
    /// Represents a component which provides the data and functionality of a 3D text label.
    /// </summary>
    public class TextLabel : Component
    {
        /// <inheritdoc />
        public TextLabel(string text, Color color, Vector3 position, float drawDistance, int virtualWorld,
            bool testLos)
        {
            Text = text;
            Color = color;
            Position = position;
            DrawDistance = drawDistance;
            VirtualWorld = virtualWorld;
            TestLos = testLos;
        }

        /// <summary>
        /// Gets the color of this text label.
        /// </summary>
        public Color Color { get; }

        /// <summary>
        /// Gets the text of this text label.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the position of this text label.
        /// </summary>
        public Vector3 Position { get; }

        /// <summary>
        /// Gets the draw distance.
        /// </summary>
        public float DrawDistance { get; }

        /// <summary>
        /// Gets the virtual world.
        /// </summary>
        public int VirtualWorld { get; }

        /// <summary>
        /// Gets a value indicating whether to test the line of sight.
        /// </summary>
        public bool TestLos { get; }

        /// <summary>
        /// Gets the attached entity.
        /// </summary>
        public Entity AttachedEntity { get; } // TODO: Implement

        /// <inheritdoc />
        protected override void OnDestroyComponent()
        {
            GetComponent<NativeTextLabel>().Delete3DTextLabel();
        }
    }
}