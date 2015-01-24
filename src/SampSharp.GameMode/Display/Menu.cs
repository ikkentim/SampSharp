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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Display
{
    /// <summary>
    ///     Represents a menu
    /// </summary>
    public class Menu : Pool<Menu>
    {
        #region Fields

        /// <summary>
        ///     Gets the ID commonly returned by methods to point to no menu.
        /// </summary>
        public const int InvalidId = Misc.InvalidMenu;

        private readonly List<GtaPlayer> _viewers = new List<GtaPlayer>();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the Menu class.
        /// </summary>
        /// <param name="title">The title of the menu.</param>
        /// <param name="x">The x-position of the menu on the screen.</param>
        /// <param name="y">The y-position of the menu on the screen.</param>
        /// <param name="columns">The columns to display in the menu.</param>
        /// <param name="rows">The rows to display in the menu.</param>
        public Menu(string title, float x, float y, List<MenuColumn> columns = null, List<MenuRow> rows = null)
        {
            Id = InvalidId;
            Title = title;
            X = x;
            Y = y;
            Columns = columns ?? new List<MenuColumn>();
            Rows = rows ?? new List<MenuRow>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the id of this menu.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        ///     Gets or sets the title of this menu.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets the x-position of this menu on the screen.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        ///     Gets or sets the y-position of this menu on the screen.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        ///     Gets a list of viewers of this menu.
        /// </summary>
        public ReadOnlyCollection<GtaPlayer> Viewers
        {
            get { return _viewers.AsReadOnly(); }
        }

        /// <summary>
        ///     Gets a collection of columns in this menu.
        /// </summary>
        public List<MenuColumn> Columns { get; private set; }

        /// <summary>
        ///     Gets a collection of rows in this menu.
        /// </summary>
        public List<MenuRow> Rows { get; private set; }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when this <see cref="Menu" /> was exited.
        /// </summary>
        public event EventHandler<EventArgs> Exit;

        /// <summary>
        ///     Occurs when there has been responded to this <see cref="Menu" />.
        /// </summary>
        public event EventHandler<MenuRowEventArgs> Response;

        #endregion

        #region Methods

        /// <summary>
        ///     Show this menu for the specified player.
        /// </summary>
        /// <param name="player">The player to show this menu for.</param>
        /// <returns>True when successfull, False otherwise.</returns>
        public bool Show(GtaPlayer player)
        {
            CheckDisposed();

            if (player == null)
                throw new ArgumentNullException("player");

            if (Id == InvalidId)
            {
                Create();
            }

            if (Id == InvalidId)
            {
                return false;
            }

            _viewers.Add(player);
            Native.ShowMenuForPlayer(Id, player.Id);

            return true;
        }

        /// <summary>
        ///     Hides this menu for the specified player.
        /// </summary>
        /// <param name="player">The player to hide this menu for.</param>
        public void Hide(GtaPlayer player)
        {
            CheckDisposed();

            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            _viewers.Remove(player);

            if (Id != InvalidId)
            {
                Native.HideMenuForPlayer(Id, player.Id);
            }

            if (_viewers.Count == 0)
            {
                Destroy();
            }
        }

        /// <summary>
        ///     Hides this menu for all players that are viewing this menu.
        /// </summary>
        public void HideForAll()
        {
            CheckDisposed();

            //Clone list and hide for all.
            foreach (GtaPlayer p in _viewers.ToList())
                Hide(p);
        }

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

            Id = Native.CreateMenu(Title, Columns.Count, X, Y,
                Columns[0].Width, Columns.Count == 2 ? Columns[1].Width : 0);

            if (Id == InvalidId)
            {
                return;
            }

            for (int i = 0; i < Math.Min(Columns.Count, 2); i++)
            {
                if (Columns[i].Caption != null)
                {
                    Native.SetMenuColumnHeader(Id, i, Columns[i].Caption);
                }
            }

            for (int i = 0; i < Rows.Count; i++)
            {
                for (int j = 0; j < Math.Min(Columns.Count, 2); j++)
                {
                    if (Rows[i].Text.Length > j)
                        Native.AddMenuItem(Id, j, Rows[i].Text[j]);
                }

                if (Rows[i].Disabled)
                {
                    Native.DisableMenuRow(Id, i);
                }
            }
        }

        private void Destroy()
        {
            if (Id != InvalidId)
            {
                HideForAll();

                Native.DestroyMenu(Id);
                Id = InvalidId;
            }
        }

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

        #region Event raisers

        /// <summary>
        ///     Raises the <see cref="Exit" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public void OnExit(EventArgs e)
        {
            if (Exit != null)
                Exit(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Response" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MenuRowEventArgs" /> instance containing the event data.</param>
        public void OnResponse(MenuRowEventArgs e)
        {
            if (Response != null)
                Response(this, e);
        }

        #endregion
    }
}