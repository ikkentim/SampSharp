using System.Reflection;
using Moq;
using SampSharp.Entities.Commands;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Commands.Tests.Core;

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
        var mockMatcher = new Mock<CommandComponentMatcher>();

        return new CommandDefinition(
            name,
            group,
            method,
            parameters,
            typeof(CommandDefinitionTests),
            parsedParams,
            mockInvoker.Object,
            0,
            aliases ?? [],
            [],
            mockMatcher.Object
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
        var mockMatcher = new Mock<CommandComponentMatcher>();

        Should.Throw<ArgumentException>(() => new CommandDefinition(
            null!,
            null,
            method,
            method.GetParameters(),
            typeof(CommandDefinitionTests),
            [],
            mockInvoker.Object,
            0,
            [],
            [],
            mockMatcher.Object
        ));
    }

    [Fact]
    public void Constructor_WithEmptyName_ThrowsArgumentException()
    {
        var method = typeof(CommandDefinitionTests).GetMethod(nameof(DummyMethod), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        var mockInvoker = new Mock<CommandInvoker>();
        var mockMatcher = new Mock<CommandComponentMatcher>();

        Should.Throw<ArgumentException>(() => new CommandDefinition(
            "",
            null,
            method,
            method.GetParameters(),
            typeof(CommandDefinitionTests),
            [],
            mockInvoker.Object,
            0,
            [],
            [],
            mockMatcher.Object
        ));
    }

    [Fact]
    public void Constructor_WithNullMethod_ThrowsArgumentNullException()
    {
        var mockInvoker = new Mock<CommandInvoker>();
        var mockMatcher = new Mock<CommandComponentMatcher>();

        Should.Throw<ArgumentNullException>(() => new CommandDefinition(
            "test",
            null,
            null!,
            [],
            typeof(CommandDefinitionTests),
            [],
            mockInvoker.Object,
            0,
            [],
            [],
            mockMatcher.Object
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
        var mockMatcher = new Mock<CommandComponentMatcher>();

        var def = new CommandDefinition(
            "test",
            null,
            method,
            parameters,
            typeof(CommandDefinitionTests),
            [],
            mockInvoker.Object,
            0,
            [],
            tags,
            mockMatcher.Object
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
        var mockMatcher = new Mock<CommandComponentMatcher>();

        var def = new CommandDefinition(
            "test",
            null,
            method,
            method.GetParameters(),
            typeof(CommandDefinitionTests),
            [],
            mockInvoker.Object,
            1,
            [],
            [],
            mockMatcher.Object
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
