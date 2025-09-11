// SampSharp
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

using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events;

/// <summary>Provides data for the <see cref="Npc.WeaponDataChange" /> event.</summary>
public class WeaponStateChangeEventArgs : System.EventArgs
{
    /// <summary>Initializes a new instance of the <see cref="WeaponStateChangeEventArgs" /> class.</summary>
    /// <param name="oldState">The old state.</param>
    /// <param name="newState">The new state.</param>
    public WeaponStateChangeEventArgs(WeaponState oldState, WeaponState newState)
    {
        OldState = oldState;
        NewState = newState;
    }

    /// <summary>Gets the weapon.</summary>
    public WeaponState OldState { get; }
    /// <summary>Gets the weapon.</summary>
    public WeaponState NewState { get; }
}