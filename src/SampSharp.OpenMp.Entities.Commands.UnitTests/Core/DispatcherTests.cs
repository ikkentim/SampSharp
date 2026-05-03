using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Shouldly;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP.Commands.Core;
using SampSharp.Entities.SAMP.Commands.Core.Execution;
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
        var definition = new CommandDefinition("test", null, new[] { overload });
        _registry.Register(definition);

        var result = _dispatcher.Dispatch(_registry, "test", new object[0]);
        result.Response.ShouldBe(DispatchResponse.Success);
        result.CommandDefinition.ShouldBe(definition);
    }

    [Fact]
    public void Dispatch_CommandWithAlias_FindsByAlias()
    {
        var testMethod = typeof(CommandDispatcherTests).GetMethod(
            nameof(TestCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandDispatcherTests), new CommandParameterInfo[0]);
        var aliases = new[] { new CommandAlias("t"), new CommandAlias("shorttest") };
        var definition = new CommandDefinition("test", null, new[] { overload }, aliases);
        _registry.Register(definition);

        // Should find by alias
        var result = _dispatcher.Dispatch(_registry, "t", new object[0]);
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

        var overload = new CommandOverload(testMethod, parameters, typeof(CommandDispatcherTests), paramInfos);

        // Call the private TryMatchParameters method via reflection
        var method = typeof(CommandDispatcher).GetMethod(
            "TryMatchParameters",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        method.ShouldNotBeNull("TryMatchParameters method should exist");

        var invokeResult = method!.Invoke(_dispatcher, new object[] { overload, "3 5" });
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

        var overload = new CommandOverload(testMethod, parameters, typeof(CommandDispatcherTests), paramInfos);
        var definition = new CommandDefinition("add_numbers", null, new[] { overload });
        _registry.Register(definition);

        // Test 1: Find with just the command name
        var result1 = _registry.TryFindByPath(new List<string> { "add_numbers" });
        result1.ShouldNotBeNull("Should find with single token");

        // Test 2: Find with extra tokens (numbers)
        var result2 = _registry.TryFindByPath(new List<string> { "add_numbers", "3", "5" });
        result2.ShouldNotBeNull($"TryFindByPath returned null for path with extra tokens");

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

        var overload = new CommandOverload(testMethod, parameters, typeof(CommandDispatcherTests), paramInfos);
        var definition = new CommandDefinition("add_numbers", null, new[] { overload });

        // Debug: Check what gets registered
        definition.FullName.ShouldBe("add_numbers");

        _registry.Register(definition);

        // Debug: Try to find it  
        var lookup1 = _registry.TryFind("add_numbers");
        lookup1.ShouldNotBeNull("Should find 'add_numbers' by TryFind");

        var lookup2 = _registry.TryFindByPath(new List<string> { "add_numbers" });
        lookup2.ShouldNotBeNull("Should find 'add_numbers' by TryFindByPath with single token");

        // This test should help us understand what tokens are being split
        var inputText = "add_numbers 3 5";
        var tokens = inputText.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        tokens.Length.ShouldBe(3);
        tokens[0].ShouldBe("add_numbers");
        tokens[1].ShouldBe("3");
        tokens[2].ShouldBe("5");

        // Try to find the command using the new overload that returns consumedTokens
        CommandDefinition? foundCommand = null;
        int consumedTokens = 0;

        foundCommand = _registry.TryFindByPath(tokens.ToList(), out consumedTokens);

        foundCommand.ShouldNotBeNull($"Should have found command in registry");
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

        var overload = new CommandOverload(testMethod, parameters, typeof(CommandDispatcherTests), paramInfos);

        // Verify the overload was created properly
        overload.ParsedParameters.Length.ShouldBe(2, $"Overload should have 2 parsed params, got {overload.ParsedParameters.Length}");
        overload.ParsedParameters[0].Name.ShouldBe("a");
        overload.ParsedParameters[1].Name.ShouldBe("b");

        var definition = new CommandDefinition("add_numbers", null, new[] { overload });
        _registry.Register(definition);

        // Test that command is registered
        var found = _registry.TryFind("add_numbers");
        found.ShouldNotBeNull("Command should be found in registry");
        found!.Overloads.Count.ShouldBeGreaterThanOrEqualTo(1, $"Should have at least 1 overload, got {found.Overloads.Count}");
        found.Overloads[0].ParsedParameters.Length.ShouldBeGreaterThanOrEqualTo(2, $"Overload should have at least 2 params, got {found.Overloads[0].ParsedParameters.Length}");

        var result = _dispatcher.Dispatch(_registry, "add_numbers 3 5", new object[0]);
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

        var overload = new CommandOverload(testMethod, parameters, typeof(CommandDispatcherTests), paramInfos);
        var aliases = new[] { new CommandAlias("add") };
        var definition = new CommandDefinition("add_numbers", null, new[] { overload }, aliases);
        _registry.Register(definition);

        // Using alias with parameters
        var result = _dispatcher.Dispatch(_registry, "add 3 5", new object[0]);
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

        var overload = new CommandOverload(testMethod, parameters, typeof(CommandDispatcherTests), paramInfos);
        var definition = new CommandDefinition("add_numbers", null, new[] { overload });
        _registry.Register(definition);

        // Missing second parameter
        var result = _dispatcher.Dispatch(_registry, "add_numbers 3", new object[0]);
        result.Response.ShouldBe(DispatchResponse.InvalidArguments);
        result.UsageMessage.ShouldContain("Usage:");
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

        var overload = new CommandOverload(testMethod, parameters, typeof(CommandDispatcherTests), paramInfos);
        var definition = new CommandDefinition("add_numbers", null, new[] { overload });
        _registry.Register(definition);

        // Non-integer parameter
        var result = _dispatcher.Dispatch(_registry, "add_numbers abc def", new object[0]);
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

        var overload = new CommandOverload(testMethod, parameters, typeof(CommandDispatcherTests), paramInfos);
        var definition = new CommandDefinition("add_numbers", null, new[] { overload });
        _registry.Register(definition);

        // Extra parameter
        var result = _dispatcher.Dispatch(_registry, "add_numbers 3 5 7", new object[0]);
        result.Response.ShouldBe(DispatchResponse.InvalidArguments);
    }

    private void TestCommand() { }

    private void AddCommand(int a, int b) { }
}

