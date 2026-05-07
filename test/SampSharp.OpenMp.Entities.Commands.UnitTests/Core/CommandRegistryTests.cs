using System;
using System.Linq;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Core;

/// <summary>
/// Tests for CommandRegistry, which manages command registration and lookup.
/// </summary>
public class CommandRegistryTests
{
    private static CommandDefinition CreateCommand(
        string name = "test",
        CommandGroup? group = null,
        CommandAlias[] aliases = null!)
    {
        var method = typeof(CommandRegistryTests).GetMethod(nameof(DummyMethod), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        var parameters = method.GetParameters();
        var mockInvoker = new Mock<CommandInvoker>();

        return new CommandDefinition(
            name,
            group,
            method,
            parameters,
            typeof(CommandRegistryTests),
            Array.Empty<CommandParameterInfo>(),
            mockInvoker.Object,
            0,
            aliases ?? Array.Empty<CommandAlias>(),
            Array.Empty<CommandTag>()
        );
    }

    private void DummyMethod() { }

    [Fact]
    public void Register_SingleCommand_StoresCorrectly()
    {
        var registry = new CommandRegistry();
        var command = CreateCommand("test");

        registry.Register(command);

        var found = ((ICommandRegistry)registry).TryFind("test");
        found.ShouldNotBeNull();
        found.Name.ShouldBe("test");
    }

    [Fact]
    public void Register_CommandWithGroup_StoresWithFullPath()
    {
        var registry = new CommandRegistry();
        var command = CreateCommand("give", new CommandGroup("admin", "money"));

        registry.Register(command);

        var found = ((ICommandRegistry)registry).TryFindByPath(new[] { "admin", "money", "give" });
        found.ShouldNotBeNull();
        found.FullName.ShouldBe("admin money give");
    }

    [Fact]
    public void Register_MultipleOverloads_GroupsTogether()
    {
        var registry = new CommandRegistry();
        var command1 = CreateCommand("test");
        var command2 = CreateCommand("test");

        registry.Register(command1);
        registry.Register(command2);

        var found = ((ICommandRegistry)registry).TryFind("test");
        found.ShouldNotBeNull();
    }

    [Fact]
    public void Register_WithAlias_FindableByAlias()
    {
        var registry = new CommandRegistry();
        var aliases = new[] { new CommandAlias("pm") };
        var command = CreateCommand("message", aliases: aliases);

        registry.Register(command);

        var found = ((ICommandRegistry)registry).TryFind("pm");
        found.ShouldNotBeNull();
    }

    [Fact]
    public void Register_MultipleAliases_AllFindable()
    {
        var registry = new CommandRegistry();
        var aliases = new[] { new CommandAlias("pm"), new CommandAlias("msg") };
        var command = CreateCommand("message", aliases: aliases);

        registry.Register(command);

        ((ICommandRegistry)registry).TryFind("pm").ShouldNotBeNull();
        ((ICommandRegistry)registry).TryFind("msg").ShouldNotBeNull();
    }

    [Fact]
    public void TryFind_NonExistentCommand_ReturnsNull()
    {
        var registry = new CommandRegistry();
        var found = ((ICommandRegistry)registry).TryFind("nonexistent");
        found.ShouldBeNull();
    }

    [Fact]
    public void TryFind_CaseInsensitive()
    {
        var registry = new CommandRegistry();
        var command = CreateCommand("Test");
        registry.Register(command);

        ((ICommandRegistry)registry).TryFind("test").ShouldNotBeNull();
        ((ICommandRegistry)registry).TryFind("TEST").ShouldNotBeNull();
        ((ICommandRegistry)registry).TryFind("Test").ShouldNotBeNull();
    }

    [Fact]
    public void TryFindByPath_WithGroup_FindsCorrectly()
    {
        var registry = new CommandRegistry();
        var command = CreateCommand("ban", new CommandGroup("admin", "player"));
        registry.Register(command);

        var found = ((ICommandRegistry)registry).TryFindByPath(new[] { "admin", "player", "ban" });
        found.ShouldNotBeNull();
        found.FullName.ShouldBe("admin player ban");
    }

    [Fact]
    public void TryFindByPath_PartialPath_FindsClosestNode()
    {
        var registry = new CommandRegistry();
        var command = CreateCommand("test", new CommandGroup("admin", "money", "give"));
        registry.Register(command);

        var found = ((ICommandRegistry)registry).TryFindByPath(new[] { "admin", "money" }, out var consumed);
        // Should find intermediate group if it exists as a node
        consumed.ShouldBe(2);
    }

    [Fact]
    public void TryFindByPath_EmptyPath_ReturnsNull()
    {
        var registry = new CommandRegistry();
        var found = ((ICommandRegistry)registry).TryFindByPath(Array.Empty<string>());
        found.ShouldBeNull();
    }

    [Fact]
    public void TryFindByPath_NullPath_ReturnsNull()
    {
        var registry = new CommandRegistry();
        var found = ((ICommandRegistry)registry).TryFindByPath(null!);
        found.ShouldBeNull();
    }

    [Fact]
    public void GetAll_ReturnsAllCommands()
    {
        var registry = new CommandRegistry();
        var cmd1 = CreateCommand("test1");
        var cmd2 = CreateCommand("test2");
        var cmd3 = CreateCommand("test3");

        registry.Register(cmd1);
        registry.Register(cmd2);
        registry.Register(cmd3);

        var all = ((ICommandRegistry)registry).GetAll().ToList();
        all.Count.ShouldBe(3);
    }

    [Fact]
    public void GetAll_WithMultipleOverloads_IncludesAllOverloads()
    {
        var registry = new CommandRegistry();
        var cmd1 = CreateCommand("test");
        var cmd2 = CreateCommand("test");
        var cmd3 = CreateCommand("test");

        registry.Register(cmd1);
        registry.Register(cmd2);
        registry.Register(cmd3);

        var all = ((ICommandRegistry)registry).GetAll().ToList();
        all.Count.ShouldBe(3);
    }

    [Fact]
    public void GetCommandsInGroup_ReturnsCommandsInGroup()
    {
        var registry = new CommandRegistry();
        var group = new CommandGroup("admin");
        var cmd1 = CreateCommand("kick", group);
        var cmd2 = CreateCommand("ban", group);
        var cmd3 = CreateCommand("test");

        registry.Register(cmd1);
        registry.Register(cmd2);
        registry.Register(cmd3);

        var inGroup = ((ICommandRegistry)registry).GetCommandsInGroup(group).ToList();
        inGroup.Count.ShouldBe(2);
    }

    [Fact]
    public void GetCommandsInGroup_WithNoCommands_ReturnsEmpty()
    {
        var registry = new CommandRegistry();
        var group = new CommandGroup("admin");

        var inGroup = ((ICommandRegistry)registry).GetCommandsInGroup(group).ToList();
        inGroup.ShouldBeEmpty();
    }

    [Fact]
    public void GetGroups_ReturnsAllGroups()
    {
        var registry = new CommandRegistry();
        var group1 = new CommandGroup("admin");
        var group2 = new CommandGroup("player");

        var cmd1 = CreateCommand("kick", group1);
        var cmd2 = CreateCommand("ban", group1);
        var cmd3 = CreateCommand("profile", group2);

        registry.Register(cmd1);
        registry.Register(cmd2);
        registry.Register(cmd3);

        var groups = ((ICommandRegistry)registry).GetGroups().ToList();
        groups.Count.ShouldBe(2);
        groups.ShouldContain(group1);
        groups.ShouldContain(group2);
    }

    [Fact]
    public void GetGroups_WithNoGroups_ReturnsEmpty()
    {
        var registry = new CommandRegistry();
        var cmd1 = CreateCommand("test");
        registry.Register(cmd1);

        var groups = ((ICommandRegistry)registry).GetGroups().ToList();
        groups.ShouldBeEmpty();
    }

    [Fact]
    public void Register_UpdatesExistingOverload()
    {
        var registry = new CommandRegistry();
        var cmd1 = CreateCommand("test");
        var cmd2 = CreateCommand("test");

        registry.Register(cmd1);
        registry.Register(cmd2);

        var all = ((ICommandRegistry)registry).GetAll().ToList();
        all.Count.ShouldBe(2);
    }

    [Fact]
    public void Register_NullCommand_ThrowsArgumentNullException()
    {
        var registry = new CommandRegistry();
        Should.Throw<ArgumentNullException>(() => registry.Register(null!));
    }

    [Fact]
    public void GetCommandGroupByPath_FindsExactMatch()
    {
        var registry = new CommandRegistry();
        var command = CreateCommand("give", new CommandGroup("admin", "money"));
        registry.Register(command);

        var found = registry.GetCommandGroupByPath(new[] { "admin", "money", "give" }, out var consumed);
        found.ShouldNotBeNull();
        consumed.ShouldBe(3);
    }

    [Fact]
    public void GetCommandGroupByPath_EmptyPath_ReturnsNull()
    {
        var registry = new CommandRegistry();
        var found = registry.GetCommandGroupByPath(Array.Empty<string>(), out _);
        found.ShouldBeNull();
    }

    [Fact]
    public void GetCommand_FindsByName()
    {
        var registry = new CommandRegistry();
        var cmd = CreateCommand("test");
        registry.Register(cmd);

        var found = registry.GetCommand("test");
        found.ShouldNotBeNull();
    }

    [Fact]
    public void GetCommand_EmptyName_ReturnsNull()
    {
        var registry = new CommandRegistry();
        var found = registry.GetCommand("");
        found.ShouldBeNull();
    }

    [Fact]
    public void GetCommand_NullName_ReturnsNull()
    {
        var registry = new CommandRegistry();
        var found = registry.GetCommand(null!);
        found.ShouldBeNull();
    }
}
