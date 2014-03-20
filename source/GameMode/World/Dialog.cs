using System;
using System.Collections.Generic;
using GameMode.Definitions;

namespace GameMode.World
{
    public class Dialog
    {
        #region Fields

        /// <summary>
        /// Contains all instances of Dialogs.
        /// </summary>
        protected static List<Dialog> Instances = new List<Dialog>();

        /// <summary>
        /// Gets an ID commonly returned by methods to point out that no dialog matched the requirements.
        /// </summary>
        public const int InvalidId = 0;

        #endregion

        public Dialog(Player player)
        {
            
        }

        /// <summary>
        /// Registers all events the Dialog class listens to.
        /// </summary>
        /// <param name="baseMode">An instance of the BaseMode to which to listen.</param>
        /// <param name="cast">A function to get a <see cref="Dialog"/> object from a dialogid.</param>
        public static void RegisterEvents(BaseMode baseMode, Func<int, Dialog> cast)
        {

        }

        /// <summary>
        /// Registers all events the Dialog class listens to.
        /// </summary>
        /// <param name="gameMode">An instance of BaseMode to which to listen.</param>
        public static void RegisterEvents(BaseMode gameMode)
        {
            RegisterEvents(gameMode, Find);
        }
    }
}
