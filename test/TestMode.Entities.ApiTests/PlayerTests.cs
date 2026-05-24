using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using Shouldly;
using Xunit;

namespace TestMode.Entities.ApiTests;

public class PlayerTests : TestBase
{
    [Fact]
    public void Position_get_should_succeed()
    {
        _ = Player.Position;
    }

    [Fact]
    public void Position_set_should_succeed()
    {
        Player.Position = new Vector3(10, 20, 30);
    }

    [Fact]
    public void Name_should_roundtrip()
    {
        Player.SetName("TestName");
        Player.Name.ShouldBe("TestName");
    }

    [Fact]
    public void Interior_set_should_succeed()
    {
        // cannot roundtrip - setter sends a packet to the client
        Player.Interior = 1;
    }

    [Fact]
    public void Interior_get_should_succeed()
    {
        _ = Player.Interior;
    }

    [Fact]
    public void Health_get_should_succeed()
    {
        _ = Player.Health;
    }

    [Fact]
    public void Health_set_should_succeed()
    {
        Player.Health = 100.0f;
    }

    [Fact]
    public void Armour_get_should_succeed()
    {
        _ = Player.Armour;
    }

    [Fact]
    public void Armour_set_should_succeed()
    {
        Player.Armour = 50.0f;
    }

    [Fact]
    public void Team_should_roundtrip()
    {
        Player.Team = 2;
        Player.Team.ShouldBe(2);
    }

    [Fact]
    public void Score_should_roundtrip()
    {
        Player.Score = 10;
        Player.Score.ShouldBe(10);
    }

    [Fact]
    public void DrunkLevel_should_roundtrip()
    {
        Player.DrunkLevel = 5;
        Player.DrunkLevel.ShouldBe(5);
    }

    [Fact]
    public void Color_should_roundtrip()
    {
        var color = new Color(255, 0, 0);
        Player.Color = color;
        Player.Color.ShouldBe(color);
    }

    [Fact]
    public void Skin_should_roundtrip()
    {
        Player.Skin = 3;
        Player.Skin.ShouldBe(3);
    }

    [Fact]
    public void Money_should_roundtrip()
    {
        Player.Money = 1000;
        Player.Money.ShouldBe(1000);
    }

    [Fact]
    public void WantedLevel_should_roundtrip()
    {
        Player.WantedLevel = 3;
        Player.WantedLevel.ShouldBe(3);
    }

    [Fact]
    public void FightStyle_should_roundtrip()
    {
        Player.FightStyle = FightStyle.Boxing;
        Player.FightStyle.ShouldBe(FightStyle.Boxing);
    }

    [Fact]
    public void Velocity_set_should_succeed()
    {
        Player.Velocity = new Vector3(1, 2, 3);
    }

    [Fact]
    public void Velocity_get_should_succeed()
    {
        _ = Player.Velocity;
    }

    [Fact]
    public void SpecialAction_get_should_succeed()
    {
        _ = Player.SpecialAction;
    }

    [Fact]
    public void SpecialAction_set_should_succeed()
    {
        Player.SpecialAction = SpecialAction.Duck;
    }

    [Fact]
    public void CameraPosition_should_roundtrip()
    {
        var position = new Vector3(1, 2, 3);
        Player.CameraPosition = position;
        Player.CameraPosition.ShouldBe(position);
    }

    [Fact]
    public void WeaponAmmo_should_succeed()
    {
        _ = Player.WeaponAmmo;
    }

    [Fact]
    public void WeaponState_should_succeed()
    {
        _ = Player.WeaponState;
    }

    [Fact]
    public void Weapon_should_succeed()
    {
        _ = Player.Weapon;
    }

    [Fact]
    public void TargetPlayer_should_succeed()
    {
        _ = Player.TargetPlayer;
    }

    [Fact]
    public void State_should_succeed()
    {
        _ = Player.State;
    }

    [Fact]
    public void IpAddress_should_succeed()
    {
        _ = Player.IpAddress;
    }

    [Fact]
    public void EndPoint_should_succeed()
    {
        _ = Player.EndPoint;
    }

