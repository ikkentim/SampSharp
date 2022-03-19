using System;

namespace SampSharp.Entities.SAMP.Commands
{
    /// <summary>
    /// Provides the functionality for invoking player commands.
    /// </summary>
    public interface IPlayerCommandService
    {
        /// <summary>
        /// Invokes a player command using the specified <paramref name="inputText" /> for the specified <paramref name="player" />.
        /// </summary>
        /// <param name="services">A service provider.</param>
        /// <param name="player">The player for which the command is invoked.</param>
        /// <param name="inputText">The input text to be parsed.</param>
        /// <returns><c>true</c> if the command was handled; otherwise <c>false</c>.</returns>
        bool Invoke(IServiceProvider services, EntityId player, string inputText);
    }
}