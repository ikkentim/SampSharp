using System;
using System.Collections.Generic;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;
using SampSharp.Entities.SAMP;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Services;

/// <summary>
/// Tests for service layer utilities: permission checking and text formatting.
/// </summary>
public class ServiceUtilityTests
{
    #region DefaultPermissionChecker Tests

    [Fact]
    public void DefaultPermissionChecker_AlwaysGrantsPermission()
    {
        var checker = new DefaultPermissionChecker();
        
        // DefaultPermissionChecker always returns true for any input
        // We don't need to mock Player since the checker doesn't use player-specific logic
        var result = checker.HasPermission(null!, null!);
        
        result.ShouldBeTrue();
    }

    [Fact]
    public void DefaultPermissionChecker_WithMultipleCalls_AlwaysGrantsPermission()
    {
        var checker = new DefaultPermissionChecker();
        
        // DefaultPermissionChecker always returns true regardless of input
        for (int i = 0; i < 5; i++)
        {
            var result = checker.HasPermission(null!, null!);
            result.ShouldBeTrue();
        }
    }

    #endregion

    #region DefaultCommandTextFormatter Tests

    [Fact]
    public void DefaultCommandTextFormatter_SimpleCommand_FormattedCorrectly()
    {
        var formatter = new DefaultCommandTextFormatter();
        var result = formatter.FormatCommandUsage("test", null, Array.Empty<CommandParameterInfo>());
        
        result.ShouldBe("/test");
    }

    [Fact]
    public void DefaultCommandTextFormatter_WithoutSlash()
    {
        var formatter = new DefaultCommandTextFormatter();
        var result = formatter.FormatCommandUsage("test", null, Array.Empty<CommandParameterInfo>(), includeSlash: false);
        
        result.ShouldBe("test");
    }

    [Fact]
    public void DefaultCommandTextFormatter_WithGroup()
    {
        var formatter = new DefaultCommandTextFormatter();
        var result = formatter.FormatCommandUsage("give", "admin money", Array.Empty<CommandParameterInfo>());
        
        result.ShouldBe("/admin money give");
    }

    [Fact]
    public void DefaultCommandTextFormatter_WithRequiredParameter()
    {
        var formatter = new DefaultCommandTextFormatter();
        var mockParser = new Mock<ICommandParameterParser>();
        var param = new CommandParameterInfo("amount", mockParser.Object, isRequired: true, null, 0);
        
        var result = formatter.FormatCommandUsage("give", null, new[] { param });
        
        result.ShouldBe("/give <amount>");
    }

    [Fact]
    public void DefaultCommandTextFormatter_WithOptionalParameter()
    {
        var formatter = new DefaultCommandTextFormatter();
        var mockParser = new Mock<ICommandParameterParser>();
        var param = new CommandParameterInfo("reason", mockParser.Object, isRequired: false, "", 0);
        
        var result = formatter.FormatCommandUsage("ban", null, new[] { param });
        
        result.ShouldBe("/ban [reason]");
    }

    [Fact]
    public void DefaultCommandTextFormatter_WithMultipleParameters()
    {
        var formatter = new DefaultCommandTextFormatter();
        var mockParser = new Mock<ICommandParameterParser>();
        var param1 = new CommandParameterInfo("player", mockParser.Object, isRequired: true, null, 0);
        var param2 = new CommandParameterInfo("amount", mockParser.Object, isRequired: true, null, 1);
        var param3 = new CommandParameterInfo("reason", mockParser.Object, isRequired: false, "", 2);
        
        var result = formatter.FormatCommandUsage("give", null, new[] { param1, param2, param3 });
        
        result.ShouldBe("/give <player> <amount> [reason]");
    }

    [Fact]
    public void DefaultCommandTextFormatter_WithGroupAndMultipleParameters()
    {
        var formatter = new DefaultCommandTextFormatter();
        var mockParser = new Mock<ICommandParameterParser>();
        var param1 = new CommandParameterInfo("player", mockParser.Object, isRequired: true, null, 0);
        var param2 = new CommandParameterInfo("amount", mockParser.Object, isRequired: true, null, 1);
        
        var result = formatter.FormatCommandUsage("give", "admin money", new[] { param1, param2 });
        
        result.ShouldBe("/admin money give <player> <amount>");
    }

    [Fact]
    public void DefaultCommandTextFormatter_WithGroupAndNoSlash()
    {
        var formatter = new DefaultCommandTextFormatter();
        var result = formatter.FormatCommandUsage("give", "admin money", Array.Empty<CommandParameterInfo>(), includeSlash: false);
        
        result.ShouldBe("admin money give");
    }

    [Fact]
    public void DefaultCommandTextFormatter_ParameterOrderPreserved()
    {
        var formatter = new DefaultCommandTextFormatter();
        var mockParser = new Mock<ICommandParameterParser>();
        var params_array = new[]
        {
            new CommandParameterInfo("first", mockParser.Object, isRequired: true, null, 0),
            new CommandParameterInfo("second", mockParser.Object, isRequired: true, null, 1),
            new CommandParameterInfo("third", mockParser.Object, isRequired: false, "", 2),
        };
        
        var result = formatter.FormatCommandUsage("cmd", null, params_array);
        
        result.ShouldBe("/cmd <first> <second> [third]");
    }

    [Fact]
    public void DefaultCommandTextFormatter_AllOptionalParameters()
    {
        var formatter = new DefaultCommandTextFormatter();
        var mockParser = new Mock<ICommandParameterParser>();
        var params_array = new[]
        {
            new CommandParameterInfo("opt1", mockParser.Object, isRequired: false, "", 0),
            new CommandParameterInfo("opt2", mockParser.Object, isRequired: false, "", 1),
        };
        
        var result = formatter.FormatCommandUsage("cmd", null, params_array);
        
        result.ShouldBe("/cmd [opt1] [opt2]");
    }

    #endregion
}
