using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using SampSharp.OpenMp.Core.Api;
using Shouldly;
using Xunit;

namespace TestMode.Entities.ApiTests;

public class GangZoneTests : TestBase
{
    private readonly GangZone _gangZone;

    public GangZoneTests()
    {
        _gangZone = Services.GetRequiredService<IWorldService>().CreateGangZone(new Vector2(10, 11), new Vector2(20, 21));
        _gangZone.Color = new Colour(255, 0, 0, 100);
    }

    protected override void Cleanup()
    {
        _gangZone?.Destroy();
    }

    [Fact]
    public void CreateGangZone_should_set_properties()
    {
        _gangZone.Min.ShouldBe(new Vector2(10, 11));
        _gangZone.Max.ShouldBe(new Vector2(20, 21));
    }

    [Fact]
    public void Min_should_be_correct()
    {
        _gangZone.Min.ShouldBe(new Vector2(10, 11));
    }

    [Fact]
    public void Max_should_be_correct()
    {
        _gangZone.Max.ShouldBe(new Vector2(20, 21));
    }

    [Fact]
    public void Color_should_rountrip()
    {
        _gangZone.Color = new Color(1, 2, 3, 4);
        _gangZone.Color.ShouldBe(new Color(1, 2, 3, 4));
    }

    [Fact]
    public void Show_should_work()
    {
        _gangZone.Show();
    }

    [Fact]
    public void Hide_should_work()
    {
        _gangZone.Hide();
    }

    [Fact]
    public void Show_should_work_for_player()
    {
        _gangZone.Show(Player);
    }

    [Fact]
    public void Hide_should_work_for_player()
    {
        _gangZone.Show(Player);
        _gangZone.Hide(Player);
    }

    [Fact]
    public void Flash_should_work()
    {
        _gangZone.Flash(Color.White);
    }

    [Fact]
    public void Flash_should_work_for_player()
    {
        _gangZone.Flash(Player, Color.White);
    }

    [Fact]
    public void StopFlash_should_work()
    {
        _gangZone.Flash(Color.White);
        _gangZone.StopFlash();
    }

    [Fact]
    public void StopFlash_should_work_for_player()
    {
        _gangZone.Flash(Player, Color.White);
        _gangZone.StopFlash(Player);
    }

    [Fact]
    public void Show_with_color_should_work_for_player()
    {
        _gangZone.Show(Player, new Color(0, 255, 0, 200));
    }

    [Fact]
    public void IsShownForPlayer_should_be_true_after_show()
    {
        _gangZone.Show(Player);
        _gangZone.IsShownForPlayer(Player).ShouldBeTrue();
    }

    [Fact]
    public void IsShownForPlayer_should_be_false_after_hide()
    {
        _gangZone.Show(Player);
        _gangZone.Hide(Player);
        _gangZone.IsShownForPlayer(Player).ShouldBeFalse();
    }

    [Fact]
    public void IsFlashingForPlayer_should_be_true_after_flash()
    {
        _gangZone.Show(Player);
        _gangZone.Flash(Player, Color.White);
        _gangZone.IsFlashingForPlayer(Player).ShouldBeTrue();
    }

    [Fact]
    public void IsFlashingForPlayer_should_be_false_after_stop_flash()
    {
        _gangZone.Show(Player);
        _gangZone.Flash(Player, Color.White);
        _gangZone.StopFlash(Player);
        _gangZone.IsFlashingForPlayer(Player).ShouldBeFalse();
    }

    [Fact(Skip = "Broken test")]
    public void GetColorForPlayer_should_return_shown_color()
    {
        var color = new Color(0, 128, 255, 200);
        _gangZone.Show(Player, color);
        _gangZone.GetColorForPlayer(Player).ShouldBe(color);
    }

    [Fact(Skip = "Broken test")]
    public void GetFlashingColorForPlayer_should_return_flash_color()
    {
        _gangZone.Show(Player);
        _gangZone.Flash(Player, Color.White);
        _gangZone.GetFlashingColorForPlayer(Player).ShouldBe(Color.White);
    }

    [Fact]
    public void IsPlayerInside_should_return_false_without_check_enabled()
    {
        _gangZone.IsPlayerInside(Player).ShouldBeFalse();
    }

    [Fact]
    public void GetShownFor_should_include_player_after_show()
    {
        _gangZone.Show(Player);
        _gangZone.GetShownFor().ShouldContain(Player);
    }

    [Fact]
    public void MinX_should_be_correct()
    {
        _gangZone.MinX.ShouldBe(10);
    }

    [Fact]
    public void MinY_should_be_correct()
    {
        _gangZone.MinY.ShouldBe(11);
    }

    [Fact]
    public void MaxX_should_be_correct()
    {
        _gangZone.MaxX.ShouldBe(20);
    }

    [Fact]
    public void MaxY_should_be_correct()
    {
        _gangZone.MaxY.ShouldBe(21);
    }

    [Fact]
    public void SetPosition_should_update_bounds()
    {
        _gangZone.SetPosition(new Vector2(5, 6), new Vector2(15, 16));
        _gangZone.Min.ShouldBe(new Vector2(5, 6));
        _gangZone.Max.ShouldBe(new Vector2(15, 16));
    }
}