    [Fact]
    public void Ping_should_succeed()
    {
        _ = Player.Ping;
    }

    [Fact]
    public void CameraFrontVector_should_succeed()
    {
        _ = Player.CameraFrontVector;
    }

    [Fact]
    public void CameraMode_should_succeed()
    {
        _ = Player.CameraMode;
    }

    [Fact]
    public void TargetActor_should_succeed()
    {
        _ = Player.TargetActor;
    }

    [Fact]
    public void CameraTargetGlobalObject_should_succeed()
    {
        _ = Player.CameraTargetGlobalObject;
    }

    [Fact]
    public void CameraTargetVehicle_should_succeed()
    {
        _ = Player.CameraTargetVehicle;
    }

    [Fact]
    public void CameraTargetPlayer_should_succeed()
    {
        _ = Player.CameraTargetPlayer;
    }

    [Fact]
    public void CameraTargetActor_should_succeed()
    {
        _ = Player.CameraTargetActor;
    }

    [Fact]
    public void IsNpc_should_succeed()
    {
        _ = Player.IsNpc;
    }

    [Fact]
    public void Version_should_succeed()
    {
        _ = Player.ClientVersion;
    }

    [Fact]
    public void Gpci_should_succeed()
    {
        _ = Player.Gpci;
    }

    [Fact]
    public void MessagesReceived_should_succeed()
    {
        _ = Player.MessagesReceived;
    }

    [Fact]
    public void MessagesReceivedPerSecond_should_succeed()
    {
        _ = Player.MessagesReceivedPerSecond;
    }

    [Fact]
    public void MessagesSent_should_succeed()
    {
        _ = Player.MessagesSent;
    }

    [Fact]
    public void BytesReceived_should_succeed()
    {
        _ = Player.BytesReceived;
    }

    [Fact]
    public void BytesSent_should_succeed()
    {
        _ = Player.BytesSent;
    }

    [Fact]
    public void AspectCameraRatio_should_succeed()
    {
        _ = Player.AspectCameraRatio;
    }

    [Fact]
    public void CameraZoom_should_succeed()
    {
        _ = Player.CameraZoom;
    }

    [Fact]
    public void GetNetworkStats_should_succeed()
    {
        _ = Player.GetNetworkStats();
    }

    [Fact]
    public void Spawn_should_succeed()
    {
        Player.Spawn();
    }

    [Fact]
    public void PutCameraBehindPlayer_should_succeed()
    {
        Player.PutCameraBehindPlayer();
    }

    [Fact]
    public void SetPositionFindZ_should_succeed()
    {
        Player.SetPositionFindZ(new Vector3(1, 2, 3));
    }

    [Fact]
    public void IsPlayerStreamedIn_should_succeed()
    {
        var result = Player.IsPlayerStreamedIn(Player);

        result.ShouldBeTrue();
    }

    [Fact]
    public void SetAmmo_should_succeed()
    {
        Player.GiveWeapon(Weapon.Colt45, 10);
        Player.SetAmmo(Weapon.Colt45, 50);
    }

    [Fact]
    public void GiveWeapon_should_succeed()
    {
        Player.GiveWeapon(Weapon.Colt45, 100);
    }

    [Fact]
    public void ResetWeapons_should_succeed()
    {
        Player.ResetWeapons();
    }

    [Fact]
    public void SetArmedWeapon_should_succeed()
    {
        Player.SetArmedWeapon(Weapon.Colt45);
    }

    [Fact]
    public void GetWeaponData_should_succeed()
    {
        Player.GetWeaponData(0, out _, out _);
    }

    [Fact]
    public void GiveMoney_should_succeed()
    {
        Player.GiveMoney(1000);
    }

    [Fact]
    public void ResetMoney_should_succeed()
    {
        Player.ResetMoney();
    }

    [Fact]
    public void GetKeys_should_succeed()
    {
        Player.GetKeys(out _, out _, out _);
    }

    [Fact]
    public void SetTime_should_succeed()
    {
        Player.SetTime(12, 30);
    }

    [Fact]
    public void GetTime_should_succeed()
    {
        Player.GetTime(out _, out _);
    }

