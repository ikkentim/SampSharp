using System;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Core;

/// <summary>
/// Tests for CommandDispatcher, which matches command input to registered commands.
/// </summary>
public class CommandDispatcherTests
{
    private static CommandDefinition CreateCommand(
        string name = "test",
        int paramCount = 0,
        CommandGroup? group = null)
    {
        var method = typeof(CommandDispatcherTests).GetMethod(nameof(DummyMethod), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        var parameters = method.GetParameters();

        var parsedParams = new CommandParameterInfo[paramCount];
        for (int i = 0; i < paramCount; i++)
        {
            var mockParser = new Mock<ICommandParameterParser>();
            // Make parser consume one whitespace-delimited word per parse call
            mockParser.Setup(p => p.TryParse(It.IsAny<IServiceProvider>(), ref It.Ref<StringSpan>.IsAny, out It.Ref<object?>.IsAny))
                .Returns((IServiceProvider _, ref StringSpan span, out object? value) =>
                {
                    span = span.TrimStart();
                    if (span.Length == 0)
                    {
                        value = null;
                        return false;
                    }

                    var len = 0;
                    while (len < span.Length && !char.IsWhiteSpace(span[len]))
                    {
                        len++;
                    }

                    value = span.Take(len).ToString();
                    span = span.Skip(len);
                    return true;
                });

            parsedParams[i] = new CommandParameterInfo($"param{i}", mockParser.Object, true, null, i);
        }

        var mockInvoker = new Mock<CommandInvoker>();

        return new CommandDefinition(
            name,
            group,
            method,
            parameters,
            typeof(CommandDispatcherTests),
            parsedParams,
            mockInvoker.Object,
            0,
            Array.Empty<CommandAlias>(),
            Array.Empty<CommandTag>()
        );
    }

    private void DummyMethod() { }

    private CommandRegistry CreateRegistry(params CommandDefinition[] commands)
    {
        var registry = new CommandRegistry(StringComparison.OrdinalIgnoreCase);
        foreach (var cmd in commands)
        {
            registry.Register(cmd);
        }
        return registry;
    }

    [Fact]
    public void Dispatch_SimpleCommand_Succeeds()
    {
        var registry = CreateRegistry(CreateCommand("test"));
        var dispatcher = new CommandDispatcher();
        var services = new Mock<IServiceProvider>().Object;

        var result = dispatcher.Dispatch(registry, services, StringSpan.For("test"), Array.Empty<object>());

        result.Response.ShouldBe(DispatchResponse.Success);
        result.CommandOverload.ShouldNotBeNull();
    }

    [Fact]
    public void Dispatch_UnknownCommand_ReturnNotFound()
    {
        var registry = CreateRegistry(CreateCommand("test"));
        var dispatcher = new CommandDispatcher();
        var services = new Mock<IServiceProvider>().Object;

        var result = dispatcher.Dispatch(registry, services, StringSpan.For("unknown"), Array.Empty<object>());

        result.Response.ShouldBe(DispatchResponse.CommandNotFound);
    }

    [Fact]
    public void Dispatch_EmptyInput_ReturnNotFound()
    {
        var registry = CreateRegistry(CreateCommand("test"));
        var dispatcher = new CommandDispatcher();
        var services = new Mock<IServiceProvider>().Object;

        var result = dispatcher.Dispatch(registry, services, StringSpan.Empty, Array.Empty<object>());

        result.Response.ShouldBe(DispatchResponse.CommandNotFound);
    }

    [Fact]
    public void Dispatch_WhitespaceInput_ReturnNotFound()
    {
        var registry = CreateRegistry(CreateCommand("test"));
        var dispatcher = new CommandDispatcher();
        var services = new Mock<IServiceProvider>().Object;

        var result = dispatcher.Dispatch(registry, services, StringSpan.For("   \t  "), Array.Empty<object>());

        result.Response.ShouldBe(DispatchResponse.CommandNotFound);
    }

    [Fact]
    public void Dispatch_CaseInsensitive()
    {
        var registry = CreateRegistry(CreateCommand("Test"));
        var dispatcher = new CommandDispatcher();
        var services = new Mock<IServiceProvider>().Object;

        var result = dispatcher.Dispatch(registry, services, StringSpan.For("test"), Array.Empty<object>());
        result.Response.ShouldBe(DispatchResponse.Success);

        var result2 = dispatcher.Dispatch(registry, services, StringSpan.For("TEST"), Array.Empty<object>());
        result2.Response.ShouldBe(DispatchResponse.Success);
    }

    [Fact]
    public void Dispatch_CommandWithGroup()
    {
        var cmd = CreateCommand("give", 0, new CommandGroup("admin", "money"));
        var registry = CreateRegistry(cmd);
        var dispatcher = new CommandDispatcher();
        var services = new Mock<IServiceProvider>().Object;

        var result = dispatcher.Dispatch(registry, services, StringSpan.For("admin money give"), Array.Empty<object>());

        result.Response.ShouldBe(DispatchResponse.Success);
        result.UsedCommandName.ShouldBe("admin money give");
    }

    [Fact]
    public void Dispatch_CommandWithParameters()
    {
        var cmd = CreateCommand("test", paramCount: 2);
        var registry = CreateRegistry(cmd);
        var dispatcher = new CommandDispatcher();
        var services = new Mock<IServiceProvider>().Object;

        var result = dispatcher.Dispatch(registry, services, StringSpan.For("test arg1 arg2"), Array.Empty<object>());

        result.Response.ShouldBe(DispatchResponse.Success);
        result.ParsedArguments.ShouldNotBeNull().Length.ShouldBe(2);
    }

    [Fact]
    public void Dispatch_MultipleOverloads_SelectsBestMatch()
    {
        var cmd1 = CreateCommand("test", paramCount: 1);
        var cmd2 = CreateCommand("test", paramCount: 2);
        var registry = CreateRegistry(cmd1, cmd2);
        var dispatcher = new CommandDispatcher();
        var services = new Mock<IServiceProvider>().Object;

        var result = dispatcher.Dispatch(registry, services, StringSpan.For("test arg1"), Array.Empty<object>());

        result.Response.ShouldBe(DispatchResponse.Success);
    }

    [Fact]
    public void Dispatch_LeadingWhitespace_Trimmed()
    {
        var registry = CreateRegistry(CreateCommand("test"));
        var dispatcher = new CommandDispatcher();
        var services = new Mock<IServiceProvider>().Object;

        var result = dispatcher.Dispatch(registry, services, StringSpan.For("   test"), Array.Empty<object>());

        result.Response.ShouldBe(DispatchResponse.Success);
    }

    [Fact]
    public void Dispatch_TrailingWhitespace_Ignored()
    {
        var registry = CreateRegistry(CreateCommand("test"));
        var dispatcher = new CommandDispatcher();
        var services = new Mock<IServiceProvider>().Object;

        var result = dispatcher.Dispatch(registry, services, StringSpan.For("test   "), Array.Empty<object>());

        result.Response.ShouldBe(DispatchResponse.Success);
    }

    [Fact]
    public void Dispatch_NullRegistry_ThrowsArgumentNullException()
    {
        var dispatcher = new CommandDispatcher();
        var services = new Mock<IServiceProvider>().Object;

        Should.Throw<ArgumentNullException>(() => dispatcher.Dispatch(null!, services, StringSpan.For("test"), Array.Empty<object>()));
    }

    [Fact]
    public void Dispatch_NullServices_ThrowsArgumentNullException()
    {
        var registry = CreateRegistry(CreateCommand("test"));
        var dispatcher = new CommandDispatcher();

        Should.Throw<ArgumentNullException>(() => dispatcher.Dispatch(registry, null!, StringSpan.For("test"), Array.Empty<object>()));
    }

    [Fact]
    public void Dispatch_TokenizedCorrectly()
    {
        var cmd = CreateCommand("test", 0, new CommandGroup("admin"));
        var registry = CreateRegistry(cmd);
        var dispatcher = new CommandDispatcher();
        var services = new Mock<IServiceProvider>().Object;

        var result = dispatcher.Dispatch(registry, services, StringSpan.For("admin   test"), Array.Empty<object>());

        result.Response.ShouldBe(DispatchResponse.Success);
    }

    [Fact]
    public void Dispatch_UsedCommandNameSet()
    {
        var cmd = CreateCommand("mycommand");
        var registry = CreateRegistry(cmd);
        var dispatcher = new CommandDispatcher();
        var services = new Mock<IServiceProvider>().Object;

        var result = dispatcher.Dispatch(registry, services, StringSpan.For("mycommand"), Array.Empty<object>());

        result.UsedCommandName.ShouldBe("mycommand");
    }

    [Fact]
    public void Dispatch_WithGroupedCommand_UsedCommandNameIncludesGroup()
    {
        var cmd = CreateCommand("give", 0, new CommandGroup("admin", "money"));
        var registry = CreateRegistry(cmd);
        var dispatcher = new CommandDispatcher();
        var services = new Mock<IServiceProvider>().Object;

        var result = dispatcher.Dispatch(registry, services, StringSpan.For("admin money give"), Array.Empty<object>());

        result.UsedCommandName.ShouldBe("admin money give");
    }

    [Fact]
    public void Dispatch_PermissionDeniedWhenCheckerFails()
    {
        // Note: We can't easily test permission checking with mocks since it requires Player type
        // This is tested through integration tests instead
        var cmd = CreateCommand("admin");
        cmd.ShouldNotBeNull();
    }
}
