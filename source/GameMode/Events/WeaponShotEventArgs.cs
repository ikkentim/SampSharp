using GameMode.Definitions;
using GameMode.World;

namespace GameMode.Events
{
    public class WeaponShotEventArgs : PlayerClickMapEventArgs
    {
        public WeaponShotEventArgs(int playerid, Weapon weapon, BulletHitType hittype, int hitid, Position position) : base(playerid, position)
        {
            Weapon = weapon;
            BulletHitType = hittype;
            HitId = hitid;
        }

        public Weapon Weapon { get; private set; }

        public BulletHitType BulletHitType { get; private set; }

        public int HitId { get; private set; }
    }
}
