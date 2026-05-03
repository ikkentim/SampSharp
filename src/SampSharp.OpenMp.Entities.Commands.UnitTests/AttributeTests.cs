using Shouldly;
using SampSharp.Entities.SAMP.Commands.Attributes;
using Xunit;

namespace SampSharp.Entities.SAMP.Commands.Tests.Attributes;

public class CommandAttributeTests
{
    [Fact]
    public void PlayerCommandAttribute_HasDefaultName()
    {
        var attr = new PlayerCommandAttribute();
        attr.Name.ShouldBeNull();
    }

    [Fact]
    public void PlayerCommandAttribute_SetName()
    {
        var attr = new PlayerCommandAttribute { Name = "custom" };
        attr.Name.ShouldBe("custom");
    }

    [Fact]
    public void ConsoleCommandAttribute_HasDefaultName()
    {
        var attr = new ConsoleCommandAttribute();
        attr.Name.ShouldBeNull();
    }

    [Fact]
    public void ConsoleCommandAttribute_SetName()
    {
        var attr = new ConsoleCommandAttribute { Name = "custom" };
        attr.Name.ShouldBe("custom");
    }

    [Fact]
    public void CommandGroupAttribute_StoresSinglePart()
    {
        var attr = new CommandGroupAttribute("admin");
        attr.Parts.Length.ShouldBe(1);
        attr.Parts[0].ShouldBe("admin");
    }

    [Fact]
    public void CommandGroupAttribute_StoresMultipleParts()
    {
        var attr = new CommandGroupAttribute("admin", "player");
        attr.Parts.Length.ShouldBe(2);
        attr.Parts[0].ShouldBe("admin");
        attr.Parts[1].ShouldBe("player");
    }

    [Fact]
    public void AliasAttribute_StoresSingleAlias()
    {
        var attr = new AliasAttribute("k");
        attr.Aliases.Length.ShouldBe(1);
        attr.Aliases[0].ShouldBe("k");
    }

    [Fact]
    public void AliasAttribute_StoresMultipleAliases()
    {
        var attr = new AliasAttribute("k", "remove");
        attr.Aliases.Length.ShouldBe(2);
        attr.Aliases[0].ShouldBe("k");
        attr.Aliases[1].ShouldBe("remove");
    }

    [Fact]
    public void RequiresPermissionAttribute_StoresPermissions()
    {
        var attr = new RequiresPermissionAttribute("admin", "moderator");
        attr.Permissions.Length.ShouldBe(2);
        attr.Permissions[0].ShouldBe("admin");
        attr.Permissions[1].ShouldBe("moderator");
    }
}
