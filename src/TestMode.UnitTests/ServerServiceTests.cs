using System.Numerics;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using SampSharp.OpenMp.Core.Api;
using Shouldly;
using Xunit;

namespace TestMode.UnitTests;

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
        var classId = Sut.AddPlayerClass(1, 2, new Vector3(0, 0, 0), 0.0f, Weapon.Colt45, 100);
        classId.ShouldBeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public void AddPlayerClass_without_team_should_succeed()
    {
        var classId = Sut.AddPlayerClass(2, new Vector3(0, 0, 0), 0.0f, Weapon.Colt45, 100);
        classId.ShouldBeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public void ConnectNpc_should_succeed()
    {
        Sut.ConnectNpc("TestNpc", "npc_script");
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
}
