namespace GameMode.Events
{
    /// <summary>
    /// Represents the method that will handle timer ticks, passed to <see cref="Server.SetTimer"/>.
    /// </summary>
    /// <param name="timerid">The ID of the Timer that ticked.</param>
    /// <param name="args">An object that is passed to <see cref="Server.SetTimer"/>.</param>
    /// <returns>False to kill the timer, True otherwise.</returns>
    public delegate bool TimerTickHandler(int timerid, object args);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.Initialized"/> or <see cref="Server.Exited"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="GameModeEventArgs"/> that contains the event data.</param>
    public delegate void GameModeHandler(object sender, GameModeEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerConnected"/>, <see cref="Server.PlayerSpawned"/>, <see cref="Server.PlayerEnterCheckpoint"/>, <see cref="Server.PlayerLeaveCheckpoint"/>, <see cref="Server.PlayerEnterRaceCheckpoint"/>, <see cref="Server.PlayerLeaveRaceCheckpoint"/>, <see cref="Server.PlayerRequestSpawn"/>, <see cref="Server.VehicleDamageStatusUpdated"/>, <see cref="Server.PlayerExitedMenu"/> or <see cref="Server.PlayerUpdate"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerEventArgs"/> that contains the event data.</param>
    public delegate void PlayerHandler(object sender, PlayerEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerDisconnected"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerDisconnectedEventArgs"/> that contains the event data.</param>
    public delegate void PlayerDisconnectedHandler(object sender, PlayerDisconnectedEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerDied"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerDeathEventArgs"/> that contains the event data.</param>
    public delegate void PlayerDeathHandler(object sender, PlayerDeathEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.VehicleSpawned"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="VehicleEventArgs"/> that contains the event data.</param>
    public delegate void VehicleSpawnedHandler(object sender, VehicleEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerText"/> or <see cref="Server.PlayerCommandText"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerTextEventArgs"/> that contains the event data.</param>
    public delegate void PlayerTextHandler(object sender, PlayerTextEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerRequestClass"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerRequestClassEventArgs"/> that contains the event data.</param>
    public delegate void PlayerRequestClassHandler(object sender, PlayerRequestClassEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerEnterVehicle"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerEnterVehicleEventArgs"/> that contains the event data.</param>
    public delegate void PlayerEnterVehicleHandler(object sender, PlayerEnterVehicleEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.VehicleDied"/>, <see cref="Server.PlayerExitVehicle"/>, <see cref="Server.VehicleStreamIn"/> or <see cref="Server.VehicleStreamOut"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerVehicleEventArgs"/> that contains the event data.</param>
    public delegate void PlayerVehicleHandler(object sender, PlayerVehicleEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerStateChanged"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerStateEventArgs"/> that contains the event data.</param>
    public delegate void PlayerStateHandler(object sender, PlayerStateEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.RconCommand"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="RconEventArgs"/> that contains the event data.</param>
    public delegate void RconHandler(object sender, RconEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.ObjectMoved"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="ObjectEventArgs"/> that contains the event data.</param>
    public delegate void ObjectHandler(object sender, ObjectEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerObjectMoved"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerObjectEventArgs"/> that contains the event data.</param>
    public delegate void PlayerObjectHandler(object sender, PlayerObjectEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerPickUpPickup"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerPickupEventArgs"/> that contains the event data.</param>
    public delegate void PlayerPickupHandler(object sender, PlayerPickupEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.VehicleMod"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="VehicleModEventArgs"/> that contains the event data.</param>
    public delegate void VehicleModHandler(object sender, VehicleModEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerEnterExitModShop"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerEnterModShopEventArgs"/> that contains the event data.</param>
    public delegate void PlayerEnterModShopHandler(object sender, PlayerEnterModShopEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.VehiclePaintjobApplied"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="VehiclePaintjobEventArgs"/> that contains the event data.</param>
    public delegate void VehiclePaintjobHandler(object sender, VehiclePaintjobEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.VehicleResprayed"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="VehicleResprayedEventArgs"/> that contains the event data.</param>
    public delegate void VehicleResprayedHandler(object sender, VehicleResprayedEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.UnoccupiedVehicleUpdated"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="UnoccupiedVehicleEventArgs"/> that contains the event data.</param>
    public delegate void UnoccupiedVehicleUpdatedHandler(object sender, UnoccupiedVehicleEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerSelectedMenuRow"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerSelectedMenuRowEventArgs"/> that contains the event data.</param>
    public delegate void PlayerSelectedMenuRowHandler(object sender, PlayerSelectedMenuRowEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerInteriorChanged"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerInteriorChangedEventArgs"/> that contains the event data.</param>
    public delegate void PlayerInteriorChangedHandler(object sender, PlayerInteriorChangedEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerKeyStateChanged"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerKeyStateChangedEventArgs"/> that contains the event data.</param>
    public delegate void PlayerKeyStateChangedHandler(object sender, PlayerKeyStateChangedEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.RconLoginAttempt"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="RconLoginAttemptEventArgs"/> that contains the event data.</param>
    public delegate void RconLoginAttemptHandler(object sender, RconLoginAttemptEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerStreamIn"/> or <see cref="Server.PlayerStreamOut"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="StreamPlayerEventArgs"/> that contains the event data.</param>
    public delegate void StreamPlayerHandler(object sender, StreamPlayerEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.DialogResponse"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="DialogResponseEventArgs"/> that contains the event data.</param>
    public delegate void DialogResponseHandler(object sender, DialogResponseEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerTakeDamage"/> or <see cref="Server.PlayerGiveDamage"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerDamageEventArgs"/> that contains the event data.</param>
    public delegate void PlayerDamageHandler(object sender, PlayerDamageEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerClickMap"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerClickMapEventArgs"/> that contains the event data.</param>
    public delegate void PlayerClickMapHandler(object sender, PlayerClickMapEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerClickTextDraw"/> or <see cref="Server.PlayerClickPlayerTextDraw"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerClickTextDrawEventArgs"/> that contains the event data.</param>
    public delegate void PlayerClickTextDrawHandler(object sender, PlayerClickTextDrawEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerClickPlayer"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerClickPlayerEventArgs"/> that contains the event data.</param>
    public delegate void PlayerClickPlayerHandler(object sender, PlayerClickPlayerEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerEditObject"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerEditObjectEventArgs"/> that contains the event data.</param>
    public delegate void PlayerEditObjectHandler(object sender, PlayerEditObjectEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerEditAttachedObject"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerEditAttachedObjectEventArgs"/> that contains the event data.</param>
    public delegate void PlayerEditAttachedObjectHandler(object sender, PlayerEditAttachedObjectEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerSelectObject"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PlayerSelectObjectEventArgs"/> that contains the event data.</param>
    public delegate void PlayerSelectObjectHandler(object sender, PlayerSelectObjectEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="Server.PlayerWeaponShot"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="WeaponShotEventArgs"/> that contains the event data.</param>
    public delegate void WeaponShotHandler(object sender, WeaponShotEventArgs e);
}
