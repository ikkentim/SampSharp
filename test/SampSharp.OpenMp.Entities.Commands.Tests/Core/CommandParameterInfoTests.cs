using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Core;

/// <summary>
/// Tests for CommandParameterInfo, which stores metadata about a parsed command parameter.
/// </summary>
public class CommandParameterInfoTests
{
    [Fact]
    public void Constructor_SetsAllProperties()
    {
        var parser = new Mock<ICommandParameterParser>().Object;

        var info = new CommandParameterInfo("amount", parser, isRequired: true, defaultValue: null, parameterIndex: 2);

        info.Name.ShouldBe("amount");
        info.Parser.ShouldBeSameAs(parser);
        info.IsRequired.ShouldBeTrue();
        info.DefaultValue.ShouldBeNull();
        info.ParameterIndex.ShouldBe(2);
    }

    [Fact]
    public void Constructor_OptionalWithDefault_SetsIsRequiredFalse()
    {
        var parser = new Mock<ICommandParameterParser>().Object;

        var info = new CommandParameterInfo("reason", parser, isRequired: false, defaultValue: "none", parameterIndex: 0);

        info.IsRequired.ShouldBeFalse();
        info.DefaultValue.ShouldBe("none");
    }

    [Fact]
    public void Constructor_NullDefaultValue_Allowed()
    {
        var parser = new Mock<ICommandParameterParser>().Object;

        var info = new CommandParameterInfo("param", parser, isRequired: false, defaultValue: null, parameterIndex: 0);

        info.DefaultValue.ShouldBeNull();
    }

    [Fact]
    public void Constructor_IntDefaultValue_Stored()
    {
        var parser = new Mock<ICommandParameterParser>().Object;

        var info = new CommandParameterInfo("count", parser, isRequired: false, defaultValue: 42, parameterIndex: 1);

        info.DefaultValue.ShouldBe(42);
    }

    [Fact]
    public void Name_IsReadOnly()
    {
        var parser = new Mock<ICommandParameterParser>().Object;
        var info = new CommandParameterInfo("myParam", parser, true, null, 0);

        info.Name.ShouldBe("myParam");
    }

    [Fact]
    public void ParameterIndex_IsPreserved()
    {
        var parser = new Mock<ICommandParameterParser>().Object;

        var info = new CommandParameterInfo("p", parser, true, null, 5);

        info.ParameterIndex.ShouldBe(5);
    }

    [Fact]
    public void Parser_IsPreserved()
    {
        var parserMock = new Mock<ICommandParameterParser>();
        var info = new CommandParameterInfo("p", parserMock.Object, true, null, 0);

        info.Parser.ShouldBeSameAs(parserMock.Object);
    }
}