public class CommandExecutorTests
{
    private readonly CommandExecutor _executor;
    private readonly Mock<IEntityManager> _mockEntityManager;
    private readonly Mock<IServiceProvider> _mockServices;
    private readonly Mock<ISystem> _mockSystem;

    public CommandExecutorTests()
    {
        _mockEntityManager = new Mock<IEntityManager>();
        _mockServices = new Mock<IServiceProvider>();
        _mockSystem = new Mock<ISystem>();
        _executor = new CommandExecutor(_mockEntityManager.Object);
    }

    [Fact]
    public void Execute_CommandWithNoPrefixParameters_DoesNotIncludePrefixInArgs()
    {
        // Setup: Create a simple overload with NO prefix parameters
        var mockMethod = new Mock<MethodInfo>();
        mockMethod.Setup(m => m.Name).Returns("TestCommand");
        mockMethod.Setup(m => m.ReturnType).Returns(typeof(void));

        var parameters = new ParameterInfo[2];
        var paramInfo1 = new Mock<ParameterInfo>();
        paramInfo1.Setup(p => p.Name).Returns("a");
        paramInfo1.Setup(p => p.ParameterType).Returns(typeof(int));
        parameters[0] = paramInfo1.Object;

        var paramInfo2 = new Mock<ParameterInfo>();
        paramInfo2.Setup(p => p.Name).Returns("b");
        paramInfo2.Setup(p => p.ParameterType).Returns(typeof(int));
        parameters[1] = paramInfo2.Object;

        var parsedParams = new[]
        {
            new CommandParameterInfo("a", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 0),
            new CommandParameterInfo("b", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 1)
        };

        // Track the arguments passed to the invoker
        object?[]? invokerArgs = null;
        MethodInvoker invoker = (target, args, services, entityManager) =>
        {
            invokerArgs = args;
            return null;
        };

        // Create overload with PrefixParameterCount = 0 (NO prefix params expected)
        var overload = new CommandOverload(
            mockMethod.Object,
            parameters,
            typeof(ISystem),
            parsedParams,
            invoker: invoker,
            prefixParameterCount: 0);

        // Execute: Pass extra prefix args (should be ignored)
        var prefixArgs = new object?[] { "extra_prefix_1", "extra_prefix_2" };
        var parsedArgs = new object?[] { 3, 5 };

        _executor.Execute(
            overload,
            prefixArgs,
            parsedArgs,
            _mockServices.Object,
            _mockSystem.Object);

        // Verify: The invoker should receive ONLY the parsed args, not the prefix args
        invokerArgs.ShouldNotBeNull("Invoker should have been called");
        invokerArgs!.Length.ShouldBe(2, "Should have exactly 2 arguments (no prefix)");
        invokerArgs[0].ShouldBe(3, "First argument should be parsed arg");
        invokerArgs[1].ShouldBe(5, "Second argument should be parsed arg");
    }

