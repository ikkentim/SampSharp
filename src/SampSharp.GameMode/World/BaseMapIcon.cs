using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using static SampSharp.GameMode.World.BasePlayer;

namespace SampSharp.GameMode.World
{
    /// <summary>
    /// Represents a Map Icon.
    /// </summary>
    public class BaseMapIcon
    {
        /// <summary>
        /// Where to show the map icon.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// The type of the icon.
        /// </summary>
        public MapIcon Type { get; set; }

        /// <summary>
        /// The style (global etc.)
        /// </summary>
        public MapIconType Style { get; set; }

        /// <summary>
        /// The color of the icon.
        /// Used for the dynamic square.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// The Id assigned by SAMP to the icon.
        /// </summary>
        private int _iconId;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="type"></param>
        /// <param name="style"></param>
        /// <param name="color"></param>
        public BaseMapIcon(Vector3 position, MapIcon type, MapIconType style, Color color = default)
        {
            Position = position;
            Type = type;
            Style = style;
            Color = color;
        }

        /// <summary>
        /// Show the icon to a player.
        /// </summary>
        /// <param name="player"></param>
        public bool Show(BasePlayer player)
        {
            player.MapIcons.Add(this);
            _iconId = player.MapIcons.Count;
            return PlayerInternal.Instance.SetPlayerMapIcon(player.Id, _iconId, Position.X, Position.Y, Position.Z, (int)Type, (int)Color,
                (int)Style);
        }

        /// <summary>
        /// Hide the icon from the player.
        /// </summary>
        /// <param name="player"></param>
        public void Hide(BasePlayer player)
        {
            player.MapIcons.Remove(this);
            PlayerInternal.Instance.RemovePlayerMapIcon(player.Id, _iconId);
        }
    }
}
