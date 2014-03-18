using GameMode.World;

namespace GameMode.Controllers
{
    public class PlayerController : IController
    {
        public void RegisterEvents(Server server)
        {
            server.PlayerConnected += (sender, args) => Player.Find(args.PlayerId).OnConnected(args);
            server.PlayerDisconnected += (sender, args) => Player.Find(args.PlayerId).OnDisconnected(args);
            server.PlayerSpawned += (sender, args) => Player.Find(args.PlayerId).OnSpawned(args);
            server.PlayerDied += (sender, args) => Player.Find(args.PlayerId).OnDeath(args);
            server.PlayerText += (sender, args) => Player.Find(args.PlayerId).OnText(args);
            server.PlayerCommandText += (sender, args) => Player.Find(args.PlayerId).OnCommandText(args);
            server.PlayerRequestClass += (sender, args) => Player.Find(args.PlayerId).OnRequestClass(args);
            server.PlayerEnterVehicle += (sender, args) => Player.Find(args.PlayerId).OnEnterVehicle(args);
            server.PlayerExitVehicle += (sender, args) => Player.Find(args.PlayerId).OnExitVehicle(args);
            server.PlayerStateChanged += (sender, args) => Player.Find(args.PlayerId).OnStateChanged(args);
            server.PlayerEnterCheckpoint += (sender, args) => Player.Find(args.PlayerId).OnEnterCheckpoint(args);
            server.PlayerLeaveCheckpoint += (sender, args) => Player.Find(args.PlayerId).OnLeaveCheckpoint(args);
            server.PlayerEnterRaceCheckpoint += (sender, args) => Player.Find(args.PlayerId).OnEnterRaceCheckpoint(args);
            server.PlayerLeaveRaceCheckpoint += (sender, args) => Player.Find(args.PlayerId).OnLeaveRaceCheckpoint(args);
            server.PlayerRequestSpawn += (sender, args) => Player.Find(args.PlayerId).OnRequestSpawn(args);
            server.PlayerPickUpPickup += (sender, args) => Player.Find(args.PickupId).OnPickUpPickup(args);
            server.PlayerEnterExitModShop += (sender, args) => Player.Find(args.PlayerId).OnEnterExitModShop(args);
            server.PlayerSelectedMenuRow += (sender, args) => Player.Find(args.PlayerId).OnSelectedMenuRow(args);
            server.PlayerExitedMenu += (sender, args) => Player.Find(args.PlayerId).OnExitedMenu(args);
            server.PlayerInteriorChanged += (sender, args) => Player.Find(args.PlayerId).OnInteriorChanged(args);
            server.PlayerKeyStateChanged += (sender, args) => Player.Find(args.PlayerId).OnKeyStateChanged(args);
            server.PlayerUpdate += (sender, args) => Player.Find(args.PlayerId).OnUpdate(args);
            server.PlayerStreamIn += (sender, args) => Player.Find(args.PlayerId).OnStreamIn(args);
            server.PlayerStreamOut += (sender, args) => Player.Find(args.PlayerId).OnStreamOut(args);
            server.DialogResponse += (sender, args) => Player.Find(args.PlayerId).OnDialogResponse(args);
            server.PlayerTakeDamage += (sender, args) => Player.Find(args.PlayerId).OnTakeDamage(args);
            server.PlayerGiveDamage += (sender, args) => Player.Find(args.PlayerId).OnGiveDamage(args);
            server.PlayerClickMap += (sender, args) => Player.Find(args.PlayerId).OnClickMap(args);
            server.PlayerClickTextDraw += (sender, args) => Player.Find(args.PlayerId).OnClickTextDraw(args);
            server.PlayerClickPlayerTextDraw += (sender, args) => Player.Find(args.PlayerId).OnClickPlayerTextDraw(args);
            server.PlayerClickPlayer += (sender, args) => Player.Find(args.PlayerId).OnClickPlayer(args);
            server.PlayerEditObject += (sender, args) => Player.Find(args.PlayerId).OnEditObject(args);
            server.PlayerEditAttachedObject += (sender, args) => Player.Find(args.PlayerId).OnEditAttachedObject(args);
            server.PlayerSelectObject += (sender, args) => Player.Find(args.PlayerId).OnSelectObject(args);
            server.PlayerWeaponShot += (sender, args) => Player.Find(args.PlayerId).OnWeaponShot(args);
        }

    }
}
