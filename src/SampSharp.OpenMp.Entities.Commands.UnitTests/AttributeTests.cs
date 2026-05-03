using FluentAssertions;
using SampSharp.Entities.SAMP.Commands.Attributes;
using Xunit;

namespace SampSharp.Entities.SAMP.Commands.Tests.Attributes;

public class CommandAttributeTests
{
    [Fact]
    public void PlayerCommandAttribute_HasDefaultName()
    {
        var attr = new PlayerCommandAttribute();
        attr.Name.Should().BeNull();
    }

    [Fact]
    public void PlayerCommandAttribute_SetName()
    {
        var attr = new PlayerCommandAttribute { Name = "custom" };
        attr.Name.Should().Be("custom");
    }

    [Fact]
    public void ConsoleCommandAttribute_HasDefaultName()
    {
        var attr = new ConsoleCommandAttribute();
        attr.Name.Should().BeNull();
    }

    [Fact]
    public void ConsoleCommandAttribute_SetName()
    {
        var attr = new ConsoleCommandAttribute { Name = "custom" };
        attr.Name.Should().Be("custom");
    }

    [Fact]
    public void CommandGroupAttribute_StoresSinglePart()
    {
        var attr = new CommandGroupAttribute("admin");
        attr.Parts.Should().HaveCount(1);
        attr.Parts[0].Should().Be("admin");
    }

    [Fact]
    public void CommandGroupAttribute_StoresMultipleParts()
    {
        var attr = new CommandGroupAttribute("admin", "player");
        attr.Parts.Should().HaveCount(2);
        attr.Parts[0].Should().Be("admin");
        attr.Parts[1].Should().Be("player");
    }

    [Fact]
    public void AliasAttribute_StoresSingleAlias()
    {
        var attr = new AliasAttribute("k");
        attr.Aliases.Should().HaveCount(1);
        attr.Aliases[0].Should().Be("k");
    }

    [Fact]
    public void AliasAttribute_StoresMultipleAliases()
    {
        var attr = new AliasAttribute("k", "remove");
        attr.Aliases.Should().HaveCount(2);
        attr.Aliases[0].Should().Be("k");
        attr.Aliases[1].Should().Be("remove");
    }

    [Fact]
    public void RequiresPermissionAttribute_StoresPermissions()
    {
        var attr = new RequiresPermissionAttribute("admin", "moderator");
        attr.Permissions.Should().HaveCount(2);
        attr.Permissions[0].Should().Be("admin");
        attr.Permissions[1].Should().Be("moderator");
    }
}
