using GameMode.Definitions;
using GameMode.World;

namespace GameMode.Events
{
    public class PlayerSelectObjectEventArgs : PlayerClickMapEventArgs
    {
        public PlayerSelectObjectEventArgs(int playerid, ObjectType type, int objectid, int modelid, Position position)
            : base(playerid, position)
        {
            ObjectType = type;
            ObjectId = objectid;
            ModelId = modelid;
        }

        public ObjectType ObjectType { get; private set; }

        public int ObjectId { get; private set; }

        public int ModelId { get; private set; }
    }
}