    [Fact]
    public void ToggleClock_should_succeed()
    {
        Player.ToggleClock(true);
    }

    [Fact]
    public void SetWeather_should_succeed()
    {
        Player.Weather = 1;
    }

    [Fact]
    public void ForceClassSelection_should_succeed()
    {
        Player.ForceClassSelection();
    }

    [Fact]
    public void PlayCrimeReport_should_succeed()
    {
        Player.PlayCrimeReport(Player, 16);
    }

    [Fact]
    public void PlayAudioStream_with_position_should_succeed()
    {
        Player.PlayAudioStream("http://example.com/stream", new Vector3(1, 2, 3), 100.0f);
    }

    [Fact]
    public void PlayAudioStream_should_succeed()
    {
        Player.PlayAudioStream("http://example.com/stream");
    }

    [Fact]
    public void DisableRemoteVehicleCollisions_should_succeed()
    {
        Player.DisableRemoteVehicleCollisions(true);
    }

    [Fact]
    public void EnablePlayerCameraTarget_should_succeed()
    {
        Player.EnablePlayerCameraTarget(true);
    }

    [Fact]
    public void StopAudioStream_should_succeed()
    {
        Player.StopAudioStream();
    }

    [Fact]
    public void SetShopName_should_succeed()
    {
        Player.SetShopName(ShopName.Ammunation1);
    }

    [Fact]
    public void SetSkillLevel_should_succeed()
    {
        Player.SetSkillLevel(WeaponSkill.Pistol, 999);
    }

    [Fact]
    public void PutInVehicle_with_seatId_should_succeed()
    {
        var vehicle = Services.GetRequiredService<IWorldService>().CreateVehicle(VehicleModelType.BMX, new Vector3(1, 2, 3), 0, 0, 0);

        try
        {
            Player.PutInVehicle(vehicle, 0);
        }
        finally
        {
            vehicle.DestroyEntity();
        }
    }

    [Fact]
    public void PutInVehicle_should_succeed()
    {
        var vehicle = Services.GetRequiredService<IWorldService>().CreateVehicle(VehicleModelType.BMX, new Vector3(1, 2, 3), 0, 0, 0);

        try
        {
            Player.PutInVehicle(vehicle);
        }
        finally
        {
            vehicle.DestroyEntity();
        }
    }

    [Fact]
    public void RemoveFromVehicle_should_succeed()
    {
        Player.RemoveFromVehicle();
    }

    [Fact]
    public void ToggleControllable_should_succeed()
    {
        Player.ToggleControllable(true);
    }

    [Fact]
    public void PlaySound_with_point_should_succeed()
    {
        Player.PlaySound(1, new Vector3(1, 2, 3));
    }

    [Fact]
    public void PlaySound_should_succeed()
    {
        Player.PlaySound(1);
    }

    [Fact]
    public void ApplyAnimation_with_forceSync_should_succeed()
    {
        Player.ApplyAnimation("AIRPORT", "THRW_BARL_THRW", 4.1f, true, false, false, false, TimeSpan.Zero, PlayerAnimationSyncType.Sync);
    }

    [Fact]
    public void ApplyAnimation_should_succeed()
    {
        Player.ApplyAnimation("AIRPORT", "THRW_BARL_THRW", 4.1f, true, false, false, false, TimeSpan.Zero);
    }

    [Fact]
    public void ClearAnimations_with_forceSync_should_succeed()
    {
        Player.ClearAnimations(PlayerAnimationSyncType.Sync);
    }

    [Fact]
    public void ClearAnimations_should_succeed()
    {
        Player.ClearAnimations();
    }

    [Fact]
    public void GetAnimationName_should_succeed()
    {
        Player.GetAnimationName(out _, out _);
    }

    [Fact]
    public void SetPlayerMarker_should_succeed()
    {
        Player.SetPlayerMarker(Player, new Color(255, 0, 0));
    }

    [Fact]
    public void ShowNameTagForPlayer_should_succeed()
    {
        Player.ShowNameTagForPlayer(Player, true);
    }

