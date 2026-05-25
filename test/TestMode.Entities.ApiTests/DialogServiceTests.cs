using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using Shouldly;
using Xunit;

namespace TestMode.Entities.ApiTests;

public class DialogServiceTests : TestBase
{
    private IDialogService Sut => Services.GetRequiredService<IDialogService>();

    [Fact]
    public void Show_MessageDialog_should_succeed()
    {
        var dialog = new MessageDialog("Caption", "Content", "OK", "Cancel");
        Should.NotThrow(() => Sut.Show(Player, dialog, _ => { }));
    }

    [Fact]
    public void Show_MessageDialog_with_single_button_should_succeed()
    {
        var dialog = new MessageDialog("Caption", "Content", "OK");
        Should.NotThrow(() => Sut.Show(Player, dialog, _ => { }));
    }

    [Fact]
    public void Show_InputDialog_should_succeed()
    {
        var dialog = new InputDialog("Caption", "Enter something:", "OK", "Cancel");
        Should.NotThrow(() => Sut.Show(Player, dialog, _ => { }));
    }

    [Fact]
    public void Show_InputDialog_password_should_succeed()
    {
        var dialog = new InputDialog("Caption", "Enter password:", "OK") { IsPassword = true };
        Should.NotThrow(() => Sut.Show(Player, dialog, _ => { }));
    }

    [Fact]
    public void Show_ListDialog_should_succeed()
    {
        var dialog = new ListDialog("Caption", "Select", "Cancel");
        dialog.Add("Row 1");
        dialog.Add("Row 2");
        Should.NotThrow(() => Sut.Show(Player, dialog, _ => { }));
    }

    [Fact]
    public void Show_ListDialog_with_tag_should_succeed()
    {
        var dialog = new ListDialog("Caption", "Select");
        dialog.Add("Row 1", tag: 42);
        Should.NotThrow(() => Sut.Show(Player, dialog, _ => { }));
    }

    [Fact]
    public void Show_TablistDialog_without_headers_should_succeed()
    {
        var dialog = new TablistDialog("Caption", "Select", "Cancel", columnCount: 2);
        dialog.Add("Col1", "Col2");
        Should.NotThrow(() => Sut.Show(Player, dialog, _ => { }));
    }

    [Fact]
    public void Show_TablistDialog_with_headers_should_succeed()
    {
        var dialog = new TablistDialog("Caption", "Select", "Cancel", "Header1", "Header2");
        dialog.Add("Col1", "Col2");
        Should.NotThrow(() => Sut.Show(Player, dialog, _ => { }));
    }
}
