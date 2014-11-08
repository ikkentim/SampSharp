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
            gameMode.PlayerDisconnected += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnDisconnected(args);
            gameMode.PlayerCleanup += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnCleanup(args);
            gameMode.PlayerSpawned += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnSpawned(args);
            gameMode.PlayerDied += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnDeath(args);
            gameMode.PlayerText += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnText(args);
            gameMode.PlayerCommandText += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnCommandText(args);
            gameMode.PlayerRequestClass += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnRequestClass(args);
            gameMode.PlayerEnterVehicle += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnEnterVehicle(args);
            gameMode.PlayerExitVehicle += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnExitVehicle(args);
            gameMode.PlayerStateChanged += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnStateChanged(args);
            gameMode.PlayerEnterCheckpoint +=
                (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnEnterCheckpoint(args);
            gameMode.PlayerLeaveCheckpoint +=
                (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnLeaveCheckpoint(args);
            gameMode.PlayerEnterRaceCheckpoint +=
                (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnEnterRaceCheckpoint(args);
            gameMode.PlayerLeaveRaceCheckpoint +=
                (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnLeaveRaceCheckpoint(args);
            gameMode.PlayerRequestSpawn += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnRequestSpawn(args);
            gameMode.PlayerPickUpPickup += (sender, args) => GtaPlayer.FindOrCreate(args.PickupId).OnPickUpPickup(args);
            gameMode.PlayerEnterExitModShop +=
                (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnEnterExitModShop(args);
            gameMode.PlayerSelectedMenuRow +=
                (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnSelectedMenuRow(args);
            gameMode.PlayerExitedMenu += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnExitedMenu(args);
            gameMode.PlayerInteriorChanged +=
                (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnInteriorChanged(args);
            gameMode.PlayerKeyStateChanged +=
                (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnKeyStateChanged(args);
            gameMode.PlayerUpdate += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnUpdate(args);
            gameMode.PlayerStreamIn += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnStreamIn(args);
            gameMode.PlayerStreamOut += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnStreamOut(args);
            gameMode.DialogResponse += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnDialogResponse(args);
            gameMode.PlayerTakeDamage += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnTakeDamage(args);
            gameMode.PlayerGiveDamage += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnGiveDamage(args);
            gameMode.PlayerClickMap += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnClickMap(args);
            gameMode.PlayerClickTextDraw +=
                (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnClickTextDraw(args);
            gameMode.PlayerClickPlayerTextDraw +=
                (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnClickPlayerTextDraw(args);
            gameMode.PlayerClickPlayer += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnClickPlayer(args);
            gameMode.PlayerEditObject += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnEditObject(args);
            gameMode.PlayerEditAttachedObject +=
                (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnEditAttachedObject(args);
            gameMode.PlayerSelectObject += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnSelectObject(args);
            gameMode.PlayerWeaponShot += (sender, args) => GtaPlayer.FindOrCreate(args.PlayerId).OnWeaponShot(args);
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