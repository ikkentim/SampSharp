using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using Shouldly;
using Xunit;

namespace TestMode.Entities.ApiTests;

public class ServerServiceTests : TestBase
{
    private IServerService Sut => Services.GetRequiredService<IServerService>();

    [Fact]
    public void BlockIpAddress_should_succeed()
    {
        Sut.BlockIpAddress("127.0.0.1");
    }

    [Fact]
    public void UnblockIpAddress_should_succeed()
    {
        Sut.UnBlockIpAddress("127.0.0.1");
    }

    [Fact]
    public void AddPlayerClass_with_team_should_succeed()
    {
        var playerClass = Sut.AddPlayerClass(1, 2, new Vector3(0, 0, 0), 0.0f, Weapon.Colt45, 100);
        playerClass.ShouldNotBeNull();
        playerClass.Id.ShouldBeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public void AddPlayerClass_without_team_should_succeed()
    {
        var playerClass = Sut.AddPlayerClass(2, new Vector3(0, 0, 0), 0.0f, Weapon.Colt45, 100);
        playerClass.ShouldNotBeNull();
        playerClass.Id.ShouldBeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public void ConnectNpc_should_succeed()
    {
        // xunit sets current dir to the assembly directory. reset to server directory, allowing NPC to connect.
        Directory.SetCurrentDirectory(Services.GetRequiredService<TestContext>().ServerDirectory);

        Sut.ConnectNpc("TestNpc", "npcidle");
    }

    [Fact]
    public void DisableInteriorEnterExits_should_succeed()
    {
        Sut.DisableInteriorEnterExits();
    }

    [Fact]
    public void EnableStuntBonus_should_succeed()
    {
        Sut.EnableStuntBonus(true);
    }

    [Fact]
    public void EnableVehicleFriendlyFire_should_succeed()
    {
        Sut.EnableVehicleFriendlyFire();
    }

    [Fact]
    public void GameModeExit_should_succeed()
    {
        Sut.GameModeExit();
    }

    [Fact]
    public void GetConsoleVarAsBool_should_return_correct_value()
    {
        var result = Sut.GetConsoleVarAsBool("some_var");
        result.ShouldBeFalse();
    }

    [Fact]
    public void GetConsoleVarAsInt_should_return_correct_value()
    {
        var result = Sut.GetConsoleVarAsInt("some_var");
        result.ShouldBe(0);
    }

    [Fact]
    public void GetConsoleVarAsString_should_return_correct_value()
    {
        var result = Sut.GetConsoleVarAsString("some_var");
        result.ShouldBeNull();
    }

    [Fact]
    public void LimitGlobalChatRadius_should_succeed()
    {
        Sut.LimitGlobalChatRadius(100.0f);
    }

    [Fact]
    public void LimitPlayerMarkerRadius_should_succeed()
    {
        Sut.LimitPlayerMarkerRadius(100.0f);
    }

    [Fact]
    public void ManualVehicleEngineAndLights_should_succeed()
    {
        Sut.ManualVehicleEngineAndLights();
    }

    [Fact]
    public void SendRconCommand_should_succeed()
    {
        Sut.SendRconCommand("echo Test");
    }

    [Fact]
    public void SetGameModeText_should_succeed()
    {
        Sut.SetGameModeText("TestMode");
    }

    [Fact]
    public void SetNameTagDrawDistance_should_succeed()
    {
        Sut.SetNameTagDrawDistance(100.0f);
    }

    [Fact]
    public void SetWorldTime_should_succeed()
    {
        Sut.SetWorldTime(12);
    }

    [Fact]
    public void ShowNameTags_should_succeed()
    {
        Sut.ShowNameTags(true);
    }

    [Fact]
    public void ShowPlayerMarkers_should_succeed()
    {
        Sut.ShowPlayerMarkers(PlayerMarkersMode.Global);
    }

    [Fact]
    public void UsePlayerPedAnims_should_succeed()
    {
        Sut.UsePlayerPedAnims();
    }

    [Fact]
    public void ActorPoolSize_should_succeed()
    {
        _ = Sut.ActorPoolSize;
    }

    [Fact]
    public void MaxPlayers_should_be_positive()
    {
        Sut.MaxPlayers.ShouldBeGreaterThan(0);
    }

    [Fact]
    public void PlayerPoolSize_should_succeed()
    {
        _ = Sut.PlayerPoolSize;
    }

    [Fact]
    public void TickCount_should_succeed()
    {
        _ = Sut.TickCount;
    }

    [Fact]
    public void TickRate_should_succeed()
    {
        _ = Sut.TickRate;
    }

    [Fact]
    public void VehiclePoolSize_should_succeed()
    {
        _ = Sut.VehiclePoolSize;
    }

    [Fact]
    public void AddPlayerClass_with_spawn_data_should_succeed()
    {
        var spawnData = new PlayerSpawnData
        {
            Skin = 3,
            Location = new Vector3(0, 0, 0),
            Angle = 0
        };

        var playerClass = Sut.AddPlayerClass(spawnData);

        playerClass.ShouldNotBeNull();
        playerClass.Id.ShouldBeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public void SetServerName_should_succeed()
    {
        Sut.SetServerName("TestServer");
    }

    [Fact]
    public void SetMapName_should_succeed()
    {
        Sut.SetMapName("TestMap");
    }

    [Fact]
    public void SetLanguage_should_succeed()
    {
        Sut.SetLanguage("English");
    }

    [Fact]
    public void SetWebsiteUrl_should_succeed()
    {
        Sut.SetWebsiteUrl("http://example.com");
    }

    [Fact]
    public void SetServerPassword_should_succeed()
    {
        Sut.SetServerPassword("testpass");
        Sut.SetServerPassword(null);
    }

    [Fact]
    public void SetAdminPassword_should_succeed()
    {
        Sut.SetAdminPassword("adminpass");
        Sut.SetAdminPassword(null);
    }

    [Fact]
    public void SendEmptyDeathMessage_should_succeed()
    {
        Sut.SendEmptyDeathMessage();
    }

    [Fact]
    public void IsNameValid_should_return_true_for_valid_name()
    {
        Sut.IsNameValid("ValidName").ShouldBeTrue();
    }

    [Fact]
    public void IsNameValid_should_return_false_for_invalid_name()
    {
        Sut.IsNameValid("Invalid Name With Spaces").ShouldBeFalse();
    }

    [Fact]
    public void IsNameTaken_should_return_true_for_connected_player()
    {
        Sut.IsNameTaken(Player.Name).ShouldBeTrue();
    }

    [Fact]
    public void IsNameTaken_should_return_false_when_skipping_player()
    {
        Sut.IsNameTaken(Player.Name, Player).ShouldBeFalse();
    }

    [Fact]
    public void IsNameTaken_should_return_false_for_unused_name()
    {
        Sut.IsNameTaken("UnusedNameXYZ123").ShouldBeFalse();
    }

    [Fact]
    public void AllowNickNameCharacter_and_IsNickNameCharacterAllowed_should_roundtrip()
    {
        Sut.AllowNickNameCharacter('@', true);
        Sut.IsNickNameCharacterAllowed('@').ShouldBeTrue();

        Sut.AllowNickNameCharacter('@', false);
        Sut.IsNickNameCharacterAllowed('@').ShouldBeFalse();
    }

    [Fact]
    public void GetDefaultColor_should_succeed()
    {
        _ = Sut.GetDefaultColor(Player.Id);
    }
}
