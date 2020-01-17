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
    /// Represents a component which provides the data and functionality of a menu.
    /// </summary>
    public sealed class Menu : Component
    {
        private string _col0Header;
        private string _col1Header;

        private Menu(string title, int columns, Vector2 position, float col0Width, float col1Width)
        {
            Title = title;
            Columns = columns;
            Position = position;
            Col0Width = col0Width;
            Col1Width = col1Width;
        }

        /// <summary>
        /// Gets the title of this menu.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the number of columns in this menu.
        /// </summary>
        public int Columns { get; }

        /// <summary>
        /// Gets the position of this menu.
        /// </summary>
        public Vector2 Position { get; }

        /// <summary>
        /// Gets the width of the left column in this menu.
        /// </summary>
        public float Col0Width { get; }

        /// <summary>
        /// Gets the width of the right column in this menu.
        /// </summary>
        public float Col1Width { get; }

        /// <summary>
        /// Gets or sets the caption of the left column in this menu.
        /// </summary>
        public string Col0Header
        {
            get => _col0Header;
            set
            {
                GetComponent<NativeMenu>().SetMenuColumnHeader(0, value);
                _col0Header = value;
            }
        }

        /// <summary>
        /// Gets or sets the caption of the right column in this menu.
        /// </summary>
        public string Col1Header
        {
            get => _col1Header;
            set
            {
                GetComponent<NativeMenu>().SetMenuColumnHeader(1, value);
                _col1Header = value;
            }
        }

        /// <summary>
        /// Adds an item to this menu.
        /// </summary>
        /// <param name="col0Text">The text for the left column.</param>
        /// <param name="col1Text">The text for the right column. If this menu only has one column, this value is ignored.</param>
        /// <returns>The index of the row this item was added to.</returns>
        /// <remarks>
        /// You can only have 12 items per menu (13th goes to the right side of the header of column name (colored), 14th
        /// and higher not display at all). Maximum length of menu item is 31 symbols.
        /// </remarks>
        public int AddItem(string col0Text, string col1Text = null)
        {
            if (col0Text == null) throw new ArgumentNullException(nameof(col0Text));

            if(col1Text == null && Columns == 2)
                throw new ArgumentNullException(nameof(col1Text), "The text for the right column may not be null because this menu has 2 columns.");

            var result = GetComponent<NativeMenu>().AddMenuItem(0, col0Text);

            if (Columns == 2)
                GetComponent<NativeMenu>().AddMenuItem(1, col1Text);

            return result;
        }

        /// <summary>
        /// Shows this menu for the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player.</param>
        public void Show(Entity player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            if (!player.IsOfType(SampEntities.PlayerType))
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

            GetComponent<NativeMenu>().ShowMenuForPlayer(player.Id);
        }

        /// <summary>
        /// Hides this menu for the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player.</param>
        public void Hide(Entity player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            if (!player.IsOfType(SampEntities.PlayerType))
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

            GetComponent<NativeMenu>().HideMenuForPlayer(player.Id);
        }

        /// <summary>
        /// Disable input for this menu. Any item will lose the ability to be selected.
        /// </summary>
        public void Disable()
        {
            GetComponent<NativeMenu>().DisableMenu();
        }

        /// <summary>
        /// Disable a specific row in this menu for all players. It will be greyed-out and can't be selected by players.
        /// </summary>
        /// <param name="row">The index of the row to disable.</param>
        public void DisableRow(int row)
        {
            GetComponent<NativeMenu>().DisableMenuRow(row);
        }

        /// <inheritdoc />
        protected override void OnDestroyComponent()
        {
            GetComponent<NativeMenu>().DestroyMenu();
        }
    }
}