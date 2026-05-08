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

        var span = StringSpan.For("test");
        var found = tree.FindCommand(ref span);
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

        var span = StringSpan.For("admin money give");
        var found = tree.FindCommand(ref span);
        found.ShouldNotBeNull();
        found.Name.ShouldBe("give");
        span.Length.ShouldBe(0); // all consumed
    }

    [Fact]
    public void FindCommand_UnknownCommand_ReturnsNull()
    {
        var tree = new CommandTree();

        var span = StringSpan.For("unknown");
        var found = tree.FindCommand(ref span);

        found.ShouldBeNull();
    }

    [Fact]
    public void FindCommand_EmptyInput_ReturnsNull()
    {
        var tree = new CommandTree();
        tree.Register(CreateCommandSet("test"));

        var span = StringSpan.Empty;
        var found = tree.FindCommand(ref span);

        found.ShouldBeNull();
    }

    [Fact]
    public void FindCommand_CaseInsensitive()
    {
        var tree = new CommandTree();
        tree.Register(CreateCommandSet("Test"));

        var span1 = StringSpan.For("test");
        tree.FindCommand(ref span1).ShouldNotBeNull();

        var span2 = StringSpan.For("TEST");
        tree.FindCommand(ref span2).ShouldNotBeNull();

        var span3 = StringSpan.For("Test");
        tree.FindCommand(ref span3).ShouldNotBeNull();
    }

    [Fact]
    public void FindCommand_AdvancesSpanByConsumedWords()
    {
        var tree = new CommandTree();
        var group = new CommandGroup("admin");
        tree.Register(CreateCommandSet("kick", group));

        var span = StringSpan.For("admin kick arg1");
        tree.FindCommand(ref span);

        // span should be advanced past "admin kick"
        span.TrimStart().ToString().ShouldBe("arg1");
    }

    [Fact]
    public void FindCommand_PartialMatch_StopsAtDeepestNode()
    {
        var tree = new CommandTree();
        var group = new CommandGroup("admin");
        tree.Register(CreateCommandSet("kick", group));

        // "foo" is not a child of "admin", so traversal stops at "admin" (no CommandSet -> null)
        var span = StringSpan.For("admin foo");
        var found = tree.FindCommand(ref span);

        found.ShouldBeNull();
        span.TrimStart().ToString().ShouldBe("foo");
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

        var span = StringSpan.For("pm");
        var found = tree.FindCommand(ref span);

        found.ShouldNotBeNull();
    }

    [Fact]
    public void Clear_RemovesAllCommands()
    {
        var tree = new CommandTree();
        tree.Register(CreateCommandSet("test"));
        tree.Register(CreateCommandSet("kick", new CommandGroup("admin")));

        tree.Clear();

        var span1 = StringSpan.For("test");
        tree.FindCommand(ref span1).ShouldBeNull();
        var span2 = StringSpan.For("admin kick");
        tree.FindCommand(ref span2).ShouldBeNull();
    }

    [Fact]
    public void Register_MultipleCommands_AllFindable()
    {
        var tree = new CommandTree();
        tree.Register(CreateCommandSet("help"));
        tree.Register(CreateCommandSet("kick", new CommandGroup("admin")));
        tree.Register(CreateCommandSet("ban", new CommandGroup("admin")));

        var span1 = StringSpan.For("help");
        tree.FindCommand(ref span1).ShouldNotBeNull();
        var span2 = StringSpan.For("admin kick");
        tree.FindCommand(ref span2).ShouldNotBeNull();
        var span3 = StringSpan.For("admin ban");
        tree.FindCommand(ref span3).ShouldNotBeNull();
    }

    [Fact]
    public void Root_IsAccessible()
    {
        var tree = new CommandTree();

        tree.Root.ShouldNotBeNull();
    }
}
