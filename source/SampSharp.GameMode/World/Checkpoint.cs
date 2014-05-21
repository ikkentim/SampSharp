using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.Pools;

namespace SampSharp.GameMode.World
{
    public class Checkpoint : Pool<Checkpoint>
    {
        private bool _isVisible;
        private readonly List<Player> _viewers = new List<Player>();
        private readonly List<Player> _active = new List<Player>();
        private readonly List<Player> _forced = new List<Player>();
 
        public Checkpoint(Vector position, float size)
        {
            Position = position;
            Size = size;
        }

        public Vector Position { get; set; }

        public float Size { get; set; }

        public ReadOnlyCollection<Player> Viewers
        {
            get { return _viewers.AsReadOnly(); }
        } 

        public EventHandler<PlayerEventArgs> Enter;

        public EventHandler<PlayerEventArgs> Leave;

        public void OnEnter(PlayerEventArgs e)
        {
            if (Enter != null)
                Enter(this, e);
        }

        public void OnLeave(PlayerEventArgs e)
        {
            if (Leave != null)
                Leave(this, e);
        }

        public void Show()
        {
            _isVisible = true;
        }

        public void Show(Player player)
        {
            if(!IsVisible(player))
                _viewers.Add(player);
        }

        public void Hide()
        {
            _isVisible = false;
        }

        public void Hide(Player player)
        {
            _viewers.Remove(player);
        }

        public bool IsVisible()
        {
            return _isVisible;
        }

        public bool IsVisible(Player player)
        {
            return _isVisible || _viewers.Contains(player);
        }

        public void Activate(Player player)
        {
            if (IsActive(player)) return;

            Native.SetPlayerCheckpoint(player.Id, Position, Size);
            _active.Add(player);
        }

        public void Deactivate(Player player)
        {
            if (!IsActive(player)) return;

            Native.DisablePlayerCheckpoint(player.Id);
            _active.Remove(player);
        }
    
        public bool IsActive(Player player)
        {
            return _active.Contains(player);
        }

        public void Force(Player player)
        {
            if (IsForced(player)) return;

            _forced.Add(player);
        }

        public void Unforce(Player player)
        {
            _forced.Remove(player);
        }

        public bool IsForced(Player player)
        {
            return _forced.Contains(player);
        }
    }
}
