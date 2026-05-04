using Shouldly;
using SampSharp.Entities.SAMP.Commands.Core;
using Xunit;

namespace SampSharp.Entities.SAMP.Commands.Tests.Core;

public class CommandGroupTests
{
    [Fact]
    public void Constructor_WithSinglePart_CreatesGroup()
    {
        var group = new CommandGroup("admin");
        group.Parts.Count.ShouldBe(1);
        group.Parts[0].ShouldBe("admin");
    }

    [Fact]
    public void Constructor_WithMultipleParts_CreatesHierarchicalGroup()
    {
        var parts = new[] { "admin", "player", "ban" };
        var group = new CommandGroup(parts);
        group.Parts.ShouldBe(parts);
    }

    [Fact]
    public void Equals_SamePartsAndOrder_ReturnsTrue()
    {
        var group1 = new CommandGroup(new[] { "admin", "player" });
        var group2 = new CommandGroup(new[] { "admin", "player" });
        group1.ShouldBe(group2);
    }

    [Fact]
    public void Equals_DifferentOrder_ReturnsFalse()
    {
        var group1 = new CommandGroup(new[] { "admin", "player" });
        var group2 = new CommandGroup(new[] { "player", "admin" });
        group1.ShouldNotBe(group2);
    }

    [Fact]
    public void ToString_ReturnsSpaceSeparatedParts()
    {
        var group = new CommandGroup(new[] { "admin", "player", "ban" });
        group.ToString().ShouldBe("admin player ban");
    }

    [Fact]
    public void Parent_SinglePart_ReturnsNull()
    {
        var group = new CommandGroup("admin");
        group.Depth.ShouldBe(1);
    }

    [Fact]
    public void Parent_MultipleParts_ReturnsParentGroup()
    {
        var group = new CommandGroup(new[] { "admin", "player", "ban" });
        var parent = group.GetParent(2);
        parent.Parts.ShouldBe(new[] { "admin", "player" });
    }
}