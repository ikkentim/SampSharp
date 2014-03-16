namespace GameMode.Events
{
    public class ObjectEventArgs : GameModeEventArgs
    {
        public ObjectEventArgs(int objectid)
        {
            ObjectId = objectid;
        }

        public int ObjectId { get; private set; }
    }
}
