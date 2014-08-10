using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.Pools;

namespace SampSharp.GameMode.World
{
    public class Pickup : IdentifiedPool<Pickup>, IIdentifyable, IWorldObject
    {
        public event EventHandler<PlayerPickupEventArgs> PlayerPickup;

        #region Properties
        /// <summary>
        /// The ID of the Pickup.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// The position in the world of the pickup.
        /// 
        /// After creation, a pickup <b>cannot</b> be moved. So any change of the position won't affect the in-game object.
        /// </summary>
        public Vector Position { get; set; }

        /// <summary>
        /// The virtualworld assigned to this pickup
        /// </summary>
        public int VirtualWorld { get; private set; }

        /// <summary>
        /// The model of the pickup.
        /// 
        /// <seealso cref="http://wiki.sa-mp.com/wiki/Pickup_IDs"/> Model pickups id list
        /// </summary>
        public int Model { get; private set; }

        /// <summary>
        /// It's the type of the pickup.
        /// 
        /// <seealso cref="http://wiki.sa-mp.com/wiki/PickupTypes"/> List of pickup types
        /// </summary>
        public int SpawnType { get; private set; }
        #endregion

        public Pickup(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Create a pickup in the game.
        /// 
        /// It's the equivalent of call CreatePickup in a PAWN script.
        /// </summary>
        /// <param name="model">The model of the pickup</param>
        /// <param name="type">The pickup spawn type.</param>
        /// <param name="position">The position where the pickup should be spawned</param>
        /// <param name="virtualWorld">The virtual world ID of the pickup. By default, it will be -1 which shows the pickup in all worlds.</param>
        /// <returns>The created pickup or null if it cannot be created</returns>
        public static Pickup Create(int model, int type, Vector position, int virtualWorld = -1)
        {
            var id = Native.CreatePickup(model, type, position.X, position.Y, position.Z, virtualWorld);

            var pickup = id == -1 ? null : FindOrCreate(id);

            if (pickup == null)
            {
                return null;
            }

            pickup.Position = position;
            pickup.VirtualWorld = virtualWorld;
            pickup.Model = model;
            pickup.SpawnType = type;
            return pickup;
        }

        /// <summary>
        /// Create a pickup in the game.
        /// 
        /// It's the equivalent of call AddStaticPickup in a PAWN script.
        /// <seealso cref="http://wiki.sa-mp.com/wiki/CreatePickup"/>
        /// </summary>
        /// <param name="model">The model of the pickup</param>
        /// <param name="type">The pickup spawn type.</param>
        /// <param name="position">The position where the pickup should be spawned</param>
        /// <param name="virtualWorld">The virtual world ID of the pickup. By default, it will be -1 which shows the pickup in all worlds.</param>
        /// <returns>True if the pickup has been created</returns>
        public static bool CreateStatic(int model, int type, Vector position, int virtualWorld = -1)
        {
            return Native.AddStaticPickup(model, type, position.X, position.Y, position.Z, virtualWorld) == 1;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Native.DestroyPickup(Id);
        }

        #region Methods
        public override int GetHashCode()
        {
            return Id;
        }

        public override string ToString()
        {
            return string.Format("Pickup(Id: {0}, Model: {1})", Id, Model);
        }
        #endregion

        #region Events

        public virtual void OnPickup(PlayerPickupEventArgs e)
        {
            if (PlayerPickup != null)
            {
                PlayerPickup(this, e);
            }
        }
        #endregion
    }
}
