using System;
using SampSharp.GameMode.Definitions;

namespace SampSharp.GameMode.World
{
    /// <summary>
    /// A weapon that can be given to a <see cref="BasePlayer" />.
    /// </summary>
    public class Weapon
    {
        /// <summary>
        /// The Id of the weapon.
        /// </summary>
        public WeaponType WeaponId { get; set; }

        /// <summary>
        /// The name of the weapon.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The number of bullets of the weapon.
        /// </summary>
        public int Bullets { get; set; }

        /// <summary>
        /// Can this weapon shoot ACTUAL bullets?
        /// </summary>
        public bool CanShoot { get; }

        /// <summary>
        /// The slot this weapon occupies.
        /// </summary>
        public int Slot { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="weaponId">The Id of the weapon.</param>
        /// <param name="bullets">The number of bullets of the weapon.</param>
        public Weapon(WeaponType weaponId, int bullets)
        {
            WeaponId = weaponId;
            Name = GetWeaponName(weaponId);
            Bullets = bullets;
            CanShoot = CanWeaponShoot(weaponId);
            Slot = GetWeaponSlot(weaponId);
        }

        /// <summary>
        /// Give this weapon to a player.
        /// </summary>
        /// <param name="player">The player who receives this weapon.</param>
        public void GiveToPlayer(BasePlayer player)
        {
            player.GiveWeapon(this, Bullets);
        }
        /// <summary>
        /// Give this weapon to a player with custom bullet number.
        /// </summary>
        /// <param name="player">The player who receives this weapon.</param>
        /// <param name="bullets">The number of bullets to receive.</param>
        public void GiveToPlayer(BasePlayer player, int bullets)
        {
            player.GiveWeapon(this, bullets);
        }

        /// <summary>
        /// Give this weapons to all players in a range.
        /// </summary>
        /// <param name="range">The range in which the players must be of the point.</param>
        /// <param name="point">The center point.</param>
        public void GiveToRange(float range, Vector3 point)
        {
            foreach (var player in BasePlayer.All)
                if (player.IsInRangeOfPoint(range, point))
                    player.GiveWeapon(this, Bullets);
        }
        /// <summary>
        /// Give this weapons to all players in a range.
        /// </summary>
        /// <param name="range">The range in which the players must be of the point.</param>
        /// <param name="point">The center point.</param>
        /// <param name="bullets">The custom number of bullets.</param>
        public void GiveToRange(float range, Vector3 point, int bullets)
        {
            foreach (var player in BasePlayer.All)
                if (player.IsInRangeOfPoint(range, point))
                    player.GiveWeapon(this, bullets);
        }

        /// <summary>
        /// Get the slot this weapon occupies.
        /// </summary>
        /// <param name="weaponId">The id of the weapon.</param>
        public static int GetWeaponSlot(WeaponType weaponId)
        {
            switch (weaponId)
            {
                case WeaponType.None:
                    return 0;
                case WeaponType.Brassknuckle:
                    return 0;
                case WeaponType.Golfclub:
                    return 1;
                case WeaponType.Nitestick:
                    return 1;
                case WeaponType.Knife:
                    return 1;
                case WeaponType.Bat:
                    return 1;
                case WeaponType.Shovel:
                    return 1;
                case WeaponType.Poolstick:
                    return 1;
                case WeaponType.Katana:
                    return 1;
                case WeaponType.Chainsaw:
                    return 1;
                case WeaponType.DoubleEndedDildo:
                    return 10;
                case WeaponType.Dildo:
                    return 10;
                case WeaponType.Vibrator:
                    return 10;
                case WeaponType.SilverVibrator:
                    return 10;
                case WeaponType.Flower:
                    return 10;
                case WeaponType.Cane:
                    return 10;
                case WeaponType.Grenade:
                    return 8;
                case WeaponType.Teargas:
                    return 8;
                case WeaponType.Moltov:
                    return 8;
                case WeaponType.Colt45:
                    return 2;
                case WeaponType.Silenced:
                    return 2;
                case WeaponType.Deagle:
                    return 2;
                case WeaponType.Shotgun:
                    return 3;
                case WeaponType.Sawedoff:
                    return 3;
                case WeaponType.CombatShotgun:
                    return 3;
                case WeaponType.Uzi:
                    return 4;
                case WeaponType.MP5:
                    return 4;
                case WeaponType.AK47:
                    return 5;
                case WeaponType.M4:
                    return 5;
                case WeaponType.Tec9:
                    return 4;
                case WeaponType.Rifle:
                    return 6;
                case WeaponType.Sniper:
                    return 6;
                case WeaponType.RocketLauncher:
                    return 7;
                case WeaponType.HeatSeeker:
                    return 7;
                case WeaponType.FlameThrower:
                    return 7;
                case WeaponType.Minigun:
                    return 7;
                case WeaponType.SatchelCharge:
                    return 8;
                case WeaponType.Detonator:
                    return 12;
                case WeaponType.Spraycan:
                    return 9;
                case WeaponType.FireExtinguisher:
                    return 9;
                case WeaponType.Camera:
                    return 9;
                case WeaponType.NightVisionGoggles:
                    return 9;
                case WeaponType.ThermalGoggles:
                    return 11;
                case WeaponType.Parachute:
                    return 11;
                case WeaponType.FakePistol:
                    return 11;
                case WeaponType.Vehicle:
                    return -1;
                case WeaponType.HelicopterBlades:
                    return -1;
                case WeaponType.Explosion:
                    return -1;
                case WeaponType.Drown:
                    return -1;
                case WeaponType.Collision:
                    return -1;
                case WeaponType.Connect:
                    return -1;
                case WeaponType.Disconnect:
                    return -1;
                case WeaponType.Suicide:
                    return -1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(weaponId), weaponId, null);
            }
        }

