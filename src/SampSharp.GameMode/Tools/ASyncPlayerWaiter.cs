using System.Threading.Tasks;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Tools
{
    /// <summary>
    ///     Contains methods for awaiting calls. Throws <see cref="PlayerDisconnectedException"/> if the player has disconnected.
    /// </summary>
    /// <typeparam name="TArguments"></typeparam>
    public class ASyncPlayerWaiter<TArguments> : ASyncWaiter<BasePlayer, TArguments>
    {
        #region Overrides of ASyncWaiter<TKey,TArguments>

        /// <summary>
        ///     Waits for the <see cref="ASyncWaiter{TKey,TArguments}.Fire" /> to be called with the specified <paramref name="key" />.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The arguments passed to the <see cref="ASyncWaiter{TKey,TArguments}.Fire" /> method.</returns>
        public override async Task<TArguments> Result(BasePlayer key)
        {
            try
            {
                return await base.Result(key);
            }
            catch (TaskCanceledException e)
            {
                throw new PlayerDisconnectedException("The player has left the server.", e);
            }
        }

        #endregion
    }
}