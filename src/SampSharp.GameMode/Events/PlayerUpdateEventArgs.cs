using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampSharp.GameMode.Events
{
    public class PlayerUpdateEventArgs : PlayerEventArgs
    {
        public PlayerUpdateEventArgs(int playerid) : base(playerid)
        {
        }


        /// <summary>
        /// Gets or sets whether to stop syncing the update to other players.
        /// </summary>
        public bool PreventPropagation { get; set; }
    }
}
