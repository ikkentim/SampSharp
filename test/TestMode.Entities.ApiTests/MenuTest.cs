using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using SampSharp.OpenMp.Core.Api;
using Shouldly;
using Xunit;

namespace TestMode.Entities.ApiTests;

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

        ((IMenu)_menu).IsEnabled().ShouldBeFalse();
    }

    [Fact]
    public void DisableRow_should_succeed()
    {
        _menu.AddItem("a", "b");
        _menu.AddItem("c", "d");
        _menu.DisableRow(0);

        ((IMenu)_menu).IsRowEnabled(0).ShouldBeFalse();
        ((IMenu)_menu).IsRowEnabled(1).ShouldBeTrue();
    }

    [Fact]
    public void IsEnabled_should_be_true_initially()
    {
        _menu.IsEnabled.ShouldBeTrue();
    }

    [Fact]
    public void Col0RowCount_should_reflect_added_items()
    {
        _menu.AddItem("first", "a");
        _menu.AddItem("second", "b");
        _menu.Col0RowCount.ShouldBe(2);
    }

    [Fact]
    public void Col1RowCount_should_reflect_added_items()
    {
        _menu.AddItem("x", "one");
        _menu.AddItem("y", "two");
        _menu.Col1RowCount.ShouldBe(2);
    }

    [Fact]
    public void GetCell_should_return_correct_text()
    {
        _menu.AddItem("left text", "right text");
        _menu.GetCell(0, 0).ShouldBe("left text");
        _menu.GetCell(0, 1).ShouldBe("right text");
    }

    [Fact]
    public void IsRowEnabled_via_menu_property_should_work()
    {
        _menu.AddItem("a", "b");
        _menu.DisableRow(0);
        _menu.IsRowEnabled(0).ShouldBeFalse();
    }
}