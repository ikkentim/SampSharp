using GameMode;
using GameMode.Controllers;
using TestMode.World;

namespace TestMode.Controllers
{
    class MyPlayerContoller : IController
    {
        public void RegisterEvents(BaseMode gameMode)
        {
            //Register all MyPlayer events
            gameMode.PlayerConnected += (sender, args) => MyPlayer.Find(args.PlayerId).OnConnected(args);
            gameMode.PlayerDisconnected += (sender, args) => MyPlayer.Find(args.PlayerId).OnDisconnected(args);
            gameMode.PlayerSpawned += (sender, args) => MyPlayer.Find(args.PlayerId).OnSpawned(args);
            gameMode.PlayerDied += (sender, args) => MyPlayer.Find(args.PlayerId).OnDeath(args);
            gameMode.PlayerText += (sender, args) => MyPlayer.Find(args.PlayerId).OnText(args);
            gameMode.PlayerCommandText += (sender, args) => MyPlayer.Find(args.PlayerId).OnCommandText(args);
            gameMode.PlayerRequestClass += (sender, args) => MyPlayer.Find(args.PlayerId).OnRequestClass(args);
            gameMode.PlayerEnterVehicle += (sender, args) => MyPlayer.Find(args.PlayerId).OnEnterVehicle(args);
            gameMode.PlayerExitVehicle += (sender, args) => MyPlayer.Find(args.PlayerId).OnExitVehicle(args);
            gameMode.PlayerStateChanged += (sender, args) => MyPlayer.Find(args.PlayerId).OnStateChanged(args);
            gameMode.PlayerEnterCheckpoint += (sender, args) => MyPlayer.Find(args.PlayerId).OnEnterCheckpoint(args);
            gameMode.PlayerLeaveCheckpoint += (sender, args) => MyPlayer.Find(args.PlayerId).OnLeaveCheckpoint(args);
            gameMode.PlayerEnterRaceCheckpoint += (sender, args) => MyPlayer.Find(args.PlayerId).OnEnterRaceCheckpoint(args);
            gameMode.PlayerLeaveRaceCheckpoint += (sender, args) => MyPlayer.Find(args.PlayerId).OnLeaveRaceCheckpoint(args);
            gameMode.PlayerRequestSpawn += (sender, args) => MyPlayer.Find(args.PlayerId).OnRequestSpawn(args);
            gameMode.PlayerPickUpPickup += (sender, args) => MyPlayer.Find(args.PickupId).OnPickUpPickup(args);
            gameMode.PlayerEnterExitModShop += (sender, args) => MyPlayer.Find(args.PlayerId).OnEnterExitModShop(args);
            gameMode.PlayerSelectedMenuRow += (sender, args) => MyPlayer.Find(args.PlayerId).OnSelectedMenuRow(args);
            gameMode.PlayerExitedMenu += (sender, args) => MyPlayer.Find(args.PlayerId).OnExitedMenu(args);
            gameMode.PlayerInteriorChanged += (sender, args) => MyPlayer.Find(args.PlayerId).OnInteriorChanged(args);
            gameMode.PlayerKeyStateChanged += (sender, args) => MyPlayer.Find(args.PlayerId).OnKeyStateChanged(args);
            gameMode.PlayerUpdate += (sender, args) => MyPlayer.Find(args.PlayerId).OnUpdate(args);
            gameMode.PlayerStreamIn += (sender, args) => MyPlayer.Find(args.PlayerId).OnStreamIn(args);
            gameMode.PlayerStreamOut += (sender, args) => MyPlayer.Find(args.PlayerId).OnStreamOut(args);
            gameMode.DialogResponse += (sender, args) => MyPlayer.Find(args.PlayerId).OnDialogResponse(args);
            gameMode.PlayerTakeDamage += (sender, args) => MyPlayer.Find(args.PlayerId).OnTakeDamage(args);
            gameMode.PlayerGiveDamage += (sender, args) => MyPlayer.Find(args.PlayerId).OnGiveDamage(args);
            gameMode.PlayerClickMap += (sender, args) => MyPlayer.Find(args.PlayerId).OnClickMap(args);
            gameMode.PlayerClickTextDraw += (sender, args) => MyPlayer.Find(args.PlayerId).OnClickTextDraw(args);
            gameMode.PlayerClickPlayerTextDraw += (sender, args) => MyPlayer.Find(args.PlayerId).OnClickPlayerTextDraw(args);
            gameMode.PlayerClickPlayer += (sender, args) => MyPlayer.Find(args.PlayerId).OnClickPlayer(args);
            gameMode.PlayerEditObject += (sender, args) => MyPlayer.Find(args.PlayerId).OnEditObject(args);
            gameMode.PlayerEditAttachedObject += (sender, args) => MyPlayer.Find(args.PlayerId).OnEditAttachedObject(args);
            gameMode.PlayerSelectObject += (sender, args) => MyPlayer.Find(args.PlayerId).OnSelectObject(args);
            gameMode.PlayerWeaponShot += (sender, args) => MyPlayer.Find(args.PlayerId).OnWeaponShot(args);
        }
    }
}
