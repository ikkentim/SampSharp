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

using SampSharp.GameMode.Display;
using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all player actions.
    /// </summary>
    public class GtaPlayerController : Disposable, IEventListener, ITypeProvider
    {
        /// <summary>
        ///     Registers the events this PlayerController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            //Register all player events
            gameMode.PlayerConnected += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnConnected(args);
            gameMode.PlayerDisconnected += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnDisconnected(args);
            };
            gameMode.PlayerCleanup += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnCleanup(args);
            };
            gameMode.PlayerSpawned += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnSpawned(args);
            };
            gameMode.PlayerDied += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnDeath(args);
            };
            gameMode.PlayerText += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnText(args);
            };
            gameMode.PlayerCommandText += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnCommandText(args);
            };
            gameMode.PlayerRequestClass += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnRequestClass(args);
            };
            gameMode.PlayerEnterVehicle += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnEnterVehicle(args);
            };
            gameMode.PlayerExitVehicle += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnExitVehicle(args);
            };
            gameMode.PlayerStateChanged += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnStateChanged(args);
            };
            gameMode.PlayerEnterCheckpoint += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnEnterCheckpoint(args);
            };
            gameMode.PlayerLeaveCheckpoint += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnLeaveCheckpoint(args);
            };
            gameMode.PlayerEnterRaceCheckpoint += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnEnterRaceCheckpoint(args);
            };
            gameMode.PlayerLeaveRaceCheckpoint += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnLeaveRaceCheckpoint(args);
            };
            gameMode.PlayerRequestSpawn += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnRequestSpawn(args);
            };
            gameMode.PlayerPickUpPickup += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnPickUpPickup(args);
            };
            gameMode.PlayerEnterExitModShop += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnEnterExitModShop(args);
            };
            gameMode.PlayerSelectedMenuRow += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnSelectedMenuRow(args);
            };
            gameMode.PlayerExitedMenu += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnExitedMenu(args);
            };
            gameMode.PlayerInteriorChanged += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnInteriorChanged(args);
            };
            gameMode.PlayerKeyStateChanged += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnKeyStateChanged(args);
            };
            gameMode.PlayerUpdate += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnUpdate(args);
            };
            gameMode.PlayerStreamIn += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnStreamIn(args);
            };
            gameMode.PlayerStreamOut += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnStreamOut(args);
            };
            gameMode.DialogResponse += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnDialogResponse(args);
            };
            gameMode.PlayerTakeDamage += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnTakeDamage(args);
            };
            gameMode.PlayerGiveDamage += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnGiveDamage(args);
            };
            gameMode.PlayerClickMap += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnClickMap(args);
            };
            gameMode.PlayerClickTextDraw += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (args.TextDrawId == TextDraw.InvalidId)
                {
                    if (player != null)
                        player.OnCancelClickTextDraw(args);
                }
                else
                {
                    if (player != null)
                        player.OnClickTextDraw(args);
                }
            };
            gameMode.PlayerClickPlayerTextDraw += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnClickPlayerTextDraw(args);
            };
            gameMode.PlayerClickPlayer += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnClickPlayer(args);
            };
            gameMode.PlayerEditObject += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnEditObject(args);
            };
            gameMode.PlayerEditAttachedObject += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnEditAttachedObject(args);
            };
            gameMode.PlayerSelectObject += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnSelectObject(args);
            };
            gameMode.PlayerWeaponShot += (sender, args) =>
            {
                GtaPlayer player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnWeaponShot(args);
            };
        }

        /// <summary>
        ///     Registers types this PlayerController requires the system to use.
        /// </summary>
        public virtual void RegisterTypes()
        {
            GtaPlayer.Register<GtaPlayer>();
        }

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (GtaPlayer player in GtaPlayer.All)
                {
                    player.Dispose();
                }
            }
        }
    }
}