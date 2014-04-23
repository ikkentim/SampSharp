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

using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all player actions.
    /// </summary>
    public class PlayerController : IController
    {
        /// <summary>
        ///     Registers the events this PlayerController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            //Register all player events
            gameMode.PlayerConnected += (sender, args) => Player.Find(args.PlayerId).OnConnected(args);
            gameMode.PlayerDisconnected += (sender, args) => Player.Find(args.PlayerId).OnDisconnected(args);
            gameMode.PlayerSpawned += (sender, args) => Player.Find(args.PlayerId).OnSpawned(args);
            gameMode.PlayerDied += (sender, args) => Player.Find(args.PlayerId).OnDeath(args);
            gameMode.PlayerText += (sender, args) => Player.Find(args.PlayerId).OnText(args);
            gameMode.PlayerCommandText += (sender, args) => Player.Find(args.PlayerId).OnCommandText(args);
            gameMode.PlayerRequestClass += (sender, args) => Player.Find(args.PlayerId).OnRequestClass(args);
            gameMode.PlayerEnterVehicle += (sender, args) => Player.Find(args.PlayerId).OnEnterVehicle(args);
            gameMode.PlayerExitVehicle += (sender, args) => Player.Find(args.PlayerId).OnExitVehicle(args);
            gameMode.PlayerStateChanged += (sender, args) => Player.Find(args.PlayerId).OnStateChanged(args);
            gameMode.PlayerEnterCheckpoint += (sender, args) => Player.Find(args.PlayerId).OnEnterCheckpoint(args);
            gameMode.PlayerLeaveCheckpoint += (sender, args) => Player.Find(args.PlayerId).OnLeaveCheckpoint(args);
            gameMode.PlayerEnterRaceCheckpoint +=
                (sender, args) => Player.Find(args.PlayerId).OnEnterRaceCheckpoint(args);
            gameMode.PlayerLeaveRaceCheckpoint +=
                (sender, args) => Player.Find(args.PlayerId).OnLeaveRaceCheckpoint(args);
            gameMode.PlayerRequestSpawn += (sender, args) => Player.Find(args.PlayerId).OnRequestSpawn(args);
            gameMode.PlayerPickUpPickup += (sender, args) => Player.Find(args.PickupId).OnPickUpPickup(args);
            gameMode.PlayerEnterExitModShop += (sender, args) => Player.Find(args.PlayerId).OnEnterExitModShop(args);
            gameMode.PlayerSelectedMenuRow += (sender, args) => Player.Find(args.PlayerId).OnSelectedMenuRow(args);
            gameMode.PlayerExitedMenu += (sender, args) => Player.Find(args.PlayerId).OnExitedMenu(args);
            gameMode.PlayerInteriorChanged += (sender, args) => Player.Find(args.PlayerId).OnInteriorChanged(args);
            gameMode.PlayerKeyStateChanged += (sender, args) => Player.Find(args.PlayerId).OnKeyStateChanged(args);
            gameMode.PlayerUpdate += (sender, args) => Player.Find(args.PlayerId).OnUpdate(args);
            gameMode.PlayerStreamIn += (sender, args) => Player.Find(args.PlayerId).OnStreamIn(args);
            gameMode.PlayerStreamOut += (sender, args) => Player.Find(args.PlayerId).OnStreamOut(args);
            gameMode.DialogResponse += (sender, args) => Player.Find(args.PlayerId).OnDialogResponse(args);
            gameMode.PlayerTakeDamage += (sender, args) => Player.Find(args.PlayerId).OnTakeDamage(args);
            gameMode.PlayerGiveDamage += (sender, args) => Player.Find(args.PlayerId).OnGiveDamage(args);
            gameMode.PlayerClickMap += (sender, args) => Player.Find(args.PlayerId).OnClickMap(args);
            gameMode.PlayerClickTextDraw += (sender, args) => Player.Find(args.PlayerId).OnClickTextDraw(args);
            gameMode.PlayerClickPlayerTextDraw +=
                (sender, args) => Player.Find(args.PlayerId).OnClickPlayerTextDraw(args);
            gameMode.PlayerClickPlayer += (sender, args) => Player.Find(args.PlayerId).OnClickPlayer(args);
            gameMode.PlayerEditObject += (sender, args) => Player.Find(args.PlayerId).OnEditObject(args);
            gameMode.PlayerEditAttachedObject += (sender, args) => Player.Find(args.PlayerId).OnEditAttachedObject(args);
            gameMode.PlayerSelectObject += (sender, args) => Player.Find(args.PlayerId).OnSelectObject(args);
            gameMode.PlayerWeaponShot += (sender, args) => Player.Find(args.PlayerId).OnWeaponShot(args);
        }

        /// <summary>
        ///     Registers types this PlayerController requires the system to use.
        /// </summary>
        public virtual void RegisterTypes()
        {
            Player.Register<Player>();
        }
    }
}