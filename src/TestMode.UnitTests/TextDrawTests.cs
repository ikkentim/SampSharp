using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using Shouldly;
using Xunit;

namespace TestMode.UnitTests;

public class TextDrawTests : TestBase
{
    private readonly TextDraw _textDraw;
    
    public TextDrawTests()
    {
        _textDraw = Services.GetRequiredService<IWorldService>().CreateTextDraw(Vector2.One, "text");
    }

    protected override void Cleanup()
    {
        _textDraw.DestroyEntity();
    }

    [Fact]
    public void CreatePlayerTextDraw_should_set_properties()
    {
        _textDraw.Position.ShouldBe(Vector2.One);
        _textDraw.Text.ShouldBe("text");
    }

    [Fact]
    public void Text_should_roundtrip()
    {
        _textDraw.Text = "new text";
        _textDraw.Text.ShouldBe("new text");
    }
    
    [Fact]
    public void Show_should_succeed()
    {
        _textDraw.Show();
    }

    [Fact]
    public void Show_player_should_succeed()
    {
        _textDraw.Show(Player);
    }
    
    [Fact]
    public void Hide_should_succeed()
    {
        _textDraw.Hide();
    }

    [Fact]
    public void Hide_player_should_succeed()
    {
        _textDraw.Hide(Player);
    }

    [Fact]
    public void PreviewModel_should_roundtrip()
    {
        _textDraw.PreviewModel = 123;
        _textDraw.PreviewModel.ShouldBe(123);
    }

    [Fact]
    public void Position_should_roundtrip()
    {
        _ = _textDraw.Position;
    }

    [Fact]
    public void SetPreviewRotation_should_succeed()
    {
        _textDraw.SetPreviewRotation(Vector3.One);
    }

    [Fact]
    public void SetPreviewVehicleColor_should_succeed()
    {
        _textDraw.SetPreviewVehicleColor(1, 2);
    }

    [Fact]
    public void LetterSize_should_roundtrip()
    {
        _textDraw.LetterSize = new Vector2(2, 3);
        _textDraw.LetterSize.ShouldBe(new Vector2(2, 3));
    }

    [Fact]
    public void TextSize_should_roundtrip()
    {
        _textDraw.TextSize = new Vector2(4, 5);
        _textDraw.TextSize.ShouldBe(new Vector2(4, 5));
    }

    [Fact]
    public void Alignment_should_roundtrip()
    {
        _textDraw.Alignment = TextDrawAlignment.Center;
        _textDraw.Alignment.ShouldBe(TextDrawAlignment.Center);
    }

    [Fact]
    public void ForeColor_should_roundtrip()
    {
        var color = new Color(255, 0, 0);
        _textDraw.ForeColor = color;
        _textDraw.ForeColor.ShouldBe(color);
    }

    [Fact]
    public void UseBox_should_roundtrip()
    {
        _textDraw.UseBox = true;
        _textDraw.UseBox.ShouldBeTrue();
    }

    [Fact]
    public void BoxColor_should_roundtrip()
    {
        var color = new Color(0, 255, 0);
        _textDraw.BoxColor = color;
        _textDraw.BoxColor.ShouldBe(color);
    }

    [Fact]
    public void Shadow_should_roundtrip()
    {
        _textDraw.Shadow = 2;
        _textDraw.Shadow.ShouldBe(2);
    }

    [Fact]
    public void Outline_should_roundtrip()
    {
        _textDraw.Outline = 1;
        _textDraw.Outline.ShouldBe(1);
    }

    [Fact]
    public void BackColor_should_roundtrip()
    {
        var color = new Color(0, 0, 255);
        _textDraw.BackColor = color;
        _textDraw.BackColor.ShouldBe(color);
    }

    [Fact]
    public void Font_should_roundtrip()
    {
        _textDraw.Font = TextDrawFont.DrawSprite;
        _textDraw.Font.ShouldBe(TextDrawFont.DrawSprite);
    }

    [Fact]
    public void Proportional_should_roundtrip()
    {
        _textDraw.Proportional = true;
        _textDraw.Proportional.ShouldBeTrue();
    }

    [Fact]
    public void Selectable_should_roundtrip()
    {
        _textDraw.Selectable = true;
        _textDraw.Selectable.ShouldBeTrue();
    }

    [Fact]
    public void SetPreviewRotation_with_zoom_should_succeed()
    {
        _textDraw.SetPreviewRotation(Vector3.One, 2.0f);
    }
}