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
    private static CommandSet CreateCommandSet(string name)
    {
        var method = typeof(CommandTreeNodeTests).GetMethod(nameof(DummyMethod), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        var mockInvoker = new Mock<CommandInvoker>();
        var definition = new CommandDefinition(
            name, null, method, method.GetParameters(),
            typeof(CommandTreeNodeTests), Array.Empty<CommandParameterInfo>(),
            mockInvoker.Object, 0,
            Array.Empty<CommandAlias>(), Array.Empty<CommandTag>());

        return new CommandSet(name, null, [definition]);
    }

    private void DummyMethod() { }

    [Fact]
    public void Constructor_CommandSetIsNullByDefault()
    {
        var node = new CommandTreeNode();

        node.CommandSet.ShouldBeNull();
    }

    [Fact]
    public void Constructor_ChildrenIsEmptyByDefault()
    {
        var node = new CommandTreeNode();

        node.Children.ShouldBeEmpty();
    }

    [Fact]
    public void CommandSet_CanBeSet()
    {
        var node = new CommandTreeNode();
        var commandSet = CreateCommandSet("test");

        node.CommandSet = commandSet;

        node.CommandSet.ShouldBeSameAs(commandSet);
    }

    [Fact]
    public void GetOrCreateChild_CreatesNewChild()
    {
        var node = new CommandTreeNode();

        var child = node.GetOrCreateChild("test");

        child.ShouldNotBeNull();
        node.Children.ContainsKey("test").ShouldBeTrue();
    }

    [Fact]
    public void GetOrCreateChild_ReturnsSameChildOnSecondCall()
    {
        var node = new CommandTreeNode();

        var child1 = node.GetOrCreateChild("test");
        var child2 = node.GetOrCreateChild("test");

        child1.ShouldBeSameAs(child2);
    }

    [Fact]
    public void GetOrCreateChild_CaseInsensitive()
    {
        var node = new CommandTreeNode();

        var child1 = node.GetOrCreateChild("Test");
        var child2 = node.GetOrCreateChild("test");

        child1.ShouldBeSameAs(child2);
    }

    [Fact]
    public void GetOrCreateChild_DifferentWords_CreatesDifferentChildren()
    {
        var node = new CommandTreeNode();

        var child1 = node.GetOrCreateChild("kick");
        var child2 = node.GetOrCreateChild("ban");

        child1.ShouldNotBeSameAs(child2);
        node.Children.Count.ShouldBe(2);
    }

    [Fact]
    public void TryGetChild_ExistingChild_ReturnsTrue()
    {
        var node = new CommandTreeNode();
        node.GetOrCreateChild("test");

        var found = node.TryGetChild("test", out var child);

        found.ShouldBeTrue();
        child.ShouldNotBeNull();
    }

    [Fact]
    public void TryGetChild_NonExistentChild_ReturnsFalse()
    {
        var node = new CommandTreeNode();

        var found = node.TryGetChild("nonexistent", out _);

        found.ShouldBeFalse();
    }

    [Fact]
    public void TryGetChild_CaseInsensitive()
    {
        var node = new CommandTreeNode();
        node.GetOrCreateChild("Test");

        node.TryGetChild("test", out _).ShouldBeTrue();
        node.TryGetChild("TEST", out _).ShouldBeTrue();
    }

    [Fact]
    public void Traverse_EmptyPath_ReturnsRoot()
    {
        var node = new CommandTreeNode();

        var result = node.Traverse([], out var consumed);

        result.ShouldBeSameAs(node);
        consumed.ShouldBe(0);
    }

    [Fact]
    public void Traverse_SingleMatchingSegment_ReturnsChild()
    {
        var root = new CommandTreeNode();
        var child = root.GetOrCreateChild("kick");

        var result = root.Traverse(["kick"], out var consumed);

        result.ShouldBeSameAs(child);
        consumed.ShouldBe(1);
    }

    [Fact]
    public void Traverse_MultipleMatchingSegments_TraversesDeep()
    {
        var root = new CommandTreeNode();
        var admin = root.GetOrCreateChild("admin");
        var money = admin.GetOrCreateChild("money");
        var give = money.GetOrCreateChild("give");

        var result = root.Traverse(["admin", "money", "give"], out var consumed);

        result.ShouldBeSameAs(give);
        consumed.ShouldBe(3);
    }

    [Fact]
    public void Traverse_StopsAtUnknownSegment()
    {
        var root = new CommandTreeNode();
        var admin = root.GetOrCreateChild("admin");

        var result = root.Traverse(["admin", "unknown"], out var consumed);

        result.ShouldBeSameAs(admin);
        consumed.ShouldBe(1);
    }

    [Fact]
    public void Traverse_NoMatchingSegments_ReturnsRoot()
    {
        var root = new CommandTreeNode();
        root.GetOrCreateChild("admin");

        var result = root.Traverse(["unknown"], out var consumed);

        result.ShouldBeSameAs(root);
        consumed.ShouldBe(0);
    }
}
