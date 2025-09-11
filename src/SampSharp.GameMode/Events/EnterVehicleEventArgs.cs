﻿// SampSharp
// Copyright 2022 Tim Potze
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

using System;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events;

/// <summary>
/// Provides data for the <see cref="BaseMode.PlayerEnterVehicle" />, <see cref="BasePlayer.EnterVehicle" /> or <see cref="BaseVehicle.PlayerEnter" />
/// event.
/// </summary>
public class EnterVehicleEventArgs : EventArgs
{
    /// <summary>Initializes a new instance of the <see cref="EnterVehicleEventArgs" /> class.</summary>
    /// <param name="player">The player.</param>
    /// <param name="vehicle">The vehicle.</param>
    /// <param name="isPassenger">if set to <c>true</c> the player is a passenger.</param>
    public EnterVehicleEventArgs(BasePlayer player, BaseVehicle vehicle, bool isPassenger)
    {
        Player = player;
        Vehicle = vehicle;
        IsPassenger = isPassenger;
    }

    /*
     * Since the BaseMode.OnVehicleEnter can either have a GtaPlayer of GtaVehicle instance as sender,
     * we add both to the event args so we can access what's not the sender.
     */

    /// <summary>Gets the player.</summary>
    public BasePlayer Player { get; }

    /// <summary>Gets the vehicle.</summary>
    public BaseVehicle Vehicle { get; }

    /// <summary>Gets a value indicating whether the <see cref="Player" /> is passenger.</summary>
    public bool IsPassenger { get; }
}