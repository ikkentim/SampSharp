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
    private static CommandDefinition CreateDefinition(string name, CommandGroup? group = null)
    {
        var method = typeof(CommandTreeTests).GetMethod(nameof(DummyMethod), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        var mockInvoker = new Mock<CommandInvoker>();
        return new CommandDefinition(
            name, group, method, method.GetParameters(),
            typeof(CommandTreeTests), Array.Empty<CommandParameterInfo>(),
            mockInvoker.Object, 0,
            Array.Empty<CommandAlias>(), Array.Empty<CommandTag>());
    }

    private void DummyMethod() { }

    private static CommandTree CreateTree()
    {
        return new CommandTree(StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Register_SingleCommand_CanBeFound()
    {
        var tree = CreateTree();
        var def = CreateDefinition("test");

        tree.Register(def, null, "test");

        var span = StringSpan.For("test");
        var found = tree.FindCommand(ref span);
        found.ShouldNotBeNull();
        found.Count.ShouldBe(1);
        found[0].Name.ShouldBe("test");
    }

    [Fact]
    public void Register_NullCommand_ThrowsArgumentNullException()
    {
        var tree = CreateTree();

        Should.Throw<ArgumentNullException>(() => tree.Register(null!, null, "test"));
    }

    [Fact]
    public void Register_NullName_ThrowsArgumentNullException()
    {
        var tree = CreateTree();
        var def = CreateDefinition("test");

        Should.Throw<ArgumentNullException>(() => tree.Register(def, null, null!));
    }

    [Fact]
    public void Register_CommandWithGroup_CanBeFoundByFullPath()
    {
        var tree = CreateTree();
        var group = new CommandGroup("admin", "money");
        var def = CreateDefinition("give", group);

        tree.Register(def, group, "give");

        var span = StringSpan.For("admin money give");
        var found = tree.FindCommand(ref span);
        found.ShouldNotBeNull();
        found![0].Name.ShouldBe("give");
        span.Length.ShouldBe(0); // all consumed
    }

    [Fact]
    public void FindCommand_UnknownCommand_ReturnsNull()
    {
        var tree = CreateTree();

        var span = StringSpan.For("unknown");
        var found = tree.FindCommand(ref span);

        found.ShouldBeNull();
    }

    [Fact]
    public void FindCommand_EmptyInput_ReturnsNull()
    {
        var tree = CreateTree();
        var def = CreateDefinition("test");
        tree.Register(def, null, "test");

        var span = StringSpan.Empty;
        var found = tree.FindCommand(ref span);

        found.ShouldBeNull();
    }

    [Fact]
    public void FindCommand_CaseInsensitive()
    {
        var tree = CreateTree();
        var def = CreateDefinition("Test");
        tree.Register(def, null, "Test");

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
        var tree = CreateTree();
        var group = new CommandGroup("admin");
        var def = CreateDefinition("kick", group);
        tree.Register(def, group, "kick");

        var span = StringSpan.For("admin kick arg1");
        tree.FindCommand(ref span);

        // span should be advanced past "admin kick"
        span.TrimStart().ToString().ShouldBe("arg1");
    }

    [Fact]
    public void FindCommand_PartialMatch_StopsAtDeepestNode()
    {
        var tree = CreateTree();
        var group = new CommandGroup("admin");
        var def = CreateDefinition("kick", group);
        tree.Register(def, group, "kick");

        // "foo" is not a child of "admin", so traversal stops at "admin" which has no commands
        // registered, returning null.
        var span = StringSpan.For("admin foo");
        var found = tree.FindCommand(ref span);

        found.ShouldBeNull();
        span.TrimStart().ToString().ShouldBe("foo");
    }

    [Fact]
    public void Register_MultipleOverloads_SameNode_AccumulatesAll()
    {
        var tree = CreateTree();
        var def1 = CreateDefinition("test");
        var def2 = CreateDefinition("test");

        tree.Register(def1, null, "test");
        tree.Register(def2, null, "test");

        var span = StringSpan.For("test");
        var found = tree.FindCommand(ref span);

        found.ShouldNotBeNull();
        found!.Count.ShouldBe(2);
    }

    [Fact]
    public void Register_AliasAsTopLevel_CanBeFound()
    {
        var tree = CreateTree();
        var def = CreateDefinition("message");
        tree.Register(def, null, "message");
        tree.Register(def, null, "pm");

        var span = StringSpan.For("pm");
        var found = tree.FindCommand(ref span);

        found.ShouldNotBeNull();
    }

    [Fact]
    public void Clear_RemovesAllCommands()
    {
        var tree = CreateTree();
        var def1 = CreateDefinition("test");
        var def2 = CreateDefinition("kick", new CommandGroup("admin"));
        tree.Register(def1, null, "test");
        tree.Register(def2, new CommandGroup("admin"), "kick");

        tree.Clear();

        var span1 = StringSpan.For("test");
        tree.FindCommand(ref span1).ShouldBeNull();
        var span2 = StringSpan.For("admin kick");
        tree.FindCommand(ref span2).ShouldBeNull();
    }

    [Fact]
    public void Register_MultipleCommands_AllFindable()
    {
        var tree = CreateTree();
        var def1 = CreateDefinition("help");
        var def2 = CreateDefinition("kick", new CommandGroup("admin"));
        var def3 = CreateDefinition("ban", new CommandGroup("admin"));
        tree.Register(def1, null, "help");
        tree.Register(def2, new CommandGroup("admin"), "kick");
        tree.Register(def3, new CommandGroup("admin"), "ban");

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
        var tree = CreateTree();

        tree.Root.ShouldNotBeNull();
    }
}
