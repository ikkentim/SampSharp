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

    private static CommandRegistry CreateRegistry()
    {
        return new CommandRegistry(StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Register_SingleCommand_StoresCorrectly()
    {
        var registry = CreateRegistry();
        var command = CreateCommand("test");

        registry.Register(command);

        var all = ((ICommandRegistry)registry).GetAll().ToList();
        all.Count.ShouldBe(1);
        all[0].Name.ShouldBe("test");
    }

    [Fact]
    public void Register_CommandWithGroup_CanBeFoundByPath()
    {
        var registry = CreateRegistry();
        var command = CreateCommand("give", new CommandGroup("admin", "money"));

        registry.Register(command);

        var span = StringSpan.For("admin money give");
        var found = registry.GetCommandGroupByPath(ref span);
        found.ShouldNotBeNull();
        found![0].FullName.ShouldBe("admin money give");
    }

    [Fact]
    public void Register_MultipleOverloads_AllStoredInGetAll()
    {
        var registry = CreateRegistry();
        var command1 = CreateCommand("test");
        var command2 = CreateCommand("test");

        registry.Register(command1);
        registry.Register(command2);

        var all = ((ICommandRegistry)registry).GetAll().ToList();
        all.Count.ShouldBe(2);
    }

    [Fact]
    public void Register_WithAlias_FindableByAlias()
    {
        var registry = CreateRegistry();
        var aliases = new[] { new CommandAlias("pm") };
        var command = CreateCommand("message", aliases: aliases);

        registry.Register(command);

        var span = StringSpan.For("pm");
        var found = registry.GetCommandGroupByPath(ref span);
        found.ShouldNotBeNull();
    }

    [Fact]
    public void Register_MultipleAliases_AllFindable()
    {
        var registry = CreateRegistry();
        var aliases = new[] { new CommandAlias("pm"), new CommandAlias("msg") };
        var command = CreateCommand("message", aliases: aliases);

        registry.Register(command);

        var span1 = StringSpan.For("pm");
        registry.GetCommandGroupByPath(ref span1).ShouldNotBeNull();
        var span2 = StringSpan.For("msg");
        registry.GetCommandGroupByPath(ref span2).ShouldNotBeNull();
    }

    [Fact]
    public void Register_MultipleOverloadsSharedAlias_AllReachableViaAlias()
    {
        var registry = CreateRegistry();
        var aliases1 = new[] { new CommandAlias("pm") };
        var aliases2 = new[] { new CommandAlias("pm") };
        var command1 = CreateCommand("message", aliases: aliases1);
        var command2 = CreateCommand("message", aliases: aliases2);

        registry.Register(command1);
        registry.Register(command2);

        var span = StringSpan.For("pm");
        var found = registry.GetCommandGroupByPath(ref span);
        found.ShouldNotBeNull();
        found!.Count.ShouldBe(2);
    }

    [Fact]
    public void GetAll_ReturnsAllCommands()
    {
        var registry = CreateRegistry();
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
        var registry = CreateRegistry();
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
        var registry = CreateRegistry();
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
        var registry = CreateRegistry();
        var group = new CommandGroup("admin");

        var inGroup = ((ICommandRegistry)registry).GetCommandsInGroup(group).ToList();
        inGroup.ShouldBeEmpty();
    }

    [Fact]
    public void GetGroups_ReturnsAllGroups()
    {
        var registry = CreateRegistry();
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
        var registry = CreateRegistry();
        var cmd1 = CreateCommand("test");
        registry.Register(cmd1);

        var groups = ((ICommandRegistry)registry).GetGroups().ToList();
        groups.ShouldBeEmpty();
    }

    [Fact]
    public void Register_NullCommand_ThrowsArgumentNullException()
    {
        var registry = CreateRegistry();
        Should.Throw<ArgumentNullException>(() => registry.Register(null!));
    }

    [Fact]
    public void GetCommandGroupByPath_FindsExactMatch()
    {
        var registry = CreateRegistry();
        var command = CreateCommand("give", new CommandGroup("admin", "money"));
        registry.Register(command);

        var span = StringSpan.For("admin money give");
        var found = registry.GetCommandGroupByPath(ref span);
        found.ShouldNotBeNull();
        span.Length.ShouldBe(0); // all words consumed
    }

    [Fact]
    public void GetCommandGroupByPath_EmptyInput_ReturnsNull()
    {
        var registry = CreateRegistry();
        var span = StringSpan.Empty;
        var found = registry.GetCommandGroupByPath(ref span);
        found.ShouldBeNull();
    }
}
