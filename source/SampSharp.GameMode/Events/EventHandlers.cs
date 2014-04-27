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

using System;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.Initialized" /> or <see cref="BaseMode.Exited" />
    ///     event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="GameModeEventArgs" /> that contains the event data.</param>
    public delegate void GameModeHandler(object sender, GameModeEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerConnected" />,
    ///     <see cref="BaseMode.PlayerSpawned" />, <see cref="BaseMode.PlayerEnterCheckpoint" />,
    ///     <see cref="BaseMode.PlayerLeaveCheckpoint" />, <see cref="BaseMode.PlayerEnterRaceCheckpoint" />,
    ///     <see cref="BaseMode.PlayerLeaveRaceCheckpoint" />, <see cref="BaseMode.PlayerRequestSpawn" />,
    ///     <see cref="BaseMode.VehicleDamageStatusUpdated" />, <see cref="BaseMode.PlayerExitedMenu" /> or
    ///     <see cref="BaseMode.PlayerUpdate" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerEventArgs" /> that contains the event data.</param>
    public delegate void PlayerHandler(object sender, PlayerEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerDisconnected" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerDisconnectedEventArgs" /> that contains the event data.</param>
    public delegate void PlayerDisconnectedHandler(object sender, PlayerDisconnectedEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerDied" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerDeathEventArgs" /> that contains the event data.</param>
    public delegate void PlayerDeathHandler(object sender, PlayerDeathEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.VehicleSpawned" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="VehicleEventArgs" /> that contains the event data.</param>
    public delegate void VehicleHandler(object sender, VehicleEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerText" /> or
    ///     <see cref="BaseMode.PlayerCommandText" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerTextEventArgs" /> that contains the event data.</param>
    public delegate void PlayerTextHandler(object sender, PlayerTextEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerRequestClass" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerRequestClassEventArgs" /> that contains the event data.</param>
    public delegate void PlayerRequestClassHandler(object sender, PlayerRequestClassEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerEnterVehicle" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerEnterVehicleEventArgs" /> that contains the event data.</param>
    public delegate void PlayerEnterVehicleHandler(object sender, PlayerEnterVehicleEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.VehicleDied" />,
    ///     <see cref="BaseMode.PlayerExitVehicle" />, <see cref="BaseMode.VehicleStreamIn" /> or
    ///     <see cref="BaseMode.VehicleStreamOut" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerVehicleEventArgs" /> that contains the event data.</param>
    public delegate void PlayerVehicleHandler(object sender, PlayerVehicleEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerStateChanged" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerStateEventArgs" /> that contains the event data.</param>
    public delegate void PlayerStateHandler(object sender, PlayerStateEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.RconCommand" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="RconEventArgs" /> that contains the event data.</param>
    public delegate void RconHandler(object sender, RconEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.ObjectMoved" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="ObjectEventArgs" /> that contains the event data.</param>
    public delegate void ObjectHandler(object sender, ObjectEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerObjectMoved" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerObjectEventArgs" /> that contains the event data.</param>
    public delegate void PlayerObjectHandler(object sender, PlayerObjectEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerPickUpPickup" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerPickupEventArgs" /> that contains the event data.</param>
    public delegate void PlayerPickupHandler(object sender, PlayerPickupEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.VehicleMod" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="VehicleModEventArgs" /> that contains the event data.</param>
    public delegate void VehicleModHandler(object sender, VehicleModEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerEnterExitModShop" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerEnterModShopEventArgs" /> that contains the event data.</param>
    public delegate void PlayerEnterModShopHandler(object sender, PlayerEnterModShopEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.VehiclePaintjobApplied" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="VehiclePaintjobEventArgs" /> that contains the event data.</param>
    public delegate void VehiclePaintjobHandler(object sender, VehiclePaintjobEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.VehicleResprayed" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="VehicleResprayedEventArgs" /> that contains the event data.</param>
    public delegate void VehicleResprayedHandler(object sender, VehicleResprayedEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.UnoccupiedVehicleUpdated" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="UnoccupiedVehicleEventArgs" /> that contains the event data.</param>
    public delegate void UnoccupiedVehicleUpdatedHandler(object sender, UnoccupiedVehicleEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerSelectedMenuRow" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerSelectedMenuRowEventArgs" /> that contains the event data.</param>
    public delegate void PlayerSelectedMenuRowHandler(object sender, PlayerSelectedMenuRowEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerInteriorChanged" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerInteriorChangedEventArgs" /> that contains the event data.</param>
    public delegate void PlayerInteriorChangedHandler(object sender, PlayerInteriorChangedEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerKeyStateChanged" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerKeyStateChangedEventArgs" /> that contains the event data.</param>
    public delegate void PlayerKeyStateChangedHandler(object sender, PlayerKeyStateChangedEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.RconLoginAttempt" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="RconLoginAttemptEventArgs" /> that contains the event data.</param>
    public delegate void RconLoginAttemptHandler(object sender, RconLoginAttemptEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerStreamIn" /> or
    ///     <see cref="BaseMode.PlayerStreamOut" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="StreamPlayerEventArgs" /> that contains the event data.</param>
    public delegate void StreamPlayerHandler(object sender, StreamPlayerEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.DialogResponse" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="DialogResponseEventArgs" /> that contains the event data.</param>
    public delegate void DialogResponseHandler(object sender, DialogResponseEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerTakeDamage" /> or
    ///     <see cref="BaseMode.PlayerGiveDamage" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerDamageEventArgs" /> that contains the event data.</param>
    public delegate void PlayerDamageHandler(object sender, PlayerDamageEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerClickMap" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerClickMapEventArgs" /> that contains the event data.</param>
    public delegate void PlayerClickMapHandler(object sender, PlayerClickMapEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerClickTextDraw" /> or
    ///     <see cref="BaseMode.PlayerClickPlayerTextDraw" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerClickTextDrawEventArgs" /> that contains the event data.</param>
    public delegate void PlayerClickTextDrawHandler(object sender, PlayerClickTextDrawEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerClickPlayer" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerClickPlayerEventArgs" /> that contains the event data.</param>
    public delegate void PlayerClickPlayerHandler(object sender, PlayerClickPlayerEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerEditObject" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerEditObjectEventArgs" /> that contains the event data.</param>
    public delegate void PlayerEditObjectHandler(object sender, PlayerEditObjectEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerEditAttachedObject" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerEditAttachedObjectEventArgs" /> that contains the event data.</param>
    public delegate void PlayerEditAttachedObjectHandler(object sender, PlayerEditAttachedObjectEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerSelectObject" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerSelectObjectEventArgs" /> that contains the event data.</param>
    public delegate void PlayerSelectObjectHandler(object sender, PlayerSelectObjectEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.PlayerWeaponShot" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="WeaponShotEventArgs" /> that contains the event data.</param>
    public delegate void WeaponShotHandler(object sender, WeaponShotEventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.TimerTick" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="EventArgs" /> that contains the event data.</param>
    public delegate void TimerTickHandler(object sender, EventArgs e);

    /// <summary>
    ///     Represents the method that will handle the <see cref="BaseMode.IncomingConnection" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="ConnectionEventArgs" /> that contains the event data.</param>
    public delegate void ConnectionHandler(object sender, ConnectionEventArgs e);
}