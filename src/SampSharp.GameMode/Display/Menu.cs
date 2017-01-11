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
using System;
using System.Collections.Generic;
using System.Linq;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Display
{
    /// <summary>
    ///     Represents a SA:MP menu.
    /// </summary>
    public partial class Menu : Pool<Menu>
    {
        private readonly List<BasePlayer> _viewers = new List<BasePlayer>();

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the Menu class.
        /// </summary>
        /// <param name="title">The title of the menu.</param>
        /// <param name="position">The position of the menu on the screen.</param>
        /// <param name="columns">The columns to display in the menu.</param>
        /// <param name="rows">The rows to display in the menu.</param>
        public Menu(string title, Vector2 position, IList<MenuColumn> columns = null, IList<MenuRow> rows = null)
        {
            Id = InvalidId;
            Title = title;
            Position = position;
            Columns = columns ?? new List<MenuColumn>();
            Rows = rows ?? new List<MenuRow>();
            Viewers = _viewers.AsReadOnly();
        }

        #endregion

        #region Implementation of IIdentifiable

        /// <summary>
        ///     Gets the Identity of this <see cref="IIdentifiable" />.
        /// </summary>
        public int Id { get; private set; }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            Destroy();

            base.Dispose(disposing);
        }

        #endregion

        #region Constants of Menu

        /// <summary>
        ///     Identifier indicating the handle is invalid.
        /// </summary>
        public const int InvalidId = 0xFF;

        /// <summary>
        ///     Maximum number of menus which can exist.
        /// </summary>
        public const int Max = 128;

        #endregion

        #region Implementation of IMenu

        /// <summary>
        ///     Gets the title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        ///     Gets the position.
        /// </summary>
        public Vector2 Position { get; }

        /// <summary>
        ///     Gets an <see cref="IReadOnlyCollection{T}" /> of <see cref="BasePlayer" /> instances which are viewing this
        ///     instance.
        /// </summary>
        public IReadOnlyCollection<BasePlayer> Viewers { get; private set; }

        /// <summary>
        ///     Gets a collection of columns.
        /// </summary>
        public IList<MenuColumn> Columns { get; }

        /// <summary>
        ///     Gets a collection of rows.
        /// </summary>
        public IList<MenuRow> Rows { get; }

        /// <summary>
        ///     Occurs when this <see cref="Menu" /> was exited.
        /// </summary>
        public event EventHandler<EventArgs> Exit;

        /// <summary>
        ///     Occurs when there has been responded to this <see cref="Menu" />.
        /// </summary>
        public event EventHandler<MenuRowEventArgs> Response;

        /// <summary>
        ///     Show this <see cref="Menu" /> to the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player to show this menu to.</param>
        /// <returns>True when successful; False otherwise.</returns>
        public bool Show(BasePlayer player)
        {
            AssertNotDisposed();

            if (player == null)
                throw new ArgumentNullException(nameof(player));

            if (Id == InvalidId)
            {
                Create();
            }

            if (Id == InvalidId)
            {
                return false;
            }

            _viewers.Add(player);
            MenuInternal.Instance.ShowMenuForPlayer(Id, player.Id);

            return true;
        }

        /// <summary>
        ///     Hides this <see cref="Menu" /> for the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player to hide this menu for.</param>
        public void Hide(BasePlayer player)
        {
            AssertNotDisposed();

            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            _viewers.Remove(player);

            if (Id != InvalidId)
            {
                MenuInternal.Instance.HideMenuForPlayer(Id, player.Id);
            }

            if (_viewers.Count == 0)
            {
                Destroy();
            }
        }

        /// <summary>
        ///     Hides this <see cref="Menu" /> for all viewers.
        /// </summary>
        public void HideForAll()
        {
            AssertNotDisposed();

            //Clone list and hide for all.
            foreach (var p in _viewers.ToList())
                Hide(p);
        }

        /// <summary>
        ///     Raises the <see cref="Menu.Exit" /> event.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public void OnExit(BasePlayer player, EventArgs e)
        {
            Exit?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="Menu.Response" /> event.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="e">The <see cref="MenuRowEventArgs" /> instance containing the event data.</param>
        public void OnResponse(BasePlayer player, MenuRowEventArgs e)
        {
            Response?.Invoke(player, e);
        }

        #endregion

        #region Methods of Menu

        private void Create()
        {
            Destroy();

            if (Columns == null || Columns.Count == 0)
            {
                throw new Exception("This menu contains no columns");
            }

            if (Rows == null || Rows.Count == 0)
            {
                throw new Exception("This menu contains no rows");
            }

            Id = MenuInternal.Instance.CreateMenu(Title, Columns.Count, Position.X, Position.Y,
                Columns[0].Width, Columns.Count == 2 ? Columns[1].Width : 0);

            if (Id == InvalidId)
            {
                return;
            }

            for (var i = 0; i < Math.Min(Columns.Count, 2); i++)
            {
                if (Columns[i].Caption != null)
                {
                    MenuInternal.Instance.SetMenuColumnHeader(Id, i, Columns[i].Caption);
                }
            }

            for (var i = 0; i < Rows.Count; i++)
            {
                MenuInternal.Instance.AddMenuItem(Id, 0, Rows[i].Column1Text ?? string.Empty);
                if (!string.IsNullOrEmpty(Rows[i].Column2Text))
                    MenuInternal.Instance.AddMenuItem(Id, 1, Rows[i].Column2Text);

                if (Rows[i].Disabled)
                {
                    MenuInternal.Instance.DisableMenuRow(Id, i);
                }
            }
        }

        private void Destroy()
        {
            if (Id != InvalidId)
            {
                HideForAll();

                MenuInternal.Instance.DestroyMenu(Id);
                Id = InvalidId;
            }
        }

        #endregion
    }
}