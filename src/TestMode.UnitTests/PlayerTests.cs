using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using Shouldly;
using Xunit;

namespace TestMode.UnitTests;

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
        _ = Player.Version;
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
        Player.SetWeather(1);
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
        Player.ApplyAnimation("AIRPORT", "THRW_BARL_THRW", 4.1f, true, false, false, false, TimeSpan.Zero, true);
    }

    [Fact]
    public void ApplyAnimation_should_succeed()
    {
        Player.ApplyAnimation("AIRPORT", "THRW_BARL_THRW", 4.1f, true, false, false, false, TimeSpan.Zero);
    }

    [Fact]
    public void ClearAnimations_with_forceSync_should_succeed()
    {
        Player.ClearAnimations(true);
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
        Player.GameText("Test text", TimeSpan.FromSeconds(5), 1);
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
}
