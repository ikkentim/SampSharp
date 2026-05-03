using System;
using System.Reflection;
using FluentAssertions;
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
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Dispatch_EmptyInput_ReturnsNotFound()
    {
        var result = _dispatcher.Dispatch(_registry, "", new object[0]);
        result.Response.Should().Be(DispatchResponse.CommandNotFound);
    }

    [Fact]
    public void Dispatch_WhitespaceInput_ReturnsNotFound()
    {
        var result = _dispatcher.Dispatch(_registry, "   ", new object[0]);
        result.Response.Should().Be(DispatchResponse.CommandNotFound);
    }

    [Fact]
    public void Dispatch_UnregisteredCommand_ReturnsNotFound()
    {
        var result = _dispatcher.Dispatch(_registry, "unknown", new object[0]);
        result.Response.Should().Be(DispatchResponse.CommandNotFound);
    }

    [Fact]
    public void Dispatch_RegisteredCommand_ReturnsSuccess()
    {
        var testMethod = typeof(CommandDispatcherTests).GetMethod(
            nameof(TestCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, typeof(void), new CommandParameter[0]);
        var definition = new CommandDefinition("test", null, new[] { overload }, null, true, false);
        _registry.Register(definition);

        var result = _dispatcher.Dispatch(_registry, "test", new object[0]);
        result.Response.Should().Be(DispatchResponse.Success);
        result.CommandDefinition.Should().Be(definition);
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
        var result = parser.TryParse(_mockServices.Object, ref _, out var value);
        result.Should().BeTrue();
    }

    [Fact]
    public void BooleanParser_TrueValue_ParsesSuccessfully()
    {
        var parser = new BooleanParser();
        var input = "true";
        var result = parser.TryParse(_mockServices.Object, ref input, out var value);
        result.Should().BeTrue();
        value.Should().Be(true);
    }

    [Fact]
    public void BooleanParser_FalseValue_ParsesSuccessfully()
    {
        var parser = new BooleanParser();
        var input = "false";
        var result = parser.TryParse(_mockServices.Object, ref input, out var value);
        result.Should().BeTrue();
        value.Should().Be(false);
    }

    [Fact]
    public void BooleanParser_InvalidValue_ReturnsFalse()
    {
        var parser = new BooleanParser();
        var input = "invalid";
        var result = parser.TryParse(_mockServices.Object, ref input, out var value);
        result.Should().BeFalse();
    }

    [Fact]
    public void StringParser_AnyText_ParsesSuccessfully()
    {
        var parser = new StringParser();
        var input = "hello world";
        var result = parser.TryParse(_mockServices.Object, ref input, out var value);
        result.Should().BeTrue();
        value.Should().Be("hello world");
    }
}
