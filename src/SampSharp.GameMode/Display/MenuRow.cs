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
    ///     Represents a row in a <see cref="Menu" />.
    /// </summary>
    public struct MenuRow
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MenuRow" /> class.
        /// </summary>
        /// <param name="column1Text">The text in the first column.</param>
        /// <param name="disabled">Whether this row is disabled.</param>
        public MenuRow(string column1Text, bool disabled = false) : this()
        {
            Column1Text = column1Text;
            Column2Text = null;
            Disabled = disabled;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MenuRow" /> class.
        /// </summary>
        /// <param name="column1Text">The text in the text displayed in the first column.</param>
        /// <param name="column2Text">The text in the text displayed in the second column.</param>
        /// <param name="disabled">Whether this row is disabled.</param>
        public MenuRow(string column1Text, string column2Text, bool disabled = false)
            : this()
        {
            Column1Text = column1Text;
            Column2Text = column2Text;
            Disabled = disabled;
        }

        /// <summary>
        ///     Gets the text displayed in the first column.
        /// </summary>
        public string Column1Text { get; private set; }

        /// <summary>
        ///     Gets the text displayed in the second column.
        /// </summary>
        public string Column2Text { get; private set; }

        /// <summary>
        ///     Gets whether this row is disabled.
        /// </summary>
        public bool Disabled { get; private set; }
    }
}