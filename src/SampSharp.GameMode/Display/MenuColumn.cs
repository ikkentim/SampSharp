// SampSharp
// Copyright 2017 Tim Potze
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
namespace SampSharp.GameMode.Display
{
    /// <summary>
    ///     Represents a column in a <see cref="Menu" />.
    /// </summary>
    public struct MenuColumn
    {
        /// <summary>
        ///     Initializes a new instance of the MenuColumn class.
        /// </summary>
        /// <param name="caption">The caption of the column.</param>
        /// <param name="width">The width of the column.</param>
        public MenuColumn(string caption, float width) : this()
        {
            Caption = caption;
            Width = width;
        }

        /// <summary>
        ///     Initializes a new instance of the menuColumn structure.
        /// </summary>
        /// <param name="width">The width of the column.</param>
        public MenuColumn(float width) : this()
        {
            Width = width;
        }

        /// <summary>
        ///     Gets or sets the caption of this column.
        /// </summary>
        public string Caption { get; private set; }

        /// <summary>
        ///     Gets or sets the width if this column.
        /// </summary>
        public float Width { get; private set; }
    }
}