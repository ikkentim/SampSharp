using System;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    /// Provides data for the <see cref="BaseMode.PlayerClickPlayerTextDraw"/>, <see cref="PlayerTextDraw.Click"/> or <see cref="GtaPlayer.ClickPlayerTextDraw"/> event.
    /// </summary>
    public class ClickPlayerTextDrawEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ClickPlayerTextDrawEventArgs class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="playerTextDraw">The player text draw.</param>
        public ClickPlayerTextDrawEventArgs(GtaPlayer player, PlayerTextDraw playerTextDraw)
        {
            Player = player;
            PlayerTextDraw = playerTextDraw;
        }

        /// <summary>
        /// Gets the player.
        /// </summary>
        public GtaPlayer Player { get; private set; }

        /// <summary>
        ///     Gets the text draw.
        /// </summary>
        public PlayerTextDraw PlayerTextDraw { get; private set; }
    }
}