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

namespace SampSharp.GameMode.Display
{
    /// <summary>
    ///     Represents a row in a <see cref="Menu" />.
    /// </summary>
    public class MenuRow
    {
        /// <summary>
        ///     Initializes a new instance of the MenuRow class.
        /// </summary>
        /// <param name="col1Text">The text in the first column.</param>
        /// <param name="disabled">Whether this row should be disabled.</param>
        public MenuRow(string col1Text, bool disabled = false)
        {
            Text = new[] {col1Text};
            Disabled = disabled;
        }

        /// <summary>
        ///     Initializes a new instance of the MenuRow class.
        /// </summary>
        /// <param name="col1Text">The text in the first column.</param>
        /// <param name="col2Text">The text in the second column.</param>
        /// <param name="disabled">Whether this row should be disabled.</param>
        public MenuRow(string col1Text, string col2Text, bool disabled = false)
        {
            Text = new[] {col1Text, col2Text};
            Disabled = disabled;
        }

        /// <summary>
        ///     Gets or sets the text displayed in this row on each column.
        /// </summary>
        public string[] Text { get; set; }

        /// <summary>
        ///     Gets or sets whether this row is disabled.
        /// </summary>
        public bool Disabled { get; set; }
    }
}