        /// <summary>
        /// Get the name of a weapon by Id.
        /// </summary>
        /// <param name="weaponId"></param>
        /// <returns>The weapon's name</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static string GetWeaponName(WeaponType weaponId)
        {
            switch (weaponId)
            {
                case WeaponType.None:
                    return "None";
                case WeaponType.Brassknuckle:
                    return "Brass Knuckle";
                case WeaponType.Golfclub:
                    return "Golf Club";
                case WeaponType.Nitestick:
                    return "Night Stick";
                case WeaponType.Knife:
                    return "Knife"; 
                case WeaponType.Bat:
                    return "Bat";
                case WeaponType.Shovel:
                    return "Shovel";
                case WeaponType.Poolstick:
                    return "Cue Stick";
                case WeaponType.Katana:
                    return "Katana";
                case WeaponType.Chainsaw:
                    return "Chainsaw";
                case WeaponType.DoubleEndedDildo:
                    return "Double-ended Dildo";
                case WeaponType.Dildo:
                    return "Dildo";
                case WeaponType.Vibrator:
                    return "Vibrator";
                case WeaponType.SilverVibrator:
                    return "Silver Vibrator";
                case WeaponType.Flower:
                    return "Flower";
                case WeaponType.Cane:
                    return "Cane";
                case WeaponType.Grenade:
                    return "Grenade";
                case WeaponType.Teargas:
                    return "Tear Gas";
                case WeaponType.Moltov:
                    return "Molotov";
                case WeaponType.Colt45:
                    return "Colt-45";
                case WeaponType.Silenced:
                    return "Silenced Pistol";
                case WeaponType.Deagle:
                    return "Desert Eagle";
                case WeaponType.Shotgun:
                    return "Shotgun";
                case WeaponType.Sawedoff:
                    return "Sawed-Off Shotgun";
                case WeaponType.CombatShotgun:
                    return "Combat Shotgun";
                case WeaponType.Uzi:
                    return "UZI";
                case WeaponType.MP5:
                    return "MP5";
                case WeaponType.AK47:
                    return "AK47";
                case WeaponType.M4:
                    return "M4";
                case WeaponType.Tec9:
                    return "Tec-9";
                case WeaponType.Rifle:
                    return "Rifle";
                case WeaponType.Sniper:
                    return "Sniper Rifle";
                case WeaponType.RocketLauncher:
                    return "RPG";
                case WeaponType.HeatSeeker:
                    return "Heat-Seeking Rocket";
                case WeaponType.FlameThrower:
                    return "Flamethrower";
                case WeaponType.Minigun:
                    return "Minigun";
                case WeaponType.SatchelCharge:
                    return "Satchel Charge";
                case WeaponType.Detonator:
                    return "Detonator";
                case WeaponType.Spraycan:
                    return "Spray Can";
                case WeaponType.FireExtinguisher:
                    return "Fire Extinguisher";
                case WeaponType.Camera:
                    return "Camera";
                case WeaponType.NightVisionGoggles:
                    return "Night Vision Goggles";
                case WeaponType.ThermalGoggles:
                    return "Thermal Goggles";
                case WeaponType.Parachute:
                    return "Parachute";
                case WeaponType.FakePistol:
                    return "Fake Pistol";
                case WeaponType.Vehicle:
                    return "Vehicle";
                case WeaponType.HelicopterBlades:
                    return "Helicopter Blades";
                case WeaponType.Explosion:
                    return "Explosion";
                case WeaponType.Drown:
                    return "Drown";
                case WeaponType.Collision:
                    return "Collision";
                case WeaponType.Connect:
                    return "Connection";
                case WeaponType.Disconnect:
                    return "Disconnection";
                case WeaponType.Suicide:
                    return "Suicide";
                default:
                    throw new ArgumentOutOfRangeException(nameof(weaponId), weaponId, null);
            }
        }

        /// <summary>
        /// Can this weapon shoot bullets?
        /// </summary>
        /// <remarks>throwables and spray cans are NOT considered bullets</remarks>
        /// <returns>
        /// true if the weapon can shoot bullets.
        /// false if the weapon cannot shoot bullet.
        /// </returns>
        private static bool CanWeaponShoot(WeaponType weaponId)
        {
            if (weaponId == WeaponType.Colt45)
                return true;
            if (weaponId == WeaponType.Silenced)
                return true;
            if (weaponId == WeaponType.Deagle)
                return true;
            if (weaponId == WeaponType.Shotgun)
                return true;
            if (weaponId == WeaponType.Sawedoff)
                return true;
            if (weaponId == WeaponType.CombatShotgun)
                return true;
            if (weaponId == WeaponType.Uzi)
                return true;
            if (weaponId == WeaponType.MP5)
                return true;
            if (weaponId == WeaponType.AK47)
                return true;
            if (weaponId == WeaponType.M4)
                return true;
            if (weaponId == WeaponType.Tec9)
                return true;
            if (weaponId == WeaponType.Rifle)
                return true;
            if (weaponId == WeaponType.Sniper)
                return true;
            if (weaponId == WeaponType.RocketLauncher)
                return true;
            if (weaponId == WeaponType.HeatSeeker)
                return true;
            if (weaponId == WeaponType.Minigun)
                return true;
            return false;
        }
    }
}
