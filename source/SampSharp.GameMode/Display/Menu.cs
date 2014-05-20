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

        private MenuColumn[] _columns;
        private readonly List<Player> _viewers = new List<Player>(); 

        #endregion

        #region Constructors

        public Menu(string title, float x, float y, MenuColumn[] columns)
        {
            Id = InvalidId;
            Title = title;
            X = x;
            Y = y;
            Columns = columns;
        }

        public Menu(string title, float x, float y, MenuColumn[] columns, MenuRow[] rows)
            : this(title, x, y, columns)
        {
            Rows = rows;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the id of this menu.
        /// </summary>
        public int Id { get; private set; }

        public string Title { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public ReadOnlyCollection<Player> Viewers
        {
            get { return _viewers.AsReadOnly(); }
        } 

        public MenuColumn[] Columns
        {
            get { return _columns; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value cannot be null");
                
                if (value.Length < 1 || value.Length > 2)
                    throw new ArgumentOutOfRangeException("value must contain 1 or 2 elements");
                

                _columns = value;
            }
        }

        public MenuRow[] Rows { get; set; }

        #endregion

        #region Events

        public event EventHandler<PlayerEventArgs> Exit;

        public event EventHandler<PlayerSelectedMenuRowEventArgs> Response;

        #endregion

        #region Methods
        public bool Show(Player player)
        {
            if(Id == InvalidId)
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
            //Remove menu from viewers list
            _viewers.Remove(player);

            //Hide menu
            if (Id != InvalidId)
                Native.HideMenuForPlayer(Id, player.Id);

            //Keep dialog count low
            if(_viewers.Count == 0)
                Destroy();
            
        }

        public void HideForAll()
        {
            //Clone list and hide for all.
            foreach (Player p in _viewers.ToList())
                Hide(p);
        }

        private void Create()
        {
            Destroy();
            
            //Create menu
            Id = Native.CreateMenu(Title, Columns.Length, X, Y,
                Columns[0].Width, Columns.Length == 2 ? Columns[1].Width : 0);

            //Check success
            if (Id == InvalidId)
                return;

            //Set captions of all columns
            for (int i = 0; i < Columns.Length; i++)
            {
                if (Columns[i].Caption != null)
                    Native.SetMenuColumnHeader(Id, i, Columns[i].Caption);
            }

            //Add rows  to menu
            for (int i = 0; i < Rows.Length; i++)
            {
                //Set text
                for (int j = 0; j < Columns.Length; j++)
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

        public override void Dispose()
        {
            Destroy();
            base.Dispose();
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
