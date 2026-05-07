using System;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Services;

public class DefaultCommandTextFormatterTests
{
    private readonly DefaultCommandTextFormatter _formatter = new();

    [Fact]
    public void DefaultCommandTextFormatter_SimpleCommand_FormattedCorrectly()
    {
        var result = _formatter.FormatCommandUsage("test", null, Array.Empty<CommandParameterInfo>());

        result.ShouldBe("/test");
    }

    [Fact]
    public void DefaultCommandTextFormatter_WithoutSlash()
    {
        var result = _formatter.FormatCommandUsage("test", null, Array.Empty<CommandParameterInfo>(), includeSlash: false);

        result.ShouldBe("test");
    }

    [Fact]
    public void DefaultCommandTextFormatter_WithGroup()
    {
        var result = _formatter.FormatCommandUsage("give", "admin money", Array.Empty<CommandParameterInfo>());

        result.ShouldBe("/admin money give");
    }

    [Fact]
    public void DefaultCommandTextFormatter_WithRequiredParameter()
    {
        var mockParser = new Mock<ICommandParameterParser>();
        var param = new CommandParameterInfo("amount", mockParser.Object, isRequired: true, null, 0);

        var result = _formatter.FormatCommandUsage("give", null, new[] { param });

        result.ShouldBe("/give <amount>");
    }

    [Fact]
    public void DefaultCommandTextFormatter_WithOptionalParameter()
    {
        var mockParser = new Mock<ICommandParameterParser>();
        var param = new CommandParameterInfo("reason", mockParser.Object, isRequired: false, "", 0);

        var result = _formatter.FormatCommandUsage("ban", null, new[] { param });

        result.ShouldBe("/ban [reason]");
    }

    [Fact]
    public void DefaultCommandTextFormatter_WithMultipleParameters()
    {
        var mockParser = new Mock<ICommandParameterParser>();
        var param1 = new CommandParameterInfo("player", mockParser.Object, isRequired: true, null, 0);
        var param2 = new CommandParameterInfo("amount", mockParser.Object, isRequired: true, null, 1);
        var param3 = new CommandParameterInfo("reason", mockParser.Object, isRequired: false, "", 2);

        var result = _formatter.FormatCommandUsage("give", null, new[] { param1, param2, param3 });

        result.ShouldBe("/give <player> <amount> [reason]");
    }

    [Fact]
    public void DefaultCommandTextFormatter_WithGroupAndMultipleParameters()
    {
        var mockParser = new Mock<ICommandParameterParser>();
        var param1 = new CommandParameterInfo("player", mockParser.Object, isRequired: true, null, 0);
        var param2 = new CommandParameterInfo("amount", mockParser.Object, isRequired: true, null, 1);

        var result = _formatter.FormatCommandUsage("give", "admin money", new[] { param1, param2 });

        result.ShouldBe("/admin money give <player> <amount>");
    }

    [Fact]
    public void DefaultCommandTextFormatter_WithGroupAndNoSlash()
    {
        var result = _formatter.FormatCommandUsage("give", "admin money", Array.Empty<CommandParameterInfo>(), includeSlash: false);

        result.ShouldBe("admin money give");
    }

    [Fact]
    public void DefaultCommandTextFormatter_ParameterOrderPreserved()
    {
        var mockParser = new Mock<ICommandParameterParser>();
        var params_array = new[]
        {
            new CommandParameterInfo("first", mockParser.Object, isRequired: true, null, 0),
            new CommandParameterInfo("second", mockParser.Object, isRequired: true, null, 1),
            new CommandParameterInfo("third", mockParser.Object, isRequired: false, "", 2),
        };

        var result = _formatter.FormatCommandUsage("cmd", null, params_array);

        result.ShouldBe("/cmd <first> <second> [third]");
    }

    [Fact]
    public void DefaultCommandTextFormatter_AllOptionalParameters()
    {
        var mockParser = new Mock<ICommandParameterParser>();
        var params_array = new[]
        {
            new CommandParameterInfo("opt1", mockParser.Object, isRequired: false, "", 0),
            new CommandParameterInfo("opt2", mockParser.Object, isRequired: false, "", 1),
        };

        var result = _formatter.FormatCommandUsage("cmd", null, params_array);

        result.ShouldBe("/cmd [opt1] [opt2]");
    }
}
