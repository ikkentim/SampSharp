using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using Shouldly;
using Xunit;

namespace TestMode.UnitTests;

public class MenuTest : TestBase
{
    private readonly Menu _menu;

    public MenuTest()
    {
        _menu = Services.GetRequiredService<IWorldService>().CreateMenu("title", Vector2.One, 400, 200);
    }

    protected override void Cleanup()
    {
        _menu.DestroyEntity();
    }

    [Fact]
    public void CreateMenu_should_set_properties()
    {
        _menu.Title.ShouldBe("title");
        _menu.Position.ShouldBe(Vector2.One);
        _menu.Columns.ShouldBe(2);
        _menu.Col0Width.ShouldBe(400);
        _menu.Col1Width.ShouldBe(200);
    }

    [Fact]
    public void Col0Header_should_roundtrip()
    {
        _menu.Col0Header = "col0";
        _menu.Col0Header.ShouldBe("col0");
    }

    [Fact]
    public void Col1Header_should_roundtrip()
    {
        _menu.Col1Header = "col1";
        _menu.Col1Header.ShouldBe("col1");
    }

    [Fact]
    public void AddItem_should_succeed()
    {
        _menu.AddItem("left", "right");
    }

    [Fact]
    public void AddItem_should_throw_on_invalid_col_count()
    {
        var ex = Should.Throw<ArgumentNullException>(() => _menu.AddItem("left"));

        ex.ParamName.ShouldBe("col1Text");
    }

    [Fact]
    public void Show_should_succeed()
    {
        _menu.Show(Player);
    }

    [Fact]
    public void Hide_should_succeed()
    {
        _menu.Hide(Player);
    }

    [Fact]
    public void Disable_should_succeed()
    {
        _menu.Disable();

        _menu.IsEnabled().ShouldBeFalse();
    }

    [Fact]
    public void DisableRow_should_succeed()
    {
        _menu.AddItem("a", "b");
        _menu.AddItem("c", "d");
        _menu.DisableRow(0);

        _menu.IsRowEnabled(0).ShouldBeFalse();
        _menu.IsRowEnabled(1).ShouldBeTrue();
    }

    [Fact]
    public void GetCell_should_return_text()
    {
        _menu.AddItem("left", "right");

        _menu.GetCell(0, 0).ShouldBe("left");
        _menu.GetCell(1, 0).ShouldBe("right");
    }

    [Fact]
    public void GetRowCount_should_return_count()
    {
        _menu.AddItem("a", "b");
        _menu.AddItem("c", "d");

        _menu.GetRowCount(0).ShouldBe(2);
        _menu.GetRowCount(1).ShouldBe(2);
    }
}