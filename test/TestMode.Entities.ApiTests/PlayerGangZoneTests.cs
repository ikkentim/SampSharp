using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using Shouldly;
using Xunit;

namespace TestMode.Entities.ApiTests;

public class PlayerGangZoneTests : TestBase
{
    private readonly PlayerGangZone _gangZone;
    private readonly IWorldService _worldService;

    public PlayerGangZoneTests()
    {
        _worldService = Services.GetRequiredService<IWorldService>();
        _gangZone = _worldService.CreatePlayerGangZone(Player, new Vector2(10, 11), new Vector2(20, 21));
    }

    protected override void Cleanup()
    {
        _gangZone?.Destroy();
    }

    [Fact]
    public void CreatePlayerGangZone_should_set_properties()
    {
        _gangZone.ShouldNotBeNull();
        _gangZone.Min.ShouldBe(new Vector2(10, 11));
        _gangZone.Max.ShouldBe(new Vector2(20, 21));
    }

    [Fact]
    public void Show_should_succeed()
    {
        _gangZone.Show();
    }

    [Fact]
    public void Hide_should_succeed()
    {
        _gangZone.Show();
        _gangZone.Hide();
    }

    [Fact]
    public void IsShown_should_be_true_after_show()
    {
        _gangZone.Show();
        _gangZone.IsShown().ShouldBeTrue();
    }

    [Fact]
    public void IsShown_should_be_false_after_hide()
    {
        _gangZone.Show();
        _gangZone.Hide();
        _gangZone.IsShown().ShouldBeFalse();
    }

    [Fact]
    public void Flash_should_succeed()
    {
        _gangZone.Show();
        _gangZone.Flash(Color.White);
    }

    [Fact]
    public void IsFlashing_should_be_true_after_flash()
    {
        _gangZone.Show();
        _gangZone.Flash(Color.White);
        _gangZone.IsFlashing().ShouldBeTrue();
    }

    [Fact]
    public void StopFlash_should_succeed()
    {
        _gangZone.Show();
        _gangZone.Flash(Color.White);
        _gangZone.StopFlash();
        _gangZone.IsFlashing().ShouldBeFalse();
    }

    [Fact]
    public void GetFlashingColor_should_succeed()
    {
        _gangZone.Show();
        _gangZone.Flash(Color.White);
        _ = _gangZone.GetFlashingColor();
    }

    [Fact]
    public void IsPlayerInside_should_return_false_without_check_enabled()
    {
        _gangZone.IsPlayerInside().ShouldBeFalse();
    }

    [Fact]
    public void UseGangZoneCheck_should_succeed()
    {
        _worldService.UseGangZoneCheck(_gangZone, true);
        _worldService.UseGangZoneCheck(_gangZone, false);
    }
}
