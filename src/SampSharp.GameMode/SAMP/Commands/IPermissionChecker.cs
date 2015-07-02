using System;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     A Permission checker is used to check if a player
    ///     is allowed to use a specific command.
    /// 
    ///     Every class that implement this interface
    ///     should create parameter-less constructor or let
    ///     the compiler create one
    /// </summary>
    public interface IPermissionChecker
    {
        /// <summary>
        ///     The message that the user should see when 
        ///     he doesn't have the permission to use the command.
        /// 
        ///     If null the default SA-MP message will be used.
        /// </summary>
        string Message { get; }

        /// <summary>
        ///     Called when a user tries to use a command
        ///     that require permission.
        /// </summary>
        /// <param name="player">The player that has tried to execute the command</param>
        /// <returns>Return true if the player passed as argument is allowed to use the command, False otherwise.</returns>
        bool Check(GtaPlayer player);
    }
}