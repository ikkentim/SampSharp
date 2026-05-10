using System;
using System.Collections.Generic;
using System.Reflection;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Console;

/// <summary>
/// Tests for DefaultConsoleCommandMessageService, which formats and sends usage messages to the console.
/// </summary>
public class DefaultConsoleCommandMessageServiceTests
{
    private static CommandDefinition CreateDefinition(string name, CommandParameterInfo[]? parsedParams = null)
    {
        var method = typeof(DefaultConsoleCommandMessageServiceTests).GetMethod(nameof(DummyMethod), BindingFlags.Instance | BindingFlags.NonPublic)!;
        var mockInvoker = new Mock<CommandInvoker>();

        return new CommandDefinition(
            name, null, method, method.GetParameters(),
            typeof(DefaultConsoleCommandMessageServiceTests),
            parsedParams ?? Array.Empty<CommandParameterInfo>(),
            mockInvoker.Object, 0,
            Array.Empty<CommandAlias>(), Array.Empty<CommandTag>());
    }

    private void DummyMethod() { }

    [Fact]
    public void Constructor_NullFormatter_ThrowsArgumentNullException()
    {
        Should.Throw<ArgumentNullException>(() => new DefaultConsoleCommandMessageService(null!));
    }

    [Fact]
    public void SendUsage_SingleOverload_SendsFormattedMessage()
    {
        var messages = new List<string>();
        var context = new ConsoleCommandDispatchContext(null, messages.Add);
        var formatterMock = new Mock<ICommandTextFormatter>();
        formatterMock.Setup(f => f.FormatCommandUsage(It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<CommandParameterInfo[]>(), It.IsAny<bool>()))
            .Returns("test <amount>");

        var service = new DefaultConsoleCommandMessageService(formatterMock.Object);
        var overload = CreateDefinition("test");

        service.SendUsage(context, new[] { overload });

        messages.Count.ShouldBe(1);
        messages[0].ShouldContain("test <amount>");
    }

    [Fact]
    public void SendUsage_MultipleOverloads_SendsHeaderAndEachOverload()
    {
        var messages = new List<string>();
        var context = new ConsoleCommandDispatchContext(null, messages.Add);
        var formatterMock = new Mock<ICommandTextFormatter>();
        formatterMock.Setup(f => f.FormatCommandUsage(It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<CommandParameterInfo[]>(), It.IsAny<bool>()))
            .Returns("formatted");

        var service = new DefaultConsoleCommandMessageService(formatterMock.Object);
        var overload1 = CreateDefinition("test");
        var overload2 = CreateDefinition("test");

        service.SendUsage(context, new[] { overload1, overload2 });

        // Header + 2 overload lines
        messages.Count.ShouldBe(3);
        messages[0].ShouldBe("Usage:");
    }

    [Fact]
    public void SendUsage_WithUsedCommandName_UsesAliasName()
    {
        string? capturedName = null;
        var context = new ConsoleCommandDispatchContext(null, _ => { });
        var formatterMock = new Mock<ICommandTextFormatter>();
        formatterMock.Setup(f => f.FormatCommandUsage(It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<CommandParameterInfo[]>(), It.IsAny<bool>()))
            .Callback((string name, string? group, CommandParameterInfo[] _, bool _) => capturedName = name)
            .Returns("formatted");

        var service = new DefaultConsoleCommandMessageService(formatterMock.Object);
        var overload = CreateDefinition("message");

        service.SendUsage(context, new[] { overload }, usedCommandName: "pm");

        capturedName.ShouldBe("pm");
    }

    [Fact]
    public void SendUsage_SingleOverload_DoesNotIncludeSlash()
    {
        bool? capturedIncludeSlash = null;
        var context = new ConsoleCommandDispatchContext(null, _ => { });
        var formatterMock = new Mock<ICommandTextFormatter>();
        formatterMock.Setup(f => f.FormatCommandUsage(It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<CommandParameterInfo[]>(), It.IsAny<bool>()))
            .Callback((string _, string? _, CommandParameterInfo[] _, bool includeSlash) => capturedIncludeSlash = includeSlash)
            .Returns("formatted");

        var service = new DefaultConsoleCommandMessageService(formatterMock.Object);
        var overload = CreateDefinition("test");

        service.SendUsage(context, new[] { overload });

        capturedIncludeSlash.ShouldBe(false);
    }

    [Fact]
    public void SendUsage_ReturnsTrue()
    {
        var context = new ConsoleCommandDispatchContext(null, _ => { });
        var formatterMock = new Mock<ICommandTextFormatter>();
        formatterMock.Setup(f => f.FormatCommandUsage(It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<CommandParameterInfo[]>(), It.IsAny<bool>()))
            .Returns("formatted");

        var service = new DefaultConsoleCommandMessageService(formatterMock.Object);
        var overload = CreateDefinition("test");

        var result = service.SendUsage(context, new[] { overload });

        result.ShouldBeTrue();
    }
}
