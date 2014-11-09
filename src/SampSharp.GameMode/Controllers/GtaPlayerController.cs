// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

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
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnDisconnected(args);
            };
            gameMode.PlayerCleanup += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnCleanup(args);
            };
            gameMode.PlayerSpawned += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnSpawned(args);
            };
            gameMode.PlayerDied += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnDeath(args);
            };
            gameMode.PlayerText += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnText(args);
            };
            gameMode.PlayerCommandText += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnCommandText(args);
            };
            gameMode.PlayerRequestClass += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnRequestClass(args);
            };
            gameMode.PlayerEnterVehicle += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnEnterVehicle(args);
            };
            gameMode.PlayerExitVehicle += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnExitVehicle(args);
            };
            gameMode.PlayerStateChanged += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnStateChanged(args);
            };
            gameMode.PlayerEnterCheckpoint += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnEnterCheckpoint(args);
            };
            gameMode.PlayerLeaveCheckpoint += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnLeaveCheckpoint(args);
            };
            gameMode.PlayerEnterRaceCheckpoint += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnEnterRaceCheckpoint(args);
            };
            gameMode.PlayerLeaveRaceCheckpoint += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnLeaveRaceCheckpoint(args);
            };
            gameMode.PlayerRequestSpawn += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnRequestSpawn(args);
            };
            gameMode.PlayerPickUpPickup += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnPickUpPickup(args);
            };
            gameMode.PlayerEnterExitModShop += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnEnterExitModShop(args);
            };
            gameMode.PlayerSelectedMenuRow += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnSelectedMenuRow(args);
            };
            gameMode.PlayerExitedMenu += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnExitedMenu(args);
            };
            gameMode.PlayerInteriorChanged += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnInteriorChanged(args);
            };
            gameMode.PlayerKeyStateChanged += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnKeyStateChanged(args);
            };
            gameMode.PlayerUpdate += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnUpdate(args);
            };
            gameMode.PlayerStreamIn += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnStreamIn(args);
            };
            gameMode.PlayerStreamOut += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnStreamOut(args);
            };
            gameMode.DialogResponse += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnDialogResponse(args);
            };
            gameMode.PlayerTakeDamage += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnTakeDamage(args);
            };
            gameMode.PlayerGiveDamage += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnGiveDamage(args);
            };
            gameMode.PlayerClickMap += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnClickMap(args);
            };
            gameMode.PlayerClickTextDraw += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (args.TextDrawId == TextDraw.InvalidId)
                {
                    
                }
                else
                {
                    if (player != null)
                        player.OnClickTextDraw(args);
                }
            };
            gameMode.PlayerClickPlayerTextDraw += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnClickPlayerTextDraw(args);
            };
            gameMode.PlayerClickPlayer += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnClickPlayer(args);
            };
            gameMode.PlayerEditObject += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnEditObject(args);
            };
            gameMode.PlayerEditAttachedObject += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnEditAttachedObject(args);
            };
            gameMode.PlayerSelectObject += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

                if (player != null)
                    player.OnSelectObject(args);
            };
            gameMode.PlayerWeaponShot += (sender, args) =>
            {
                var player = GtaPlayer.Find(args.PlayerId);

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