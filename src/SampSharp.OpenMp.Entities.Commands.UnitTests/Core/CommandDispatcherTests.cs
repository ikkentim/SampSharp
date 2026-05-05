using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Shouldly;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace SampSharp.Entities.SAMP.Commands.Tests.Core;

public class CommandDispatcherTests
{
    private readonly CommandDispatcher _dispatcher = new();
    private readonly CommandRegistry _registry = new();
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcherTests()
    {
        var services = new ServiceCollection();
        _serviceProvider = services.BuildServiceProvider();
    }
    public void Dispatch_NullRegistry_ThrowsArgumentNullException()
    {
        var action = () => _dispatcher.Dispatch(null!, "test", []);
        Should.Throw<ArgumentNullException>(action);
    }

    [Fact]
    public void Dispatch_EmptyInput_ReturnsNotFound()
    {
        var result = _dispatcher.Dispatch(_registry, _serviceProvider, "", [], null);
        result.Response.ShouldBe(DispatchResponse.CommandNotFound);
    }

    [Fact]
    public void Dispatch_WhitespaceInput_ReturnsNotFound()
    {
        var result = _dispatcher.Dispatch(_registry, _serviceProvider, "   ", [], null);
        result.Response.ShouldBe(DispatchResponse.CommandNotFound);
    }

    [Fact]
    public void Dispatch_UnregisteredCommand_ReturnsNotFound()
    {
        var result = _dispatcher.Dispatch(_registry, _serviceProvider, "unknown", [], null);
        result.Response.ShouldBe(DispatchResponse.CommandNotFound);
    }

    [Fact]
    public void Dispatch_RegisteredCommand_ReturnsSuccess()
    {
        var testMethod = typeof(CommandDispatcherTests).GetMethod(
            nameof(TestCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandDispatcherTests), [], TestInvoker, 0);
        var definition = new CommandDefinition("test", null, [overload]);
        _registry.Register(definition);

        var result = _dispatcher.Dispatch(_registry, _serviceProvider, "test", [], null);
        result.Response.ShouldBe(DispatchResponse.Success);
        result.CommandDefinition.ShouldBe(definition);
    }

    [Fact]
    public void Dispatch_CommandWithAlias_FindsByAlias()
    {
        var testMethod = typeof(CommandDispatcherTests).GetMethod(
            nameof(TestCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandDispatcherTests), [], TestInvoker, 0);
        var aliases = new[] { new CommandAlias("t"), new CommandAlias("shorttest") };
        var definition = new CommandDefinition("test", null, [overload], aliases);
        _registry.Register(definition);

        // Should find by alias
        var result = _dispatcher.Dispatch(_registry, _serviceProvider, "t", [], null);
        result.Response.ShouldBe(DispatchResponse.Success);
        result.CommandDefinition.ShouldBe(definition);
    }

    [Fact]
    public void WordParser_BasicTest()
    {
        var parser = new WordParser();
        string input = "3 5";
        var result = parser.TryParse(new ServiceCollection().BuildServiceProvider(), ref input, out var value);
        result.ShouldBeTrue();
        value.ShouldBe("3");
        input.ShouldBe(" 5");
    }

    [Fact]
    public void IntParser_BasicTest()
    {
        var parser = new IntParser();
        string input = "3 5";
        var result = parser.TryParse(new ServiceCollection().BuildServiceProvider(), ref input, out var value);
        result.ShouldBeTrue();
        value.ShouldBe(3);
        input.ShouldBe(" 5");
    }