    [Fact]
    public void SetCameraLookAt_with_cut_should_succeed()
    {
        Player.SetCameraLookAt(new Vector3(1, 2, 3), CameraCut.Cut);
    }

    [Fact]
    public void SetCameraLookAt_should_succeed()
    {
        Player.SetCameraLookAt(new Vector3(1, 2, 3));
    }

    [Fact]
    public void InterpolateCameraPosition_should_succeed()
    {
        Player.InterpolateCameraPosition(new Vector3(1, 2, 3), new Vector3(4, 5, 6), TimeSpan.FromSeconds(1), CameraCut.Cut);
    }

    [Fact]
    public void InterpolateCameraLookAt_should_succeed()
    {
        Player.InterpolateCameraLookAt(new Vector3(1, 2, 3), new Vector3(4, 5, 6), TimeSpan.FromSeconds(1), CameraCut.Cut);
    }

    [Fact]
    public void EnableStuntBonus_should_succeed()
    {
        Player.EnableStuntBonus(true);
    }

    [Fact]
    public void ToggleSpectating_should_succeed()
    {
        Player.ToggleSpectating(true);
    }

    [Fact]
    public void SpectatePlayer_with_mode_should_succeed()
    {
        Player.SpectatePlayer(Player, SpectateMode.Normal);
    }

    [Fact]
    public void SpectatePlayer_should_succeed()
    {
        Player.SpectatePlayer(Player);
    }

    [Fact]
    public void SpectateVehicle_with_mode_should_succeed()
    {
        var vehicle = Services.GetRequiredService<IWorldService>().CreateVehicle(VehicleModelType.BMX, new Vector3(1, 2, 3), 0, 0, 0);

        try
        {
            Player.SpectateVehicle(vehicle, SpectateMode.Normal);
        }
        finally
        {
            vehicle.DestroyEntity();
        }
    }

    [Fact]
    public void SpectateVehicle_should_succeed()
    {
        var vehicle = Services.GetRequiredService<IWorldService>().CreateVehicle(VehicleModelType.BMX, new Vector3(1, 2, 3), 0, 0, 0);

        try
        {
            Player.SpectateVehicle(vehicle);
        }
        finally
        {
            vehicle.DestroyEntity();
        }
    }

    [Fact]
    public void SendClientMessage_with_color_should_succeed()
    {
        Player.SendClientMessage(new Color(255, 0, 0), "Test message");
    }

    [Fact]
    public void Kick_should_succeed()
    {
        Player.Kick();
    }

    [Fact]
    public void Ban_with_reason_should_succeed()
    {
        Player.Ban("Test reason");
    }

    [Fact]
    public void SendPlayerMessageToPlayer_should_succeed()
    {
        Player.SendPlayerMessageToPlayer(Player, "Test message");
    }

    [Fact]
    public void GameText_should_succeed()
    {
        Player.GameText("Test text", TimeSpan.FromSeconds(5), GameTextStyle.Style1);
    }

    [Fact]
    public void CreateExplosion_should_succeed()
    {
        Player.CreateExplosion(new Vector3(1, 2, 3), ExplosionType.LargeInvisible, 10.0f);
    }

    [Fact]
    public void SendDeathMessage_should_succeed()
    {
        Player.SendDeathMessage(Player, Player, Weapon.Colt45);
    }

    [Fact]
    public void AttachCameraToObject_GlobalObject_should_succeed()
    {
        var obj = Services.GetRequiredService<IWorldService>().CreateObject(400, Vector3.Zero, Vector3.Zero);

        try
        {
            Player.AttachCameraToObject(obj);
        }
        finally
        {
            obj.DestroyEntity();
        }
    }

    [Fact]
    public void AttachCameraToObject_PlayerObject_should_succeed()
    {
        var obj = Services.GetRequiredService<IWorldService>().CreateObject(400, Vector3.Zero, Vector3.Zero);

        try
        {
            Player.AttachCameraToObject(obj);
        }
        finally
        {
            obj.DestroyEntity();
        }
    }

