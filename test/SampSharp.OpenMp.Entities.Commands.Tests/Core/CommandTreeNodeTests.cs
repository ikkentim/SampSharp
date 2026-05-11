using System;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Core;

/// <summary>
/// Tests for CommandTreeNode, the individual nodes in the command lookup tree.
/// </summary>
public class CommandTreeNodeTests
{
    private static CommandDefinition CreateDefinition(string name)
    {
        var method = typeof(CommandTreeNodeTests).GetMethod(nameof(DummyMethod), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        var mockInvoker = new Mock<CommandInvoker>();
        return new CommandDefinition(
            name, null, method, method.GetParameters(),
            typeof(CommandTreeNodeTests), Array.Empty<CommandParameterInfo>(),
            mockInvoker.Object, 0,
            Array.Empty<CommandAlias>(), Array.Empty<CommandTag>());
    }

    private void DummyMethod() { }

    private static CommandTreeNode CreateNode()
    {
        return new CommandTreeNode(StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Constructor_CommandsIsNullByDefault()
    {
        var node = CreateNode();

        node.Commands.ShouldBeNull();
    }

    [Fact]
    public void Constructor_ChildrenIsEmptyByDefault()
    {
        var node = CreateNode();

        node.Children.ShouldBeEmpty();
    }

    [Fact]
    public void AddCommand_CommandsContainsDefinition()
    {
        var node = CreateNode();
        var def = CreateDefinition("test");

        node.AddCommand(def);

        node.Commands.ShouldNotBeNull();
        node.Commands!.Count.ShouldBe(1);
        node.Commands[0].ShouldBeSameAs(def);
    }

    [Fact]
    public void AddCommand_MultipleOverloads_AllStored()
    {
        var node = CreateNode();
        var def1 = CreateDefinition("test");
        var def2 = CreateDefinition("test");

        node.AddCommand(def1);
        node.AddCommand(def2);

        node.Commands!.Count.ShouldBe(2);
    }

    [Fact]
    public void GetOrCreateChild_CreatesNewChild()
    {
        var node = CreateNode();

        var child = node.GetOrCreateChild("test");

        child.ShouldNotBeNull();
        node.Children.ContainsKey("test").ShouldBeTrue();
    }

    [Fact]
    public void GetOrCreateChild_ReturnsSameChildOnSecondCall()
    {
        var node = CreateNode();

        var child1 = node.GetOrCreateChild("test");
        var child2 = node.GetOrCreateChild("test");

        child1.ShouldBeSameAs(child2);
    }

    [Fact]
    public void GetOrCreateChild_CaseInsensitive()
    {
        var node = CreateNode();

        var child1 = node.GetOrCreateChild("Test");
        var child2 = node.GetOrCreateChild("test");

        child1.ShouldBeSameAs(child2);
    }

    [Fact]
    public void GetOrCreateChild_DifferentWords_CreatesDifferentChildren()
    {
        var node = CreateNode();

        var child1 = node.GetOrCreateChild("kick");
        var child2 = node.GetOrCreateChild("ban");

        child1.ShouldNotBeSameAs(child2);
        node.Children.Count.ShouldBe(2);
    }

    [Fact]
    public void TryGetChild_ExistingChild_ReturnsTrue()
    {
        var node = CreateNode();
        node.GetOrCreateChild("test");

        var found = node.TryGetChild("test", out var child);

        found.ShouldBeTrue();
        child.ShouldNotBeNull();
    }

    [Fact]
    public void TryGetChild_NonExistentChild_ReturnsFalse()
    {
        var node = CreateNode();

        var found = node.TryGetChild("nonexistent", out _);

        found.ShouldBeFalse();
    }

    [Fact]
    public void TryGetChild_CaseInsensitive()
    {
        var node = CreateNode();
        node.GetOrCreateChild("Test");

        node.TryGetChild("test", out _).ShouldBeTrue();
        node.TryGetChild("TEST", out _).ShouldBeTrue();
    }

    [Fact]
    public void Traverse_EmptyInput_ReturnsRoot()
    {
        var node = CreateNode();
        var span = StringSpan.Empty;

        var result = node.Traverse(ref span);

        result.ShouldBeSameAs(node);
        span.Length.ShouldBe(0);
    }

    [Fact]
    public void Traverse_SingleMatchingWord_ReturnsChild()
    {
        var root = CreateNode();
        var child = root.GetOrCreateChild("kick");
        var span = StringSpan.For("kick");

        var result = root.Traverse(ref span);

        result.ShouldBeSameAs(child);
        span.Length.ShouldBe(0);
    }

    [Fact]
    public void Traverse_MultipleMatchingWords_TraversesDeep()
    {
        var root = CreateNode();
        var admin = root.GetOrCreateChild("admin");
        var money = admin.GetOrCreateChild("money");
        var give = money.GetOrCreateChild("give");
        var span = StringSpan.For("admin money give");

        var result = root.Traverse(ref span);

        result.ShouldBeSameAs(give);
        span.Length.ShouldBe(0);
    }

    [Fact]
    public void Traverse_StopsAtUnknownWord()
    {
        var root = CreateNode();
        var admin = root.GetOrCreateChild("admin");
        var span = StringSpan.For("admin unknown");

        var result = root.Traverse(ref span);

        result.ShouldBeSameAs(admin);
        span.TrimStart().ToString().ShouldBe("unknown");
    }

    [Fact]
    public void Traverse_NoMatchingWords_ReturnsRoot()
    {
        var root = CreateNode();
        root.GetOrCreateChild("admin");
        var span = StringSpan.For("unknown");

        var result = root.Traverse(ref span);

        result.ShouldBeSameAs(root);
        span.ToString().ShouldBe("unknown");
    }

    [Fact]
    public void Traverse_MultipleSpacesBetweenWords_StillMatches()
    {
        var root = CreateNode();
        var admin = root.GetOrCreateChild("admin");
        var kick = admin.GetOrCreateChild("kick");
        var span = StringSpan.For("admin   kick");

        var result = root.Traverse(ref span);

        result.ShouldBeSameAs(kick);
        span.Length.ShouldBe(0);
    }

    [Fact]
    public void Traverse_InputWithRemainingArgs_LeavesArgsInSpan()
    {
        var root = CreateNode();
        root.GetOrCreateChild("kick");
        var span = StringSpan.For("kick playerName");

        root.Traverse(ref span);

        span.TrimStart().ToString().ShouldBe("playerName");
    }
}
