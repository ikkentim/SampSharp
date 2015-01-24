using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    /// Provides data for the <see cref="BaseMode.PlayerEditGlobalObject" />, <see cref="GtaPlayer.EditGlobalObject" /> or <see cref="GlobalObject.Edited" /> event.
    /// </summary>
    public class EditGlobalObjectEventArgs : PositionEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditGlobalObjectEventArgs"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="object">The global object.</param>
        /// <param name="response">The response.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        public EditGlobalObjectEventArgs(GtaPlayer player, GlobalObject @object, EditObjectResponse response, Vector position, Vector rotation) : base(position)
        {
            Player = player;
            Object = @object;
            EditObjectResponse = response;
            Rotation = rotation;
        }

        /*
         * Since the BaseMode.OnPlayerEditGlobalObject can either have a GtaPlayer of GlobalObject instance as sender,
         * we add both to the event args so we can access what's not the sender.
         */
        /// <summary>
        /// Gets the player.
        /// </summary>
        public GtaPlayer Player { get; private set; }

        /// <summary>
        /// Gets the global object.
        /// </summary>
        public GlobalObject Object { get; private set; }

        /// <summary>
        /// Gets the edit object response.
        /// </summary>
        public EditObjectResponse EditObjectResponse { get; private set; }

        /// <summary>
        /// Gets the rotation.
        /// </summary>
        public Vector Rotation { get; private set; }
    }
}