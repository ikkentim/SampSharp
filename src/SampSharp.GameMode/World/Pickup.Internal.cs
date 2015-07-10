using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode.Natives;

namespace SampSharp.GameMode.World
{
    public partial class Pickup
    {
        private static class Internal
        {
            public delegate int AddStaticPickupImpl(int model, int type, float x, float y, float z, int virtualworld);

            public delegate int CreatePickupImpl(int model, int type, float x, float y, float z, int virtualworld);

            public delegate bool DestroyPickupImpl(int pickupid);

            [Native("AddStaticPickup")]
            public static readonly AddStaticPickupImpl AddStaticPickup = null;
            [Native("CreatePickup")]
            public static readonly CreatePickupImpl CreatePickup = null;
            [Native("DestroyPickup")]
            public static readonly DestroyPickupImpl DestroyPickup = null;

        }
    }
}