    [Fact]
    public void TryMatchParameters_WithTwoInts()
    {
        var testMethod = typeof(CommandDispatcherTests).GetMethod(
            nameof(AddCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var parameters = testMethod.GetParameters();
        var paramInfos = new[]
        {
            new CommandParameterInfo("a", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 0),
            new CommandParameterInfo("b", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 1)
        };

        var overload = new CommandOverload(testMethod, parameters, typeof(CommandDispatcherTests), paramInfos, TestInvoker, 0);

        // Call the private TryMatchParameters method via reflection
        var method = typeof(CommandDispatcher).GetMethod(
            "TryMatchParameters",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        method.ShouldNotBeNull("TryMatchParameters method should exist");

        var invokeResult = method!.Invoke(_dispatcher, [overload, "3 5"]);
        invokeResult.ShouldNotBeNull();

        // Cast to tuple
        dynamic result = invokeResult;
        ((bool)result.Item1).ShouldBeTrue($"Should match. Usage: {result.Item2}");
        ((object[])result.Item3).ShouldNotBeNull("Should have parsed arguments");
        ((object[])result.Item3).Length.ShouldBe(2);
        ((object[])result.Item3)[0].ShouldBe(3);
        ((object[])result.Item3)[1].ShouldBe(5);
    }

    [Fact]
    public void RegistryTryFindByPath_WithExtraTokens()
    {
        var testMethod = typeof(CommandDispatcherTests).GetMethod(
            nameof(AddCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var parameters = testMethod.GetParameters();
        var paramInfos = new[]
        {
            new CommandParameterInfo("a", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 0),
            new CommandParameterInfo("b", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 1)
        };

        var overload = new CommandOverload(testMethod, parameters, typeof(CommandDispatcherTests), paramInfos, TestInvoker, 0);
        var definition = new CommandDefinition("add_numbers", null, [overload]);
        _registry.Register(definition);

        // Test 1: Find with just the command name
        var result1 = _registry.TryFindByPath(new List<string> { "add_numbers" });
        result1.ShouldNotBeNull("Should find with single token");

        // Test 2: Find with extra tokens (numbers)
        var result2 = _registry.TryFindByPath(new List<string> { "add_numbers", "3", "5" });
        result2.ShouldNotBeNull("TryFindByPath returned null for path with extra tokens");

        // The question: is it the same command?
        result2!.Name.ShouldBe("add_numbers", "Found command should be 'add_numbers', not something with '3' and '5' in it");
    }

    [Fact]
    public void Dispatch_CommandWithDirectNameAndTwoInts_DebugTokenSplit()
    {
        var testMethod = typeof(CommandDispatcherTests).GetMethod(
            nameof(AddCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var parameters = testMethod.GetParameters();
        var paramInfos = new[]
        {
            new CommandParameterInfo("a", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 0),
            new CommandParameterInfo("b", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 1)
        };

        var overload = new CommandOverload(testMethod, parameters, typeof(CommandDispatcherTests), paramInfos, TestInvoker, 0);
        var definition = new CommandDefinition("add_numbers", null, [overload]);

        // Debug: Check what gets registered
        definition.FullName.ShouldBe("add_numbers");

        _registry.Register(definition);

        // Debug: Try to find it  
        var lookup1 = _registry.TryFind("add_numbers");
        lookup1.ShouldNotBeNull("Should find 'add_numbers' by TryFind");

        var lookup2 = _registry.TryFindByPath(new List<string> { "add_numbers" });
        lookup2.ShouldNotBeNull("Should find 'add_numbers' by TryFindByPath with single token");

        // This test should help us understand what tokens are being split
        const string inputText = "add_numbers 3 5";
        var tokens = inputText.Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries);

        tokens.Length.ShouldBe(3);
        tokens[0].ShouldBe("add_numbers");
        tokens[1].ShouldBe("3");
        tokens[2].ShouldBe("5");

        // Try to find the command using the new overload that returns consumedTokens
        var foundCommand = _registry.TryFindByPath(tokens.ToList(), out var consumedTokens);

        foundCommand.ShouldNotBeNull("Should have found command in registry");
        consumedTokens.ShouldBe(1, $"Should have consumed 1 token, consumed {consumedTokens}");

        // Remaining tokens
        var remainingTokens = tokens.Skip(consumedTokens).ToList();
        var remainingArgs = remainingTokens.Count > 0 ? string.Join(" ", remainingTokens) : "";
        remainingArgs.ShouldBe("3 5", $"Remaining args should be '3 5', got '{remainingArgs}'");
    }

    [Fact]
    public void Dispatch_CommandWithDirectNameAndTwoInts_ParsesSuccessfully()
    {
        var testMethod = typeof(CommandDispatcherTests).GetMethod(
            nameof(AddCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var parameters = testMethod.GetParameters();

        // Verify the method has 2 parameters
        parameters.Length.ShouldBe(2, $"Method should have 2 parameters, found {parameters.Length}");
        parameters[0].ParameterType.ShouldBe(typeof(int), $"First param should be int, got {parameters[0].ParameterType}");
        parameters[1].ParameterType.ShouldBe(typeof(int), $"Second param should be int, got {parameters[1].ParameterType}");

        var paramInfos = new[]
        {
            new CommandParameterInfo("a", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 0),
            new CommandParameterInfo("b", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 1)
        };

        var overload = new CommandOverload(testMethod, parameters, typeof(CommandDispatcherTests), paramInfos, TestInvoker, 0);

        // Verify the overload was created properly
        overload.ParsedParameters.Length.ShouldBe(2, $"Overload should have 2 parsed params, got {overload.ParsedParameters.Length}");
        overload.ParsedParameters[0].Name.ShouldBe("a");
        overload.ParsedParameters[1].Name.ShouldBe("b");

        var definition = new CommandDefinition("add_numbers", null, [overload]);
        _registry.Register(definition);

        // Test that command is registered
        var found = _registry.TryFind("add_numbers");
        found.ShouldNotBeNull("Command should be found in registry");
        found!.Overloads.Count.ShouldBeGreaterThanOrEqualTo(1, $"Should have at least 1 overload, got {found.Overloads.Count}");
        found.Overloads[0].ParsedParameters.Length.ShouldBeGreaterThanOrEqualTo(2, $"Overload should have at least 2 params, got {found.Overloads[0].ParsedParameters.Length}");

        var result = _dispatcher.Dispatch(_registry, _serviceProvider, "add_numbers 3 5", [], null);
        result.Response.ShouldBe(DispatchResponse.Success, $"Expected Success but got {result.Response} with message: {result.UsageMessage}");
        result.ParsedArguments.ShouldNotBeNull("ParsedArguments should not be null");
        result.ParsedArguments!.Length.ShouldBe(2, $"Should have parsed 2 arguments, got {result.ParsedArguments.Length}");
        result.ParsedArguments[0].ShouldBe(3, $"First arg should be 3, got {result.ParsedArguments[0]}");
        result.ParsedArguments[1].ShouldBe(5, $"Second arg should be 5, got {result.ParsedArguments[1]}");
    }

    [Fact]
    public void Dispatch_CommandWithAliasAndTwoInts_ParsesSuccessfully()
    {
        var testMethod = typeof(CommandDispatcherTests).GetMethod(
            nameof(AddCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var parameters = testMethod.GetParameters();
        var paramInfos = new[]
        {
            new CommandParameterInfo("a", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 0),
            new CommandParameterInfo("b", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 1)
        };

        var overload = new CommandOverload(testMethod, parameters, typeof(CommandDispatcherTests), paramInfos, TestInvoker, 0);
        var aliases = new[] { new CommandAlias("add") };
        var definition = new CommandDefinition("add_numbers", null, [overload], aliases);
        _registry.Register(definition);

        // Using alias with parameters
        var result = _dispatcher.Dispatch(_registry, _serviceProvider, "add 3 5", [], null);
        result.Response.ShouldBe(DispatchResponse.Success);
        result.ParsedArguments.ShouldNotBeNull();
        result.ParsedArguments!.Length.ShouldBe(2);
        result.ParsedArguments[0].ShouldBe(3);
        result.ParsedArguments[1].ShouldBe(5);
    }

    [Fact]
    public void Dispatch_MissingRequiredParameter_ReturnsInvalidArguments()
    {
        var testMethod = typeof(CommandDispatcherTests).GetMethod(
            nameof(AddCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var parameters = testMethod.GetParameters();
        var paramInfos = new[]
        {
            new CommandParameterInfo("a", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 0),
            new CommandParameterInfo("b", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 1)
        };

        var overload = new CommandOverload(testMethod, parameters, typeof(CommandDispatcherTests), paramInfos, TestInvoker, 0);
        var definition = new CommandDefinition("add_numbers", null, [overload]);
        _registry.Register(definition);

        // Missing second parameter
        var result = _dispatcher.Dispatch(_registry, _serviceProvider, "add_numbers 3", [], null);
        result.Response.ShouldBe(DispatchResponse.InvalidArguments);
        result.UsageMessage.ShouldNotBeNull().ShouldContain("Usage:");
    }

    [Fact]
    public void Dispatch_InvalidParameterType_ReturnsInvalidArguments()
    {
        var testMethod = typeof(CommandDispatcherTests).GetMethod(
            nameof(AddCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var parameters = testMethod.GetParameters();
        var paramInfos = new[]
        {
            new CommandParameterInfo("a", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 0),
            new CommandParameterInfo("b", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 1)
        };

        var overload = new CommandOverload(testMethod, parameters, typeof(CommandDispatcherTests), paramInfos, TestInvoker, 0);
        var definition = new CommandDefinition("add_numbers", null, [overload]);
        _registry.Register(definition);

        // Non-integer parameter
        var result = _dispatcher.Dispatch(_registry, _serviceProvider, "add_numbers abc def", [], null);
        result.Response.ShouldBe(DispatchResponse.InvalidArguments);
    }

    [Fact]
    public void Dispatch_TooManyParameters_ReturnsInvalidArguments()
    {
        var testMethod = typeof(CommandDispatcherTests).GetMethod(
            nameof(AddCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var parameters = testMethod.GetParameters();
        var paramInfos = new[]
        {
            new CommandParameterInfo("a", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 0),
            new CommandParameterInfo("b", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 1)
        };

        var overload = new CommandOverload(testMethod, parameters, typeof(CommandDispatcherTests), paramInfos, TestInvoker, 0);
        var definition = new CommandDefinition("add_numbers", null, [overload]);
        _registry.Register(definition);

        // Extra parameter
        var result = _dispatcher.Dispatch(_registry, _serviceProvider, "add_numbers 3 5 7", [], null);
        result.Response.ShouldBe(DispatchResponse.InvalidArguments);
    }

    private void TestCommand() { }

    private static object TestInvoker(object target, object[] args, IServiceProvider services, IEntityManager entityManager)
    {
        throw new InvalidOperationException();
    }

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    private void AddCommand(int a, int b) { }
}