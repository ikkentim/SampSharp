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

namespace SampSharp.Entities.SAMP;

/// <summary>Contains all pickup types</summary>
public enum PickupType
{
    /// <summary>
    /// The pickup does not always display. If displayed, it can't be picked up and does not trigger OnPlayerPickUpPickup and it will stay after server
    /// shutdown.
    /// </summary>
    ShowButNotPickupable = 0,

    /// <summary>
    /// Exists always. Disables pickup scripts such as horseshoes and oysters to allow for scripted actions ONLY. Will trigger OnPlayerPickUpPickup every few
    /// seconds.
    /// </summary>
    ScriptedActionsOnlyEveryFewSeconds = 1,

    /// <summary>Disappears after pickup, respawns after 30 seconds if the player is at a distance of at least 15 meters.</summary>
    ShowNearAndRespawnWhenPickup = 2,

    /// <summary>Disappears after pickup, respawns after death.</summary>
    ShowAndRespawnWhenDeath = 3,

    /// <summary>Disappears after 15 to 20 seconds. Respawns after death.</summary>
    ShowTemporary1520AndRespawnWhenDeath = 4,

    /// <summary>Disappears after pickup, but has no effect.</summary>
    ShowTillPickedUp = 8,

    /// <summary>Blows up a few seconds after being created (bombs?)</summary>
    ShowAndExplode = 11,

    /// <summary>Blows up a few seconds after being created.</summary>
    ShowAndExplode2 = 12,

    /// <summary>Invisible. Triggers checkpoint sound when picked up with a vehicle, but doesn't trigger OnPlayerPickUpPickup.</summary>
    HiddenPlaySoundButNotPickupable = 13,

    /// <summary>Disappears after pickup, can only be picked up with a vehicle. Triggers checkpoint sound.</summary>
    ShowAndPickupableWithVehicleWithSound = 14,

    /// <summary>Disappears after pickup, respawns after 30 seconds if the player is at a distance of at least 15 meters.</summary>
    ShowNearAndRespawnWhenPickup2 = 15,

    /// <summary>Similar to type 1. Pressing Tab (KEY_ACTION) makes it disappear but the key press doesn't trigger OnPlayerPickUpPickup.</summary>
    ScriptedActionsOnlyEveryFewSecondsButTabDisappear = 18,

    /// <summary>Disappears after pickup, but doesn't respawn. Makes "cash pickup" sound if picked up.</summary>
    ShowAndNoRespawnAfterPickupWithSound = 19,

    /// <summary>
    /// Similar to type 1. Disappears when you take a picture of it with the Camera weapon, which triggers "Snapshot # out of 0" message. Taking a picture
    /// doesn't trigger OnPlayerPickUpPickup.
    /// </summary>
    ScriptedActionsOnlyEveryFewSecondsButCameraWillDestroy = 20,

    /// <summary>Disappears after pickup, respawns after death.</summary>
    ShowAndRespawnWhenDeath2 = 22
}