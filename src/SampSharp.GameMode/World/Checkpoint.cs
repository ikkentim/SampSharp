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

        public Checkpoint(Vector position, float size, bool isVisible)
            : this(position, size)
        {
            _isVisible = isVisible;
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
            CheckDisposure();

            _isVisible = true;
        }

        public void Show(Player player)
        {
            CheckDisposure();

            if (player == null)
                throw new ArgumentNullException("player");

            if (!IsVisible(player))
                _viewers.Add(player);
        }

        public void Hide()
        {
            CheckDisposure();

            _isVisible = false;
        }

        public void Hide(Player player)
        {
            CheckDisposure();

            if (player == null)
                throw new ArgumentNullException("player");

            _viewers.Remove(player);
        }

        public bool IsVisible()
        {
            CheckDisposure();

            return _isVisible;
        }

        public bool IsVisible(Player player)
        {
            CheckDisposure();

            return player != null && (_isVisible || _viewers.Contains(player));
        }

        public void Activate(Player player)
        {
            CheckDisposure();

            if (player == null)
                throw new ArgumentNullException("player");

            if (IsActive(player)) return;

            player.SetCheckpoint(Position, Size);
            _active.Add(player);
        }

        public void Deactivate(Player player)
        {
            CheckDisposure();

            if (player == null)
                throw new ArgumentNullException("player");

            if (!IsActive(player)) return;

            player.DisableCheckpoint();
            _active.Remove(player);
        }

        public bool IsActive(Player player)
        {
            CheckDisposure();

            return player != null && _active.Contains(player);
        }

        public void Force(Player player)
        {
            CheckDisposure();

            if (player == null)
                throw new ArgumentNullException("player");

            if (IsForced(player)) return;

            //Unforce previously forced checkpoints
            var forced = All.FirstOrDefault(cp => cp.IsForced(player));

            if (forced != null)
                forced.Unforce(player);

            _forced.Add(player);
        }

        public void Unforce(Player player)
        {
            CheckDisposure();

            if (player == null)
                throw new ArgumentNullException("player");

            _forced.Remove(player);
        }

        public bool IsForced(Player player)
        {
            CheckDisposure();

            return player != null && _forced.Contains(player);
        }
    }
}