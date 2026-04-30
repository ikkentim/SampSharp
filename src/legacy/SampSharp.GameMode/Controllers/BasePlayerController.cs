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

using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers;

/// <summary>A controller processing all player actions.</summary>
[Controller]
public class BasePlayerController : Disposable, IEventListener, ITypeProvider
{
    /// <summary>Registers the events this PlayerController wants to listen to.</summary>
    /// <param name="gameMode">The running GameMode.</param>
    public virtual void RegisterEvents(BaseMode gameMode)
    {
        //Register all player events
        gameMode.PlayerConnected += (sender, args) => (sender as BasePlayer)?.OnConnected(args);
        gameMode.PlayerDisconnected += (sender, args) => (sender as BasePlayer)?.OnDisconnected(args);
        gameMode.PlayerCleanup += (sender, args) => (sender as BasePlayer)?.OnCleanup(args);
        gameMode.PlayerSpawned += (sender, args) => (sender as BasePlayer)?.OnSpawned(args);
        gameMode.PlayerDied += (sender, args) => (sender as BasePlayer)?.OnDeath(args);
        gameMode.PlayerText += (sender, args) => (sender as BasePlayer)?.OnText(args);
        gameMode.PlayerCommandText += (sender, args) => (sender as BasePlayer)?.OnCommandText(args);
        gameMode.PlayerRequestClass += (sender, args) => (sender as BasePlayer)?.OnRequestClass(args);
        gameMode.PlayerEnterVehicle += (sender, args) => (sender as BasePlayer)?.OnEnterVehicle(args);
        gameMode.PlayerExitVehicle += (sender, args) => (sender as BasePlayer)?.OnExitVehicle(args);
        gameMode.PlayerStateChanged += (sender, args) => (sender as BasePlayer)?.OnStateChanged(args);
        gameMode.PlayerEnterCheckpoint += (sender, args) => (sender as BasePlayer)?.OnEnterCheckpoint(args);
        gameMode.PlayerLeaveCheckpoint += (sender, args) => (sender as BasePlayer)?.OnLeaveCheckpoint(args);
        gameMode.PlayerEnterRaceCheckpoint += (sender, args) => (sender as BasePlayer)?.OnEnterRaceCheckpoint(args);
        gameMode.PlayerLeaveRaceCheckpoint += (sender, args) => (sender as BasePlayer)?.OnLeaveRaceCheckpoint(args);
        gameMode.PlayerRequestSpawn += (sender, args) => (sender as BasePlayer)?.OnRequestSpawn(args);
        gameMode.PlayerEnterExitModShop += (sender, args) => (sender as BasePlayer)?.OnEnterExitModShop(args);
        gameMode.PlayerSelectedMenuRow += (sender, args) => (sender as BasePlayer)?.OnSelectedMenuRow(args);
        gameMode.PlayerExitedMenu += (sender, args) => (sender as BasePlayer)?.OnExitedMenu(args);
        gameMode.PlayerInteriorChanged += (sender, args) => (sender as BasePlayer)?.OnInteriorChanged(args);
        gameMode.PlayerKeyStateChanged += (sender, args) => (sender as BasePlayer)?.OnKeyStateChanged(args);
        gameMode.PlayerUpdate += (sender, args) => (sender as BasePlayer)?.OnUpdate(args);
        gameMode.PlayerStreamIn += (sender, args) => (sender as BasePlayer)?.OnStreamIn(args);
        gameMode.PlayerStreamOut += (sender, args) => (sender as BasePlayer)?.OnStreamOut(args);
        gameMode.DialogResponse += (sender, args) => (sender as BasePlayer)?.OnDialogResponse(args);
        gameMode.PlayerTakeDamage += (sender, args) => (sender as BasePlayer)?.OnTakeDamage(args);
        gameMode.PlayerGiveDamage += (sender, args) => (sender as BasePlayer)?.OnGiveDamage(args);
        gameMode.PlayerClickMap += (sender, args) => (sender as BasePlayer)?.OnClickMap(args);
        gameMode.PlayerClickTextDraw += (sender, args) => (sender as BasePlayer)?.OnClickTextDraw(args);
        gameMode.PlayerClickPlayerTextDraw += (sender, args) => (sender as BasePlayer)?.OnClickPlayerTextDraw(args);
        gameMode.PlayerCancelClickTextDraw += (sender, args) => (sender as BasePlayer)?.OnCancelClickTextDraw(args);
        gameMode.PlayerClickPlayer += (sender, args) => (sender as BasePlayer)?.OnClickPlayer(args);
        gameMode.PlayerEditGlobalObject += (sender, args) => (sender as BasePlayer)?.OnEditGlobalObject(args);
        gameMode.PlayerEditPlayerObject += (sender, args) => (sender as BasePlayer)?.OnEditPlayerObject(args);
        gameMode.PlayerEditAttachedObject += (sender, args) => (sender as BasePlayer)?.OnEditAttachedObject(args);
        gameMode.PlayerSelectGlobalObject += (sender, args) => (sender as BasePlayer)?.OnSelectGlobalObject(args);
        gameMode.PlayerSelectPlayerObject += (sender, args) => (sender as BasePlayer)?.OnSelectPlayerObject(args);
        gameMode.PlayerWeaponShot += (sender, args) => (sender as BasePlayer)?.OnWeaponShot(args);
        gameMode.PlayerPickUpPickup += (sender, args) => (sender as BasePlayer)?.OnPickUpPickup(args);
    }

    /// <summary>Registers types this PlayerController requires the system to use.</summary>
    public virtual void RegisterTypes()
    {
        BasePlayer.Register<BasePlayer>();
    }

    /// <summary>Performs tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
    /// <param name="disposing">Whether managed resources should be disposed.</param>
    protected override void Dispose(bool disposing)
    {
        if (!disposing) return;
        
        foreach (var player in BasePlayer.All)
        {
            player.Dispose();
        }
    }
}