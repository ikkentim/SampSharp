using Shouldly;
using Xunit;

namespace SampSharp.Entities.SAMP.Commands.Tests.Core;

public class CommandAliasTests
{
    [Fact]
    public void Constructor_StoresValue()
    {
        var alias = new CommandAlias("kick");
        alias.Name.ShouldBe("kick");
    }

    [Fact]
    public void Equals_SameValue_ReturnsTrue()
    {
        var alias1 = new CommandAlias("kick");
        var alias2 = new CommandAlias("kick");
        alias1.ShouldBe(alias2);
    }

    [Fact]
    public void Equals_DifferentValue_ReturnsFalse()
    {
        var alias1 = new CommandAlias("kick");
        var alias2 = new CommandAlias("ban");
        alias1.ShouldNotBe(alias2);
    }

    [Fact]
    public void ToString_ReturnsValue()
    {
        var alias = new CommandAlias("kick");
        alias.ToString().ShouldBe("kick");
    }
}