    [Fact]
    public void Execute_CommandWithPrefixParameter_IncludesOnlyExpectedPrefix()
    {
        // Setup: Create an overload with ONE prefix parameter
        var mockMethod = new Mock<MethodInfo>();
        mockMethod.Setup(m => m.Name).Returns("TestCommand");
        mockMethod.Setup(m => m.ReturnType).Returns(typeof(void));

        var parameters = new ParameterInfo[3];
        var paramInfo1 = new Mock<ParameterInfo>();
        paramInfo1.Setup(p => p.Name).Returns("player");
        paramInfo1.Setup(p => p.ParameterType).Returns(typeof(object));
        parameters[0] = paramInfo1.Object;

        var paramInfo2 = new Mock<ParameterInfo>();
        paramInfo2.Setup(p => p.Name).Returns("a");
        paramInfo2.Setup(p => p.ParameterType).Returns(typeof(int));
        parameters[1] = paramInfo2.Object;

        var paramInfo3 = new Mock<ParameterInfo>();
        paramInfo3.Setup(p => p.Name).Returns("b");
        paramInfo3.Setup(p => p.ParameterType).Returns(typeof(int));
        parameters[2] = paramInfo3.Object;

        var parsedParams = new[]
        {
            new CommandParameterInfo("a", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 1),
            new CommandParameterInfo("b", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 2)
        };

        // Track the arguments passed to the invoker
        object?[]? invokerArgs = null;
        MethodInvoker invoker = (target, args, services, entityManager) =>
        {
            invokerArgs = args;
            return null;
        };

        // Create overload with PrefixParameterCount = 1 (ONE prefix param expected)
        var overload = new CommandOverload(
            mockMethod.Object,
            parameters,
            typeof(ISystem),
            parsedParams,
            invoker: invoker,
            prefixParameterCount: 1);

        // Execute: Pass one prefix arg and two parsed args
        var prefixArgs = new object?[] { "player_obj" };
        var parsedArgs = new object?[] { 3, 5 };

        _executor.Execute(
            overload,
            prefixArgs,
            parsedArgs,
            _mockServices.Object,
            _mockSystem.Object);

        // Verify: The invoker should receive prefix + parsed args
        invokerArgs.ShouldNotBeNull("Invoker should have been called");
        invokerArgs!.Length.ShouldBe(3, "Should have exactly 3 arguments (1 prefix + 2 parsed)");
        invokerArgs[0].ShouldBe("player_obj", "First argument should be prefix arg");
        invokerArgs[1].ShouldBe(3, "Second argument should be first parsed arg");
        invokerArgs[2].ShouldBe(5, "Third argument should be second parsed arg");
    }
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
