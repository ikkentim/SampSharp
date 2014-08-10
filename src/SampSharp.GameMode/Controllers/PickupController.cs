using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    public class PickupController : IEventListener, ITypeProvider
    {
        /// <summary>
        ///     Registers the events this TextDrawController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public void RegisterEvents(BaseMode gameMode)
        {
            gameMode.PlayerPickUpPickup += (sender, args) =>
            {
                var obj = Pickup.Find(args.PickupId);

                if (obj != null)
                {
                    obj.OnPickup(args);
                }
            };
        }

        /// <summary>
        ///     Registers types this PickupController requires the system to use.
        /// </summary>
        public void RegisterTypes()
        {
            Pickup.Register<Pickup>();
        }
    }
}
