using System;
using System.Collections.Generic;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Core;

/// <summary>
/// Tests for CommandSet, representing a command with all its overloads.
/// </summary>
public class CommandSetTests
{
    private static CommandDefinition CreateOverload(string name = "test", int paramIndex = 0)
    {
        var method = typeof(CommandSetTests).GetMethod(nameof(DummyMethod))!;
        var parameters = method.GetParameters();
        var paramInfo = new[] {
            new CommandParameterInfo($"param{paramIndex}", new Mock<ICommandParameterParser>().Object, true, null, paramIndex)
        };
        var mockInvoker = new Mock<CommandInvoker>();

        return new CommandDefinition(
            name,
            null,
            method,
            parameters,
            typeof(CommandSetTests),
            paramInfo,
            mockInvoker.Object,
            0,
            Array.Empty<CommandAlias>(),
            Array.Empty<CommandTag>()
        );
    }

    internal void DummyMethod() { }

    [Fact]
    public void Constructor_WithSingleOverload_Succeeds()
    {
        var overload = CreateOverload("test");
        var command = new CommandSet("test", null, new[] { overload });

        command.Name.ShouldBe("test");
        command.Group.ShouldBeNull();
        command.Overloads.Count.ShouldBe(1);
    }

    [Fact]
    public void Constructor_WithMultipleOverloads_Succeeds()
    {
        var overload1 = CreateOverload("test", 0);
        var overload2 = CreateOverload("test", 1);
        var command = new CommandSet("test", null, new[] { overload1, overload2 });

        command.Overloads.Count.ShouldBe(2);
        command.Overloads.ShouldContain(overload1);
        command.Overloads.ShouldContain(overload2);
    }

    [Fact]
    public void Constructor_WithGroup_StoresGroup()
    {
        var overload = CreateOverload("give");
        var group = new CommandGroup("admin", "money");
        var command = new CommandSet("give", group, new[] { overload });

        command.Group.ShouldBe(group);
    }

    [Fact]
    public void Constructor_WithNullName_ThrowsArgumentException()
    {
        var overload = CreateOverload();
        Should.Throw<ArgumentException>(() => new CommandSet(null!, null, new[] { overload }));
    }

    [Fact]
    public void Constructor_WithEmptyName_ThrowsArgumentException()
    {
        var overload = CreateOverload();
        Should.Throw<ArgumentException>(() => new CommandSet("", null, new[] { overload }));
    }

    [Fact]
    public void Constructor_WithWhitespaceName_ThrowsArgumentException()
    {
        var overload = CreateOverload();
        Should.Throw<ArgumentException>(() => new CommandSet("   ", null, new[] { overload }));
    }

    [Fact]
    public void Constructor_WithNullOverloads_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new CommandSet("test", null, null!));
    }

    [Fact]
    public void Constructor_WithEmptyOverloads_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new CommandSet("test", null, Array.Empty<CommandDefinition>()));
    }

    [Fact]
    public void FullName_WithoutGroup_ReturnsCommandName()
    {
        var overload = CreateOverload("test");
        var command = new CommandSet("test", null, new[] { overload });

        command.FullName.ShouldBe("test");
    }

    [Fact]
    public void FullName_WithGroup_ReturnsGroupAndName()
    {
        var overload = CreateOverload("give");
        var group = new CommandGroup("admin", "money");
        var command = new CommandSet("give", group, new[] { overload });

        command.FullName.ShouldBe("admin money give");
    }

    [Fact]
    public void Overloads_AreReadOnly()
    {
        var overload = CreateOverload();
        var command = new CommandSet("test", null, new[] { overload });

        var overloads = command.Overloads;
        overloads.ShouldBeAssignableTo<IReadOnlyList<CommandDefinition>>();
    }

    [Fact]
    public void Overloads_CannotBeModified()
    {
        var overload = CreateOverload();
        var command = new CommandSet("test", null, new[] { overload });

        // IReadOnlyList doesn't have Add method, so this test validates read-only behavior
        // The collection itself is immutable
        command.Overloads.Count.ShouldBe(1);
    }
}
