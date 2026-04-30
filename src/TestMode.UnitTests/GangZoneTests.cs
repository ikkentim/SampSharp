using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using SampSharp.OpenMp.Core.Api;
using Shouldly;
using Xunit;

namespace TestMode.UnitTests;

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
}