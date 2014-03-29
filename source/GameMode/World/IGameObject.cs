using GameMode.Definitions;

namespace GameMode.World
{
    public interface IGameObject : IWorldObject
    {
        bool IsMoving { get; }
        bool IsValid { get; }
        int ModelId { get; }
        float DrawDistance { get; }
        int ObjectId { get; }


        void AttachTo(Player player, Vector offset, Vector rotation);
        void AttachTo(Vehicle vehicle, Vector offset, Vector rotation);
        int Move(Vector position, float speed, Vector rotation);
        int Move(Vector position, float speed);
        void Stop();
        void SetMaterial(int materialindex, int modelid, string txdname, string texturename, Color materialcolor);

        void SetMaterialText(string text, int materialindex, ObjectMaterialSize materialsize, string fontface,
            int fontsize, bool bold, Color foreColor, Color backColor, ObjectMaterialTextAlign textalignment);
    }
}
