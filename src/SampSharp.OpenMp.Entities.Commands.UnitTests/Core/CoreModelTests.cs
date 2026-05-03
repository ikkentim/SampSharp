using System.Reflection;
using FluentAssertions;
using SampSharp.Entities.SAMP.Commands.Core;
using Xunit;

namespace SampSharp.Entities.SAMP.Commands.Tests.Core;

public class CommandGroupTests
{
    [Fact]
    public void Constructor_WithSinglePart_CreatesGroup()
    {
        var group = new CommandGroup("admin");
        group.Parts.Should().HaveCount(1);
        group.Parts[0].Should().Be("admin");
    }

    [Fact]
    public void Constructor_WithMultipleParts_CreatesHierarchicalGroup()
    {
        var parts = new[] { "admin", "player", "ban" };
        var group = new CommandGroup(parts);
        group.Parts.Should().Equal(parts);
    }

    [Fact]
    public void Equals_SamePartsAndOrder_ReturnsTrue()
    {
        var group1 = new CommandGroup(new[] { "admin", "player" });
        var group2 = new CommandGroup(new[] { "admin", "player" });
        group1.Should().Be(group2);
    }

    [Fact]
    public void Equals_DifferentOrder_ReturnsFalse()
    {
        var group1 = new CommandGroup(new[] { "admin", "player" });
        var group2 = new CommandGroup(new[] { "player", "admin" });
        group1.Should().NotBe(group2);
    }

    [Fact]
    public void GetHashCode_SamePartsAndOrder_ReturnsSameHash()
    {
        var group1 = new CommandGroup(new[] { "admin", "player" });
        var group2 = new CommandGroup(new[] { "admin", "player" });
        group1.GetHashCode().Should().Be(group2.GetHashCode());
    }

    [Fact]
    public void ToString_ReturnsSpaceSeparatedParts()
    {
        var group = new CommandGroup(new[] { "admin", "player", "ban" });
        group.ToString().Should().Be("admin player ban");
    }

    [Fact]
    public void Parent_SinglePart_ReturnsNull()
    {
        var group = new CommandGroup("admin");
        group.Parent.Should().BeNull();
    }

    [Fact]
    public void Parent_MultipleParts_ReturnsParentGroup()
    {
        var group = new CommandGroup(new[] { "admin", "player", "ban" });
        var parent = group.Parent;
        parent.Should().NotBeNull();
        parent!.Parts.Should().Equal("admin", "player");
    }
}

public class CommandAliasTests
{
    [Fact]
    public void Constructor_StoresValue()
    {
        var alias = new CommandAlias("kick");
        alias.Value.Should().Be("kick");
    }

    [Fact]
    public void Equals_SameValue_ReturnsTrue()
    {
        var alias1 = new CommandAlias("kick");
        var alias2 = new CommandAlias("kick");
        alias1.Should().Be(alias2);
    }

    [Fact]
    public void Equals_DifferentValue_ReturnsFalse()
    {
        var alias1 = new CommandAlias("kick");
        var alias2 = new CommandAlias("ban");
        alias1.Should().NotBe(alias2);
    }

    [Fact]
    public void ToString_ReturnsValue()
    {
        var alias = new CommandAlias("kick");
        alias.ToString().Should().Be("kick");
    }
}

public class CommandOverloadTests
{
    private readonly MethodInfo _testMethod;

    public CommandOverloadTests()
    {
        _testMethod = typeof(CommandOverloadTests).GetMethod(nameof(DummyCommand), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
    }

    [Fact]
    public void Constructor_StoresMethodInfo()
    {
        var overload = new CommandOverload(_testMethod, typeof(void), new CommandParameter[0]);
        overload.Method.Should().Be(_testMethod);
    }

    [Fact]
    public void IsAsync_VoidReturn_ReturnsFalse()
    {
        var overload = new CommandOverload(_testMethod, typeof(void), new CommandParameter[0]);
        overload.IsAsync.Should().BeFalse();
    }

    [Fact]
    public void IsAsync_TaskReturn_ReturnsTrue()
    {
        var overload = new CommandOverload(_testMethod, typeof(Task), new CommandParameter[0]);
        overload.IsAsync.Should().BeTrue();
    }

    [Fact]
    public void IsAsync_TaskBoolReturn_ReturnsTrue()
    {
        var overload = new CommandOverload(_testMethod, typeof(Task<bool>), new CommandParameter[0]);
        overload.IsAsync.Should().BeTrue();
    }

    private void DummyCommand() { }
}

public class CommandRegistryTests
{
    [Fact]
    public void Register_AddsCommandDefinition()
    {
        var registry = new CommandRegistry();
        var definition = new CommandDefinition(
            name: "test",
            group: null,
            overloads: new CommandOverload[0],
            aliases: null,
            playerCommand: true,
            consoleCommand: false);

        registry.Register(definition);

        registry.TryFind("test").Should().NotBeNull();
    }

