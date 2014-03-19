using System.Linq;
using GameMode.World;

namespace TestMode.World
{
    public class MyPlayer : Player
    {
        protected MyPlayer(int playerId) : base(playerId)
        {
        }

        /// <summary>
        /// Database ID (Test purpose, not connected to a DB)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Returns an instance of <see cref="MyPlayer"/> that deals with <paramref name="playerId"/>.
        /// </summary>
        /// <param name="playerId">The ID of the player we are dealing with.</param>
        /// <returns>An instance of <see cref="MyPlayer"/>.</returns>
        public static new MyPlayer Find(int playerId)
        {
            //Find player in memory or initialize new player
            return PlayerInstances.Cast<MyPlayer>().FirstOrDefault(p => p.PlayerId == playerId) ?? new MyPlayer(playerId);
        }
    }
}
