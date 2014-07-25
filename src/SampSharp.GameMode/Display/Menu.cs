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
    public class Menu : Pool<Menu>
    {
        #region Fields

        /// <summary>
        ///     Gets an ID commonly returned by methods to point to no menu.
        /// </summary>
        public const int InvalidId = Misc.InvalidMenu;

        private readonly List<Player> _viewers = new List<Player>();

        #endregion

        #region Constructors

        public Menu(string title, float x, float y)
            : this(title, x, y, new List<MenuColumn>(), new List<MenuRow>())
        {
        }

        public Menu(string title, float x, float y, List<MenuColumn> columns)
            : this(title, x, y, columns, new List<MenuRow>())
        {
        }

        public Menu(string title, float x, float y, List<MenuColumn> columns, List<MenuRow> rows)
        {
            Id = InvalidId;
            Title = title;
            X = x;
            Y = y;
            Columns = columns;
            Rows = rows;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the id of this menu.
        /// </summary>
        public int Id { get; private set; }

        public string Title { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public ReadOnlyCollection<Player> Viewers
        {
            get { return _viewers.AsReadOnly(); }
        }

        public List<MenuColumn> Columns { get; set; }

        public List<MenuRow> Rows { get; set; }

        #endregion

        #region Events

        public event EventHandler<PlayerEventArgs> Exit;

        public event EventHandler<PlayerSelectedMenuRowEventArgs> Response;

        #endregion

        #region Methods

        public bool Show(Player player)
        {
            CheckDisposure();

            if (player == null)
                throw new ArgumentNullException("player");

            if (Id == InvalidId)
                Create();

            //Check for successful creation
            if (Id == InvalidId)
                return false;

            //Show menu
            _viewers.Add(player);
            Native.ShowMenuForPlayer(Id, player.Id);

            return true;
        }

        public void Hide(Player player)
        {
            CheckDisposure();

            if (player == null)
                throw new ArgumentNullException("player");

            //Remove menu from viewers list
            _viewers.Remove(player);

            //Hide menu
            if (Id != InvalidId)
                Native.HideMenuForPlayer(Id, player.Id);

            //Keep dialog count low
            if (_viewers.Count == 0)
                Destroy();
        }

        public void HideForAll()
        {
            CheckDisposure();

            //Clone list and hide for all.
            foreach (Player p in _viewers.ToList())
                Hide(p);
        }

        private void Create()
        {
            Destroy();

            //Check for data
            if (Columns == null || Columns.Count == 0 || Rows == null || Rows.Count == 0)
                return;

            //Create menu
            Id = Native.CreateMenu(Title, Columns.Count, X, Y,
                Columns[0].Width, Columns.Count == 2 ? Columns[1].Width : 0);

            //Check success
            if (Id == InvalidId)
                return;

            //Set captions of all columns
            for (int i = 0; i < Math.Min(Columns.Count, 2); i++)
            {
                if (Columns[i].Caption != null)
                    Native.SetMenuColumnHeader(Id, i, Columns[i].Caption);
            }

            //Add rows  to menu
            for (int i = 0; i < Rows.Count; i++)
            {
                //Set text
                for (int j = 0; j < Math.Min(Columns.Count, 2); j++)
                {
                    if (Rows[i].Text.Length > j)
                        Native.AddMenuItem(Id, j, Rows[i].Text[j]);
                }

                //Set disabled
                if (Rows[i].Disabled)
                    Native.DisableMenuRow(Id, i);
            }
        }

        private void Destroy()
        {
            if (Id != InvalidId)
            {
                //Hide for fall players
                HideForAll();

                //Destroy resource
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