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
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Pools;

namespace SampSharp.GameMode.World
{
    public class Checkpoint : Pool<Checkpoint>
    {
        private readonly List<Player> _active = new List<Player>();
        private readonly List<Player> _forced = new List<Player>();
        private readonly List<Player> _viewers = new List<Player>();
        public EventHandler<PlayerEventArgs> Enter;

        public EventHandler<PlayerEventArgs> Leave;
        private bool _isVisible;

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
            if (player == null)
                throw new NullReferenceException("player cannot be null");

            if (!IsVisible(player))
                _viewers.Add(player);
        }

        public void Hide()
        {
            _isVisible = false;
        }

        public void Hide(Player player)
        {
            if (player == null)
                throw new NullReferenceException("player cannot be null");

            _viewers.Remove(player);
        }

        public bool IsVisible()
        {
            return _isVisible;
        }

        public bool IsVisible(Player player)
        {
            return player != null && (_isVisible || _viewers.Contains(player));
        }

        public void Activate(Player player)
        {
            if (player == null)
                throw new NullReferenceException("player cannot be null");

            if (IsActive(player)) return;

            player.SetCheckpoint(Position, Size);
            _active.Add(player);
        }

        public void Deactivate(Player player)
        {
            if (player == null)
                throw new NullReferenceException("player cannot be null");

            if (!IsActive(player)) return;

            player.DisableCheckpoint();
            _active.Remove(player);
        }

        public bool IsActive(Player player)
        {
            return player != null && _active.Contains(player);
        }

        public void Force(Player player)
        {
            if (player == null)
                throw new NullReferenceException("player cannot be null");

            if (IsForced(player)) return;

            _forced.Add(player);
        }

        public void Unforce(Player player)
        {
            if (player == null)
                throw new NullReferenceException("player cannot be null");

            _forced.Remove(player);
        }

        public bool IsForced(Player player)
        {
            return player != null && _forced.Contains(player);
        }
    }
}