    [Fact]
    public void TryFind_ExistingCommand_ReturnsDefinition()
    {
        var registry = new CommandRegistry();
        var definition = new CommandDefinition("kick", null, new CommandOverload[0], null, true, false);
        registry.Register(definition);

        var found = registry.TryFind("kick");
        found.Should().NotBeNull();
        found!.Name.Should().Be("kick");
    }

    [Fact]
    public void TryFind_NonExistentCommand_ReturnsNull()
    {
        var registry = new CommandRegistry();
        registry.TryFind("nonexistent").Should().BeNull();
    }

    [Fact]
    public void GetAll_ReturnsAllCommands()
    {
        var registry = new CommandRegistry();
        registry.Register(new CommandDefinition("test1", null, new CommandOverload[0], null, true, false));
        registry.Register(new CommandDefinition("test2", null, new CommandOverload[0], null, true, false));

        registry.GetAll().Should().HaveCount(2);
    }

    [Fact]
    public void GetPlayerCommands_ReturnsOnlyPlayerCommands()
    {
        var registry = new CommandRegistry();
        registry.Register(new CommandDefinition("player_cmd", null, new CommandOverload[0], null, playerCommand: true, consoleCommand: false));
        registry.Register(new CommandDefinition("console_cmd", null, new CommandOverload[0], null, playerCommand: false, consoleCommand: true));

        var playerCommands = registry.GetPlayerCommands();
        playerCommands.Should().HaveCount(1);
        playerCommands.First().Name.Should().Be("player_cmd");
    }

    [Fact]
    public void GetConsoleCommands_ReturnsOnlyConsoleCommands()
    {
        var registry = new CommandRegistry();
        registry.Register(new CommandDefinition("player_cmd", null, new CommandOverload[0], null, playerCommand: true, consoleCommand: false));
        registry.Register(new CommandDefinition("console_cmd", null, new CommandOverload[0], null, playerCommand: false, consoleCommand: true));

        var consoleCommands = registry.GetConsoleCommands();
        consoleCommands.Should().HaveCount(1);
        consoleCommands.First().Name.Should().Be("console_cmd");
    }
}

public class DispatchResultTests
{
    [Fact]
    public void CreateSuccess_CreatesSuccessResult()
    {
        var result = DispatchResult.CreateSuccess();
        result.Response.Should().Be(DispatchResponse.Success);
    }

    [Fact]
    public void CreateNotFound_CreatesNotFoundResult()
    {
        var result = DispatchResult.CreateNotFound();
        result.Response.Should().Be(DispatchResponse.CommandNotFound);
    }

    [Fact]
    public void CreateInvalidArguments_CreatesInvalidArgumentsResult()
    {
        var result = DispatchResult.CreateInvalidArguments("Usage: /kick <player>");
        result.Response.Should().Be(DispatchResponse.InvalidArguments);
        result.UsageMessage.Should().Be("Usage: /kick <player>");
    }

    [Fact]
    public void CreatePermissionDenied_CreatesPermissionDeniedResult()
    {
        var result = DispatchResult.CreatePermissionDenied("You don't have permission");
        result.Response.Should().Be(DispatchResponse.PermissionDenied);
        result.Message.Should().Be("You don't have permission");
    }

    [Fact]
    public void CreateError_CreatesErrorResult()
    {
        var result = DispatchResult.CreateError("An error occurred");
        result.Response.Should().Be(DispatchResponse.Error);
        result.Message.Should().Be("An error occurred");
    }
}
