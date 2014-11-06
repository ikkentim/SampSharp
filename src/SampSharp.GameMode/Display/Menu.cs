// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

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

        public event EventHandler<PlayerEventArgs> Exit;

        public event EventHandler<PlayerSelectedMenuRowEventArgs> Response;

        #endregion

        #region Methods

        /// <summary>
        ///     Show this menu for the specified player.
        /// </summary>
        /// <param name="player">The player to show this menu for.</param>
        /// <returns>True when successfull, False otherwise.</returns>
        public bool Show(GtaPlayer player)
        {
            CheckDisposure();

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
            CheckDisposure();

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
            CheckDisposure();

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

        protected override void Dispose(bool disposing)
        {
            Destroy();

            base.Dispose(disposing);
        }

        #endregion

        #region Event raisers

        public void OnExit(PlayerEventArgs e)
        {
            if (Exit != null)
                Exit(this, e);
        }

        public void OnResponse(PlayerSelectedMenuRowEventArgs e)
        {
            if (Response != null)
                Response(this, e);
        }

        #endregion
    }
}