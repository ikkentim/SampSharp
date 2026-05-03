using System;
using System.Reflection;
using Shouldly;
using Moq;
using SampSharp.Entities.SAMP.Commands.Core;
using SampSharp.Entities.SAMP.Commands.Core.Scanning;
using SampSharp.Entities.SAMP.Commands.Parsers;
using Xunit;

namespace SampSharp.Entities.SAMP.Commands.Tests.Core;

public class CommandDispatcherTests
{
    private readonly CommandDispatcher _dispatcher;
    private readonly CommandRegistry _registry;

    public CommandDispatcherTests()
    {
        _dispatcher = new CommandDispatcher();
        _registry = new CommandRegistry();
    }

    [Fact]
    public void Dispatch_NullRegistry_ThrowsArgumentNullException()
    {
        var action = () => _dispatcher.Dispatch(null!, "test", new object[0]);
        Should.Throw<ArgumentNullException>(action);
    }

    [Fact]
    public void Dispatch_EmptyInput_ReturnsNotFound()
    {
        var result = _dispatcher.Dispatch(_registry, "", new object[0]);
        result.Response.ShouldBe(DispatchResponse.CommandNotFound);
    }

    [Fact]
    public void Dispatch_WhitespaceInput_ReturnsNotFound()
    {
        var result = _dispatcher.Dispatch(_registry, "   ", new object[0]);
        result.Response.ShouldBe(DispatchResponse.CommandNotFound);
    }

    [Fact]
    public void Dispatch_UnregisteredCommand_ReturnsNotFound()
    {
        var result = _dispatcher.Dispatch(_registry, "unknown", new object[0]);
        result.Response.ShouldBe(DispatchResponse.CommandNotFound);
    }

    [Fact]
    public void Dispatch_RegisteredCommand_ReturnsSuccess()
    {
        var testMethod = typeof(CommandDispatcherTests).GetMethod(
            nameof(TestCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandDispatcherTests), new CommandParameterInfo[0]);
        var definition = new CommandDefinition("test", null, new[] { overload }, null, null, true, false);
        _registry.Register(definition);

        var result = _dispatcher.Dispatch(_registry, "test", new object[0]);
        result.Response.ShouldBe(DispatchResponse.Success);
        result.CommandDefinition.ShouldBe(definition);
    }

    private void TestCommand() { }
}

public class ParameterParserTests
{
    private readonly Mock<IServiceProvider> _mockServices = new();

    public ParameterParserTests()
    {
        _mockServices = new Mock<IServiceProvider>();
    }

    [Fact]
    public void IntParser_ValidNumber_ParsesSuccessfully()
    {
        var parser = new IntParser();
        var input = "42";
        var result = parser.TryParse(_mockServices.Object, ref input, out var value);
        result.ShouldBeTrue();
        value.ShouldBe(42);
    }

    [Fact]
    public void BooleanParser_TrueValue_ParsesSuccessfully()
    {
        var parser = new BooleanParser();
        var input = "true";
        var result = parser.TryParse(_mockServices.Object, ref input, out var value);
        result.ShouldBeTrue();
        value.ShouldBe(true);
    }

    [Fact]
    public void BooleanParser_FalseValue_ParsesSuccessfully()
    {
        var parser = new BooleanParser();
        var input = "false";
        var result = parser.TryParse(_mockServices.Object, ref input, out var value);
        result.ShouldBeTrue();
        value.ShouldBe(false);
    }

    [Fact]
    public void BooleanParser_InvalidValue_ReturnsFalse()
    {
        var parser = new BooleanParser();
        var input = "invalid";
        var result = parser.TryParse(_mockServices.Object, ref input, out var value);
        result.ShouldBeFalse();
    }

    [Fact]
    public void StringParser_AnyText_ParsesSuccessfully()
    {
        var parser = new StringParser();
        var input = "hello world";
        var result = parser.TryParse(_mockServices.Object, ref input, out var value);
        result.ShouldBeTrue();
        value.ShouldBe("hello world");
    }
}
