using System;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Core;

/// <summary>
/// Tests for CommandTree, the hierarchical command lookup structure.
/// </summary>
public class CommandTreeTests
{
    private static CommandSet CreateCommandSet(string name, CommandGroup? group = null)
    {
        var method = typeof(CommandTreeTests).GetMethod(nameof(DummyMethod), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        var mockInvoker = new Mock<CommandInvoker>();
        var definition = new CommandDefinition(
            name, group, method, method.GetParameters(),
            typeof(CommandTreeTests), Array.Empty<CommandParameterInfo>(),
            mockInvoker.Object, 0,
            Array.Empty<CommandAlias>(), Array.Empty<CommandTag>());

        return new CommandSet(name, group, [definition]);
    }

    private void DummyMethod() { }

    [Fact]
    public void Register_SingleCommand_CanBeFound()
    {
        var tree = new CommandTree();
        var commandSet = CreateCommandSet("test");

        tree.Register(commandSet);

        var found = tree.FindCommand(["test"], out _);
        found.ShouldNotBeNull();
        found.Name.ShouldBe("test");
    }

    [Fact]
    public void Register_NullCommandSet_ThrowsArgumentNullException()
    {
        var tree = new CommandTree();

        Should.Throw<ArgumentNullException>(() => tree.Register(null!));
    }

    [Fact]
    public void Register_CommandWithGroup_CanBeFoundByFullPath()
    {
        var tree = new CommandTree();
        var group = new CommandGroup("admin", "money");
        var commandSet = CreateCommandSet("give", group);

        tree.Register(commandSet);

        var found = tree.FindCommand(["admin", "money", "give"], out var consumed);
        found.ShouldNotBeNull();
        found.Name.ShouldBe("give");
        consumed.ShouldBe(3);
    }

    [Fact]
    public void FindCommand_UnknownCommand_ReturnsNull()
    {
        var tree = new CommandTree();

        var found = tree.FindCommand(["unknown"], out _);

        found.ShouldBeNull();
    }

    [Fact]
    public void FindCommand_EmptyPath_ReturnsNull()
    {
        var tree = new CommandTree();
        tree.Register(CreateCommandSet("test"));

        var found = tree.FindCommand([], out _);

        found.ShouldBeNull();
    }

    [Fact]
    public void FindCommand_CaseInsensitive()
    {
        var tree = new CommandTree();
        tree.Register(CreateCommandSet("Test"));

        tree.FindCommand(["test"], out _).ShouldNotBeNull();
        tree.FindCommand(["TEST"], out _).ShouldNotBeNull();
        tree.FindCommand(["Test"], out _).ShouldNotBeNull();
    }

    [Fact]
    public void FindCommand_ReturnsConsumedCount()
    {
        var tree = new CommandTree();
        var group = new CommandGroup("admin");
        tree.Register(CreateCommandSet("kick", group));

        tree.FindCommand(["admin", "kick"], out var consumed);

        consumed.ShouldBe(2);
    }

    [Fact]
    public void FindCommand_PartialMatch_StopsAtDeepestNode()
    {
        var tree = new CommandTree();
        var group = new CommandGroup("admin");
        tree.Register(CreateCommandSet("kick", group));

        // Only "admin" is in the tree (as an intermediate node), not "admin foo"
        tree.FindCommand(["admin", "foo"], out var consumed);

        consumed.ShouldBe(1);
    }

    [Fact]
    public void RegisterAlias_NullAliasName_ThrowsArgumentNullException()
    {
        var tree = new CommandTree();
        var commandSet = CreateCommandSet("test");

        Should.Throw<ArgumentNullException>(() => tree.RegisterAlias(null!, commandSet));
    }

    [Fact]
    public void RegisterAlias_NullCommandSet_ThrowsArgumentNullException()
    {
        var tree = new CommandTree();

        Should.Throw<ArgumentNullException>(() => tree.RegisterAlias("alias", null!));
    }

    [Fact]
    public void RegisterAlias_AliasCanBeFound()
    {
        var tree = new CommandTree();
        var commandSet = CreateCommandSet("message");
        tree.Register(commandSet);
        tree.RegisterAlias("pm", commandSet);

        var found = tree.FindCommand(["pm"], out _);

        found.ShouldNotBeNull();
    }

    [Fact]
    public void Clear_RemovesAllCommands()
    {
        var tree = new CommandTree();
        tree.Register(CreateCommandSet("test"));
        tree.Register(CreateCommandSet("kick", new CommandGroup("admin")));

        tree.Clear();

        tree.FindCommand(["test"], out _).ShouldBeNull();
        tree.FindCommand(["admin", "kick"], out _).ShouldBeNull();
    }

    [Fact]
    public void Register_MultipleCommands_AllFindable()
    {
        var tree = new CommandTree();
        tree.Register(CreateCommandSet("help"));
        tree.Register(CreateCommandSet("kick", new CommandGroup("admin")));
        tree.Register(CreateCommandSet("ban", new CommandGroup("admin")));

        tree.FindCommand(["help"], out _).ShouldNotBeNull();
        tree.FindCommand(["admin", "kick"], out _).ShouldNotBeNull();
        tree.FindCommand(["admin", "ban"], out _).ShouldNotBeNull();
    }

    [Fact]
    public void Root_IsAccessible()
    {
        var tree = new CommandTree();

        tree.Root.ShouldNotBeNull();
    }
}
