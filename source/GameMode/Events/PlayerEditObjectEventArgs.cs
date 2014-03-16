using GameMode.Definitions;
using GameMode.World;

namespace GameMode.Events
{
    public class PlayerEditObjectEventArgs : PlayerClickMapEventArgs
    {
        public PlayerEditObjectEventArgs(int playerid, bool playerobject, int objectid, EditObjectResponse response,
            Position position, Rotation rotation) : base(playerid, position)
        {
            PlayerObject = playerobject;
            ObjectId = objectid;
            EditObjectResponse = response;
            Rotation = rotation;
        }

        public bool PlayerObject { get; private set; }

        public int ObjectId { get; private set; }

        public EditObjectResponse EditObjectResponse { get; private set; }

        public Rotation Rotation { get; private set; }

    }
}