    [Fact]
    public void RemoveDefaultObjects_should_succeed()
    {
        Player.RemoveDefaultObjects(1, new Vector3(1, 2, 3), 10.0f);
    }

    [Fact]
    public void RemoveMapIcon_should_succeed()
    {
        Player.RemoveMapIcon(1);
    }

    [Fact]
    public void SetMapIcon_should_succeed()
    {
        Player.SetMapIcon(0, new Vector3(1, 2, 3), MapIcon.AirYard, Color.White, MapIconType.Local);
    }

    [Fact]
    public void SetMapIcon_with_icon_id_below_0_should_throw()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Player.SetMapIcon(-1, new Vector3(1, 2, 3), MapIcon.AirYard, Color.White, MapIconType.Local));
    }

    [Fact]
    public void SetMapIcon_with_icon_id_above_99_should_throw()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Player.SetMapIcon(100, new Vector3(1, 2, 3), MapIcon.AirYard, Color.White, MapIconType.Local));
    }

    [Fact]
    public void GetWeaponData_with_slot_below_0_should_throw()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Player.GetWeaponData(-1, out _, out _));
    }

    [Fact]
    public void GetWeaponData_with_slot_above_12_should_throw()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Player.GetWeaponData(13, out _, out _));
    }

    [Fact]
    public void SetTime_with_hour_below_0_should_throw()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Player.SetTime(-1, 0));
    }

    [Fact]
    public void SetTime_with_hour_above_23_should_throw()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Player.SetTime(24, 0));
    }

    [Fact]
    public void SetTime_with_minutes_below_0_should_throw()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Player.SetTime(0, -1));
    }

    [Fact]
    public void SetTime_with_minutes_above_59_should_throw()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Player.SetTime(0, 60));
    }

    [Fact]
    public void Angle_should_roundtrip()
    {
        Player.Angle = 45.0f;
        Player.Angle.ShouldBe(45.0f, tolerance: 1.0f);
    }

    [Fact]
    public void Ip_should_succeed()
    {
        _ = Player.Ip;
    }

    [Fact]
    public void IsAdmin_should_be_false_for_npc()
    {
        Player.IsAdmin.ShouldBeFalse();
    }

    [Fact]
    public void IsAlive_should_be_true()
    {
        Player.IsAlive.ShouldBeTrue();
    }

    [Fact]
    public void IsSelectingTextDraw_should_be_false()
    {
        Player.IsSelectingTextDraw.ShouldBeFalse();
    }

    [Fact]
    public void ConnectedTime_should_be_positive()
    {
        Player.ConnectedTime.ShouldBeGreaterThan(TimeSpan.Zero);
    }

    [Fact]
    public void ConnectionStatus_should_succeed()
    {
        _ = Player.ConnectionStatus;
    }

    [Fact]
    public void IsUsingOfficialClient_should_succeed()
    {
        _ = Player.IsUsingOfficialClient;
    }

    [Fact]
    public void IsUsingOmp_should_succeed()
    {
        _ = Player.IsUsingOmp;
    }

    [Fact]
    public void ClientVersionName_should_succeed()
    {
        _ = Player.ClientVersionName;
    }

    [Fact]
    public void IsGhostModeEnabled_should_roundtrip()
    {
        Player.IsGhostModeEnabled = true;
        Player.IsGhostModeEnabled.ShouldBeTrue();
        Player.IsGhostModeEnabled = false;
        Player.IsGhostModeEnabled.ShouldBeFalse();
    }

    [Fact]
    public void AreWeaponsAllowed_should_roundtrip()
    {
        Player.AreWeaponsAllowed = false;
        Player.AreWeaponsAllowed.ShouldBeFalse();
        Player.AreWeaponsAllowed = true;
        Player.AreWeaponsAllowed.ShouldBeTrue();
    }

    [Fact]
    public void IsTeleportAllowed_should_roundtrip()
    {
        Player.IsTeleportAllowed = true;
        Player.IsTeleportAllowed.ShouldBeTrue();
        Player.IsTeleportAllowed = false;
        Player.IsTeleportAllowed.ShouldBeFalse();
    }

    [Fact]
    public void WorldBounds_get_should_succeed()
    {
        _ = Player.WorldBounds;
    }

    [Fact]
    public void HasWidescreen_should_roundtrip()
    {
        Player.HasWidescreen = true;
        Player.HasWidescreen.ShouldBeTrue();
        Player.HasWidescreen = false;
        Player.HasWidescreen.ShouldBeFalse();
    }

    [Fact]
    public void Weather_should_roundtrip()
    {
        Player.Weather = 5;
        Player.Weather.ShouldBe(5);
    }

    [Fact]
    public void StreamedForPlayers_should_not_be_null()
    {
        Player.StreamedForPlayers.ShouldNotBeNull();
    }

    [Fact]
    public void DefaultObjectsRemoved_should_succeed()
    {
        _ = Player.DefaultObjectsRemoved;
    }

    [Fact]
    public void IsBeingKicked_should_be_false()
    {
        Player.IsBeingKicked.ShouldBeFalse();
    }

    [Fact]
    public void VehicleSeat_should_succeed()
    {
        _ = Player.VehicleSeat;
    }

    [Fact]
    public void AnimationIndex_should_succeed()
    {
        _ = Player.AnimationIndex;
    }

    [Fact]
    public void InAnyVehicle_should_be_false()
    {
        Player.InAnyVehicle.ShouldBeFalse();
    }

    [Fact]
    public void InCheckpoint_should_be_false()
    {
        Player.InCheckpoint.ShouldBeFalse();
    }

    [Fact]
    public void InRaceCheckpoint_should_be_false()
    {
        Player.InRaceCheckpoint.ShouldBeFalse();
    }

    [Fact]
    public void Vehicle_should_be_null()
    {
        Player.Vehicle.ShouldBeNull();
    }

    [Fact]
    public void Menu_should_be_null()
    {
        Player.Menu.ShouldBeNull();
    }

    [Fact]
    public void Gravity_should_roundtrip()
    {
        Player.Gravity = 0.012f;
        Player.Gravity.ShouldBe(0.012f, tolerance: 0.001f);
    }

    [Fact]
    public void SurfingEntity_should_be_null()
    {
        Player.SurfingEntity.ShouldBeNull();
    }

    [Fact]
    public void IsInRangeOfPoint_should_return_true_when_close()
    {
        Player.Position = new Vector3(10, 10, 5);
        Player.IsInRangeOfPoint(100.0f, new Vector3(10, 10, 5)).ShouldBeTrue();
    }

    [Fact]
    public void IsInRangeOfPoint_should_return_false_when_far()
    {
        Player.Position = new Vector3(0, 0, 0);
        Player.IsInRangeOfPoint(1.0f, new Vector3(9999, 9999, 0)).ShouldBeFalse();
    }

    [Fact]
    public void GetDistanceFromPoint_should_return_correct_distance()
    {
        Player.Position = new Vector3(10, 10, 0);
        Player.GetDistanceFromPoint(new Vector3(20, 10, 0)).ShouldBe(10.0f, tolerance: 0.1f);
    }

    [Fact]
    public void IsInVehicle_should_return_false_when_not_in_vehicle()
    {
        var vehicle = Services.GetRequiredService<IWorldService>().CreateVehicle(VehicleModelType.BMX, new Vector3(1, 2, 3), 0, 0, 0);

        try
        {
            Player.IsInVehicle(vehicle).ShouldBeFalse();
        }
        finally
        {
            vehicle.Destroy();
        }
    }

    [Fact]
    public void RemoveWeapon_should_succeed()
    {
        Player.GiveWeapon(Weapon.Colt45, 50);
        Player.RemoveWeapon(Weapon.Colt45);
    }

    [Fact]
    public void SendClientMessage_string_only_should_succeed()
    {
        Player.SendClientMessage("Hello world");
    }

    [Fact]
    public void SetChatBubble_should_succeed()
    {
        Player.SetChatBubble("Hello!", Color.White, 20.0f, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void SetCheckpoint_should_succeed()
    {
        Player.SetCheckpoint(new Vector3(10, 20, 5), 5.0f);
    }

    [Fact]
    public void DisableCheckpoint_should_succeed()
    {
        Player.SetCheckpoint(new Vector3(10, 20, 5), 5.0f);
        Player.DisableCheckpoint();
    }

    [Fact]
    public void SetRaceCheckpoint_should_succeed()
    {
        Player.SetRaceCheckpoint(CheckpointType.Normal, new Vector3(10, 20, 5), new Vector3(20, 30, 5), 5.0f);
    }

    [Fact]
    public void DisableRaceCheckpoint_should_succeed()
    {
        Player.SetRaceCheckpoint(CheckpointType.Normal, new Vector3(10, 20, 5), new Vector3(20, 30, 5), 5.0f);
        Player.DisableRaceCheckpoint();
    }

    [Fact]
    public void SelectTextDraw_should_succeed()
    {
        Player.SelectTextDraw(Color.White);
    }

    [Fact]
    public void CancelSelectTextDraw_should_succeed()
    {
        Player.SelectTextDraw(Color.White);
        Player.CancelSelectTextDraw();
    }

    [Fact]
    public void GetLastShot_should_succeed()
    {
        Player.GetLastShot(out _, out _);
    }

    [Fact]
    public void SetAttachedObject_should_return_true()
    {
        var result = Player.SetAttachedObject(0, 400, Bone.Spine, Vector3.Zero, Vector3.Zero, Vector3.One, Color.White, Color.White);
        result.ShouldBeTrue();
    }

    [Fact]
    public void IsAttachedObjectSlotUsed_should_be_true_after_set()
    {
        Player.SetAttachedObject(0, 400, Bone.Spine, Vector3.Zero, Vector3.Zero, Vector3.One, Color.White, Color.White);
        Player.IsAttachedObjectSlotUsed(0).ShouldBeTrue();
    }

    [Fact]
    public void RemoveAttachedObject_should_succeed()
    {
        Player.SetAttachedObject(0, 400, Bone.Spine, Vector3.Zero, Vector3.Zero, Vector3.One, Color.White, Color.White);
        Player.RemoveAttachedObject(0).ShouldBeTrue();
        Player.IsAttachedObjectSlotUsed(0).ShouldBeFalse();
    }

    [Fact]
    public void ClearTasks_should_succeed()
    {
        Player.ClearTasks(PlayerAnimationSyncType.NoSync);
    }

    [Fact]
    public void SetWorldTime_should_succeed()
    {
        Player.SetWorldTime(TimeSpan.FromHours(12));
    }

    [Fact]
    public void HideGameText_should_succeed()
    {
        Player.HideGameText(0);
    }

    [Fact]
    public void HasGameText_should_return_false_when_not_shown()
    {
        Player.HideGameText(0);
        Player.HasGameText(0).ShouldBeFalse();
    }

    [Fact]
    public void SetSpawnInfo_and_GetSpawnInfo_should_roundtrip()
    {
        var spawnData = new PlayerSpawnData { Skin = 7, Location = new Vector3(100, 200, 10), Angle = 90.0f, Team = 3 };
        Player.SetSpawnInfo(spawnData);

        var result = Player.GetSpawnInfo();
        result.Skin.ShouldBe(7);
        result.Team.ShouldBe(3);
    }

    [Fact]
    public void Edit_GlobalObject_and_CancelEdit_should_succeed()
    {
        var obj = Services.GetRequiredService<IWorldService>().CreateObject(400, Vector3.Zero, Vector3.Zero);

        try
        {
            Player.Edit(obj);
            Player.CancelEdit();
        }
        finally
        {
            obj.Destroy();
        }
    }

    [Fact]
    public void StreamInForPlayer_should_succeed()
    {
        Player.StreamInForPlayer(Player);
    }

    [Fact]
    public void StreamOutForPlayer_should_succeed()
    {
        Player.StreamOutForPlayer(Player);
    }

    [Fact]
    public void SetConsoleAccessibility_should_succeed()
    {
        Player.SetConsoleAccessibility(false);
    }

    [Fact]
    public void Select_object_mode_should_succeed()
    {
        Player.Select();
    }
}
