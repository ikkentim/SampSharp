using System;
using System.Reflection;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Core;

/// <summary>
/// Tests for CommandDefinition, representing a single command overload.
/// </summary>
public class CommandDefinitionTests
{
    private static CommandParameterInfo[] CreateParamInfo(int count = 0)
    {
        var result = new CommandParameterInfo[count];
        for (int i = 0; i < count; i++)
        {
            var mockParser = new Mock<ICommandParameterParser>();
            result[i] = new CommandParameterInfo($"param{i}", mockParser.Object, true, null, i);
        }
        return result;
    }

    private static CommandDefinition CreateDefinition(
        string name = "test",
        CommandGroup? group = null,
        int paramCount = 0,
        CommandAlias[]? aliases = null)
    {
        var method = typeof(CommandDefinitionTests).GetMethod(nameof(DummyMethod), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        var parameters = method.GetParameters();
        var parsedParams = CreateParamInfo(paramCount);
        var mockInvoker = new Mock<CommandInvoker>();

        return new CommandDefinition(
            name,
            group,
            method,
            parameters,
            typeof(CommandDefinitionTests),
            parsedParams,
            mockInvoker.Object,
            0,
            aliases ?? Array.Empty<CommandAlias>(),
            Array.Empty<CommandTag>()
        );
    }

    private void DummyMethod() { }

    [Fact]
    public void Constructor_WithValidParameters_InitializesCorrectly()
    {
        var def = CreateDefinition("give", new CommandGroup("admin", "money"), 2);

        def.Name.ShouldBe("give");
        def.Group.ShouldBe(new CommandGroup("admin", "money"));
        def.ParsedParameters.Length.ShouldBe(2);
    }

    [Fact]
    public void Constructor_WithNullName_ThrowsArgumentException()
    {
        var method = typeof(CommandDefinitionTests).GetMethod(nameof(DummyMethod), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        var mockInvoker = new Mock<CommandInvoker>();

        Should.Throw<ArgumentException>(() => new CommandDefinition(
            null!,
            null,
            method,
            method.GetParameters(),
            typeof(CommandDefinitionTests),
            Array.Empty<CommandParameterInfo>(),
            mockInvoker.Object,
            0,
            Array.Empty<CommandAlias>(),
            Array.Empty<CommandTag>()
        ));
    }

    [Fact]
    public void Constructor_WithEmptyName_ThrowsArgumentException()
    {
        var method = typeof(CommandDefinitionTests).GetMethod(nameof(DummyMethod), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        var mockInvoker = new Mock<CommandInvoker>();

        Should.Throw<ArgumentException>(() => new CommandDefinition(
            "",
            null,
            method,
            method.GetParameters(),
            typeof(CommandDefinitionTests),
            Array.Empty<CommandParameterInfo>(),
            mockInvoker.Object,
            0,
            Array.Empty<CommandAlias>(),
            Array.Empty<CommandTag>()
        ));
    }

    [Fact]
    public void Constructor_WithNullMethod_ThrowsArgumentNullException()
    {
        var mockInvoker = new Mock<CommandInvoker>();

        Should.Throw<ArgumentNullException>(() => new CommandDefinition(
            "test",
            null,
            null!,
            Array.Empty<ParameterInfo>(),
            typeof(CommandDefinitionTests),
            Array.Empty<CommandParameterInfo>(),
            mockInvoker.Object,
            0,
            Array.Empty<CommandAlias>(),
            Array.Empty<CommandTag>()
        ));
    }

    [Fact]
    public void FullName_WithoutGroup_ReturnsCommandName()
    {
        var def = CreateDefinition("give", null);
        def.FullName.ShouldBe("give");
    }

    [Fact]
    public void FullName_WithGroup_ReturnsGroupAndName()
    {
        var def = CreateDefinition("give", new CommandGroup("admin", "money"));
        def.FullName.ShouldBe("admin money give");
    }

    [Fact]
    public void Aliases_EmptyByDefault()
    {
        var def = CreateDefinition();
        def.Aliases.ShouldBeEmpty();
    }

    [Fact]
    public void Aliases_ReturnsProvidedAliases()
    {
        var aliases = new[] { new CommandAlias("pm"), new CommandAlias("msg") };
        var def = CreateDefinition(aliases: aliases);

        def.Aliases.Count.ShouldBe(2);
        def.Aliases.ShouldContain(new CommandAlias("pm"));
        def.Aliases.ShouldContain(new CommandAlias("msg"));
    }

    [Fact]
    public void Tags_EmptyByDefault()
    {
        var def = CreateDefinition();
        def.Tags.ShouldBeEmpty();
    }

    [Fact]
    public void Tags_ReturnsProvidedTags()
    {
        var tags = new[] { new CommandTag("category", "admin"), new CommandTag("version", "1.0") };
        var method = typeof(CommandDefinitionTests).GetMethod(nameof(DummyMethod), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        var parameters = method.GetParameters();
        var mockInvoker = new Mock<CommandInvoker>();

        var def = new CommandDefinition(
            "test",
            null,
            method,
            parameters,
            typeof(CommandDefinitionTests),
            Array.Empty<CommandParameterInfo>(),
            mockInvoker.Object,
            0,
            Array.Empty<CommandAlias>(),
            tags
        );

        def.Tags.Count.ShouldBe(2);
        def.Tags["category"].ShouldBe("admin");
        def.Tags["version"].ShouldBe("1.0");
    }

    [Fact]
    public void PrefixParameterCount_IsStored()
    {
        var method = typeof(CommandDefinitionTests).GetMethod(nameof(DummyMethod), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        var mockInvoker = new Mock<CommandInvoker>();

        var def = new CommandDefinition(
            "test",
            null,
            method,
            method.GetParameters(),
            typeof(CommandDefinitionTests),
            Array.Empty<CommandParameterInfo>(),
            mockInvoker.Object,
            1,
            Array.Empty<CommandAlias>(),
            Array.Empty<CommandTag>()
        );

        def.PrefixParameterCount.ShouldBe(1);
    }

    [Fact]
    public void DeclaringSystemType_IsStored()
    {
        var def = CreateDefinition();
        def.DeclaringSystemType.ShouldBe(typeof(CommandDefinitionTests));
    }

    [Fact]
    public void ParsedParameters_IsStored()
    {
        var def = CreateDefinition(paramCount: 3);
        def.ParsedParameters.Length.ShouldBe(3);
    }
}
