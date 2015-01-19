// SampSharp
// Copyright 2015 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.VehicleDied" />,
    ///     <see cref="BaseMode.PlayerExitVehicle" />, <see cref="BaseMode.VehicleStreamIn" /> or
    ///     <see cref="BaseMode.VehicleStreamOut" /> event.
    /// </summary>
    public class PlayerVehicleEventArgs : PlayerEventArgs
    {
        public PlayerVehicleEventArgs(int playerid, int vehicleid) : base(playerid)
        {
            VehicleId = vehicleid;
        }

        public int VehicleId { get; private set; }

        public GtaVehicle Vehicle
        {
            get { return VehicleId == GtaVehicle.InvalidId ? null : GtaVehicle.Find(VehicleId); }
        }
    }


    /// <summary>
    ///     Provides data for the <see cref="BaseMode.TrailerUpdate" /> event.
    /// </summary>
    public class PlayerTrailerEventArgs : PlayerEventArgs
    {
        public PlayerTrailerEventArgs(int playerid, int vehicleid)
            : base(playerid)
        {
            VehicleId = vehicleid;
        }

        public int VehicleId { get; private set; }

        public GtaVehicle Vehicle
        {
            get { return VehicleId == GtaVehicle.InvalidId ? null : GtaVehicle.Find(VehicleId); }
        }

        /// <summary>
        ///     Gets or sets whether to stop the vehicle syncing its position to other players.
        /// </summary>
        public bool PreventPropagation { get; set; }
    }
}